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
    public class SupplierType : ITableModel
    {
        private SortedDictionary<int, SupplierTypeTable> m_SupplierTypeList = new SortedDictionary<int, SupplierTypeTable>();

        static private SupplierType m_instance = null;

        private SupplierType()
        {
            load();
        }

        static public SupplierType getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SupplierType();
            }

            return m_instance;
        }

        private void load()
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

            m_SupplierTypeList = supplierGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_SUPPLIER_TYPE", pkey, false))
            {
                load();
            }
        }

        public void update(int pkey, SupplierTypeTable supplierType)
        {
            string update = "UPDATE [dbo].[BASE_SUPPLIER_TYPE] SET ";

            update += "[TYPE_NAME] = '" + supplierType.name + "',";
            update += "[DESC] = '" + supplierType.desc + "' ";
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

        public void insert(SupplierTypeTable supplierType)
        {
            string insert = "INSERT INTO [dbo].[BASE_SUPPLIER_TYPE] ([TYPE_NAME],[DESC]) VALUES (";

            insert += "'" + supplierType.name + "',";
            insert += "'" + supplierType.desc + "'";
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

        public SupplierTypeTable getSupplierTypeInfoFromPkey(int pkey)
        {
            SupplierTypeTable supplierType = new SupplierTypeTable();

            if (m_SupplierTypeList.ContainsKey(pkey))
            {
                supplierType = (SupplierTypeTable)m_SupplierTypeList[pkey];
            }

            return supplierType;
        }

        public int getMaxPkey()
        {
            int peky = -1;

            foreach (KeyValuePair<int, SupplierTypeTable> index in m_SupplierTypeList)
            {
                if(index.Key > peky)
                {
                    peky = index.Key;
                }
            }

            return peky;
        }
    }

    public class SupplierTypeTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
    }
}
