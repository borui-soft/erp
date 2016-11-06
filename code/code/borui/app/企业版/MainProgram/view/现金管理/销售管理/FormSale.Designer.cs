namespace MainProgram
{
    partial class FormSale
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
            this.panelSalePrice = new System.Windows.Forms.Panel();
            this.labelSalePrice = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSaleInvoice = new System.Windows.Forms.Panel();
            this.labelSaleInvoice = new System.Windows.Forms.Label();
            this.panelSaleOut = new System.Windows.Forms.Panel();
            this.labelSaleOut = new System.Windows.Forms.Label();
            this.panelSaleOrder = new System.Windows.Forms.Panel();
            this.labelSaleOrder = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panelSalePrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelSaleInvoice.SuspendLayout();
            this.panelSaleOut.SuspendLayout();
            this.panelSaleOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSalePrice
            // 
            this.panelSalePrice.BackgroundImage = global::MainProgram.Properties.Resources.销售管理_报价单;
            this.panelSalePrice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSalePrice.Controls.Add(this.labelSalePrice);
            this.panelSalePrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelSalePrice.Location = new System.Drawing.Point(80, 50);
            this.panelSalePrice.Name = "panelSalePrice";
            this.panelSalePrice.Size = new System.Drawing.Size(103, 92);
            this.panelSalePrice.TabIndex = 18;
            this.panelSalePrice.Click += new System.EventHandler(this.panelSalePrice_Click);
            this.panelSalePrice.MouseEnter += new System.EventHandler(this.panelSalePrice_MouseEnter);
            this.panelSalePrice.MouseLeave += new System.EventHandler(this.panelSalePrice_MouseEnter);
            // 
            // labelSalePrice
            // 
            this.labelSalePrice.AutoSize = true;
            this.labelSalePrice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSalePrice.ForeColor = System.Drawing.Color.Black;
            this.labelSalePrice.Location = new System.Drawing.Point(19, 74);
            this.labelSalePrice.Name = "labelSalePrice";
            this.labelSalePrice.Size = new System.Drawing.Size(63, 14);
            this.labelSalePrice.TabIndex = 0;
            this.labelSalePrice.Text = "销售报价";
            this.labelSalePrice.Click += new System.EventHandler(this.panelSalePrice_Click);
            this.labelSalePrice.MouseEnter += new System.EventHandler(this.panelSalePrice_MouseEnter);
            this.labelSalePrice.MouseLeave += new System.EventHandler(this.panelSalePrice_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MainProgram.Properties.Resources.右箭头;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(456, 315);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(123, 23);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.右箭头;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(198, 315);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 23);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // panelSaleInvoice
            // 
            this.panelSaleInvoice.BackgroundImage = global::MainProgram.Properties.Resources.销售管理_销售发票;
            this.panelSaleInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSaleInvoice.Controls.Add(this.labelSaleInvoice);
            this.panelSaleInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelSaleInvoice.Location = new System.Drawing.Point(600, 274);
            this.panelSaleInvoice.Name = "panelSaleInvoice";
            this.panelSaleInvoice.Size = new System.Drawing.Size(103, 92);
            this.panelSaleInvoice.TabIndex = 15;
            this.panelSaleInvoice.Click += new System.EventHandler(this.panelSaleInvoice_Click);
            this.panelSaleInvoice.MouseEnter += new System.EventHandler(this.panelSaleInvoice_MouseEnter);
            this.panelSaleInvoice.MouseLeave += new System.EventHandler(this.panelSaleInvoice_MouseEnter);
            // 
            // labelSaleInvoice
            // 
            this.labelSaleInvoice.AutoSize = true;
            this.labelSaleInvoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleInvoice.ForeColor = System.Drawing.Color.Black;
            this.labelSaleInvoice.Location = new System.Drawing.Point(16, 74);
            this.labelSaleInvoice.Name = "labelSaleInvoice";
            this.labelSaleInvoice.Size = new System.Drawing.Size(63, 14);
            this.labelSaleInvoice.TabIndex = 0;
            this.labelSaleInvoice.Text = "销售发票";
            this.labelSaleInvoice.Click += new System.EventHandler(this.panelSaleInvoice_Click);
            this.labelSaleInvoice.MouseEnter += new System.EventHandler(this.panelSaleInvoice_MouseEnter);
            this.labelSaleInvoice.MouseLeave += new System.EventHandler(this.panelSaleInvoice_MouseEnter);
            // 
            // panelSaleOut
            // 
            this.panelSaleOut.BackgroundImage = global::MainProgram.Properties.Resources.销售管理_销售出库;
            this.panelSaleOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSaleOut.Controls.Add(this.labelSaleOut);
            this.panelSaleOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelSaleOut.Location = new System.Drawing.Point(336, 274);
            this.panelSaleOut.Name = "panelSaleOut";
            this.panelSaleOut.Size = new System.Drawing.Size(103, 92);
            this.panelSaleOut.TabIndex = 14;
            this.panelSaleOut.Click += new System.EventHandler(this.panelSaleOut_Click);
            this.panelSaleOut.MouseEnter += new System.EventHandler(this.panelSaleOut_MouseEnter);
            this.panelSaleOut.MouseLeave += new System.EventHandler(this.panelSaleOut_MouseEnter);
            // 
            // labelSaleOut
            // 
            this.labelSaleOut.AutoSize = true;
            this.labelSaleOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleOut.ForeColor = System.Drawing.Color.Black;
            this.labelSaleOut.Location = new System.Drawing.Point(20, 74);
            this.labelSaleOut.Name = "labelSaleOut";
            this.labelSaleOut.Size = new System.Drawing.Size(63, 14);
            this.labelSaleOut.TabIndex = 0;
            this.labelSaleOut.Text = "销售出库";
            this.labelSaleOut.Click += new System.EventHandler(this.panelSaleOut_Click);
            this.labelSaleOut.MouseEnter += new System.EventHandler(this.panelSaleOut_MouseEnter);
            this.labelSaleOut.MouseLeave += new System.EventHandler(this.panelSaleOut_MouseEnter);
            // 
            // panelSaleOrder
            // 
            this.panelSaleOrder.BackgroundImage = global::MainProgram.Properties.Resources.销售管理_销售订单;
            this.panelSaleOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSaleOrder.Controls.Add(this.labelSaleOrder);
            this.panelSaleOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelSaleOrder.Location = new System.Drawing.Point(80, 274);
            this.panelSaleOrder.Name = "panelSaleOrder";
            this.panelSaleOrder.Size = new System.Drawing.Size(103, 92);
            this.panelSaleOrder.TabIndex = 13;
            this.panelSaleOrder.Click += new System.EventHandler(this.panelSaleOrder_Click);
            this.panelSaleOrder.MouseEnter += new System.EventHandler(this.panelSaleOrder_MouseEnter);
            this.panelSaleOrder.MouseLeave += new System.EventHandler(this.panelSaleOrder_MouseEnter);
            // 
            // labelSaleOrder
            // 
            this.labelSaleOrder.AutoSize = true;
            this.labelSaleOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleOrder.ForeColor = System.Drawing.Color.Black;
            this.labelSaleOrder.Location = new System.Drawing.Point(21, 74);
            this.labelSaleOrder.Name = "labelSaleOrder";
            this.labelSaleOrder.Size = new System.Drawing.Size(63, 14);
            this.labelSaleOrder.TabIndex = 0;
            this.labelSaleOrder.Text = "销售订单";
            this.labelSaleOrder.Click += new System.EventHandler(this.panelSaleOrder_Click);
            this.labelSaleOrder.MouseEnter += new System.EventHandler(this.panelSaleOrder_MouseEnter);
            this.labelSaleOrder.MouseLeave += new System.EventHandler(this.panelSaleOrder_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::MainProgram.Properties.Resources.下箭头_;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(116, 157);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 121);
            this.pictureBox3.TabIndex = 19;
            this.pictureBox3.TabStop = false;
            // 
            // FormSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panelSalePrice);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelSaleInvoice);
            this.Controls.Add(this.panelSaleOut);
            this.Controls.Add(this.panelSaleOrder);
            this.Controls.Add(this.pictureBox3);
            this.Name = "FormSale";
            this.Text = "销售管理";
            this.panelSalePrice.ResumeLayout(false);
            this.panelSalePrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelSaleInvoice.ResumeLayout(false);
            this.panelSaleInvoice.PerformLayout();
            this.panelSaleOut.ResumeLayout(false);
            this.panelSaleOut.PerformLayout();
            this.panelSaleOrder.ResumeLayout(false);
            this.panelSaleOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSaleOrder;
        private System.Windows.Forms.Label labelSaleOrder;
        private System.Windows.Forms.Panel panelSaleOut;
        private System.Windows.Forms.Label labelSaleOut;
        private System.Windows.Forms.Panel panelSaleInvoice;
        private System.Windows.Forms.Label labelSaleInvoice;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panelSalePrice;
        private System.Windows.Forms.Label labelSalePrice;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}