namespace MainProgram
{
    partial class FormDisplayCountInfoFromSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplayCountInfoFromSQL));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.billDetail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printDisplay = new System.Windows.Forms.ToolStripButton();
            this.print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCountInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelMaterieInOutlCountInfo = new System.Windows.Forms.Label();
            this.toolStripButtonRefreshHS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.toolStripButtonRefreshHS,
            this.toolStripSeparator5,
            this.close,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(967, 45);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // billDetail
            // 
            this.billDetail.Image = global::MainProgram.Properties.Resources.Filter;
            this.billDetail.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.billDetail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.billDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.billDetail.Name = "billDetail";
            this.billDetail.Size = new System.Drawing.Size(84, 42);
            this.billDetail.Text = "更多过滤条件";
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
            // dataGridViewList
            // 
            this.dataGridViewList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Location = new System.Drawing.Point(0, 48);
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.RowHeadersWidth = 4;
            this.dataGridViewList.RowTemplate.Height = 23;
            this.dataGridViewList.Size = new System.Drawing.Size(967, 435);
            this.dataGridViewList.TabIndex = 3;
            this.dataGridViewList.Click += new System.EventHandler(this.dataGridViewBilConfigList_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCountInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(967, 22);
            this.statusStrip1.TabIndex = 28;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCountInfo
            // 
            this.toolStripStatusLabelCountInfo.Name = "toolStripStatusLabelCountInfo";
            this.toolStripStatusLabelCountInfo.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabelCountInfo.Text = "toolStripStatusLabel1";
            // 
            // labelMaterieInOutlCountInfo
            // 
            this.labelMaterieInOutlCountInfo.AutoSize = true;
            this.labelMaterieInOutlCountInfo.Location = new System.Drawing.Point(579, 31);
            this.labelMaterieInOutlCountInfo.Name = "labelMaterieInOutlCountInfo";
            this.labelMaterieInOutlCountInfo.Size = new System.Drawing.Size(41, 12);
            this.labelMaterieInOutlCountInfo.TabIndex = 29;
            this.labelMaterieInOutlCountInfo.Text = "label1";
            // 
            // toolStripButtonRefreshHS
            // 
            this.toolStripButtonRefreshHS.Image = global::MainProgram.Properties.Resources.review;
            this.toolStripButtonRefreshHS.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButtonRefreshHS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonRefreshHS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefreshHS.Name = "toolStripButtonRefreshHS";
            this.toolStripButtonRefreshHS.Size = new System.Drawing.Size(36, 42);
            this.toolStripButtonRefreshHS.Text = "刷新";
            this.toolStripButtonRefreshHS.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonRefreshHS.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButtonRefreshHS.Click += new System.EventHandler(this.toolStripButtonRefreshHS_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // FormDisplayCountInfoFromSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 480);
            this.Controls.Add(this.labelMaterieInOutlCountInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDisplayCountInfoFromSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "采购信息统计";
            this.Load += new System.EventHandler(this.FormPurchaseInfoCount_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton billDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCountInfo;
        private System.Windows.Forms.Label labelMaterieInOutlCountInfo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshHS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}