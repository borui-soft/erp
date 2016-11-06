namespace MainProgram
{
    partial class FormMaterielStorageAmountHistoryInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaterielStorageAmountHistoryInfo));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printDisplay = new System.Windows.Forms.ToolStripButton();
            this.print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewMaterielOrg = new System.Windows.Forms.TreeView();
            this.labelCountInfo = new System.Windows.Forms.Label();
            this.dataGridViewMaterielList = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBoxSerach = new System.Windows.Forms.TextBox();
            this.dateTimePickerHistoryDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterielList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.export,
            this.toolStripSeparator3,
            this.printDisplay,
            this.print,
            this.toolStripSeparator4,
            this.close,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(879, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "_CLOSE.GIF");
            this.imageList.Images.SetKeyName(1, "_OPEN.GIF");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewMaterielOrg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelCountInfo);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewMaterielList);
            this.splitContainer1.Size = new System.Drawing.Size(877, 437);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeViewMaterielOrg
            // 
            this.treeViewMaterielOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewMaterielOrg.ImageIndex = 0;
            this.treeViewMaterielOrg.ImageList = this.imageList;
            this.treeViewMaterielOrg.Location = new System.Drawing.Point(0, 0);
            this.treeViewMaterielOrg.Name = "treeViewMaterielOrg";
            this.treeViewMaterielOrg.SelectedImageIndex = 0;
            this.treeViewMaterielOrg.Size = new System.Drawing.Size(160, 432);
            this.treeViewMaterielOrg.TabIndex = 2;
            this.treeViewMaterielOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaterielOrg_BeforeSelect);
            this.treeViewMaterielOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMaterielOrg_AfterSelect);
            // 
            // labelCountInfo
            // 
            this.labelCountInfo.AutoSize = true;
            this.labelCountInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCountInfo.Location = new System.Drawing.Point(0, 3);
            this.labelCountInfo.Name = "labelCountInfo";
            this.labelCountInfo.Size = new System.Drawing.Size(49, 14);
            this.labelCountInfo.TabIndex = 1;
            this.labelCountInfo.Text = "label1";
            // 
            // dataGridViewMaterielList
            // 
            this.dataGridViewMaterielList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMaterielList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMaterielList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaterielList.GridColor = System.Drawing.Color.Silver;
            this.dataGridViewMaterielList.Location = new System.Drawing.Point(0, 22);
            this.dataGridViewMaterielList.MultiSelect = false;
            this.dataGridViewMaterielList.Name = "dataGridViewMaterielList";
            this.dataGridViewMaterielList.ReadOnly = true;
            this.dataGridViewMaterielList.RowHeadersWidth = 4;
            this.dataGridViewMaterielList.RowTemplate.Height = 23;
            this.dataGridViewMaterielList.Size = new System.Drawing.Size(731, 410);
            this.dataGridViewMaterielList.TabIndex = 0;
            this.dataGridViewMaterielList.Click += new System.EventHandler(this.dataGridViewMaterielList_Click);
            // 
            // textBoxSerach
            // 
            this.textBoxSerach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxSerach.Location = new System.Drawing.Point(655, 13);
            this.textBoxSerach.Name = "textBoxSerach";
            this.textBoxSerach.Size = new System.Drawing.Size(211, 21);
            this.textBoxSerach.TabIndex = 18;
            this.textBoxSerach.Text = "输入物料名称，按回车键实现快速查找";
            this.textBoxSerach.Click += new System.EventHandler(this.textBoxSerach_Click);
            this.textBoxSerach.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSerach_MouseClick);
            this.textBoxSerach.DoubleClick += new System.EventHandler(this.textBoxSerach_Click);
            this.textBoxSerach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSerach_KeyDown);
            // 
            // dateTimePickerHistoryDate
            // 
            this.dateTimePickerHistoryDate.Location = new System.Drawing.Point(412, 13);
            this.dateTimePickerHistoryDate.Name = "dateTimePickerHistoryDate";
            this.dateTimePickerHistoryDate.Size = new System.Drawing.Size(152, 21);
            this.dateTimePickerHistoryDate.TabIndex = 19;
            this.dateTimePickerHistoryDate.CloseUp += new System.EventHandler(this.dateTimePickerHistoryDate_CloseUp);
            this.dateTimePickerHistoryDate.ValueChanged += new System.EventHandler(this.dateTimePickerHistoryDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "截止日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(599, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "物料名称";
            // 
            // FormMaterielStorageAmountHistoryInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(879, 488);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerHistoryDate);
            this.Controls.Add(this.textBoxSerach);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMaterielStorageAmountHistoryInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "物料库存历史信息";
            this.Load += new System.EventHandler(this.FormMaterielStorageAmountHistoryInfo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterielList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewMaterielOrg;
        private System.Windows.Forms.DataGridView dataGridViewMaterielList;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelCountInfo;
        private System.Windows.Forms.TextBox textBoxSerach;
        private System.Windows.Forms.DateTimePicker dateTimePickerHistoryDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}