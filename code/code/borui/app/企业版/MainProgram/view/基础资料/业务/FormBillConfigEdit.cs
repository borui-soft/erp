using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormBillConfigEdit : Form
    {
        private BillConfigTable m_billConfig = new BillConfigTable();
        
        public FormBillConfigEdit(string winText, int pkey)
        {
            InitializeComponent();
            this.Text = winText;
            m_billConfig = BillConfig.getInctance().getBillConfigInfoFromPeky(pkey);
        }

        private void FormBillConfigEdit_Load(object sender, EventArgs e)
        {
            if (m_billConfig.isInput == 1)
            {
                this.checkBoxIsInput.Checked = true;
            }

            if (m_billConfig.isAutoSave == 1)
            {
                this.checkBoxIsAutoSave.Checked = true;
            }

            if (m_billConfig.isUseRules == 1)
            {
                this.checkBoxIsUse.Checked = true;
            }

            if (m_billConfig.isUseSysdate == 1)
            {
                this.checkBoxUseSysdate.Checked = true;
            }

            this.numericUpDownNum.Text = Convert.ToString(m_billConfig.num);
            this.textBoxFront.Text = m_billConfig.front;
            this.textBoxCode.Text = m_billConfig.code;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            BillConfigTable billConfig = new BillConfigTable();

            int inputChecked = 0;
            int autoSaveChecked = 0;
            int useChecked = 0;
            int useSysdateeChecked = 0;

            if (checkBoxIsInput.Checked)
            {
                inputChecked = 1;
            }
            if (checkBoxIsUse.Checked)
            {
                useChecked = 1;
            }
            if (checkBoxIsAutoSave.Checked)
            {
                autoSaveChecked = 1;
            }
            if (checkBoxUseSysdate.Checked)
            {
                useSysdateeChecked = 1;
            }

            billConfig.pkey = m_billConfig.pkey;

            billConfig.isAutoSave = autoSaveChecked;
            billConfig.isUseSysdate = useSysdateeChecked;
            billConfig.isUseRules = useChecked;
            billConfig.isInput = inputChecked;

            billConfig.code = this.textBoxCode.Text;
            billConfig.front = this.textBoxFront.Text;
            billConfig.num = Convert.ToInt32(this.numericUpDownNum.Text.ToString());

            BillConfig.getInctance().update(billConfig.pkey, billConfig);

            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxFront_TextChanged(object sender, EventArgs e)
        {
            setTextBoxCodeText();
        }

        private void checkBoxUseSysdate_Click(object sender, EventArgs e)
        {
            setTextBoxCodeText();
            setButtonSaveEnable();
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            setButtonSaveEnable();
        }

        private bool getCheckBoxStateIsChange()
        {
            bool isRet = false;

            int inputChecked = 0;
            int autoSaveChecked = 0;
            int useChecked = 0;
            int useSysdateeChecked = 0;

            if(checkBoxIsInput.Checked)
            {
                inputChecked = 1;
            }
            if (checkBoxIsUse.Checked)
            {
                useChecked = 1;
            }
            if (checkBoxIsAutoSave.Checked)
            {
                autoSaveChecked = 1;
            }
            if (checkBoxUseSysdate.Checked)
            {
                useSysdateeChecked = 1;
            }

            if (inputChecked != m_billConfig.isInput ||
                useChecked != m_billConfig.isUseRules ||
                autoSaveChecked != m_billConfig.isAutoSave ||
                useSysdateeChecked != m_billConfig.isUseSysdate)
            {
                isRet = true;
            }

            return isRet;
        }

        private void setTextBoxCodeText()
        {
            string str = "";

            str += this.textBoxFront.Text + "+";

            if(checkBoxUseSysdate.Checked)
            {
                str += "SYSDATE+";
            }

            for (int i = 0; i < Convert.ToInt32(this.numericUpDownNum.Text.ToString()) - 1; i++)
            {
                str += "0";
            }
            str += "1";

            this.textBoxCode.Text = str;
        }

        private void setButtonSaveEnable()
        {
            if (this.textBoxCode.Text != m_billConfig.code || getCheckBoxStateIsChange())
            {
                this.buttonSave.Enabled = true;
            }
            else
            {
                this.buttonSave.Enabled = false;
            }
        }

        private void numericUpDownNum_ValueChanged(object sender, EventArgs e)
        {
            setTextBoxCodeText();
        }

        private void numericUpDownNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            setTextBoxCodeText();
        }

        private void numericUpDownNum_MouseUp(object sender, MouseEventArgs e)
        {
            setTextBoxCodeText();
        }
    }
}
