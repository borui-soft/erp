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
    public partial class FormStorage : Form
    {
        public FormStorage()
        {
            InitializeComponent();
        }

        #region 鼠标滑过事件
        private void panelStorageBuyIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageBuyIn);
        }

        private void panelStorageProductsIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageProductsIn);
        }

        private void panelStorageEarningsIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageEarningsIn);
        }

        private void panelStorageOtherIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageOtherIn);
        }

        private void panelStorageAllocate_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageAllocate);
        }

        private void panelStorageInventory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageInventory);
        }

        private void panelStorageAssembly_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageAssembly);
        }

        private void panelStorageSaleOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageSaleOut);
        }

        private void panelStorageProductionOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageProductionOut);
        }

        private void panelStorageLossesOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageLossesOut);
        }

        private void panelStorageOtherOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelStorageOtherOut);
        }
        #endregion

        private void panelStorageBuyIn_Click(object sender, EventArgs e)
        {
            FormPurchaseInOrder fpio = new FormPurchaseInOrder();
            fpio.ShowDialog();
        }

        private void panelStorageProductsIn_Click(object sender, EventArgs e)
        {
            FormMaterielInOrder fmoo = new FormMaterielInOrder();
            fmoo.ShowDialog();
        }

        private void panelStorageEarningsIn_Click(object sender, EventArgs e)
        {
            FormMaterielInEarningsOrder fmieo = new FormMaterielInEarningsOrder();
            fmieo.ShowDialog();
        }

        private void panelStorageOtherIn_Click(object sender, EventArgs e)
        {
            FormMaterielInOtherOrder fmieo = new FormMaterielInOtherOrder();
            fmieo.ShowDialog();
        }

        private void panelStorageAllocate_Click(object sender, EventArgs e)
        {

        }

        private void panelStorageInventory_Click(object sender, EventArgs e)
        {

        }

        private void panelStorageAssembly_Click(object sender, EventArgs e)
        {

        }

        private void panelStorageSaleOut_Click(object sender, EventArgs e)
        {
            FormSaleOutOrder fsoo = new FormSaleOutOrder();
            fsoo.ShowDialog();
        }

        private void panelStorageProductionOut_Click(object sender, EventArgs e)
        {
            FormMaterielOutOrder fmoo = new FormMaterielOutOrder();
            fmoo.ShowDialog();
        }

        private void panelStorageLossesOut_Click(object sender, EventArgs e)
        {
            FormMaterielOutEarningsOrder fmoeo = new FormMaterielOutEarningsOrder();
            fmoeo.ShowDialog();
        }

        private void panelStorageOtherOut_Click(object sender, EventArgs e)
        {
            FormMaterielOutOtherOrder fmooo = new FormMaterielOutOtherOrder();
            fmooo.ShowDialog();
        }
    }
}
