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
    public partial class FormProductionReport : Form
    {
        public FormProductionReport()
        {
            InitializeComponent();
        }

        #region 鼠标滑过事件
        private void labelMateriel_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMateriel);
        }

        private void labelSupplier_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelProjectInfo);
        }

        private void labelPurchasePirce_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchasePirce);
        }

        private void labelPurchaseOrder_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseOrder);
        }

        private void labelPurchaseIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseIn);
        }

        private void labelPurchaseInvoice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseInvoice);
        }

        private void labelInventory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventory);
        }

        private void labelPurchaseOrderExecute_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseOrderExecute);
        }

        private void labelPurchaseInPayment_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseInPayment);
        }

        private void labelAmountCountByMateriel_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelAmountCountByMateriel);
        }

        private void labelAmountCountByPeople_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelAmountCountByPeople);
        }

        private void labelAmountCountBySupplier_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelAmountCountBySupplier);
        }

        private void labelPurchasePirceHistory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchasePirceHistory);
        }
        #endregion

        private void labelMateriel_Click(object sender, EventArgs e)
        {
            MessageBoxExtend.messageOK("功能研发中...");
        }

        private void labelSupplier_Click(object sender, EventArgs e)
        {
            MessageBoxExtend.messageOK("功能研发中...");
        }

        private void labelPurchasePirce_Click(object sender, EventArgs e)
        {
            FormMateirelPriceManager fphpc = new FormMateirelPriceManager(false, true);
            fphpc.ShowDialog();
        }

        private void labelPurchasePirceHistory_Click(object sender, EventArgs e)
        {
            FormMateirelPriceManager fphpc = new FormMateirelPriceManager(true, true);
            fphpc.ShowDialog();
        }

        private void labelPurchaseOrder_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.DevMaterielInfo);
            fpos.ShowDialog();
        }

        private void labelPurchaseIn_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.EleMaterielInfo);
            fpos.ShowDialog();
        }

        private void labelPurchaseInvoice_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.EngMaterielInfo);
            fpos.ShowDialog();
        }

        private void labelInventory_Click(object sender, EventArgs e)
        {
            FormProjectInfoTrack fptt = new FormProjectInfoTrack(FormProjectInfoTrack.OrderType.ALL);
            fptt.ShowDialog();
        }

        private void labelPurchaseOrderExecute_Click(object sender, EventArgs e)
        {
            FormProjectInfoTrack fptt = new FormProjectInfoTrack(FormProjectInfoTrack.OrderType.EleMaterielInfo);
            fptt.ShowDialog();
        }

        private void labelPurchaseInPayment_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseInOrderExcute);
            fpos.ShowDialog();
        }

        private void labelAmountCountByMateriel_Click(object sender, EventArgs e)
        {
            FormProjectInfoTrack fptt = new FormProjectInfoTrack(FormProjectInfoTrack.OrderType.EngMaterielInfo);
            fptt.ShowDialog();
        }

        private void labelAmountCountByPeople_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.PurchasePeople);
            fpic.ShowDialog();
        }

        private void labelAmountCountBySupplier_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.PurchaseSupplier);
            fpic.ShowDialog();
        }

        private void labelInventoryHistory_Click(object sender, EventArgs e)
        {
            FormProjectInfoTrack fptt = new FormProjectInfoTrack((int)FormProjectInfoTrack.OrderType.DevMaterielInfo);
            fptt.ShowDialog();
        }

        private void labelInventoryHistory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryHistory);
        }

        private void setPageActionEnable(int authID)
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(authID);

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

        private void FormProductionReport_Load(object sender, EventArgs e)
        {
            setPageActionEnable(804);
            setPageActionEnable(805);
        }
    }
}
