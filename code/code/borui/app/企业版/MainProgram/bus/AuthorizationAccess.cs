using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using TIV.Core.DatabaseAccess;
using TIV.Core.TivLogger;

namespace MainProgram.bus
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class AccessAuthorization
    {
        private static AccessAuthorization m_instance = null;

        private SortedDictionary<int, LoginUserInfo> m_userList = new SortedDictionary<int, LoginUserInfo>();
        private SortedDictionary<int, int> m_currentLoginUserActionList = new SortedDictionary<int, int>();

        private AccessAuthorization()
        {
 
        }

        static public AccessAuthorization getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new AccessAuthorization();
            }

            return m_instance;
        }

        public bool resetPasswork(int userID, string newPwd)
        {
            string sql = "UPDATE BASE_STAFF_LIST SET PASSWORD = '" + newPwd + "' WHERE PKEY = " + userID;

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, sql);
                loadAllUserInfomation();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return false;
            }

            return true;
        }

        public SortedDictionary<int, LoginUserInfo> getAllLoginUser()
        {
            if (m_userList.Count <= 0)
            {
                loadAllUserInfomation();
            }

            return m_userList;
        }

        public bool passwordIsValidate(string userName, string password, out LoginUserInfo loginUserInfo)
        {
            bool isRet = false;
            loginUserInfo = null;
            string passwordToLower = password.ToLower();

            SortedDictionary<int, LoginUserInfo> list = new SortedDictionary<int, LoginUserInfo>();

            if (m_userList.Count <= 0)
            {
                loadAllUserInfomation();
            }

            foreach (KeyValuePair<int, LoginUserInfo> index in m_userList)
            {
                if (index.Value.staffName == userName)
                {
                    if (index.Value.password == passwordToLower)
                    {
                        isRet = true;
                        loginUserInfo = index.Value;
                    }

                    break;
                }
            }

            return isRet;
        }

        public bool passwordIsValidate(int userID, string password)
        {
            bool isRet = false;
            SortedDictionary<int, LoginUserInfo> list = new SortedDictionary<int, LoginUserInfo>();

            if (m_userList.Count <= 0)
            {
                loadAllUserInfomation();
            }

            foreach (KeyValuePair<int, LoginUserInfo> index in m_userList)
            {
                if (index.Value.pkey == userID)
                {
                    if (index.Value.password == password)
                    {
                        isRet = true;
                    }

                    break;
                }
            }

            return isRet;
        }

        public bool isAccessAuthorization(int actionID, string userID)
        {
            bool isRet = false;

            if (userID == "1")
            {
                // 针对Manager用户，赋予其所有权限
                return true;
            }
            else
            {
                if (m_currentLoginUserActionList.Count <= 0)
                {
                    loadUserAction(userID);
                }

                if (m_currentLoginUserActionList.ContainsKey(actionID))
                {
                    isRet = true; 
                }
            }

            return isRet;
        }

        private void loadAllUserInfomation()
        {
            string query = "SELECT PKEY, NAME, PASSWORD FROM BASE_STAFF_LIST WHERE PKEY IN ";
            query += "(SELECT VALUE FROM BASE_USER_ORG_STRUCT WHERE DEPARTMENT_OR_STAFF = 1)";

            m_userList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    LoginUserInfo userInfo = new LoginUserInfo();

                    userInfo.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    userInfo.staffName = DbDataConvert.ToString(row["NAME"]);
                    userInfo.password = DbDataConvert.ToString(row["PASSWORD"]);

                    m_userList.Add(m_userList.Count, userInfo);
                }
            }
        }

        private void loadUserAction(string userID)
        {
            string query = " SELECT ACTION_ID FROM BASE_ACTION_STAFF WHERE STAFF_ID = " + userID;

            m_currentLoginUserActionList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    LoginUserInfo userInfo = new LoginUserInfo();

                    int actionPkey = DbDataConvert.ToInt32(row["ACTION_ID"]);

                    m_currentLoginUserActionList.Add(actionPkey, actionPkey);
                }
            }
        }
    }

    public class LoginUserInfo
    {
        public int pkey { get; set; }
        public string staffName { get; set; }
        public string password { get; set; }
    }
}