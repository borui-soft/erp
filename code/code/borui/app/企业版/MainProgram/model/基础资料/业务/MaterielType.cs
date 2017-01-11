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
    public class MaterielType : ITableModel
    {
        private SortedDictionary<int, MaterielTypeTable> m_MaterielTypeList = new SortedDictionary<int, MaterielTypeTable>();

        static private MaterielType m_instance = null;

        private MaterielType()
        {
            load();
        }

        static public MaterielType getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielType();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, MaterielTypeTable> materielGroupList = new SortedDictionary<int, MaterielTypeTable>();
            string materielQuery = "SELECT PKEY, TYPE_NAME, GROUP_NUM, [DESC] FROM  BASE_MATERIEL_TYPE ORDER BY PKEY";

            if (m_MaterielTypeList.Count > 0)
            {
                m_MaterielTypeList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, materielQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielTypeTable materielTyoe = new MaterielTypeTable();
                    materielTyoe.pkey = DbDataConvert.ToInt32(row[0]);
                    materielTyoe.name = DbDataConvert.ToString(row[1]);
                    materielTyoe.num = DbDataConvert.ToString(row[2]);
                    materielTyoe.desc = DbDataConvert.ToString(row[3]);

                    materielGroupList.Add(materielTyoe.pkey, materielTyoe);
                }
            }

            m_MaterielTypeList = materielGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_MATERIEL_TYPE", pkey, false))
            {
                load();
            }
        }

        public void update(int pkey, MaterielTypeTable materielType)
        {
            string update = "UPDATE [dbo].[BASE_MATERIEL_TYPE] SET ";

            update += "[TYPE_NAME] = '" + materielType.name + "',";
            update += "[GROUP_NUM] = '" + materielType.num + "',";
            update += "[DESC] = '" + materielType.desc + "' ";
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

        public void insert(MaterielTypeTable materielType)
        {
            string insert = "INSERT INTO [dbo].[BASE_MATERIEL_TYPE] ([TYPE_NAME],[GROUP_NUM],[DESC]) VALUES (";

            insert += "'" + materielType.name + "',";
            insert += "'" + materielType.num + "',";
            insert += "'" + materielType.desc + "'";
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

        public string getMaterielTypeNameFromPkey(int pkey)
        {
            string materielTypeName = "未知分类名称";

            if (m_MaterielTypeList.ContainsKey(pkey))
            {
                MaterielTypeTable record = (MaterielTypeTable)m_MaterielTypeList[pkey];
                materielTypeName = record.name;
            }

            return materielTypeName;
        }

        public string getMaterielTypeNumFromPkey(int pkey)
        {
            string materielTypeNum = "";

            if (m_MaterielTypeList.ContainsKey(pkey))
            {
                MaterielTypeTable record = (MaterielTypeTable)m_MaterielTypeList[pkey];
                materielTypeNum = record.num;
            }

            return materielTypeNum;
        }

        public MaterielTypeTable getMaterielTypeInfoFromPkey(int pkey)
        {
            MaterielTypeTable materielType = new MaterielTypeTable();

            if (m_MaterielTypeList.ContainsKey(pkey))
            {
                materielType = (MaterielTypeTable)m_MaterielTypeList[pkey];
            }

            return materielType;
        }

        public int getMaxPkey()
        {
            int peky = -1;

            foreach (KeyValuePair<int, MaterielTypeTable> index in m_MaterielTypeList)
            {
                if(index.Key > peky)
                {
                    peky = index.Key;
                }
            }

            return peky;
        }
    }

    public class MaterielTypeTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string num { get; set; }
        public string desc { get; set; }
    }
}
