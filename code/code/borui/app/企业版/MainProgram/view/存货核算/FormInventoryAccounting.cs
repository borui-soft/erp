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
    public partial class FormInventoryAccounting : Form
    {
        FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();

        public FormInventoryAccounting()
        {
            InitializeComponent();
        }

        private void FormInventoryAccounting_Load(object sender, EventArgs e)
        {
            DateTime nowDate = DateTime.Now;
            DateTime currentMonthFirstDay = new DateTime(nowDate.Year, nowDate.Month, 1);
            DateTime currentMonthLastDay = currentMonthFirstDay.AddMonths(1).AddDays(-1);

            // 显示当前会计期间，已审核的所有单据
            m_filter.startDate = currentMonthFirstDay.ToString("yyyy-MM-dd");
            m_filter.endDate = currentMonthLastDay.ToString("yyyy-MM-dd");
            m_filter.allReview = "0";
            m_filter.billColor = "2";
        }

        private void panelInventoryAccountingBuyIn_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseIn);
            fpos.ShowDialog();
        }

        private void panelInventoryAccountingProducts_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fphpc = new FormPurchaseOrderSequence(
                FormPurchaseOrderSequence.OrderType.StorageProductIn);
            fphpc.setDataFilter(m_filter);

            fphpc.ShowDialog();
        }

        private void panelInventoryAccountingOther_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fphpc = new FormPurchaseOrderSequence(
                FormPurchaseOrderSequence.OrderType.StorageInOther);
            fphpc.setDataFilter(m_filter);
            fphpc.ShowDialog();
        }

        private void panelInventoryAccountingOut_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOut);
            fphpc.ShowDialog();
        }

        #region 鼠标滑过事件
        private void panelInventoryAccountingBuyIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAccountingBuyIn);
        }

        private void panelInventoryAccountingOther_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAccountingOther);
        }

        private void panelInventoryAccountingProducts_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAccountingProducts);
        }

        private void panelInventoryAccountingOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAccountingOut);
        }
        #endregion
    }
}
