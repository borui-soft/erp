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
            if (checkIsQuit())
            {
                MessageBoxExtend.messageWarning("系统目前为非注册(正版)软件，单据现已超过200张，无法再继续使用。\n请联系软件供应商，索取正版序列号。");
                CurrentLoginUser.getInctance().delete();

                Process.GetCurrentProcess().Kill();
            }

            string newBillNumber = "";

            //newBillNumber = getNewBillNumberFunction1(billTypeNumber);
            //newBillNumber = getNewBillNumberFunction2(billTypeNumber);
            newBillNumber = getNewBillNumberFunction5(billTypeNumber);
            
            return newBillNumber;
        }

        // 序列号 第一种计算法方法：根据用户配置的单据号的格式
        private string getNewBillNumberFunction1(int billTypeNumber)
        {
            string newBillNumber = "";

            try
            {
                // 查询BASE_BILL_CONFIG表，根据单据类型得到单据的前缀

                string front = "";
                int isUsrRules = 0, isUseSysdate = 0, num = 0;

                string query = "SELECT FRONT, IS_USE_RULES, IS_USE_SYSDATE, NUM FROM BASE_BILL_CONFIG WHERE BILL_TYPE = " + billTypeNumber;

                using (DataTable dataTable1 = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
                {
                    if (dataTable1.Rows.Count > 0)
                    {
                        front = DbDataConvert.ToString(dataTable1.Rows[0][0]);
                        isUsrRules = DbDataConvert.ToInt32(dataTable1.Rows[0][1]);
                        isUseSysdate = DbDataConvert.ToInt32(dataTable1.Rows[0][2]);
                        num = DbDataConvert.ToInt32(dataTable1.Rows[0][3]);

                        if (isUsrRules == 1)
                        {
                            newBillNumber = front;

                            string sNewBillNumber = Convert.ToString(getMaxBillNumber(billTypeNumber, DateTime.Now.ToString("yyyyMMdd")));

                            if (isUseSysdate == 1)
                            {
                                newBillNumber += "-" + DateTime.Now.ToString("yyyyMMdd");
                            }
                            else
                            {
                                newBillNumber += "-" + DateTime.Now.ToString("ddssfff");
                            }

                            newBillNumber += "-";

                            for (int i = 0; i < num - sNewBillNumber.Length; i++)
                            {
                                newBillNumber += "0";
                            }

                            newBillNumber += sNewBillNumber;
                        }
                    }
                }

            }
            catch (Exception)
            {

            }

            return newBillNumber;
        }

        // 序列号 第二种计算法方法：单据编号单纯的单纯的又单据类型和yyyyMMddHHmmssfff这样格式的时间组成
        private string getNewBillNumberFunction2(int billTypeNumber)
        {
            string newBillNumber = "";

            if (billTypeNumber < 10)
            {
                newBillNumber += "0";
            }

            newBillNumber += Convert.ToString(billTypeNumber) + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            return newBillNumber;
        }

        private string getNewBillNumberFunction3(int billTypeNumber)
        {
            string newBillNumber = "";

            newBillNumber = DateTime.Now.ToString("ssfff");                                                 // 长度5位
            newBillNumber += "_";                                                                           // 长度1位
            newBillNumber += DateTime.Now.ToString("yyyyMMdd");                                             // 长度8位
            newBillNumber += "_";                                                                           // 长度1位

            // 计算下本年度累计单据张数
            DateTime dt = DateTime.Now;  //当前时间
            DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初

            string quety = "SELECT COUNT(*) FROM BILL_NUMBER WHERE TRADING_DATE >= '" + startYear.ToString("yyyy-MM-dd") + "'";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, quety))
            {
                int recordCount = DbDataConvert.ToInt32(dataTable.Rows[0][0]);
                recordCount += 1;

                if (recordCount > 99999)
                {
                    recordCount = recordCount % 99999;
                }

                string tmpRecordCount = Convert.ToString(recordCount);

                for (int i = 0; i < 5 - tmpRecordCount.Length; i++)
                {
                    newBillNumber += "0";
                }

                newBillNumber += Convert.ToString(recordCount);
            }

            return newBillNumber;
        }

        private string getNewBillNumberFunction4(int billTypeNumber)
        {
            string newBillNumber = "";

            if (billTypeNumber < 10)
            {
                newBillNumber += "0";
            }

            newBillNumber += Convert.ToString(billTypeNumber) + DateTime.Now.ToString("fff");               // 长度5位
            newBillNumber += "_";                                                                           // 长度1位
            newBillNumber += DateTime.Now.ToString("yyyyMMdd");                                             // 长度8位
            newBillNumber += "_";                                                                           // 长度1位

            // 计算下本年度累计单据张数
            DateTime dt = DateTime.Now;  //当前时间
            DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初

            string quety = "SELECT COUNT(*) FROM BILL_NUMBER WHERE TRADING_DATE >= '" + startYear.ToString("yyyy-MM-dd") + "'";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, quety))
            {
                int recordCount = DbDataConvert.ToInt32(dataTable.Rows[0][0]);
                recordCount += 1;

                if (recordCount > 99999)
                {
                    recordCount = recordCount % 99999;
                }

                string tmpRecordCount = Convert.ToString(recordCount);

                for (int i = 0; i < 5 - tmpRecordCount.Length; i++)
                {
                    newBillNumber += "0";
                }

                newBillNumber += Convert.ToString(recordCount);
            }

            return newBillNumber;
        }

        private string getNewBillNumberFunction5(int billTypeNumber)
        {
            /*
             *  目前系统中，单据号规则分别如下 2017/05/07
             *  采购申请单：0
             *  采购订单：1
             *  采购入库单：2

             *  销售询价：4
             *  销售订单：5
             *  销售出库：6

             *  产品入库：8
             *  盘盈入库：9
             *  其他入库：10

             *  领料单：14
             *  盘亏亏损：15
             *  其他出库：16

             *  库存预占：17

             *  期初成本调整单据：20

             *  xx材料表：51、52、53
             *  xx材料变更表：54

             *  付款单：61
             *  收款单：62
            */


            string newBillNumber = "";

            newBillNumber = DateTime.Now.ToString("ssfff");                                                 // 长度5位
            newBillNumber += "_";                                                                           // 长度1位
            newBillNumber += DateTime.Now.ToString("yyyyMMdd");                                             // 长度8位
            newBillNumber += "_";                                                                           // 长度1位

            // 计算下本年度累计单据张数
            DateTime dt = DateTime.Now;  //当前时间
            DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初

            string quety = "SELECT COUNT(*) FROM BILL_NUMBER WHERE TRADING_DATE >= '" + startYear.ToString("yyyy-MM-dd") + "' AND BILL_TYPE_NUMBER IN(";

            
            /*
             *  单据号按如下规则进行分类汇总
             *  采购入库、其他入库
             *  生产领料和其他出库
             *  盘盈和盘亏
             *  采购申请单和采购订单
             *  其他的各自分为依赖
            */
            string orderType = "";
            if(billTypeNumber == 2 || billTypeNumber == 10)
            {
                orderType = "2, 10";
            }
            else if(billTypeNumber == 14 || billTypeNumber == 16)
            {
                orderType = "14, 16";
            }
            else if(billTypeNumber == 9 || billTypeNumber == 15)
            {
                orderType = "9, 15";
            }
            else if(billTypeNumber == 0 || billTypeNumber == 1)
            {
                orderType = "0, 1";
            }
            else
            {
                orderType = Convert.ToString(billTypeNumber);
            }

            quety += orderType + ")";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, quety))
            {
                int recordCount = DbDataConvert.ToInt32(dataTable.Rows[0][0]);
                recordCount += 1;

                if (recordCount > 99999)
                {
                    recordCount = recordCount % 99999;
                }

                string tmpRecordCount = Convert.ToString(recordCount);

                for (int i = 0; i < 5 - tmpRecordCount.Length; i++)
                {
                    newBillNumber += "0";
                }

                newBillNumber += Convert.ToString(recordCount);
            }

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