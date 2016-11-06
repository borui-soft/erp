namespace MainProgram
{
    partial class FormBaseAuxiliaryMaterial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseAuxiliaryMaterial));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.add = new System.Windows.Forms.ToolStripButton();
            this.modify = new System.Windows.Forms.ToolStripButton();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printDisplay = new System.Windows.Forms.ToolStripButton();
            this.print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.labelCustomerGroupName = new System.Windows.Forms.Label();
            this.dataGridViewAuxiliaryMaterialList = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifyCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ToolStripMenuItemDeleteCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuxiliaryMaterialList)).BeginInit();
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
            this.export,
            this.toolStripSeparator3,
            this.printDisplay,
            this.print,
            this.toolStripSeparator4,
            this.close,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(745, 45);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelCustomerGroupName);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewAuxiliaryMaterialList);
            this.splitContainer1.Size = new System.Drawing.Size(743, 437);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(180, 437);
            this.treeView.TabIndex = 2;
            this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewCustomerOrg_BeforeSelect);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCustomerOrg_AfterSelect);
            // 
            // labelCustomerGroupName
            // 
            this.labelCustomerGroupName.AutoSize = true;
            this.labelCustomerGroupName.Location = new System.Drawing.Point(3, 2);
            this.labelCustomerGroupName.Name = "labelCustomerGroupName";
            this.labelCustomerGroupName.Size = new System.Drawing.Size(77, 12);
            this.labelCustomerGroupName.TabIndex = 1;
            this.labelCustomerGroupName.Text = "客户分组名称";
            // 
            // dataGridViewAuxiliaryMaterialList
            // 
            this.dataGridViewAuxiliaryMaterialList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAuxiliaryMaterialList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAuxiliaryMaterialList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAuxiliaryMaterialList.GridColor = System.Drawing.Color.Silver;
            this.dataGridViewAuxiliaryMaterialList.Location = new System.Drawing.Point(0, 16);
            this.dataGridViewAuxiliaryMaterialList.MultiSelect = false;
            this.dataGridViewAuxiliaryMaterialList.Name = "dataGridViewAuxiliaryMaterialList";
            this.dataGridViewAuxiliaryMaterialList.ReadOnly = true;
            this.dataGridViewAuxiliaryMaterialList.RowHeadersWidth = 4;
            this.dataGridViewAuxiliaryMaterialList.RowTemplate.Height = 23;
            this.dataGridViewAuxiliaryMaterialList.Size = new System.Drawing.Size(572, 421);
            this.dataGridViewAuxiliaryMaterialList.TabIndex = 0;
            this.dataGridViewAuxiliaryMaterialList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCustomerList_CellMouseDown);
            this.dataGridViewAuxiliaryMaterialList.Click += new System.EventHandler(this.dataGridViewCustomerList_Click);
            this.dataGridViewAuxiliaryMaterialList.DoubleClick += new System.EventHandler(this.dataGridViewAuxiliaryMaterialList_DoubleClick);
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddCustomer,
            this.ToolStripMenuItemModifyCustomer,
            this.ToolStripMenuItemDeleteCustomer});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(153, 92);
            // 
            // ToolStripMenuItemAddCustomer
            // 
            this.ToolStripMenuItemAddCustomer.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemAddCustomer.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.ToolStripMenuItemAddCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemAddCustomer.Name = "ToolStripMenuItemAddCustomer";
            this.ToolStripMenuItemAddCustomer.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemAddCustomer.Text = "新增";
            this.ToolStripMenuItemAddCustomer.Click += new System.EventHandler(this.add_Click);
            // 
            // ToolStripMenuItemModifyCustomer
            // 
            this.ToolStripMenuItemModifyCustomer.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemModifyCustomer.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.ToolStripMenuItemModifyCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemModifyCustomer.Name = "ToolStripMenuItemModifyCustomer";
            this.ToolStripMenuItemModifyCustomer.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemModifyCustomer.Text = "修改";
            this.ToolStripMenuItemModifyCustomer.Click += new System.EventHandler(this.modify_Click);
            // 
            // ToolStripMenuItemDeleteCustomer
            // 
            this.ToolStripMenuItemDeleteCustomer.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItemDeleteCustomer.BackgroundImage = global::MainProgram.Properties.Resources.delete;
            this.ToolStripMenuItemDeleteCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStripMenuItemDeleteCustomer.Name = "ToolStripMenuItemDeleteCustomer";
            this.ToolStripMenuItemDeleteCustomer.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemDeleteCustomer.Text = "删除";
            this.ToolStripMenuItemDeleteCustomer.Click += new System.EventHandler(this.delete_Click);
            // 
            // FormBaseAuxiliaryMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(745, 488);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaseAuxiliaryMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "辅助资料";
            this.Load += new System.EventHandler(this.FormBaseCustomer_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuxiliaryMaterialList)).EndInit();
            this.contextMenuStripDataGridView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton add;
        private System.Windows.Forms.ToolStripButton modify;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.DataGridView dataGridViewAuxiliaryMaterialList;
        private System.Windows.Forms.Label labelCustomerGroupName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddCustomer;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyCustomer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteCustomer;
    }
}