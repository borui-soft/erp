using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    class BalanceManager
    {
        #region 现金相余额
        // 得到当前系统现金账余额
        public static double getCachBalance()
        {
            double balance = -1;

            // 查询现金明细账中，最后一笔通过的审核的单据余额，就是当前系统的库存现金余额
            SortedDictionary<int, CashCashsubLedgerTable> cashCashsubLedgerList = 
                CashCashsubLedger.getInctance().getAllReviewCashCashsubLedgerInfo();

            foreach (KeyValuePair<int, CashCashsubLedgerTable> index in cashCashsubLedgerList)
            {
                balance = index.Value.balance;
            }

            if (balance == -1)
            {
                // 如果系统中还没有任何一笔通过审核的现金交易,则查询 现金结存信息账(上月结转)表，得到当前系统的现余额
                balance = getCashBalanceLaseMonth();
            }

            return balance;
        }

        public static double getCashBalanceLaseMonth()
        {
            double balance = 0;

            SortedDictionary<int, CashBalanceLastMonthTable> CashBalanceLastMonthList
                = CashBalanceLastMonth.getInctance().getAllCashBalanceLastMonthInfo();

            if (CashBalanceLastMonthList.Count > 0)
            {
                balance = CashBalanceLastMonthList[0].balance;
            }

            return balance;
        }
        # endregion
        # region 银行存款相余额
        // 得到当前系统某银行账户余额
        public static double getBankBalance(string bankName)
        {
            double balance = -1;

            // 查询银行存款明细账中对应账户余额信息，最后一笔通过的审核的单据余额，就是当前账户银行存款余额
            SortedDictionary<int, BankCashsubLedgerTable> bankCashsubLedgerList =
                BankCashsubLedger.getInctance().getAllReviewBankCashsubLedgerInfo();

            foreach (KeyValuePair<int, BankCashsubLedgerTable> index in bankCashsubLedgerList)
            {
                if (index.Value.bankName == bankName)
                {
                    balance = index.Value.balance;
                }
            }

            if (balance == -1)
            {
                // 如果系统中还没有任何一笔通过审核的银行交易,则查询 银行结存信息账(上月结转)表，得到当前系统的银行存款余额
                balance = getBankBalanceLaseMonth(bankName);
            }

            return balance;
        }

        public static double getBankBalanceLaseMonth(string bankName)
        {
            double balance = 0;

            SortedDictionary<int, BankBalanceLastMonthTable> bankBalanceLastMonthList
                = BankBalanceLastMonth.getInctance().getAllBankBalanceLastMonthInfo();

            foreach (KeyValuePair<int, BankBalanceLastMonthTable> index in bankBalanceLastMonthList)
            {
                if (index.Value.bankName == bankName)
                {
                    balance = index.Value.balance;
                    break;
                }
            }

            return balance;
        }

        public static double getAllBankBalance()
        {
            double balance = 0.0;

            // 得到所有银行信息
            SortedDictionary<int, AuxiliaryMaterialDataTable> bankList
                = AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData("BASE_BANK_LIST");

            foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in bankList)
            {
                // 得到每个银行的当前存款金额，并累加
                balance += getBankBalance(index.Value.name);
            }

            return balance;
        }
        # endregion

        #region 应收应付相关金额余额
        public static double getSumAccountPayable()
        {
            double balance = 0;

            string query  = "SELECT SUM(BALANCE) FROM INIT_ACCOUNT_PAYABLE"; 
            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                if (dataTable.Rows.Count > 0)
                {
                    balance = DbDataConvert.ToDouble(dataTable.Rows[0][0]);
                }
            }

            return balance;
        }

        public static double getSumAccountReceiveable()
        {
            double balance = 0;

            string query = "SELECT SUM(BALANCE) FROM INIT_ACCOUNT_RECEIVABLE";
            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                if (dataTable.Rows.Count > 0)
                {
                    balance = DbDataConvert.ToDouble(dataTable.Rows[0][0]);
                }
            }

            return balance;
        }
        # endregion

        #region 库存货物金额余额
        
        public static double getSumStorageStockBalance()
        {
            double balance = 0;

            string query = "SELECT SUM(VALUE * Convert(FLOAT,PRICE)) FROM INIT_STORAGE_STOCK";
            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                if (dataTable.Rows.Count > 0)
                {
                    balance = DbDataConvert.ToDouble(dataTable.Rows[0][0]);
                }
            }

            return balance;
        }

        # endregion
    }
}