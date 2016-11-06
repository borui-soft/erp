using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormCreateUser : Form
    {
        private bool m_isAddStaff = false;
        private int m_staffPkey = 0;
        public FormCreateUser()
        {
            InitializeComponent();
        }

        private void buttonSelectUser_Click(object sender, EventArgs e)
        {
            FormBaseStaff fbs = new FormBaseStaff(true);
            fbs.ShowDialog();
            m_staffPkey = fbs.getSelectRecordPkey();
            this.textBoxStaffName.Text = Staff.getInctance().getStaffNameFromPkey(m_staffPkey);
            m_isAddStaff = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            m_isAddStaff = false;
            this.Close();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if(textBoxPassword1.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("密码不能为空");
                return;
            }

            if (textBoxPassword1.Text.Length > 10)
            {
                MessageBoxExtend.messageWarning("密码最大长度不能超过10");
                textBoxPassword1.Text = "";
                return;
            }

            if (textBoxPassword1.Text != textBoxPassword2.Text)
            {
                MessageBoxExtend.messageWarning("两次输入的密码不同，请重新输入");
                textBoxPassword1.Text = "";
                textBoxPassword2.Text = "";
                return;
            }

            Staff.getInctance().updateStaffPassword(m_staffPkey, textBoxPassword1.Text.ToLower());

            this.Close();
        }

        public int getSelectRecordPkey()
        {
            return m_staffPkey;
        }

        public bool isAddStaff()
        {
            return m_isAddStaff;
        }
    }
}
