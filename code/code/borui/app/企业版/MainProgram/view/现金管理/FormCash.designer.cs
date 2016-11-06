namespace MainProgram
{
    partial class FormCash
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
            this.panelCashInvoice = new System.Windows.Forms.Panel();
            this.labelCashInvoice = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelCashIn = new System.Windows.Forms.Panel();
            this.labelCashIn = new System.Windows.Forms.Label();
            this.panelCashOrder = new System.Windows.Forms.Panel();
            this.labelCashOrder = new System.Windows.Forms.Label();
            this.panelCashInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelCashIn.SuspendLayout();
            this.panelCashOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCashInvoice
            // 
            this.panelCashInvoice.BackgroundImage = global::MainProgram.Properties.Resources.现金管理_结转损益;
            this.panelCashInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelCashInvoice.Controls.Add(this.labelCashInvoice);
            this.panelCashInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCashInvoice.Location = new System.Drawing.Point(323, 319);
            this.panelCashInvoice.Name = "panelCashInvoice";
            this.panelCashInvoice.Size = new System.Drawing.Size(103, 92);
            this.panelCashInvoice.TabIndex = 20;
            this.panelCashInvoice.Click += new System.EventHandler(this.panelCashInvoice_Click);
            this.panelCashInvoice.MouseEnter += new System.EventHandler(this.panelCashInvoice_MouseEnter);
            this.panelCashInvoice.MouseLeave += new System.EventHandler(this.panelCashInvoice_MouseEnter);
            // 
            // labelCashInvoice
            // 
            this.labelCashInvoice.AutoSize = true;
            this.labelCashInvoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashInvoice.ForeColor = System.Drawing.Color.Black;
            this.labelCashInvoice.Location = new System.Drawing.Point(19, 74);
            this.labelCashInvoice.Name = "labelCashInvoice";
            this.labelCashInvoice.Size = new System.Drawing.Size(63, 14);
            this.labelCashInvoice.TabIndex = 0;
            this.labelCashInvoice.Text = "结转损益";
            this.labelCashInvoice.Click += new System.EventHandler(this.panelCashInvoice_Click);
            this.labelCashInvoice.MouseEnter += new System.EventHandler(this.panelCashInvoice_MouseEnter);
            this.labelCashInvoice.MouseLeave += new System.EventHandler(this.panelCashInvoice_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.存货核算_箭头;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(128, 232);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(509, 110);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // panelCashIn
            // 
            this.panelCashIn.BackgroundImage = global::MainProgram.Properties.Resources.现金管理_收款单;
            this.panelCashIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelCashIn.Controls.Add(this.labelCashIn);
            this.panelCashIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCashIn.Location = new System.Drawing.Point(81, 141);
            this.panelCashIn.Name = "panelCashIn";
            this.panelCashIn.Size = new System.Drawing.Size(103, 92);
            this.panelCashIn.TabIndex = 19;
            this.panelCashIn.Click += new System.EventHandler(this.panelCashIn_Click);
            this.panelCashIn.MouseEnter += new System.EventHandler(this.panelCashIn_MouseEnter);
            this.panelCashIn.MouseLeave += new System.EventHandler(this.panelCashIn_MouseEnter);
            // 
            // labelCashIn
            // 
            this.labelCashIn.AutoSize = true;
            this.labelCashIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashIn.ForeColor = System.Drawing.Color.Black;
            this.labelCashIn.Location = new System.Drawing.Point(28, 74);
            this.labelCashIn.Name = "labelCashIn";
            this.labelCashIn.Size = new System.Drawing.Size(49, 14);
            this.labelCashIn.TabIndex = 0;
            this.labelCashIn.Text = "收款单";
            this.labelCashIn.Click += new System.EventHandler(this.panelCashIn_Click);
            this.labelCashIn.MouseEnter += new System.EventHandler(this.panelCashIn_MouseEnter);
            this.labelCashIn.MouseLeave += new System.EventHandler(this.panelCashIn_MouseEnter);
            // 
            // panelCashOrder
            // 
            this.panelCashOrder.BackgroundImage = global::MainProgram.Properties.Resources.现金管理_付款单;
            this.panelCashOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelCashOrder.Controls.Add(this.labelCashOrder);
            this.panelCashOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCashOrder.Location = new System.Drawing.Point(558, 141);
            this.panelCashOrder.Name = "panelCashOrder";
            this.panelCashOrder.Size = new System.Drawing.Size(103, 92);
            this.panelCashOrder.TabIndex = 18;
            this.panelCashOrder.Click += new System.EventHandler(this.panelCashOrder_Click);
            this.panelCashOrder.MouseEnter += new System.EventHandler(this.panelCashOrder_MouseEnter);
            this.panelCashOrder.MouseLeave += new System.EventHandler(this.panelCashOrder_MouseEnter);
            // 
            // labelCashOrder
            // 
            this.labelCashOrder.AutoSize = true;
            this.labelCashOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashOrder.ForeColor = System.Drawing.Color.Black;
            this.labelCashOrder.Location = new System.Drawing.Point(31, 74);
            this.labelCashOrder.Name = "labelCashOrder";
            this.labelCashOrder.Size = new System.Drawing.Size(49, 14);
            this.labelCashOrder.TabIndex = 0;
            this.labelCashOrder.Text = "付款单";
            this.labelCashOrder.Click += new System.EventHandler(this.panelCashOrder_Click);
            this.labelCashOrder.MouseEnter += new System.EventHandler(this.panelCashOrder_MouseEnter);
            this.labelCashOrder.MouseLeave += new System.EventHandler(this.panelCashOrder_MouseEnter);
            // 
            // FormCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panelCashIn);
            this.Controls.Add(this.panelCashOrder);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelCashInvoice);
            this.Name = "FormCash";
            this.Text = "现金管理";
            this.Load += new System.EventHandler(this.FormCash_Load);
            this.panelCashInvoice.ResumeLayout(false);
            this.panelCashInvoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelCashIn.ResumeLayout(false);
            this.panelCashIn.PerformLayout();
            this.panelCashOrder.ResumeLayout(false);
            this.panelCashOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCashOrder;
        private System.Windows.Forms.Label labelCashIn;
        private System.Windows.Forms.Panel panelCashIn;
        private System.Windows.Forms.Label labelCashInvoice;
        private System.Windows.Forms.Panel panelCashInvoice;
        private System.Windows.Forms.Panel panelCashOrder;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}