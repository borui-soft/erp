using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;
using System.Net;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class CurrentLoginUser : ITableModel
    {
        static private CurrentLoginUser m_instance = null;
        private SortedDictionary<int, CurrentLoginUserTable> m_currentLoginUserList = new SortedDictionary<int, CurrentLoginUserTable>();

        private CurrentLoginUser()
        {
            load();
        }

        static public CurrentLoginUser getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new CurrentLoginUser();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, CurrentLoginUserTable> userGroupList = new SortedDictionary<int, CurrentLoginUserTable>();
            string query = "SELECT [PKEY],[HOST_NAME],[USER_ID] ,[START_TIME] FROM [dbo].[BASE_SYSTEM_CURRENT_LOGIN_USER]";

            if (m_currentLoginUserList.Count > 0)
            {
                m_currentLoginUserList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CurrentLoginUserTable record = new CurrentLoginUserTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.hostName = DbDataConvert.ToString(row[1]);
                    record.userID = DbDataConvert.ToInt32(row[2]);
                    record.startTime = DbDataConvert.ToString(row[3]);

                    userGroupList.Add(record.userID, record);
                }
            }

            m_currentLoginUserList = userGroupList;
        }

        public void delete()
        {
            string deleteSql = "DELETE FROM BASE_SYSTEM_CURRENT_LOGIN_USER WHERE USER_ID = ";
            deleteSql += DbPublic.getInctance().getCurrentLoginUserID();
            
            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, deleteSql);
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void insert()
        {
            // 插入之前先执行删除操作
            if (m_currentLoginUserList.ContainsKey(DbPublic.getInctance().getCurrentLoginUserID()))
            {
                delete();
            }

            string insert = "INSERT INTO [dbo].[BASE_SYSTEM_CURRENT_LOGIN_USER]([HOST_NAME],[USER_ID]) VALUES(";

            insert += "'" + Dns.GetHostName() + "',";
            insert += Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID());
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);
                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public bool userIsOnline(int pkey)
        {
            if (m_currentLoginUserList.Count == 0)
            {
                load();
            }

            return m_currentLoginUserList.ContainsKey(pkey);
        }
    }

    public class CurrentLoginUserTable
    {
        public int pkey { get; set; }
        public string hostName { get; set; }
        public int userID { get; set; }
        public string startTime { get; set; }
    }
}
