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
    public class StaffOrgStruct : ITableModel
    {
        static private StaffOrgStruct m_instance = null;
        private SortedDictionary<int, int> m_childNodes = new SortedDictionary<int, int>();
        private SortedDictionary<int, StaffOrgStructTable> m_StaffOrgList = new SortedDictionary<int, StaffOrgStructTable>();

        private StaffOrgStruct()
        {
            load();
        }

        static public StaffOrgStruct getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new StaffOrgStruct();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, StaffOrgStructTable> materielGroupList = new SortedDictionary<int, StaffOrgStructTable>();
            string materielQuery = "SELECT PKEY, VALUE, PARENT_PKEY FROM  BASE_STAFF_ORG_STRUCT ORDER BY PKEY";

            if (m_StaffOrgList.Count > 0)
            {
                m_StaffOrgList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, materielQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    StaffOrgStructTable record = new StaffOrgStructTable();
                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.value = DbDataConvert.ToInt32(row[1]);
                    record.parentPkey = DbDataConvert.ToInt32(row[2]);

                    materielGroupList.Add(record.pkey, record);
                }
            }

            m_StaffOrgList = materielGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_STAFF_ORG_STRUCT", pkey))
            {
                load();
            }
        }

        public void update(int pkey, StaffOrgStructTable materielOrgStruct)
        {
            string update = "UPDATE [dbo].[BASE_STAFF_ORG_STRUCT] SET ";

            update += "[VALUE] = '" + materielOrgStruct.value + "',";
            update += "[PARENT_PKEY] = " + Convert.ToString(materielOrgStruct.parentPkey) + " ";
            update += "WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("数据修改成功");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void insert(StaffOrgStructTable staffOrgStruct)
        {
            string insert = "INSERT INTO [dbo].[BASE_STAFF_ORG_STRUCT] ([VALUE],[PARENT_PKEY]) VALUES (";

            insert += +staffOrgStruct.value + ",";
            insert += staffOrgStruct.parentPkey;
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

        public int getRootNodePkey()
        {
            int rootNodeID = 0;

            StaffOrgStructTable record = new StaffOrgStructTable();
            foreach (KeyValuePair<int, StaffOrgStructTable> index in m_StaffOrgList)
            {
                record = index.Value;
                if (record.parentPkey == 0)
                {
                    rootNodeID = record.pkey;
                }
            }

            return rootNodeID;
        }

        public int getNoteValueFromPkey(int pkey)
        {
            int noteValue = 0;

            if (m_StaffOrgList.ContainsKey(pkey))
            {
                StaffOrgStructTable record = (StaffOrgStructTable)m_StaffOrgList[pkey];
                noteValue = record.value;
            }

            return noteValue;
        }

        public ArrayList getNodesFormParentID(int parentID)
        {
            ArrayList nodes = new ArrayList();

            StaffOrgStructTable record = new StaffOrgStructTable();

            foreach (KeyValuePair<int, StaffOrgStructTable> index in m_StaffOrgList)
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
            int peky = 0;
            ArrayList nodes = new ArrayList();

            StaffOrgStructTable record = new StaffOrgStructTable();

            foreach (KeyValuePair<int, StaffOrgStructTable> index in m_StaffOrgList)
            {
                record = index.Value;
                if (record.value == value)
                {
                    peky = record.pkey;
                }
            }

            return peky;
        }

        public SortedDictionary<int, int> getAllChildNodeValue(int value)
        {
            m_childNodes.Clear();
            getAStaffOrgValue(getPkeyFromValue(value), value);
            return m_childNodes;
        }

        private void getAStaffOrgValue(int parentID, int value)
        {
            ArrayList nodeList = getNodesFormParentID(parentID);

            if (nodeList.Count == 0)
            {
                if (!m_childNodes.ContainsKey(value))
                {
                    m_childNodes.Add(value, value);
                }
            }
            else
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    StaffOrgStructTable record = (StaffOrgStructTable)nodeList[i];

                    if (!m_childNodes.ContainsKey(record.value))
                    {
                        m_childNodes.Add(record.value, record.value);
                    }

                    getAStaffOrgValue(record.pkey, record.value);
                }
            }
        }
    }

    public class StaffOrgStructTable
    {
        public int pkey { get; set; }
        public int value { get; set; }
        public int parentPkey { get; set; }
    }
}
