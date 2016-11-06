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

namespace MainProgram
{
    public partial class FormCash : Form
    {
        public FormCash()
        {
            InitializeComponent();
        }

        private void FormCash_Load(object sender, EventArgs e)
        {
            setPageActionEnable();
        }

        private void panelCashOrder_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashOrder);
        }

        private void panelCashIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashIn);
        }

        private void panelCashInvoice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashInvoice);
        }

        private void panelCashOrder_Click(object sender, EventArgs e)
        {
            FormPaymentOrder fpo = new FormPaymentOrder();
            fpo.ShowDialog();
        }

        private void panelCashIn_Click(object sender, EventArgs e)
        {
            FormReceivableOrder fro = new FormReceivableOrder();
            fro.ShowDialog();
        }

        private void panelCashInvoice_Click(object sender, EventArgs e)
        {
            /*函数实现逻辑
             *1、检查系统中是否存在未审核的（采购入库(赊购)，销售出库(赊购)，付款单，收款单）
             *2、得到当前系统记录的现金余额，更新cash_balance_last_month表
             *3、得到当前系统记录的各银行存款余额，更新bank_balance_last_month表
             *4、计算企业当前库存现金、银行存款、应收账款合计、应付账款合计、存货金额合计信息到企业利润表
             *4、提示用户是否保存成功，如果保存失败，提示保存失败原因
            */
            if (DbPublic.getInctance().isCheckOut())
            {
                MessageBoxExtend.messageWarning("当前会计期间已经执行结转损益，不能重复执行！");
                return;
            }

            string msg = "建议执行结转损益操作放在每个会计期间的最后一天，结转损益执行完毕后，本会计期间无法再审核新单据。\n";
            msg += "请确认当前系统不存在未审核的以下单据:\n";
            msg += "1、采购入库(赊购)\n";
            msg += "2、销售出库(赊购)\n";
            msg += "3、付款单\n";
            msg += "4、收款单\n";
            msg += "确定执行结转吗?";
            if(MessageBoxExtend.messageQuestion(msg))
            {
                try
                {
                    // 现金余额结转
                    double cachBalance = BalanceManager.getCachBalance();
                    CashBalanceLastMonth.getInctance().insert(cachBalance, "结转余额");

                    // 银行存款余额结转
                    SortedDictionary<int, AuxiliaryMaterialDataTable> bankList
                        = AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData("BASE_BANK_LIST");

                    foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in bankList)
                    {
                        double bankBalance = BalanceManager.getBankBalance(index.Value.name);
                        BankBalanceLastMonth.getInctance().insert(index.Value.pkey, bankBalance, "结转余额");
                    }

                    // 企业利润信息
                    CompanyProfit.getInctance().insertCashInvoiceData();

                    OperatorLog.getInctance().insert(503, "执行结转损益.");
                }
                catch (Exception exp)
                {
                    MessageBoxExtend.messageWarning(exp.ToString());
                }
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(503);

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
