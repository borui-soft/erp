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
    public partial class FormCashReport : Form
    {
        public FormCashReport()
        {
            InitializeComponent();
        }

        private void FormCashReport_Load(object sender, EventArgs e)
        {
            setPageActionEnable();
        }

        private void labelCashOutDetails_Click(object sender, EventArgs e)
        {
            FormReceivablerAndPaymentOrder frap = new FormReceivablerAndPaymentOrder(false);
            frap.ShowDialog();
        }

        private void labelCashOutCount_Click(object sender, EventArgs e)
        {
            FormReceivablerAndPaymentOrder frap = new FormReceivablerAndPaymentOrder(true);
            frap.ShowDialog();
        }

        private void labelCashInDetails_Click(object sender, EventArgs e)
        {
            FormBankCashDetail fard = new FormBankCashDetail("现金日记账", true);
            fard.ShowDialog();
        }

        private void labelCashInCount_Click(object sender, EventArgs e)
        {
            FormBankCashDetail fard = new FormBankCashDetail("银行存款日记账", false);
            fard.ShowDialog();
        }

        private void labelIn_Click(object sender, EventArgs e)
        {
            FormAccountReceivablerCount fard = new FormAccountReceivablerCount(true);
            fard.ShowDialog();
        }

        private void labelOut_Click(object sender, EventArgs e)
        {
            FormAccountReceivablerCount fard = new FormAccountReceivablerCount(false);
            fard.ShowDialog();
        }

        private void labelCompanyProfit_Click(object sender, EventArgs e)
        {
            FormCompanyProfit fcp = new FormCompanyProfit();
            fcp.ShowDialog();
            
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(504);

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
        #region 鼠标滑过事件
        private void labelCashOutDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashOutDetails);
        }

        private void labelCashOutCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashOutCount);
        }

        private void labelCashInDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashInDetails);
        }

        private void labelCashInCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCashInCount);
        }

        private void labelIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelIn);
        }

        private void labelOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelOut);
        }

        private void labelCompanyProfit_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCompanyProfit);
        }
        #endregion
    }
}
