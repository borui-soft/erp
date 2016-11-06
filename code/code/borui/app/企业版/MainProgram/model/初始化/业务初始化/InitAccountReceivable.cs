using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public class InitAccountReceivable : ITableModel
    {
        private SortedDictionary<int, InitAccountReceivableTable> m_accountReceivableList = new SortedDictionary<int, InitAccountReceivableTable>();

        static private InitAccountReceivable m_instance = null;

        private InitAccountReceivable()
        {
            load();
        }

        static public InitAccountReceivable getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new InitAccountReceivable();
            }

            return m_instance;
        }

        public void insert(InitAccountReceivableTable record, bool isDisplayMessage = true, bool isUpdateCashAccountReceivableDatail = true)
        {
            string insert = "INSERT INTO INIT_ACCOUNT_RECEIVABLE(TRADING_DATE, CUSTOMER_ID, BALANCE) VALUES (";

            // 检查数据库是否已经存在该客户应收账款信息，如果存在，本次只是累计欠款金额，然后执行更新操作
            if (checkcustomerOrSupplierIDIsExist(record.customerOrSupplierID))
            {
                InitAccountReceivableTable storageExistRecord = getInfoFromCustomerOrSuoolierID(record.customerOrSupplierID);
                InitAccountReceivableTable newRecord = new InitAccountReceivableTable();

                newRecord.pkey = storageExistRecord.pkey;
                newRecord.customerOrSupplierID = record.customerOrSupplierID;
                newRecord.tradingDate = record.tradingDate;
                newRecord.balance = storageExistRecord.balance + record.balance;

                update(newRecord.pkey, newRecord);
                return;
            }

            insert += "'" + record.tradingDate + "',";
            insert += record.customerOrSupplierID + ",";
            insert += record.balance;
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                load();

                if (isUpdateCashAccountReceivableDatail)
                {
                    insertToCashAccountReceivableDatail(record);
                }

                if (isDisplayMessage)
                {
                    MessageBoxExtend.messageOK("数据保存成功");
                }
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void delete(int pkey)
        {
            if (deleteRecord("INIT_ACCOUNT_RECEIVABLE", pkey))
            {
                load();
            }
        }

        public void update(int pkey, InitAccountReceivableTable record, bool isUpdateCashAccountReceivableDatail = true)
        {
            string update = "UPDATE [dbo].[INIT_ACCOUNT_RECEIVABLE] SET ";

            update += "[BALANCE] = " + record.balance;
            update += ", [TRADING_DATE] = '" + record.tradingDate+ "'";
            update += " WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
                load();

                if (isUpdateCashAccountReceivableDatail)
                {
                    insertToCashAccountReceivableDatail(record);
                }
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        private void load()
        {
            string sql = "SELECT A.PKEY, A.CUSTOMER_ID, B.NAME, A.BALANCE, A.TRADING_DATE, B.CREDIT FROM INIT_ACCOUNT_RECEIVABLE A, BASE_CUSTOMER_LIST B ";
            sql += "WHERE A.CUSTOMER_ID = B.PKEY ORDER BY A.PKEY";

            m_accountReceivableList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    InitAccountReceivableTable record = new InitAccountReceivableTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.customerOrSupplierID= DbDataConvert.ToInt32(row[1]);
                    record.name = DbDataConvert.ToString(row[2]);
                    record.balance = DbDataConvert.ToDouble(row[3]);
                    record.tradingDate = DbDataConvert.ToDateTime(row[4]).ToString("yyyy-MM-dd");
                    record.credit = DbDataConvert.ToDouble(row[5]);

                    m_accountReceivableList.Add(m_accountReceivableList.Count, record);
                }
            }
        }

        public SortedDictionary<int, InitAccountReceivableTable> getAllInitAccountReceivableInfo()
        {
            if (m_accountReceivableList.Count == 0)
            {
                load();
            }

            return m_accountReceivableList;
        }

        public InitAccountReceivableTable getAccountReceivableInfoFromPkey(int pkey)
        {
            InitAccountReceivableTable accountReceivableRecord = new InitAccountReceivableTable();

            foreach (KeyValuePair<int, InitAccountReceivableTable> index in m_accountReceivableList)
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    accountReceivableRecord = record;
                    break;
                }
            }

            return accountReceivableRecord;
        }

        public int getCustomerOrSupplierIDFromPkey(int pkey)
        {
            int customerOrSupplierID = -1;

            foreach (KeyValuePair<int, InitAccountReceivableTable> index in m_accountReceivableList)
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    customerOrSupplierID = record.customerOrSupplierID;
                    break;
                }
            }

            return customerOrSupplierID;
        }

        public bool checkcustomerOrSupplierIDIsExist(int customerOrSupplierID)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, InitAccountReceivableTable> index in m_accountReceivableList)
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();
                record = index.Value;

                if (record.customerOrSupplierID == customerOrSupplierID)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public InitAccountReceivableTable getInfoFromCustomerOrSuoolierID(int customerOrSupplierID)
        {
            InitAccountReceivableTable data = new InitAccountReceivableTable();

            foreach (KeyValuePair<int, InitAccountReceivableTable> index in m_accountReceivableList)
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();
                record = index.Value;

                if (record.customerOrSupplierID == customerOrSupplierID)
                {
                    data = record;
                    break;
                }
            }

            return data;
        }

        public void fileImport(string filePath)
        {
            int rowIndex = 0;

            try
            {
                // 正式导入数据前，首先情况INIT_STORAGE_STOCK表数据
                DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, "DELETE FROM [dbo].[INIT_ACCOUNT_RECEIVABLE]");

                string sheetName = "应收账款初始数据";

                if (ExcelDocProc.getInstance().openFile(filePath))
                {
                    string supplierID, value, date;
                    for (rowIndex = 0; ; rowIndex++)
                    {
                        supplierID = ExcelDocProc.getInstance().getGridValue(sheetName, rowIndex, 0);
                        value = ExcelDocProc.getInstance().getGridValue(sheetName, rowIndex, 1);
                        date = ExcelDocProc.getInstance().getGridValue(sheetName, rowIndex, 2);

                        if (supplierID.Length == 0)
                        {
                            break;
                        }
                        else
                        {
                            InitAccountReceivableTable record = new InitAccountReceivableTable();
                            record.customerOrSupplierID = Convert.ToInt32(supplierID);
                            record.balance = Convert.ToDouble(value);
                            record.tradingDate = date;

                            insert(record, false);
                        }
                    }

                    MessageBoxExtend.messageOK("应收账款初始数据导入成功");
                }
            }
            catch (Exception)
            {
                MessageBoxExtend.messageWarning("文件导入失败，[" + Convert.ToString(rowIndex) + "]行数据有误，请仔细核对");
                return;
            }
        }

        private void insertToCashAccountReceivableDatail(InitAccountReceivableTable record)
        {
            // 把期初的客户欠款余额信息更新到应收账款明细表中。作为使用在现金管理里面查询企业跟该客户往来账的依据
            CashAccountReceivableDetailTable data = new CashAccountReceivableDetailTable();
            data.billTypeName = "期初数据";
            data.customerOrSupplierID = record.customerOrSupplierID;
            data.turnover = record.balance;
            data.balance = record.balance;

            CashAccountReceivableDetail.getInctance().insert(data);
        }
    }


    public class InitAccountReceivableTable
    {
        public int pkey { get; set; }
        public int customerOrSupplierID { get; set; }
        public string name { get; set; }
        // 交易日期
        public string tradingDate { get; set; }

        // 交易金额
        public double balance { get; set; }

        // 信用额度
        public double credit { get; set; }
    }
}