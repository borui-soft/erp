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
    public class User : ITableModel
    {
        private SortedDictionary<int, UserTable> m_noForbidUserList = new SortedDictionary<int, UserTable>();
        private SortedDictionary<int, UserTable> m_forbidUserList = new SortedDictionary<int, UserTable>();

        static private User m_instance = null;

        private User()
        {
            load();
        }

        static public User getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new User();
            }

            return m_instance;
        }

        public void insert(UserTable user)
        {
            string insert = "INSERT INTO [ERP].[dbo].[BASE_STAFF_LIST]([NUMBER],[NAME],[SEX]";
            insert += ",[EDU_BACKGROUND],[NO],[TEL],[ADDRESS],[E_MAIL],[ENTRY_DATE],[PROFILE_ID]";
            insert += ",[DEPARTMENT],[STAFF_TYPE],[REMARKS],[STATE]) VALUES (";

            insert += "'" + user.number + "',";
            insert += "'" + user.name + "',";
            insert += "'" + user.sex + "',";
            insert += "'" + user.eduBackground + "',";
            insert += "'" + user.NO + "',";
            insert += "'" + user.tel + "',";
            insert += "'" + user.address + "',";
            insert += "'" + user.email + "',";
            insert += "'" + user.enterDate+ "',";
            insert += user.prifileID + "',";
            insert += user.departmentID + "',";
            insert += user.staffType + "',";
            insert += "'" + user.remarks + "', 0";
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
            if (deleteRecord("BASE_SUPPLIER_LIST", pkey))
            {
                load();
            }
        }

        public void update(int pkey, UserTable user)
        {
            string update = "UPDATE [ERP].[dbo].[BASE_SUPPLIER_LIST] SET ";

            update += "[NAME] = " + user.name + ",";
            update += "[SEX] = '" + user.sex + "',";
            update += "[EDU_BACKGROUND] = '" + user.eduBackground + "',";
            update += "[NO] = '" + user.NO + "',";
            update += "[TEL] = '" + user.tel + "',";
            update += "[ADDRESS] = " + user.address + ",";
            update += "[E_MAIL] = " + user.email + ",";
            update += "[ENTRY_DATE] = '" + user.enterDate + "',";
            update += "[PROFILE_ID] = " + user.prifileID + ",";
            update += "[DEPARTMENT] = " + user.departmentID + ",";
            update += "[STAFF_TYPE] = " + user.staffType + ",";
            update += "[REMARKS] = '" + user.remarks + "'";
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
            string userQuery = "SELECT [PKEY],[NUMBER],[NAME],[SEX],[EDU_BACKGROUND],[NO],[TEL],[ADDRESS],[E_MAIL]";
            userQuery += ",[ENTRY_DATE],[PROFILE_ID],[DEPARTMENT],[STAFF_TYPE],[STATE],[REMARKS] FROM [ERP].[dbo].[BASE_STAFF_LIST]";

            m_noForbidUserList.Clear();
            m_forbidUserList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, userQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    UserTable user = new UserTable();

                    user.pkey = DbDataConvert.ToInt32(row[0]);
                    user.number = DbDataConvert.ToString(row[1]);
                    user.name = DbDataConvert.ToString(row[2]);
                    user.sex = DbDataConvert.ToString(row[3]);
                    user.eduBackground = DbDataConvert.ToString(row[4]);
                    user.NO = DbDataConvert.ToString(row[5]);
                    user.tel = DbDataConvert.ToString(row[6]);
                    user.address = DbDataConvert.ToString(row[7]);
                    user.email = DbDataConvert.ToString(row[8]);
                    user.enterDate = DbDataConvert.ToString(row[9]);
                    user.prifileID = ConvertExtend.toInt32(row[10].ToString());
                    user.departmentID = ConvertExtend.toInt32(row[11].ToString());
                    user.staffType = ConvertExtend.toInt32(row[12].ToString());
                    user.state = ConvertExtend.toInt32(row[13].ToString());
                    user.remarks = DbDataConvert.ToString(row[14]);

                    if (user.state == 1)
                    {
                        m_forbidUserList.Add(m_forbidUserList.Count, user);
                    }
                    else
                    {
                        m_noForbidUserList.Add(m_noForbidUserList.Count, user);
                    }
                }
            }
        }

        public SortedDictionary<int, UserTable> getAllUserInfo()
        {
            if (m_noForbidUserList.Count == 0)
            {
                load();
            }

            return m_noForbidUserList;
        }

        public SortedDictionary<int, UserTable> getAllForbidUserInfo()
        {
            if (m_noForbidUserList.Count == 0)
            {
                load();
            }

            return m_forbidUserList;
        }

        public UserTable getUserInfoFromPkey(int pkey)
        {
            if (m_noForbidUserList.Count == 0)
            {
                load();
            }

            UserTable supplier = new UserTable();

            foreach (KeyValuePair<int, UserTable> index in m_noForbidUserList)
            {
                UserTable record = new UserTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    supplier = record;
                }
            }

            return supplier;
        }

        public void forbidUser(int pkey)
        {
            string update = "UPDATE [ERP].[dbo].[BASE_SUPPLIER_LIST] SET [IS_FORBID] = 1 WHERE PKEY = ";
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

        public void noForbidUser(int pkey)
        {
            string update = "UPDATE [ERP].[dbo].[BASE_SUPPLIER_LIST] SET [IS_FORBID] = 0 WHERE PKEY = ";
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

        public string getUserNameFromPkey(int pkey)
        {
            string userName = "";

            if (m_noForbidUserList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, UserTable> index in m_noForbidUserList)
            {
                UserTable record = new UserTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    userName = record.name;
                    break;
                }
            }

            return userName;
        }

        public bool isOnline(int pkey)
        {
            return CurrentLoginUser.getInctance().userIsOnline(pkey);
        }
    }

    public class UserTable
    {
        public int pkey { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string eduBackground { get; set; }
        public string NO { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string enterDate { get; set; }
        public int prifileID { get; set; }
        public int departmentID { get; set; }
        public int staffType { get; set; }
        public int state { get; set; }
        public string remarks { get; set; }
    }
}