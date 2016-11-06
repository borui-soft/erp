namespace MainProgram
{
    partial class FormInventoryAccountingReport
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
            this.labelSaleProfitDetails = new System.Windows.Forms.Label();
            this.labelSaleProfitCount = new System.Windows.Forms.Label();
            this.labelSaleCostDetails = new System.Windows.Forms.Label();
            this.labelSaleCostCount = new System.Windows.Forms.Label();
            this.labelProductionOutDetails = new System.Windows.Forms.Label();
            this.labelProductionOutCount = new System.Windows.Forms.Label();
            this.labelPurchaseCostDetails = new System.Windows.Forms.Label();
            this.labelInventoryDetails = new System.Windows.Forms.Label();
            this.labelPurchaseCostCount = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.labelCost = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelMaterielInOutCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSaleProfitDetails
            // 
            this.labelSaleProfitDetails.AutoSize = true;
            this.labelSaleProfitDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleProfitDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleProfitDetails.ForeColor = System.Drawing.Color.Black;
            this.labelSaleProfitDetails.Location = new System.Drawing.Point(64, 330);
            this.labelSaleProfitDetails.Name = "labelSaleProfitDetails";
            this.labelSaleProfitDetails.Size = new System.Drawing.Size(105, 14);
            this.labelSaleProfitDetails.TabIndex = 70;
            this.labelSaleProfitDetails.Text = "销售毛利润明细";
            this.labelSaleProfitDetails.Click += new System.EventHandler(this.labelSaleProfitDetails_Click);
            this.labelSaleProfitDetails.MouseEnter += new System.EventHandler(this.labelSaleCostDetails_MouseEnter);
            this.labelSaleProfitDetails.MouseLeave += new System.EventHandler(this.labelSaleCostDetails_MouseEnter);
            // 
            // labelSaleProfitCount
            // 
            this.labelSaleProfitCount.AutoSize = true;
            this.labelSaleProfitCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleProfitCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleProfitCount.ForeColor = System.Drawing.Color.Black;
            this.labelSaleProfitCount.Location = new System.Drawing.Point(64, 306);
            this.labelSaleProfitCount.Name = "labelSaleProfitCount";
            this.labelSaleProfitCount.Size = new System.Drawing.Size(119, 14);
            this.labelSaleProfitCount.TabIndex = 69;
            this.labelSaleProfitCount.Text = "销售毛利润汇总表";
            this.labelSaleProfitCount.Click += new System.EventHandler(this.labelSaleProfitCount_Click);
            this.labelSaleProfitCount.MouseEnter += new System.EventHandler(this.labelSaleCostCount_MouseEnter);
            this.labelSaleProfitCount.MouseLeave += new System.EventHandler(this.labelSaleCostCount_MouseEnter);
            // 
            // labelSaleCostDetails
            // 
            this.labelSaleCostDetails.AutoSize = true;
            this.labelSaleCostDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleCostDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleCostDetails.ForeColor = System.Drawing.Color.Black;
            this.labelSaleCostDetails.Location = new System.Drawing.Point(63, 282);
            this.labelSaleCostDetails.Name = "labelSaleCostDetails";
            this.labelSaleCostDetails.Size = new System.Drawing.Size(105, 14);
            this.labelSaleCostDetails.TabIndex = 68;
            this.labelSaleCostDetails.Text = "销售成本明细表";
            this.labelSaleCostDetails.Click += new System.EventHandler(this.labelSaleCostDetails_Click);
            this.labelSaleCostDetails.MouseEnter += new System.EventHandler(this.labelProductionOutDetails_MouseEnter);
            this.labelSaleCostDetails.MouseLeave += new System.EventHandler(this.labelProductionOutDetails_MouseEnter);
            // 
            // labelSaleCostCount
            // 
            this.labelSaleCostCount.AutoSize = true;
            this.labelSaleCostCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleCostCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleCostCount.ForeColor = System.Drawing.Color.Black;
            this.labelSaleCostCount.Location = new System.Drawing.Point(63, 258);
            this.labelSaleCostCount.Name = "labelSaleCostCount";
            this.labelSaleCostCount.Size = new System.Drawing.Size(105, 14);
            this.labelSaleCostCount.TabIndex = 67;
            this.labelSaleCostCount.Text = "销售成本汇总表";
            this.labelSaleCostCount.Click += new System.EventHandler(this.labelSaleCostCount_Click);
            this.labelSaleCostCount.MouseEnter += new System.EventHandler(this.labelProductionOutCount_MouseEnter);
            this.labelSaleCostCount.MouseLeave += new System.EventHandler(this.labelProductionOutCount_MouseEnter);
            // 
            // labelProductionOutDetails
            // 
            this.labelProductionOutDetails.AutoSize = true;
            this.labelProductionOutDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProductionOutDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProductionOutDetails.ForeColor = System.Drawing.Color.Black;
            this.labelProductionOutDetails.Location = new System.Drawing.Point(63, 234);
            this.labelProductionOutDetails.Name = "labelProductionOutDetails";
            this.labelProductionOutDetails.Size = new System.Drawing.Size(133, 14);
            this.labelProductionOutDetails.TabIndex = 66;
            this.labelProductionOutDetails.Text = "生产领料成本明细表";
            this.labelProductionOutDetails.Click += new System.EventHandler(this.labelProductionOutDetails_Click);
            this.labelProductionOutDetails.MouseEnter += new System.EventHandler(this.labelInventoryAppraisalDetails_MouseEnter);
            this.labelProductionOutDetails.MouseLeave += new System.EventHandler(this.labelInventoryAppraisalDetails_MouseEnter);
            // 
            // labelProductionOutCount
            // 
            this.labelProductionOutCount.AutoSize = true;
            this.labelProductionOutCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProductionOutCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProductionOutCount.ForeColor = System.Drawing.Color.Black;
            this.labelProductionOutCount.Location = new System.Drawing.Point(63, 210);
            this.labelProductionOutCount.Name = "labelProductionOutCount";
            this.labelProductionOutCount.Size = new System.Drawing.Size(133, 14);
            this.labelProductionOutCount.TabIndex = 65;
            this.labelProductionOutCount.Text = "生产领料成本汇总表";
            this.labelProductionOutCount.Click += new System.EventHandler(this.labelProductionOutCount_Click);
            this.labelProductionOutCount.MouseEnter += new System.EventHandler(this.labelInventoryAppraisalCount_MouseEnter);
            this.labelProductionOutCount.MouseLeave += new System.EventHandler(this.labelInventoryAppraisalCount_MouseEnter);
            // 
            // labelPurchaseCostDetails
            // 
            this.labelPurchaseCostDetails.AutoSize = true;
            this.labelPurchaseCostDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseCostDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseCostDetails.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseCostDetails.Location = new System.Drawing.Point(63, 186);
            this.labelPurchaseCostDetails.Name = "labelPurchaseCostDetails";
            this.labelPurchaseCostDetails.Size = new System.Drawing.Size(105, 14);
            this.labelPurchaseCostDetails.TabIndex = 64;
            this.labelPurchaseCostDetails.Text = "采购成本明细表";
            this.labelPurchaseCostDetails.Click += new System.EventHandler(this.labelPurchaseCostDetails_Click);
            this.labelPurchaseCostDetails.MouseEnter += new System.EventHandler(this.labelPurchaseCostDetails_MouseEnter);
            this.labelPurchaseCostDetails.MouseLeave += new System.EventHandler(this.labelPurchaseCostDetails_MouseEnter);
            // 
            // labelInventoryDetails
            // 
            this.labelInventoryDetails.AutoSize = true;
            this.labelInventoryDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryDetails.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryDetails.Location = new System.Drawing.Point(63, 114);
            this.labelInventoryDetails.Name = "labelInventoryDetails";
            this.labelInventoryDetails.Size = new System.Drawing.Size(77, 14);
            this.labelInventoryDetails.TabIndex = 63;
            this.labelInventoryDetails.Text = "存货明细账";
            this.labelInventoryDetails.Click += new System.EventHandler(this.labelInventoryDetails_Click);
            this.labelInventoryDetails.MouseEnter += new System.EventHandler(this.labelInventoryDetails_MouseEnter);
            this.labelInventoryDetails.MouseLeave += new System.EventHandler(this.labelInventoryDetails_MouseEnter);
            // 
            // labelPurchaseCostCount
            // 
            this.labelPurchaseCostCount.AutoSize = true;
            this.labelPurchaseCostCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseCostCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseCostCount.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseCostCount.Location = new System.Drawing.Point(63, 162);
            this.labelPurchaseCostCount.Name = "labelPurchaseCostCount";
            this.labelPurchaseCostCount.Size = new System.Drawing.Size(105, 14);
            this.labelPurchaseCostCount.TabIndex = 62;
            this.labelPurchaseCostCount.Text = "采购成本汇总表";
            this.labelPurchaseCostCount.Click += new System.EventHandler(this.labelPurchaseCostCount_Click);
            this.labelPurchaseCostCount.MouseEnter += new System.EventHandler(this.labelPurchaseCostCount_MouseEnter);
            this.labelPurchaseCostCount.MouseLeave += new System.EventHandler(this.labelPurchaseCostCount_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::MainProgram.Properties.Resources.报表_数据统计;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(10, 110);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 48);
            this.pictureBox3.TabIndex = 61;
            this.pictureBox3.TabStop = false;
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCost.ForeColor = System.Drawing.Color.Black;
            this.labelCost.Location = new System.Drawing.Point(63, 44);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(91, 14);
            this.labelCost.TabIndex = 56;
            this.labelCost.Text = "期初成本调整";
            this.labelCost.Click += new System.EventHandler(this.labelCost_Click);
            this.labelCost.MouseEnter += new System.EventHandler(this.labelCost_MouseEnter);
            this.labelCost.MouseLeave += new System.EventHandler(this.labelCost_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.报表_基础资料;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(10, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 48);
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // labelMaterielInOutCount
            // 
            this.labelMaterielInOutCount.AutoSize = true;
            this.labelMaterielInOutCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMaterielInOutCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMaterielInOutCount.ForeColor = System.Drawing.Color.Black;
            this.labelMaterielInOutCount.Location = new System.Drawing.Point(63, 138);
            this.labelMaterielInOutCount.Name = "labelMaterielInOutCount";
            this.labelMaterielInOutCount.Size = new System.Drawing.Size(105, 14);
            this.labelMaterielInOutCount.TabIndex = 71;
            this.labelMaterielInOutCount.Text = "物料出入库核算";
            this.labelMaterielInOutCount.Click += new System.EventHandler(this.labelMaterielInOutCount_Click);
            this.labelMaterielInOutCount.MouseEnter += new System.EventHandler(this.labelMaterielInOutCount_MouseEnter);
            this.labelMaterielInOutCount.MouseLeave += new System.EventHandler(this.labelMaterielInOutCount_MouseEnter);
            // 
            // FormInventoryAccountingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(199, 562);
            this.Controls.Add(this.labelMaterielInOutCount);
            this.Controls.Add(this.labelSaleProfitDetails);
            this.Controls.Add(this.labelSaleProfitCount);
            this.Controls.Add(this.labelSaleCostDetails);
            this.Controls.Add(this.labelSaleCostCount);
            this.Controls.Add(this.labelProductionOutDetails);
            this.Controls.Add(this.labelProductionOutCount);
            this.Controls.Add(this.labelPurchaseCostDetails);
            this.Controls.Add(this.labelInventoryDetails);
            this.Controls.Add(this.labelPurchaseCostCount);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormInventoryAccountingReport";
            this.Text = "存货核算报表";
            this.Load += new System.EventHandler(this.FormInventoryAccountingReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSaleProfitDetails;
        private System.Windows.Forms.Label labelSaleProfitCount;
        private System.Windows.Forms.Label labelSaleCostDetails;
        private System.Windows.Forms.Label labelSaleCostCount;
        private System.Windows.Forms.Label labelProductionOutDetails;
        private System.Windows.Forms.Label labelProductionOutCount;
        private System.Windows.Forms.Label labelPurchaseCostDetails;
        private System.Windows.Forms.Label labelInventoryDetails;
        private System.Windows.Forms.Label labelPurchaseCostCount;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelMaterielInOutCount;

    }
}