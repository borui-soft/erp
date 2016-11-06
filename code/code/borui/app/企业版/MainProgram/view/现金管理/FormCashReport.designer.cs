namespace MainProgram
{
    partial class FormCashReport
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
            this.labelCashInDetails = new System.Windows.Forms.Label();
            this.labelCashOutDetails = new System.Windows.Forms.Label();
            this.labelCashOutCount = new System.Windows.Forms.Label();
            this.labelCashInCount = new System.Windows.Forms.Label();
            this.labelOut = new System.Windows.Forms.Label();
            this.labelIn = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.labelCompanyProfit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCashInDetails
            // 
            this.labelCashInDetails.AutoSize = true;
            this.labelCashInDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCashInDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashInDetails.ForeColor = System.Drawing.Color.Black;
            this.labelCashInDetails.Location = new System.Drawing.Point(63, 91);
            this.labelCashInDetails.Name = "labelCashInDetails";
            this.labelCashInDetails.Size = new System.Drawing.Size(77, 14);
            this.labelCashInDetails.TabIndex = 21;
            this.labelCashInDetails.Text = "现金日记账";
            this.labelCashInDetails.Click += new System.EventHandler(this.labelCashInDetails_Click);
            this.labelCashInDetails.MouseEnter += new System.EventHandler(this.labelCashInDetails_MouseEnter);
            this.labelCashInDetails.MouseLeave += new System.EventHandler(this.labelCashInDetails_MouseEnter);
            // 
            // labelCashOutDetails
            // 
            this.labelCashOutDetails.AutoSize = true;
            this.labelCashOutDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCashOutDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashOutDetails.ForeColor = System.Drawing.Color.Black;
            this.labelCashOutDetails.Location = new System.Drawing.Point(63, 67);
            this.labelCashOutDetails.Name = "labelCashOutDetails";
            this.labelCashOutDetails.Size = new System.Drawing.Size(91, 14);
            this.labelCashOutDetails.TabIndex = 20;
            this.labelCashOutDetails.Text = "付款单明细薄";
            this.labelCashOutDetails.Click += new System.EventHandler(this.labelCashOutDetails_Click);
            this.labelCashOutDetails.MouseEnter += new System.EventHandler(this.labelCashOutDetails_MouseEnter);
            this.labelCashOutDetails.MouseLeave += new System.EventHandler(this.labelCashOutDetails_MouseEnter);
            // 
            // labelCashOutCount
            // 
            this.labelCashOutCount.AutoSize = true;
            this.labelCashOutCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCashOutCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashOutCount.ForeColor = System.Drawing.Color.Black;
            this.labelCashOutCount.Location = new System.Drawing.Point(63, 43);
            this.labelCashOutCount.Name = "labelCashOutCount";
            this.labelCashOutCount.Size = new System.Drawing.Size(91, 14);
            this.labelCashOutCount.TabIndex = 19;
            this.labelCashOutCount.Text = "收款单明细薄";
            this.labelCashOutCount.Click += new System.EventHandler(this.labelCashOutCount_Click);
            this.labelCashOutCount.MouseEnter += new System.EventHandler(this.labelCashOutCount_MouseEnter);
            this.labelCashOutCount.MouseLeave += new System.EventHandler(this.labelCashOutCount_MouseEnter);
            // 
            // labelCashInCount
            // 
            this.labelCashInCount.AutoSize = true;
            this.labelCashInCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCashInCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCashInCount.ForeColor = System.Drawing.Color.Black;
            this.labelCashInCount.Location = new System.Drawing.Point(63, 115);
            this.labelCashInCount.Name = "labelCashInCount";
            this.labelCashInCount.Size = new System.Drawing.Size(105, 14);
            this.labelCashInCount.TabIndex = 22;
            this.labelCashInCount.Text = "银行存款日记账";
            this.labelCashInCount.Click += new System.EventHandler(this.labelCashInCount_Click);
            this.labelCashInCount.MouseEnter += new System.EventHandler(this.labelCashInCount_MouseEnter);
            this.labelCashInCount.MouseLeave += new System.EventHandler(this.labelCashInCount_MouseEnter);
            // 
            // labelOut
            // 
            this.labelOut.AutoSize = true;
            this.labelOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOut.ForeColor = System.Drawing.Color.Black;
            this.labelOut.Location = new System.Drawing.Point(63, 163);
            this.labelOut.Name = "labelOut";
            this.labelOut.Size = new System.Drawing.Size(105, 14);
            this.labelOut.TabIndex = 24;
            this.labelOut.Text = "应付账款明细薄";
            this.labelOut.Click += new System.EventHandler(this.labelOut_Click);
            this.labelOut.MouseEnter += new System.EventHandler(this.labelOut_MouseEnter);
            this.labelOut.MouseLeave += new System.EventHandler(this.labelOut_MouseEnter);
            // 
            // labelIn
            // 
            this.labelIn.AutoSize = true;
            this.labelIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIn.ForeColor = System.Drawing.Color.Black;
            this.labelIn.Location = new System.Drawing.Point(63, 139);
            this.labelIn.Name = "labelIn";
            this.labelIn.Size = new System.Drawing.Size(105, 14);
            this.labelIn.TabIndex = 23;
            this.labelIn.Text = "应收账款明细薄";
            this.labelIn.Click += new System.EventHandler(this.labelIn_Click);
            this.labelIn.MouseEnter += new System.EventHandler(this.labelIn_MouseEnter);
            this.labelIn.MouseLeave += new System.EventHandler(this.labelIn_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::MainProgram.Properties.Resources.报表_数据统计;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(10, 40);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 48);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // labelCompanyProfit
            // 
            this.labelCompanyProfit.AutoSize = true;
            this.labelCompanyProfit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCompanyProfit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCompanyProfit.ForeColor = System.Drawing.Color.Black;
            this.labelCompanyProfit.Location = new System.Drawing.Point(63, 187);
            this.labelCompanyProfit.Name = "labelCompanyProfit";
            this.labelCompanyProfit.Size = new System.Drawing.Size(91, 14);
            this.labelCompanyProfit.TabIndex = 25;
            this.labelCompanyProfit.Text = "企业利润汇总";
            this.labelCompanyProfit.Click += new System.EventHandler(this.labelCompanyProfit_Click);
            this.labelCompanyProfit.MouseEnter += new System.EventHandler(this.labelCompanyProfit_MouseEnter);
            this.labelCompanyProfit.MouseLeave += new System.EventHandler(this.labelCompanyProfit_MouseEnter);
            // 
            // FormCashReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(219, 562);
            this.Controls.Add(this.labelCompanyProfit);
            this.Controls.Add(this.labelOut);
            this.Controls.Add(this.labelIn);
            this.Controls.Add(this.labelCashInCount);
            this.Controls.Add(this.labelCashInDetails);
            this.Controls.Add(this.labelCashOutDetails);
            this.Controls.Add(this.labelCashOutCount);
            this.Controls.Add(this.pictureBox3);
            this.Name = "FormCashReport";
            this.Text = "现金管理报表";
            this.Load += new System.EventHandler(this.FormCashReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCashInDetails;
        private System.Windows.Forms.Label labelCashOutDetails;
        private System.Windows.Forms.Label labelCashOutCount;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label labelCashInCount;
        private System.Windows.Forms.Label labelOut;
        private System.Windows.Forms.Label labelIn;
        private System.Windows.Forms.Label labelCompanyProfit;

    }
}