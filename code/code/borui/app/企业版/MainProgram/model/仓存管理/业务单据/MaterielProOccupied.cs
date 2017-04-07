using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;
using MainProgram.model;
using System.Collections;


namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class MaterielProOccupiedOrder : ITableModel
    {
        private bool m_isRedBill = false;
        private SortedDictionary<int, MaterielProOccupiedOrderTable> m_tableDataList = new SortedDictionary<int, MaterielProOccupiedOrderTable>();

        static private MaterielProOccupiedOrder m_instance = null;

        private MaterielProOccupiedOrder()
        {
            load();
        }

        static public MaterielProOccupiedOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielProOccupiedOrder();
            }

            return m_instance;
        }

        public void refreshRecord()
        {
            load();
        }

        public void insert(MaterielProOccupiedOrderTable record, bool isDisplayMessageBox = true)
        {
            MaterielProOccupiedOrderTable oldRecord = new MaterielProOccupiedOrderTable();

            string insert = "INSERT INTO [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED]([TRADING_DATE],[BILL_NUMBER],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],";
            insert += "[MAKE_ORDER_STAFF],[APPLY_STAFF]) VALUES(";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.exchangesUnit + "',";
            insert += "'" + record.sumValue + "',";
            insert += "'" + record.sumMoney + "',";

            insert += record.makeOrderStaff + ",";
            insert += record.applyStaffId + ")";

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

            writeOperatorLog(306, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string billNumber)
        {
            string delete = "DELETE FROM WAREHOUSE_MANAGEMENT_PRO_OCCUPIED WHERE BILL_NUMBER = '" + billNumber + "'"; 

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

        public void billReview(string billNumber, bool isRedBill = false, bool isDispalyMess = true)
        {
            m_isRedBill = isRedBill;

            // 更新单据审核标志，把审核标志置为1
            string update = "UPDATE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED] SET ";

            update += "[ORDERR_REVIEW] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "REVIEW_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_REVIEW = 1";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                if (isDispalyMess)
                {
                    MessageBoxExtend.messageOK("单据[" + billNumber + "]审核成功");
                }

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(306, OperatorLogType.Review, billNumber);
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[TRADING_DATE],[BILL_NUMBER],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[MAKE_ORDER_STAFF],[APPLY_STAFF]";
            sql += ",[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW] ";
            sql += " FROM [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.exchangesUnit = DbDataConvert.ToString(row["EXCHANGES_UNIT"]);

                    record.sumValue = DbDataConvert.ToString(row["SUM_VALUE"]);
                    record.sumMoney = DbDataConvert.ToString(row["SUM_MONEY"]);

                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    record.applyStaffId = DbDataConvert.ToInt32(row["APPLY_STAFF"]);
                    record.applyStaffName = Staff.getInctance().getStaffNameFromPkey(record.applyStaffId);

                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);

                    if (DbDataConvert.ToString(row["ORDERR_REVIEW"]).Length > 0)
                    {
                        record.orderrReview = DbDataConvert.ToInt32(row["ORDERR_REVIEW"]);
                        record.orderrReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderrReview);
                        record.reviewDate = DbDataConvert.ToDateTime(row["REVIEW_DATE"]).ToString("yyyy-MM-dd");
                    }
                    
                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, MaterielProOccupiedOrderTable> getAllMaterielProOccupiedOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, MaterielProOccupiedOrderTable> getAllReviewMaterielProOccupiedOrderInfo()
        {
            SortedDictionary<int, MaterielProOccupiedOrderTable> list = new SortedDictionary<int, MaterielProOccupiedOrderTable>();

            foreach (KeyValuePair<int, MaterielProOccupiedOrderTable> index in m_tableDataList)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public ArrayList getAllReviewMaterielProBillNumFromProjectNum(string projectNum)
        {
            ArrayList tempList = new ArrayList();

            if (m_tableDataList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, MaterielProOccupiedOrderTable> index in m_tableDataList)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();
                record = index.Value;

                if (index.Value.exchangesUnit == projectNum)
                {
                    tempList.Add(index.Value.billNumber);
                }
            }

            return tempList;
        }

        public SortedDictionary<int, MaterielProOccupiedOrderTable> getAllNotReviewMaterielProOccupiedOrderInfo()
        {
            SortedDictionary<int, MaterielProOccupiedOrderTable> list = new SortedDictionary<int, MaterielProOccupiedOrderTable>();

            foreach (KeyValuePair<int, MaterielProOccupiedOrderTable> index in m_tableDataList)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public MaterielProOccupiedOrderTable getMaterielProOccupiedOrderInfoFromBillNumber(string billNumber)
        {
            MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();

            foreach (KeyValuePair<int, MaterielProOccupiedOrderTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    return record;
                }
            }

            return null;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, MaterielProOccupiedOrderTable> index in m_tableDataList)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();

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

            foreach (KeyValuePair<int, MaterielProOccupiedOrderTable> index in m_tableDataList)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();

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

    public class MaterielProOccupiedOrderTable
    {
        // 单据基本信息
        public int pkey { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string exchangesUnit { get; set; }

        // 金额信息
        public string sumValue { get; set; }
        public string sumMoney { get; set; }

        // 申请人
        public int applyStaffId { get; set; }
        public string applyStaffName { get; set; }

        // 制单人
        public int makeOrderStaff { get; set; }
        public string makeOrderStaffName { get; set; }

        // 审核人
        public int orderrReview { get; set; }
        public string isReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
    }
}