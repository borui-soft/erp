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
    public partial class FormPaymentOrder : Form
    {
        private SupplierTable m_supplierData = new SupplierTable();
        private CashCashsubLedgerTable m_cashRecord = new CashCashsubLedgerTable();
        private BankCashsubLedgerTable m_bankRecord = new BankCashsubLedgerTable();

        private int PAYMENT_ORDER_BILL_NUMBER = 61;
        private string PAYMENT_ORDER_BILL_NAME = "付款单";
        private string m_billNumber = "";
        private bool m_isBankBill = false;

        public FormPaymentOrder(string billNumber = "", bool isBankBill = false)
        {
            InitializeComponent();
            m_billNumber = billNumber;
            m_isBankBill = isBankBill;
        }

        private void FormPaymentOrder_Load(object sender, EventArgs e)
        {
            // 付款类型初始化
            ComboBoxExtend.initComboBox(comboBoxPaymentType, "BASE_PAYMENT_TYPE_LIST");
            if (comboBoxPaymentType.Items.Count >= 1)
            {
                this.comboBoxPaymentType.SelectedIndex = 1;
            }

            // 银行账户初始化
            ComboBoxExtend.initComboBox(comboBoxBank, "BASE_BANK_LIST", true);

            // 付款科目下拉列表初始化
            this.comboBoxPaymentObject.Items.Add("库存现金");
            this.comboBoxPaymentObject.Items.Add("银行存款");
            this.comboBoxPaymentObject.SelectedIndex = 0;

            this.textBoxMakeOrderStaff.Text = DbPublic.getInctance().getCurrentLoginUserName();

            this.textBoxPaymentOrderNumber.Text = BillNumber.getInctance().getNewBillNumber(PAYMENT_ORDER_BILL_NUMBER, DateTime.Now.ToString("yyyyMMdd"));

            // 如果单据号不为空，说明用户是在其他明细账中查看该单据详细信息，此处需要显示出单据的详细信息
            if (m_billNumber.Length > 0)
            {
                this.comboBoxPaymentObject.Enabled = false;
                initUserInterfaceValue();
            }
            else 
            {
                this.toolStripButtonReview.Enabled = false;
            }

            setPageActionEnable();
        }

        private void comboBoxPaymentObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxPaymentObject.SelectedIndex == 1)
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

        private void comboBoxPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelSounceOrderNumber.Visible = true;
            textBoxSourceOrderNumber.Visible = true;
            panelSourceOrderNumber.Visible = true;
            this.textBoxSourceOrderNumber.Enabled = false;

            if (this.comboBoxPaymentType.Text.IndexOf("采购") != -1)
            {
                this.labelSounceOrderNumber.Text = "源单据号";
            }
            else if (this.comboBoxPaymentType.Text.IndexOf("应付账款") != -1)
            {
                this.labelSounceOrderNumber.Text = "往来单位";
            }
            else if (this.comboBoxPaymentType.Text.IndexOf("其他") != -1 || this.comboBoxPaymentType.Text.IndexOf("其它") != -1)
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
            if (comboBoxPaymentType.Text.IndexOf("应付") != -1)
            {
                FormInitAccountReceivabler fiar = new FormInitAccountReceivabler(false, true);
                fiar.ShowDialog();
                m_supplierData = Supplier.getInctance().getSupplierInfoFromPkey(fiar.getCustomerOrSupplierID());
                this.textBoxSourceOrderNumber.Text = m_supplierData.name;
                this.textBoxSourceOrderNumber.ReadOnly = true;
            }
            else if (comboBoxPaymentType.Text.IndexOf("采购入库") != -1)
            {
                FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseIn, true);
                fpos.ShowDialog();
                this.textBoxSourceOrderNumber.Text = fpos.getSelectOrderNumber();
            }
            else
            {
                this.textBoxSourceOrderNumber.Text = "";
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            /** 
             * 函数处理逻辑
             * 1、得到页面所有控件的值
             * 2、根据付款科目类型，看本次交易是现金交易，还是银行交易
             * 3、如果是现金交易写现金明细账，如果是银行存款交易写银行存款明细账
             * 4、提示保存成功或者保存失败的原因。
             * 5、把本次交易的单据号中的数字部分取出，写入到BILL_NUMBER
             * NOTE:单据保存时，不需要考虑是否审核，审核人，现金账或银行帐余额这几个信息。
             * 
             * */
            try
            {
                if ((this.comboBoxPaymentType.Text.IndexOf("应付") != -1 ||
                    this.comboBoxPaymentType.Text.IndexOf("采购") != -1) && 
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

                if (this.comboBoxPaymentObject.Text.IndexOf("现金") != -1)
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
                    this.textBoxPaymentOrderNumber.Text.ToString());

                OperatorLog.getInctance().insert(502, "新增单据[" + this.textBoxPaymentOrderNumber.Text + "]");

                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void selectSourceOrder_Click(object sender, EventArgs e)
        {
            panelSourceOrderNumber_Click(sender, e);
        }

        private void toolStripButtonReview_Click(object sender, EventArgs e)
        {
            /*
             * 函数实现逻辑
             * 1、检查界面数据有效性
             * 1、查看付款单类型是否是应付账款，如果是，则根据供应商ID，更新应付账款表
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

                    if (m_bankRecord.turnover > currentSystemBankBalance)
                    {
                        string msg = "付款交易失败\n";
                        msg += "[" + m_bankRecord.bankName + "]余额为：";
                        msg += Convert.ToString(currentSystemBankBalance) + ",不足以完成本次支付.";
                        MessageBoxExtend.messageWarning(msg);
                        return;
                    }
                    else
                    {
                        BankCashsubLedger.getInctance().update(
                            m_billNumber,
                            DbPublic.getInctance().getCurrentLoginUserID(),
                            currentSystemBankBalance - m_bankRecord.turnover);
                    }
                }
                else
                {
                    double currentSystemCachBalance = BalanceManager.getCachBalance();

                    if (m_cashRecord.turnover > currentSystemCachBalance)
                    {
                        string msg = "付款交易失败\n";
                        msg += "库存现金余额为：";
                        msg += Convert.ToString(currentSystemCachBalance) + ",不足以完成本次支付.";
                        MessageBoxExtend.messageWarning(msg);
                        return;
                    }
                    else 
                    {
                        CashCashsubLedger.getInctance().update(
                            m_billNumber,
                            DbPublic.getInctance().getCurrentLoginUserID(),
                            currentSystemCachBalance - m_cashRecord.turnover);
                    }
                }

                if (this.comboBoxPaymentType.Text.IndexOf("应付") != -1)
                {
                    updateAccountPayment();
                }
                else if (this.comboBoxPaymentType.Text.IndexOf("采购入库") != -1)
                {
                    PurchaseInOrder.getInctance().updataPaymentInfo(this.textBoxSourceOrderNumber.Text.ToString(),
                        Convert.ToDouble(textBoxTransactionAmount.Text.ToString()));
                }

                MessageBoxExtend.messageOK("审核成功.");
                OperatorLog.getInctance().insert(502, "单据审核[" + m_billNumber + "]");
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
                        comboBoxPaymentType.Text != m_bankRecord.billTypeName ||
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
                        comboBoxPaymentType.Text != m_cashRecord.billTypeName ||
                        textBoxTransactionAmount.Text != Convert.ToString(m_cashRecord.turnover))
                    {
                        MessageBoxExtend.messageWarning("数据被修改且未曾保存，请先保存再进行审核.");
                        isRet = false;
                    }
                }
            }

            return isRet;
        }

        private void updateAccountPayment()
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

                CashAccountPayableDetail.getInctance().insert(record);
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
            record.billNumber = this.textBoxPaymentOrderNumber.Text;
            record.vouchersNumber = this.textBoxVouchersNumber.Text;
            record.billName = PAYMENT_ORDER_BILL_NAME;
            record.billTypeID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_PAYMENT_TYPE_LIST", comboBoxPaymentType.Text); ;

            if (this.comboBoxPaymentType.Text.IndexOf("采购入库") != -1)
            {
                // 如果是采购入库，则需要采集原始单据号
                record.sourceBillNumber = this.textBoxSourceOrderNumber.Text;
            }
            else if (this.comboBoxPaymentType.Text.IndexOf("应付") != -1)
            {
                // 如果是应付账款，则需要记录往来单位名称
                record.exchangesUnit = getExchangesUnitID();
            }
            else if (this.comboBoxPaymentType.Text.IndexOf("其他") != -1 || this.comboBoxPaymentType.Text.IndexOf("其它") != -1)
            {
                // 如果是其他付款，则需要记录其他付款条目到备注字段
                record.note = this.textBoxSourceOrderNumber.Text;
            }

            record.turnover = Convert.ToDouble(this.textBoxTransactionAmount.Text);
            record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();

            CashCashsubLedger.getInctance().insert(record);
        }

        private int getExchangesUnitID()
        {
            int exchangesUnit = 0;

            if (m_supplierData.pkey <= 0)
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
                exchangesUnit = m_supplierData.pkey;
            }

            return exchangesUnit;
        }

        private void writeDataToBankCashsubLedger()
        {
            BankCashsubLedgerTable record = new BankCashsubLedgerTable();
            record.tradingDate = this.dateTime.Value.ToString("yyyyMMdd");
            record.bankID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_BANK_LIST", this.comboBoxBank.Text);
            record.tradingDate = this.dateTime.Value.ToString("yyyyMMdd");
            record.billNumber = this.textBoxPaymentOrderNumber.Text;
            record.vouchersNumber = this.textBoxVouchersNumber.Text;
            record.billName = PAYMENT_ORDER_BILL_NAME;
            record.billTypeID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName("BASE_PAYMENT_TYPE_LIST", comboBoxPaymentType.Text); ;
            
            if (this.comboBoxPaymentType.Text.IndexOf("采购入库") != -1)
            {
                // 如果是采购入库，则需要采集原始单据号
                record.sourceBillNumber = this.textBoxSourceOrderNumber.Text;
            }
            else if (this.comboBoxPaymentType.Text.IndexOf("应付") != -1)
            {
                // 如果是应付账款，则需要记录往来单位名称
                record.exchangesUnit = getExchangesUnitID();
            }
            else if (this.comboBoxPaymentType.Text.IndexOf("其他") != -1 || this.comboBoxPaymentType.Text.IndexOf("其它") != -1)
            {
                // 如果是其他付款，则需要记录其他付款条目到备注字段
                record.note = this.textBoxSourceOrderNumber.Text;
            }

            record.turnover = Convert.ToDouble(this.textBoxTransactionAmount.Text);
            record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();

            BankCashsubLedger.getInctance().insert(record);
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

                this.textBoxPaymentOrderNumber.Text = m_bankRecord.billNumber;
                this.textBoxVouchersNumber.Text = m_bankRecord.vouchersNumber;
                this.dateTime.Text = m_bankRecord.tradingDate;

                this.comboBoxPaymentType.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_PAYMENT_TYPE_LIST", m_bankRecord.billTypeID);
                comboBoxPaymentType_SelectedIndexChanged(this.comboBoxPaymentType, null);

                if (this.comboBoxPaymentType.Text.IndexOf("采购入库") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_bankRecord.sourceBillNumber;
                }
                else if (this.comboBoxPaymentType.Text.IndexOf("应付") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_bankRecord.exchangesUnitName;
                }
                else if (this.comboBoxPaymentType.Text.IndexOf("其他") != -1 || this.comboBoxPaymentType.Text.IndexOf("其它") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_bankRecord.note;
                }
                this.comboBoxPaymentObject.Text = "银行存款";
                this.comboBoxBank.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_BANK_LIST", m_bankRecord.bankID);
                comboBoxPaymentObject_SelectedIndexChanged(this.comboBoxPaymentObject, null);

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

                this.textBoxPaymentOrderNumber.Text = m_cashRecord.billNumber;
                this.textBoxVouchersNumber.Text = m_cashRecord.vouchersNumber;
                this.dateTime.Text = m_cashRecord.tradingDate;

                this.comboBoxPaymentType.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_PAYMENT_TYPE_LIST", m_cashRecord.billTypeID);
                comboBoxPaymentType_SelectedIndexChanged(this.comboBoxPaymentType, null);

                if (this.comboBoxPaymentType.Text.IndexOf("采购入库") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_cashRecord.sourceBillNumber;
                }
                else if (this.comboBoxPaymentType.Text.IndexOf("应付") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_cashRecord.exchangesUnitName;
                }
                else if (this.comboBoxPaymentType.Text.IndexOf("其他") != -1 || this.comboBoxPaymentType.Text.IndexOf("其它") != -1)
                {
                    this.textBoxSourceOrderNumber.Text = m_cashRecord.note;
                }
                this.comboBoxPaymentObject.Text = "库存现金";
                this.textBoxTransactionAmount.Text = Convert.ToString(m_cashRecord.turnover);
                this.textBoxMakeOrderStaff.Text = m_cashRecord.makeOrderStaffName;
                this.textBoxOrderReview.Text = m_cashRecord.orderReviewName;
            }
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

        private void setAllActiveEnableAttribute(bool value)
        {
            this.save.Enabled = value;
            this.textBoxSourceOrderNumber.Enabled = value;
            this.toolStripButtonReview.Enabled = value;
            this.selectSourceOrder.Enabled = value;
            this.textBoxVouchersNumber.Enabled = value;
            this.dateTime.Enabled = value;
            this.comboBoxPaymentType.Enabled = value;
            this.textBoxSourceOrderNumber.Enabled = value;
            this.comboBoxPaymentObject.Enabled = value;
            this.textBoxTransactionAmount.Enabled = value;
            this.comboBoxBank.Enabled = value;
            this.panelIsReview.Visible = !value;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(502);

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