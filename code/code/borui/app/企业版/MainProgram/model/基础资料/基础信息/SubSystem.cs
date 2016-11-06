using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class SubSystem : ITableModel
    {
        private SortedDictionary<int, SubSystemTable> m_subSystemList = new SortedDictionary<int, SubSystemTable>();

        static private SubSystem m_instance = null;

        private SubSystem()
        {
            load();
        }

        static public SubSystem getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SubSystem();
            }

            return m_instance;
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[ID],[NAME] FROM [dbo].[BASE_SUB_SYSTEM_LIST] ORDER BY PKEY";

            m_subSystemList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SubSystemTable record = new SubSystemTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.id = DbDataConvert.ToInt32(row["ID"]);
                    record.name = DbDataConvert.ToString(row["NAME"]);

                    m_subSystemList.Add(m_subSystemList.Count, record);
                }
            }
        }

        public SortedDictionary<int, SubSystemTable> getAllSubSystemInfo()
        {
            if (m_subSystemList.Count == 0)
            {
                load();
            }

            return m_subSystemList;
        }

        public string getsubSystemNameFromSystemID(int SystemID)
        {
            string name = "";

            if (m_subSystemList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, SubSystemTable> index in m_subSystemList)
            {
                if (index.Value.id == SystemID)
                {
                    name = index.Value.name;
                    break;
                }
            }

            return name;
        }
    }

    public class SubSystemTable
    {
        public int pkey { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}