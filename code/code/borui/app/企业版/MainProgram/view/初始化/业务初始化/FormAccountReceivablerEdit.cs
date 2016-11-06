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
    public partial class FormAccountReceivablerEdit : Form
    {
        private bool m_isAddToInitList = false;
        private int m_selectRecordPkey;
        userInterfaceValue m_userInterfaceValue = new userInterfaceValue();

        public FormAccountReceivablerEdit(userInterfaceValue uiValue)
        {
            InitializeComponent();
            this.Text = uiValue.winText;

            m_userInterfaceValue = uiValue;
        }

        private void FormAccountReceivablerEdit_Load(object sender, EventArgs e)
        {
            if (m_userInterfaceValue.isEditDate)
            {
                this.buttonEdit.Enabled = true;
                this.buttonSelect.Enabled = false;
                this.textBoxBalance.Enabled = false;
                this.dateTimePickerDate.Enabled = false;
                this.buttonEnter.Enabled = false;

                this.textBoxName.Text = m_userInterfaceValue.textBoxText;
                this.dateTimePickerDate.Text = m_userInterfaceValue.dateTimePickerDateText;
                this.textBoxBalance.Text = m_userInterfaceValue.textBoxBalance;
            }

            if(!m_userInterfaceValue.isAccountReceivable)
            {
                this.groupBox2.Text = "供应商信息";
                this.label5.Text = "供应商名称";
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (m_userInterfaceValue.isAccountReceivable)
            {
                FormBaseCustomer fbs = new FormBaseCustomer(true);
                fbs.ShowDialog();
                m_selectRecordPkey = fbs.getSelectRecordPkey();
                CustomerTable record = Customer.getInctance().getCustomerInfoFromPkey(m_selectRecordPkey);
                this.textBoxName.Text = Convert.ToString(record.pkey) + "-" + record.name;
            }
            else
            {
                FormBaseSupplier fbs = new FormBaseSupplier(true);
                fbs.ShowDialog();
                m_selectRecordPkey = fbs.getSelectRecordPkey();
                SupplierTable record = Supplier.getInctance().getSupplierInfoFromPkey(m_selectRecordPkey);
                this.textBoxName.Text = Convert.ToString(record.pkey) + "-" + record.name;
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxBalance.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("欠款金额不能为空");
                return;
            }

            if (textBoxBalance.Text.Length > 20)
            {
                MessageBoxExtend.messageWarning("单价最大长度不能超过10");
                textBoxBalance.Text = "";
                return;
            }

            if (dateTimePickerDate.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("日期不能");
                return;
            }

            InitAccountReceivableTable record = new InitAccountReceivableTable();

            if (m_userInterfaceValue.isEditDate)
            {
                record.customerOrSupplierID = m_userInterfaceValue.customerOrSupplierID;
            }
            else
            {
                record.customerOrSupplierID = m_selectRecordPkey;
            }

            record.balance = Convert.ToDouble(this.textBoxBalance.Text.ToString());
            record.tradingDate = this.dateTimePickerDate.Value.ToString("yyyyMMdd");

            if (m_userInterfaceValue.isEditDate)
            {
                if (m_userInterfaceValue.isAccountReceivable)
                {
                    InitAccountReceivable.getInctance().update(m_userInterfaceValue.pkey, record);
                }
                else
                {
                    InitAccountPayable.getInctance().update(m_userInterfaceValue.pkey, record);
                }
            }
            else
            {
                if (m_userInterfaceValue.isAccountReceivable)
                {
                    InitAccountReceivable.getInctance().insert(record);
                }
                else 
                {
                    InitAccountPayable.getInctance().insert(record);
                }
            }

            m_isAddToInitList = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            m_isAddToInitList = false;
            this.Close();
        }

        public int getSelectRecordPkey()
        {
            return m_selectRecordPkey;
        }

        public bool isAddToInitMaterielList()
        {
            return m_isAddToInitList;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.buttonEdit.Enabled = false;
            this.textBoxBalance.Enabled = true;
            this.dateTimePickerDate.Enabled = true;
            this.buttonEnter.Enabled = true;
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 单价只能只能输入数字或小数点或退格键
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '.' && this.textBoxBalance.Text.IndexOf(".") == -1)
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }
    }

    public class userInterfaceValue
    {
        public string winText;
        public int pkey;
        public bool isAccountReceivable;
        public bool isEditDate;
        public string textBoxText;
        public string textBoxBalance;
        public string dateTimePickerDateText;
        public int customerOrSupplierID;
    }
}
