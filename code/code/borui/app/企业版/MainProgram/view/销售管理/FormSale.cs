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
    public partial class FormSale : Form
    {
        public FormSale()
        {
            InitializeComponent();
        }

        private void panelSalePrice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSalePrice);
        }

        private void panelSaleOrder_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleOrder);
        }

        private void panelSaleOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleOut);
        }

        private void panelSaleInvoice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleInvoice);
        }

        private void panelSalePrice_Click(object sender, EventArgs e)
        {
            FormSaleQuotationOrder fsqo = new FormSaleQuotationOrder();
            fsqo.ShowDialog();
        }

        private void panelSaleOrder_Click(object sender, EventArgs e)
        {
            FormSaleOrder fso = new FormSaleOrder();
            fso.ShowDialog();
        }

        private void panelSaleOut_Click(object sender, EventArgs e)
        {
            FormSaleOutOrder fsoo = new FormSaleOutOrder();
            fsoo.ShowDialog();
        }

        private void panelSaleInvoice_Click(object sender, EventArgs e)
        {

        }
    }
}
