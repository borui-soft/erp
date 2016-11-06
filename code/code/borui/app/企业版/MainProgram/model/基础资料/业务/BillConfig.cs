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
    public class BillConfig : ITableModel
    {
        private SortedDictionary<int, BillConfigTable> m_billConfigList = new SortedDictionary<int, BillConfigTable>();

        static private BillConfig m_instance = null;

        private BillConfig()
        {
            load();
        }

        static public BillConfig getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new BillConfig();
            }

            return m_instance;
        }

        #region BASE_BILL_CONFIG
        private void load()
        {
            string sql = "SELECT [PKEY],[BILL_TYPE],[NAME],[CODE],[IS_INPUT],[IS_AUTO_SAVE],[IS_USE_RULES],[FRONT],[IS_USE_SYSDATE],[NUM] ";
            sql += "FROM [dbo].[BASE_BILL_CONFIG] ORDER BY PKEY";

            m_billConfigList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BillConfigTable record = new BillConfigTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.billpType = DbDataConvert.ToInt32(row["BILL_TYPE"]);
                    record.name = DbDataConvert.ToString(row["NAME"]);
                    record.code = DbDataConvert.ToString(row["CODE"]);
                    record.isInput = DbDataConvert.ToInt32(row["IS_INPUT"]);
                    record.isAutoSave = DbDataConvert.ToInt32(row["IS_AUTO_SAVE"]);
                    record.isUseRules = DbDataConvert.ToInt32(row["IS_USE_RULES"]);
                    record.front = DbDataConvert.ToString(row["FRONT"]);
                    record.isUseSysdate = DbDataConvert.ToInt32(row["IS_USE_SYSDATE"]);
                    record.num = DbDataConvert.ToInt32(row["NUM"]);

                    m_billConfigList.Add(m_billConfigList.Count, record);
                }
            }
        }

        public SortedDictionary<int, BillConfigTable> getAllBillConfigInfo()
        {
            if (m_billConfigList.Count == 0)
            {
                load();
            }

            return m_billConfigList;
        }

        public BillConfigTable getBillConfigInfoFromPeky(int pkey)
        {
            BillConfigTable billConfig = new BillConfigTable();

            if (m_billConfigList.Count == 0)
            {
                load();
            }

            foreach(KeyValuePair<int, BillConfigTable> index in m_billConfigList)
            {
                BillConfigTable record = new BillConfigTable();
                record = index.Value;
                if(record.pkey == pkey)
                {
                    billConfig = record;
                    break;
                }
            }

            return billConfig;
        }

        public void update(int pkey, BillConfigTable record)
        {
            string sql = "UPDATE [dbo].[BASE_BILL_CONFIG] SET ";

            sql += "CODE = '" + Convert.ToString(record.code) + "',";
            sql += "IS_INPUT = " + Convert.ToString(record.isInput) + ",";
            sql += "IS_AUTO_SAVE = " + Convert.ToString(record.isAutoSave) + ",";
            sql += "IS_USE_SYSDATE = " + Convert.ToString(record.isUseSysdate) + ",";
            sql += "NUM = " + Convert.ToString(record.num) + ",";
            sql += "FRONT = '" + Convert.ToString(record.front) + "',";
            sql += "IS_USE_RULES = " + Convert.ToString(record.isUseRules);
            sql += "WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, sql);

                MessageBoxExtend.messageOK("数据修改成功");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        #endregion
    }

    public class BillConfigTable
    {
        public int pkey { get; set; }
        public int billpType { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int isInput { get; set; }
        public int isAutoSave { get; set; }
        public int isUseRules { get; set; }
        public string front { get; set; }
        public int isUseSysdate { get; set; }
        public int num { get; set; }
    }
}