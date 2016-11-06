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
    public class SettlmentWay : ITableModel
    {
        private SortedDictionary<int, SettlmentWayTable> m_settlmentWayList = new SortedDictionary<int, SettlmentWayTable>();

        static private SettlmentWay m_instance = null;

        private string m_softwareKey = "";
        private SettlmentWay()
        {
            load();
        }

        static public SettlmentWay getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SettlmentWay();
            }

            return m_instance;
        }

        #region BASE_BILL_CONFIG

        public void insert(SettlmentWayTable record, bool isMessage = true)
        {
            string insert = "INSERT INTO [dbo].[BASE_SETTLMENT_WAY]([NAME],[SUBJECTID]) VALUES(";

            insert += "'" + record.name + "',";
            insert += "'" + record.subjectID + "'";
            insert += ")";

            try
            {                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                if (isMessage)
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

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_SETTLMENT_WAY", pkey))
            {
                load();
            }
        }

        public void update(int pkey, SettlmentWayTable record)
        {
            string sql = "UPDATE [dbo].[BASE_SETTLMENT_WAY] SET ";

            sql += "NAME = '" + record.name + "',";
            sql += "SUBJECTID = '" + record.subjectID + "'";
            sql += " WHERE PKEY = " + Convert.ToString(pkey);

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

        private void load()
        {
            string sql = "SELECT [PKEY],[NAME],[SUBJECTID] FROM [dbo].[BASE_SETTLMENT_WAY] ORDER BY PKEY";

            m_settlmentWayList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SettlmentWayTable record = new SettlmentWayTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.name = DbDataConvert.ToString(row["NAME"]);
                    record.subjectID = DbDataConvert.ToString(row["SUBJECTID"]);

                    // 如果subjectID等于#####，说明此条记录用于正版软件验证，则不用先到界面上
                    if (record.subjectID == "#####")
                    {
                        m_softwareKey = record.name;
                    }
                    else
                    {
                        m_settlmentWayList.Add(m_settlmentWayList.Count, record);
                    }
                }
            }
        }

        public SortedDictionary<int, SettlmentWayTable> getAllSettlmentWayInfo()
        {
            if (m_settlmentWayList.Count == 0)
            {
                load();
            }

            return m_settlmentWayList;
        }

        public SettlmentWayTable getSettlmentWayInfoFromPeky(int pkey)
        {
            SettlmentWayTable settlmentWay = new SettlmentWayTable();

            if (m_settlmentWayList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, SettlmentWayTable> index in m_settlmentWayList)
            {
                SettlmentWayTable record = new SettlmentWayTable();
                record = index.Value;
                if (record.pkey == pkey)
                {
                    settlmentWay = record;
                    break;
                }
            }

            return settlmentWay;
        }

        public string getSoftwareKey()
        {
            return m_softwareKey;
        }
        #endregion
    }

    public class SettlmentWayTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string subjectID { get; set; }
    }
}