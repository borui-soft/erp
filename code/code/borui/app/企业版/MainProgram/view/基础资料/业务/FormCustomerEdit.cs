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
    public partial class FormCustomerEdit : Form
    {
        private int m_customerGroupPkey = 1;
        private bool m_isCustomerGroup = false;

        private CustomerTable m_customer;
        private CustomerTypeTable m_customerType;

        private bool m_isAdd = true;
        private bool m_isEditCustomer = true;
        private int m_customerPkey = 0;
        
        public FormCustomerEdit(bool isAdd, int customerGroupPkey, bool isEditCustomer = true, int pkey = 0)
        {
            m_isAdd = isAdd;
            m_customerGroupPkey = customerGroupPkey;
            m_isEditCustomer = isEditCustomer;
            m_customerPkey = pkey;

            InitializeComponent();
        }

        private void FormCustomerEdit_Load(object sender, EventArgs e)
        {
            this.textBoxCredit.Text = "0";
            this.textBoxVatRate.Text = "17";

            if (!m_isAdd)
            {
                this.buttonAdd.Enabled = false;
                this.buttonCustomerGroup.Enabled = false;

                if (m_isEditCustomer)
                {
                    m_customer = Customer.getInctance().getCustomerInfoFromPkey(m_customerPkey);
                }
                else
                {
                    m_customerType = CustomerType.getInctance().getCustomerTypeInfoFromPkey(m_customerGroupPkey);
                    m_isCustomerGroup = true;
                }

                setPageActiveState();
                setPageActiveValue();
            }

            // 初始化区域下拉框值
            ComboBoxExtend.initComboBox(this.comboBoxArea, "BASE_AREA_LIST");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(m_isAdd)
            {
                if (!m_isCustomerGroup)
                {
                    addCustomer();
                }
                else
                {
                    addCustomerType();
                }
            }
            else
            {
                if (!m_isCustomerGroup)
                {
                    modifyCustomer();
                }
                else
                {
                    modifyCustomerType();
                }
            }

            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCustomerGroup_Click(object sender, EventArgs e)
        {
			if(m_isCustomerGroup)
			{
				m_isCustomerGroup = false;
			}
			else
			{
				m_isCustomerGroup = true;
			}

            setPageActiveState();
        }

        private void setPageActiveState()
        {
            if (m_isCustomerGroup)
            {
                buttonCustomerGroup.BackColor = System.Drawing.Color.LightGray;
                tabControl1.Visible = false;
                groupBoxCustomerGroup.Show();
            }
            else
            {
                buttonCustomerGroup.BackColor = System.Drawing.Color.Transparent;
                tabControl1.Visible = true;
                groupBoxCustomerGroup.Hide();
            }
        }

        private void setPageActiveValue()
        {
            if (m_isCustomerGroup)
            {
                this.textBoxCustomerGroupName.Text = m_customerType.name;
                this.textBoxCustomerGroupDesc.Text = m_customerType.desc;
            }
            else
            {
                // 基本信息-基础信息
                this.textBoxName.Text = m_customer.name;
                this.textBoxShortName.Text = m_customer.nameShort;
                this.textBoxMnemonicCode.Text = m_customer.mnemonicCode;
                this.textBoxCredit.Text = Convert.ToString(m_customer.credit);
                this.textBoxVatRate.Text = Convert.ToString(m_customer.varRate);

                // 基础信息-银行信息
                this.textBoxBankName.Text = m_customer.bankName;
                this.textBoxBankAccount.Text = m_customer.bankAccount;
                this.textBoxTaxAccount.Text = m_customer.taxAccount;

                // 联系方式
                this.comboBoxArea.Text = m_customer.area;
                this.textBoxContact.Text = m_customer.contact;
                this.textBoxTel.Text = m_customer.tel;
                this.textBoxFax.Text = m_customer.fax;
                this.textBoxHomePage.Text = m_customer.homePage;
                this.textBoxEmail.Text = m_customer.email;
                this.textBoxAddress.Text = m_customer.address;
                this.textBoxZipCode.Text = m_customer.zipCode;
                this.textBoxNote.Text = m_customer.note;
            }
        }

        private void addCustomer()
        {
            CustomerTable customer = new CustomerTable();

            // 基本信息-基础信息
            customer.customerType = m_customerGroupPkey;
            customer.name = this.textBoxName.Text.ToString();
            customer.nameShort = this.textBoxShortName.Text.ToString();
            customer.mnemonicCode = this.textBoxMnemonicCode.Text.ToString();
            if (this.textBoxCredit.Text.ToString().Length == 0)
            {
                customer.credit = 0;
            }
            else
            {
                customer.credit = Convert.ToInt32(this.textBoxCredit.Text.ToString());
            }

            if (this.textBoxVatRate.Text.ToString().Length == 0)
            {
                customer.varRate = Convert.ToInt32(this.textBoxVatRate.Text.ToString());
            }
            else
            {
                // 增值税税率默认为17%
                customer.varRate = 17;
            }

            // 基础信息-银行信息
            customer.bankName = this.textBoxBankName.Text.ToString();
            customer.bankAccount = this.textBoxBankAccount.Text.ToString();
            customer.taxAccount = this.textBoxTaxAccount.Text.ToString();

            // 联系方式
            customer.area = this.comboBoxArea.Text.ToString();
            customer.contact = this.textBoxContact.Text.ToString();
            customer.tel = this.textBoxTel.Text.ToString();
            customer.fax = this.textBoxFax.Text.ToString();
            customer.homePage = this.textBoxHomePage.Text.ToString();
            customer.email = this.textBoxEmail.Text.ToString();
            customer.address = this.textBoxAddress.Text.ToString();
            customer.zipCode = this.textBoxZipCode.Text.ToString();
            customer.note = this.textBoxNote.Text.ToString();

            if (customer.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("公司名称不能为空，请重新填写!");
                return;
            }

            Customer.getInctance().insert(customer);
        }

        private void addCustomerType()
        {
            CustomerTypeTable customerType = new CustomerTypeTable();
            customerType.name = this.textBoxCustomerGroupName.Text.ToString();
            customerType.desc = this.textBoxCustomerGroupDesc.Text.ToString();

            if (customerType.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("组名称不能为空，请重新填写!");
                return;
            }

            CustomerType.getInctance().insert(customerType);


            // 客户组织结构
            CustomerOrgStructTable customerOrgInfo = new CustomerOrgStructTable();

            customerOrgInfo.parentPkey = CustomerOrgStruct.getInctance().getPkeyFromValue(m_customerGroupPkey);
            customerOrgInfo.value = CustomerType.getInctance().getMaxPkey();
            CustomerOrgStruct.getInctance().insert(customerOrgInfo);
        }

        private void modifyCustomer()
        {
            CustomerTable customer = new CustomerTable();

            // 基本信息-基础信息
            customer.customerType = m_customerGroupPkey;
            customer.name = this.textBoxName.Text.ToString();
            customer.nameShort = this.textBoxShortName.Text.ToString();
            customer.mnemonicCode = this.textBoxMnemonicCode.Text.ToString();
            if (this.textBoxCredit.Text.ToString().Length == 0)
            {
                customer.credit = 0;
            }
            else
            {
                customer.credit = Convert.ToInt32(this.textBoxCredit.Text.ToString());
            }

            if (this.textBoxVatRate.Text.ToString().Length == 0)
            {
                customer.varRate = Convert.ToInt32(this.textBoxVatRate.Text.ToString());
            }
            else
            {
                customer.varRate = 17;
            }

            // 基础信息-银行信息
            customer.bankName = this.textBoxBankName.Text.ToString();
            customer.bankAccount = this.textBoxBankAccount.Text.ToString();
            customer.taxAccount = this.textBoxTaxAccount.Text.ToString();

            // 联系方式
            customer.area = this.comboBoxArea.Text.ToString();
            customer.contact = this.textBoxContact.Text.ToString();
            customer.tel = this.textBoxTel.Text.ToString();
            customer.fax = this.textBoxFax.Text.ToString();
            customer.homePage = this.textBoxHomePage.Text.ToString();
            customer.email = this.textBoxEmail.Text.ToString();
            customer.address = this.textBoxAddress.Text.ToString();
            customer.zipCode = this.textBoxZipCode.Text.ToString();
            customer.note = this.textBoxNote.Text.ToString();

            if (customer.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("公司名称不能为空，请重新填写!");
                return;
            }

            Customer.getInctance().update(m_customerPkey, customer);
        }

        private void modifyCustomerType()
        {
            CustomerTypeTable customerType = new CustomerTypeTable();
            customerType.name = this.textBoxCustomerGroupName.Text.ToString();
            customerType.desc = this.textBoxCustomerGroupDesc.Text.ToString();

            if (customerType.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("组名称不能为空，请重新填写!");
                return;
            }

            CustomerType.getInctance().update(m_customerGroupPkey, customerType);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!m_isAdd)
            {
                if (!m_isCustomerGroup)
                {
                    if (m_customer != null)
                    {
                        if (m_customer.name.CompareTo(this.textBoxName.Text) != 0 ||
                            m_customer.nameShort.CompareTo(this.textBoxShortName.Text) != 0 ||
                            m_customer.mnemonicCode.CompareTo(this.textBoxMnemonicCode.Text) != 0 ||
                            m_customer.credit != Convert.ToInt32(this.textBoxCredit.Text.ToString()) ||
                            m_customer.varRate != Convert.ToInt32(this.textBoxVatRate.Text.ToString()) ||
                            m_customer.bankName.CompareTo(this.textBoxBankName.Text) != 0 ||
                            m_customer.bankAccount.CompareTo(this.textBoxBankAccount.Text) != 0 ||
                            m_customer.taxAccount.CompareTo(this.textBoxTaxAccount.Text) != 0 ||
                            m_customer.area.CompareTo(this.comboBoxArea.Text) != 0 ||
                            m_customer.contact.CompareTo(this.textBoxContact.Text) != 0 ||
                            m_customer.tel.CompareTo(this.textBoxTel.Text) != 0 ||
                            m_customer.fax.CompareTo(this.textBoxFax.Text) != 0 ||
                            m_customer.homePage.CompareTo(this.textBoxHomePage.Text) != 0 ||
                            m_customer.email.CompareTo(this.textBoxEmail.Text) != 0 ||
                            m_customer.address.CompareTo(this.textBoxAddress.Text) != 0 ||
                            m_customer.zipCode.CompareTo(this.textBoxZipCode.Text) != 0 ||
                            m_customer.note.CompareTo(this.textBoxNote.Text) != 0)
                        {
                            this.buttonAdd.Enabled = true;
                        }
                        else
                        {
                            this.buttonAdd.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (m_customerType != null)
                    {
                        if (m_customerType.name.CompareTo(this.textBoxCustomerGroupName.Text) != 0 ||
                            m_customerType.desc.CompareTo(this.textBoxCustomerGroupDesc.Text) != 0)
                        {
                            this.buttonAdd.Enabled = true;
                        }
                        else
                        {
                            this.buttonAdd.Enabled = false;
                        }
                    }
                }
            }
        }
    }
}