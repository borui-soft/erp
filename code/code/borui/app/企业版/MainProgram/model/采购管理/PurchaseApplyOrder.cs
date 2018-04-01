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
    public class PurchaseApplyOrder : ITableModel
    {
        private SortedDictionary<int, PurchaseApplyOrderTable> m_tableDataList = new SortedDictionary<int, PurchaseApplyOrderTable>();

        static private PurchaseApplyOrder m_instance = null;

        private PurchaseApplyOrder()
        {
            load();
        }

        static public PurchaseApplyOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new PurchaseApplyOrder();
            }

            return m_instance;
        }

        public bool checkBillIsReview(string billNumber)
        {
            bool isRet = false;

            string sql = "SELECT [IS_REVIEW] FROM PURCHASE_APPLY_ORDER WHERE BILL_NUMBER = '" + billNumber + "'";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                if (dataTable.Rows.Count > 0)
                {
                    if (DbDataConvert.ToString(dataTable.Rows[0][0]) == "1")
                    {
                        isRet = true;
                    }
                }
            }

            return isRet;
        }

        public void insert(PurchaseApplyOrderTable record, bool isDisplayMessageBox = true)
        {
            string insert = "INSERT INTO [dbo].[PURCHASE_APPLY_ORDER]([APPLY_ID],[TRADING_DATE],[BILL_NUMBER],[PROJECT_NUM],[PAYMENT_DATE],";
            insert += "[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[SUM_OTHER_COST],[TOTAL_MONEY],[MAKE_ORDER_STAFF],[IS_REVIEW]) VALUES(";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            insert += record.applyId + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.srcOrderNum + "',";
            insert += "'" + record.paymentDate + "',";
            insert += "'" + record.exchangesUnit + "',";
            insert += "'" + record.sumValue + "',";
            insert += "'" + record.sumMoney + "',";
            insert += "'" + record.sumOtherCost + "',";
            insert += "'" + record.totalMoney + "',";
            insert += record.makeOrderStaff + ", 0";
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

            writeOperatorLog(101, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string biilNumber)
        {
            string delete = "DELETE FROM PURCHASE_APPLY_ORDER WHERE BILL_NUMBER = '" + biilNumber + "'"; 

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
            string update = "UPDATE [dbo].[PURCHASE_APPLY_ORDER] SET ";

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

            writeOperatorLog(101, OperatorLogType.Review, billNumber);
        }

        public void refreshRecord()
        {
            load();
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[APPLY_ID],[TRADING_DATE],[BILL_NUMBER],[PROJECT_NUM],[PAYMENT_DATE],[EXCHANGES_UNIT],";
            sql += "[SUM_VALUE],[SUM_MONEY],[SUM_OTHER_COST],[TOTAL_MONEY], [MAKE_ORDER_STAFF],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW]";
            sql += "FROM [dbo].[PURCHASE_APPLY_ORDER] ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.applyId = DbDataConvert.ToInt32(row["APPLY_ID"]);
                    record.applyName = Staff.getInctance().getStaffNameFromPkey(record.applyId);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    record.srcOrderNum = DbDataConvert.ToString(row["PROJECT_NUM"]);
                    record.paymentDate = DbDataConvert.ToDateTime(row["PAYMENT_DATE"]).ToString("yyyy-MM-dd");
                    record.exchangesUnit = DbDataConvert.ToString(row["EXCHANGES_UNIT"]);


                    record.sumValue = DbDataConvert.ToString(row["SUM_VALUE"]);
                    record.sumMoney = DbDataConvert.ToString(row["SUM_MONEY"]);
                    record.sumOtherCost = DbDataConvert.ToString(row["SUM_OTHER_COST"]);
                    record.totalMoney = DbDataConvert.ToString(row["TOTAL_MONEY"]);

                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    if (DbDataConvert.ToString(row["ORDERR_REVIEW"]).Length > 0)
                    {
                        record.orderrReview = DbDataConvert.ToInt32(row["ORDERR_REVIEW"]);
                        record.orderrReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderrReview);
                        record.reviewDate = DbDataConvert.ToDateTime(row["REVIEW_DATE"]).ToString("yyyy-MM-dd");
                    }

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, PurchaseApplyOrderTable> getAllPurchaseOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, PurchaseApplyOrderTable> getAllReviewPurchaseOrderInfo()
        {
            SortedDictionary<int, PurchaseApplyOrderTable> list = new SortedDictionary<int, PurchaseApplyOrderTable>();

            foreach (KeyValuePair<int, PurchaseApplyOrderTable> index in m_tableDataList)
            {
                PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, PurchaseApplyOrderTable> getAllNotReviewPurchaseOrderInfo()
        {
            SortedDictionary<int, PurchaseApplyOrderTable> list = new SortedDictionary<int, PurchaseApplyOrderTable>();

            foreach (KeyValuePair<int, PurchaseApplyOrderTable> index in m_tableDataList)
            {
                PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public PurchaseApplyOrderTable getPurchaseInfoFromBillNumber(string billNumber)
        {
            PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();

            foreach (KeyValuePair<int, PurchaseApplyOrderTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    break;
                }
            }

            return record;
        }

        public SortedDictionary<int, PurchaseApplyOrderTable> getAllPurchaseOrderInfoFromProjectNum(string projectNum)
        {
            SortedDictionary<int, PurchaseApplyOrderTable> list = new SortedDictionary<int, PurchaseApplyOrderTable>();

            foreach (KeyValuePair<int, PurchaseApplyOrderTable> index in m_tableDataList)
            {
                PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();
                record = index.Value;

                if (index.Value.srcOrderNum .IndexOf(projectNum) >= 0)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, PurchaseApplyOrderTable> index in m_tableDataList)
            {
                PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class PurchaseApplyOrderTable
    {
        public int pkey { get; set; }
        public int applyId { get; set; }
        public string applyName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }

        // 总材料单编号、期望交货日志和摘要
        public string srcOrderNum { get; set; }
        public string paymentDate { get; set; }
        public string exchangesUnit { get; set; }

        public string sumValue { get; set; }
        public string sumMoney { get; set; }
        public string sumOtherCost { get; set; }
        public string totalMoney { get; set; }

        public int makeOrderStaff { get; set; }
        public string makeOrderStaffName { get; set; }
        public int orderrReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
        public string isReview { get; set; }
    }
}