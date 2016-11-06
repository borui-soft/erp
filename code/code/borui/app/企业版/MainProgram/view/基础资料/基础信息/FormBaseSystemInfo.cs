using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormBaseSystemInfo : Form
    {
        public FormBaseSystemInfo()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(this.tabControl1.SelectedIndex == 0)
            {
                SystemInfoTable systemInfo = new SystemInfoTable();

                systemInfo.companyName = this.textBoxCompanyName.Text;
                systemInfo.grows = this.textBoxGorws.Text;
                systemInfo.bankAccount = this.textBoxBankAccount.Text;
                systemInfo.phone = this.textBoxPhone.Text;
                systemInfo.fax = this.textBoxFax.Text;
                systemInfo.eMail = this.textBoxEmail.Text;
                systemInfo.address = this.textBoxAddress.Text;

                // 当信息中的任何一项不为空时，保存
                if (systemInfo.companyName.Length > 0 ||
                    systemInfo.grows.Length > 0 ||
                    systemInfo.bankAccount.Length > 0 ||
                    systemInfo.phone.Length > 0 ||
                    systemInfo.fax.Length > 0 ||
                    systemInfo.eMail.Length > 0 ||
                    systemInfo.address.Length > 0)
                {
                    SystemInfo.getInctance().insert(systemInfo);
                }
            }
            else
            {
                // 业务参数页面信息
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(701);

            foreach (KeyValuePair<int, ActionTable> index in list)
            {
                object activeObject = this.GetType().GetField(index.Value.uiActionName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                bool isEnable = AccessAuthorization.getInctance().isAccessAuthorization(index.Value.pkey,
                    Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));

                if (activeObject != null)
                {
                    UserInterfaceActonState.setUserInterfaceActonState(activeObject,
                        ((System.Reflection.MemberInfo)(activeObject.GetType())).Name.ToString(), isEnable);
                }
            }
        }

        private void FormBaseSystemInfo_Load(object sender, EventArgs e)
        {
            // 读取数据库数据，如果数据库存在数据，则显示到页面上
            readSystemInfoToUI();
            setPageActionEnable();
        }

        private void readSystemInfoToUI()
        {
            SystemInfoTable systemInfo = new SystemInfoTable();

            systemInfo = SystemInfo.getInctance().getSystemInfo();

            this.textBoxCompanyName.Text = systemInfo.companyName;
            this.textBoxGorws.Text = systemInfo.grows;
            this.textBoxBankAccount.Text = systemInfo.bankAccount;
            this.textBoxPhone.Text = systemInfo.phone;
            this.textBoxFax.Text = systemInfo.fax;
            this.textBoxEmail.Text = systemInfo.eMail;
            this.textBoxAddress.Text = systemInfo.address;
        }
    }
}