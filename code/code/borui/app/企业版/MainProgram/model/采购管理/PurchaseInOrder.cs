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
    public class PurchaseInOrder : ITableModel
    {
        private bool m_isRedBill;
        private SortedDictionary<int, PurchaseInOrderTable> m_tableDataList = new SortedDictionary<int, PurchaseInOrderTable>();

        static private PurchaseInOrder m_instance = null;

        private PurchaseInOrder()
        {
            load();
        }

        static public PurchaseInOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new PurchaseInOrder();
            }

            return m_instance;
        }

        public void insert(PurchaseInOrderTable record, bool isDisplayMessageBox = true)
        {
            bool isExistReview = false;
            PurchaseInOrderTable oldRecord = new PurchaseInOrderTable();

            string insert = "INSERT INTO [dbo].[PURCHASE_IN_ORDER]([SUPPLIER_ID],[TRADING_DATE],[BILL_NUMBER],[CONTRACT_NUM],[PURCHASE_TYPE],";
            insert += "[PAYMENT_DATE],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],[TOTAL_MONEY],";
            insert += "[BUSINESS_PEOPLE_ID],[MAKE_ORDER_STAFF],[SOURCE_BILL_TYPE],[SOURCE_BILL_NUMBER],[STAFF_SAVE_ID],";
            insert += "[STAFF_CHECK_ID],[PAYMENT_OK],[PAYMENT_NO_OK],[IS_RED_BILL],[IS_IN_LEDGER]";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                if (checkBillIsReview(record.billNumber))
                {
                    isExistReview = true;
                    insert += ",[ORDERR_REVIEW], [REVIEW_DATE], [IS_REVIEW] ";
                    oldRecord = getPurchaseInfoFromBillNumber(record.billNumber);
                }

                delete(record.billNumber);
            }

            insert += ") VALUES(";

            insert += record.supplierId + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";

            insert += "'" + record.contractNum + "',";

            insert += "'" + record.purchaseType + "',";
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
            insert += record.totalMoney + ", ";

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

            writeOperatorLog(102, OperatorLogType.Add, record.billNumber);
        }

        private void delete(string billNumber)
        {
            string delete = "DELETE FROM PURCHASE_IN_ORDER WHERE BILL_NUMBER = '" + billNumber + "'"; 

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

        public void refreshRecord()
        {
            load();
        }

        public void billReview(string billNumber)
        {
            // 更新单据审核标志，把审核标志置为
            string update = "UPDATE [dbo].[PURCHASE_IN_ORDER] SET ";

            update += "[ORDERR_REVIEW] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "REVIEW_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_REVIEW = 1";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("单据[" + billNumber + "]审核成功");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(102, OperatorLogType.Review, billNumber);
        }

        public void registerInLedger(string billNumber, bool isRedBill)
        {
            /*函数处理逻辑如下：
             * 1、更新单据是否入账标示，把标示修改为1(已入账)
             * 1、如果采购方式为赊购并且发生了运输费用或其他费用，询问用户是否把运输费用或者其他费用计入应付账款
             * 2、根据采购单详细信息，更新库存表，如果发生了运输费用或其他费用，需要把运输费用和其他费用计入物料单价
             * 3、如果单据是根据采购订单生成，更新对应采购订单中的实际入库数量
             */
            m_isRedBill = isRedBill;

            // 更新应付账款
            updateAccountPayableData(billNumber);

            // 更新库存表
            updateMaterielData(billNumber);

            // 如果本张采购入库单是根据某采购订单生成，需要更新采购订单的实际入库数量字段
            updataActualValue(billNumber);

            // 更新是否入账标示
            string update = "UPDATE [dbo].[PURCHASE_IN_ORDER] SET ";

            update += "[ORDERR_IN_LEDGER] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "IN_LEDGER_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_IN_LEDGER = 1";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("单据[" + billNumber + "]入账成功");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(102, OperatorLogType.Register, billNumber);
        }

        public void updataPaymentInfo(string billNumber, double paymentOK)
        {
            PurchaseInOrderTable record = getPurchaseInfoFromBillNumber(billNumber);
            double paymentNoOK = Convert.ToDouble(record.totalMoney.ToString()) - paymentOK;

            string update = "UPDATE [dbo].[PURCHASE_IN_ORDER] SET ";
            update += "PAYMENT_OK = " + paymentOK + ", ";
            update += "PAYMENT_NO_OK = " + paymentNoOK;
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

        private void updateAccountPayableData(string billNumber)
        {
            PurchaseInOrderTable accountPayableData = getPurchaseInfoFromBillNumber(billNumber);

            if(accountPayableData.purchaseType == "赊购")
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();

                // 应付款金额
                double turnover = Convert.ToDouble(accountPayableData.sumMoney.ToString());

                string message = "单据 [" + accountPayableData.billNumber + "] 采购类型为赊购，";
                message += "系统会自动产生一笔应付账款信息记录到供应商 [" + accountPayableData.supplierName + "] 账户下，";
                message += "请确实应付账款金额，然后点击【确定】按钮(如需调整金额，请点击调整【金额调整】按钮,再点击【确定】)";

                FormAdjustAmount faa = new FormAdjustAmount("应付账款金额确认", message, accountPayableData.totalMoney);
                faa.ShowDialog();
                turnover = faa.getAmount();

                if (!m_isRedBill)
                {
                    if (!InitAccountPayable.getInctance().checkcustomerOrSupplierIDIsExist(accountPayableData.supplierId))
                    {
                        /* 说明
                         * 系统应收应付账款的汇总表和详细的收款还款记录是分开存放，并且默认情况下会相互更新
                         * 比如正常的逻辑(用于应付应付胀库初始化模块)：
                         *      当在应付账款汇总表插入一条记录的时候，程序会自动插入一条期初数据到应付账款详细表
                         *      当有一笔交易更新了应付账款表时，程序会自动查询应付账款统计表，更新对应供应商的欠款余额
                         * 此处的逻辑如下：             
                         *      当发生采购入库赊购业务时候，自动产生一条应付账款信息
                         *      这里需要首先查询下系统中是否存在跟该供应商的应付账款
                         *          如果不存在，需要同时更新应付账款汇总表和应付账款详细表，并且不需要两个表之间相同更新余额
                         *          如果存在，只需要更新应付账款详细表，应付账款详细表会自动更新应付账款汇总表中的余额
                         */

                        // 把数据插入到应付账款汇总表
                        InitAccountReceivableTable accountReceivableCount = new InitAccountReceivableTable();
                        accountReceivableCount.customerOrSupplierID = accountPayableData.supplierId;                // 供应商ID
                        accountReceivableCount.tradingDate = accountPayableData.paymentDate;                        // 交易日期
                        accountReceivableCount.balance = turnover;
                        InitAccountPayable.getInctance().insert(accountReceivableCount, false, false);

                        // 把数据插入到应付账款明细表
                        CashAccountReceivableDetailTable cashAccountReceivableDetailrecord = new CashAccountReceivableDetailTable();
                        cashAccountReceivableDetailrecord.customerOrSupplierID = accountPayableData.supplierId;     // 供应商ID
                        cashAccountReceivableDetailrecord.billTypeName = "采购入库";                                // 账单类型
                        cashAccountReceivableDetailrecord.billNumber = billNumber;                                  // 单据号
                        cashAccountReceivableDetailrecord.tradingDate = accountPayableData.paymentDate;             // 交易日期
                        cashAccountReceivableDetailrecord.turnover = turnover;                                      // 应付款金额
                        cashAccountReceivableDetailrecord.staffID = accountPayableData.businessPeopleId;            // 采购员ID
                        CashAccountPayableDetail.getInctance().insert(cashAccountReceivableDetailrecord, false);
                    }
                    else
                    {
                        // 把数据插入到应付账款明细表
                        CashAccountReceivableDetailTable cashAccountReceivableDetailrecord = new CashAccountReceivableDetailTable();
                        cashAccountReceivableDetailrecord.customerOrSupplierID = accountPayableData.supplierId;     // 供应商ID
                        cashAccountReceivableDetailrecord.billTypeName = "采购入库";                                // 账单类型
                        cashAccountReceivableDetailrecord.billNumber = billNumber;                                  // 单据号
                        cashAccountReceivableDetailrecord.tradingDate = accountPayableData.paymentDate;             // 交易日期
                        cashAccountReceivableDetailrecord.turnover = turnover;                                      // 应付款金额
                        cashAccountReceivableDetailrecord.staffID = accountPayableData.businessPeopleId;            // 采购员ID
                        CashAccountPayableDetail.getInctance().insert(cashAccountReceivableDetailrecord, true);
                    }
                }
                else 
                {
                    // 代表是红字凭证 把数据插入到应付账款明细表
                    CashAccountReceivableDetailTable cashAccountReceivableDetailrecord = new CashAccountReceivableDetailTable();
                    cashAccountReceivableDetailrecord.customerOrSupplierID = accountPayableData.supplierId;     // 供应商ID
                    cashAccountReceivableDetailrecord.billTypeName = "采购退货";                                // 账单类型
                    cashAccountReceivableDetailrecord.billNumber = billNumber;                                  // 单据号
                    cashAccountReceivableDetailrecord.tradingDate = accountPayableData.paymentDate;             // 交易日期
                    cashAccountReceivableDetailrecord.turnover = turnover;                                      // 应付款金额
                    cashAccountReceivableDetailrecord.staffID = accountPayableData.businessPeopleId;            // 采购员ID
                    CashAccountPayableDetail.getInctance().insert(cashAccountReceivableDetailrecord, true);
                }
            }
        }

        private void updateMaterielData(string billNumber)
        {
            SortedDictionary<int, PurchaseInOrderDetailsTable> dataList =
                PurchaseInOrderDetails.getInctance().getPurchaseInfoFromBillNumber(billNumber);

            foreach (KeyValuePair<int, PurchaseInOrderDetailsTable> index in dataList)
            {
                PurchaseInOrderDetailsTable record = index.Value;
                #region 更新库存汇总表(INIT_STORAGE_STOCK)
                InitMaterielTable materielRecord = new InitMaterielTable();
                materielRecord.materielID = record.materielID;
                materielRecord.value = record.value;
                materielRecord.price = record.price;

                if (record.sumMoney != record.totalMoney)
                {
                    // 如果物料总费用和总金额不相等，代表有需要计入成本的费用发生，需要重新核算物料单价
                    materielRecord.price = (record.costApportionments + record.sumMoney) / record.value;
                }

                if (m_isRedBill)
                {
                    InitMateriel.getInctance().materielOutStorage(materielRecord, false);
                }
                else
                {
                    InitMateriel.getInctance().insert(materielRecord, false);
                }
                #endregion

                #region 更新存货明细账表(STORAGE_STOCK_DETAIL) 2012-1-16 01:22
                StorageStockDetailTable storageStockDetailRecord = new StorageStockDetailTable();
                storageStockDetailRecord.materielID = record.materielID;
                storageStockDetailRecord.tradingDate = DateTime.Now.ToString("yyyyMMdd");
                storageStockDetailRecord.billNumber = billNumber;
                storageStockDetailRecord.thingsType = "采购入库";
                storageStockDetailRecord.isIn = 1;

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
                InitMaterielTable materielStorageData = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);
                storageStockDetailRecord.storageValue = materielStorageData.value;
                storageStockDetailRecord.storagePrice = materielStorageData.price;

                StorageStockDetail.getInctance().insert(storageStockDetailRecord);
                #endregion
            }
        }

        private void updataActualValue(string billNumber)
        {
            PurchaseInOrderTable accountPayableData = getPurchaseInfoFromBillNumber(billNumber);

            if (accountPayableData.sourceBillType == "采购订单" && accountPayableData.sourceBillNumber.Length > 0)
            {
                PurchaseOrder.getInctance().updataActualValue(accountPayableData.sourceBillNumber, Convert.ToDouble(accountPayableData.sumValue));
            }
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[SUPPLIER_ID],[TRADING_DATE],[BILL_NUMBER],[CONTRACT_NUM],[PURCHASE_TYPE],[PAYMENT_DATE],";
            sql += "[EXCHANGES_UNIT],[BUSINESS_PEOPLE_ID],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],";
            sql += "[TOTAL_MONEY], [MAKE_ORDER_STAFF],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW],";
            sql += "[SOURCE_BILL_TYPE], [SOURCE_BILL_NUMBER],[STAFF_SAVE_ID],[STAFF_CHECK_ID],[PAYMENT_OK],[PAYMENT_NO_OK],";
            sql += "[IS_RED_BILL],[IS_IN_LEDGER],[ORDERR_IN_LEDGER],[IN_LEDGER_DATE] ";
            sql += "FROM [dbo].[PURCHASE_IN_ORDER] ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    PurchaseInOrderTable record = new PurchaseInOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.supplierId = DbDataConvert.ToInt32(row["SUPPLIER_ID"]);
                    record.supplierName = Supplier.getInctance().getSupplierNameFromPkey(record.supplierId);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.contractNum = DbDataConvert.ToString(row["CONTRACT_NUM"]);
                    record.purchaseType = DbDataConvert.ToString(row["PURCHASE_TYPE"]);
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

                    // 采购入库单比采购订单中多出的字段
                    record.sourceBillType = DbDataConvert.ToString(row["SOURCE_BILL_TYPE"]);
                    record.sourceBillNumber = DbDataConvert.ToString(row["SOURCE_BILL_NUMBER"]);
                    record.staffSaveId = DbDataConvert.ToInt32(row["STAFF_SAVE_ID"]);
                    record.staffSaveName = Staff.getInctance().getStaffNameFromPkey(record.staffSaveId);
                    record.staffCheckId = DbDataConvert.ToInt32(row["STAFF_CHECK_ID"]);
                    record.staffCheckName = Staff.getInctance().getStaffNameFromPkey(record.staffCheckId);

                    // 已还款金额和未还款金额
                    record.paymentOk = DbDataConvert.ToString(row["PAYMENT_OK"]);
                    record.paymentNoOk = DbDataConvert.ToString(row["PAYMENT_NO_OK"]);

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

        public SortedDictionary<int, PurchaseInOrderTable> getAllPurchaseInOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, PurchaseInOrderTable> getAllReviewPurchaseInOrderInfo()
        {
            SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

            foreach (KeyValuePair<int, PurchaseInOrderTable> index in m_tableDataList)
            {
                PurchaseInOrderTable record = new PurchaseInOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, PurchaseInOrderTable> getAllNotReviewPurchaseInOrderInfo()
        {
            SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

            foreach (KeyValuePair<int, PurchaseInOrderTable> index in m_tableDataList)
            {
                PurchaseInOrderTable record = new PurchaseInOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public PurchaseInOrderTable getPurchaseInfoFromBillNumber(string billNumber)
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            PurchaseInOrderTable record = new PurchaseInOrderTable();

            foreach (KeyValuePair<int, PurchaseInOrderTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, PurchaseInOrderTable> index in m_tableDataList)
            {
                PurchaseInOrderTable record = new PurchaseInOrderTable();

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

            foreach (KeyValuePair<int, PurchaseInOrderTable> index in m_tableDataList)
            {
                PurchaseInOrderTable record = new PurchaseInOrderTable();

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

        public SortedDictionary<int, PurchaseInOrderTable> getPurchaseInOrderCountInfo(string startDate, string endDate, bool isAllBill = false, int countType = 1)
        {
            SortedDictionary<int, PurchaseInOrderTable> dataList = new SortedDictionary<int, PurchaseInOrderTable>();
            string groupBySQL = "", querySQL = "";

            // countType 等于1是按供应商汇总，countType等于2是采购员汇总
            if (countType == 1)
            {
                querySQL = "SELECT [SUPPLIER_ID],SUM([TOTAL_MONEY]) AS TOTAL_MONEY FROM [dbo].[PURCHASE_IN_ORDER] WHERE ";
                groupBySQL += " GROUP BY SUPPLIER_ID";
            }
            else if (countType == 2)
            {
                querySQL = "SELECT [BUSINESS_PEOPLE_ID],SUM([TOTAL_MONEY]) AS TOTAL_MONEY FROM [dbo].[PURCHASE_IN_ORDER] WHERE ";
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
                    PurchaseInOrderTable record = new PurchaseInOrderTable();

                    if (countType == 1)
                    {
                        record.supplierId = DbDataConvert.ToInt32(row["SUPPLIER_ID"]);
                        record.supplierName = Supplier.getInctance().getSupplierNameFromPkey(record.supplierId);
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

    public class PurchaseInOrderTable
    {
        public int pkey { get; set; }
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string purchaseType { get; set; }
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

        // 项目合同编号
        public string contractNum { get; set; }
        
    }
}