using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class SaleOutOrder : ITableModel
    {
        private bool m_isRedBill = false;
        private SortedDictionary<int, SaleOutOrderTable> m_tableDataList = new SortedDictionary<int, SaleOutOrderTable>();

        static private SaleOutOrder m_instance = null;

        private SaleOutOrder()
        {
            load();
        }

        static public SaleOutOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SaleOutOrder();
            }

            return m_instance;
        }

        public void insert(SaleOutOrderTable record, bool isDisplayMessageBox = true)
        {
            bool isExistReview = false;
            SaleOutOrderTable oldRecord = new SaleOutOrderTable();

            string insert = "INSERT INTO [dbo].[SALE_OUT_ORDER]([CUSTOMER_ID],[TRADING_DATE],[BILL_NUMBER],[SALE_TYPE],";
            insert += "[PAYMENT_DATE],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],[TOTAL_MONEY],";
            insert += "[BUSINESS_PEOPLE_ID],[MAKE_ORDER_STAFF],[SOURCE_BILL_TYPE],[SOURCE_BILL_NUMBER],[STAFF_SAVE_ID],";
            insert += "[STAFF_CHECK_ID],[RECEIVED_OK],[RECEIVED_NO_OK], [IS_RED_BILL], [IS_IN_LEDGER]";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                if (checkBillIsReview(record.billNumber))
                {
                    isExistReview = true;
                    insert += ",[ORDERR_REVIEW], [REVIEW_DATE], [IS_REVIEW] ";
                    oldRecord = getSaleInfoFromBillNumber(record.billNumber);
                }

                delete(record.billNumber);
            }

            insert += ") VALUES(";

            insert += record.customerId + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.saleType + "',";
            insert += "'" + record.paymentDate + "',";
            insert += "'" + record.exchangesUnit + "',";
            insert += "'" + record.sumValue + "',";
            insert += "'" + record.sumMoney + "',";
            insert += "'" + record.sumTransportationCost + "',";
            insert += "'" + record.sumOtherCost + "',";
            insert += "'" + record.totalMoney + "',";
            insert += record.businessPeopleId + ",";
            insert += record.makeOrderStaff + ",";
            insert += "'" + record.sourceBillType + "',";
            insert += "'" + record.sourceBillNumber + "',";
            insert += record.staffSaveId + ",";
            insert += record.staffCheckId + ",";
            
            // 已付款金额默认为0
            insert += "0,"; 

            // 未付款金额为总金额
            insert += record.totalMoney + ",";

            // 红字蓝字标示和是否入账标示(默认未入账)
            insert += record.isRedBill + ", 0";

            // 如果单据已经审核过，把之前的审核信息写到该记录中
            if (isExistReview)
            {
                insert += ",";
                insert += oldRecord.orderrReview + ",";
                insert += "'" + oldRecord.reviewDate + "', 1";
            }

            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                if (isDisplayMessageBox)
                {
                    MessageBoxExtend.messageOK("数据保存成功");
                }

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(203, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string billNumber)
        {
            string delete = "DELETE FROM SALE_OUT_ORDER WHERE BILL_NUMBER = '" + billNumber + "'"; 

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, delete);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void billReview(string billNumber)
        {
             // 更新单据审核标志，把审核标志置为1
            string update = "UPDATE [dbo].[SALE_OUT_ORDER] SET ";

            update += "[ORDERR_REVIEW] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "REVIEW_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_REVIEW = 1";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                load();

                MessageBoxExtend.messageOK("单据[" + billNumber + "]审核成功");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(203, OperatorLogType.Review, billNumber);
        }

        public void registerInLedger(string billNumber, bool isRedBill)
        {
            /*函数处理逻辑如下：
             * 1、更新单据是否入账标示，把标示修改为1(已入账)
             * 1、如果采购方式为赊购并且发生了运输费用或其他费用，询问用户是否把运输费用或者其他费用计入应付账款
             * 2、根据采购单详细信息，更新库存表，如果发生了运输费用或其他费用，需要把运输费用和其他费用计入物料单价
             * 3、如果单据是根据采购订单生成，更新对应采购订单中的实际入库数量
             */

            writeOperatorLog(203, OperatorLogType.Register, billNumber, "开始");

            m_isRedBill = isRedBill;

            // 更新库存表
            if (updateMaterielData(billNumber))
            {
                // 更新记账标示
                string update = "UPDATE [dbo].[SALE_OUT_ORDER] SET ";

                update += "[ORDERR_IN_LEDGER] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
                update += "IN_LEDGER_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_IN_LEDGER = 1";
                update += " WHERE BILL_NUMBER = '" + billNumber + "'";

                try
                {
                    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                    load();

                    MessageBoxExtend.messageOK("单据[" + billNumber + "]入账成功");
                }
                catch (Exception error)
                {
                    MessageBoxExtend.messageWarning(error.Message);
                    return;
                }

                // 更新应收账款
                updateAccountReceivableData(billNumber);

                // 如果本张销售出库单是根据某销售订单生成，需要更新销售订单的实际入库数量字段
                updataActualValue(billNumber);
            }

            writeOperatorLog(203, OperatorLogType.Register, billNumber, "结束");
        }

        public void updataReceivedInfo(string billNumber, double paymentOK)
        {
            SaleOutOrderTable record = getSaleInfoFromBillNumber(billNumber);
            double paymentNoOK = Convert.ToDouble(record.totalMoney.ToString()) - paymentOK;

            string update = "UPDATE [dbo].[SALE_OUT_ORDER] SET ";
            update += "RECEIVED_OK = " + paymentOK + ", ";
            update += "RECEIVED_NO_OK = " + paymentNoOK;
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        private void updateAccountReceivableData(string billNumber)
        {
            SaleOutOrderTable accountReceivableData = getSaleInfoFromBillNumber(billNumber);

            if(accountReceivableData.saleType == "赊购")
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();

                // 应收款金额
                double turnover = Convert.ToDouble(accountReceivableData.sumMoney.ToString());

                string message = "单据 [" + accountReceivableData.billNumber + "] 销售类型为赊购，";
                message += "系统会自动产生一笔应收账款信息记录到客户 [" + accountReceivableData.customerName + "] 账户下，";
                message += "请确实应收账款金额，然后点击【确定】按钮(如需调整金额，请点击调整【金额调整】按钮,再点击【确定】)";

                FormAdjustAmount faa = new FormAdjustAmount("应收账款金额确认", message, accountReceivableData.totalMoney);
                faa.ShowDialog();
                turnover = faa.getAmount();

                if (!m_isRedBill)
                {
                    if (!InitAccountReceivable.getInctance().checkcustomerOrSupplierIDIsExist(accountReceivableData.customerId))
                    {
                        /* 说明
                         * 系统应收应收账款的汇总表和详细的收款还款记录是分开存放，并且默认情况下会相互更新
                         * 比如正常的逻辑(用于应收应收胀库初始化模块)：
                         *      当在应收账款汇总表插入一条记录的时候，程序会自动插入一条期初数据到应收账款详细表
                         *      当有一笔交易更新了应收账款表时，程序会自动查询应收账款统计表，更新对应客户的欠款余额
                         * 此处的逻辑如下：             
                         *      当发生销售出库赊购业务时候，自动产生一条应收账款信息
                         *      这里需要首先查询下系统中是否存在跟该客户的应收账款
                         *          如果不存在，需要同时更新应收账款汇总表和应收账款详细表，并且不需要两个表之间相同更新余额
                         *          如果存在，只需要更新应收账款详细表，应收账款详细表会自动更新应收账款汇总表中的余额
                         */

                        // 把数据插入到应收账款汇总表
                        InitAccountReceivableTable accountReceivableCount = new InitAccountReceivableTable();
                        accountReceivableCount.customerOrSupplierID = accountReceivableData.customerId;                // 客户ID
                        accountReceivableCount.tradingDate = accountReceivableData.paymentDate;                        // 交易日期
                        accountReceivableCount.balance = turnover;
                        InitAccountReceivable.getInctance().insert(accountReceivableCount, false, false);

                        // 把数据插入到应收账款明细表
                        CashAccountReceivableDetailTable cashAccountReceivableDetailrecord = new CashAccountReceivableDetailTable();
                        cashAccountReceivableDetailrecord.customerOrSupplierID = accountReceivableData.customerId;     // 客户ID
                        cashAccountReceivableDetailrecord.billTypeName = "销售出库";                                // 账单类型
                        cashAccountReceivableDetailrecord.billNumber = billNumber;                                  // 单据号
                        cashAccountReceivableDetailrecord.tradingDate = accountReceivableData.paymentDate;             // 交易日期
                        cashAccountReceivableDetailrecord.turnover = turnover;                                      // 应收款金额
                        cashAccountReceivableDetailrecord.staffID = accountReceivableData.businessPeopleId;            // 业务员ID
                        CashAccountReceivableDetail.getInctance().insert(cashAccountReceivableDetailrecord, false);
                    }
                    else
                    {
                        // 把数据插入到应收账款明细表
                        CashAccountReceivableDetailTable cashAccountReceivableDetailrecord = new CashAccountReceivableDetailTable();
                        cashAccountReceivableDetailrecord.customerOrSupplierID = accountReceivableData.customerId;     // 客户ID
                        cashAccountReceivableDetailrecord.billTypeName = "销售出库";                                // 账单类型
                        cashAccountReceivableDetailrecord.billNumber = billNumber;                                  // 单据号
                        cashAccountReceivableDetailrecord.tradingDate = accountReceivableData.paymentDate;             // 交易日期
                        cashAccountReceivableDetailrecord.turnover = turnover;                                      // 应收款金额
                        cashAccountReceivableDetailrecord.staffID = accountReceivableData.businessPeopleId;            // 业务员ID
                        CashAccountReceivableDetail.getInctance().insert(cashAccountReceivableDetailrecord, true);
                    }
                }
                else 
                {
                    // 把数据插入到应收账款明细表
                    CashAccountReceivableDetailTable cashAccountReceivableDetailrecord = new CashAccountReceivableDetailTable();
                    cashAccountReceivableDetailrecord.customerOrSupplierID = accountReceivableData.customerId;     // 客户ID
                    cashAccountReceivableDetailrecord.billTypeName = "销售退货";                                // 账单类型
                    cashAccountReceivableDetailrecord.billNumber = billNumber;                                  // 单据号
                    cashAccountReceivableDetailrecord.tradingDate = accountReceivableData.paymentDate;             // 交易日期
                    cashAccountReceivableDetailrecord.turnover = turnover;                                      // 应收款金额
                    cashAccountReceivableDetailrecord.staffID = accountReceivableData.businessPeopleId;            // 业务员ID
                    CashAccountReceivableDetail.getInctance().insert(cashAccountReceivableDetailrecord, true);
                }
            }
        }

        private bool updateMaterielData(string billNumber)
        {
            bool isRet = true;

            SortedDictionary<int, SaleOutOrderDetailsTable> dataList =
                SaleOutOrderDetails.getInctance().getSaleOutInfoFromBillNumber(billNumber);

            if (!m_isRedBill)
            {
                // 首先检查物料库存余额是否大于本次出库单交易额，如果小于，则提示用户，但是审核失败
                foreach (KeyValuePair<int, SaleOutOrderDetailsTable> index in dataList)
                {
                    SaleOutOrderDetailsTable record = index.Value;

                    InitMaterielTable materielRecord = new InitMaterielTable();
                    materielRecord.materielID = record.materielID;
                    materielRecord.value = record.value;

                    InitMaterielTable materielStorageData = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);

                    if (materielStorageData.value < materielRecord.value)
                    {
                        isRet = false;
                        MessageBoxExtend.messageWarning("物料：[" + materielRecord.materielID + "]库存数量小于当前交易数量,单据审核失败");
                        break;
                    }
                }
            }

            if (isRet)
            {
                foreach (KeyValuePair<int, SaleOutOrderDetailsTable> index in dataList)
                {
                    SaleOutOrderDetailsTable record = index.Value;

                    #region 更新库存汇总表(INIT_STORAGE_STOCK)

                    // 交易完毕后库存数量
                    double tradingStocksValue = record.value;  

                    // 交易后库存数量等于当前仓库库存数减去本次交易数
                    InitMaterielTable materielStorageData = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);
                    if (m_isRedBill)
                    {
                        // 如果是销售退货，库存数量应该增加
                        tradingStocksValue = materielStorageData.value + tradingStocksValue;
                    }
                    else
                    {
                        tradingStocksValue = materielStorageData.value - tradingStocksValue;
                    }

                    InitMateriel.getInctance().materielOutStorage(record.materielID, tradingStocksValue);

                    #endregion
                    
                    #region 更新存货明细账表(STORAGE_STOCK_DETAIL) 2012-1-16 00:56
                    
                    StorageStockDetailTable storageStockDetailRecord = new StorageStockDetailTable();
                    storageStockDetailRecord.materielID = record.materielID;
                    storageStockDetailRecord.tradingDate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                    storageStockDetailRecord.billNumber = billNumber;
                    storageStockDetailRecord.thingsType = "销售出库";
                    storageStockDetailRecord.isIn = 0;

                    // 本次交易数量和单价
                    if (m_isRedBill)
                    {
                        storageStockDetailRecord.value = record.value * -1;
                    }
                    else
                    {
                        storageStockDetailRecord.value = record.value;
                    }
                    storageStockDetailRecord.price = record.price;

                    // 交易完毕后数量和单价
                    storageStockDetailRecord.storageValue = tradingStocksValue;
                    storageStockDetailRecord.storagePrice = materielStorageData.price;

                    StorageStockDetail.getInctance().insert(storageStockDetailRecord);
                    #endregion
                }
            }

            return isRet;
        }

        private void updataActualValue(string billNumber)
        {
            SaleOutOrderTable accountReceivableData = getSaleInfoFromBillNumber(billNumber);

            if (accountReceivableData.sourceBillType == "销售订单" && accountReceivableData.sourceBillNumber.Length > 0)
            {
                SaleOrder.getInctance().updataActualValue(accountReceivableData.sourceBillNumber, Convert.ToDouble(accountReceivableData.sumValue));
            }
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[CUSTOMER_ID],[TRADING_DATE],[BILL_NUMBER],[SALE_TYPE],[PAYMENT_DATE],";
            sql += "[EXCHANGES_UNIT],[BUSINESS_PEOPLE_ID],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],";
            sql += "[TOTAL_MONEY], [MAKE_ORDER_STAFF],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW],";
            sql += "[SOURCE_BILL_TYPE], [SOURCE_BILL_NUMBER],[STAFF_SAVE_ID],[STAFF_CHECK_ID],[RECEIVED_OK],[RECEIVED_NO_OK],";
            sql += "[IS_RED_BILL],[IS_IN_LEDGER],[ORDERR_IN_LEDGER],[IN_LEDGER_DATE] ";
            sql += "FROM [dbo].[SALE_OUT_ORDER] ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleOutOrderTable record = new SaleOutOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.customerId = DbDataConvert.ToInt32(row["CUSTOMER_ID"]);
                    record.customerName = Customer.getInctance().getCustomerNameFromPkey(record.customerId);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.saleType = DbDataConvert.ToString(row["SALE_TYPE"]);
                    record.paymentDate = DbDataConvert.ToDateTime(row["PAYMENT_DATE"]).ToString("yyyy-MM-dd");
                    record.exchangesUnit = DbDataConvert.ToString(row["EXCHANGES_UNIT"]);

                    record.businessPeopleId = DbDataConvert.ToInt32(row["BUSINESS_PEOPLE_ID"]);
                    record.businessPeopleName = Staff.getInctance().getStaffNameFromPkey(record.businessPeopleId);
                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    if (DbDataConvert.ToString(row["ORDERR_REVIEW"]).Length > 0)
                    {
                        record.orderrReview = DbDataConvert.ToInt32(row["ORDERR_REVIEW"]);
                        record.orderrReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderrReview);
                        record.reviewDate = DbDataConvert.ToDateTime(row["REVIEW_DATE"]).ToString("yyyy-MM-dd");
                    }

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);

                    record.sumValue = DbDataConvert.ToString(row["SUM_VALUE"]);
                    record.sumMoney = DbDataConvert.ToString(row["SUM_MONEY"]);
                    record.sumTransportationCost = DbDataConvert.ToString(row["SUM_TRANSPORTATION_COST"]);
                    record.sumOtherCost = DbDataConvert.ToString(row["SUM_OTHER_COST"]);
                    record.totalMoney = DbDataConvert.ToString(row["TOTAL_MONEY"]);

                    // 销售出库单比销售订单中多出的字段
                    record.sourceBillType = DbDataConvert.ToString(row["SOURCE_BILL_TYPE"]);
                    record.sourceBillNumber = DbDataConvert.ToString(row["SOURCE_BILL_NUMBER"]);
                    record.staffSaveId = DbDataConvert.ToInt32(row["STAFF_SAVE_ID"]);
                    record.staffSaveName = Staff.getInctance().getStaffNameFromPkey(record.staffSaveId);
                    record.staffCheckId = DbDataConvert.ToInt32(row["STAFF_CHECK_ID"]);
                    record.staffCheckName = Staff.getInctance().getStaffNameFromPkey(record.staffCheckId);

                    // 已收款金额和未收款金额
                    record.paymentOk = DbDataConvert.ToString(row["RECEIVED_OK"]);
                    record.paymentNoOk = DbDataConvert.ToString(row["RECEIVED_NO_OK"]);

                    // 红字蓝字标示和是否入账标记
                    record.isRedBill = DbDataConvert.ToInt32(row["IS_RED_BILL"]);

                    // 记账相关信息
                    if (DbDataConvert.ToString(row["ORDERR_IN_LEDGER"]).Length > 0)
                    {
                        record.orderInLedger = DbDataConvert.ToInt32(row["ORDERR_IN_LEDGER"]);
                        record.orderInLedgerName = Staff.getInctance().getStaffNameFromPkey(record.orderInLedger);
                        record.inLedgerDate = DbDataConvert.ToDateTime(row["IN_LEDGER_DATE"]).ToString("yyyy-MM-dd");
                    }
                    record.isInLedger = DbDataConvert.ToInt32(row["IS_IN_LEDGER"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, SaleOutOrderTable> getAllSaleOutOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, SaleOutOrderTable> getAllReviewSaleOutOrderInfo()
        {
            SortedDictionary<int, SaleOutOrderTable> list = new SortedDictionary<int, SaleOutOrderTable>();

            foreach (KeyValuePair<int, SaleOutOrderTable> index in m_tableDataList)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, SaleOutOrderTable> getAllNotReviewSaleOutOrderInfo()
        {
            SortedDictionary<int, SaleOutOrderTable> list = new SortedDictionary<int, SaleOutOrderTable>();

            foreach (KeyValuePair<int, SaleOutOrderTable> index in m_tableDataList)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SaleOutOrderTable getSaleInfoFromBillNumber(string billNumber)
        {
            SaleOutOrderTable record = new SaleOutOrderTable();

            foreach (KeyValuePair<int, SaleOutOrderTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    break;
                }
            }

            return record;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, SaleOutOrderTable> index in m_tableDataList)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public bool checkBillIsReview(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, SaleOutOrderTable> index in m_tableDataList)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    if (index.Value.isReview == "1")
                    {
                        isRet = true;
                    }
                    break;
                }
            }

            return isRet;
        }

        public void refreshRecord()
        {
            load();
        }

        public SortedDictionary<int, SaleOutOrderTable> getSaleOutOrderCountInfo(string startDate, string endDate, bool isAllBill = false, int countType = 1)
        {
            SortedDictionary<int, SaleOutOrderTable> dataList = new SortedDictionary<int, SaleOutOrderTable>();
            string groupBySQL = "", querySQL = "";

            // countType 等于1是按客户汇总，countType等于2是业务员汇总
            if (countType == 1)
            {
                querySQL = "SELECT [CUSTOMER_ID],SUM([TOTAL_MONEY]) AS TOTAL_MONEY FROM [dbo].[SALE_OUT_ORDER] WHERE ";
                groupBySQL += " GROUP BY CUSTOMER_ID";
            }
            else if (countType == 2)
            {
                querySQL = "SELECT [BUSINESS_PEOPLE_ID],SUM([TOTAL_MONEY]) AS TOTAL_MONEY FROM [dbo].[SALE_OUT_ORDER] WHERE ";
                groupBySQL += " GROUP BY BUSINESS_PEOPLE_ID";
            }

            string query = querySQL;
            query += " TRADING_DATE >= '" + startDate + "' AND TRADING_DATE <= '" + endDate + "' AND IS_RED_BILL = 0 ";

            if (!isAllBill)
            {
                query += " AND IS_REVIEW = 1";
            }

            query += groupBySQL;
            query += " ORDER BY TOTAL_MONEY DESC";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleOutOrderTable record = new SaleOutOrderTable();

                    if (countType == 1)
                    {
                        record.customerId = DbDataConvert.ToInt32(row["CUSTOMER_ID"]);
                        record.customerName = Customer.getInctance().getCustomerNameFromPkey(record.customerId);
                    }
                    else if (countType == 2)
                    {
                        record.businessPeopleId = DbDataConvert.ToInt32(row["BUSINESS_PEOPLE_ID"]);
                        record.businessPeopleName = Staff.getInctance().getStaffNameFromPkey(record.businessPeopleId);
                    }

                    record.totalMoney = DbDataConvert.ToString(row["TOTAL_MONEY"]);

                    dataList.Add(dataList.Count, record);
                }
            }
            return dataList;
        }
    }

    public class SaleOutOrderTable
    {
        public int pkey { get; set; }
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string saleType { get; set; }
        public string paymentDate { get; set; }
        public string exchangesUnit { get; set; }
        public int businessPeopleId { get; set; }
        public string businessPeopleName { get; set; }
        public string sumValue { get; set; }
        public string sumMoney { get; set; }
        public string sumTransportationCost { get; set; }
        public string sumOtherCost { get; set; }
        public string totalMoney { get; set; }
        public int makeOrderStaff { get; set; }
        public string makeOrderStaffName { get; set; }
        public int orderrReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
        public string isReview { get; set; }

        public string sourceBillType { get; set; }
        public string sourceBillNumber { get; set; }
        public int staffSaveId { get; set; }
        public string staffSaveName { get; set; }
        public int staffCheckId { get; set; }
        public string staffCheckName { get; set; }

        public string paymentOk { get; set; }
        public string paymentNoOk { get; set; }

        // 红字蓝字单据标示
        public int isRedBill { get; set; }

        // 是否已记账
        public int orderInLedger { get; set; }
        public string orderInLedgerName { get; set; }
        public string inLedgerDate { get; set; }
        public int isInLedger { get; set; }
    }
}