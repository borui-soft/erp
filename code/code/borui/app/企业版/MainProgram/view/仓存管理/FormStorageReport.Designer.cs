namespace MainProgram
{
    partial class FormStorageReport
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
            this.labelMaterielDetails = new System.Windows.Forms.Label();
            this.labelMaterielCount = new System.Windows.Forms.Label();
            this.labelInventory = new System.Windows.Forms.Label();
            this.labelOrderInventory = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.labelOrderMaterielProOccupied = new System.Windows.Forms.Label();
            this.labelOrderIn = new System.Windows.Forms.Label();
            this.labelOrderOut = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelMateriel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelInventoryAge = new System.Windows.Forms.Label();
            this.labelInventoryAnalysis = new System.Windows.Forms.Label();
            this.labelInventoryAlarm = new System.Windows.Forms.Label();
            this.labelProductionOut = new System.Windows.Forms.Label();
            this.labelInventoryLife = new System.Windows.Forms.Label();
            this.labelInventoryPassAge = new System.Windows.Forms.Label();
            this.labelInventoryHistory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMaterielDetails
            // 
            this.labelMaterielDetails.AutoSize = true;
            this.labelMaterielDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMaterielDetails.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMaterielDetails.ForeColor = System.Drawing.Color.Black;
            this.labelMaterielDetails.Location = new System.Drawing.Point(63, 306);
            this.labelMaterielDetails.Name = "labelMaterielDetails";
            this.labelMaterielDetails.Size = new System.Drawing.Size(105, 14);
            this.labelMaterielDetails.TabIndex = 49;
            this.labelMaterielDetails.Text = "生产领料汇总表";
            this.labelMaterielDetails.Click += new System.EventHandler(this.labelMaterielDetails_Click);
            this.labelMaterielDetails.MouseEnter += new System.EventHandler(this.labelMaterielDetails_MouseEnter);
            this.labelMaterielDetails.MouseLeave += new System.EventHandler(this.labelMaterielDetails_MouseEnter);
            // 
            // labelMaterielCount
            // 
            this.labelMaterielCount.AutoSize = true;
            this.labelMaterielCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMaterielCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMaterielCount.ForeColor = System.Drawing.Color.Black;
            this.labelMaterielCount.Location = new System.Drawing.Point(63, 282);
            this.labelMaterielCount.Name = "labelMaterielCount";
            this.labelMaterielCount.Size = new System.Drawing.Size(105, 14);
            this.labelMaterielCount.TabIndex = 48;
            this.labelMaterielCount.Text = "产品入库汇总表";
            this.labelMaterielCount.Click += new System.EventHandler(this.labelMaterielCount_Click);
            this.labelMaterielCount.MouseEnter += new System.EventHandler(this.labelMaterielCount_MouseEnter);
            this.labelMaterielCount.MouseLeave += new System.EventHandler(this.labelMaterielCount_MouseEnter);
            // 
            // labelInventory
            // 
            this.labelInventory.AutoSize = true;
            this.labelInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventory.ForeColor = System.Drawing.Color.Black;
            this.labelInventory.Location = new System.Drawing.Point(63, 210);
            this.labelInventory.Name = "labelInventory";
            this.labelInventory.Size = new System.Drawing.Size(49, 14);
            this.labelInventory.TabIndex = 47;
            this.labelInventory.Text = "库存表";
            this.labelInventory.Click += new System.EventHandler(this.labelInventory_Click);
            this.labelInventory.MouseEnter += new System.EventHandler(this.labelInventory_MouseEnter);
            this.labelInventory.MouseLeave += new System.EventHandler(this.labelInventory_MouseEnter);
            // 
            // labelOrderInventory
            // 
            this.labelOrderInventory.AutoSize = true;
            this.labelOrderInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelOrderInventory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderInventory.ForeColor = System.Drawing.Color.Black;
            this.labelOrderInventory.Location = new System.Drawing.Point(63, 258);
            this.labelOrderInventory.Name = "labelOrderInventory";
            this.labelOrderInventory.Size = new System.Drawing.Size(105, 14);
            this.labelOrderInventory.TabIndex = 46;
            this.labelOrderInventory.Text = "物料出入库明细";
            this.labelOrderInventory.Click += new System.EventHandler(this.labelOrderInventory_Click);
            this.labelOrderInventory.MouseEnter += new System.EventHandler(this.labelOrderInventory_MouseEnter);
            this.labelOrderInventory.MouseLeave += new System.EventHandler(this.labelOrderInventory_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::MainProgram.Properties.Resources.报表_数据统计;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(10, 206);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 48);
            this.pictureBox3.TabIndex = 45;
            this.pictureBox3.TabStop = false;
            // 
            // labelOrderMaterielProOccupied
            // 
            this.labelOrderMaterielProOccupied.AutoSize = true;
            this.labelOrderMaterielProOccupied.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelOrderMaterielProOccupied.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderMaterielProOccupied.ForeColor = System.Drawing.Color.Black;
            this.labelOrderMaterielProOccupied.Location = new System.Drawing.Point(63, 164);
            this.labelOrderMaterielProOccupied.Name = "labelOrderMaterielProOccupied";
            this.labelOrderMaterielProOccupied.Size = new System.Drawing.Size(119, 14);
            this.labelOrderMaterielProOccupied.TabIndex = 44;
            this.labelOrderMaterielProOccupied.Text = "库存预占单序时薄";
            this.labelOrderMaterielProOccupied.Click += new System.EventHandler(this.labelOrderAllocate_Click);
            this.labelOrderMaterielProOccupied.MouseEnter += new System.EventHandler(this.labelOrderAllocate_MouseEnter);
            this.labelOrderMaterielProOccupied.MouseLeave += new System.EventHandler(this.labelOrderAllocate_MouseEnter);
            // 
            // labelOrderIn
            // 
            this.labelOrderIn.AutoSize = true;
            this.labelOrderIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelOrderIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderIn.ForeColor = System.Drawing.Color.Black;
            this.labelOrderIn.Location = new System.Drawing.Point(63, 116);
            this.labelOrderIn.Name = "labelOrderIn";
            this.labelOrderIn.Size = new System.Drawing.Size(119, 14);
            this.labelOrderIn.TabIndex = 43;
            this.labelOrderIn.Text = "入库类单据序时薄";
            this.labelOrderIn.Click += new System.EventHandler(this.labelOrderIn_Click);
            this.labelOrderIn.MouseEnter += new System.EventHandler(this.labelOrderIn_MouseEnter);
            this.labelOrderIn.MouseLeave += new System.EventHandler(this.labelOrderIn_MouseEnter);
            // 
            // labelOrderOut
            // 
            this.labelOrderOut.AutoSize = true;
            this.labelOrderOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelOrderOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderOut.ForeColor = System.Drawing.Color.Black;
            this.labelOrderOut.Location = new System.Drawing.Point(63, 140);
            this.labelOrderOut.Name = "labelOrderOut";
            this.labelOrderOut.Size = new System.Drawing.Size(119, 14);
            this.labelOrderOut.TabIndex = 42;
            this.labelOrderOut.Text = "出库类单据序时薄";
            this.labelOrderOut.Click += new System.EventHandler(this.labelOrderOut_Click);
            this.labelOrderOut.MouseEnter += new System.EventHandler(this.labelOrderOut_MouseEnter);
            this.labelOrderOut.MouseLeave += new System.EventHandler(this.labelOrderOut_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MainProgram.Properties.Resources.报表_序时薄;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(10, 112);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 48);
            this.pictureBox2.TabIndex = 41;
            this.pictureBox2.TabStop = false;
            // 
            // labelMateriel
            // 
            this.labelMateriel.AutoSize = true;
            this.labelMateriel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMateriel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMateriel.ForeColor = System.Drawing.Color.Black;
            this.labelMateriel.Location = new System.Drawing.Point(63, 44);
            this.labelMateriel.Name = "labelMateriel";
            this.labelMateriel.Size = new System.Drawing.Size(35, 14);
            this.labelMateriel.TabIndex = 39;
            this.labelMateriel.Text = "物料";
            this.labelMateriel.Click += new System.EventHandler(this.labelMateriel_Click);
            this.labelMateriel.MouseEnter += new System.EventHandler(this.labelMateriel_MouseEnter);
            this.labelMateriel.MouseLeave += new System.EventHandler(this.labelMateriel_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.报表_基础资料;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(10, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 48);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // labelInventoryAge
            // 
            this.labelInventoryAge.AutoSize = true;
            this.labelInventoryAge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAge.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAge.Location = new System.Drawing.Point(63, 378);
            this.labelInventoryAge.Name = "labelInventoryAge";
            this.labelInventoryAge.Size = new System.Drawing.Size(105, 14);
            this.labelInventoryAge.TabIndex = 52;
            this.labelInventoryAge.Text = "库存账龄分析表";
            this.labelInventoryAge.Click += new System.EventHandler(this.labelInventoryAge_Click);
            this.labelInventoryAge.MouseEnter += new System.EventHandler(this.labelInventoryAge_MouseEnter);
            this.labelInventoryAge.MouseLeave += new System.EventHandler(this.labelInventoryAge_MouseEnter);
            // 
            // labelInventoryAnalysis
            // 
            this.labelInventoryAnalysis.AutoSize = true;
            this.labelInventoryAnalysis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryAnalysis.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAnalysis.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAnalysis.Location = new System.Drawing.Point(63, 354);
            this.labelInventoryAnalysis.Name = "labelInventoryAnalysis";
            this.labelInventoryAnalysis.Size = new System.Drawing.Size(126, 14);
            this.labelInventoryAnalysis.TabIndex = 51;
            this.labelInventoryAnalysis.Text = "超储/短缺库存分析";
            this.labelInventoryAnalysis.Click += new System.EventHandler(this.labelInventoryAnalysis_Click);
            this.labelInventoryAnalysis.MouseEnter += new System.EventHandler(this.labelInventoryAnalysis_MouseEnter);
            this.labelInventoryAnalysis.MouseLeave += new System.EventHandler(this.labelInventoryAnalysis_MouseEnter);
            // 
            // labelInventoryAlarm
            // 
            this.labelInventoryAlarm.AutoSize = true;
            this.labelInventoryAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryAlarm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryAlarm.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryAlarm.Location = new System.Drawing.Point(63, 330);
            this.labelInventoryAlarm.Name = "labelInventoryAlarm";
            this.labelInventoryAlarm.Size = new System.Drawing.Size(119, 14);
            this.labelInventoryAlarm.TabIndex = 50;
            this.labelInventoryAlarm.Text = "安全库存预警分析";
            this.labelInventoryAlarm.Click += new System.EventHandler(this.labelInventoryAlarm_Click);
            this.labelInventoryAlarm.MouseEnter += new System.EventHandler(this.labelInventoryAlarm_MouseEnter);
            this.labelInventoryAlarm.MouseLeave += new System.EventHandler(this.labelInventoryAlarm_MouseEnter);
            // 
            // labelProductionOut
            // 
            this.labelProductionOut.AutoSize = true;
            this.labelProductionOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProductionOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProductionOut.ForeColor = System.Drawing.Color.Black;
            this.labelProductionOut.Location = new System.Drawing.Point(63, 450);
            this.labelProductionOut.Name = "labelProductionOut";
            this.labelProductionOut.Size = new System.Drawing.Size(119, 14);
            this.labelProductionOut.TabIndex = 54;
            this.labelProductionOut.Text = "超出保质期统计表";
            this.labelProductionOut.Visible = false;
            this.labelProductionOut.Click += new System.EventHandler(this.labelProductionOut_Click);
            this.labelProductionOut.MouseEnter += new System.EventHandler(this.labelProductionOut_MouseEnter);
            this.labelProductionOut.MouseLeave += new System.EventHandler(this.labelProductionOut_MouseEnter);
            // 
            // labelInventoryLife
            // 
            this.labelInventoryLife.AutoSize = true;
            this.labelInventoryLife.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryLife.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryLife.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryLife.Location = new System.Drawing.Point(63, 426);
            this.labelInventoryLife.Name = "labelInventoryLife";
            this.labelInventoryLife.Size = new System.Drawing.Size(105, 14);
            this.labelInventoryLife.TabIndex = 53;
            this.labelInventoryLife.Text = "保质期预警分析";
            this.labelInventoryLife.Visible = false;
            this.labelInventoryLife.Click += new System.EventHandler(this.labelInventoryLife_Click);
            this.labelInventoryLife.MouseEnter += new System.EventHandler(this.labelInventoryLife_MouseEnter);
            this.labelInventoryLife.MouseLeave += new System.EventHandler(this.labelInventoryLife_MouseEnter);
            // 
            // labelInventoryPassAge
            // 
            this.labelInventoryPassAge.AutoSize = true;
            this.labelInventoryPassAge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryPassAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryPassAge.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryPassAge.Location = new System.Drawing.Point(62, 402);
            this.labelInventoryPassAge.Name = "labelInventoryPassAge";
            this.labelInventoryPassAge.Size = new System.Drawing.Size(119, 14);
            this.labelInventoryPassAge.TabIndex = 55;
            this.labelInventoryPassAge.Text = "库存呆滞料分析表";
            this.labelInventoryPassAge.Visible = false;
            this.labelInventoryPassAge.Click += new System.EventHandler(this.labelInventoryPassAge_Click);
            this.labelInventoryPassAge.MouseEnter += new System.EventHandler(this.labelInventoryPassAge_MouseEnter);
            this.labelInventoryPassAge.MouseLeave += new System.EventHandler(this.labelInventoryPassAge_MouseEnter);
            // 
            // labelInventoryHistory
            // 
            this.labelInventoryHistory.AutoSize = true;
            this.labelInventoryHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryHistory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryHistory.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryHistory.Location = new System.Drawing.Point(63, 234);
            this.labelInventoryHistory.Name = "labelInventoryHistory";
            this.labelInventoryHistory.Size = new System.Drawing.Size(77, 14);
            this.labelInventoryHistory.TabIndex = 56;
            this.labelInventoryHistory.Text = "历史库存表";
            this.labelInventoryHistory.Click += new System.EventHandler(this.labelInventoryHistory_Click);
            this.labelInventoryHistory.MouseEnter += new System.EventHandler(this.labelInventoryHistory_MouseEnter);
            this.labelInventoryHistory.MouseLeave += new System.EventHandler(this.labelInventoryHistory_MouseEnter);
            // 
            // FormStorageReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(199, 562);
            this.Controls.Add(this.labelInventoryHistory);
            this.Controls.Add(this.labelInventoryPassAge);
            this.Controls.Add(this.labelProductionOut);
            this.Controls.Add(this.labelInventoryLife);
            this.Controls.Add(this.labelInventoryAge);
            this.Controls.Add(this.labelInventoryAnalysis);
            this.Controls.Add(this.labelInventoryAlarm);
            this.Controls.Add(this.labelMaterielDetails);
            this.Controls.Add(this.labelMaterielCount);
            this.Controls.Add(this.labelInventory);
            this.Controls.Add(this.labelOrderInventory);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelOrderMaterielProOccupied);
            this.Controls.Add(this.labelOrderIn);
            this.Controls.Add(this.labelOrderOut);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelMateriel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormStorageReport";
            this.Text = "仓存管理报表";
            this.Load += new System.EventHandler(this.FormStorageReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMaterielDetails;
        private System.Windows.Forms.Label labelMaterielCount;
        private System.Windows.Forms.Label labelInventory;
        private System.Windows.Forms.Label labelOrderInventory;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label labelOrderMaterielProOccupied;
        private System.Windows.Forms.Label labelOrderIn;
        private System.Windows.Forms.Label labelOrderOut;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelMateriel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelInventoryAge;
        private System.Windows.Forms.Label labelInventoryAnalysis;
        private System.Windows.Forms.Label labelInventoryAlarm;
        private System.Windows.Forms.Label labelProductionOut;
        private System.Windows.Forms.Label labelInventoryLife;
        private System.Windows.Forms.Label labelInventoryPassAge;
        private System.Windows.Forms.Label labelInventoryHistory;

    }
}