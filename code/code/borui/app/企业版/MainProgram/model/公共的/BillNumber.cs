using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Diagnostics;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public class BillNumber : ITableModel
    {
        private SortedDictionary<int, BankCashsubLedgerTable> m_bankCashsubLedgerList = new SortedDictionary<int, BankCashsubLedgerTable>();

        static private BillNumber m_instance = null;

        private BillNumber()
        {
        }

        static public BillNumber getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new BillNumber();
            }

            return m_instance;
        }
        
        public string getNewBillNumber(int billTypeNumber, string date)
        {
            string newBillNumber = "";

            if (billTypeNumber < 10)
            {
                newBillNumber += "0";
            }

            newBillNumber += Convert.ToString(billTypeNumber) + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            if (checkIsQuit())
            {
                MessageBoxExtend.messageWarning("系统目前为非注册(正版)软件，单据现已超过200张，无法再继续使用。\n请联系软件供应商，索取正版序列号。");
                CurrentLoginUser.getInctance().delete();

                Process.GetCurrentProcess().Kill();
            }
            //else
            //{
            //    try
            //    {
            //        // 查询BASE_BILL_CONFIG表，根据单据类型得到单据的前缀

            //        string front = "";
            //        int isUsrRules = 0, isUseSysdate = 0, num = 0;

            //        string query = "SELECT FRONT, IS_USE_RULES, IS_USE_SYSDATE, NUM FROM BASE_BILL_CONFIG WHERE BILL_TYPE = " + billTypeNumber;

            //        using (DataTable dataTable1 = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            //        {
            //            if (dataTable1.Rows.Count > 0)
            //            {
            //                front = DbDataConvert.ToString(dataTable1.Rows[0][0]);
            //                isUsrRules = DbDataConvert.ToInt32(dataTable1.Rows[0][1]);
            //                isUseSysdate = DbDataConvert.ToInt32(dataTable1.Rows[0][2]);
            //                num = DbDataConvert.ToInt32(dataTable1.Rows[0][3]);

            //                if (isUsrRules == 1)
            //                {
            //                    newBillNumber = front;

            //                    string sNewBillNumber = Convert.ToString(getMaxBillNumber(billTypeNumber, date));

            //                    if (isUseSysdate == 1)
            //                    {
            //                        newBillNumber += "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //                    }
            //                    else
            //                    {
            //                        newBillNumber += "-" + DateTime.Now.ToString("ddssfff") + DateTime.Now.ToString("ddssfff");
            //                    }

            //                    //newBillNumber += "-";

            //                    //for (int i = 0; i < num - sNewBillNumber.Length; i++)
            //                    //{
            //                    //    newBillNumber += "0";
            //                    //}

            //                    //newBillNumber += sNewBillNumber;
            //                }
            //            }
            //        }

            //    }
            //    catch (Exception)
            //    {

            //    }
            //}

            return newBillNumber;
        }

        public void inserBillNumber(int billTypeNumber, string date, string billNumber)
        {
            string insert = "INSERT INTO BILL_NUMBER (BILL_TYPE_NUMBER, BILL_MAX_NUMBER, DATE) VALUES (";
            insert += billTypeNumber + ",";
            insert += getCurrentOrderNumber(billNumber) + ",";
            insert += "'" + date  + "')";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);
            }
            catch (Exception)
            {
 
            }
        }

        private int getMaxBillNumber(int billTypeNumber, string date)
        {
            int iNewBillNumber = 1;

            string sql = "SELECT [BILL_MAX_NUMBER] FROM [dbo].[BILL_NUMBER]";
            sql += " WHERE BILL_TYPE_NUMBER = " + billTypeNumber;
            sql += " AND DATE = '" + date + "'";
            sql += " ORDER BY TRADING_DATE DESC, BILL_MAX_NUMBER DESC";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                if (dataTable.Rows.Count > 0)
                {
                    iNewBillNumber = DbDataConvert.ToInt32(dataTable.Rows[0][0]);
                    iNewBillNumber = iNewBillNumber + 1;
                }
            }

            return iNewBillNumber;
        }

        private int getCurrentOrderNumber(string billNumber)
        {
            int orderNumber = 0;
            string str = billNumber;

            int pos = str.LastIndexOf("-");

            if (pos != -1)
            {
                str = str.Substring(pos + 1);

                orderNumber = Convert.ToInt32(str.ToString());
            }


            return orderNumber;
        }

        private bool checkIsQuit()
        {
            // 对于未经注册的非正版软件，各种单据累计不得超过100张，当超过100张时，提示用户需要注册为正版才能继续使用，并推出程序
            bool isRet = true;
            if (DbPublic.getInctance().isGenuineSoftware())
            {
                isRet = false;
            }
            else
            {
                string quety = "SELECT COUNT(*) FROM BILL_NUMBER";

                using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, quety))
                {
                    int recordCount = DbDataConvert.ToInt32(dataTable.Rows[0][0]);

                    if (recordCount <= 200)
                    {
                        isRet = false;
                    }
                }
            }

            return isRet;
        }
    }
}