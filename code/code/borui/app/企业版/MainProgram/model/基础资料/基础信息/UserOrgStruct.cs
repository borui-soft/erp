using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class UserOrgStruct : ITableModel
    {
        static private UserOrgStruct m_instance = null;
        private SortedDictionary<int, int> m_childNodes = new SortedDictionary<int, int>();
        private SortedDictionary<int, UserOrgStructTable> m_UserOrgList = new SortedDictionary<int, UserOrgStructTable>();

        private UserOrgStruct()
        {
            load();
        }

        static public UserOrgStruct getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new UserOrgStruct();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, UserOrgStructTable> userGroupList = new SortedDictionary<int, UserOrgStructTable>();
            string userQuery = "SELECT [PKEY],[DEPARTMENT_OR_STAFF],[VALUE],[PARENT_PKEY] FROM ";
            userQuery += "[dbo].[BASE_USER_ORG_STRUCT] ORDER BY PKEY";

            if (m_UserOrgList.Count > 0)
            {
                m_UserOrgList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, userQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    UserOrgStructTable record = new UserOrgStructTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.departmentOrStaff = DbDataConvert.ToInt32(row[1]);
                    record.value = DbDataConvert.ToInt32(row[2]);
                    record.parentPkey = DbDataConvert.ToInt32(row[3]);

                    userGroupList.Add(record.pkey, record);
                }
            }

            m_UserOrgList = userGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_USER_ORG_STRUCT", pkey))
            {
                load();
            }
        }

        public void insert(UserOrgStructTable userOrgStruct)
        {
            string insert = "INSERT INTO [dbo].[BASE_USER_ORG_STRUCT] ([DEPARTMENT_OR_STAFF],[VALUE],[PARENT_PKEY]) VALUES (";

            insert += Convert.ToString(userOrgStruct.departmentOrStaff) + ",";
            insert += Convert.ToString(userOrgStruct.value) + ",";
            insert += Convert.ToString(userOrgStruct.parentPkey);
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);
                MessageBoxExtend.messageOK("数据保存成功");
                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public int getChildNodesCount(int pkey)
        {
            int childNodesCount = 0;

            UserOrgStructTable record = new UserOrgStructTable();
            foreach (KeyValuePair<int, UserOrgStructTable> index in m_UserOrgList)
            {
                record = index.Value;
                if (record.parentPkey == pkey)
                {
                    childNodesCount++;
                }
            }

            return childNodesCount;
        }

        public ArrayList getNodesFormParentID(int parentID)
        {
            ArrayList nodes = new ArrayList();

            UserOrgStructTable record = new UserOrgStructTable();

            foreach (KeyValuePair<int, UserOrgStructTable> index in m_UserOrgList)
            {
                record = index.Value;
                if (record.parentPkey == parentID)
                {
                    nodes.Add(record);
                }
            }

            return nodes;
        }

        public int getPkeyFromValue(int value)
        {
            int pkey = -1;

            UserOrgStructTable record = new UserOrgStructTable();

            foreach (KeyValuePair<int, UserOrgStructTable> index in m_UserOrgList)
            {
                record = index.Value;

                if (record.value == value)
                {
                    pkey = record.pkey;
                    break;
                }
            }

            return pkey;
        }
    }

    public class UserOrgStructTable
    {
        public int pkey { get; set; }
        public int departmentOrStaff { get; set; }
        public int value { get; set; }
        public int parentPkey { get; set; }
    }
}