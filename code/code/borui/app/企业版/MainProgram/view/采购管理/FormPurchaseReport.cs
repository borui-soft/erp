using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormPurchaseReport : Form
    {
        public FormPurchaseReport()
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
            PanelExtend.setLableControlStyle(this.labelSupplier);
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
            FormBaseMateriel fbm = new FormBaseMateriel();
            fbm.ShowDialog();
        }

        private void labelSupplier_Click(object sender, EventArgs e)
        {
            FormBaseSupplier fbs = new FormBaseSupplier();
            fbs.ShowDialog();
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
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseOrder);
            fpos.ShowDialog();
        }

        private void labelPurchaseIn_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseIn);
            fpos.ShowDialog();
        }

        private void labelPurchaseInvoice_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseInvoice);
            fpos.ShowDialog();
        }

        private void labelInventory_Click(object sender, EventArgs e)
        {
            FormMaterielStorageAmountInfo fmc = new FormMaterielStorageAmountInfo((int)FormMaterielStorageAmountInfo.DisplayDataType.Materiel);
            fmc.ShowDialog();
        }

        private void labelPurchaseOrderExecute_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseOrderExcute);
            fpos.ShowDialog();
        }

        private void labelPurchaseInPayment_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseInOrderExcute);
            fpos.ShowDialog();
        }

        private void labelAmountCountByMateriel_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.PurchaseMateriel);
            fpic.ShowDialog();
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
            FormMaterielStorageAmountHistoryInfo fmc = new FormMaterielStorageAmountHistoryInfo((int)FormMaterielStorageAmountInfo.DisplayDataType.Materiel, true);
            fmc.ShowDialog();
        }

        private void labelInventoryHistory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryHistory);
        }

        private void labelPurchaseApplyOrder_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseApplyOrder);
            fpos.ShowDialog();
        }

        private void labelPurchaseApplyOrder_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseApplyOrder);
        }
    }
}
