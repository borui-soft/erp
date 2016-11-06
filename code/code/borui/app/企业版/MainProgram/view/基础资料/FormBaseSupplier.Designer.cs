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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewSupplierOrg = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(678, 50);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // add
            // 
            this.add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(36, 47);
            this.add.Text = "新增";
            this.add.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // modify
            // 
            this.modify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modify.Image = ((System.Drawing.Image)(resources.GetObject("modify.Image")));
            this.modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(36, 47);
            this.modify.Text = "修改";
            this.modify.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(36, 47);
            this.delete.Text = "删除";
            this.delete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // forbid
            // 
            this.forbid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.forbid.Image = ((System.Drawing.Image)(resources.GetObject("forbid.Image")));
            this.forbid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forbid.Name = "forbid";
            this.forbid.Size = new System.Drawing.Size(36, 47);
            this.forbid.Text = "禁用";
            this.forbid.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.forbid.Click += new System.EventHandler(this.forbid_Click);
            // 
            // noForbid
            // 
            this.noForbid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.noForbid.Image = ((System.Drawing.Image)(resources.GetObject("noForbid.Image")));
            this.noForbid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.noForbid.Name = "noForbid";
            this.noForbid.Size = new System.Drawing.Size(48, 47);
            this.noForbid.Text = "反禁用";
            this.noForbid.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.noForbid.Click += new System.EventHandler(this.noForbid_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // export
            // 
            this.export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.export.Image = ((System.Drawing.Image)(resources.GetObject("export.Image")));
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(60, 47);
            this.export.Text = "数据导出";
            this.export.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 50);
            // 
            // printDisplay
            // 
            this.printDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.printDisplay.Image = ((System.Drawing.Image)(resources.GetObject("printDisplay.Image")));
            this.printDisplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printDisplay.Name = "printDisplay";
            this.printDisplay.Size = new System.Drawing.Size(60, 47);
            this.printDisplay.Text = "打印预览";
            this.printDisplay.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.printDisplay.Click += new System.EventHandler(this.printDisplay_Click);
            // 
            // print
            // 
            this.print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.print.Image = ((System.Drawing.Image)(resources.GetObject("print.Image")));
            this.print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(36, 47);
            this.print.Text = "打印";
            this.print.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 50);
            // 
            // close
            // 
            this.close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.close.Image = ((System.Drawing.Image)(resources.GetObject("close.Image")));
            this.close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(36, 47);
            this.close.Text = "关闭";
            this.close.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 50);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "_CLOSE.GIF");
            this.imageList.Images.SetKeyName(1, "_OPEN.GIF");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::MainProgram.Properties.Resources.add;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(17, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(17, 22);
            this.panel1.TabIndex = 11;
            this.panel1.Click += new System.EventHandler(this.add_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::MainProgram.Properties.Resources.modify;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Location = new System.Drawing.Point(53, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(17, 22);
            this.panel2.TabIndex = 12;
            this.panel2.Click += new System.EventHandler(this.modify_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::MainProgram.Properties.Resources.delete;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel3.Location = new System.Drawing.Point(89, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(17, 22);
            this.panel3.TabIndex = 13;
            this.panel3.Click += new System.EventHandler(this.delete_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewSupplierOrg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(676, 384);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeViewSupplierOrg
            // 
            this.treeViewSupplierOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSupplierOrg.ImageIndex = 0;
            this.treeViewSupplierOrg.ImageList = this.imageList;
            this.treeViewSupplierOrg.Location = new System.Drawing.Point(0, 0);
            this.treeViewSupplierOrg.Name = "treeViewSupplierOrg";
            this.treeViewSupplierOrg.SelectedImageIndex = 0;
            this.treeViewSupplierOrg.Size = new System.Drawing.Size(179, 384);
            this.treeViewSupplierOrg.TabIndex = 2;
            this.treeViewSupplierOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSupplierOrg_BeforeSelect);
            this.treeViewSupplierOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSupplierOrg_AfterSelect);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(490, 384);
            this.dataGridView1.TabIndex = 0;
            // 
            // FormBaseSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 437);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormBaseSupplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "供应商";
            this.Load += new System.EventHandler(this.FormBaseSupplier_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewSupplierOrg;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}