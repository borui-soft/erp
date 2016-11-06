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

    public class CashAccountReceivableDetail : ITableModel
    {
        static private CashAccountReceivableDetail m_instance = null;
        private SortedDictionary<int, CashAccountReceivableDetailTable> m_accountReceivableDetailList = new SortedDictionary<int, CashAccountReceivableDetailTable>();

        private CashAccountReceivableDetail()
        {
            load();
        }

        static public CashAccountReceivableDetail getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new CashAccountReceivableDetail();
            }

            return m_instance;
        }

        private void load()
        {
            string sql = "SELECT A.PKEY, A.CUSTOMER_ID, B.NAME, A.BILL_TYPE_NAME, A.TRADING_DATE, A.BILL_NUMBER, A.TURNOVER, ";
            sql += "A.BALANCE, A.STAFF_ID, C.NAME AS STAFF_NAME, A.NOTE FROM CASH_ACCOUNT_RECEIVABLE_DETAIL A, BASE_CUSTOMER_LIST B, BASE_STAFF_LIST C ";
            sql += "WHERE A.CUSTOMER_ID = B.PKEY AND A.STAFF_ID = C.PKEY";
            sql += " ORDER BY A.TRADING_DATE;";

            m_accountReceivableDetailList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CashAccountReceivableDetailTable record = new CashAccountReceivableDetailTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.customerOrSupplierID = DbDataConvert.ToInt32(row["CUSTOMER_ID"]);
                    record.name= DbDataConvert.ToString(row["NAME"]);
                    record.billTypeName = DbDataConvert.ToString(row["BILL_TYPE_NAME"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.turnover = DbDataConvert.ToDouble(row["TURNOVER"]);
                    record.balance = DbDataConvert.ToDouble(row["BALANCE"]);
                    record.staffID = DbDataConvert.ToInt32(row["STAFF_ID"]);
                    record.staffName = DbDataConvert.ToString(row["STAFF_NAME"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);

                    m_accountReceivableDetailList.Add(m_accountReceivableDetailList.Count, record);
                }
            }
        }

        public void insert(CashAccountReceivableDetailTable record, bool isUpdateInitAccountReceivableTable = true)
        {
            string insert = "INSERT INTO [dbo].[CASH_ACCOUNT_RECEIVABLE_DETAIL]([CUSTOMER_ID]";
            insert += ",[BILL_TYPE_NAME],[BILL_NUMBER],[TURNOVER],[BALANCE],[STAFF_ID],[NOTE]) VALUES(";

            insert += record.customerOrSupplierID + ",";
            insert += "'" + record.billTypeName + "',";
            insert += "'" + record.billNumber + "',";
            insert += record.turnover + ",";

            if (isUpdateInitAccountReceivableTable)
            {
                insert += getNewBalance(record) + ",";
            }
            else
            {
                insert += record.turnover + ",";
            }

            insert += DbPublic.getInctance().getCurrentLoginUserID() + ",";
            insert += "'" + record.note + "'";
            insert += ")";

            try
            {
                if (record.billTypeName == "期初数据")
                {
                    // 如果单据类型是期初数据，插入之前应该删除掉之前存在的该客户的期初欠款记录信息，有一个原则就是
                    // 一个客户的期初欠款记录在数据库中只有一条
                    string delete = "DELETE FROM CASH_ACCOUNT_RECEIVABLE_DETAIL WHERE BILL_TYPE_NAME = '期初数据' AND CUSTOMER_ID = ";
                    delete += record.customerOrSupplierID;

                    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, delete);
                }
            
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public SortedDictionary<int, CashAccountReceivableDetailTable> getAccountReceivableDetailFromCustomerID(int customerID)
        {
            SortedDictionary<int, CashAccountReceivableDetailTable> list = new SortedDictionary<int, CashAccountReceivableDetailTable>();
            
            if (m_accountReceivableDetailList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, CashAccountReceivableDetailTable> index in m_accountReceivableDetailList)
            {
                CashAccountReceivableDetailTable record = new CashAccountReceivableDetailTable();
                record = index.Value;

                if (record.customerOrSupplierID == customerID)
                {
                    list.Add(list.Count, record);
                }
            }

            return list;
        }

        private double getNewBalance(CashAccountReceivableDetailTable record)
        {
            double newBalance = 0;
            double oldBalance = 0;
            /*
             * 函数逻辑
             * 本次交易后欠款余额的最终结果取决于本次交易单据的类型
             * 如果本张单据是期初数据，交易后欠款等于本次交易额
             * 如果本张单据是付款单，交易后欠款月等于之前的欠款额减去本次交易额
             * 如果本张单据是销售出库，交易后欠款余额等于之前的余额加上本次交易额
             * */

            if (record.billTypeName.IndexOf("期初数据") != -1)
            {
                newBalance = record.balance;
            }
            else
            {
                InitAccountReceivableTable temp = InitAccountReceivable.getInctance().getInfoFromCustomerOrSuoolierID(record.customerOrSupplierID);

                if (temp != null)
                {
                    oldBalance = temp.balance;

                }

                if (record.billTypeName.IndexOf("收款") != -1 ||
                    record.billTypeName == "销售退货")
                {
                    newBalance = oldBalance - record.turnover;
                }
                else
                {
                    newBalance = oldBalance + record.turnover;
                }

                InitAccountReceivableTable newRecord = new InitAccountReceivableTable();
                newRecord.balance = newBalance;
                newRecord.tradingDate = record.tradingDate;
                newRecord.customerOrSupplierID = record.customerOrSupplierID;

                InitAccountReceivable.getInctance().update(temp.pkey, newRecord, false);
            }

            return newBalance;
        }
    }

    public class CashAccountReceivableDetailTable
    {
        public int pkey { get; set; }
        public int customerOrSupplierID { get; set; }
        public string name { get; set; }
        public string billTypeName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public double turnover { get; set; }
        public double balance { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string note { get; set; }
    }
}