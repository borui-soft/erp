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
    public partial class FormMaterielPriceSheetEdit : Form
    {
        private bool m_isEditRecord = false;
        private bool m_isDisplaySelectButton = false;
        private int m_supplierPkey = -1;
        private int m_matetielID = 0;
        private int m_editRecordPkey = 0;

        private SupplierPriceSheetTable m_supplierPriceSheetRecord = new SupplierPriceSheetTable();

        public FormMaterielPriceSheetEdit(int matetielID, bool isEditRecord = false, int editRecordPkey = 0, bool isDisplaySelectButton = true)
        {
            InitializeComponent();
            m_matetielID = matetielID;
            m_isEditRecord = isEditRecord;
            m_editRecordPkey = editRecordPkey;
            m_isDisplaySelectButton = isDisplaySelectButton;

            if (m_isDisplaySelectButton)
            {
                this.Text = "供应商报价编辑";
            }
            else 
            {
                this.Text = "商品销售价格编辑";
            }
        }

        private void FormSupplierPriceSheetEdit_Load(object sender, EventArgs e)
        {
            if (m_isEditRecord)
            {
                this.buttonEdit.Enabled = true;
                this.buttonSelect.Enabled = false;
                this.textBoxOrnmFromValue.Enabled = false;
                this.textBoxOrnmToValue.Enabled = false;
                this.textBoxPrice.Enabled = false;
                this.textBoxNote.Enabled = false;
                this.buttonEnter.Enabled = false;

                m_supplierPriceSheetRecord = SupplierPriceSheet.getInctance().getSupplierPriceSheetInfoFromPkey(m_editRecordPkey);

                m_supplierPkey = m_supplierPriceSheetRecord.supplierId;
                this.textBoxSupplierName.Text = Convert.ToString(m_supplierPriceSheetRecord.supplierId) + "-" + m_supplierPriceSheetRecord.supplierName;
                this.textBoxOrnmFromValue.Text = m_supplierPriceSheetRecord.ORNMFromValue;
                this.textBoxOrnmToValue.Text = m_supplierPriceSheetRecord.ORNMToValue;
                this.textBoxPrice.Text = m_supplierPriceSheetRecord.pirce;
                this.textBoxNote.Text = m_supplierPriceSheetRecord.note;
            }

            if (!m_isDisplaySelectButton)
            {
                this.labelSupplier.Hide();
                this.textBoxSupplierName.Hide();
                this.buttonSelect.Hide();
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FormBaseSupplier fbs = new FormBaseSupplier(true);
            fbs.ShowDialog();

            m_supplierPkey = fbs.getSelectRecordPkey();
            SupplierTable record = Supplier.getInctance().getSupplierInfoFromPkey(m_supplierPkey);
            this.textBoxSupplierName.Text = Convert.ToString(record.pkey) + "-" + record.name;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (m_isDisplaySelectButton)
            {
                if (textBoxSupplierName.Text.Length == 0)
                {
                    MessageBoxExtend.messageWarning("供应商不能为空");
                    return;
                }
            }

            if (textBoxPrice.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("单价不能为空");
                return;
            }

            if (textBoxPrice.Text.Length > 10)
            {
                MessageBoxExtend.messageWarning("单价最大长度不能超过10");
                textBoxPrice.Text = "";
                return;
            }

            SupplierPriceSheetTable record = new SupplierPriceSheetTable();
            record.supplierId = m_supplierPkey;
            record.matetielId = m_matetielID;

            if (this.textBoxOrnmFromValue.Text.Length == 0)
            {
                record.ORNMFromValue = "0";
            }
            else 
            {
                record.ORNMFromValue = this.textBoxOrnmFromValue.Text;
            }

            if (this.textBoxOrnmToValue.Text.Length == 0)
            {
                record.ORNMToValue = "无限制";
            }
            else
            {
                record.ORNMToValue = this.textBoxOrnmToValue.Text;
            }

            record.pirce = this.textBoxPrice.Text;
            record.note = this.textBoxNote.Text;

            if (!m_isEditRecord)
            {
                SupplierPriceSheet.getInctance().insert(record);
            }
            else 
            {
                SupplierPriceSheet.getInctance().update(m_editRecordPkey, record);
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.buttonEdit.Enabled = false;
            this.textBoxPrice.Enabled = true;
            this.textBoxOrnmFromValue.Enabled = true;
            this.textBoxOrnmToValue.Enabled = true;
            this.textBoxNote.Enabled = true;
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

            if (e.KeyChar == '.' && this.textBoxPrice.Text.IndexOf(".") == -1)
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
