namespace MainProgram
{
    partial class FormProductionReport
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
            this.labelProjectInfo = new System.Windows.Forms.Label();
            this.labelMateriel = new System.Windows.Forms.Label();
            this.labelPurchasePirce = new System.Windows.Forms.Label();
            this.labelPurchaseInvoice = new System.Windows.Forms.Label();
            this.labelPurchaseOrder = new System.Windows.Forms.Label();
            this.labelPurchaseIn = new System.Windows.Forms.Label();
            this.labelPurchaseInPayment = new System.Windows.Forms.Label();
            this.labelInventory = new System.Windows.Forms.Label();
            this.labelPurchaseOrderExecute = new System.Windows.Forms.Label();
            this.labelAmountCountByMateriel = new System.Windows.Forms.Label();
            this.labelAmountCountBySupplier = new System.Windows.Forms.Label();
            this.labelAmountCountByPeople = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelPurchasePirceHistory = new System.Windows.Forms.Label();
            this.labelInventoryHistory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProjectInfo
            // 
            this.labelProjectInfo.AutoSize = true;
            this.labelProjectInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProjectInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProjectInfo.ForeColor = System.Drawing.Color.Black;
            this.labelProjectInfo.Location = new System.Drawing.Point(63, 68);
            this.labelProjectInfo.Name = "labelProjectInfo";
            this.labelProjectInfo.Size = new System.Drawing.Size(77, 14);
            this.labelProjectInfo.TabIndex = 11;
            this.labelProjectInfo.Text = "项目资料表";
            this.labelProjectInfo.Click += new System.EventHandler(this.labelSupplier_Click);
            this.labelProjectInfo.MouseEnter += new System.EventHandler(this.labelSupplier_MouseEnter);
            this.labelProjectInfo.MouseLeave += new System.EventHandler(this.labelSupplier_MouseEnter);
            // 
            // labelMateriel
            // 
            this.labelMateriel.AutoSize = true;
            this.labelMateriel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMateriel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMateriel.ForeColor = System.Drawing.Color.Black;
            this.labelMateriel.Location = new System.Drawing.Point(63, 44);
            this.labelMateriel.Name = "labelMateriel";
            this.labelMateriel.Size = new System.Drawing.Size(70, 14);
            this.labelMateriel.TabIndex = 12;
            this.labelMateriel.Text = "产品BOM表";
            this.labelMateriel.Click += new System.EventHandler(this.labelMateriel_Click);
            this.labelMateriel.MouseEnter += new System.EventHandler(this.labelMateriel_MouseEnter);
            this.labelMateriel.MouseLeave += new System.EventHandler(this.labelMateriel_MouseEnter);
            // 
            // labelPurchasePirce
            // 
            this.labelPurchasePirce.AutoSize = true;
            this.labelPurchasePirce.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchasePirce.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchasePirce.ForeColor = System.Drawing.Color.Black;
            this.labelPurchasePirce.Location = new System.Drawing.Point(63, 92);
            this.labelPurchasePirce.Name = "labelPurchasePirce";
            this.labelPurchasePirce.Size = new System.Drawing.Size(105, 14);
            this.labelPurchasePirce.TabIndex = 13;
            this.labelPurchasePirce.Text = "采购价格参照表";
            this.labelPurchasePirce.Visible = false;
            this.labelPurchasePirce.Click += new System.EventHandler(this.labelPurchasePirce_Click);
            this.labelPurchasePirce.MouseEnter += new System.EventHandler(this.labelPurchasePirce_MouseEnter);
            this.labelPurchasePirce.MouseLeave += new System.EventHandler(this.labelPurchasePirce_MouseEnter);
            // 
            // labelPurchaseInvoice
            // 
            this.labelPurchaseInvoice.AutoSize = true;
            this.labelPurchaseInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseInvoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseInvoice.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseInvoice.Location = new System.Drawing.Point(63, 212);
            this.labelPurchaseInvoice.Name = "labelPurchaseInvoice";
            this.labelPurchaseInvoice.Size = new System.Drawing.Size(133, 14);
            this.labelPurchaseInvoice.TabIndex = 17;
            this.labelPurchaseInvoice.Text = "工程总材料表序时薄";
            this.labelPurchaseInvoice.Click += new System.EventHandler(this.labelPurchaseInvoice_Click);
            this.labelPurchaseInvoice.MouseEnter += new System.EventHandler(this.labelPurchaseInvoice_MouseEnter);
            this.labelPurchaseInvoice.MouseLeave += new System.EventHandler(this.labelPurchaseInvoice_MouseEnter);
            // 
            // labelPurchaseOrder
            // 
            this.labelPurchaseOrder.AutoSize = true;
            this.labelPurchaseOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseOrder.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseOrder.Location = new System.Drawing.Point(63, 164);
            this.labelPurchaseOrder.Name = "labelPurchaseOrder";
            this.labelPurchaseOrder.Size = new System.Drawing.Size(133, 14);
            this.labelPurchaseOrder.TabIndex = 16;
            this.labelPurchaseOrder.Text = "设备总材料表序时薄";
            this.labelPurchaseOrder.Click += new System.EventHandler(this.labelPurchaseOrder_Click);
            this.labelPurchaseOrder.MouseEnter += new System.EventHandler(this.labelPurchaseOrder_MouseEnter);
            this.labelPurchaseOrder.MouseLeave += new System.EventHandler(this.labelPurchaseOrder_MouseEnter);
            // 
            // labelPurchaseIn
            // 
            this.labelPurchaseIn.AutoSize = true;
            this.labelPurchaseIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseIn.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseIn.Location = new System.Drawing.Point(63, 188);
            this.labelPurchaseIn.Name = "labelPurchaseIn";
            this.labelPurchaseIn.Size = new System.Drawing.Size(133, 14);
            this.labelPurchaseIn.TabIndex = 15;
            this.labelPurchaseIn.Text = "电器总材料表序时薄";
            this.labelPurchaseIn.Click += new System.EventHandler(this.labelPurchaseIn_Click);
            this.labelPurchaseIn.MouseEnter += new System.EventHandler(this.labelPurchaseIn_MouseEnter);
            this.labelPurchaseIn.MouseLeave += new System.EventHandler(this.labelPurchaseIn_MouseEnter);
            // 
            // labelPurchaseInPayment
            // 
            this.labelPurchaseInPayment.AutoSize = true;
            this.labelPurchaseInPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseInPayment.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseInPayment.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseInPayment.Location = new System.Drawing.Point(63, 333);
            this.labelPurchaseInPayment.Name = "labelPurchaseInPayment";
            this.labelPurchaseInPayment.Size = new System.Drawing.Size(119, 14);
            this.labelPurchaseInPayment.TabIndex = 21;
            this.labelPurchaseInPayment.Text = "工程总材料表跟踪";
            this.labelPurchaseInPayment.Click += new System.EventHandler(this.labelPurchaseInPayment_Click);
            this.labelPurchaseInPayment.MouseEnter += new System.EventHandler(this.labelPurchaseInPayment_MouseEnter);
            this.labelPurchaseInPayment.MouseLeave += new System.EventHandler(this.labelPurchaseInPayment_MouseEnter);
            // 
            // labelInventory
            // 
            this.labelInventory.AutoSize = true;
            this.labelInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventory.ForeColor = System.Drawing.Color.Black;
            this.labelInventory.Location = new System.Drawing.Point(63, 261);
            this.labelInventory.Name = "labelInventory";
            this.labelInventory.Size = new System.Drawing.Size(119, 14);
            this.labelInventory.TabIndex = 20;
            this.labelInventory.Text = "项目整体跟踪情况";
            this.labelInventory.Click += new System.EventHandler(this.labelInventory_Click);
            this.labelInventory.MouseEnter += new System.EventHandler(this.labelInventory_MouseEnter);
            this.labelInventory.MouseLeave += new System.EventHandler(this.labelInventory_MouseEnter);
            // 
            // labelPurchaseOrderExecute
            // 
            this.labelPurchaseOrderExecute.AutoSize = true;
            this.labelPurchaseOrderExecute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchaseOrderExecute.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchaseOrderExecute.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseOrderExecute.Location = new System.Drawing.Point(63, 309);
            this.labelPurchaseOrderExecute.Name = "labelPurchaseOrderExecute";
            this.labelPurchaseOrderExecute.Size = new System.Drawing.Size(119, 14);
            this.labelPurchaseOrderExecute.TabIndex = 19;
            this.labelPurchaseOrderExecute.Text = "电器总材料表跟踪";
            this.labelPurchaseOrderExecute.Click += new System.EventHandler(this.labelPurchaseOrderExecute_Click);
            this.labelPurchaseOrderExecute.MouseEnter += new System.EventHandler(this.labelPurchaseOrderExecute_MouseEnter);
            this.labelPurchaseOrderExecute.MouseLeave += new System.EventHandler(this.labelPurchaseOrderExecute_MouseEnter);
            // 
            // labelAmountCountByMateriel
            // 
            this.labelAmountCountByMateriel.AutoSize = true;
            this.labelAmountCountByMateriel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAmountCountByMateriel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAmountCountByMateriel.ForeColor = System.Drawing.Color.Black;
            this.labelAmountCountByMateriel.Location = new System.Drawing.Point(63, 357);
            this.labelAmountCountByMateriel.Name = "labelAmountCountByMateriel";
            this.labelAmountCountByMateriel.Size = new System.Drawing.Size(147, 14);
            this.labelAmountCountByMateriel.TabIndex = 22;
            this.labelAmountCountByMateriel.Text = "采购金额统计(按物料)";
            this.labelAmountCountByMateriel.Visible = false;
            this.labelAmountCountByMateriel.Click += new System.EventHandler(this.labelAmountCountByMateriel_Click);
            this.labelAmountCountByMateriel.MouseEnter += new System.EventHandler(this.labelAmountCountByMateriel_MouseEnter);
            this.labelAmountCountByMateriel.MouseLeave += new System.EventHandler(this.labelAmountCountByMateriel_MouseEnter);
            // 
            // labelAmountCountBySupplier
            // 
            this.labelAmountCountBySupplier.AutoSize = true;
            this.labelAmountCountBySupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAmountCountBySupplier.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAmountCountBySupplier.ForeColor = System.Drawing.Color.Black;
            this.labelAmountCountBySupplier.Location = new System.Drawing.Point(63, 405);
            this.labelAmountCountBySupplier.Name = "labelAmountCountBySupplier";
            this.labelAmountCountBySupplier.Size = new System.Drawing.Size(161, 14);
            this.labelAmountCountBySupplier.TabIndex = 24;
            this.labelAmountCountBySupplier.Text = "采购金额统计(按供应商)";
            this.labelAmountCountBySupplier.Visible = false;
            this.labelAmountCountBySupplier.Click += new System.EventHandler(this.labelAmountCountBySupplier_Click);
            this.labelAmountCountBySupplier.MouseEnter += new System.EventHandler(this.labelAmountCountBySupplier_MouseEnter);
            this.labelAmountCountBySupplier.MouseLeave += new System.EventHandler(this.labelAmountCountBySupplier_MouseEnter);
            // 
            // labelAmountCountByPeople
            // 
            this.labelAmountCountByPeople.AutoSize = true;
            this.labelAmountCountByPeople.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAmountCountByPeople.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAmountCountByPeople.ForeColor = System.Drawing.Color.Black;
            this.labelAmountCountByPeople.Location = new System.Drawing.Point(63, 381);
            this.labelAmountCountByPeople.Name = "labelAmountCountByPeople";
            this.labelAmountCountByPeople.Size = new System.Drawing.Size(161, 14);
            this.labelAmountCountByPeople.TabIndex = 23;
            this.labelAmountCountByPeople.Text = "采购金额统计(按采购员)";
            this.labelAmountCountByPeople.Visible = false;
            this.labelAmountCountByPeople.Click += new System.EventHandler(this.labelAmountCountByPeople_Click);
            this.labelAmountCountByPeople.MouseEnter += new System.EventHandler(this.labelAmountCountByPeople_MouseEnter);
            this.labelAmountCountByPeople.MouseLeave += new System.EventHandler(this.labelAmountCountByPeople_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::MainProgram.Properties.Resources.报表_数据统计;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(10, 257);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 48);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MainProgram.Properties.Resources.报表_序时薄;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(10, 160);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 48);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.报表_基础资料;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(10, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 48);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // labelPurchasePirceHistory
            // 
            this.labelPurchasePirceHistory.AutoSize = true;
            this.labelPurchasePirceHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPurchasePirceHistory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPurchasePirceHistory.ForeColor = System.Drawing.Color.Black;
            this.labelPurchasePirceHistory.Location = new System.Drawing.Point(63, 115);
            this.labelPurchasePirceHistory.Name = "labelPurchasePirceHistory";
            this.labelPurchasePirceHistory.Size = new System.Drawing.Size(133, 14);
            this.labelPurchasePirceHistory.TabIndex = 25;
            this.labelPurchasePirceHistory.Text = "供应商历史价格查询";
            this.labelPurchasePirceHistory.Visible = false;
            this.labelPurchasePirceHistory.Click += new System.EventHandler(this.labelPurchasePirceHistory_Click);
            this.labelPurchasePirceHistory.MouseEnter += new System.EventHandler(this.labelPurchasePirceHistory_MouseEnter);
            this.labelPurchasePirceHistory.MouseLeave += new System.EventHandler(this.labelPurchasePirceHistory_MouseEnter);
            // 
            // labelInventoryHistory
            // 
            this.labelInventoryHistory.AutoSize = true;
            this.labelInventoryHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventoryHistory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventoryHistory.ForeColor = System.Drawing.Color.Black;
            this.labelInventoryHistory.Location = new System.Drawing.Point(63, 285);
            this.labelInventoryHistory.Name = "labelInventoryHistory";
            this.labelInventoryHistory.Size = new System.Drawing.Size(119, 14);
            this.labelInventoryHistory.TabIndex = 26;
            this.labelInventoryHistory.Text = "设备总材料表跟踪";
            this.labelInventoryHistory.Click += new System.EventHandler(this.labelInventoryHistory_Click);
            this.labelInventoryHistory.MouseEnter += new System.EventHandler(this.labelInventoryHistory_MouseEnter);
            this.labelInventoryHistory.MouseLeave += new System.EventHandler(this.labelInventoryHistory_MouseEnter);
            // 
            // FormProductionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(219, 562);
            this.Controls.Add(this.labelInventoryHistory);
            this.Controls.Add(this.labelPurchasePirceHistory);
            this.Controls.Add(this.labelAmountCountBySupplier);
            this.Controls.Add(this.labelAmountCountByPeople);
            this.Controls.Add(this.labelAmountCountByMateriel);
            this.Controls.Add(this.labelPurchaseInPayment);
            this.Controls.Add(this.labelInventory);
            this.Controls.Add(this.labelPurchaseOrderExecute);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelPurchaseInvoice);
            this.Controls.Add(this.labelPurchaseOrder);
            this.Controls.Add(this.labelPurchaseIn);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelPurchasePirce);
            this.Controls.Add(this.labelMateriel);
            this.Controls.Add(this.labelProjectInfo);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormProductionReport";
            this.Text = "项目管理报表";
            this.Load += new System.EventHandler(this.FormProductionReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelProjectInfo;
        private System.Windows.Forms.Label labelMateriel;
        private System.Windows.Forms.Label labelPurchasePirce;
        private System.Windows.Forms.Label labelPurchaseInvoice;
        private System.Windows.Forms.Label labelPurchaseOrder;
        private System.Windows.Forms.Label labelPurchaseIn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelPurchaseInPayment;
        private System.Windows.Forms.Label labelInventory;
        private System.Windows.Forms.Label labelPurchaseOrderExecute;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label labelAmountCountByMateriel;
        private System.Windows.Forms.Label labelAmountCountBySupplier;
        private System.Windows.Forms.Label labelAmountCountByPeople;
        private System.Windows.Forms.Label labelPurchasePirceHistory;
        private System.Windows.Forms.Label labelInventoryHistory;

    }
}