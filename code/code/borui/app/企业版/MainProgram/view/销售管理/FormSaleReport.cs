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
    public partial class FormSaleReport : Form
    {
        public FormSaleReport()
        {
            InitializeComponent();
        }

        #region 鼠标滑过事件
        private void labelMateriel_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMateriel);
        }

        private void labelCustom_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCustom);
        }

        private void labelSaleBasePrice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleBasePrice);
        }

        private void SalePrice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.SalePrice);
        }

        private void labelSaleOrder_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleOrder);
        }

        private void labelSaleOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleOut);
        }

        private void labelSaleInvoice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleInvoice);
        }

        private void labelInventory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventory);
        }

        private void labelSaleOrderExecute_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleOrderExecute);
        }

        private void labelSaleIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleIn);
        }

        private void labelSaleCountByProducts_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleCountByProducts);
        }

        private void labelSaleCountByPeople_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleCountByPeople);
        }

        private void labelSaleCountByCustom_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleCountByCustom);
        }
        #endregion

        private void labelMateriel_Click(object sender, EventArgs e)
        {
            FormBaseMateriel fbm = new FormBaseMateriel();
            fbm.ShowDialog();
        }

        private void labelCustom_Click(object sender, EventArgs e)
        {
            FormBaseCustomer fbc = new FormBaseCustomer();
            fbc.ShowDialog();
        }

        private void labelSaleBasePrice_Click(object sender, EventArgs e)
        {
            FormMateirelPriceManager fphpc = new FormMateirelPriceManager(false, false);
            fphpc.ShowDialog();
        }

        // 销售报价序时簿
        private void SalePrice_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleQuotation);
            fphpc.ShowDialog();
        }

        private void labelSaleOrder_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOrder);
            fphpc.ShowDialog();
        }

        private void labelSaleOut_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOut);
            fphpc.ShowDialog();
        }

        private void labelSaleInvoice_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleInvoice);
            fphpc.ShowDialog();
        }

        private void labelInventory_Click(object sender, EventArgs e)
        {
            FormMaterielStorageAmountInfo fmc = new FormMaterielStorageAmountInfo((int)FormMaterielStorageAmountInfo.DisplayDataType.Product);
            fmc.ShowDialog();
        }

        private void labelSaleOrderExecute_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOrderExcute);
            fphpc.ShowDialog();
        }

        private void labelSaleIn_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOutOrderExcute);
            fphpc.ShowDialog();
        }

        private void labelSaleCountByProducts_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.SaleMateriel);
            fpic.ShowDialog();
        }

        private void labelSaleCountByPeople_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.SalePeople);
            fpic.ShowDialog();
        }

        private void labelSaleCountByCustom_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.SaleCustomer);
            fpic.ShowDialog();
        }

        private void labelInventoryHistory_Click(object sender, EventArgs e)
        {
            FormMaterielStorageAmountHistoryInfo fmc = new FormMaterielStorageAmountHistoryInfo((int)FormMaterielStorageAmountInfo.DisplayDataType.Product, true);
            fmc.ShowDialog();
        }

        private void labelInventoryHistory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryHistory);
        }
    }
}
