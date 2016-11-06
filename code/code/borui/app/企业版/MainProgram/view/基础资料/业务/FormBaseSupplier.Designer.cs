namespace MainProgram
{
    partial class FormBaseSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseSupplier));
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
            this.treeViewSupplierOrg = new System.Windows.Forms.TreeView();
            this.contextMenuStripSupplierGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuppierGroupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupModify = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSupplierGroupName = new System.Windows.Forms.Label();
            this.dataGridViewSupplierList = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifySupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemForbidSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNoForbidSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripSupplierGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSupplierList)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(879, 45);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeViewSupplierOrg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelSupplierGroupName);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewSupplierList);
            this.splitContainer1.Size = new System.Drawing.Size(877, 437);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeViewSupplierOrg
            // 
            this.treeViewSupplierOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSupplierOrg.ContextMenuStrip = this.contextMenuStripSupplierGroup;
            this.treeViewSupplierOrg.ImageIndex = 0;
            this.treeViewSupplierOrg.ImageList = this.imageList;
            this.treeViewSupplierOrg.Location = new System.Drawing.Point(0, 0);
            this.treeViewSupplierOrg.Name = "treeViewSupplierOrg";
            this.treeViewSupplierOrg.SelectedImageIndex = 0;
            this.treeViewSupplierOrg.Size = new System.Drawing.Size(160, 437);
            this.treeViewSupplierOrg.TabIndex = 2;
            this.treeViewSupplierOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSupplierOrg_BeforeSelect);
            this.treeViewSupplierOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSupplierOrg_AfterSelect);
            // 
            // contextMenuStripSupplierGroup
            // 
            this.contextMenuStripSupplierGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SuppierGroupAdd,
            this.SuppierGroupModify,
            this.SuppierGroupDelete});
            this.contextMenuStripSupplierGroup.Name = "contextMenuStripSupplierGroup";
            this.contextMenuStripSupplierGroup.Size = new System.Drawing.Size(101, 70);
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
            // labelSupplierGroupName
            // 
            this.labelSupplierGroupName.AutoSize = true;
            this.labelSupplierGroupName.Location = new System.Drawing.Point(3, 2);
            this.labelSupplierGroupName.Name = "labelSupplierGroupName";
            this.labelSupplierGroupName.Size = new System.Drawing.Size(89, 12);
            this.labelSupplierGroupName.TabIndex = 1;
            this.labelSupplierGroupName.Text = "供应商分组名称";
            // 
            // dataGridViewSupplierList
            // 
            this.dataGridViewSupplierList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSupplierList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSupplierList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSupplierList.GridColor = System.Drawing.Color.Silver;
            this.dataGridViewSupplierList.Location = new System.Drawing.Point(0, 16);
            this.dataGridViewSupplierList.MultiSelect = false;
            this.dataGridViewSupplierList.Name = "dataGridViewSupplierList";
            this.dataGridViewSupplierList.ReadOnly = true;
            this.dataGridViewSupplierList.RowHeadersWidth = 4;
            this.dataGridViewSupplierList.RowTemplate.Height = 23;
            this.dataGridViewSupplierList.Size = new System.Drawing.Size(753, 421);
            this.dataGridViewSupplierList.TabIndex = 0;
            this.dataGridViewSupplierList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSupplierList_CellMouseDown);
            this.dataGridViewSupplierList.Click += new System.EventHandler(this.dataGridViewSupplierList_Click);
            this.dataGridViewSupplierList.DoubleClick += new System.EventHandler(this.dataGridViewSupplierList_DoubleClick);
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddSupplier,
            this.ToolStripMenuItemModifySupplier,
            this.ToolStripMenuItemDeleteSupplier,
            this.toolStripSeparator6,
            this.ToolStripMenuItemForbidSupplier,
            this.ToolStripMenuItemNoForbidSupplier});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(153, 142);
            // 
            // ToolStripMenuItemAddSupplier
            // 
            this.ToolStripMenuItemAddSupplier.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemAddSupplier.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.ToolStripMenuItemAddSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemAddSupplier.Name = "ToolStripMenuItemAddSupplier";
            this.ToolStripMenuItemAddSupplier.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemAddSupplier.Text = "新增";
            this.ToolStripMenuItemAddSupplier.Click += new System.EventHandler(this.add_Click);
            // 
            // ToolStripMenuItemModifySupplier
            // 
            this.ToolStripMenuItemModifySupplier.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemModifySupplier.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.ToolStripMenuItemModifySupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemModifySupplier.Name = "ToolStripMenuItemModifySupplier";
            this.ToolStripMenuItemModifySupplier.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemModifySupplier.Text = "修改";
            this.ToolStripMenuItemModifySupplier.Click += new System.EventHandler(this.modify_Click);
            // 
            // ToolStripMenuItemDeleteSupplier
            // 
            this.ToolStripMenuItemDeleteSupplier.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemDeleteSupplier.BackgroundImage = global::MainProgram.Properties.Resources.delete;
            this.ToolStripMenuItemDeleteSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemDeleteSupplier.Name = "ToolStripMenuItemDeleteSupplier";
            this.ToolStripMenuItemDeleteSupplier.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemDeleteSupplier.Text = "删除";
            this.ToolStripMenuItemDeleteSupplier.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // ToolStripMenuItemForbidSupplier
            // 
            this.ToolStripMenuItemForbidSupplier.BackgroundImage = global::MainProgram.Properties.Resources.forbid;
            this.ToolStripMenuItemForbidSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemForbidSupplier.Name = "ToolStripMenuItemForbidSupplier";
            this.ToolStripMenuItemForbidSupplier.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemForbidSupplier.Text = "禁用";
            this.ToolStripMenuItemForbidSupplier.Click += new System.EventHandler(this.forbid_Click);
            // 
            // ToolStripMenuItemNoForbidSupplier
            // 
            this.ToolStripMenuItemNoForbidSupplier.BackgroundImage = global::MainProgram.Properties.Resources.noForbid;
            this.ToolStripMenuItemNoForbidSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemNoForbidSupplier.Name = "ToolStripMenuItemNoForbidSupplier";
            this.ToolStripMenuItemNoForbidSupplier.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemNoForbidSupplier.Text = "反禁用";
            this.ToolStripMenuItemNoForbidSupplier.Click += new System.EventHandler(this.noForbid_Click);
            // 
            // FormBaseSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(879, 488);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaseSupplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "供应商";
            this.Load += new System.EventHandler(this.FormBaseSupplier_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripSupplierGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSupplierList)).EndInit();
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
        private System.Windows.Forms.TreeView treeViewSupplierOrg;
        private System.Windows.Forms.DataGridView dataGridViewSupplierList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSupplierGroup;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupAdd;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupModify;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupDelete;
        private System.Windows.Forms.Label labelSupplierGroupName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddSupplier;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifySupplier;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteSupplier;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemForbidSupplier;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNoForbidSupplier;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}