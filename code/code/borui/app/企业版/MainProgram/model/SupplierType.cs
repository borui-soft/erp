using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class SupplierType
    {
        private SortedDictionary<int, SupplierTypeTable> m_SupplierTypeList = new SortedDictionary<int, SupplierTypeTable>();

        static private SupplierType m_instance = null;

        private SupplierType()
        {
            loadSupplierTypeInfo(out m_SupplierTypeList);
        }

        static public SupplierType getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SupplierType();
            }

            return m_instance;
        }

        private void loadSupplierTypeInfo(out SortedDictionary<int, SupplierTypeTable> supplierTypeList)
        {
            SortedDictionary<int, SupplierTypeTable> supplierGroupList = new SortedDictionary<int, SupplierTypeTable>();
            string supplierQuery = "SELECT PKEY, TYPE_NAME, [DESC] FROM  BASE_SUPPLIER_TYPE ORDER BY PKEY";

            if (m_SupplierTypeList.Count > 0)
            {
                m_SupplierTypeList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, supplierQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SupplierTypeTable supplierTyoe = new SupplierTypeTable();
                    supplierTyoe.pkey = DbDataConvert.ToInt32(row[0]);
                    supplierTyoe.name = DbDataConvert.ToString(row[1]);
                    supplierTyoe.desc = DbDataConvert.ToString(row[2]);

                    supplierGroupList.Add(supplierTyoe.pkey, supplierTyoe);
                }
            }

            supplierTypeList = supplierGroupList;
        }

        public string getSupplierTypeNameFromPkey(int pkey)
        {
            string supplierTypeName = "未知分类名称";

            if (m_SupplierTypeList.ContainsKey(pkey))
            {
                SupplierTypeTable record = (SupplierTypeTable)m_SupplierTypeList[pkey];
                supplierTypeName = record.name;
            }

            return supplierTypeName;
        }
    }

    public class SupplierTypeTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
    }
}
