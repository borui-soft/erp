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

    public class CashBalanceLastMonth : ITableModel
    {
        private SortedDictionary<int, CashBalanceLastMonthTable> m_cashBalanceLastMonthList = new SortedDictionary<int, CashBalanceLastMonthTable>();

        static private CashBalanceLastMonth m_instance = null;

        private CashBalanceLastMonth()
        {
            load();
        }

        static public CashBalanceLastMonth getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new CashBalanceLastMonth();
            }

            return m_instance;
        }

        public void delete()
        {
            string sql = "DELETE FROM [dbo].[CASH_BALANCE_LAST_MONTH]";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, sql);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void insert(double balance, string note)
        {
            string systemDate = DateTime.Now.ToString("yyyyMMdd");

            string insert = "INSERT INTO [dbo].[CASH_BALANCE_LAST_MONTH]([OPERATE_DATE],[YEAR],[MONTH],[BALANCE], [NOTE]) VALUES (";
            insert += "'" + systemDate + "',";
            insert += "'" + systemDate.Substring(0, 4) + "',";
            insert += "'" + systemDate.Substring(4, 2) + "',";
            insert += Convert.ToDouble(balance) + ",";
            insert += "'" + note + "'";
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

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
            string sql = "SELECT [PKEY],[OPERATE_DATE],[YEAR],[MONTH],[BALANCE],[NOTE] FROM [dbo].[CASH_BALANCE_LAST_MONTH] ORDER BY OPERATE_DATE DESC";

            m_cashBalanceLastMonthList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CashBalanceLastMonthTable record = new CashBalanceLastMonthTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.operateDate = DbDataConvert.ToDateTime(row["OPERATE_DATE"]).ToString("yyyy-MM-dd");
                    record.year = DbDataConvert.ToString(row["YEAR"]);
                    record.month= DbDataConvert.ToString(row["MONTH"]);
                    record.balance = DbDataConvert.ToDouble(row["BALANCE"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);

                    m_cashBalanceLastMonthList.Add(m_cashBalanceLastMonthList.Count, record);
                }
            }
        }

        public SortedDictionary<int, CashBalanceLastMonthTable> getAllCashBalanceLastMonthInfo()
        {
            if (m_cashBalanceLastMonthList.Count == 0)
            {
                load();
            }

            return m_cashBalanceLastMonthList;
        }
    }

    public class CashBalanceLastMonthTable
    {
        public int pkey { get; set; }
        public string operateDate { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public double balance { get; set; }
        public string note { get; set; }
    }
}