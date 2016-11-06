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

    public class BankCashsubLedger : ITableModel
    {
        private SortedDictionary<int, BankCashsubLedgerTable> m_bankCashsubLedgerList = new SortedDictionary<int, BankCashsubLedgerTable>();

        static private BankCashsubLedger m_instance = null;

        private BankCashsubLedger()
        {
            load();
        }

        static public BankCashsubLedger getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new BankCashsubLedger();
            }

            return m_instance;
        }

        public void insert(BankCashsubLedgerTable record)
        {
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            string insert = "INSERT INTO [dbo].[BANK_CASHSUB_LEDGER]([TRADING_DATE],[BANK_ID], [BILL_NUMBER],[VOUCHERS_NUMBER],[BILL_NAME],";
            insert += "[BILL_TYPE_ID],[SOURCE_BILL_NUMBER],[EXCHANGES_UNIT],[TURNOVER],[MAKE_ORDER_STAFF],[IS_REVIEW],[NOTE]) VALUES(";

            insert += "'" + record.tradingDate + "',";
            insert += record.bankID + ",";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.vouchersNumber + "',";
            insert += "'" + record.billName + "',";
            insert += record.billTypeID + ",";
            insert += "'" + record.sourceBillNumber + "',";
            insert += record.exchangesUnit + ",";
            insert += record.turnover + ",";
            insert += record.makeOrderStaff + ", 0,";
            insert += "'" + record.note + "'";
            insert += ")";


            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                load();

                MessageBoxExtend.messageOK("数据保存成功");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void delete(string billNumber)
        {
            try
            {
                string sql = "DELETE FROM BANK_CASHSUB_LEDGER WHERE BILL_NUMBER = '" + billNumber + "'";

                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, sql);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void update(string billNubmber, int reviewStaffID, double balance)
        {
            string update = "UPDATE [dbo].[BANK_CASHSUB_LEDGER] SET ";

            update += "[ORDERR_EVIEW] = " + reviewStaffID;
            update += ", [BALANCE] = " + balance;
            update += ", [IS_REVIEW] = 1";
            update += " WHERE BILL_NUMBER = '" + billNubmber + "'";

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
            string sql = "SELECT [PKEY],[TRADING_DATE],[BANK_ID], [BILL_NUMBER],[VOUCHERS_NUMBER],[BILL_NAME],[BILL_TYPE_ID],[SOURCE_BILL_NUMBER],[EXCHANGES_UNIT],";
            sql += "[TURNOVER],[BALANCE],[MAKE_ORDER_STAFF],[ORDERR_EVIEW],[IS_REVIEW],[NOTE] FROM [dbo].[BANK_CASHSUB_LEDGER] ORDER BY TRADING_DATE";

            m_bankCashsubLedgerList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BankCashsubLedgerTable record = new BankCashsubLedgerTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.bankID = DbDataConvert.ToInt32(row["BANK_ID"]);
                    record.bankName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_BANK_LIST", record.bankID);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.vouchersNumber = DbDataConvert.ToString(row["VOUCHERS_NUMBER"]);
                    record.billName = DbDataConvert.ToString(row["BILL_NAME"]);
                    record.billTypeID = DbDataConvert.ToInt32(row["BILL_TYPE_ID"]);
                    record.sourceBillNumber = DbDataConvert.ToString(row["SOURCE_BILL_NUMBER"]);
                    record.exchangesUnit = DbDataConvert.ToInt32(row["EXCHANGES_UNIT"]);

                    if (record.billName == "付款单")
                    {
                        record.billTypeName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_PAYMENT_TYPE_LIST", record.billTypeID);
                        record.exchangesUnitName = Supplier.getInctance().getSupplierNameFromPkey(record.exchangesUnit);
                    }
                    else 
                    {
                        record.billTypeName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_RECEIVABLE_TYPE_LIST", record.billTypeID);
                        record.exchangesUnitName = Customer.getInctance().getCustomerNameFromPkey(record.exchangesUnit);
                    }

                    record.turnover = DbDataConvert.ToDouble(row["TURNOVER"]);

                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);

                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);

                    if (record.isReview == "1")
                    {
                        record.balance = DbDataConvert.ToDouble(row["BALANCE"]);
                        record.orderReview = DbDataConvert.ToInt32(row["ORDERR_EVIEW"]);
                        record.orderReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderReview);
                    }

                    m_bankCashsubLedgerList.Add(m_bankCashsubLedgerList.Count, record);
                }
            }
        }

        public SortedDictionary<int, BankCashsubLedgerTable> getAllBankCashsubLedgerInfo()
        {
            if (m_bankCashsubLedgerList.Count == 0)
            {
                load();
            }

            return m_bankCashsubLedgerList;
        }

        public SortedDictionary<int, BankCashsubLedgerTable> getAllReviewBankCashsubLedgerInfo()
        {
            if (m_bankCashsubLedgerList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, BankCashsubLedgerTable> list = new SortedDictionary<int, BankCashsubLedgerTable>();

            foreach (KeyValuePair<int, BankCashsubLedgerTable> index in m_bankCashsubLedgerList)
            {
                if (index.Value.isReview.Length > 0 &&  index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public BankCashsubLedgerTable getBankCashsubLedgerInfoFromOrderNumber(string billNumber)
        {
            if (m_bankCashsubLedgerList.Count == 0)
            {
                load();
            }

            BankCashsubLedgerTable bill = new BankCashsubLedgerTable();

            foreach (KeyValuePair<int, BankCashsubLedgerTable> index in m_bankCashsubLedgerList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    bill = index.Value;
                }
            }

            return bill;
        }

        public SortedDictionary<int, BankCashsubLedgerTable> getCurrentMonthAllCashCashsubLedgerInfo(bool isReview)
        {
            if (m_bankCashsubLedgerList.Count == 0)
            {
                load();
            }
            string monthFirstDay = DateTime.Now.ToString("yyyy-MM") + "-01";
            string monthLastDay = DateTime.Now.ToString("yyyy-MM") + "-31";

            SortedDictionary<int, BankCashsubLedgerTable> list = new SortedDictionary<int, BankCashsubLedgerTable>();

            foreach (KeyValuePair<int, BankCashsubLedgerTable> index in m_bankCashsubLedgerList)
            {
                if (isReview)
                {
                    if (index.Value.isReview.Length > 0 &&
                        index.Value.isReview == "1" &&
                        index.Value.tradingDate.CompareTo(monthFirstDay) >= 0 &&
                        index.Value.tradingDate.CompareTo(monthLastDay) <= 0)
                    {
                        list.Add(list.Count, index.Value);
                    }
                }
                else
                {
                    if (index.Value.tradingDate.CompareTo(monthFirstDay) >= 0 &&
                        index.Value.tradingDate.CompareTo(monthLastDay) <= 0)
                    {
                        list.Add(list.Count, index.Value);
                    }
                }
            }

            return list;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, BankCashsubLedgerTable> index in m_bankCashsubLedgerList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class BankCashsubLedgerTable
    {
        public int pkey { get; set; }
        public string tradingDate { get; set; }
        public int bankID { get; set; }
        public string bankName { get; set; }
        public string billNumber { get; set; }
        public string vouchersNumber { get; set; }
        public string billName { get; set; }
        public int billTypeID { get; set; }
        public string billTypeName { get; set; }
        public string sourceBillNumber { get; set; }
        public int exchangesUnit { get; set; }
        public string exchangesUnitName { get; set; }
        public double turnover { get; set; }
        public double balance { get; set; }
        public int makeOrderStaff { get; set; }
        public string makeOrderStaffName { get; set; }
        public int orderReview { get; set; }
        public string orderReviewName { get; set; }
        public string isReview { get; set; }
        public string note { get; set; }
    }
}