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
    public class SaleOrder : ITableModel
    {
        private SortedDictionary<int, SaleOrderTable> m_tableDataList = new SortedDictionary<int, SaleOrderTable>();

        static private SaleOrder m_instance = null;

        private SaleOrder()
        {
            load();
        }

        static public SaleOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SaleOrder();
            }

            return m_instance;
        }

        public void insert(SaleOrderTable record, bool isDisplayMessageBox = true)
        {
            string insert = "INSERT INTO [dbo].[SALE_ORDER]([CUSTOMER_ID],[TRADING_DATE],[BILL_NUMBER],[SALE_TYPE],";
            insert += "[DELIVERY_DATE],[PAYMENT_DATE],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],[TOTAL_MONEY],";
            insert += "[BUSINESS_PEOPLE_ID],[MAKE_ORDER_STAFF],[IS_REVIEW],[IS_OUT_STORAGE],[ACTUAL_VALUE]) VALUES(";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            insert += record.customerId + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.saleType + "',";
            insert += "'" + record.deliveryDate + "',";
            insert += "'" + record.paymentDate + "',";
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

            writeOperatorLog(202, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string biilNumber)
        {
            string delete = "DELETE FROM SALE_ORDER WHERE BILL_NUMBER = '" + biilNumber + "'"; 

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
            string update = "UPDATE [dbo].[SALE_ORDER] SET ";

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

            writeOperatorLog(202, OperatorLogType.Review, billNumber);
        }

        public void updataActualValue(string billNumber, double actualValue)
        {
            string update = "UPDATE [dbo].[SALE_ORDER] SET ";
            update += "IS_OUT_STORAGE = 1, ACTUAL_VALUE = " + actualValue;
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

        private void load()
        {
            string sql = "SELECT [PKEY],[CUSTOMER_ID],[TRADING_DATE],[BILL_NUMBER],[SALE_TYPE],[DELIVERY_DATE],[PAYMENT_DATE],";
            sql += "[EXCHANGES_UNIT],[BUSINESS_PEOPLE_ID],[SUM_VALUE],[SUM_MONEY],[SUM_TRANSPORTATION_COST],[SUM_OTHER_COST],";
            sql += "[TOTAL_MONEY], [MAKE_ORDER_STAFF],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW],[IS_OUT_STORAGE],[ACTUAL_VALUE] ";
            sql += "FROM [dbo].[SALE_ORDER] ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleOrderTable record = new SaleOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.customerId = DbDataConvert.ToInt32(row["CUSTOMER_ID"]);
                    record.customerName = Customer.getInctance().getCustomerNameFromPkey(record.customerId);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.saleType = DbDataConvert.ToString(row["SALE_TYPE"]);
                    record.deliveryDate = DbDataConvert.ToDateTime(row["DELIVERY_DATE"]).ToString("yyyy-MM-dd");
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

                    // 是否已入库和实际入库数量
                    record.isInStorage = DbDataConvert.ToString(row["IS_OUT_STORAGE"]);
                    record.actualValue = DbDataConvert.ToString(row["ACTUAL_VALUE"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, SaleOrderTable> getAllSaleOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, SaleOrderTable> getAllReviewSaleOrderInfo()
        {
            SortedDictionary<int, SaleOrderTable> list = new SortedDictionary<int, SaleOrderTable>();

            foreach (KeyValuePair<int, SaleOrderTable> index in m_tableDataList)
            {
                SaleOrderTable record = new SaleOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, SaleOrderTable> getAllNotReviewSaleOrderInfo()
        {
            SortedDictionary<int, SaleOrderTable> list = new SortedDictionary<int, SaleOrderTable>();

            foreach (KeyValuePair<int, SaleOrderTable> index in m_tableDataList)
            {
                SaleOrderTable record = new SaleOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SaleOrderTable getSaleInfoFromBillNumber(string billNumber)
        {
            SaleOrderTable record = new SaleOrderTable();

            foreach (KeyValuePair<int, SaleOrderTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, SaleOrderTable> index in m_tableDataList)
            {
                SaleOrderTable record = new SaleOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class SaleOrderTable
    {
        public int pkey { get; set; }
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string saleType { get; set; }
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
    }
}