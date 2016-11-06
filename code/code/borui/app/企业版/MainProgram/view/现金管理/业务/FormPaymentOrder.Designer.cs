namespace MainProgram
{
    partial class FormPaymentOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPaymentOrder));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printDisplay = new System.Windows.Forms.ToolStripButton();
            this.print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReview = new System.Windows.Forms.ToolStripButton();
            this.selectSourceOrder = new System.Windows.Forms.ToolStripButton();
            this.calculator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxVouchersNumber = new System.Windows.Forms.TextBox();
            this.textBoxPaymentOrderNumber = new System.Windows.Forms.TextBox();
            this.labelSounceOrderNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxPaymentType = new System.Windows.Forms.ComboBox();
            this.textBoxSourceOrderNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPaymentObject = new System.Windows.Forms.ComboBox();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.labelBank = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxTransactionAmount = new System.Windows.Forms.TextBox();
            this.textBoxMakeOrderStaff = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxOrderReview = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panelIsReview = new System.Windows.Forms.Panel();
            this.panelSourceOrderNumber = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(222, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "付款单";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "付款日期";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator1,
            this.printDisplay,
            this.print,
            this.toolStripSeparator2,
            this.toolStripButtonReview,
            this.selectSourceOrder,
            this.calculator,
            this.toolStripSeparator4,
            this.close,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(596, 45);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // save
            // 
            this.save.Image = global::MainProgram.Properties.Resources.Save;
            this.save.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(36, 42);
            this.save.Text = "保存";
            this.save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.save.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
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
            this.printDisplay.Click += new System.EventHandler(this.printDisplay_Click);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButtonReview
            // 
            this.toolStripButtonReview.Image = global::MainProgram.Properties.Resources.review;
            this.toolStripButtonReview.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButtonReview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonReview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReview.Name = "toolStripButtonReview";
            this.toolStripButtonReview.Size = new System.Drawing.Size(36, 42);
            this.toolStripButtonReview.Text = "审核";
            this.toolStripButtonReview.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonReview.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButtonReview.Click += new System.EventHandler(this.toolStripButtonReview_Click);
            // 
            // selectSourceOrder
            // 
            this.selectSourceOrder.Image = global::MainProgram.Properties.Resources.资料;
            this.selectSourceOrder.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.selectSourceOrder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.selectSourceOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectSourceOrder.Name = "selectSourceOrder";
            this.selectSourceOrder.Size = new System.Drawing.Size(48, 42);
            this.selectSourceOrder.Text = "选源单";
            this.selectSourceOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.selectSourceOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.selectSourceOrder.Click += new System.EventHandler(this.selectSourceOrder_Click);
            // 
            // calculator
            // 
            this.calculator.Image = global::MainProgram.Properties.Resources.calc;
            this.calculator.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.calculator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.calculator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.calculator.Name = "calculator";
            this.calculator.Size = new System.Drawing.Size(48, 42);
            this.calculator.Text = "计算器";
            this.calculator.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.calculator.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.calculator.Click += new System.EventHandler(this.calculator_Click);
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
            // dateTime
            // 
            this.dateTime.Location = new System.Drawing.Point(78, 104);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(127, 21);
            this.dateTime.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(439, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 10);
            this.label3.TabIndex = 26;
            this.label3.Text = "单据号";
            // 
            // textBoxVouchersNumber
            // 
            this.textBoxVouchersNumber.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxVouchersNumber.Location = new System.Drawing.Point(473, 76);
            this.textBoxVouchersNumber.Name = "textBoxVouchersNumber";
            this.textBoxVouchersNumber.Size = new System.Drawing.Size(111, 19);
            this.textBoxVouchersNumber.TabIndex = 27;
            // 
            // textBoxPaymentOrderNumber
            // 
            this.textBoxPaymentOrderNumber.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPaymentOrderNumber.Location = new System.Drawing.Point(473, 52);
            this.textBoxPaymentOrderNumber.Name = "textBoxPaymentOrderNumber";
            this.textBoxPaymentOrderNumber.ReadOnly = true;
            this.textBoxPaymentOrderNumber.Size = new System.Drawing.Size(111, 19);
            this.textBoxPaymentOrderNumber.TabIndex = 29;
            // 
            // labelSounceOrderNumber
            // 
            this.labelSounceOrderNumber.AutoSize = true;
            this.labelSounceOrderNumber.Location = new System.Drawing.Point(229, 150);
            this.labelSounceOrderNumber.Name = "labelSounceOrderNumber";
            this.labelSounceOrderNumber.Size = new System.Drawing.Size(53, 12);
            this.labelSounceOrderNumber.TabIndex = 28;
            this.labelSounceOrderNumber.Text = "源单据号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "付款类型";
            // 
            // comboBoxPaymentType
            // 
            this.comboBoxPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPaymentType.FormattingEnabled = true;
            this.comboBoxPaymentType.Location = new System.Drawing.Point(78, 145);
            this.comboBoxPaymentType.Name = "comboBoxPaymentType";
            this.comboBoxPaymentType.Size = new System.Drawing.Size(127, 20);
            this.comboBoxPaymentType.TabIndex = 31;
            this.comboBoxPaymentType.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaymentType_SelectedIndexChanged);
            // 
            // textBoxSourceOrderNumber
            // 
            this.textBoxSourceOrderNumber.Location = new System.Drawing.Point(291, 144);
            this.textBoxSourceOrderNumber.Name = "textBoxSourceOrderNumber";
            this.textBoxSourceOrderNumber.Size = new System.Drawing.Size(276, 21);
            this.textBoxSourceOrderNumber.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "付款科目";
            // 
            // comboBoxPaymentObject
            // 
            this.comboBoxPaymentObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPaymentObject.FormattingEnabled = true;
            this.comboBoxPaymentObject.Location = new System.Drawing.Point(78, 193);
            this.comboBoxPaymentObject.Name = "comboBoxPaymentObject";
            this.comboBoxPaymentObject.Size = new System.Drawing.Size(127, 20);
            this.comboBoxPaymentObject.TabIndex = 34;
            this.comboBoxPaymentObject.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaymentObject_SelectedIndexChanged);
            // 
            // comboBoxBank
            // 
            this.comboBoxBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBank.FormattingEnabled = true;
            this.comboBoxBank.Location = new System.Drawing.Point(435, 194);
            this.comboBoxBank.Name = "comboBoxBank";
            this.comboBoxBank.Size = new System.Drawing.Size(150, 20);
            this.comboBoxBank.TabIndex = 36;
            this.comboBoxBank.Visible = false;
            // 
            // labelBank
            // 
            this.labelBank.AutoSize = true;
            this.labelBank.Location = new System.Drawing.Point(376, 198);
            this.labelBank.Name = "labelBank";
            this.labelBank.Size = new System.Drawing.Size(53, 12);
            this.labelBank.TabIndex = 35;
            this.labelBank.Text = "银行账户";
            this.labelBank.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(228, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "核销金额";
            // 
            // textBoxTransactionAmount
            // 
            this.textBoxTransactionAmount.Location = new System.Drawing.Point(291, 193);
            this.textBoxTransactionAmount.Name = "textBoxTransactionAmount";
            this.textBoxTransactionAmount.Size = new System.Drawing.Size(74, 21);
            this.textBoxTransactionAmount.TabIndex = 38;
            this.textBoxTransactionAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxTransactionAmount_KeyPress);
            // 
            // textBoxMakeOrderStaff
            // 
            this.textBoxMakeOrderStaff.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMakeOrderStaff.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMakeOrderStaff.Location = new System.Drawing.Point(79, 234);
            this.textBoxMakeOrderStaff.Name = "textBoxMakeOrderStaff";
            this.textBoxMakeOrderStaff.ReadOnly = true;
            this.textBoxMakeOrderStaff.Size = new System.Drawing.Size(126, 21);
            this.textBoxMakeOrderStaff.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 44;
            this.label10.Text = "制单人";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(229, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 49;
            this.label11.Text = "审核人";
            // 
            // textBoxOrderReview
            // 
            this.textBoxOrderReview.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxOrderReview.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxOrderReview.Location = new System.Drawing.Point(292, 234);
            this.textBoxOrderReview.Name = "textBoxOrderReview";
            this.textBoxOrderReview.ReadOnly = true;
            this.textBoxOrderReview.Size = new System.Drawing.Size(73, 21);
            this.textBoxOrderReview.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(438, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 10);
            this.label9.TabIndex = 51;
            this.label9.Text = "凭证号";
            // 
            // panelIsReview
            // 
            this.panelIsReview.BackgroundImage = global::MainProgram.Properties.Resources.已审核;
            this.panelIsReview.Location = new System.Drawing.Point(530, 12);
            this.panelIsReview.Name = "panelIsReview";
            this.panelIsReview.Size = new System.Drawing.Size(63, 35);
            this.panelIsReview.TabIndex = 52;
            this.panelIsReview.Visible = false;
            // 
            // panelSourceOrderNumber
            // 
            this.panelSourceOrderNumber.BackgroundImage = global::MainProgram.Properties.Resources.Preview_16_;
            this.panelSourceOrderNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSourceOrderNumber.Location = new System.Drawing.Point(564, 143);
            this.panelSourceOrderNumber.Name = "panelSourceOrderNumber";
            this.panelSourceOrderNumber.Size = new System.Drawing.Size(21, 21);
            this.panelSourceOrderNumber.TabIndex = 50;
            this.panelSourceOrderNumber.Click += new System.EventHandler(this.panelSourceOrderNumber_Click);
            // 
            // FormPaymentOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 273);
            this.Controls.Add(this.panelIsReview);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panelSourceOrderNumber);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxOrderReview);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxMakeOrderStaff);
            this.Controls.Add(this.textBoxTransactionAmount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxBank);
            this.Controls.Add(this.labelBank);
            this.Controls.Add(this.comboBoxPaymentObject);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxSourceOrderNumber);
            this.Controls.Add(this.comboBoxPaymentType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxPaymentOrderNumber);
            this.Controls.Add(this.labelSounceOrderNumber);
            this.Controls.Add(this.textBoxVouchersNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTime);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPaymentOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "付款单";
            this.Load += new System.EventHandler(this.FormPaymentOrder_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton printDisplay;
        private System.Windows.Forms.ToolStripButton print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton calculator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.DateTimePicker dateTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVouchersNumber;
        private System.Windows.Forms.TextBox textBoxPaymentOrderNumber;
        private System.Windows.Forms.Label labelSounceOrderNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxPaymentType;
        private System.Windows.Forms.TextBox textBoxSourceOrderNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPaymentObject;
        private System.Windows.Forms.ComboBox comboBoxBank;
        private System.Windows.Forms.Label labelBank;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTransactionAmount;
        private System.Windows.Forms.ToolStripButton toolStripButtonReview;
        private System.Windows.Forms.ToolStripButton selectSourceOrder;
        private System.Windows.Forms.TextBox textBoxMakeOrderStaff;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxOrderReview;
        private System.Windows.Forms.Panel panelSourceOrderNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panelIsReview;
    }
}