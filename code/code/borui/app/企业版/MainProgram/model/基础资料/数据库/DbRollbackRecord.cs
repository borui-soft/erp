using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class DbRollbackRecord : ITableModel
    {
        private SortedDictionary<int, DbRollbackRecordTable> m_dbRollbackList = new SortedDictionary<int, DbRollbackRecordTable>();
        static private DbRollbackRecord m_instance = null;

        private DbRollbackRecord()
        {
        }

        static public DbRollbackRecord getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new DbRollbackRecord();
            }

            return m_instance;
        }
        public bool databaseRollback(string dbFile)
        {
            // RESTORE DATABASE [ERP] FROM  DISK = N'xxxxxxxxxxx' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10
            string sql = "RESTORE DATABASE [ERP] FROM  DISK = N\'";
            sql += dbFile;
            sql += "\' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME_MASTER, "ALTER DATABASE [ERP] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME_MASTER, sql);

                MessageBoxExtend.messageOK("数据库恢复成功! 需要重启ERP系统");

                CurrentLoginUser.getInctance().delete();
                Process.Start(@"MainProgram.exe"); 
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return false;
            }

            return true;
        }

        public void insert(DbRollbackRecordTable record)
        {
            string sql = "INSERT INTO [dbo].[BASE_DB_ROLLBACK_RECORD]([NAME],[HOST_NAME]";
            sql += ",[FILE_NAME],[REASON],[STATE]) VALUES(";

            sql += "'" + record.name + "',";
            sql += "'" + record.hostName + "',";
            sql += "'" + record.fileName + "',";
            sql += "'" + record.reason + "',";
            sql += "'恢复成功')";

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

        private void load()
        {
            string query = "SELECT [PKEY],[NAME],[HOST_NAME],[FILE_NAME],[REASON],[ROLLBACK_TIME]";
            query += ",[STATE] FROM [dbo].[BASE_DB_ROLLBACK_RECORD]";

            m_dbRollbackList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    DbRollbackRecordTable record = new DbRollbackRecordTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.name = DbDataConvert.ToString(row["NAME"]);
                    record.hostName = DbDataConvert.ToString(row["HOST_NAME"]);
                    record.fileName = DbDataConvert.ToString(row["FILE_NAME"]);
                    record.reason = DbDataConvert.ToString(row["REASON"]);
                    record.dateTime = DbDataConvert.ToString(row["ROLLBACK_TIME"]);
                    record.state = DbDataConvert.ToString(row["STATE"]);

                    m_dbRollbackList.Add(m_dbRollbackList.Count, record);
                }
            }
        }

        public SortedDictionary<int, DbRollbackRecordTable> getAllRecord()
        {
            if (m_dbRollbackList.Count == 0)
            {
                load();
            }

            return m_dbRollbackList;
        }
    }

    public class DbRollbackRecordTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string hostName { get; set; }
        public string fileName { get; set; }
        public string reason { get; set; }
        public string note { get; set; }
        public string dateTime { get; set; }
        public string state { get; set; }
    }
}