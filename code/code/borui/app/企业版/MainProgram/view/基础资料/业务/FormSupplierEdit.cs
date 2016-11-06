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
    public partial class FormSupplierEdit : Form
    {
        private int m_supplierGroupPkey = 1;
        private bool m_isSupplierGroup = false;

        private SupplierTable m_supplier;
        private SupplierTypeTable m_supplierType;

        private bool m_isAdd = true;
        private bool m_isEditSupplier = true;
        private int m_supplierPkey = 0;
        
        public FormSupplierEdit(bool isAdd, int supplierGroupPkey, bool isEditSupplier = true, int pkey = 0)
        {
            m_isAdd = isAdd;
            m_supplierGroupPkey = supplierGroupPkey;
            m_isEditSupplier = isEditSupplier;
            m_supplierPkey = pkey;

            InitializeComponent();
        }

        private void FormSupplierEdit_Load(object sender, EventArgs e)
        {
            this.textBoxCredit.Text = "0";
            this.textBoxVatRate.Text = "17";

            if (!m_isAdd)
            {
                this.buttonAdd.Enabled = false;
                this.buttonSupplierGroup.Enabled = false;

                if (m_isEditSupplier)
                {
                    m_supplier = Supplier.getInctance().getSupplierInfoFromPkey(m_supplierPkey);
                }
                else
                {
                    m_supplierType = SupplierType.getInctance().getSupplierTypeInfoFromPkey(m_supplierGroupPkey);
                    m_isSupplierGroup = true;
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
                if (!m_isSupplierGroup)
                {
                    addSupplier();
                }
                else
                {
                    addSupplierType();
                }
            }
            else
            {
                if (!m_isSupplierGroup)
                {
                    modifySupplier();
                }
                else
                {
                    modifySupplierType();
                }
            }

            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSupplierGroup_Click(object sender, EventArgs e)
        {
			if(m_isSupplierGroup)
			{
				m_isSupplierGroup = false;
			}
			else
			{
				m_isSupplierGroup = true;
			}

            setPageActiveState();
        }

        private void setPageActiveState()
        {
            if (m_isSupplierGroup)
            {
                buttonSupplierGroup.BackColor = System.Drawing.Color.LightGray;
                tabControl1.Visible = false;
                groupBoxSupplierGroup.Show();
            }
            else
            {
                buttonSupplierGroup.BackColor = System.Drawing.Color.Transparent;
                tabControl1.Visible = true;
                groupBoxSupplierGroup.Hide();
            }
        }

        private void setPageActiveValue()
        {
            if (m_isSupplierGroup)
            {
                this.textBoxSupplierGroupName.Text = m_supplierType.name;
                this.textBoxSupplierGroupDesc.Text = m_supplierType.desc;
            }
            else
            {
                // 基本信息-基础信息
                this.textBoxName.Text = m_supplier.name;
                this.textBoxShortName.Text = m_supplier.nameShort;
                this.textBoxMnemonicCode.Text = m_supplier.mnemonicCode;
                this.textBoxCredit.Text = Convert.ToString(m_supplier.credit);
                this.textBoxVatRate.Text = Convert.ToString(m_supplier.varRate);

                // 基础信息-银行信息
                this.textBoxBankName.Text = m_supplier.bankName;
                this.textBoxBankAccount.Text = m_supplier.bankAccount;
                this.textBoxTaxAccount.Text = m_supplier.taxAccount;

                // 联系方式
                this.comboBoxArea.Text = m_supplier.area;
                this.textBoxContact.Text = m_supplier.contact;
                this.textBoxTel.Text = m_supplier.tel;
                this.textBoxFax.Text = m_supplier.fax;
                this.textBoxHomePage.Text = m_supplier.homePage;
                this.textBoxEmail.Text = m_supplier.email;
                this.textBoxAddress.Text = m_supplier.address;
                this.textBoxZipCode.Text = m_supplier.zipCode;
                this.textBoxNote.Text = m_supplier.note;
            }
        }

        private void addSupplier()
        {
            SupplierTable supplier = new SupplierTable();

            // 基本信息-基础信息
            supplier.supplierType = m_supplierGroupPkey;
            supplier.name = this.textBoxName.Text.ToString();
            supplier.nameShort = this.textBoxShortName.Text.ToString();
            supplier.mnemonicCode = this.textBoxMnemonicCode.Text.ToString();
            if (this.textBoxCredit.Text.ToString().Length == 0)
            {
                supplier.credit = 0;
            }
            else
            {
                supplier.credit = Convert.ToInt32(this.textBoxCredit.Text.ToString());
            }

            if (this.textBoxVatRate.Text.ToString().Length == 0)
            {
                supplier.varRate = Convert.ToInt32(this.textBoxVatRate.Text.ToString());
            }
            else
            {
                // 增值税税率默认为17%
                supplier.varRate = 17;
            }

            // 基础信息-银行信息
            supplier.bankName = this.textBoxBankName.Text.ToString();
            supplier.bankAccount = this.textBoxBankAccount.Text.ToString();
            supplier.taxAccount = this.textBoxTaxAccount.Text.ToString();

            // 联系方式
            supplier.area = this.comboBoxArea.Text.ToString();
            supplier.contact = this.textBoxContact.Text.ToString();
            supplier.tel = this.textBoxTel.Text.ToString();
            supplier.fax = this.textBoxFax.Text.ToString();
            supplier.homePage = this.textBoxHomePage.Text.ToString();
            supplier.email = this.textBoxEmail.Text.ToString();
            supplier.address = this.textBoxAddress.Text.ToString();
            supplier.zipCode = this.textBoxZipCode.Text.ToString();
            supplier.note = this.textBoxNote.Text.ToString();

            if (supplier.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("公司名称不能为空，请重新填写!");
                return;
            }

            Supplier.getInctance().insert(supplier);
        }

        private void addSupplierType()
        {
            SupplierTypeTable supplierType = new SupplierTypeTable();
            supplierType.name = this.textBoxSupplierGroupName.Text.ToString();
            supplierType.desc = this.textBoxSupplierGroupDesc.Text.ToString();

            if (supplierType.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("组名称不能为空，请重新填写!");
                return;
            }

            SupplierType.getInctance().insert(supplierType);


            // 供应商组织结构
            SupplierOrgStructTable supplierOrgInfo = new SupplierOrgStructTable();

            supplierOrgInfo.parentPkey = SupplierOrgStruct.getInctance().getPkeyFromValue(m_supplierGroupPkey);
            supplierOrgInfo.value = SupplierType.getInctance().getMaxPkey();
            SupplierOrgStruct.getInctance().insert(supplierOrgInfo);
        }

        private void modifySupplier()
        {
            SupplierTable supplier = new SupplierTable();

            // 基本信息-基础信息
            supplier.supplierType = m_supplierGroupPkey;
            supplier.name = this.textBoxName.Text.ToString();
            supplier.nameShort = this.textBoxShortName.Text.ToString();
            supplier.mnemonicCode = this.textBoxMnemonicCode.Text.ToString();
            if (this.textBoxCredit.Text.ToString().Length == 0)
            {
                supplier.credit = 0;
            }
            else
            {
                supplier.credit = Convert.ToInt32(this.textBoxCredit.Text.ToString());
            }

            if (this.textBoxVatRate.Text.ToString().Length == 0)
            {
                supplier.varRate = Convert.ToInt32(this.textBoxVatRate.Text.ToString());
            }
            else
            {
                supplier.varRate = 17;
            }

            // 基础信息-银行信息
            supplier.bankName = this.textBoxBankName.Text.ToString();
            supplier.bankAccount = this.textBoxBankAccount.Text.ToString();
            supplier.taxAccount = this.textBoxTaxAccount.Text.ToString();

            // 联系方式
            supplier.area = this.comboBoxArea.Text.ToString();
            supplier.contact = this.textBoxContact.Text.ToString();
            supplier.tel = this.textBoxTel.Text.ToString();
            supplier.fax = this.textBoxFax.Text.ToString();
            supplier.homePage = this.textBoxHomePage.Text.ToString();
            supplier.email = this.textBoxEmail.Text.ToString();
            supplier.address = this.textBoxAddress.Text.ToString();
            supplier.zipCode = this.textBoxZipCode.Text.ToString();
            supplier.note = this.textBoxNote.Text.ToString();

            if (supplier.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("公司名称不能为空，请重新填写!");
                return;
            }

            Supplier.getInctance().update(m_supplierPkey, supplier);
        }

        private void modifySupplierType()
        {
            SupplierTypeTable supplierType = new SupplierTypeTable();
            supplierType.name = this.textBoxSupplierGroupName.Text.ToString();
            supplierType.desc = this.textBoxSupplierGroupDesc.Text.ToString();

            if (supplierType.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("组名称不能为空，请重新填写!");
                return;
            }

            SupplierType.getInctance().update(m_supplierGroupPkey, supplierType);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!m_isAdd)
            {
                if (!m_isSupplierGroup)
                {
                    if (m_supplier != null)
                    {
                        if (m_supplier.name.CompareTo(this.textBoxName.Text) != 0 ||
                            m_supplier.nameShort.CompareTo(this.textBoxShortName.Text) != 0 ||
                            m_supplier.mnemonicCode.CompareTo(this.textBoxMnemonicCode.Text) != 0 ||
                            m_supplier.credit != Convert.ToInt32(this.textBoxCredit.Text.ToString()) ||
                            m_supplier.varRate != Convert.ToInt32(this.textBoxVatRate.Text.ToString()) ||
                            m_supplier.bankName.CompareTo(this.textBoxBankName.Text) != 0 ||
                            m_supplier.bankAccount.CompareTo(this.textBoxBankAccount.Text) != 0 ||
                            m_supplier.taxAccount.CompareTo(this.textBoxTaxAccount.Text) != 0 ||
                            m_supplier.area.CompareTo(this.comboBoxArea.Text) != 0 ||
                            m_supplier.contact.CompareTo(this.textBoxContact.Text) != 0 ||
                            m_supplier.tel.CompareTo(this.textBoxTel.Text) != 0 ||
                            m_supplier.fax.CompareTo(this.textBoxFax.Text) != 0 ||
                            m_supplier.homePage.CompareTo(this.textBoxHomePage.Text) != 0 ||
                            m_supplier.email.CompareTo(this.textBoxEmail.Text) != 0 ||
                            m_supplier.address.CompareTo(this.textBoxAddress.Text) != 0 ||
                            m_supplier.zipCode.CompareTo(this.textBoxZipCode.Text) != 0 ||
                            m_supplier.note.CompareTo(this.textBoxNote.Text) != 0)
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
                    if (m_supplierType != null)
                    {
                        if (m_supplierType.name.CompareTo(this.textBoxSupplierGroupName.Text) != 0 ||
                            m_supplierType.desc.CompareTo(this.textBoxSupplierGroupDesc.Text) != 0)
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