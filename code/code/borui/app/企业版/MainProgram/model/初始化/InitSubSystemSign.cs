using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Management;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public class InitSubSystemSign : ITableModel
    {
        static private InitSubSystemSign m_instance = null;
        private bool m_isExistRecord = false;
        private InitSubSystemSignTable m_initSubSystemSignTable = new InitSubSystemSignTable();

        private InitSubSystemSign()
        {
            load();
        }

        static public InitSubSystemSign getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new InitSubSystemSign();
            }

            return m_instance;
        }

        private void load()
        {
            string sql = "SELECT [BUSINESS_SYSTEM],[FINANCIAL_SYSTEM],[SYSTEM_KEY],[DISK_KEY] FROM [dbo].[INIT_SUB_SYSTEM_SIGN]";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                if (dataTable.Rows.Count > 0)
                {
                    m_isExistRecord = true;
                    string businessInitSign = DbDataConvert.ToString(dataTable.Rows[0][0]);
                    string financialInitSign = DbDataConvert.ToString(dataTable.Rows[0][1]);
                    m_initSubSystemSignTable.systemKey = DbDataConvert.ToString(dataTable.Rows[0][2]);
                    m_initSubSystemSignTable.diskKey = DbDataConvert.ToString(dataTable.Rows[0][3]);

                    if (businessInitSign.Length > 0)
                    {
                        m_initSubSystemSignTable.businessInitSign = DbDataConvert.ToInt32(businessInitSign.ToString());
                    }
                    else
                    {
                        m_initSubSystemSignTable.businessInitSign = 0;
                    }

                    if (financialInitSign.Length > 0)
                    {
                        m_initSubSystemSignTable.financialInitSign = DbDataConvert.ToInt32(financialInitSign.ToString());
                    }
                    else
                    {
                        m_initSubSystemSignTable.financialInitSign = 0;
                    }
                }
                else
                {
                    m_initSubSystemSignTable.systemKey = "";
                    m_initSubSystemSignTable.businessInitSign = 0;
                    m_initSubSystemSignTable.financialInitSign = 0;
                }
            }
        }

        public void initBusinessSystem()
        {
            string sql = "";
            if (m_isExistRecord)
            {
                sql = "UPDATE [dbo].[INIT_SUB_SYSTEM_SIGN] SET BUSINESS_SYSTEM = 1, ";
                sql += "BUSINESS_INIT_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            }
            else 
            {
                sql = "INSERT INTO [dbo].[INIT_SUB_SYSTEM_SIGN] (BUSINESS_SYSTEM, BUSINESS_INIT_DATE) VALUES (1, '";
                sql += DateTime.Now.ToString("yyyyMMdd") + "')";
            }

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

        public void initFinancialSystem()
        {
            string sql = "UPDATE [dbo].[INIT_SUB_SYSTEM_SIGN] SET FINANCIAL_SYSTEM = 1, ";
            sql += "FINANCIAL_INIT_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "'";

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

        public void register(string key)
        {
            string sql = "UPDATE [dbo].[INIT_SUB_SYSTEM_SIGN] SET SYSTEM_KEY = '" + key + "', ";
            sql += "DISK_KEY = '" + GetDiskID() + "'";

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

        public string GetDiskID()
        {
            string value = "unknow";

            # region 得到本机硬盘序列号
            //try
            //{
            //    //获取硬盘ID 
            //    String HDid = "";
            //    ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            //    ManagementObjectCollection moc = mc.GetInstances();
            //    foreach (ManagementObject mo in moc)
            //    {
            //        HDid = (string)mo.Properties["Model"].Value;
            //    }
            //    moc = null;
            //    mc = null;
            //    return HDid;
            //}
            //catch
            //{
            //    return "unknow";
            //}
            //finally
            //{
            //}
            #endregion

            #region 得到访问数据库的机器名
            try
            {
                string query = "SELECT   @@SERVERNAME";
                using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
                {
                    value = DbDataConvert.ToString(dataTable.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                return value;
            }
            #endregion

            #region 得到访问数据库的磁盘卷标(此处如果能得到硬盘序列号最好)
            //string query1 = "create table temp (id int identity(1,1),info nvarchar(2000))";
            //string query2 = "insert into temp Exec xp_cmdshell 'vol'";
            //string query3 = "select info from temp where INFO LIKE '%序列号%'";
            //string query4 = "drop table temp";

            //try
            //{
            //    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, query1);
            //    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, query2);
            //    DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query3);

            //    if (dataTable.Rows.Count > 0)
            //    {
            //        value = DbDataConvert.ToString(dataTable.Rows[0][0]);
            //    }

            //    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, query4);
            //}
            //catch (Exception)
            //{
            //    return value;
            //}
            #endregion

            return value;
        }

        public bool isBusinessSystemInit()
        {
            return m_initSubSystemSignTable.businessInitSign == 0 ? false:true;
        }

        public bool isFinancialSystemInit()
        {
            return m_initSubSystemSignTable.financialInitSign == 0 ? false : true;
        }

        public string getRegisterDiskID()
        {
            return m_initSubSystemSignTable.diskKey;
        }

        public string getRegistersSoftwareKey()
        {
            return m_initSubSystemSignTable.systemKey;
        }
    }

    public class InitSubSystemSignTable
    {
        public int businessInitSign { get; set; }
        public int financialInitSign { get; set; }
        public string systemKey { get; set; }
        public string diskKey { get; set; }
        
    }
}