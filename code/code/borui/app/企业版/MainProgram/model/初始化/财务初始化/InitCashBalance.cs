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
    public class InitCashBalance : ITableModel
    {
        private SortedDictionary<int, InitCashBalanceTable> m_cashBalanceList = new SortedDictionary<int, InitCashBalanceTable>();

        static private InitCashBalance m_instance = null;

        private InitCashBalance()
        {
            load();
        }

        static public InitCashBalance getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new InitCashBalance();
            }

            return m_instance;
        }

        public void insert(double cashBalanec)
        {
            string insert = "INSERT INTO [dbo].[INIT_CASH_BALANCE] ([OBJECT_NAME],[CASH_BALANCE]) VALUES ('库存现金', ";

            insert += cashBalanec;
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                // 结转损益表插入现金期初数据
                CashBalanceLastMonth.getInctance().delete();
                CashBalanceLastMonth.getInctance().insert(cashBalanec, "初始余额");
                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void delete(int pkey)
        {
            if (deleteRecord("INIT_CASH_BALANCE", pkey))
            {
                load();
            }
        }

        public void update(int pkey, double accountBalanec)
        {
            string update = "UPDATE [dbo].[INIT_CASH_BALANCE] SET ";

            update += "[CASH_BALANCE] = " + accountBalanec;
            update += " WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                // 结转损益表插入现金期初数据
                CashBalanceLastMonth.getInctance().delete();
                CashBalanceLastMonth.getInctance().insert(accountBalanec, "初始余额");
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
            string sql = "SELECT [PKEY],[OBJECT_NAME],[CASH_BALANCE] FROM [dbo].[INIT_CASH_BALANCE]";

            m_cashBalanceList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    InitCashBalanceTable record = new InitCashBalanceTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.objectName = DbDataConvert.ToString(row[1]);
                    record.accountBalanec = DbDataConvert.ToDouble(row[2]);

                    m_cashBalanceList.Add(m_cashBalanceList.Count, record);
                }
            }

            // 如果库存现金为空时，插入一条默认数据，且库存现金为0
            if (m_cashBalanceList.Count == 0)
            {
                insert(0.0);
            }
        }

        public SortedDictionary<int, InitCashBalanceTable> getAllInitCashBalanceInfo()
        {
            if (m_cashBalanceList.Count == 0)
            {
                load();
            }

            return m_cashBalanceList;
        }
    }

    public class InitCashBalanceTable
    {
        public int pkey { get; set; }
        public string objectName { get; set; }
        public double accountBalanec { get; set; }
    }
}