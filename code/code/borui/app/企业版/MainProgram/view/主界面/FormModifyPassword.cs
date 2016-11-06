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
using TIV.Core.TivLogger;

namespace MainProgram
{
    public partial class FormModifyPassword : Form
    {
        public FormModifyPassword()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxNewPassword1.Text.Length == 0 || textBoxNewPassword2.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("新密码不能为空");
                return;
            }

            if (textBoxNewPassword1.Text != textBoxNewPassword2.Text)
            {
                MessageBoxExtend.messageWarning("两次输入的新密码不相同，请重新输入");
                textBoxNewPassword1.Text = "";
                textBoxNewPassword2.Text = "";
                return;
            }

            string newPassword1 = textBoxNewPassword1.Text;
            string newPassword2 = textBoxNewPassword2.Text;

            if (MessageBoxExtend.messageQuestion("确认要修改密码吗?"))
            {
                Staff.getInctance().updateStaffPassword(DbPublic.getInctance().getCurrentLoginUserID(), newPassword2);
                MessageBoxExtend.messageOK("密码修改成功，重新登录时，请使用新密码");
            }

            this.Close();
        }

        private void textBoxTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
        }
    }
}
