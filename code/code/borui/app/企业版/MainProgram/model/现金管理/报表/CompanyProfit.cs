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

    public class CompanyProfit : ITableModel
    {
        static private CompanyProfit m_instance = null;
        private SortedDictionary<int, CompanyProfitTable> m_companyProfitDataList = new SortedDictionary<int, CompanyProfitTable>();

        private CompanyProfit()
        {
            load();
        }

        static public CompanyProfit getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new CompanyProfit();
            }

            return m_instance;
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[DATE],[DATA_SOURCE],[CASH_BALANCE],[BANK_BALANCE],[SUM_RECEIVABLE]";
            sql +=",[SUM_PAYABLE],[SUM_STORAGE_STOCK] FROM [dbo].[COMPANY_PROFIT] ORDER BY DATE";

            m_companyProfitDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CompanyProfitTable record = new CompanyProfitTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.date = DbDataConvert.ToDateTime(row["DATE"]).ToString("yyyy-MM-dd");
                    record.dataSource = DbDataConvert.ToString(row["DATA_SOURCE"]);
                    record.cashBalance = DbDataConvert.ToDouble(row["CASH_BALANCE"]);
                    record.bankBalance = DbDataConvert.ToDouble(row["BANK_BALANCE"]);
                    record.sumReceivable = DbDataConvert.ToDouble(row["SUM_RECEIVABLE"]);
                    record.sumPayable = DbDataConvert.ToDouble(row["SUM_PAYABLE"]);
                    record.sumStorageStock = DbDataConvert.ToDouble(row["SUM_STORAGE_STOCK"]);

                    m_companyProfitDataList.Add(m_companyProfitDataList.Count, record);
                }
            }
        }

        // 当执行结转损益操作的时，执行此操作
        public void insertCashInvoiceData()
        {
            string insert = "INSERT INTO [dbo].[COMPANY_PROFIT]([DATE],[DATA_SOURCE],[CASH_BALANCE],[BANK_BALANCE]";
            insert += ",[SUM_RECEIVABLE],[SUM_PAYABLE],[SUM_STORAGE_STOCK]) VALUES (";

            insert += "'" + DateTime.Now.ToString("yyyyMMdd") + "',";
            insert += "'结转损益',";
            insert += Convert.ToString(BalanceManager.getCachBalance()) + ",";
            insert += Convert.ToString(BalanceManager.getAllBankBalance()) + ",";
            insert += Convert.ToString(BalanceManager.getSumAccountReceiveable()) + ",";
            insert += Convert.ToString(BalanceManager.getSumAccountPayable()) + ",";
            insert += Convert.ToString(BalanceManager.getSumStorageStockBalance());
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);
                load();
            }
            catch (Exception)
            {
                return;
            }
        }

        public void initAssetsSystemBalanceInfo()
        {
            if (m_companyProfitDataList.Count == 0)
            {
                insertNullInitDate();
            }

            string update = "UPDATE [dbo].[COMPANY_PROFIT] SET ";
            update += "[DATE] = '" + DateTime.Now.ToString("yyyyMMdd") + "',";
            update += "[CASH_BALANCE] = " + BalanceManager.getCashBalanceLaseMonth() + ",";
            update += "[BANK_BALANCE] = " + BalanceManager.getAllBankBalance();
            update += " WHERE [DATA_SOURCE] = '初始数据'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
                load();
            }
            catch (Exception)
            {
                return;
            }
        }

        public void initBusinessSystemBalanceInfo()
        {
            if (m_companyProfitDataList.Count == 0)
            {
                insertNullInitDate();
            }

            string update = "UPDATE [dbo].[COMPANY_PROFIT] SET ";
            update += "[DATE] = '" + DateTime.Now.ToString("yyyyMMdd") + "',";
            update += "[SUM_RECEIVABLE] = " + BalanceManager.getSumAccountReceiveable() + ",";
            update += "[SUM_PAYABLE] = " + BalanceManager.getSumAccountPayable() + ",";
            update += "[SUM_STORAGE_STOCK] = " + BalanceManager.getSumStorageStockBalance();
            update += " WHERE [DATA_SOURCE] = '初始数据'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
                load();
            }
            catch (Exception)
            {
                return;
            }
        }

        public void insertNullInitDate()
        {
            string insert = "INSERT INTO [dbo].[COMPANY_PROFIT]([DATE],[DATA_SOURCE],[CASH_BALANCE],[BANK_BALANCE]";
            insert += ",[SUM_RECEIVABLE],[SUM_PAYABLE],[SUM_STORAGE_STOCK]) VALUES (";

            insert += "'" + DateTime.Now.ToString("yyyyMMdd") + "',";
            insert += "'初始数据',";
            insert += "0, 0, 0, 0, 0)";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);
                load();
            }
            catch (Exception)
            {
                return;
            }
        }

        public SortedDictionary<int, CompanyProfitTable> getAllCompanyProfitData()
        {   
            if (m_companyProfitDataList.Count == 0)
            {
                load();
            }

            return m_companyProfitDataList;
        }
    }

    public class CompanyProfitTable
    {
        public int pkey { get; set; }
        public string date { get; set; }
        public string dataSource { get; set; }
        public double cashBalance { get; set; }
        public double bankBalance { get; set; }
        public double sumReceivable { get; set; }
        public double sumPayable { get; set; }
        public double sumStorageStock { get; set; }
    }
}