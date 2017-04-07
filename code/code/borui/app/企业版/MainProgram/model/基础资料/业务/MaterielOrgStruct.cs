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
    public class MaterielOrgStruct : ITableModel
    {
        static private MaterielOrgStruct m_instance = null;
        private SortedDictionary<int, int> m_childNodes = new SortedDictionary<int, int>();
        private SortedDictionary<int, MaterielOrgStructTable> m_MaterielOrgList = new SortedDictionary<int, MaterielOrgStructTable>();

        private MaterielOrgStruct()
        {
            load();
        }

        static public MaterielOrgStruct getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielOrgStruct();
            }

            return m_instance;
        }

        public void refreshRecord()
        {
            load();
        }

        private void load()
        {
            SortedDictionary<int, MaterielOrgStructTable> materielGroupList = new SortedDictionary<int, MaterielOrgStructTable>();
            string materielQuery = "SELECT PKEY, VALUE, PARENT_PKEY FROM  BASE_MATERIEL_ORG_STRUCT ORDER BY PKEY";

            if (m_MaterielOrgList.Count > 0)
            {
                m_MaterielOrgList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, materielQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielOrgStructTable record = new MaterielOrgStructTable();
                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.value = DbDataConvert.ToInt32(row[1]);
                    record.parentPkey = DbDataConvert.ToInt32(row[2]);

                    materielGroupList.Add(record.pkey, record);
                }
            }

            m_MaterielOrgList = materielGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_MATERIEL_ORG_STRUCT", pkey))
            {
                load();
            }
        }

        public void update(int pkey, MaterielOrgStructTable materielOrgStruct)
        {
            string update = "UPDATE [dbo].[BASE_MATERIEL_ORG_STRUCT] SET ";

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

        public void insert(MaterielOrgStructTable materielOrgStruct, bool isDisplyMessage = true)
        {
            string insert = "INSERT INTO [dbo].[BASE_MATERIEL_ORG_STRUCT] ([VALUE],[PARENT_PKEY]) VALUES (";

            insert += "'" + materielOrgStruct.value + "',";
            insert += Convert.ToString(materielOrgStruct.parentPkey);
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                if (isDisplyMessage)
                {
                    MessageBoxExtend.messageOK("数据保存成功");
                }

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

            MaterielOrgStructTable record = new MaterielOrgStructTable();
            foreach (KeyValuePair<int, MaterielOrgStructTable> index in m_MaterielOrgList)
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

            if (m_MaterielOrgList.ContainsKey(pkey))
            {
                MaterielOrgStructTable record = (MaterielOrgStructTable)m_MaterielOrgList[pkey];
                noteValue = record.value;
            }

            return noteValue;
        }

        public ArrayList getNodesFormParentID(int parentID)
        {
            ArrayList nodes = new ArrayList();

            MaterielOrgStructTable record = new MaterielOrgStructTable();

            foreach (KeyValuePair<int, MaterielOrgStructTable> index in m_MaterielOrgList)
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

            MaterielOrgStructTable record = new MaterielOrgStructTable();

            foreach (KeyValuePair<int, MaterielOrgStructTable> index in m_MaterielOrgList)
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
            getAMaterielOrgValue(getPkeyFromValue(value), value);
            return m_childNodes;
        }

        private void getAMaterielOrgValue(int parentID, int value)
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
                    MaterielOrgStructTable record = (MaterielOrgStructTable)nodeList[i];

                    if (!m_childNodes.ContainsKey(record.value))
                    {
                        m_childNodes.Add(record.value, record.value);
                    }

                    getAMaterielOrgValue(record.pkey, record.value);
                }
            }
        }
    }

    public class MaterielOrgStructTable
    {
        public int pkey { get; set; }
        public int value { get; set; }
        public int parentPkey { get; set; }
    }
}
