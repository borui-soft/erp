using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class DbBackupRecord : ITableModel
    {
        private SortedDictionary<int, DbBackupRecordTable> m_dbBackupList = new SortedDictionary<int, DbBackupRecordTable>();
        static private DbBackupRecord m_instance = null;

        private DbBackupRecord()
        {
        }

        static public DbBackupRecord getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new DbBackupRecord();
            }

            return m_instance;
        }

        private void load()
        {
            string query = "SELECT [PKEY],[BACK_TYPE],[NAME],[HOST_NAME],[SAVE_PATH1],[SAVE_PATH2],[REASON],[NOTE]";
            query += ",[BACKUP_TIME],[STATE] FROM [dbo].[BASE_DB_BACKUP_RECORD] ORDER BY PKEY";

            m_dbBackupList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    DbBackupRecordTable record = new DbBackupRecordTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.backType = DbDataConvert.ToInt32(row["BACK_TYPE"]);
                    record.name = DbDataConvert.ToString(row["NAME"]);
                    record.hostName = DbDataConvert.ToString(row["HOST_NAME"]);
                    record.savePath1 = DbDataConvert.ToString(row["SAVE_PATH1"]);
                    record.savePath2 = DbDataConvert.ToString(row["SAVE_PATH2"]);
                    record.reason = DbDataConvert.ToString(row["REASON"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);
                    record.backupTime = DbDataConvert.ToString(row["BACKUP_TIME"]);
                    record.state = DbDataConvert.ToString(row["STATE"]);

                    m_dbBackupList.Add(m_dbBackupList.Count, record);
                }
            }
        }

        public SortedDictionary<int, DbBackupRecordTable> getAllRecord()
        {
            if (m_dbBackupList.Count == 0)
            {
                load();
            }

            return m_dbBackupList;
        }

        public bool databaseBack(string filePath, bool isDispaly = true)
        {
            // BACKUP DATABASE [ERP] TO  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\ERP.bak' WITH NOFORMAT, INIT,  NAME = N'ERP-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
            string sql = "BACKUP DATABASE [ERP] TO  DISK = N\'";
            sql += filePath;
            sql += getFileName(filePath);
            sql += "\' WITH NOFORMAT, INIT,  NAME = N'ERP-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, sql);

                if (isDispaly)
                {
                    MessageBoxExtend.messageOK("数据备份成功");
                }

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return false;
            }

            return true;
        }

        private string getFileName(string filePath)
        {
            int index = 1;

            string[] fileNames = Directory.GetFiles(filePath);

            foreach (var fileName in fileNames)
            {
                string name = fileName.ToString();
                string serach = "ERP.bck." + DateTime.Now.ToString("yyyyMMdd");
                if (name.IndexOf(serach) > 0)
                {
                    int pos1 = name.LastIndexOf(".");

                    if (pos1 > 0)
                    {
                        string num = name.Substring(pos1 + 1);
                        try
                        {
                            index = Convert.ToInt32(num) + 1;
                        }
                        catch (Exception)
                        {
                            index = 1;
                        }
                    }
                }
            }

            string fullName = "ERP.bck." + DateTime.Now.ToString("yyyyMMdd") + "." + Convert.ToString(index);

            return fullName;
        }

        public void insert(DbBackupRecordTable record)
        {
            string sql = "INSERT INTO [dbo].[BASE_DB_BACKUP_RECORD]([BACK_TYPE],[NAME],[HOST_NAME],";
            sql += "[SAVE_PATH1],[SAVE_PATH2],[REASON],[NOTE],[STATE]) VALUES(";

            sql += "0,"; // 代表手动备份
            sql += "'" + record.name + "',";
            sql += "'" + record.hostName + "',";
            sql += "'" + record.savePath1 + "',";
            sql += "'" + record.savePath2 + "',";
            sql += "'" + record.reason + "',";
            sql += "'" + record.note + "','备份成功')";

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

        public bool isBackTheDay()
        {
            bool isRet = false;

            string sql = "SELECT COUNT(*) FROM BASE_DB_BACKUP_RECORD where backup_time >= '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            sql += " AND NAME = '" + DbPublic.getInctance().getCurrentLoginUserName() + "'";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                int recordCount = DbDataConvert.ToInt32(dataTable.Rows[0][0]);
                
                if (recordCount == 0)
                {
                    isRet = true;
                }
            }

            return isRet;
        }
    }

    public class DbBackupRecordTable
    {
        public int pkey { get; set; }
        public int backType { get; set; }
        public string name { get; set; }
        public string hostName { get; set; }
        public string savePath1 { get; set; }
        public string savePath2 { get; set; }
        public string reason { get; set; }
        public string note { get; set; }
        public string backupTime { get; set; }
        public string state { get; set; }
    }
}