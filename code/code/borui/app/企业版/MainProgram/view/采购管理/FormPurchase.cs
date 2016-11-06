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
    public partial class FormPurchase : Form
    {
        public FormPurchase()
        {
            InitializeComponent();
        }

        private void FormPurchase_Load(object sender, EventArgs e)
        {

        }

        private void panelPurchaseOrder_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseOrder);
        }

        private void panelPurchaseIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseIn);
        }

        private void panelPurchaseInvoice_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseInvoice);
        }

        private void panelPurchaseOrder_Click(object sender, EventArgs e)
        {
            FormPurchaseOrder fpo = new FormPurchaseOrder();
            fpo.ShowDialog();
        }

        private void panelPurchaseIn_Click(object sender, EventArgs e)
        {
            FormPurchaseInOrder fpio = new FormPurchaseInOrder();
            fpio.ShowDialog();
        }

        private void panelPurchaseInvoice_Click(object sender, EventArgs e)
        {

        }
    }
}
