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
    public class SystemInfo : ITableModel
    {
        static private SystemInfo m_instance = null;
        private SystemInfoTable m_systemInfo = new SystemInfoTable();
        private bool m_isExistRecord = false;

        private SystemInfo()
        {
            load();
        }

        static public SystemInfo getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SystemInfo();
            }

            return m_instance;
        }

        public void insert(SystemInfoTable systemInfo)
        {
            string insert = "INSERT INTO BASE_SYSTEM_INFO([COMPANY_NAME],[GROWS],[BANK_ACCOUNT],[PHONE],[FAX],[E_MAIL],[ADDRESS]) VALUES( ";

            insert += "'" + systemInfo.companyName + "',";
            insert += "'" + systemInfo.grows + "',";
            insert += "'" + systemInfo.bankAccount + "',";
            insert += "'" + systemInfo.phone + "',";
            insert += "'" + systemInfo.fax + "',";
            insert += "'" + systemInfo.eMail + "',";
            insert += "'" + systemInfo.address + "'";
            insert += ")";

            try
            {
                if (m_isExistRecord)
                {
                    delete();
                }

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

        public void delete()
        {
            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, "DELETE FROM BASE_SYSTEM_INFO");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        private void load()
        {
            string userQuery = "SELECT [PKEY],[COMPANY_NAME],[GROWS],[BANK_ACCOUNT],[PHONE],[FAX],[E_MAIL],[ADDRESS] FROM BASE_SYSTEM_INFO";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, userQuery))
            {
                if (dataTable.Rows.Count > 0)
                {
                    m_isExistRecord = true;
                    m_systemInfo.pkey = DbDataConvert.ToInt32(dataTable.Rows[0][0]);
                    m_systemInfo.companyName = DbDataConvert.ToString(dataTable.Rows[0][1]);
                    m_systemInfo.grows = DbDataConvert.ToString(dataTable.Rows[0][2]);
                    m_systemInfo.bankAccount = DbDataConvert.ToString(dataTable.Rows[0][3]);
                    m_systemInfo.phone = DbDataConvert.ToString(dataTable.Rows[0][4]);
                    m_systemInfo.fax = DbDataConvert.ToString(dataTable.Rows[0][5]);
                    m_systemInfo.eMail = DbDataConvert.ToString(dataTable.Rows[0][6]);
                    m_systemInfo.address = DbDataConvert.ToString(dataTable.Rows[0][7]);
                }
                else 
                {
                    m_isExistRecord = false;
                }
            }
        }

        public SystemInfoTable getSystemInfo()
        {
            return m_systemInfo;
        }
    }

    public class SystemInfoTable
    {
        public int pkey { get; set; }
        public string companyName { get; set; }
        public string grows { get; set; }
        public string bankAccount { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string eMail { get; set; }
        public string address { get; set; }
    }
}