using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using System.Collections;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class Action : ITableModel
    {
        private SortedDictionary<int, ActionTable> m_actionList = new SortedDictionary<int, ActionTable>();

        static private Action m_instance = null;

        private Action()
        {
            load();
        }

        static public Action getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new Action();
            }

            return m_instance;
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[ACTION_NAME],[MODULE_ID],[UI_ACTION_NAME] FROM [dbo].[BASE_ACTION_LIST] ORDER BY PKEY";

            m_actionList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ActionTable record = new ActionTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.actionName = DbDataConvert.ToString(row["ACTION_NAME"]);
                    record.moduleID = DbDataConvert.ToInt32(row["MODULE_ID"]);
                    record.uiActionName = DbDataConvert.ToString(row["UI_ACTION_NAME"]);

                    m_actionList.Add(m_actionList.Count, record);
                }
            }
        }

        public SortedDictionary<int, ActionTable> getActionInfoFromModuleID(int moduleID)
        {
            if (m_actionList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, ActionTable> moduleList = new SortedDictionary<int, ActionTable>();

            foreach (KeyValuePair<int, ActionTable> index in m_actionList)
            {
                ActionTable record = new ActionTable();
                record = index.Value;

                if (record.moduleID == moduleID)
                {
                    moduleList.Add(moduleList.Count, record);
                }
            }

            return moduleList;
        }

        public int getActionPkey(string moduleName, string actionName)
        {
            int pkey = 0;

            int moduleID = SystemModule.getInctance().getModuleIDFromModuleName(moduleName);

            foreach (KeyValuePair<int, ActionTable> index in m_actionList)
            {
                ActionTable record = new ActionTable();
                record = index.Value;

                if (record.moduleID == moduleID && record.actionName == actionName)
                {
                    pkey = record.pkey;
                }
            }

            return pkey;
        }

        public ArrayList getAllActionPkeyFromModuleID(int moduleID)
        {
            ArrayList actions = new ArrayList();

            foreach (KeyValuePair<int, ActionTable> index in m_actionList)
            {
                ActionTable record = new ActionTable();
                record = index.Value;

                if (record.moduleID == moduleID)
                {
                    actions.Add(record.pkey);
                }
            }
            return actions;
        }
    }

    public class ActionTable
    {
        public int pkey { get; set; }
        public string actionName { get; set; }
        public string uiActionName { get; set; }
        public int moduleID { get; set; }
    }
}