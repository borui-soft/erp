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
    public partial class FormNewFilterValue : Form
    {
        private int m_currentFilterValue = 10;
        public FormNewFilterValue(int currentFilterValue)
        {
            InitializeComponent();
            m_currentFilterValue = currentFilterValue;
        }

        private void enter_Click(object sender, EventArgs e)
        {
            m_currentFilterValue = Convert.ToInt32(this.textBoxValue.Text.ToString());
            this.Close();
        }

        private void FormNewFilterValue_Load(object sender, EventArgs e)
        {
            this.textBoxValue.Text = Convert.ToString(m_currentFilterValue);
        }

        private void FormNewFilterValue_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public int getNewFilterValue()
        {
            return m_currentFilterValue;
        }

        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 单价只能只能输入数字或小数点或退格键
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }
    }
}