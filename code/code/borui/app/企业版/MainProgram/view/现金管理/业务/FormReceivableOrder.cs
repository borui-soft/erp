using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.bus;
using MainProgram.model;
using System.Diagnostics;

namespace MainProgram
{
    public partial class FormReceivableOrder : Form
    {
        private int PAYMENT_ORDER_BILL_NUMBER = 62;
        private string PAYMENT_ORDER_BILL_NAME = "收款单";
        private string m_billNumber = "";
        private bool m_isBankBill = false;

        //private 
        private CustomerTable m_customerData = new CustomerTable();
        private CashCashsubLedgerTable m_cashRecord = new CashCashsubLedgerTable();
        private BankCashsubLedgerTable m_bankRecord = new BankCashsubLedgerTable();

        public FormReceivableOrder(string billNumber = "", bool isBankBill = false)
        {
            InitializeComponent();
            m_billNumber = billNumber;
            m_isBankBill = isBankBill;
        }

        private void FormReceivableOrder_Load(object sender, EventArgs e)
        {
            // 收款类型初始化
            ComboBoxExtend.initComboBox(comboBoxReceivableType, "BASE_RECEIVABLE_TYPE_LIST");
            if (comboBoxReceivableType.Items.Count >= 1)
            {
                this.comboBoxReceivableType.SelectedIndex = 1;
            }

            // 银行账户初始化
            ComboBoxExtend.initComboBox(comboBoxBank, "BASE_BANK_LIST", true);

            // 收款科目下拉列表初始化
            this.comboBoxReceivableObject.Items.Add("库存现金");
            this.comboBoxReceivableObject.Items.Add("银行存款");
            this.comboBoxReceivableObject.SelectedIndex = 0;

            this.textBoxMakeOrderStaff.Text = DbPublic.getInctance().getCurrentLoginUserName();

            this.textBoxReceivableOrderNumber.Text = BillNumber.getInctance().getNewBillNumber(PAYMENT_ORDER_BILL_NUMBER, DateTime.Now.ToString("yyyyMMdd"));

            // 如果单据号不为空，说明用户是在其他明细账中查看该单据详细信息，此处需要显示出单据的详细信息
            if (m_billNumber.Length > 0)
            {
                this.comboBoxReceivableObject.Enabled = false;
                initUserInterfaceValue();
            }
            else
            {
                this.toolStripButtonReview.Enabled = false;
            }

            setPageActionEnable();
        }

