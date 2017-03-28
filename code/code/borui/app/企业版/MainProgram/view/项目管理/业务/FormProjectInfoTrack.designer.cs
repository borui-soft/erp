namespace MainProgram
{
    partial class FormProjectInfoTrack
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProjectInfoTrack));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.billDetail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printDisplay = new System.Windows.Forms.ToolStripButton();
            this.print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemProOccupied = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemToApply = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemToProduce = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemPurchaseApplyOrderInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPurchaseOrderInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPurchaseInOrderInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemMaterielOutOrderInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.projectRowMergeView = new RowMergeView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStripDataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectRowMergeView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.billDetail,
            this.toolStripSeparator2,
            this.export,
            this.toolStripSeparator3,
            this.printDisplay,
            this.print,
            this.toolStripSeparator4,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.close,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1330, 45);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // billDetail
            // 
            this.billDetail.Image = global::MainProgram.Properties.Resources.search;
            this.billDetail.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.billDetail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.billDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.billDetail.Name = "billDetail";
            this.billDetail.Size = new System.Drawing.Size(84, 42);
            this.billDetail.Text = "查看关联单据";
            this.billDetail.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.billDetail.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.billDetail.Click += new System.EventHandler(this.billDetail_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // export
            // 
            this.export.Image = global::MainProgram.Properties.Resources.export;
            this.export.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(60, 42);
            this.export.Text = "数据导出";
            this.export.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.export.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // printDisplay
            // 
            this.printDisplay.Image = global::MainProgram.Properties.Resources.Printers;
            this.printDisplay.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.printDisplay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.printDisplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printDisplay.Name = "printDisplay";
            this.printDisplay.Size = new System.Drawing.Size(60, 42);
            this.printDisplay.Text = "打印预览";
            this.printDisplay.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.printDisplay.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.printDisplay.Click += new System.EventHandler(this.print_Click);
            // 
            // print
            // 
            this.print.Image = global::MainProgram.Properties.Resources.Printers2;
            this.print.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.print.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(36, 42);
            this.print.Text = "打印";
            this.print.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.print.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::MainProgram.Properties.Resources.review;
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 42);
            this.toolStripButton1.Text = "刷新";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButton1.ToolTipText = "刷新";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // close
            // 
            this.close.Image = global::MainProgram.Properties.Resources.close;
            this.close.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.close.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(36, 42);
            this.close.Text = "关闭";
            this.close.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.close.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 45);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 722);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1330, 22);
            this.statusStrip.TabIndex = 29;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemProOccupied,
            this.ToolStripMenuItemToApply,
            this.toolStripMenuItemToProduce,
            this.toolStripSeparator7,
            this.ToolStripMenuItemPurchaseApplyOrderInfo,
            this.ToolStripMenuItemPurchaseOrderInfo,
            this.toolStripMenuItemPurchaseInOrderInfo,
            this.toolStripSeparator6,
            this.ToolStripMenuItemMaterielOutOrderInfo});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(149, 170);
            // 
            // toolStripMenuItemProOccupied
            // 
            this.toolStripMenuItemProOccupied.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItemProOccupied.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItemProOccupied.Enabled = false;
            this.toolStripMenuItemProOccupied.Name = "toolStripMenuItemProOccupied";
            this.toolStripMenuItemProOccupied.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItemProOccupied.Text = "转库存预占";
            this.toolStripMenuItemProOccupied.Click += new System.EventHandler(this.toolStripMenuItemProOccupied_Click);
            // 
            // ToolStripMenuItemToApply
            // 
            this.ToolStripMenuItemToApply.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemToApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemToApply.Name = "ToolStripMenuItemToApply";
            this.ToolStripMenuItemToApply.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemToApply.Text = "转采购申请";
            this.ToolStripMenuItemToApply.Click += new System.EventHandler(this.ToolStripMenuItemToApply_Click);
            // 
            // toolStripMenuItemToProduce
            // 
            this.toolStripMenuItemToProduce.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItemToProduce.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItemToProduce.Name = "toolStripMenuItemToProduce";
            this.toolStripMenuItemToProduce.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItemToProduce.Text = "转生产领料";
            this.toolStripMenuItemToProduce.Click += new System.EventHandler(this.toolStripMenuItemToProduce_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(145, 6);
            // 
            // ToolStripMenuItemPurchaseApplyOrderInfo
            // 
            this.ToolStripMenuItemPurchaseApplyOrderInfo.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemPurchaseApplyOrderInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemPurchaseApplyOrderInfo.Name = "ToolStripMenuItemPurchaseApplyOrderInfo";
            this.ToolStripMenuItemPurchaseApplyOrderInfo.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemPurchaseApplyOrderInfo.Text = "采购申请详情";
            this.ToolStripMenuItemPurchaseApplyOrderInfo.Click += new System.EventHandler(this.ToolStripMenuItemPurchaseApplyOrderInfo_Click);
            // 
            // ToolStripMenuItemPurchaseOrderInfo
            // 
            this.ToolStripMenuItemPurchaseOrderInfo.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemPurchaseOrderInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemPurchaseOrderInfo.Name = "ToolStripMenuItemPurchaseOrderInfo";
            this.ToolStripMenuItemPurchaseOrderInfo.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemPurchaseOrderInfo.Text = "采购订单详情";
            this.ToolStripMenuItemPurchaseOrderInfo.Click += new System.EventHandler(this.ToolStripMenuItemPurchaseOrderInfo_Click);
            // 
            // toolStripMenuItemPurchaseInOrderInfo
            // 
            this.toolStripMenuItemPurchaseInOrderInfo.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItemPurchaseInOrderInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItemPurchaseInOrderInfo.Name = "toolStripMenuItemPurchaseInOrderInfo";
            this.toolStripMenuItemPurchaseInOrderInfo.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItemPurchaseInOrderInfo.Text = "采购入库详情";
            this.toolStripMenuItemPurchaseInOrderInfo.Click += new System.EventHandler(this.toolStripMenuItemPurchaseInOrderInfo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(145, 6);
            // 
            // ToolStripMenuItemMaterielOutOrderInfo
            // 
            this.ToolStripMenuItemMaterielOutOrderInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemMaterielOutOrderInfo.Name = "ToolStripMenuItemMaterielOutOrderInfo";
            this.ToolStripMenuItemMaterielOutOrderInfo.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemMaterielOutOrderInfo.Text = "生产领料详情";
            this.ToolStripMenuItemMaterielOutOrderInfo.Click += new System.EventHandler(this.ToolStripMenuItemMaterielOutOrderInfo_Click);
            // 
            // projectRowMergeView
            // 
            this.projectRowMergeView.AllowUserToAddRows = false;
            this.projectRowMergeView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.projectRowMergeView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.projectRowMergeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projectRowMergeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectRowMergeView.Location = new System.Drawing.Point(0, 45);
            this.projectRowMergeView.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.projectRowMergeView.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("projectRowMergeView.MergeColumnNames")));
            this.projectRowMergeView.Name = "projectRowMergeView";
            this.projectRowMergeView.ReadOnly = true;
            this.projectRowMergeView.RowTemplate.Height = 23;
            this.projectRowMergeView.Size = new System.Drawing.Size(1330, 699);
            this.projectRowMergeView.TabIndex = 4;
            this.projectRowMergeView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.rowMergeView1_CellClick);
            this.projectRowMergeView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.projectRowMergeView_CellMouseDown);
            this.projectRowMergeView.DoubleClick += new System.EventHandler(this.projectRowMergeView_DoubleClick);
            // 
            // FormProjectInfoTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 744);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.projectRowMergeView);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormProjectInfoTrack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XXX材料表跟踪情况";
            this.Load += new System.EventHandler(this.FormProjectInfoTrack_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripDataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectRowMergeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton billDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private RowMergeView projectRowMergeView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemToApply;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPurchaseApplyOrderInfo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPurchaseOrderInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPurchaseInOrderInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMaterielOutOrderInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemToProduce;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProOccupied;
    }
}