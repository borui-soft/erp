namespace MainProgram
{
    partial class FormBaseStaff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseStaff));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.add = new System.Windows.Forms.ToolStripButton();
            this.modify = new System.Windows.Forms.ToolStripButton();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.forbid = new System.Windows.Forms.ToolStripButton();
            this.noForbid = new System.Windows.Forms.ToolStripButton();
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
            this.treeViewStaffOrg = new System.Windows.Forms.TreeView();
            this.contextMenuStripStaffGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuppierGroupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.labelStaffGroupName = new System.Windows.Forms.Label();
            this.dataGridViewStaffList = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddStaff = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifyStaff = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteStaff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemForbidStaff = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNoForbidStaff = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripStaffGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffList)).BeginInit();
            this.contextMenuStripDataGridView.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add,
            this.modify,
            this.delete,
            this.toolStripSeparator1,
            this.forbid,
            this.noForbid,
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
            this.toolStrip1.Size = new System.Drawing.Size(1077, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // add
            // 
            this.add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.add.Image = global::MainProgram.Properties.Resources.add;
            this.add.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.add.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(36, 42);
            this.add.Text = "新增";
            this.add.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.add.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // modify
            // 
            this.modify.Image = global::MainProgram.Properties.Resources.modify;
            this.modify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.modify.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(36, 42);
            this.modify.Text = "修改";
            this.modify.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.modify.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Image = global::MainProgram.Properties.Resources.delete;
            this.delete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.delete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(36, 42);
            this.delete.Text = "删除";
            this.delete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.delete.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // forbid
            // 
            this.forbid.Image = global::MainProgram.Properties.Resources.forbid;
            this.forbid.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.forbid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.forbid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forbid.Name = "forbid";
            this.forbid.Size = new System.Drawing.Size(36, 42);
            this.forbid.Text = "禁用";
            this.forbid.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.forbid.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.forbid.Click += new System.EventHandler(this.forbid_Click);
            // 
            // noForbid
            // 
            this.noForbid.Image = global::MainProgram.Properties.Resources.noForbid;
            this.noForbid.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.noForbid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.noForbid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.noForbid.Name = "noForbid";
            this.noForbid.Size = new System.Drawing.Size(48, 42);
            this.noForbid.Text = "反禁用";
            this.noForbid.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.noForbid.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.noForbid.Click += new System.EventHandler(this.noForbid_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeViewStaffOrg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelStaffGroupName);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewStaffList);
            this.splitContainer1.Size = new System.Drawing.Size(1075, 506);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeViewStaffOrg
            // 
            this.treeViewStaffOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewStaffOrg.ContextMenuStrip = this.contextMenuStripStaffGroup;
            this.treeViewStaffOrg.ImageIndex = 0;
            this.treeViewStaffOrg.ImageList = this.imageList;
            this.treeViewStaffOrg.Location = new System.Drawing.Point(0, 0);
            this.treeViewStaffOrg.Name = "treeViewStaffOrg";
            this.treeViewStaffOrg.SelectedImageIndex = 0;
            this.treeViewStaffOrg.Size = new System.Drawing.Size(240, 506);
            this.treeViewStaffOrg.TabIndex = 2;
            this.treeViewStaffOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewStaffOrg_BeforeSelect);
            this.treeViewStaffOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewStaffOrg_AfterSelect);
            // 
            // contextMenuStripStaffGroup
            // 
            this.contextMenuStripStaffGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SuppierGroupAdd,
            this.SuppierGroupDelete});
            this.contextMenuStripStaffGroup.Name = "contextMenuStripStaffGroup";
            this.contextMenuStripStaffGroup.Size = new System.Drawing.Size(119, 48);
            // 
            // SuppierGroupAdd
            // 
            this.SuppierGroupAdd.BackColor = System.Drawing.Color.Transparent;
            this.SuppierGroupAdd.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.SuppierGroupAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SuppierGroupAdd.Name = "SuppierGroupAdd";
            this.SuppierGroupAdd.Size = new System.Drawing.Size(118, 22);
            this.SuppierGroupAdd.Text = "添加部门";
            this.SuppierGroupAdd.Click += new System.EventHandler(this.SuppierGroupAdd_Click);
            // 
            // SuppierGroupDelete
            // 
            this.SuppierGroupDelete.BackColor = System.Drawing.Color.Transparent;
            this.SuppierGroupDelete.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.SuppierGroupDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SuppierGroupDelete.Name = "SuppierGroupDelete";
            this.SuppierGroupDelete.Size = new System.Drawing.Size(118, 22);
            this.SuppierGroupDelete.Text = "移除部门";
            this.SuppierGroupDelete.Click += new System.EventHandler(this.SuppierGroupDelete_Click);
            // 
            // labelStaffGroupName
            // 
            this.labelStaffGroupName.AutoSize = true;
            this.labelStaffGroupName.Location = new System.Drawing.Point(3, 2);
            this.labelStaffGroupName.Name = "labelStaffGroupName";
            this.labelStaffGroupName.Size = new System.Drawing.Size(77, 12);
            this.labelStaffGroupName.TabIndex = 1;
            this.labelStaffGroupName.Text = "物料分组名称";
            // 
            // dataGridViewStaffList
            // 
            this.dataGridViewStaffList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStaffList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewStaffList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStaffList.ContextMenuStrip = this.contextMenuStripDataGridView;
            this.dataGridViewStaffList.GridColor = System.Drawing.Color.Silver;
            this.dataGridViewStaffList.Location = new System.Drawing.Point(1, 16);
            this.dataGridViewStaffList.MultiSelect = false;
            this.dataGridViewStaffList.Name = "dataGridViewStaffList";
            this.dataGridViewStaffList.ReadOnly = true;
            this.dataGridViewStaffList.RowHeadersWidth = 4;
            this.dataGridViewStaffList.RowTemplate.Height = 23;
            this.dataGridViewStaffList.Size = new System.Drawing.Size(839, 490);
            this.dataGridViewStaffList.TabIndex = 0;
            this.dataGridViewStaffList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewStaffList_CellMouseDown);
            this.dataGridViewStaffList.Click += new System.EventHandler(this.dataGridViewStaffList_Click);
            this.dataGridViewStaffList.DoubleClick += new System.EventHandler(this.dataGridViewStaffList_DoubleClick);
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddStaff,
            this.ToolStripMenuItemModifyStaff,
            this.ToolStripMenuItemDeleteStaff,
            this.toolStripSeparator6,
            this.ToolStripMenuItemForbidStaff,
            this.ToolStripMenuItemNoForbidStaff});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(107, 120);
            // 
            // ToolStripMenuItemAddStaff
            // 
            this.ToolStripMenuItemAddStaff.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemAddStaff.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.ToolStripMenuItemAddStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemAddStaff.Name = "ToolStripMenuItemAddStaff";
            this.ToolStripMenuItemAddStaff.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemAddStaff.Text = "新增";
            this.ToolStripMenuItemAddStaff.Click += new System.EventHandler(this.add_Click);
            // 
            // ToolStripMenuItemModifyStaff
            // 
            this.ToolStripMenuItemModifyStaff.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemModifyStaff.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.ToolStripMenuItemModifyStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemModifyStaff.Name = "ToolStripMenuItemModifyStaff";
            this.ToolStripMenuItemModifyStaff.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemModifyStaff.Text = "修改";
            this.ToolStripMenuItemModifyStaff.Click += new System.EventHandler(this.modify_Click);
            // 
            // ToolStripMenuItemDeleteStaff
            // 
            this.ToolStripMenuItemDeleteStaff.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemDeleteStaff.BackgroundImage = global::MainProgram.Properties.Resources.delete;
            this.ToolStripMenuItemDeleteStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemDeleteStaff.Name = "ToolStripMenuItemDeleteStaff";
            this.ToolStripMenuItemDeleteStaff.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemDeleteStaff.Text = "删除";
            this.ToolStripMenuItemDeleteStaff.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // ToolStripMenuItemForbidStaff
            // 
            this.ToolStripMenuItemForbidStaff.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemForbidStaff.BackgroundImage = global::MainProgram.Properties.Resources.forbid;
            this.ToolStripMenuItemForbidStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemForbidStaff.Name = "ToolStripMenuItemForbidStaff";
            this.ToolStripMenuItemForbidStaff.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemForbidStaff.Text = "禁用";
            this.ToolStripMenuItemForbidStaff.Click += new System.EventHandler(this.forbid_Click);
            // 
            // ToolStripMenuItemNoForbidStaff
            // 
            this.ToolStripMenuItemNoForbidStaff.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemNoForbidStaff.BackgroundImage = global::MainProgram.Properties.Resources.noForbid;
            this.ToolStripMenuItemNoForbidStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemNoForbidStaff.Name = "ToolStripMenuItemNoForbidStaff";
            this.ToolStripMenuItemNoForbidStaff.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemNoForbidStaff.Text = "反禁用";
            this.ToolStripMenuItemNoForbidStaff.Click += new System.EventHandler(this.noForbid_Click);
            // 
            // FormBaseStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1077, 557);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaseStaff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "职员信息管理";
            this.Load += new System.EventHandler(this.FormBaseStaff_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripStaffGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffList)).EndInit();
            this.contextMenuStripDataGridView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton add;
        private System.Windows.Forms.ToolStripButton modify;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton forbid;
        private System.Windows.Forms.ToolStripButton noForbid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewStaffOrg;
        private System.Windows.Forms.DataGridView dataGridViewStaffList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStaffGroup;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupAdd;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupDelete;
        private System.Windows.Forms.Label labelStaffGroupName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddStaff;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyStaff;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteStaff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemForbidStaff;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNoForbidStaff;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}