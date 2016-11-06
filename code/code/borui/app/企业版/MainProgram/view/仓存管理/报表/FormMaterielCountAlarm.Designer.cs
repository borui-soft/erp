namespace MainProgram
{
    partial class FormMaterielCountAlarm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaterielCountAlarm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.filter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.labelMaterielGroupName = new System.Windows.Forms.Label();
            this.dataGridViewMaterielList = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.filter,
            this.toolStripSeparator2,
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
            // filter
            // 
            this.filter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.filter.Image = global::MainProgram.Properties.Resources.Filter;
            this.filter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.filter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.filter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(36, 42);
            this.filter.Text = "差值";
            this.filter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.filter.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.filter.ToolTipText = "差值";
            this.filter.Click += new System.EventHandler(this.filter_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.labelMaterielGroupName);
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
            this.treeViewMaterielOrg.Size = new System.Drawing.Size(160, 437);
            this.treeViewMaterielOrg.TabIndex = 2;
            this.treeViewMaterielOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaterielOrg_BeforeSelect);
            this.treeViewMaterielOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMaterielOrg_AfterSelect);
            // 
            // labelMaterielGroupName
            // 
            this.labelMaterielGroupName.AutoSize = true;
            this.labelMaterielGroupName.Location = new System.Drawing.Point(3, 2);
            this.labelMaterielGroupName.Name = "labelMaterielGroupName";
            this.labelMaterielGroupName.Size = new System.Drawing.Size(77, 12);
            this.labelMaterielGroupName.TabIndex = 1;
            this.labelMaterielGroupName.Text = "物料分组名称";
            // 
            // dataGridViewMaterielList
            // 
            this.dataGridViewMaterielList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMaterielList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMaterielList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaterielList.GridColor = System.Drawing.Color.Silver;
            this.dataGridViewMaterielList.Location = new System.Drawing.Point(0, 16);
            this.dataGridViewMaterielList.MultiSelect = false;
            this.dataGridViewMaterielList.Name = "dataGridViewMaterielList";
            this.dataGridViewMaterielList.ReadOnly = true;
            this.dataGridViewMaterielList.RowHeadersWidth = 4;
            this.dataGridViewMaterielList.RowTemplate.Height = 23;
            this.dataGridViewMaterielList.Size = new System.Drawing.Size(735, 421);
            this.dataGridViewMaterielList.TabIndex = 0;
            this.dataGridViewMaterielList.Click += new System.EventHandler(this.dataGridViewMaterielList_Click);
            // 
            // FormMaterielCountAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(879, 488);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMaterielCountAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "物料";
            this.Load += new System.EventHandler(this.FormBaseMateriel_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterielList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton filter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewMaterielOrg;
        private System.Windows.Forms.DataGridView dataGridViewMaterielList;
        private System.Windows.Forms.Label labelMaterielGroupName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}