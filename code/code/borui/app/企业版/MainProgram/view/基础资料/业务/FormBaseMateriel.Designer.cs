namespace MainProgram
{
    partial class FormBaseMateriel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseMateriel));
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
            this.treeViewMaterielOrg = new System.Windows.Forms.TreeView();
            this.contextMenuStripMaterielGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuppierGroupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupModify = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.labelMaterielGroupName = new System.Windows.Forms.Label();
            this.dataGridViewMaterielList = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddMateriel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifyMateriel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteMateriel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemForbidMateriel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNoForbidMateriel = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBoxSerach = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripMaterielGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterielList)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(1086, 45);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeViewMaterielOrg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelMaterielGroupName);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewMaterielList);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 505);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeViewMaterielOrg
            // 
            this.treeViewMaterielOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewMaterielOrg.ContextMenuStrip = this.contextMenuStripMaterielGroup;
            this.treeViewMaterielOrg.ImageIndex = 0;
            this.treeViewMaterielOrg.ImageList = this.imageList;
            this.treeViewMaterielOrg.Location = new System.Drawing.Point(0, 0);
            this.treeViewMaterielOrg.Name = "treeViewMaterielOrg";
            this.treeViewMaterielOrg.SelectedImageIndex = 0;
            this.treeViewMaterielOrg.Size = new System.Drawing.Size(234, 505);
            this.treeViewMaterielOrg.TabIndex = 2;
            this.treeViewMaterielOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaterielOrg_BeforeSelect);
            this.treeViewMaterielOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMaterielOrg_AfterSelect);
            // 
            // contextMenuStripMaterielGroup
            // 
            this.contextMenuStripMaterielGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SuppierGroupAdd,
            this.SuppierGroupModify,
            this.SuppierGroupDelete});
            this.contextMenuStripMaterielGroup.Name = "contextMenuStripMaterielGroup";
            this.contextMenuStripMaterielGroup.Size = new System.Drawing.Size(101, 70);
            // 
            // SuppierGroupAdd
            // 
            this.SuppierGroupAdd.BackColor = System.Drawing.Color.Transparent;
            this.SuppierGroupAdd.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.SuppierGroupAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SuppierGroupAdd.Name = "SuppierGroupAdd";
            this.SuppierGroupAdd.Size = new System.Drawing.Size(100, 22);
            this.SuppierGroupAdd.Text = "新增";
            this.SuppierGroupAdd.Click += new System.EventHandler(this.add_Click);
            // 
            // SuppierGroupModify
            // 
            this.SuppierGroupModify.BackColor = System.Drawing.Color.Transparent;
            this.SuppierGroupModify.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.SuppierGroupModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SuppierGroupModify.Name = "SuppierGroupModify";
            this.SuppierGroupModify.Size = new System.Drawing.Size(100, 22);
            this.SuppierGroupModify.Text = "修改";
            this.SuppierGroupModify.Click += new System.EventHandler(this.SuppierGroupModify_Click);
            // 
            // SuppierGroupDelete
            // 
            this.SuppierGroupDelete.BackColor = System.Drawing.Color.Transparent;
            this.SuppierGroupDelete.BackgroundImage = global::MainProgram.Properties.Resources.delete;
            this.SuppierGroupDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SuppierGroupDelete.Name = "SuppierGroupDelete";
            this.SuppierGroupDelete.Size = new System.Drawing.Size(100, 22);
            this.SuppierGroupDelete.Text = "删除";
            this.SuppierGroupDelete.Click += new System.EventHandler(this.SuppierGroupDelete_Click);
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
            this.dataGridViewMaterielList.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewMaterielList.MultiSelect = false;
            this.dataGridViewMaterielList.Name = "dataGridViewMaterielList";
            this.dataGridViewMaterielList.ReadOnly = true;
            this.dataGridViewMaterielList.RowHeadersWidth = 4;
            this.dataGridViewMaterielList.RowTemplate.Height = 23;
            this.dataGridViewMaterielList.Size = new System.Drawing.Size(856, 489);
            this.dataGridViewMaterielList.TabIndex = 0;
            this.dataGridViewMaterielList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMaterielList_CellMouseDown);
            this.dataGridViewMaterielList.Click += new System.EventHandler(this.dataGridViewMaterielList_Click);
            this.dataGridViewMaterielList.DoubleClick += new System.EventHandler(this.dataGridViewMaterielList_DoubleClick);
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddMateriel,
            this.ToolStripMenuItemModifyMateriel,
            this.ToolStripMenuItemDeleteMateriel,
            this.toolStripSeparator6,
            this.ToolStripMenuItemForbidMateriel,
            this.ToolStripMenuItemNoForbidMateriel});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(113, 120);
            // 
            // ToolStripMenuItemAddMateriel
            // 
            this.ToolStripMenuItemAddMateriel.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemAddMateriel.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.ToolStripMenuItemAddMateriel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemAddMateriel.Name = "ToolStripMenuItemAddMateriel";
            this.ToolStripMenuItemAddMateriel.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemAddMateriel.Text = "新增";
            this.ToolStripMenuItemAddMateriel.Click += new System.EventHandler(this.add_Click);
            // 
            // ToolStripMenuItemModifyMateriel
            // 
            this.ToolStripMenuItemModifyMateriel.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemModifyMateriel.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.ToolStripMenuItemModifyMateriel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemModifyMateriel.Name = "ToolStripMenuItemModifyMateriel";
            this.ToolStripMenuItemModifyMateriel.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemModifyMateriel.Text = "修改";
            this.ToolStripMenuItemModifyMateriel.Click += new System.EventHandler(this.modify_Click);
            // 
            // ToolStripMenuItemDeleteMateriel
            // 
            this.ToolStripMenuItemDeleteMateriel.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemDeleteMateriel.BackgroundImage = global::MainProgram.Properties.Resources.delete;
            this.ToolStripMenuItemDeleteMateriel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemDeleteMateriel.Name = "ToolStripMenuItemDeleteMateriel";
            this.ToolStripMenuItemDeleteMateriel.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemDeleteMateriel.Text = "删除";
            this.ToolStripMenuItemDeleteMateriel.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(109, 6);
            // 
            // ToolStripMenuItemForbidMateriel
            // 
            this.ToolStripMenuItemForbidMateriel.BackgroundImage = global::MainProgram.Properties.Resources.forbid;
            this.ToolStripMenuItemForbidMateriel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemForbidMateriel.Name = "ToolStripMenuItemForbidMateriel";
            this.ToolStripMenuItemForbidMateriel.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemForbidMateriel.Text = "禁用";
            this.ToolStripMenuItemForbidMateriel.Click += new System.EventHandler(this.forbid_Click);
            // 
            // ToolStripMenuItemNoForbidMateriel
            // 
            this.ToolStripMenuItemNoForbidMateriel.BackgroundImage = global::MainProgram.Properties.Resources.noForbid;
            this.ToolStripMenuItemNoForbidMateriel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemNoForbidMateriel.Name = "ToolStripMenuItemNoForbidMateriel";
            this.ToolStripMenuItemNoForbidMateriel.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemNoForbidMateriel.Text = "反禁用";
            this.ToolStripMenuItemNoForbidMateriel.Click += new System.EventHandler(this.noForbid_Click);
            // 
            // textBoxSerach
            // 
            this.textBoxSerach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxSerach.Location = new System.Drawing.Point(777, 21);
            this.textBoxSerach.Name = "textBoxSerach";
            this.textBoxSerach.Size = new System.Drawing.Size(297, 21);
            this.textBoxSerach.TabIndex = 17;
            this.textBoxSerach.Text = "输入物料名称或编码或助记码，按回车键实现快速查找";
            this.textBoxSerach.Click += new System.EventHandler(this.textBoxSerach_Click);
            this.textBoxSerach.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSerach_Click);
            this.textBoxSerach.DoubleClick += new System.EventHandler(this.textBoxSerach_Click);
            this.textBoxSerach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSerach_KeyDown);
            // 
            // FormBaseMateriel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1086, 556);
            this.Controls.Add(this.textBoxSerach);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaseMateriel";
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
            this.contextMenuStripMaterielGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterielList)).EndInit();
            this.contextMenuStripDataGridView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewMaterielOrg;
        private System.Windows.Forms.DataGridView dataGridViewMaterielList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMaterielGroup;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupAdd;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupModify;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupDelete;
        private System.Windows.Forms.Label labelMaterielGroupName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddMateriel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyMateriel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteMateriel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemForbidMateriel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNoForbidMateriel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TextBox textBoxSerach;
    }
}