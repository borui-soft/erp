using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;
using TIV.Core.DatabaseAccess;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class SupplierOrgStruct
    {
        private SortedDictionary<int, SupplierOrgStructTable> m_SupplierOrgList = new SortedDictionary<int, SupplierOrgStructTable>();

        static private SupplierOrgStruct m_instance = null;

        private SupplierOrgStruct()
        {
            loadSupplierTypeInfo(out m_SupplierOrgList);
        }

        static public SupplierOrgStruct getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SupplierOrgStruct();
            }

            return m_instance;
        }

        private void loadSupplierTypeInfo(out SortedDictionary<int, SupplierOrgStructTable> supplierTypeList)
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

            supplierTypeList = supplierGroupList;
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
    }

    public class SupplierOrgStructTable
    {
        public int pkey { get; set; }
        public int value { get; set; }
        public int parentPkey { get; set; }
    }
}
