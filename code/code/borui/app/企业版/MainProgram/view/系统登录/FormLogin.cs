using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TIV.Core.DatabaseAccess;
using TIV.Core.TivLogger;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public partial class FormLogin : Form
    {
        private bool m_loginSuccessful = false;
        private LoginUserInfo m_currentLoginUser = new LoginUserInfo();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // 初始化用户名下拉列表框
            SortedDictionary<int, LoginUserInfo> userList = AccessAuthorization.getInctance().getAllLoginUser();

            // 需要把Manager用户设置成默认用户
            this.comboBoxUserName.Items.Add("Manager");

            foreach (KeyValuePair<int, LoginUserInfo> index in userList)
            {
                if (index.Value.staffName != "Manager")
                {
                    this.comboBoxUserName.Items.Add(index.Value.staffName);
                }
            }

            this.comboBoxUserName.SelectedIndex = 0;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (AccessAuthorization.getInctance().passwordIsValidate(this.comboBoxUserName.Text.ToString(),
                this.textBoxPassword.Text.ToString(), out m_currentLoginUser))
            {
                m_loginSuccessful = true;
                DbPublic.getInctance().setCurrentLoginUserName(m_currentLoginUser.staffName);
                DbPublic.getInctance().setCurrentLoginUserID(m_currentLoginUser.pkey);

                // 更新BASE_SYSTEM_CURRENT_LOGIN_USER表
                CurrentLoginUser.getInctance().insert();
                this.Close();
            }
            else 
            {
                MessageBoxExtend.messageWarning("密码错误，请重新输入！");
                this.textBoxPassword.Text = "";
                this.textBoxPassword.Focus();
            }
        }

        public bool loginSuccessful()
        {
            return m_loginSuccessful;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}