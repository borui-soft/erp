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
    public class Staff : ITableModel
    {
        private SortedDictionary<int, StaffTable> m_noForbidStaffList = new SortedDictionary<int, StaffTable>();
        private SortedDictionary<int, StaffTable> m_forbidStaffList = new SortedDictionary<int, StaffTable>();

        static private Staff m_instance = null;

        private Staff()
        {
            load();
        }

        static public Staff getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new Staff();
            }

            return m_instance;
        }

        public void insert(StaffTable user)
        {
            string insert = "INSERT INTO [dbo].[BASE_STAFF_LIST]([NUMBER],[NAME],[SEX]";
            insert += ",[EDU_BACKGROUND],[NO],[TEL],[ADDRESS],[E_MAIL],[ENTRY_DATE],[PROFILE_ID]";
            insert += ",[DEPARTMENT],[STAFF_TYPE],[REMARKS],[IS_FORBID]) VALUES (";

            insert += "'" + user.number + "',";
            insert += "'" + user.name + "',";
            insert += "'" + user.sex + "',";
            insert += "'" + user.eduBackground + "',";
            insert += "'" + user.NO + "',";
            insert += "'" + user.tel + "',";
            insert += "'" + user.address + "',";
            insert += "'" + user.email + "',";
            insert += "'" + user.enterDate+ "',";
            insert += user.prifileID + ",";
            insert += user.departmentID + ",";
            insert += user.staffType + ",";
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
            if (deleteRecord("BASE_STAFF_LIST", pkey))
            {
                load();
            }
        }
        
        public void updateStaffPassword(int pkey, string password)
        {
            string update = "UPDATE [dbo].[BASE_STAFF_LIST] SET PASSWORD = '" + password + "'";
            update += " WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void update(int pkey, StaffTable user)
        {
            string update = "UPDATE [dbo].[BASE_STAFF_LIST] SET ";

            update += "[NUMBER] = '" + user.number + "',";
            update += "[NAME] = '" + user.name + "',";
            update += "[SEX] = '" + user.sex + "',";
            update += "[EDU_BACKGROUND] = '" + user.eduBackground + "',";
            update += "[NO] = '" + user.NO + "',";
            update += "[TEL] = '" + user.tel + "',";
            update += "[ADDRESS] = '" + user.address + "',";
            update += "[E_MAIL] = '" + user.email + "',";
            update += "[ENTRY_DATE] = '" + user.enterDate + "',";
            update += "[PROFILE_ID] = " + user.prifileID + ",";

            // 员工隶属哪个部门，不得修改
            //update += "[DEPARTMENT] = " + user.departmentID + ",";

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
            userQuery += ",[ENTRY_DATE],[PROFILE_ID],[DEPARTMENT],[STAFF_TYPE],[IS_FORBID],[REMARKS] FROM [dbo].[BASE_STAFF_LIST]";

            m_noForbidStaffList.Clear();
            m_forbidStaffList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, userQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    StaffTable user = new StaffTable();

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
                        m_forbidStaffList.Add(m_forbidStaffList.Count, user);
                    }
                    else
                    {
                        m_noForbidStaffList.Add(m_noForbidStaffList.Count, user);
                    }
                }
            }
        }

        public SortedDictionary<int, StaffTable> getAllStaffInfo()
        {
            if (m_noForbidStaffList.Count == 0)
            {
                load();
            }

            return m_noForbidStaffList;
        }

        public SortedDictionary<int, StaffTable> getAllForbidStaffInfo()
        {
            if (m_forbidStaffList.Count == 0)
            {
                load();
            }

            return m_forbidStaffList;
        }

        public StaffTable getStaffInfoFromPkey(int pkey)
        {
            if (m_noForbidStaffList.Count == 0)
            {
                load();
            }

            StaffTable staff = new StaffTable();

            foreach (KeyValuePair<int, StaffTable> index in m_noForbidStaffList)
            {
                StaffTable record = new StaffTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    staff = record;
                }
            }

            return staff;
        }

        public string getStaffNameFromPkey(int pkey)
        {
            string userName = "";

            if (m_noForbidStaffList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, StaffTable> index in m_noForbidStaffList)
            {
                StaffTable record = new StaffTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    userName = record.name;
                    break;
                }
            }

            return userName;
        }

        public int getStaffPkeyFromName(string staffName)
        {
            int pkey = 0;

            if (m_noForbidStaffList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, StaffTable> index in m_noForbidStaffList)
            {
                if (index.Value.name == staffName)
                {
                    pkey = index.Value.pkey;
                    break;
                }
            }

            return pkey;
        }

        public bool isExistStaffName(string staffName)
        {
            bool isRet = false;

            if (m_noForbidStaffList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, StaffTable> index in m_noForbidStaffList)
            {
                if (index.Value.name == staffName)
                {
                    isRet = true;
                    break;
                }
            }
            return isRet;
        }

        public SortedDictionary<int, StaffTable> getStaffInfoFromDepartmentPkey(int departmentPkey)
        {
            if (m_noForbidStaffList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, StaffTable> staffList = new SortedDictionary<int, StaffTable>();

            foreach (KeyValuePair<int, StaffTable> index in m_noForbidStaffList)
            {
                StaffTable staff = new StaffTable();
                staff = index.Value;

                if (staff.departmentID == departmentPkey)
                {
                    staffList.Add(staffList.Count, staff);
                }
            }

            return staffList;
        }

        public void forbidStaff(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_STAFF_LIST] SET [IS_FORBID] = 1 WHERE PKEY = ";
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

        public void noForbidStaff(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_STAFF_LIST] SET [IS_FORBID] = 0 WHERE PKEY = ";
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

        public bool isOnline(int pkey)
        {
            return CurrentLoginUser.getInctance().userIsOnline(pkey);
        }
    }

    public class StaffTable
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