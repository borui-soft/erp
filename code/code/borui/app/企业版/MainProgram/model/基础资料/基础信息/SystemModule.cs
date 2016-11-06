using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class SystemModule : ITableModel
    {
        private SortedDictionary<int, SystemModuleTable> m_systemModuleList = new SortedDictionary<int, SystemModuleTable>();

        static private SystemModule m_instance = null;

        private SystemModule()
        {
            load();
        }

        static public SystemModule getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SystemModule();
            }

            return m_instance;
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[ID],[NAME],[SUB_SYSTEM_ID] FROM [dbo].[BASE_MODULE_LIST] ORDER BY SUB_SYSTEM_ID, ID";

            m_systemModuleList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SystemModuleTable record = new SystemModuleTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.id = DbDataConvert.ToInt32(row["ID"]);
                    record.name = DbDataConvert.ToString(row["NAME"]);
                    record.subSystemID = DbDataConvert.ToInt32(row["SUB_SYSTEM_ID"]);

                    m_systemModuleList.Add(m_systemModuleList.Count, record);
                }
            }
        }

        public SortedDictionary<int, SystemModuleTable> getAllSystemModuleInfo()
        {
            if (m_systemModuleList.Count == 0)
            {
                load();
            }

            return m_systemModuleList;
        }

        public SortedDictionary<int, SystemModuleTable> getSystemModuleInfo(int subSystemID)
        {
            if (m_systemModuleList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, SystemModuleTable> moduleList = new SortedDictionary<int, SystemModuleTable>();

            foreach (KeyValuePair<int, SystemModuleTable> index in m_systemModuleList)
            {
                SystemModuleTable record = new SystemModuleTable();
                record = index.Value;

                if (record.subSystemID == subSystemID)
                {
                    moduleList.Add(moduleList.Count, record);
                }
            }

            return moduleList;
        }

        public int getModuleIDFromModuleName(string moduleName)
        {
            int id = -1;

            if (m_systemModuleList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, SystemModuleTable> index in m_systemModuleList)
            {
                SystemModuleTable record = new SystemModuleTable();
                record = index.Value;

                if (record.name == moduleName)
                {
                    id = record.id;
                    break;
                }
            }

            return id;
        }

        public string getModuleNameFromModuleID(int muduleID)
        {
            string name = "";

            if (m_systemModuleList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, SystemModuleTable> index in m_systemModuleList)
            {
                if (index.Value.id == muduleID)
                {
                    name = index.Value.name;
                    break;
                }
            }

            return name;
        }

        public int getsystemIDFromModuleID(int muduleID)
        {
            int id = -1;

            if (m_systemModuleList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, SystemModuleTable> index in m_systemModuleList)
            {
                if (index.Value.id == muduleID)
                {
                    id = index.Value.subSystemID;
                    break;
                }
            }

            return id;
        }
    }

    public class SystemModuleTable
    {
        public int pkey { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int subSystemID { get; set; }
    }
}