        private void comboBoxReceivableObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxReceivableObject.SelectedIndex == 1)
            {
                this.labelBank.Visible = true;
                this.comboBoxBank.Visible = true;
            }
            else
            {
                this.labelBank.Visible = false;
                this.comboBoxBank.Visible = false;
            }
        }

        private void comboBoxReceivableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelSounceOrderNumber.Visible = true;
            textBoxSourceOrderNumber.Visible = true;
            panelSourceOrderNumber.Visible = true;
            this.textBoxSourceOrderNumber.Enabled = false;

            if (this.comboBoxReceivableType.Text.IndexOf("销售") != -1)
            {
                this.labelSounceOrderNumber.Text = "源单据号";
            }
            else if (this.comboBoxReceivableType.Text.IndexOf("应收账款") != -1)
            {
                this.labelSounceOrderNumber.Text = "往来单位";
            }
            else if (this.comboBoxReceivableType.Text.IndexOf("其他") != -1 || this.comboBoxReceivableType.Text.IndexOf("其它") != -1)
            {
                this.labelSounceOrderNumber.Text = "费用名称";
                this.panelSourceOrderNumber.Visible = false;
                this.textBoxSourceOrderNumber.Enabled = true;
            }
            else
            {
                labelSounceOrderNumber.Visible = false;
                textBoxSourceOrderNumber.Visible = false;
                panelSourceOrderNumber.Visible = false;
            }
        }

        private void panelSourceOrderNumber_Click(object sender, EventArgs e)
        {
            if (comboBoxReceivableType.Text.IndexOf("应收") != -1)
            {
                FormInitAccountReceivabler fiar = new FormInitAccountReceivabler(true, true);
                fiar.ShowDialog();
                m_customerData = Customer.getInctance().getCustomerInfoFromPkey(fiar.getCustomerOrSupplierID());
                this.textBoxSourceOrderNumber.Text = m_customerData.name;
                this.textBoxSourceOrderNumber.ReadOnly = true;
            }
            else if (comboBoxReceivableType.Text.IndexOf("销售出库") != -1)
            {
                FormSaleOrderSequence fsos = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOut, true);
                fsos.ShowDialog();
                this.textBoxSourceOrderNumber.Text = fsos.getSelectOrderNumber();
            }
            else 
            {
                this.textBoxSourceOrderNumber.Text = "";
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            this.ActiveControl = this.toolStrip1;

            /** 
             * 函数处理逻辑
             * 1、得到页面所有控件的值
             * 2、根据收款科目类型，看本次交易是现金交易，还是银行交易
             * 3、如果是现金交易写现金明细账，如果是银行存款交易写银行存款明细账
             * 4、提示保存成功或者保存失败的原因。
             * 5、把本次交易的单据号中的数字部分取出，写入到BILL_NUMBER
             * NOTE:单据保存时，不需要考虑是否审核，审核人，现金账或银行帐余额这几个信息。
             * 
             * */
            try
            {
                if ((this.comboBoxReceivableType.Text.IndexOf("应收") != -1 ||
                    this.comboBoxReceivableType.Text.IndexOf("销售") != -1) && 
                    textBoxSourceOrderNumber.Text.Length == 0)
                {
                    MessageBoxExtend.messageWarning("原单据号或往来单位不能为空，请选择.");
                    return;
                }

                if (this.textBoxTransactionAmount.Text.Length == 0)
                {
                    MessageBoxExtend.messageWarning("交易金额不能为空.");
                    return;
                }

                if (this.comboBoxReceivableObject.Text.IndexOf("现金") != -1)
                {
                    // 把数据插入到现金明细表
                    writeDataToCashCashsubLedger();
                }
                else
                {
                    // 把数据插入到银行存款明细表
                    writeDataToBankCashsubLedger();
                }

                BillNumber.getInctance().inserBillNumber(PAYMENT_ORDER_BILL_NUMBER, 
                    this.dateTime.Value.ToString("yyyyMMdd"), 
                    this.textBoxReceivableOrderNumber.Text.ToString());

                OperatorLog.getInctance().insert(501, "新增单据[" + this.textBoxReceivableOrderNumber.Text + "]");
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButtonReview_Click(object sender, EventArgs e)
        {
            /*
             * 函数实现逻辑
             * 1、检查界面数据有效性
             * 1、查看付款单类型是否是应收账款，如果是，则根据供应商ID，更新应付账款表
             * 2、根据付款方科目是现金或银行存款，计算本次交易完成后对应账户的余额
             * 3、更新相应表的IS_REVIEW字段
             * 4、提示保存成功，如果保存失败，提示保存失败的原因
             * */

            if (DbPublic.getInctance().isCheckOut())
            {
                MessageBoxExtend.messageWarning("当前会计期间已经执行结转损益，无法再审核单据！");
                return;
            }

            if (!checkUiValue())
            {
                return;
            };

            if (m_billNumber.Length > 0)
            {
                if (m_isBankBill)
                {
                    double currentSystemBankBalance = BalanceManager.getBankBalance(m_bankRecord.bankName);
                    BankCashsubLedger.getInctance().update(
                        m_billNumber, 
                        DbPublic.getInctance().getCurrentLoginUserID(),
                        m_bankRecord.turnover + currentSystemBankBalance);
                }
                else
                {
                    double currentSystemCachBalance = BalanceManager.getCachBalance();

                    CashCashsubLedger.getInctance().update(
                        m_billNumber,
                        DbPublic.getInctance().getCurrentLoginUserID(),
                        m_cashRecord.turnover + currentSystemCachBalance);
                }


                if (this.comboBoxReceivableType.Text.IndexOf("应收") != -1)
                {
                    updateAccountReceivable();
                }
                else if (this.comboBoxReceivableType.Text.IndexOf("销售出库") != -1)
                {
                    SaleOutOrder.getInctance().updataReceivedInfo(this.textBoxSourceOrderNumber.Text.ToString(),
                        Convert.ToDouble(textBoxTransactionAmount.Text.ToString()));
                }

                MessageBoxExtend.messageOK("审核成功.");
                OperatorLog.getInctance().insert(501, "单据审核[" + m_billNumber + "]");
                this.Close();
            }
        }

        private bool checkUiValue() 
        {
            bool isRet = true;

            if (m_billNumber.Length > 0)
            {
                if (m_isBankBill)
                {
                    if (textBoxVouchersNumber.Text != m_bankRecord.vouchersNumber ||
                        dateTime.Value.ToString("yyyy-MM-dd") != m_bankRecord.tradingDate ||
                        comboBoxReceivableType.Text != m_bankRecord.billTypeName ||
                        comboBoxBank.Text != m_bankRecord.bankName ||
                        textBoxTransactionAmount.Text != Convert.ToString(m_bankRecord.turnover))
                    {
                        MessageBoxExtend.messageWarning("数据被修改且未曾保存，请先保存再进行审核.");
                        isRet = false;
                    }
                }
                else
                {
                    if (textBoxVouchersNumber.Text != m_cashRecord.vouchersNumber ||
                        dateTime.Value.ToString("yyyy-MM-dd") != m_cashRecord.tradingDate ||
                        comboBoxReceivableType.Text != m_cashRecord.billTypeName ||
                        textBoxTransactionAmount.Text != Convert.ToString(m_cashRecord.turnover))
                    {
                        MessageBoxExtend.messageWarning("数据被修改且未曾保存，请先保存再进行审核.");
                        isRet = false;
                    }
                }
            }

            return isRet;
        }

        private void updateAccountReceivable()
        {
            try
            {
                CashAccountReceivableDetailTable record = new CashAccountReceivableDetailTable();

                record.customerOrSupplierID = getExchangesUnitID();
                record.billTypeName = PAYMENT_ORDER_BILL_NAME;
                record.billNumber = m_billNumber;
                record.tradingDate = DateTime.Now.ToString("yyyyMMdd");
                record.turnover = Convert.ToDouble(this.textBoxTransactionAmount.Text);
                record.staffID = DbPublic.getInctance().getCurrentLoginUserID();

                CashAccountReceivableDetail.getInctance().insert(record);
            }
            catch(Exception)
            {
            }
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

        private void writeDataToCashCashsubLedger()
        {
            CashCashsubLedgerTable record = new CashCashsubLedgerTable();
            record.tradingDate = this.dateTime.Value.ToString("yyyyMMdd");
            record.billNumber = this.textBoxReceivableOrderNumber.Text;
            record.vouchersNumber = this.textBoxVouchersNumber.Text;
            record.vouchersNumber = this.textBoxVouchersNumber.Text;
            record.billName = PAYMENT_ORDER_BILL_NAME;
            record.billTypeID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_RECEIVABLE_TYPE_LIST", comboBoxReceivableType.Text); ;

            if (this.comboBoxReceivableType.Text.IndexOf("销售出库") != -1)
            {
                record.sourceBillNumber = this.textBoxSourceOrderNumber.Text;
            }
            else if (this.comboBoxReceivableType.Text.IndexOf("应收") != -1)
            {
                record.exchangesUnit = getExchangesUnitID();
            }
            else if (this.comboBoxReceivableType.Text.IndexOf("其他") != -1 || this.comboBoxReceivableType.Text.IndexOf("其它") != -1)
            {
                record.note = this.textBoxSourceOrderNumber.Text;
            }

            record.turnover = Convert.ToDouble(this.textBoxTransactionAmount.Text);
            record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();

            CashCashsubLedger.getInctance().insert(record);
        }

        private void writeDataToBankCashsubLedger()
        {
            BankCashsubLedgerTable record = new BankCashsubLedgerTable();
            record.tradingDate = this.dateTime.Value.ToString("yyyyMMdd");
            record.bankID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_BANK_LIST", this.comboBoxBank.Text);
            record.tradingDate = this.dateTime.Value.ToString("yyyyMMdd");
            record.billNumber = this.textBoxReceivableOrderNumber.Text;
            record.vouchersNumber = this.textBoxVouchersNumber.Text;
            record.vouchersNumber = this.textBoxVouchersNumber.Text;
            record.billName = PAYMENT_ORDER_BILL_NAME;
            record.billTypeID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_RECEIVABLE_TYPE_LIST", comboBoxReceivableType.Text); ;
            
            if (this.comboBoxReceivableType.Text.IndexOf("销售出库") != -1)
            {
                record.sourceBillNumber = this.textBoxSourceOrderNumber.Text;
            }
            else if (this.comboBoxReceivableType.Text.IndexOf("应收") != -1)
            {
                record.exchangesUnit = getExchangesUnitID();
            }
            else if (this.comboBoxReceivableType.Text.IndexOf("其他") != -1 || this.comboBoxReceivableType.Text.IndexOf("其它") != -1)
            {
                record.note = this.textBoxSourceOrderNumber.Text;
            }

            record.turnover = Convert.ToDouble(this.textBoxTransactionAmount.Text);
            record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();

            BankCashsubLedger.getInctance().insert(record);
        }

        private int getExchangesUnitID()
        {
            int exchangesUnit = 0;

            if (m_customerData.pkey <= 0)
            {
                if (m_isBankBill)
                {
                    exchangesUnit = m_bankRecord.exchangesUnit;
                }
                else
                {
                    exchangesUnit = m_cashRecord.exchangesUnit;
                }
            }
            else
            {
                exchangesUnit = m_customerData.pkey;
            }
            return exchangesUnit;
        }

        private void textBoxTransactionAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 单价只能只能输入数字或小数点或退格键
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '.' && this.textBoxTransactionAmount.Text.IndexOf(".") == -1)
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        private void selectSourceOrder_Click(object sender, EventArgs e)
        {
            panelSourceOrderNumber_Click(sender, e);
        }

        private void initUserInterfaceValue()
        {
            if (m_isBankBill)
            {
                m_bankRecord = BankCashsubLedger.getInctance().getBankCashsubLedgerInfoFromOrderNumber(m_billNumber);

                // 如果单据已审核，则允许用户修改此单据
                if (m_bankRecord.isReview == "1")
                {
                    setAllActiveEnableAttribute(false);
                }

                this.textBoxReceivableOrderNumber.Text = m_bankRecord.billNumber;
                this.textBoxVouchersNumber.Text = m_bankRecord.vouchersNumber;
                this.dateTime.Text = m_bankRecord.tradingDate;

                this.comboBoxReceivableType.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_RECEIVABLE_TYPE_LIST", m_bankRecord.billTypeID);
                comboBoxReceivableType_SelectedIndexChanged(this.comboBoxReceivableType, null);

                if (this.comboBoxReceivableType.Text.IndexOf("销售出库") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_bankRecord.sourceBillNumber;
                }
                else if (this.comboBoxReceivableType.Text.IndexOf("应收") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_bankRecord.exchangesUnitName;
                }
                else if (this.comboBoxReceivableType.Text.IndexOf("其他") != -1 || this.comboBoxReceivableType.Text.IndexOf("其它") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_bankRecord.note;
                }
                this.comboBoxReceivableObject.Text = "银行存款";
                this.comboBoxBank.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_BANK_LIST", m_bankRecord.bankID);
                comboBoxReceivableObject_SelectedIndexChanged(this.comboBoxReceivableObject, null);

                this.textBoxTransactionAmount.Text = Convert.ToString(m_bankRecord.turnover);
                this.textBoxMakeOrderStaff.Text = m_bankRecord.makeOrderStaffName;
                this.textBoxOrderReview.Text = m_bankRecord.orderReviewName;
            }
            else
            {
                m_cashRecord = CashCashsubLedger.getInctance().getCashCashsubLedgerInfoFromOrderNumber(m_billNumber);

                // 如果单据已审核，则允许用户修改此单据
                if (m_cashRecord.isReview == "1")
                {
                    setAllActiveEnableAttribute(false);
                }

                this.textBoxReceivableOrderNumber.Text = m_cashRecord.billNumber;
                this.textBoxVouchersNumber.Text = m_cashRecord.vouchersNumber;
                this.dateTime.Text = m_cashRecord.tradingDate;

                this.comboBoxReceivableType.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_RECEIVABLE_TYPE_LIST", m_cashRecord.billTypeID);
                comboBoxReceivableType_SelectedIndexChanged(this.comboBoxReceivableType, null);

                if (this.comboBoxReceivableType.Text.IndexOf("销售出库") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_cashRecord.sourceBillNumber;
                }
                else if (this.comboBoxReceivableType.Text.IndexOf("应收") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_cashRecord.exchangesUnitName;
                }
                else if (this.comboBoxReceivableType.Text.IndexOf("其他") != -1 || this.comboBoxReceivableType.Text.IndexOf("其它") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_cashRecord.note;
                }
                this.comboBoxReceivableObject.Text = "库存现金";
                this.textBoxTransactionAmount.Text = Convert.ToString(m_cashRecord.turnover);
                this.textBoxMakeOrderStaff.Text = m_cashRecord.makeOrderStaffName;
                this.textBoxOrderReview.Text = m_cashRecord.orderReviewName;
            }
        }

        private void setAllActiveEnableAttribute(bool value)
        {
            this.save.Enabled = value;
            this.textBoxSourceOrderNumber.Enabled = value;
            this.toolStripButtonReview.Enabled = value;
            this.selectSourceOrder.Enabled = value;
            this.textBoxVouchersNumber.Enabled = value;
            this.dateTime.Enabled = value;
            this.comboBoxReceivableType.Enabled = value;
            this.textBoxSourceOrderNumber.Enabled = value;
            this.comboBoxReceivableObject.Enabled = value;
            this.textBoxTransactionAmount.Enabled = value;
            this.comboBoxBank.Enabled = value;
            this.panelIsReview.Visible = !value;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(501);

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