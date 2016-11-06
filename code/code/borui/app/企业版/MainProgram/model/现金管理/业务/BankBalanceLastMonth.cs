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

    public class BankBalanceLastMonth : ITableModel
    {
        private SortedDictionary<int, BankBalanceLastMonthTable> m_bankBalanceLastMonthList = new SortedDictionary<int, BankBalanceLastMonthTable>();

        static private BankBalanceLastMonth m_instance = null;

        private BankBalanceLastMonth()
        {
            load();
        }

        static public BankBalanceLastMonth getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new BankBalanceLastMonth();
            }

            return m_instance;
        }

        public void delete(int bankID)
        {
            string sql = "DELETE FROM [dbo].[BANK_BALANCE_LAST_MONTH] WHERE BANK_ID = " + Convert.ToInt32(bankID);

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

        public void insert(int bankID, double balance, string note)
        {
            string systemDate = DateTime.Now.ToString("yyyyMMdd");

            string insert = "INSERT INTO [dbo].[BANK_BALANCE_LAST_MONTH]([BANK_ID], [OPERATE_DATE],[YEAR],[MONTH],[BALANCE],[NOTE]) VALUES (";
            
            insert += Convert.ToInt32(bankID) + ",";
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
            string sql = "SELECT [PKEY],[OPERATE_DATE],[YEAR],[MONTH],[BANK_ID], [BALANCE], [NOTE] FROM [dbo].[BANK_BALANCE_LAST_MONTH] ORDER BY OPERATE_DATE DESC";

            m_bankBalanceLastMonthList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BankBalanceLastMonthTable record = new BankBalanceLastMonthTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.operateDate = DbDataConvert.ToDateTime(row["OPERATE_DATE"]).ToString("yyyy-MM-dd");
                    record.year = DbDataConvert.ToString(row["YEAR"]);
                    record.month= DbDataConvert.ToString(row["MONTH"]);
                    record.bankID = DbDataConvert.ToInt32(row["BANK_ID"]);
                    record.bankName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_BANK_LIST", record.bankID);
                    record.balance = DbDataConvert.ToDouble(row["BALANCE"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);

                    m_bankBalanceLastMonthList.Add(m_bankBalanceLastMonthList.Count, record);
                }
            }
        }

        public SortedDictionary<int, BankBalanceLastMonthTable> getAllBankBalanceLastMonthInfo()
        {
            if (m_bankBalanceLastMonthList.Count == 0)
            {
                load();
            }

            return m_bankBalanceLastMonthList;
        }
    }

    public class BankBalanceLastMonthTable
    {
        public int pkey { get; set; }
        public string operateDate { get; set; }
        public int bankID { get; set; }
        public string bankName { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public double balance { get; set; }
        public string note { get; set; }
    }
}