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
    public class PurchaseOrder : ITableModel
    {
        private SortedDictionary<int, PurchaseOrderTable> m_tableDataList = new SortedDictionary<int, PurchaseOrderTable>();

        static private PurchaseOrder m_instance = null;

        private PurchaseOrder()
        {
            load();
        }

        static public PurchaseOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new PurchaseOrder();
            }

            return m_instance;
        }

        public void insert(PurchaseOrderTable record, bool isDisplayMessageBox = true)
        {
            string insert = "INSERT INTO [dbo].[PURCHASE_ORDER]([SUPPLIER_ID],[TRADING_DATE],[BILL_NUMBER],[PURCHASE_TYPE],";
            insert += "[DELIVERY_DATE],[PAYMENT_DATE],[PROJECT_NUM],[SRC_ORDER_NUM],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],[TOTAL_MONEY],";
            insert += "[BUSINESS_PEOPLE_ID],[MAKE_ORDER_STAFF],[IS_REVIEW],[IS_IN_STORAGE],[ACTUAL_VALUE]) VALUES(";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            insert += record.supplierId + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.purchaseType + "',";
            insert += "'" + record.deliveryDate + "',";
            insert += "'" + record.paymentDate + "',";

            insert += "'" + record.xxMaterielTableNum + "',";
            insert += "'" + record.srcOrderNum + "',";

            insert += "'" + record.exchangesUnit + "',";
            insert += "'" + record.sumValue + "',";
            insert += "'" + record.sumMoney + "',";
            insert += "'" + record.sumTransportationCost + "',";
            insert += "'" + record.sumOtherCost + "',";
            insert += "'" + record.totalMoney + "',";
            insert += record.businessPeopleId + ",";
            insert += record.makeOrderStaff + ", 0,0,0";
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
            string delete = "DELETE FROM PURCHASE_ORDER WHERE BILL_NUMBER = '" + biilNumber + "'"; 

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
            string update = "UPDATE [dbo].[PURCHASE_ORDER] SET ";

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

        public void updataActualValue(string billNumber, double actualValue)
        {
            string update = "UPDATE [dbo].[PURCHASE_ORDER] SET ";
            update += "IS_IN_STORAGE = 1, ACTUAL_VALUE = " + actualValue;
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

        public void refreshRecord()
        {
            load();
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[SUPPLIER_ID],[TRADING_DATE],[BILL_NUMBER],[PURCHASE_TYPE],[DELIVERY_DATE],[PAYMENT_DATE],";
            sql += "[PROJECT_NUM],[SRC_ORDER_NUM],[EXCHANGES_UNIT],[BUSINESS_PEOPLE_ID],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],";
            sql += "[TOTAL_MONEY], [MAKE_ORDER_STAFF],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW],[IS_IN_STORAGE],[ACTUAL_VALUE] ";
            sql += "FROM [dbo].[PURCHASE_ORDER] ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    PurchaseOrderTable record = new PurchaseOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.supplierId = DbDataConvert.ToInt32(row["SUPPLIER_ID"]);
                    record.supplierName = Supplier.getInctance().getSupplierNameFromPkey(record.supplierId);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.purchaseType = DbDataConvert.ToString(row["PURCHASE_TYPE"]);
                    record.deliveryDate = DbDataConvert.ToDateTime(row["DELIVERY_DATE"]).ToString("yyyy-MM-dd");
                    record.paymentDate = DbDataConvert.ToDateTime(row["PAYMENT_DATE"]).ToString("yyyy-MM-dd");
                    record.xxMaterielTableNum = DbDataConvert.ToString(row["PROJECT_NUM"]);
                    record.srcOrderNum = DbDataConvert.ToString(row["SRC_ORDER_NUM"]);
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

                    // 是否已入库和实际入库数量
                    record.isInStorage = DbDataConvert.ToString(row["IS_IN_STORAGE"]);
                    record.actualValue = DbDataConvert.ToString(row["ACTUAL_VALUE"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, PurchaseOrderTable> getAllPurchaseOrderInfoFromProjectNum(string projectNum)
        {
            SortedDictionary<int, PurchaseOrderTable> list = new SortedDictionary<int, PurchaseOrderTable>();

            foreach (KeyValuePair<int, PurchaseOrderTable> index in m_tableDataList)
            {
                PurchaseOrderTable record = new PurchaseOrderTable();
                record = index.Value;

                if (index.Value.xxMaterielTableNum.IndexOf(projectNum) >= 0)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, PurchaseOrderTable> getAllPurchaseOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, PurchaseOrderTable> getAllReviewPurchaseOrderInfo()
        {
            SortedDictionary<int, PurchaseOrderTable> list = new SortedDictionary<int, PurchaseOrderTable>();

            foreach (KeyValuePair<int, PurchaseOrderTable> index in m_tableDataList)
            {
                PurchaseOrderTable record = new PurchaseOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, PurchaseOrderTable> getAllNotReviewPurchaseOrderInfo()
        {
            SortedDictionary<int, PurchaseOrderTable> list = new SortedDictionary<int, PurchaseOrderTable>();

            foreach (KeyValuePair<int, PurchaseOrderTable> index in m_tableDataList)
            {
                PurchaseOrderTable record = new PurchaseOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public PurchaseOrderTable getPurchaseInfoFromBillNumber(string billNumber)
        {
            PurchaseOrderTable record = new PurchaseOrderTable();

            foreach (KeyValuePair<int, PurchaseOrderTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, PurchaseOrderTable> index in m_tableDataList)
            {
                PurchaseOrderTable record = new PurchaseOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class PurchaseOrderTable
    {
        public int pkey { get; set; }
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string purchaseType { get; set; }
        public string deliveryDate { get; set; }
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

        public string isInStorage { get; set; }
        public string actualValue { get; set; }

        // 增加项目编号和原始项目编号
        public string xxMaterielTableNum { get; set; }
        public string srcOrderNum { get; set; }
    }
}