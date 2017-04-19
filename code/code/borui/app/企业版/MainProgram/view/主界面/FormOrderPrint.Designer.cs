namespace MainProgram
{
    partial class FormOrderPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOrderPrint));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTry = new System.Windows.Forms.ToolStripButton();
            this.open = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPageSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.printDisplay = new System.Windows.Forms.ToolStripButton();
            this.print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelPrintDevName = new System.Windows.Forms.Label();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.labelPage = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton2007 = new System.Windows.Forms.RadioButton();
            this.radioButton2003 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.labelExportStatus = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonTry,
            this.open,
            this.toolStripSeparator1,
            this.toolStripButtonPageSet,
            this.toolStripButton1,
            this.printDisplay,
            this.print,
            this.toolStripSeparator2,
            this.close});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(572, 45);
            this.toolStrip1.TabIndex = 53;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonTry
            // 
            this.toolStripButtonTry.Enabled = false;
            this.toolStripButtonTry.Image = global::MainProgram.Properties.Resources.export;
            this.toolStripButtonTry.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButtonTry.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonTry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTry.Name = "toolStripButtonTry";
            this.toolStripButtonTry.Size = new System.Drawing.Size(60, 42);
            this.toolStripButtonTry.Text = "重新导出";
            this.toolStripButtonTry.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonTry.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButtonTry.Click += new System.EventHandler(this.toolStripButtonReExport_Click);
            // 
            // open
            // 
            this.open.Enabled = false;
            this.open.Image = global::MainProgram.Properties.Resources._OPEN;
            this.open.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.open.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(60, 42);
            this.open.Text = "打开文件";
            this.open.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.open.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.open.Click += new System.EventHandler(this.openFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButtonPageSet
            // 
            this.toolStripButtonPageSet.Image = global::MainProgram.Properties.Resources.Printers;
            this.toolStripButtonPageSet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButtonPageSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPageSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPageSet.Name = "toolStripButtonPageSet";
            this.toolStripButtonPageSet.Size = new System.Drawing.Size(60, 42);
            this.toolStripButtonPageSet.Text = "页面设置";
            this.toolStripButtonPageSet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonPageSet.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButtonPageSet.Click += new System.EventHandler(this.toolStripButtonPateSet_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::MainProgram.Properties.Resources.Printers2;
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(72, 42);
            this.toolStripButton1.Text = "打印机设置";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButtonPrintDevSet_Click);
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
            this.printDisplay.Click += new System.EventHandler(this.printReview_Click);
            // 
            // print
            // 
            this.print.Enabled = false;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 54;
            this.label1.Text = "默认打印机名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "纸张尺寸：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "纸张方向：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 57;
            this.label4.Text = "状态：";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(70, 71);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(29, 12);
            this.labelStatus.TabIndex = 58;
            this.labelStatus.Text = "就绪";
            // 
            // labelPrintDevName
            // 
            this.labelPrintDevName.AutoSize = true;
            this.labelPrintDevName.Location = new System.Drawing.Point(130, 101);
            this.labelPrintDevName.Name = "labelPrintDevName";
            this.labelPrintDevName.Size = new System.Drawing.Size(41, 12);
            this.labelPrintDevName.TabIndex = 59;
            this.labelPrintDevName.Text = "状态：";
            // 
            // labelPageSize
            // 
            this.labelPageSize.AutoSize = true;
            this.labelPageSize.Location = new System.Drawing.Point(94, 131);
            this.labelPageSize.Name = "labelPageSize";
            this.labelPageSize.Size = new System.Drawing.Size(41, 12);
            this.labelPageSize.TabIndex = 60;
            this.labelPageSize.Text = "状态：";
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(94, 161);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(41, 12);
            this.labelPage.TabIndex = 61;
            this.labelPage.Text = "状态：";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 135);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机状态";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.labelExportStatus);
            this.groupBox2.Location = new System.Drawing.Point(296, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 135);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据导出";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton2007);
            this.groupBox3.Controls.Add(this.radioButton2003);
            this.groupBox3.Location = new System.Drawing.Point(18, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 72);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文件导出格式";
            // 
            // radioButton2007
            // 
            this.radioButton2007.AutoSize = true;
            this.radioButton2007.Checked = true;
            this.radioButton2007.Location = new System.Drawing.Point(21, 44);
            this.radioButton2007.Name = "radioButton2007";
            this.radioButton2007.Size = new System.Drawing.Size(125, 16);
            this.radioButton2007.TabIndex = 1;
            this.radioButton2007.TabStop = true;
            this.radioButton2007.Text = "office 2007及以上";
            this.radioButton2007.UseVisualStyleBackColor = true;
            // 
            // radioButton2003
            // 
            this.radioButton2003.AutoSize = true;
            this.radioButton2003.Location = new System.Drawing.Point(21, 21);
            this.radioButton2003.Name = "radioButton2003";
            this.radioButton2003.Size = new System.Drawing.Size(89, 16);
            this.radioButton2003.TabIndex = 0;
            this.radioButton2003.TabStop = true;
            this.radioButton2003.Text = "office 2003";
            this.radioButton2003.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 65;
            this.label7.Text = "文件导出状态：";
            // 
            // labelExportStatus
            // 
            this.labelExportStatus.AutoSize = true;
            this.labelExportStatus.Location = new System.Drawing.Point(107, 17);
            this.labelExportStatus.Name = "labelExportStatus";
            this.labelExportStatus.Size = new System.Drawing.Size(29, 12);
            this.labelExportStatus.TabIndex = 66;
            this.labelExportStatus.Text = "失败";
            // 
            // FormOrderPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 209);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.labelPageSize);
            this.Controls.Add(this.labelPrintDevName);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOrderPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据套打";
            this.Load += new System.EventHandler(this.FormOrderPrint_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // private AxDSOFramer.AxFramerControl axFramerControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelPrintDevName;
        private System.Windows.Forms.Label labelPageSize;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonPageSet;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTry;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton2007;
        private System.Windows.Forms.RadioButton radioButton2003;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelExportStatus;



    }
}