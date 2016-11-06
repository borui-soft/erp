namespace MainProgram
{
    partial class FormSaleOrderSequence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSaleOrderSequence));
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
            this.panelExport = new System.Windows.Forms.Panel();
            this.panelPrintDisplay = new System.Windows.Forms.Panel();
            this.panelPrint = new System.Windows.Forms.Panel();
            this.panelImageExit = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
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
            this.close,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(967, 50);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // billDetail
            // 
            this.billDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.billDetail.Image = ((System.Drawing.Image)(resources.GetObject("billDetail.Image")));
            this.billDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.billDetail.Name = "billDetail";
            this.billDetail.Size = new System.Drawing.Size(84, 47);
            this.billDetail.Text = "查看关联单据";
            this.billDetail.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.billDetail.Click += new System.EventHandler(this.billDetail_Click);
            this.billDetail.MouseEnter += new System.EventHandler(this.billDetail_MouseEnter);
            this.billDetail.MouseLeave += new System.EventHandler(this.billDetail_MouseLeave);
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
            this.printDisplay.Click += new System.EventHandler(this.print_Click);
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
            this.close.Image = global::MainProgram.Properties.Resources.close;
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
            // dataGridViewList
            // 
            this.dataGridViewList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Location = new System.Drawing.Point(0, 53);
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.RowHeadersWidth = 4;
            this.dataGridViewList.RowTemplate.Height = 23;
            this.dataGridViewList.Size = new System.Drawing.Size(967, 430);
            this.dataGridViewList.TabIndex = 3;
            this.dataGridViewList.Click += new System.EventHandler(this.dataGridViewBilConfigList_Click);
            this.dataGridViewList.DoubleClick += new System.EventHandler(this.dataGridViewMaterielList_DoubleClick);
            // 
            // panelExport
            // 
            this.panelExport.BackColor = System.Drawing.Color.Transparent;
            this.panelExport.BackgroundImage = global::MainProgram.Properties.Resources.export;
            this.panelExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelExport.Location = new System.Drawing.Point(115, 6);
            this.panelExport.Name = "panelExport";
            this.panelExport.Size = new System.Drawing.Size(20, 20);
            this.panelExport.TabIndex = 24;
            this.panelExport.MouseEnter += new System.EventHandler(this.panelExport_MouseEnter);
            this.panelExport.MouseLeave += new System.EventHandler(this.panelExport_MouseLeave);
            // 
            // panelPrintDisplay
            // 
            this.panelPrintDisplay.BackColor = System.Drawing.Color.Transparent;
            this.panelPrintDisplay.BackgroundImage = global::MainProgram.Properties.Resources.Printers;
            this.panelPrintDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelPrintDisplay.Location = new System.Drawing.Point(181, 7);
            this.panelPrintDisplay.Name = "panelPrintDisplay";
            this.panelPrintDisplay.Size = new System.Drawing.Size(17, 20);
            this.panelPrintDisplay.TabIndex = 23;
            this.panelPrintDisplay.MouseEnter += new System.EventHandler(this.panelPrintDisplay_MouseEnter);
            this.panelPrintDisplay.MouseLeave += new System.EventHandler(this.panelPrintDisplay_MouseLeave);
            // 
            // panelPrint
            // 
            this.panelPrint.BackColor = System.Drawing.Color.Transparent;
            this.panelPrint.BackgroundImage = global::MainProgram.Properties.Resources.Printers2;
            this.panelPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelPrint.Location = new System.Drawing.Point(232, 6);
            this.panelPrint.Name = "panelPrint";
            this.panelPrint.Size = new System.Drawing.Size(17, 20);
            this.panelPrint.TabIndex = 22;
            this.panelPrint.MouseEnter += new System.EventHandler(this.panelPrint_MouseEnter);
            this.panelPrint.MouseLeave += new System.EventHandler(this.panelPrint_MouseLeave);
            // 
            // panelImageExit
            // 
            this.panelImageExit.BackColor = System.Drawing.Color.Transparent;
            this.panelImageExit.BackgroundImage = global::MainProgram.Properties.Resources.close;
            this.panelImageExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelImageExit.Location = new System.Drawing.Point(275, 7);
            this.panelImageExit.Name = "panelImageExit";
            this.panelImageExit.Size = new System.Drawing.Size(17, 20);
            this.panelImageExit.TabIndex = 21;
            this.panelImageExit.MouseEnter += new System.EventHandler(this.panelImageExit_MouseEnter);
            this.panelImageExit.MouseLeave += new System.EventHandler(this.panelImageExit_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::MainProgram.Properties.Resources.export;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Location = new System.Drawing.Point(41, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 20);
            this.panel1.TabIndex = 27;
            this.panel1.Click += new System.EventHandler(this.billDetail_Click);
            this.panel1.MouseEnter += new System.EventHandler(this.billDetail_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.billDetail_MouseLeave);
            // 
            // FormSaleOrderSequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelExport);
            this.Controls.Add(this.panelPrintDisplay);
            this.Controls.Add(this.panelPrint);
            this.Controls.Add(this.panelImageExit);
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormSaleOrderSequence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "采购订单-入库单-发票时序簿";
            this.Load += new System.EventHandler(this.FormSaleOrderSequence_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel panelExport;
        private System.Windows.Forms.Panel panelPrintDisplay;
        private System.Windows.Forms.Panel panelPrint;
        private System.Windows.Forms.Panel panelImageExit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton billDetail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}