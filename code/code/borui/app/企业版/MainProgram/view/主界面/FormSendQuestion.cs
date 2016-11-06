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
using System.Net;
using System.Net.Mail;

namespace MainProgram
{
    public partial class FormSendQuestion : Form
    {
        public FormSendQuestion()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (this.textBoxName.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("企业名称不能为空.");
                return;
            }

            if (this.textBoxPeople.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("联系人不能为空.");
                return;
            }

            if (this.textBoxPhone.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("联系电话不能为空.");
                return;
            }

            if (this.textBoxContent.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("问题描述不能为空.");
                return;
            }

            string emailTitle = this.textBoxName.Text + "#" + this.textBoxPeople.Text + "#" + this.textBoxPhone.Text;
            string emailContent = this.textBoxContent.Text;
            try
            {
                SMTP.sendEmail(emailTitle, emailContent);
                MessageBoxExtend.messageOK("信息反馈成功");
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError("信息发送失败!\n\n请确认电脑是否已连接到Internet,然后重新.\n如果依然无法发送请联系软件供应商.\n" + exp.ToString());
            }
        }
    }
}
