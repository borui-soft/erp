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
    public class Materiel : ITableModel
    {
        private SortedDictionary<int, MaterielTable> m_noForbidMaterielList = new SortedDictionary<int, MaterielTable>();
        private SortedDictionary<int, MaterielTable> m_forbidMaterielList = new SortedDictionary<int, MaterielTable>();

        static private Materiel m_instance = null;

        private Materiel()
        {
            load();
        }

        static public Materiel getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new Materiel();
            }

            return m_instance;
        }

        public void insert(MaterielTable materiel)
        {
            string insert = "INSERT INTO [dbo].[BASE_MATERIEL_LIST] ([SUPPLIER_TYPE],[NAME],[NUM],[NAME_SHORT],[MODEL],[MNEMONIC_CODE],";
            insert += "[MAX],[MIN],[WARRANTY],[MATERIEL_ATTRIBUTE],[UNIT],[UNIT_PURCHASE],[UNIT_SALE],[UNIT_STORAGE],";
            insert += "[VALUATION],[NOTE],[STORAGE],[IS_FORBID]) VALUES(";

            insert += materiel.materielType + ",";
            insert += "'" + materiel.name + "',";
            insert += "'" + materiel.num + "',";
            insert += "'" + materiel.nameShort + "',";
            insert += "'" + materiel.model + "',";
            insert += "'" + materiel.mnemonicCode + "',";
            insert += materiel.max + ",";
            insert += materiel.min + ",";
            insert += materiel.warramty + ",";
            insert += materiel.materielAttribute + ",";
            insert += materiel.unit + ",";
            insert += materiel.unitPurchase + ",";
            insert += materiel.unitSale + ",";
            insert += materiel.unit + ",";
            insert += materiel.valuation + ",";
            insert += "'" + materiel.note + "',";
            insert += materiel.storage + ", 0";
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

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_MATERIEL_LIST", pkey))
            {
                load();
            }
        }

        public void update(int pkey, MaterielTable materiel)
        {
            string update = "UPDATE [dbo].[BASE_MATERIEL_LIST] SET ";

            //update += "[SUPPLIER_TYPE] = " + materiel.materielType + ",";
            update += "[NAME] = '" + materiel.name + "',";
            update += "[NUM] = '" + materiel.num + "',";
            update += "[NAME_SHORT] = '" + materiel.nameShort + "',";
            update += "[MODEL] = '" + materiel.model + "',";
            update += "[MNEMONIC_CODE] = '" + materiel.mnemonicCode + "',";

            update += "[MAX] = " + materiel.max + ",";
            update += "[MIN] = " + materiel.min + ",";
            update += "[WARRANTY] = " + materiel.warramty + ",";
            update += "[STORAGE] = " + materiel.storage + ",";
            update += "[MATERIEL_ATTRIBUTE] = " + materiel.materielAttribute + ",";
            update += "[UNIT] = " + materiel.unit + ",";
            update += "[UNIT_PURCHASE] = " + materiel.unitPurchase + ",";
            update += "[UNIT_SALE] = " + materiel.unitSale + ",";
            update += "[UNIT_STORAGE] = " + materiel.unitStorage + ",";
            update += "[VALUATION] = " + materiel.valuation + ",";

            update += "[NOTE] = '" + materiel.note + "'";


            update += " WHERE PKEY = " + Convert.ToString(pkey);

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

        private void load()
        {
            string materielQuery = "SELECT [PKEY],[SUPPLIER_TYPE],[NAME],[NUM],[NAME_SHORT],[MODEL],[MNEMONIC_CODE],[MAX],[MIN],[WARRANTY],";
            materielQuery += "[MATERIEL_ATTRIBUTE],[UNIT],[UNIT_PURCHASE],[UNIT_SALE],[UNIT_STORAGE],[VALUATION],[NOTE],[STORAGE],[IS_FORBID]";
            materielQuery += " FROM [dbo].[BASE_MATERIEL_LIST] ORDER BY PKEY";

            m_noForbidMaterielList.Clear();
            m_forbidMaterielList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, materielQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielTable materiel = new MaterielTable();
                    materiel.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    materiel.materielType = DbDataConvert.ToInt32(row["SUPPLIER_TYPE"]);
                    materiel.name = DbDataConvert.ToString(row["NAME"]);
                    materiel.num = DbDataConvert.ToString(row["NUM"]);
                    materiel.nameShort = DbDataConvert.ToString(row["NAME_SHORT"]);
                    materiel.model = DbDataConvert.ToString(row["MODEL"]);
                    materiel.mnemonicCode = DbDataConvert.ToString(row["MNEMONIC_CODE"]);
                    materiel.max = DbDataConvert.ToInt32(row["MAX"]);
                    materiel.min = DbDataConvert.ToInt32(row["MIN"]);
                    materiel.warramty = DbDataConvert.ToInt32(row["WARRANTY"]);
                    materiel.materielAttribute = DbDataConvert.ToInt32(row["MATERIEL_ATTRIBUTE"]);
                    materiel.unit = DbDataConvert.ToInt32(row["UNIT"]);
                    materiel.unitPurchase = DbDataConvert.ToInt32(row["UNIT_PURCHASE"]);
                    materiel.unitSale = DbDataConvert.ToInt32(row["UNIT_SALE"]);
                    materiel.unitStorage = DbDataConvert.ToInt32(row["UNIT_STORAGE"]);
                    materiel.valuation = DbDataConvert.ToInt32(row["VALUATION"]);
                    materiel.note = DbDataConvert.ToString(row["NOTE"]);
                    materiel.storage = DbDataConvert.ToInt32(row["STORAGE"]);
                    materiel.isForbid = DbDataConvert.ToInt32(row["IS_FORBID"]);

                    if (materiel.isForbid == 0)
                    {
                        m_noForbidMaterielList.Add(m_noForbidMaterielList.Count, materiel);
                    }
                    else
                    {
                        m_forbidMaterielList.Add(m_forbidMaterielList.Count, materiel);
                    }
                }
            }
        }

        public SortedDictionary<int, MaterielTable> getAllMaterielInfo()
        {
            if (m_noForbidMaterielList.Count == 0)
            {
                load();
            }

            return m_noForbidMaterielList;
        }

        public void refrensRecord()
        {
            load();
        }

        public SortedDictionary<int, MaterielTable> getAllForbidMaterielInfo()
        {
            if (m_noForbidMaterielList.Count == 0)
            {
                load();
            }

            return m_forbidMaterielList;
        }

        public SortedDictionary<int, MaterielTable> getMaterielInfoFromMaterielType(int materielTypePkey)
        {
            if (m_noForbidMaterielList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, MaterielTable> materielList = new SortedDictionary<int, MaterielTable>();

            foreach (KeyValuePair<int, MaterielTable> index in m_noForbidMaterielList)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = index.Value;

                if (materiel.materielType == materielTypePkey)
                {
                    materielList.Add(materielList.Count, materiel);
                }
            }

            return materielList;
        }

        public MaterielTable getMaterielInfoFromPkey(int pkey)
        {
            if (m_noForbidMaterielList.Count == 0)
            {
                load();
            }

            MaterielTable materiel = new MaterielTable();

            foreach (KeyValuePair<int, MaterielTable> index in m_noForbidMaterielList)
            {
                MaterielTable record = new MaterielTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    materiel = record;
                }
            }

            return materiel;
        }

        public MaterielTable getMaterielInfoFromMaterielName(string materielName)
        {
            if (m_noForbidMaterielList.Count == 0)
            {
                load();
            }

            MaterielTable materiel = new MaterielTable();
            materiel = null;

            foreach (KeyValuePair<int, MaterielTable> index in m_noForbidMaterielList)
            {
                MaterielTable record = new MaterielTable();
                record = index.Value;

                if (record.name == materielName)
                {
                    materiel = record;
                }
            }

            return materiel;
        }

        public void forbidMateriel(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_MATERIEL_LIST] SET [IS_FORBID] = 1 WHERE PKEY = ";
            update += Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void noForbidMateriel(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_MATERIEL_LIST] SET [IS_FORBID] = 0 WHERE PKEY = ";
            update += Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        // 根据页面搜索结果查询数据库
        public SortedDictionary<int, MaterielTable> getMaterielInfoFromSerachTerm(string serachTerm)
        {
            if (m_noForbidMaterielList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, MaterielTable> materielList = new SortedDictionary<int, MaterielTable>();

            foreach (KeyValuePair<int, MaterielTable> index in m_noForbidMaterielList)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = index.Value;

                if (materiel.name.IndexOf(serachTerm) >= 0)
                {
                    materielList.Add(materielList.Count, materiel);
                }
            }

            return materielList;
        }
    }

    public class MaterielTable
    {
        public int pkey { get; set; }
        public int materielType { get; set; }
        public string name { get; set; }
        public string num { get; set; }
        public string nameShort { get; set; }
        public string model { get; set; }
        public string mnemonicCode { get; set; }
        public int max { get; set; }
        public int min { get; set; }
        public int warramty { get; set; }
        public int storage { get; set; }
        public int materielAttribute { get; set; }
        public int unit { get; set; }
        public int unitPurchase { get; set; }
        public int unitSale { get; set; }
        public int unitStorage { get; set; }
        public int valuation { get; set; }
        public string note { get; set; }
        public int isForbid { get; set; }
    }
}