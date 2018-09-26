namespace MainProgram
{
    partial class FormSaleReport
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
            this.labelSaleCountByPeople = new System.Windows.Forms.Label();
            this.labelSaleIn = new System.Windows.Forms.Label();
            this.labelInventory = new System.Windows.Forms.Label();
            this.labelSaleOrderExecute = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.labelSaleOut = new System.Windows.Forms.Label();
            this.SalePrice = new System.Windows.Forms.Label();
            this.labelSaleOrder = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelSaleBasePrice = new System.Windows.Forms.Label();
            this.labelMateriel = new System.Windows.Forms.Label();
            this.labelCustom = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelSaleInvoice = new System.Windows.Forms.Label();
            this.labelSaleCountByCustom = new System.Windows.Forms.Label();
            this.labelSaleCountByProducts = new System.Windows.Forms.Label();
            this.labelInventoryHistory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSaleCountByPeople
            // 
            this.labelSaleCountByPeople.AutoSize = true;
            this.labelSaleCountByPeople.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleCountByPeople.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleCountByPeople.ForeColor = System.Drawing.Color.Black;
            this.labelSaleCountByPeople.Location = new System.Drawing.Point(63, 381);
            this.labelSaleCountByPeople.Name = "labelSaleCountByPeople";
            this.labelSaleCountByPeople.Size = new System.Drawing.Size(147, 14);
            this.labelSaleCountByPeople.TabIndex = 35;
            this.labelSaleCountByPeople.Text = "销售额统计(按业务员)";
            this.labelSaleCountByPeople.Click += new System.EventHandler(this.labelSaleCountByPeople_Click);
            this.labelSaleCountByPeople.MouseEnter += new System.EventHandler(this.labelSaleCountByPeople_MouseEnter);
            this.labelSaleCountByPeople.MouseLeave += new System.EventHandler(this.labelSaleCountByPeople_MouseEnter);
            // 
            // labelSaleIn
            // 
            this.labelSaleIn.AutoSize = true;
            this.labelSaleIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleIn.ForeColor = System.Drawing.Color.Black;
            this.labelSaleIn.Location = new System.Drawing.Point(63, 333);
            this.labelSaleIn.Name = "labelSaleIn";
            this.labelSaleIn.Size = new System.Drawing.Size(91, 14);
            this.labelSaleIn.TabIndex = 34;
            this.labelSaleIn.Text = "销售回款情况";
            this.labelSaleIn.Click += new System.EventHandler(this.labelSaleIn_Click);
            this.labelSaleIn.MouseEnter += new System.EventHandler(this.labelSaleIn_MouseEnter);
            this.labelSaleIn.MouseLeave += new System.EventHandler(this.labelSaleIn_MouseEnter);
            // 
            // labelInventory
            // 
            this.labelInventory.AutoSize = true;
            this.labelInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInventory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInventory.ForeColor = System.Drawing.Color.Black;
            this.labelInventory.Location = new System.Drawing.Point(63, 261);
            this.labelInventory.Name = "labelInventory";
            this.labelInventory.Size = new System.Drawing.Size(91, 14);
            this.labelInventory.TabIndex = 33;
            this.labelInventory.Text = "库存表(商品)";
            this.labelInventory.Click += new System.EventHandler(this.labelInventory_Click);
            this.labelInventory.MouseEnter += new System.EventHandler(this.labelInventory_MouseEnter);
            this.labelInventory.MouseLeave += new System.EventHandler(this.labelInventory_MouseEnter);
            // 
            // labelSaleOrderExecute
            // 
            this.labelSaleOrderExecute.AutoSize = true;
            this.labelSaleOrderExecute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleOrderExecute.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleOrderExecute.ForeColor = System.Drawing.Color.Black;
            this.labelSaleOrderExecute.Location = new System.Drawing.Point(63, 309);
            this.labelSaleOrderExecute.Name = "labelSaleOrderExecute";
            this.labelSaleOrderExecute.Size = new System.Drawing.Size(119, 14);
            this.labelSaleOrderExecute.TabIndex = 32;
            this.labelSaleOrderExecute.Text = "销售订单执行情况";
            this.labelSaleOrderExecute.Click += new System.EventHandler(this.labelSaleOrderExecute_Click);
            this.labelSaleOrderExecute.MouseEnter += new System.EventHandler(this.labelSaleOrderExecute_MouseEnter);
            this.labelSaleOrderExecute.MouseLeave += new System.EventHandler(this.labelSaleOrderExecute_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::MainProgram.Properties.Resources.报表_数据统计;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(10, 257);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 48);
            this.pictureBox3.TabIndex = 31;
            this.pictureBox3.TabStop = false;
            // 
            // labelSaleOut
            // 
            this.labelSaleOut.AutoSize = true;
            this.labelSaleOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleOut.ForeColor = System.Drawing.Color.Black;
            this.labelSaleOut.Location = new System.Drawing.Point(63, 188);
            this.labelSaleOut.Name = "labelSaleOut";
            this.labelSaleOut.Size = new System.Drawing.Size(105, 14);
            this.labelSaleOut.TabIndex = 30;
            this.labelSaleOut.Text = "销售出库序时薄";
            this.labelSaleOut.Click += new System.EventHandler(this.labelSaleOut_Click);
            this.labelSaleOut.MouseEnter += new System.EventHandler(this.labelSaleOut_MouseEnter);
            this.labelSaleOut.MouseLeave += new System.EventHandler(this.labelSaleOut_MouseEnter);
            // 
            // SalePrice
            // 
            this.SalePrice.AutoSize = true;
            this.SalePrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SalePrice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SalePrice.ForeColor = System.Drawing.Color.Black;
            this.SalePrice.Location = new System.Drawing.Point(63, 140);
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.Size = new System.Drawing.Size(105, 14);
            this.SalePrice.TabIndex = 29;
            this.SalePrice.Text = "销售报价序时薄";
            this.SalePrice.Click += new System.EventHandler(this.SalePrice_Click);
            this.SalePrice.MouseEnter += new System.EventHandler(this.SalePrice_MouseEnter);
            this.SalePrice.MouseLeave += new System.EventHandler(this.SalePrice_MouseEnter);
            // 
            // labelSaleOrder
            // 
            this.labelSaleOrder.AutoSize = true;
            this.labelSaleOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleOrder.ForeColor = System.Drawing.Color.Black;
            this.labelSaleOrder.Location = new System.Drawing.Point(63, 164);
            this.labelSaleOrder.Name = "labelSaleOrder";
            this.labelSaleOrder.Size = new System.Drawing.Size(105, 14);
            this.labelSaleOrder.TabIndex = 28;
            this.labelSaleOrder.Text = "销售订单序时薄";
            this.labelSaleOrder.Click += new System.EventHandler(this.labelSaleOrder_Click);
            this.labelSaleOrder.MouseEnter += new System.EventHandler(this.labelSaleOrder_MouseEnter);
            this.labelSaleOrder.MouseLeave += new System.EventHandler(this.labelSaleOrder_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MainProgram.Properties.Resources.报表_序时薄;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(10, 136);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 48);
            this.pictureBox2.TabIndex = 27;
            this.pictureBox2.TabStop = false;
            // 
            // labelSaleBasePrice
            // 
            this.labelSaleBasePrice.AutoSize = true;
            this.labelSaleBasePrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleBasePrice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleBasePrice.ForeColor = System.Drawing.Color.Black;
            this.labelSaleBasePrice.Location = new System.Drawing.Point(63, 92);
            this.labelSaleBasePrice.Name = "labelSaleBasePrice";
            this.labelSaleBasePrice.Size = new System.Drawing.Size(105, 14);
            this.labelSaleBasePrice.TabIndex = 26;
            this.labelSaleBasePrice.Text = "销售价格参照表";
            this.labelSaleBasePrice.Click += new System.EventHandler(this.labelSaleBasePrice_Click);
            this.labelSaleBasePrice.MouseEnter += new System.EventHandler(this.labelSaleBasePrice_MouseEnter);
            this.labelSaleBasePrice.MouseLeave += new System.EventHandler(this.labelSaleBasePrice_MouseEnter);
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
            this.labelMateriel.TabIndex = 25;
            this.labelMateriel.Text = "物料";
            this.labelMateriel.Click += new System.EventHandler(this.labelMateriel_Click);
            this.labelMateriel.MouseEnter += new System.EventHandler(this.labelMateriel_MouseEnter);
            this.labelMateriel.MouseLeave += new System.EventHandler(this.labelMateriel_MouseEnter);
            // 
            // labelCustom
            // 
            this.labelCustom.AutoSize = true;
            this.labelCustom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCustom.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCustom.ForeColor = System.Drawing.Color.Black;
            this.labelCustom.Location = new System.Drawing.Point(63, 68);
            this.labelCustom.Name = "labelCustom";
            this.labelCustom.Size = new System.Drawing.Size(35, 14);
            this.labelCustom.TabIndex = 24;
            this.labelCustom.Text = "客户";
            this.labelCustom.Click += new System.EventHandler(this.labelCustom_Click);
            this.labelCustom.MouseEnter += new System.EventHandler(this.labelCustom_MouseEnter);
            this.labelCustom.MouseLeave += new System.EventHandler(this.labelCustom_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MainProgram.Properties.Resources.报表_基础资料;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(10, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 48);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // labelSaleInvoice
            // 
            this.labelSaleInvoice.AutoSize = true;
            this.labelSaleInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleInvoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleInvoice.ForeColor = System.Drawing.Color.Black;
            this.labelSaleInvoice.Location = new System.Drawing.Point(63, 212);
            this.labelSaleInvoice.Name = "labelSaleInvoice";
            this.labelSaleInvoice.Size = new System.Drawing.Size(105, 14);
            this.labelSaleInvoice.TabIndex = 36;
            this.labelSaleInvoice.Text = "销售发票序时薄";
            this.labelSaleInvoice.Click += new System.EventHandler(this.labelSaleInvoice_Click);
            this.labelSaleInvoice.MouseEnter += new System.EventHandler(this.labelSaleInvoice_MouseEnter);
            this.labelSaleInvoice.MouseLeave += new System.EventHandler(this.labelSaleInvoice_MouseEnter);
            // 
            // labelSaleCountByCustom
            // 
            this.labelSaleCountByCustom.AutoSize = true;
            this.labelSaleCountByCustom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleCountByCustom.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleCountByCustom.ForeColor = System.Drawing.Color.Black;
            this.labelSaleCountByCustom.Location = new System.Drawing.Point(63, 405);
            this.labelSaleCountByCustom.Name = "labelSaleCountByCustom";
            this.labelSaleCountByCustom.Size = new System.Drawing.Size(133, 14);
            this.labelSaleCountByCustom.TabIndex = 37;
            this.labelSaleCountByCustom.Text = "销售额统计(按客户)";
            this.labelSaleCountByCustom.Click += new System.EventHandler(this.labelSaleCountByCustom_Click);
            this.labelSaleCountByCustom.MouseEnter += new System.EventHandler(this.labelSaleCountByCustom_MouseEnter);
            this.labelSaleCountByCustom.MouseLeave += new System.EventHandler(this.labelSaleCountByCustom_MouseEnter);
            // 
            // labelSaleCountByProducts
            // 
            this.labelSaleCountByProducts.AutoSize = true;
            this.labelSaleCountByProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSaleCountByProducts.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSaleCountByProducts.ForeColor = System.Drawing.Color.Black;
            this.labelSaleCountByProducts.Location = new System.Drawing.Point(63, 357);
            this.labelSaleCountByProducts.Name = "labelSaleCountByProducts";
            this.labelSaleCountByProducts.Size = new System.Drawing.Size(133, 14);
            this.labelSaleCountByProducts.TabIndex = 38;
            this.labelSaleCountByProducts.Text = "销售额统计(按商品)";
            this.labelSaleCountByProducts.Click += new System.EventHandler(this.labelSaleCountByProducts_Click);
            this.labelSaleCountByProducts.MouseEnter += new System.EventHandler(this.labelSaleCountByProducts_MouseEnter);
            this.labelSaleCountByProducts.MouseLeave += new System.EventHandler(this.labelSaleCountByProducts_MouseEnter);
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
            this.labelInventoryHistory.TabIndex = 39;
            this.labelInventoryHistory.Text = "历史库存表(商品)";
            this.labelInventoryHistory.Click += new System.EventHandler(this.labelInventoryHistory_Click);
            this.labelInventoryHistory.MouseEnter += new System.EventHandler(this.labelInventoryHistory_MouseEnter);
            this.labelInventoryHistory.MouseLeave += new System.EventHandler(this.labelInventoryHistory_MouseEnter);
            // 
            // FormSaleReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(214, 562);
            this.Controls.Add(this.labelInventoryHistory);
            this.Controls.Add(this.labelSaleCountByProducts);
            this.Controls.Add(this.labelSaleCountByCustom);
            this.Controls.Add(this.labelSaleInvoice);
            this.Controls.Add(this.labelSaleCountByPeople);
            this.Controls.Add(this.labelSaleIn);
            this.Controls.Add(this.labelInventory);
            this.Controls.Add(this.labelSaleOrderExecute);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelSaleOut);
            this.Controls.Add(this.SalePrice);
            this.Controls.Add(this.labelSaleOrder);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelSaleBasePrice);
            this.Controls.Add(this.labelMateriel);
            this.Controls.Add(this.labelCustom);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormSaleReport";
            this.Text = "销售管理报表";
            this.Load += new System.EventHandler(this.FormSaleReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSaleCountByPeople;
        private System.Windows.Forms.Label labelSaleIn;
        private System.Windows.Forms.Label labelInventory;
        private System.Windows.Forms.Label labelSaleOrderExecute;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label labelSaleOut;
        private System.Windows.Forms.Label SalePrice;
        private System.Windows.Forms.Label labelSaleOrder;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelSaleBasePrice;
        private System.Windows.Forms.Label labelMateriel;
        private System.Windows.Forms.Label labelCustom;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelSaleInvoice;
        private System.Windows.Forms.Label labelSaleCountByCustom;
        private System.Windows.Forms.Label labelSaleCountByProducts;
        private System.Windows.Forms.Label labelInventoryHistory;



    }
}