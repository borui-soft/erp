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
    public class InitBankBalance : ITableModel
    {
        private SortedDictionary<int, InitBankBalanceTable> m_bankBalanceList = new SortedDictionary<int, InitBankBalanceTable>();

        static private InitBankBalance m_instance = null;

        private InitBankBalance()
        {
            load();
        }

        static public InitBankBalance getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new InitBankBalance();
            }

            return m_instance;
        }

        private void insert(InitBankBalanceTable record)
        {
            string insert = "INSERT INTO [dbo].[INIT_BANK_BALANCE] ([BANK_ID],[ACCOUNT_BALANCE]) VALUES (";

            int bankPkey = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_BANK_LIST", record.bankName);

            insert += bankPkey + ",";
            insert += record.accountBalanec;
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                load();

                // 结转损益表插入银行存款期初数据
                BankBalanceLastMonth.getInctance().delete(bankPkey);
                BankBalanceLastMonth.getInctance().insert(bankPkey, record.accountBalanec, "初始余额");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void delete(int pkey)
        {
            if (deleteRecord("INIT_BANK_BALANCE", pkey))
            {
                load();
            }
        }

        public void update(int pkey, double accountBalanec)
        {
            string update = "UPDATE [dbo].[INIT_BANK_BALANCE] SET ";

            update += "[ACCOUNT_BALANCE] = " + accountBalanec;
            update += " WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
                load();

                // 结转损益表插入银行存款期初数据
                // 结转损益表插入银行存款期初数据
                string bankName = getBankNameFromPkey(pkey);
                int bankID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_BANK_LIST", bankName);
                BankBalanceLastMonth.getInctance().delete(bankID);
                BankBalanceLastMonth.getInctance().insert(bankID, accountBalanec, "初始余额");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        private void load()
        {
            string sql = "SELECT A.PKEY, B.NAME, A.ACCOUNT_BALANCE FROM INIT_BANK_BALANCE A, BASE_BANK_LIST B";
            sql += " WHERE A.BANK_ID = B.PKEY ORDER BY B.PKEY";

            m_bankBalanceList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    InitBankBalanceTable record = new InitBankBalanceTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.bankName = DbDataConvert.ToString(row[1]);
                    record.accountBalanec = DbDataConvert.ToDouble(row[2]);

                    m_bankBalanceList.Add(m_bankBalanceList.Count, record);
                }
            }

            // 判断是否所有的银行卡信息在余额表中都有对应的记录，如果没有，在插入一条默认0的记录进去
            SortedDictionary<int, AuxiliaryMaterialDataTable> bankList =
                AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData("BASE_BANK_LIST");

            if (m_bankBalanceList.Count != bankList.Count)
            {
                foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in bankList)
                {
                    bool isInsertToInitBankBalanceTable = true;

                    foreach (KeyValuePair<int, InitBankBalanceTable> index2 in m_bankBalanceList)
                    {
                        if(index.Value.name == index2.Value.bankName)
                        {
                            isInsertToInitBankBalanceTable = false;
                            break;
                        }
                    }

                    if (isInsertToInitBankBalanceTable)
                    {
                        InitBankBalanceTable bankBaseInfo = new InitBankBalanceTable();

                        bankBaseInfo.bankName = index.Value.name;
                        bankBaseInfo.accountBalanec = 0.00;

                        insert(bankBaseInfo);
                        m_bankBalanceList.Add(m_bankBalanceList.Count, bankBaseInfo);
                    }
                }
            }
        }

        public SortedDictionary<int, InitBankBalanceTable> getAllInitBankBalanceInfo()
        {
            load();

            return m_bankBalanceList;
        }

        public string getBankNameFromPkey(int peky)
        {
            string bankName = "";

            if (m_bankBalanceList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, InitBankBalanceTable> index in m_bankBalanceList)
            {
                if (index.Value.pkey == peky)
                {
                    bankName = index.Value.bankName;
                    break;
                }
            }

            return bankName;
        }
    }

    public class InitBankBalanceTable
    {
        public int pkey { get; set; }
        public string bankName { get; set; }
        public double accountBalanec { get; set; }
    }
}