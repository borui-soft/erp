using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Diagnostics;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormSaleQuotationOrder : Form
    {
        private int m_customerPkey = -1;
        private int m_staffPkey = -1;
        private string m_billNumber = "";
        private readonly int BillTypeNumber = 4;

        SaleQuotationOrderTable m_saleQuotationOrder = new SaleQuotationOrderTable();

        public FormSaleQuotationOrder(string billNumber = "")
        {
            InitializeComponent();
            m_billNumber = billNumber;
        }

        private void FormSaleQuotationOrder_Load(object sender, EventArgs e)
        {
            if (m_billNumber.Length == 0)
            {
                // 单据号
                this.labelBillNumber.Text = BillNumber.getInctance().getNewBillNumber(BillTypeNumber, DateTime.Now.ToString("yyyy-MM-dd"));

                // 制单人初始化
                this.labelMakeBillStaff.Visible = true;
                this.labelMakeBillStaff.Text = DbPublic.getInctance().getCurrentLoginUserName();
            }
            else 
            {
                readBillInfoToUI();
            }

            setPageActionEnable();
        }

        #region 客户
        private void panelSaleQuotationName_Click(object sender, EventArgs e)
        {
            if (!this.textBoxSaleQuotationName.Visible)
            {
                this.labelSaleQuotationName.Visible = false; 
                this.textBoxSaleQuotationName.Visible = true;
                this.textBoxSaleQuotationName.Focus();
            }
        }
        private void panelSaleQuotationName_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxSaleQuotationName.Visible)
            {
                this.labelSaleQuotationName.Visible = false; 
                this.textBoxSaleQuotationName.Visible = true;
            }
            else
            {
                FormBaseCustomer fbs = new FormBaseCustomer(true);
                fbs.ShowDialog();

                m_customerPkey = fbs.getSelectRecordPkey();
                CustomerTable record = Customer.getInctance().getCustomerInfoFromPkey(m_customerPkey);
                this.textBoxSaleQuotationName.Text = record.name;
                this.textBoxSaleQuotationName.Visible = true;
            }
        }
        private void textBoxSaleQuotationName_Leave(object sender, EventArgs e)
        {
            this.textBoxSaleQuotationName.Visible = false;
            this.labelSaleQuotationName.Text = this.textBoxSaleQuotationName.Text.ToString();
            this.labelSaleQuotationName.Visible = this.textBoxSaleQuotationName.Text.Length > 0;
        }
        #endregion

        #region 供应日期
        private void panelDateTime_Click(object sender, EventArgs e)
        {
            if (!this.dateTimePickerTradingDate.Visible)
            {
                this.panelTradingDate.Visible = false;
                this.labelTradingDate.Visible = false;
                this.dateTimePickerTradingDate.Visible = true;
                this.dateTimePickerTradingDate.Focus();
            }
        }
        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            this.panelTradingDate.Visible = true;

            this.labelTradingDate.Visible = true;

            this.dateTimePickerTradingDate.Visible = false;
            this.labelTradingDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 业务员
        private void panelBusinessPeople_Click(object sender, EventArgs e)
        {
            if (!this.textBoxBusinessPeople.Visible)
            {
                this.labelBusinessPeople.Visible = false;
                this.textBoxBusinessPeople.Visible = true;
                this.textBoxBusinessPeople.Focus();
            }
        }

        private void panelBusinessPeople_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxBusinessPeople.Visible)
            {
                this.labelBusinessPeople.Visible = false;
                this.textBoxBusinessPeople.Visible = true;
            }
            else
            {
                FormBaseStaff fbs = new FormBaseStaff(true);
                fbs.ShowDialog();

                m_staffPkey = fbs.getSelectRecordPkey();
                StaffTable record = Staff.getInctance().getStaffInfoFromPkey(m_staffPkey);
                this.textBoxBusinessPeople.Text = record.name;
                this.textBoxBusinessPeople.Visible = true;
            }
        }

        private void textBoxBusinessPeople_Leave(object sender, EventArgs e)
        {
            this.textBoxBusinessPeople.Visible = false;
            this.labelBusinessPeople.Text = this.textBoxBusinessPeople.Text.ToString();
            this.labelBusinessPeople.Visible = this.textBoxBusinessPeople.Text.Length > 0;
        }
        #endregion

        #region 联系人
        private void textBoxContact_Click(object sender, EventArgs e)
        {
            this.textBoxContact.Visible = true;
            this.labelContact.Visible = false;
            this.textBoxContact.Focus();
        }

        private void textBoxContact_Leave(object sender, EventArgs e)
        {
            this.labelContact.Text = this.textBoxContact.Text;
            this.textBoxContact.Visible = false;
            this.labelContact.Visible = true;
            this.textBoxContact.Text = "";
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.textBoxContact.Visible = true;
            this.labelContact.Visible = false;
            this.textBoxContact.Focus();
        }

        #endregion

        #region 联系电话
        private void textBoxTel_Click(object sender, EventArgs e)
        {
            this.textBoxTel.Visible = true;
            this.labelTel.Visible = false;
            this.textBoxTel.Focus();
        }

        private void textBoxTel_Leave(object sender, EventArgs e)
        {
            this.labelTel.Text = this.textBoxTel.Text;
            this.textBoxTel.Visible = false;
            this.labelTel.Visible = true;
            this.textBoxTel.Text = "";
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.textBoxTel.Visible = true;
            this.labelTel.Visible = false;
            this.textBoxTel.Focus();
        }

        #endregion

        private void save_Click(object sender, EventArgs e)
        {
            SaleQuotationOrderTable saleQuotationOrder = new SaleQuotationOrderTable();


            saleQuotationOrder.customerName = this.labelSaleQuotationName.Text;
            saleQuotationOrder.date = this.labelTradingDate.Text;
            saleQuotationOrder.billNumber = this.labelBillNumber.Text;

            saleQuotationOrder.content = this.textBoxContent.Text;
            saleQuotationOrder.note = this.textBoxNote.Text;

            saleQuotationOrder.salemanName = this.labelBusinessPeople.Text;
            saleQuotationOrder.makeOrderName = this.labelMakeBillStaff.Text;
            saleQuotationOrder.contact = this.labelContact.Text;
            saleQuotationOrder.tel = this.labelTel.Text;


            if (m_billNumber.Length == 0)
            {
                SaleQuotationOrder.getInctance().insert(saleQuotationOrder);
            }
            else
            {
                SaleQuotationOrder.getInctance().update(m_billNumber, saleQuotationOrder);
            }

            this.Close();

            BillNumber.getInctance().inserBillNumber(BillTypeNumber, this.labelTradingDate.Text, this.labelBillNumber.Text.ToString());
        }

        private void printDisplay_Click(object sender, EventArgs e)
        {
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, true);
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y);
        }

        private void calculator_Click(object sender, EventArgs e)
        {
            Process.Start("Calc");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void readBillInfoToUI()
        {
            // 单据表头表尾信息
            m_saleQuotationOrder = SaleQuotationOrder.getInctance().getSaleQuotationOrderInfoFromBillNumber(m_billNumber);

            this.labelSaleQuotationName.Visible = true;
            this.labelTradingDate.Visible = true;
            this.labelBillNumber.Visible = true;
            this.textBoxContent.Visible = true;
            this.textBoxNote.Visible = true;
            this.labelBusinessPeople.Visible = true;
            this.labelMakeBillStaff.Visible = true;
            this.labelContact.Visible = true;
            this.labelTel.Visible = true;

            this.labelSaleQuotationName.Text = m_saleQuotationOrder.customerName;
            this.labelTradingDate.Text = m_saleQuotationOrder.date;
            this.labelBillNumber.Text = m_saleQuotationOrder.billNumber;

            this.textBoxContent.Text = m_saleQuotationOrder.content;
            this.textBoxNote.Text = m_saleQuotationOrder.note;

            this.labelBusinessPeople.Text = m_saleQuotationOrder.salemanName;
            this.labelMakeBillStaff.Text = m_saleQuotationOrder.makeOrderName;
            this.labelContact.Text = m_saleQuotationOrder.contact;
            this.labelTel.Text = m_saleQuotationOrder.tel;
        }

        private void textBoxTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 物料数量只能输入数字或退格键
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

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(201);

            foreach (KeyValuePair<int, ActionTable> index in list)
            {
                object activeObject = this.GetType().GetField(index.Value.uiActionName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                bool isEnable = AccessAuthorization.getInctance().isAccessAuthorization(index.Value.pkey,
                    Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));

                if (activeObject != null)
                {
                    UserInterfaceActonState.setUserInterfaceActonState(activeObject,
                        ((System.Reflection.MemberInfo)(activeObject.GetType())).Name.ToString(), isEnable);
                }
            }
        }
    }
}