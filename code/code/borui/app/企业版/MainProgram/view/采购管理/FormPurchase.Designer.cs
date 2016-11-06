namespace MainProgram
{
    partial class FormPurchase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelPurchaseIn = new System.Windows.Forms.Panel();
            this.labelPurchaseIn = new System.Windows.Forms.Label();
            this.panelPurchaseInvoice = new System.Windows.Forms.Panel();
            this.labelPurchaseInvoice = new System.Windows.Forms.Label();
            this.panelPurchaseOrder = new System.Windows.Forms.Panel();
            this.labelPurchaseOrder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelPurchaseIn.SuspendLayout();
            this.panelPurchaseInvoice.SuspendLayout();
            this.panelPurchaseOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MainProgram.Properties.Resources.右箭头;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(457, 256);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(123, 23);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.右箭头;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(199, 256);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 23);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // panelPurchaseIn
            // 
            this.panelPurchaseIn.BackgroundImage = global::MainProgram.Properties.Resources.采购管理_采购入库;
            this.panelPurchaseIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPurchaseIn.Controls.Add(this.labelPurchaseIn);
            this.panelPurchaseIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelPurchaseIn.Location = new System.Drawing.Point(337, 215);
            this.panelPurchaseIn.Name = "panelPurchaseIn";
            this.panelPurchaseIn.Size = new System.Drawing.Size(103, 92);
            this.panelPurchaseIn.TabIndex = 19;
            this.panelPurchaseIn.Click += new System.EventHandler(this.panelPurchaseIn_Click);
            this.panelPurchaseIn.MouseEnter += new System.EventHandler(this.panelPurchaseIn_MouseEnter);
            this.panelPurchaseIn.MouseLeave += new System.EventHandler(this.panelPurchaseIn_MouseEnter);
            // 
            // labelPurchaseIn
            // 
            this.labelPurchaseIn.AutoSize = true;
            this.labelPurchaseIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseIn.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseIn.Location = new System.Drawing.Point(16, 74);
            this.labelPurchaseIn.Name = "labelPurchaseIn";
            this.labelPurchaseIn.Size = new System.Drawing.Size(63, 14);
            this.labelPurchaseIn.TabIndex = 0;
            this.labelPurchaseIn.Text = "采购入库";
            this.labelPurchaseIn.Click += new System.EventHandler(this.panelPurchaseIn_Click);
            this.labelPurchaseIn.MouseEnter += new System.EventHandler(this.panelPurchaseIn_MouseEnter);
            this.labelPurchaseIn.MouseLeave += new System.EventHandler(this.panelPurchaseIn_MouseEnter);
            // 
            // panelPurchaseInvoice
            // 
            this.panelPurchaseInvoice.BackgroundImage = global::MainProgram.Properties.Resources.采购管理_采购发票;
            this.panelPurchaseInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPurchaseInvoice.Controls.Add(this.labelPurchaseInvoice);
            this.panelPurchaseInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelPurchaseInvoice.Location = new System.Drawing.Point(601, 215);
            this.panelPurchaseInvoice.Name = "panelPurchaseInvoice";
            this.panelPurchaseInvoice.Size = new System.Drawing.Size(103, 92);
            this.panelPurchaseInvoice.TabIndex = 20;
            this.panelPurchaseInvoice.Click += new System.EventHandler(this.panelPurchaseInvoice_Click);
            this.panelPurchaseInvoice.MouseEnter += new System.EventHandler(this.panelPurchaseInvoice_MouseEnter);
            this.panelPurchaseInvoice.MouseLeave += new System.EventHandler(this.panelPurchaseInvoice_MouseEnter);
            // 
            // labelPurchaseInvoice
            // 
            this.labelPurchaseInvoice.AutoSize = true;
            this.labelPurchaseInvoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseInvoice.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseInvoice.Location = new System.Drawing.Point(22, 74);
            this.labelPurchaseInvoice.Name = "labelPurchaseInvoice";
            this.labelPurchaseInvoice.Size = new System.Drawing.Size(63, 14);
            this.labelPurchaseInvoice.TabIndex = 0;
            this.labelPurchaseInvoice.Text = "采购发票";
            this.labelPurchaseInvoice.Click += new System.EventHandler(this.panelPurchaseInvoice_Click);
            this.labelPurchaseInvoice.MouseEnter += new System.EventHandler(this.panelPurchaseInvoice_MouseEnter);
            this.labelPurchaseInvoice.MouseLeave += new System.EventHandler(this.panelPurchaseInvoice_MouseEnter);
            // 
            // panelPurchaseOrder
            // 
            this.panelPurchaseOrder.BackgroundImage = global::MainProgram.Properties.Resources.采购管理_采购订单;
            this.panelPurchaseOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPurchaseOrder.Controls.Add(this.labelPurchaseOrder);
            this.panelPurchaseOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelPurchaseOrder.Location = new System.Drawing.Point(81, 215);
            this.panelPurchaseOrder.Name = "panelPurchaseOrder";
            this.panelPurchaseOrder.Size = new System.Drawing.Size(103, 92);
            this.panelPurchaseOrder.TabIndex = 18;
            this.panelPurchaseOrder.Click += new System.EventHandler(this.panelPurchaseOrder_Click);
            this.panelPurchaseOrder.MouseEnter += new System.EventHandler(this.panelPurchaseOrder_MouseEnter);
            this.panelPurchaseOrder.MouseLeave += new System.EventHandler(this.panelPurchaseOrder_MouseEnter);
            // 
            // labelPurchaseOrder
            // 
            this.labelPurchaseOrder.AutoSize = true;
            this.labelPurchaseOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseOrder.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseOrder.Location = new System.Drawing.Point(19, 74);
            this.labelPurchaseOrder.Name = "labelPurchaseOrder";
            this.labelPurchaseOrder.Size = new System.Drawing.Size(63, 14);
            this.labelPurchaseOrder.TabIndex = 0;
            this.labelPurchaseOrder.Text = "采购订单";
            this.labelPurchaseOrder.Click += new System.EventHandler(this.panelPurchaseOrder_Click);
            this.labelPurchaseOrder.MouseEnter += new System.EventHandler(this.panelPurchaseOrder_MouseEnter);
            this.labelPurchaseOrder.MouseLeave += new System.EventHandler(this.panelPurchaseOrder_MouseEnter);
            // 
            // FormPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelPurchaseIn);
            this.Controls.Add(this.panelPurchaseInvoice);
            this.Controls.Add(this.panelPurchaseOrder);
            this.Name = "FormPurchase";
            this.Text = "采购管理";
            this.Load += new System.EventHandler(this.FormPurchase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelPurchaseIn.ResumeLayout(false);
            this.panelPurchaseIn.PerformLayout();
            this.panelPurchaseInvoice.ResumeLayout(false);
            this.panelPurchaseInvoice.PerformLayout();
            this.panelPurchaseOrder.ResumeLayout(false);
            this.panelPurchaseOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelPurchaseOrder;
        private System.Windows.Forms.Label labelPurchaseIn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelPurchaseIn;
        private System.Windows.Forms.Label labelPurchaseInvoice;
        private System.Windows.Forms.Panel panelPurchaseInvoice;
        private System.Windows.Forms.Panel panelPurchaseOrder;
    }
}