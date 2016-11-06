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
    public class ActionAndStaff : ITableModel
    {
        private SortedDictionary<int, AtionAndStaffTable> m_actionAndStaffList = new SortedDictionary<int, AtionAndStaffTable>();

        static private ActionAndStaff m_instance = null;

        private ActionAndStaff()
        {
            load();
        }

        static public ActionAndStaff getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new ActionAndStaff();
            }

            return m_instance;
        }

        public void insert(AtionAndStaffTable record)
        {
            if (record.actionID == 0)
            {
                return;
            }

            string insert = "INSERT INTO [dbo].[BASE_ACTION_STAFF]([ACTION_ID],[STAFF_ID]) VALUES(";

            insert += Convert.ToString(record.actionID) + ",";
            insert += Convert.ToString(record.staffID) + "";
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

        public void delete(int sataffID, int actionID)
        {
            string delete = "DELETE FROM [dbo].[BASE_ACTION_STAFF] WHERE [STAFF_ID] = " + Convert.ToString(sataffID);
            delete += " AND ACTION_ID = " + Convert.ToString(actionID);

            DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, delete);
        }

        private void load()
        {
            string sql = "SELECT [ACTION_ID],[STAFF_ID] FROM [dbo].[BASE_ACTION_STAFF]";

            m_actionAndStaffList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    AtionAndStaffTable record = new AtionAndStaffTable();

                    record.actionID = DbDataConvert.ToInt32(row["ACTION_ID"]);
                    record.staffID = DbDataConvert.ToInt32(row["STAFF_ID"]);

                    m_actionAndStaffList.Add(m_actionAndStaffList.Count, record);
                }
            }
        }

        // 判断某个用户是否拥有某个操作权限
        public bool isAccess(int staffID, int actionID)
        {
            bool isRet = false;

            if (m_actionAndStaffList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, AtionAndStaffTable> index in m_actionAndStaffList)
            {
                AtionAndStaffTable record = new AtionAndStaffTable();
                record = index.Value;

                if (record.staffID == staffID && record.actionID == actionID)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class AtionAndStaffTable
    {
        public int staffID { get; set; }
        public int actionID { get; set; }
    }
}