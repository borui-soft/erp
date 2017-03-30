using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MainProgram.bus;

namespace MainProgram
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 1 && args[0].CompareTo("DEBUG") == 0)
            {
                LoginUserInfo m_currentLoginUser = new LoginUserInfo();

                if (AccessAuthorization.getInctance().passwordIsValidate("Manager", "Manager", out m_currentLoginUser))
                {
                    DbPublic.getInctance().setCurrentLoginUserName(m_currentLoginUser.staffName);
                    DbPublic.getInctance().setCurrentLoginUserID(m_currentLoginUser.pkey);

                    //Application.Run(new FormProjectInfoTrack(FormProjectInfoTrack.OrderType.ALL));
                    //Application.Run(new FormTransfer());
                    //Application.Run(new FormProjectMaterielChangeOrder(1, "123"));
                    Application.Run(new FormProductionReport());
                    //Application.Run(new FormMain());

                    return;
                }
            }

            FormLogin login = new FormLogin();
            login.ShowDialog();

            if (login.loginSuccessful())
            {
                Application.Run(new FormMain());
            }
        }
    }
}