namespace MainProgram
{
    partial class FormInventoryAccounting
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelInventoryAccountingOut = new System.Windows.Forms.Panel();
            this.labelInventoryAccountingOut = new System.Windows.Forms.Label();
            this.panelInventoryAccountingProducts = new System.Windows.Forms.Panel();
            this.labelInventoryAccountingProducts = new System.Windows.Forms.Label();
            this.panelInventoryAccountingOther = new System.Windows.Forms.Panel();
            this.labelInventoryAccountingOther = new System.Windows.Forms.Label();
            this.panelInventoryAccountingBuyIn = new System.Windows.Forms.Panel();
            this.labelInventoryAccountingBuyIn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelInventoryAccountingOut.SuspendLayout();
            this.panelInventoryAccountingProducts.SuspendLayout();
            this.panelInventoryAccountingOther.SuspendLayout();
            this.panelInventoryAccountingBuyIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.存货核算_箭头;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(124, 239);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(509, 110);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // panelInventoryAccountingOut
            // 
            this.panelInventoryAccountingOut.BackgroundImage = global::MainProgram.Properties.Resources.存货核算_存货出库核算;
            this.panelInventoryAccountingOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelInventoryAccountingOut.Controls.Add(this.labelInventoryAccountingOut);
            this.panelInventoryAccountingOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelInventoryAccountingOut.Location = new System.Drawing.Point(315, 334);
            this.panelInventoryAccountingOut.Name = "panelInventoryAccountingOut";
            this.panelInventoryAccountingOut.Size = new System.Drawing.Size(103, 92);
            this.panelInventoryAccountingOut.TabIndex = 21;
            this.panelInventoryAccountingOut.Click += new System.EventHandler(this.panelInventoryAccountingOut_Click);
            this.panelInventoryAccountingOut.MouseEnter += new System.EventHandler(this.panelInventoryAccountingOut_MouseEnter);
            this.panelInventoryAccountingOut.MouseLeave += new System.EventHandler(this.panelInventoryAccountingOut_MouseEnter);
            // 
            // labelInventoryAccountingOut
            // 
            this.labelInventoryAccountingOut.AutoSize = true;
            this.labelInventoryAccountingOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAccountingOut.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAccountingOut.Location = new System.Drawing.Point(7, 74);
            this.labelInventoryAccountingOut.Name = "labelInventoryAccountingOut";
            this.labelInventoryAccountingOut.Size = new System.Drawing.Size(91, 14);
            this.labelInventoryAccountingOut.TabIndex = 0;
            this.labelInventoryAccountingOut.Text = "存货出库核算";
            this.labelInventoryAccountingOut.Click += new System.EventHandler(this.panelInventoryAccountingOut_Click);
            this.labelInventoryAccountingOut.MouseEnter += new System.EventHandler(this.panelInventoryAccountingOut_MouseEnter);
            this.labelInventoryAccountingOut.MouseLeave += new System.EventHandler(this.panelInventoryAccountingOut_MouseEnter);
            // 
            // panelInventoryAccountingProducts
            // 
            this.panelInventoryAccountingProducts.BackgroundImage = global::MainProgram.Properties.Resources.存货核算_自制入库核算;
            this.panelInventoryAccountingProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelInventoryAccountingProducts.Controls.Add(this.labelInventoryAccountingProducts);
            this.panelInventoryAccountingProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelInventoryAccountingProducts.Location = new System.Drawing.Point(324, 141);
            this.panelInventoryAccountingProducts.Name = "panelInventoryAccountingProducts";
            this.panelInventoryAccountingProducts.Size = new System.Drawing.Size(103, 92);
            this.panelInventoryAccountingProducts.TabIndex = 20;
            this.panelInventoryAccountingProducts.Click += new System.EventHandler(this.panelInventoryAccountingProducts_Click);
            this.panelInventoryAccountingProducts.MouseEnter += new System.EventHandler(this.panelInventoryAccountingProducts_MouseEnter);
            this.panelInventoryAccountingProducts.MouseLeave += new System.EventHandler(this.panelInventoryAccountingProducts_MouseEnter);
            // 
            // labelInventoryAccountingProducts
            // 
            this.labelInventoryAccountingProducts.AutoSize = true;
            this.labelInventoryAccountingProducts.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAccountingProducts.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAccountingProducts.Location = new System.Drawing.Point(5, 74);
            this.labelInventoryAccountingProducts.Name = "labelInventoryAccountingProducts";
            this.labelInventoryAccountingProducts.Size = new System.Drawing.Size(91, 14);
            this.labelInventoryAccountingProducts.TabIndex = 0;
            this.labelInventoryAccountingProducts.Text = "自制入库核算";
            this.labelInventoryAccountingProducts.Click += new System.EventHandler(this.panelInventoryAccountingProducts_Click);
            this.labelInventoryAccountingProducts.MouseEnter += new System.EventHandler(this.panelInventoryAccountingProducts_MouseEnter);
            this.labelInventoryAccountingProducts.MouseLeave += new System.EventHandler(this.panelInventoryAccountingProducts_MouseEnter);
            // 
            // panelInventoryAccountingOther
            // 
            this.panelInventoryAccountingOther.BackgroundImage = global::MainProgram.Properties.Resources.存货核算_其他入库核算;
            this.panelInventoryAccountingOther.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelInventoryAccountingOther.Controls.Add(this.labelInventoryAccountingOther);
            this.panelInventoryAccountingOther.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelInventoryAccountingOther.Location = new System.Drawing.Point(562, 141);
            this.panelInventoryAccountingOther.Name = "panelInventoryAccountingOther";
            this.panelInventoryAccountingOther.Size = new System.Drawing.Size(103, 92);
            this.panelInventoryAccountingOther.TabIndex = 20;
            this.panelInventoryAccountingOther.Click += new System.EventHandler(this.panelInventoryAccountingOther_Click);
            this.panelInventoryAccountingOther.MouseEnter += new System.EventHandler(this.panelInventoryAccountingOther_MouseEnter);
            this.panelInventoryAccountingOther.MouseLeave += new System.EventHandler(this.panelInventoryAccountingOther_MouseEnter);
            // 
            // labelInventoryAccountingOther
            // 
            this.labelInventoryAccountingOther.AutoSize = true;
            this.labelInventoryAccountingOther.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAccountingOther.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAccountingOther.Location = new System.Drawing.Point(8, 74);
            this.labelInventoryAccountingOther.Name = "labelInventoryAccountingOther";
            this.labelInventoryAccountingOther.Size = new System.Drawing.Size(91, 14);
            this.labelInventoryAccountingOther.TabIndex = 0;
            this.labelInventoryAccountingOther.Text = "其它入库核算";
            this.labelInventoryAccountingOther.Click += new System.EventHandler(this.panelInventoryAccountingOther_Click);
            this.labelInventoryAccountingOther.MouseEnter += new System.EventHandler(this.panelInventoryAccountingOther_MouseEnter);
            this.labelInventoryAccountingOther.MouseLeave += new System.EventHandler(this.panelInventoryAccountingOther_MouseEnter);
            // 
            // panelInventoryAccountingBuyIn
            // 
            this.panelInventoryAccountingBuyIn.BackgroundImage = global::MainProgram.Properties.Resources.存货核算_采购入库核算;
            this.panelInventoryAccountingBuyIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelInventoryAccountingBuyIn.Controls.Add(this.labelInventoryAccountingBuyIn);
            this.panelInventoryAccountingBuyIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelInventoryAccountingBuyIn.Location = new System.Drawing.Point(84, 141);
            this.panelInventoryAccountingBuyIn.Name = "panelInventoryAccountingBuyIn";
            this.panelInventoryAccountingBuyIn.Size = new System.Drawing.Size(103, 92);
            this.panelInventoryAccountingBuyIn.TabIndex = 19;
            this.panelInventoryAccountingBuyIn.Click += new System.EventHandler(this.panelInventoryAccountingBuyIn_Click);
            this.panelInventoryAccountingBuyIn.MouseEnter += new System.EventHandler(this.panelInventoryAccountingBuyIn_MouseEnter);
            this.panelInventoryAccountingBuyIn.MouseLeave += new System.EventHandler(this.panelInventoryAccountingBuyIn_MouseEnter);
            // 
            // labelInventoryAccountingBuyIn
            // 
            this.labelInventoryAccountingBuyIn.AutoSize = true;
            this.labelInventoryAccountingBuyIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAccountingBuyIn.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAccountingBuyIn.Location = new System.Drawing.Point(4, 74);
            this.labelInventoryAccountingBuyIn.Name = "labelInventoryAccountingBuyIn";
            this.labelInventoryAccountingBuyIn.Size = new System.Drawing.Size(91, 14);
            this.labelInventoryAccountingBuyIn.TabIndex = 0;
            this.labelInventoryAccountingBuyIn.Text = "外购入库核算";
            this.labelInventoryAccountingBuyIn.Click += new System.EventHandler(this.panelInventoryAccountingBuyIn_Click);
            this.labelInventoryAccountingBuyIn.MouseEnter += new System.EventHandler(this.panelInventoryAccountingBuyIn_MouseEnter);
            this.labelInventoryAccountingBuyIn.MouseLeave += new System.EventHandler(this.panelInventoryAccountingBuyIn_MouseEnter);
            // 
            // FormInventoryAccounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelInventoryAccountingOut);
            this.Controls.Add(this.panelInventoryAccountingProducts);
            this.Controls.Add(this.panelInventoryAccountingOther);
            this.Controls.Add(this.panelInventoryAccountingBuyIn);
            this.Name = "FormInventoryAccounting";
            this.Text = "存货核算";
            this.Load += new System.EventHandler(this.FormInventoryAccounting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelInventoryAccountingOut.ResumeLayout(false);
            this.panelInventoryAccountingOut.PerformLayout();
            this.panelInventoryAccountingProducts.ResumeLayout(false);
            this.panelInventoryAccountingProducts.PerformLayout();
            this.panelInventoryAccountingOther.ResumeLayout(false);
            this.panelInventoryAccountingOther.PerformLayout();
            this.panelInventoryAccountingBuyIn.ResumeLayout(false);
            this.panelInventoryAccountingBuyIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelInventoryAccountingBuyIn;
        private System.Windows.Forms.Label labelInventoryAccountingBuyIn;
        private System.Windows.Forms.Panel panelInventoryAccountingOther;
        private System.Windows.Forms.Label labelInventoryAccountingOther;
        private System.Windows.Forms.Panel panelInventoryAccountingProducts;
        private System.Windows.Forms.Label labelInventoryAccountingProducts;
        private System.Windows.Forms.Panel panelInventoryAccountingOut;
        private System.Windows.Forms.Label labelInventoryAccountingOut;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}