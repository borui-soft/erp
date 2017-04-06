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
    public class AuxiliaryMaterial : ITableModel
    {
        private SortedDictionary<int, AuxiliaryMaterialTable> m_auxiliaryMaterialList = new SortedDictionary<int, AuxiliaryMaterialTable>();
        private SortedDictionary<string, SortedDictionary<int, AuxiliaryMaterialDataTable>> m_materialList = new SortedDictionary<string, SortedDictionary<int, AuxiliaryMaterialDataTable>>();

        static private AuxiliaryMaterial m_instance = null;

        private AuxiliaryMaterial()
        {
            loadAuxiliaryMaterialTable();
        }

        static public AuxiliaryMaterial getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new AuxiliaryMaterial();
            }

            return m_instance;
        }

        #region BASE_AUXILIARY_MATERIAL
        private void loadAuxiliaryMaterialTable()
        {
            string sql = "SELECT [PKEY],[NODE_NAME],[TABLE_NAME] FROM [dbo].[BASE_AUXILIARY_MATERIAL] ORDER BY PKEY";

            m_auxiliaryMaterialList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    AuxiliaryMaterialTable record = new AuxiliaryMaterialTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.nodeName = DbDataConvert.ToString(row[1]);
                    record.tableName = DbDataConvert.ToString(row[2]);

                    m_auxiliaryMaterialList.Add(m_auxiliaryMaterialList.Count, record);
                }
            }
        }

        public SortedDictionary<int, AuxiliaryMaterialTable> getAllAuxiliaryMaterialInfo()
        {
            if (m_auxiliaryMaterialList.Count == 0)
            {
                loadAuxiliaryMaterialTable();
            }

            return m_auxiliaryMaterialList;
        }

        public string getAuxiliaryMaterialTableNameFromPkey(int pkey)
        {
            if (m_auxiliaryMaterialList.Count == 0)
            {
                loadAuxiliaryMaterialTable();
            }

            string tableName = "";

            foreach (KeyValuePair<int, AuxiliaryMaterialTable> index in m_auxiliaryMaterialList)
            {
                AuxiliaryMaterialTable record = new AuxiliaryMaterialTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    tableName = record.tableName;
                    break;
                }
            }

            return tableName;
        }
        #endregion

        #region 辅助资料中对应的具体表
        public void insert(string tableName, AuxiliaryMaterialDataTable material)
        {
            string insert = "INSERT INTO [dbo].[" + tableName + "]([NAME],[DESC],[IS_ALLOW_DELETE]) VALUES(";

            insert += "'" + material.name + "',";
            insert += "'" + material.desc + "',";
            insert += "1";
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                MessageBoxExtend.messageOK("数据保存成功");

                load(tableName);
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void delete(int pkey, string tableName)
        {
            if (recordIsAllowDelete(tableName, pkey))
            {
                if (deleteRecord(tableName, pkey))
                {
                    load(tableName);
                }
            }
            else 
            {
                MessageBoxExtend.messageWarning("数据删除失败,本条数据为系统基础数据,不能删除.");
            }
        }

        public void update(int pkey, string tableName, string value, string desc)
        {
            if (recordIsAllowDelete(tableName, pkey))
            {
                string update = "UPDATE [dbo].[" + tableName + "] SET ";
                update += "[NAME] = '" + value + "',";
                update += "[DESC] = '" + desc + "' ";
                update += "WHERE PKEY = " + Convert.ToString(pkey);

                try
                {
                    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                    MessageBoxExtend.messageOK("数据修改成功");

                    load(tableName);
                }
                catch (Exception error)
                {
                    MessageBoxExtend.messageWarning(error.Message);
                    return;
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据修改失败,本条数据为系统基础数据,不能修改.");
            }
        }

        private void load(string tableName)
        {
            if (tableName.Length > 0)
            {
                string sql = "SELECT [PKEY],[NAME],[DESC],[IS_ALLOW_DELETE] FROM [dbo].[" + tableName + "] ORDER BY PKEY";

                m_materialList.Clear();

                using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
                {
                    SortedDictionary<int, AuxiliaryMaterialDataTable> materialList = new SortedDictionary<int, AuxiliaryMaterialDataTable>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        AuxiliaryMaterialDataTable record = new AuxiliaryMaterialDataTable();

                        record.pkey = DbDataConvert.ToInt32(row[0]);
                        record.name = DbDataConvert.ToString(row[1]);
                        record.desc = DbDataConvert.ToString(row[2]);
                        record.isAllowDelete = DbDataConvert.ToInt32(row[3]);

                        materialList.Add(record.pkey, record);
                    }

                    if (m_materialList.ContainsKey(tableName))
                    {
                        m_materialList[tableName] = materialList;
                    }
                    else
                    {
                        m_materialList.Add(tableName, materialList);
                    }
                }
            }
        }

        public SortedDictionary<int, AuxiliaryMaterialDataTable> getAllAuxiliaryMaterialData(string tableName)
        {
            SortedDictionary<int, AuxiliaryMaterialDataTable> materialList = new SortedDictionary<int, AuxiliaryMaterialDataTable>();

            if (tableName.Length > 0)
            {
                if (!m_materialList.ContainsKey(tableName))
                {
                    load(tableName);
                }

                materialList = m_materialList[tableName];
            }

            return materialList;
        }

        public string getAuxiliaryMaterialNameFromPkey(string tableName, int pkey)
        {
            string name = "";
            SortedDictionary<int, AuxiliaryMaterialDataTable> materialList = new SortedDictionary<int, AuxiliaryMaterialDataTable>();
            

            if (tableName.Length > 0)
            {
                if (!m_materialList.ContainsKey(tableName))
                {
                    load(tableName);
                }

                materialList = m_materialList[tableName];
            }

            foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in materialList)
            {
                AuxiliaryMaterialDataTable record = new AuxiliaryMaterialDataTable();
                record = index.Value;
                if(record.pkey == pkey)
                {
                    name = record.name;
                    break;
                }
            }

            return name;
        }

        public SortedDictionary<int, AuxiliaryMaterialDataTable> getAuxiliaryListFromTableName(string tableName)
        {
            SortedDictionary<int, AuxiliaryMaterialDataTable> materialList = new SortedDictionary<int, AuxiliaryMaterialDataTable>();


            if (tableName.Length > 0)
            {
                if (!m_materialList.ContainsKey(tableName))
                {
                    load(tableName);
                }

                materialList = m_materialList[tableName];
            }

            return materialList;
        }

        public int getAuxiliaryMaterialPkeyFromName(string tableName, string name)
        {
            int pkey = 0;
            SortedDictionary<int, AuxiliaryMaterialDataTable> materialList = new SortedDictionary<int, AuxiliaryMaterialDataTable>();


            if (tableName.Length > 0)
            {
                if (!m_materialList.ContainsKey(tableName))
                {
                    load(tableName);
                }

                materialList = m_materialList[tableName];
            }

            foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in materialList)
            {
                AuxiliaryMaterialDataTable record = new AuxiliaryMaterialDataTable();
                record = index.Value;
                if (record.name == name)
                {
                    pkey = record.pkey;
                    break;
                }
            }

            return pkey;
        }

        private bool recordIsAllowDelete(string tableName, int pkey)
        {
            bool isRet = false;
            SortedDictionary<int, AuxiliaryMaterialDataTable> tableData = new SortedDictionary<int, AuxiliaryMaterialDataTable>();

            if (tableName.Length > 0)
            {
                if (!m_materialList.ContainsKey(tableName))
                {
                    load(tableName);
                }

                tableData = m_materialList[tableName];
            }

            foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in tableData)
            {
                if (index.Value.pkey == pkey)
                {
                    if (index.Value.isAllowDelete == 1)
                    {
                        isRet = true;
                    }
                    break;
                }
            }

            return isRet;
        }
        #endregion
    }

    public class AuxiliaryMaterialTable
    {
        public int pkey { get; set; }
        public string nodeName { get; set; }
        public string tableName { get; set; }
    }

    public class AuxiliaryMaterialDataTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public int isAllowDelete { get; set; }
    }
}