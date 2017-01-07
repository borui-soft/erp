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
    public class MaterielInOtherOrder : ITableModel
    {
        private bool m_isRedBill = false;
        private SortedDictionary<int, MaterielInOtherOrderTable> m_tableDataList = new SortedDictionary<int, MaterielInOtherOrderTable>();

        static private MaterielInOtherOrder m_instance = null;

        private MaterielInOtherOrder()
        {
            load();
        }

        static public MaterielInOtherOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielInOtherOrder();
            }

            return m_instance;
        }

        public void refreshRecord()
        {
            load();
        }

        public void insert(MaterielInOtherOrderTable record, bool isDisplayMessageBox = true)
        {
            bool isExistReview = false;
            MaterielInOtherOrderTable oldRecord = new MaterielInOtherOrderTable();

            string insert = "INSERT INTO [dbo].[WAREHOUSE_MANAGEMENT_IN_OTHER]([TRADING_DATE],[BILL_NUMBER],";
            insert += "[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[MAKE_ORDER_STAFF],[STAFF_SAVE_ID],[STAFF_CHECK_ID],";
            insert += "[IS_RED_BILL],[IS_IN_LEDGER]";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                if (checkBillIsReview(record.billNumber))
                {
                    isExistReview = true;
                    insert += ",[ORDERR_REVIEW], [REVIEW_DATE], [IS_REVIEW] ";
                    oldRecord = getMaterielInOtherOrderInfoFromBillNumber(record.billNumber);
                }

                delete(record.billNumber);
            }

            insert += ") VALUES(";

            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.exchangesUnit + "',";
            insert += "'" + record.sumValue + "',";
            insert += "'" + record.sumMoney + "',";
            insert += record.makeOrderStaff + ",";
            insert += record.staffSaveId + ",";
            insert += record.orderReviewStaffId + ",";
           
            // 红字蓝字标示
            insert += record.isRedBill + ",0" ;

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

            writeOperatorLog(303, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string billNumber)
        {
            string delete = "DELETE FROM WAREHOUSE_MANAGEMENT_IN_OTHER WHERE BILL_NUMBER = '" + billNumber + "'"; 

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
            string update = "UPDATE [dbo].[WAREHOUSE_MANAGEMENT_IN_OTHER] SET ";

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

            writeOperatorLog(303, OperatorLogType.Review, billNumber);
        }

        public void registerInLedger(string billNumber, bool isRedBill)
        {
            /*函数处理逻辑如下：
             * 1、根据输入的产品单价，更新对应的库存数量，库存单价
             * 2、更新单据是否入账标示，把标示修改为1(已入账)
             */
            m_isRedBill = isRedBill;

            // 更新库存表
            updateMaterielData(billNumber);

            // 更新是否入账标示
            string update = "UPDATE [dbo].[WAREHOUSE_MANAGEMENT_IN_OTHER] SET ";

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

            writeOperatorLog(303, OperatorLogType.Register, billNumber);
        }

        private void updateMaterielData(string billNumber)
        {
            SortedDictionary<int, MaterielInOtherOrderDetailsTable> dataList =
                MaterielInOtherOrderDetails.getInctance().getMaterielInOtherInfoFromBillNumber(billNumber);

            foreach (KeyValuePair<int, MaterielInOtherOrderDetailsTable> index in dataList)
            {
                MaterielInOtherOrderDetailsTable record = index.Value;

                //InitMaterielTable materielRecord = new InitMaterielTable();
                //materielRecord.materielID = record.materielID;
                //materielRecord.value = (int)record.value;
                //materielRecord.price = record.price;

                //if (m_isRedBill)
                //{
                //    InitMateriel.getInctance().materielOutStorage(materielRecord, false);
                //}
                //else
                //{
                //    InitMateriel.getInctance().insert(materielRecord, false);
                //}

                #region 更新库存汇总表(INIT_STORAGE_STOCK)
                InitMaterielTable materielRecord = new InitMaterielTable();
                materielRecord.materielID = record.materielID;
                materielRecord.value = record.value;
                materielRecord.price = record.price;

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
                storageStockDetailRecord.thingsType = "其他入库";
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

        private void load()
        {
            string sql = "SELECT [PKEY],[TRADING_DATE],[BILL_NUMBER],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],";
            sql += "[MAKE_ORDER_STAFF],[STAFF_CHECK_ID],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW],[STAFF_SAVE_ID],";
            sql += "[IS_RED_BILL],[IS_IN_LEDGER],[ORDERR_IN_LEDGER],[IN_LEDGER_DATE] ";
            sql += "FROM [dbo].[WAREHOUSE_MANAGEMENT_IN_OTHER] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.exchangesUnit = DbDataConvert.ToString(row["EXCHANGES_UNIT"]);

                    record.sumValue = DbDataConvert.ToString(row["SUM_VALUE"]);
                    record.sumMoney = DbDataConvert.ToString(row["SUM_MONEY"]);

                    record.staffSaveId = DbDataConvert.ToInt32(row["STAFF_SAVE_ID"]);
                    record.staffSaveName = Staff.getInctance().getStaffNameFromPkey(record.staffSaveId);

                    record.orderReviewStaffId = DbDataConvert.ToInt32(row["STAFF_CHECK_ID"]);
                    record.orderReviewStaffName = Staff.getInctance().getStaffNameFromPkey(record.orderReviewStaffId);

                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);
                    if (DbDataConvert.ToString(row["ORDERR_REVIEW"]).Length > 0)
                    {
                        record.orderrReview = DbDataConvert.ToInt32(row["ORDERR_REVIEW"]);
                        record.orderrReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderrReview);
                        record.reviewDate = DbDataConvert.ToDateTime(row["REVIEW_DATE"]).ToString("yyyy-MM-dd");
                    }

                    record.isRedBill = DbDataConvert.ToInt32(row["IS_RED_BILL"]);

                    // 记账相关信息
                    if (DbDataConvert.ToString(row["ORDERR_IN_LEDGER"]).Length > 0)
                    {
                        record.orderInLedger = DbDataConvert.ToInt32(row["ORDERR_IN_LEDGER"]);
                        record.orderInLedgerName = Staff.getInctance().getStaffNameFromPkey(record.orderInLedger);
                        record.inLedgerDate = DbDataConvert.ToDateTime(row["IN_LEDGER_DATE"]).ToString("yyyy-MM-dd");
                        record.isInLedger = DbDataConvert.ToInt32(row["IS_IN_LEDGER"]);
                    }

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, MaterielInOtherOrderTable> getAllMaterielInOtherOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, MaterielInOtherOrderTable> getAllReviewMaterielInOtherOrderInfo()
        {
            SortedDictionary<int, MaterielInOtherOrderTable> list = new SortedDictionary<int, MaterielInOtherOrderTable>();

            foreach (KeyValuePair<int, MaterielInOtherOrderTable> index in m_tableDataList)
            {
                MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, MaterielInOtherOrderTable> getAllNotReviewMaterielInOtherOrderInfo()
        {
            SortedDictionary<int, MaterielInOtherOrderTable> list = new SortedDictionary<int, MaterielInOtherOrderTable>();

            foreach (KeyValuePair<int, MaterielInOtherOrderTable> index in m_tableDataList)
            {
                MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public MaterielInOtherOrderTable getMaterielInOtherOrderInfoFromBillNumber(string billNumber)
        {
            MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();

            foreach (KeyValuePair<int, MaterielInOtherOrderTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, MaterielInOtherOrderTable> index in m_tableDataList)
            {
                MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();

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

            foreach (KeyValuePair<int, MaterielInOtherOrderTable> index in m_tableDataList)
            {
                MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();

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
    }

    public class MaterielInOtherOrderTable
    {
        public int pkey { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string exchangesUnit { get; set; }

        public string sumValue { get; set; }
        public string sumMoney { get; set; }

        // 保管员
        public int staffSaveId { get; set; }
        public string staffSaveName { get; set; }

        // 验收人
        public int orderReviewStaffId { get; set; }
        public string orderReviewStaffName { get; set; }

        // 制单人
        public int makeOrderStaff { get; set; }
        public string makeOrderStaffName { get; set; }

        // 审核人
        public int orderrReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
        public string isReview { get; set; }

        // 红字蓝字单据标示
        public int isRedBill { get; set; }

        // 是否已记账
        public int orderInLedger { get; set; }
        public string orderInLedgerName { get; set; }
        public string inLedgerDate { get; set; }
        public int isInLedger { get; set; }
    }
}