using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainProgram
{
    public partial class FormAdjustAmount : Form
    {
        private string m_lableMessage = "";
        private string m_amountText = "";
        private double m_amount = 0;
        public FormAdjustAmount(string winText, string lableMessage, string amount)
        {
            InitializeComponent();

            this.Text = winText;

            m_lableMessage = lableMessage;

            if (winText.IndexOf("应收") != -1)
            {
                this.labelAmount.Text = "应付账款金额";
            }
            else
            {
                this.labelAmount.Text = "应收账款金额";
            }

            m_amountText = amount;
        }

        private void FormAdjustAmount_Load(object sender, EventArgs e)
        {
            this.textBox1.Enabled = false;
            this.textBoxMessage.Text = m_lableMessage;
            this.textBox1.Text = m_amountText;
        }

        private void buttonAdjustAmount_Click(object sender, EventArgs e)
        {
            this.textBox1.Enabled = true;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_amount = Convert.ToDouble(this.textBox1.Text.ToString());
            this.Close();
        }

        public double getAmount()
        {
            return m_amount;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 单价只能只能输入数字或小数点或退格键
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '.' && this.textBox1.Text.IndexOf(".") == -1)
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        private void FormAdjustAmount_FormClosed(object sender, FormClosedEventArgs e)
        {
            buttonEnter_Click(sender, null);
        }
    }
}
