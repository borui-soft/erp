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
    public class SupplierOrgStruct : ITableModel
    {
        static private SupplierOrgStruct m_instance = null;
        private SortedDictionary<int, int> m_childNodes = new SortedDictionary<int, int>();
        private SortedDictionary<int, SupplierOrgStructTable> m_SupplierOrgList = new SortedDictionary<int, SupplierOrgStructTable>();

        private SupplierOrgStruct()
        {
            load();
        }

        static public SupplierOrgStruct getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SupplierOrgStruct();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, SupplierOrgStructTable> supplierGroupList = new SortedDictionary<int, SupplierOrgStructTable>();
            string supplierQuery = "SELECT PKEY, VALUE, PARENT_PKEY FROM  BASE_SUPPLIER_ORG_STRUCT ORDER BY PKEY";

            if (m_SupplierOrgList.Count > 0)
            {
                m_SupplierOrgList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, supplierQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SupplierOrgStructTable record = new SupplierOrgStructTable();
                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.value = DbDataConvert.ToInt32(row[1]);
                    record.parentPkey = DbDataConvert.ToInt32(row[2]);

                    supplierGroupList.Add(record.pkey, record);
                }
            }

            m_SupplierOrgList = supplierGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_SUPPLIER_ORG_STRUCT", pkey))
            {
                load();
            }
        }

        public void update(int pkey, SupplierOrgStructTable supplierOrgStruct)
        {
            string update = "UPDATE [dbo].[BASE_SUPPLIER_ORG_STRUCT] SET ";

            update += "[VALUE] = '" + supplierOrgStruct.value + "',";
            update += "[PARENT_PKEY] = " + Convert.ToString(supplierOrgStruct.parentPkey) + " ";
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

        public void insert(SupplierOrgStructTable supplierOrgStruct)
        {
            string insert = "INSERT INTO [dbo].[BASE_SUPPLIER_ORG_STRUCT] ([VALUE],[PARENT_PKEY]) VALUES (";

            insert += "'" + supplierOrgStruct.value + "',";
            insert += Convert.ToString(supplierOrgStruct.parentPkey);
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

            SupplierOrgStructTable record = new SupplierOrgStructTable();
            foreach (KeyValuePair<int, SupplierOrgStructTable> index in m_SupplierOrgList)
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

            if (m_SupplierOrgList.ContainsKey(pkey))
            {
                SupplierOrgStructTable record = (SupplierOrgStructTable)m_SupplierOrgList[pkey];
                noteValue = record.value;
            }

            return noteValue;
        }

        public ArrayList getNodesFormParentID(int parentID)
        {
            ArrayList nodes = new ArrayList();

            SupplierOrgStructTable record = new SupplierOrgStructTable();

            foreach (KeyValuePair<int, SupplierOrgStructTable> index in m_SupplierOrgList)
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

            SupplierOrgStructTable record = new SupplierOrgStructTable();

            foreach (KeyValuePair<int, SupplierOrgStructTable> index in m_SupplierOrgList)
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
            getASupplierOrgValue(getPkeyFromValue(value), value);
            return m_childNodes;
        }

        private void getASupplierOrgValue(int parentID, int value)
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
                    SupplierOrgStructTable record = (SupplierOrgStructTable)nodeList[i];

                    if (!m_childNodes.ContainsKey(record.value))
                    {
                        m_childNodes.Add(record.value, record.value);
                    }

                    getASupplierOrgValue(record.pkey, record.value);
                }
            }
        }
    }

    public class SupplierOrgStructTable
    {
        public int pkey { get; set; }
        public int value { get; set; }
        public int parentPkey { get; set; }
    }
}
