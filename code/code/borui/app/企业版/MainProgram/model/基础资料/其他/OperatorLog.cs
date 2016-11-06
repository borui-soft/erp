using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using System.Net;
using System.Collections;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class OperatorLog : ITableModel
    {
        private SortedDictionary<int, OperatorLogTable> m_dbBackupList = new SortedDictionary<int, OperatorLogTable>();
        static private OperatorLog m_instance = null;

        private OperatorLog()
        {
        }

        static public OperatorLog getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new OperatorLog();
            }

            return m_instance;
        }

        private void load()
        {
            string query = "SELECT [PKEY],[USER_NAME],[OPER_TIME],[MODULE_NAME],[OPER_DESC],";
            query += "[HOST_NAME], [SYSTEM_NAME] FROM [dbo].[BASE_OPERATOR_LOG] ORDER BY PKEY";

            m_dbBackupList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    OperatorLogTable record = new OperatorLogTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.userName = DbDataConvert.ToString(row["USER_NAME"]);
                    record.operTime = DbDataConvert.ToString(row["OPER_TIME"]);
                    record.moduleName = DbDataConvert.ToString(row["SYSTEM_NAME"]) + "-" + DbDataConvert.ToString(row["MODULE_NAME"]);
                    record.operDesc = DbDataConvert.ToString(row["OPER_DESC"]);
                    record.hostName = DbDataConvert.ToString(row["HOST_NAME"]);
                    record.systemName = DbDataConvert.ToString(row["SYSTEM_NAME"]);

                    m_dbBackupList.Add(m_dbBackupList.Count, record);
                }
            }
        }

        public SortedDictionary<int, OperatorLogTable> getAllRecord()
        {
            load();
            
            return m_dbBackupList;
        }

        public SortedDictionary<int, OperatorLogTable> getRecordFromQuerySql(string querySql)
        {
            string query = "SELECT [PKEY],[USER_NAME],[OPER_TIME],[MODULE_NAME],[OPER_DESC],";
            query += "[HOST_NAME], [SYSTEM_NAME] FROM [dbo].[BASE_OPERATOR_LOG] ";
            query += querySql;
            query += "ORDER BY PKEY";

            SortedDictionary<int, OperatorLogTable> dbBackupList = new SortedDictionary<int, OperatorLogTable>();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    OperatorLogTable record = new OperatorLogTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.userName = DbDataConvert.ToString(row["USER_NAME"]);
                    record.operTime = DbDataConvert.ToString(row["OPER_TIME"]);
                    record.moduleName = DbDataConvert.ToString(row["MODULE_NAME"]);
                    record.operDesc = DbDataConvert.ToString(row["OPER_DESC"]);
                    record.hostName = DbDataConvert.ToString(row["HOST_NAME"]);

                    dbBackupList.Add(dbBackupList.Count, record);
                }
            }

            return dbBackupList;
        }

        public ArrayList getDistinecRecord(string columnName)
        {
            ArrayList values = new ArrayList();

            string query = "SELECT DISTINCT([" + columnName + "]) FROM [BASE_OPERATOR_LOG]";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    values.Add(DbDataConvert.ToString(row[columnName]));
                }
            }

            return values;
        }

        public void insert(string systemName, string moduleName, string operDesc)
        {
            string sql = "INSERT INTO [dbo].[BASE_OPERATOR_LOG] ([USER_NAME],";
            sql += "[MODULE_NAME],[OPER_DESC],[HOST_NAME],[SYSTEM_NAME])VALUES(";

            sql += "'" + DbPublic.getInctance().getCurrentLoginUserName() + "',";
            sql += "'" + moduleName + "',";
            sql += "'" + operDesc + "',";
            sql += "'" + Dns.GetHostName() + "',";
            sql += "'" + systemName + "')";

            try
            {
                // 单据审核时，会重新保存一遍单据，此处为了防止重复的记录插入到数据库
                if (!isExistRecord(systemName, moduleName, operDesc))
                {
                    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, sql);
                }
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void insert(int billType, string operDesc)
        {
            int subSystemID = SystemModule.getInctance().getsystemIDFromModuleID(billType);
            string systemName = SubSystem.getInctance().getsubSystemNameFromSystemID(subSystemID);
            string moduleName = SystemModule.getInctance().getModuleNameFromModuleID(billType);

            insert(systemName, moduleName, operDesc);
        }

        private bool isExistRecord(string systemName, string moduleName, string operDesc)
        {
            bool isRet = false;

            string query = "SELECT COUNT(*) FROM BASE_OPERATOR_LOG WHERE ";
            query += "SYSTEM_NAME = '" + systemName + "'";
            query += " AND MODULE_NAME = '" + moduleName + "'";
            query += " AND OPER_DESC = '" + operDesc + "'";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                int count = DbDataConvert.ToInt32(dataTable.Rows[0][0]);

                if (count > 0)
                {
                    isRet = true;
                }
            }

            return isRet;
        }
    }

    public class OperatorLogTable
    {
        public int pkey { get; set; }
        public string userName { get; set; }
        public string operTime { get; set; }
        public string moduleName { get; set; }
        public string operDesc { get; set; }
        public string hostName { get; set; }
        public string systemName { get; set; }
    }
}