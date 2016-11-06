namespace MainProgram
{
    partial class FormBillConfigEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillConfigEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFront = new System.Windows.Forms.TextBox();
            this.checkBoxUseSysdate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsUse = new System.Windows.Forms.CheckBox();
            this.checkBoxIsAutoSave = new System.Windows.Forms.CheckBox();
            this.checkBoxIsInput = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNum)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "前置编码";
            // 
            // textBoxFront
            // 
            this.textBoxFront.Location = new System.Drawing.Point(66, 14);
            this.textBoxFront.Name = "textBoxFront";
            this.textBoxFront.Size = new System.Drawing.Size(71, 21);
            this.textBoxFront.TabIndex = 1;
            this.textBoxFront.TextChanged += new System.EventHandler(this.textBoxFront_TextChanged);
            // 
            // checkBoxUseSysdate
            // 
            this.checkBoxUseSysdate.AutoSize = true;
            this.checkBoxUseSysdate.Location = new System.Drawing.Point(332, 17);
            this.checkBoxUseSysdate.Name = "checkBoxUseSysdate";
            this.checkBoxUseSysdate.Size = new System.Drawing.Size(144, 16);
            this.checkBoxUseSysdate.TabIndex = 2;
            this.checkBoxUseSysdate.Text = "是否自动添加系统日期";
            this.checkBoxUseSysdate.UseVisualStyleBackColor = true;
            this.checkBoxUseSysdate.Click += new System.EventHandler(this.checkBoxUseSysdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数字位数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxUseSysdate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxFront);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 79);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "格式";
            // 
            // numericUpDownNum
            // 
            this.numericUpDownNum.Location = new System.Drawing.Point(233, 14);
            this.numericUpDownNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNum.Name = "numericUpDownNum";
            this.numericUpDownNum.Size = new System.Drawing.Size(47, 21);
            this.numericUpDownNum.TabIndex = 7;
            this.numericUpDownNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNum.MouseUp += new System.Windows.Forms.MouseEventHandler(this.numericUpDownNum_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "编码预览";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(66, 45);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.Size = new System.Drawing.Size(410, 21);
            this.textBoxCode.TabIndex = 6;
            this.textBoxCode.TextChanged += new System.EventHandler(this.textBoxCode_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxIsUse);
            this.groupBox2.Controls.Add(this.checkBoxIsAutoSave);
            this.groupBox2.Controls.Add(this.checkBoxIsInput);
            this.groupBox2.Location = new System.Drawing.Point(15, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(482, 45);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // checkBoxIsUse
            // 
            this.checkBoxIsUse.AutoSize = true;
            this.checkBoxIsUse.Location = new System.Drawing.Point(334, 20);
            this.checkBoxIsUse.Name = "checkBoxIsUse";
            this.checkBoxIsUse.Size = new System.Drawing.Size(144, 16);
            this.checkBoxIsUse.TabIndex = 11;
            this.checkBoxIsUse.Text = "单据是否使用编码规则";
            this.checkBoxIsUse.UseVisualStyleBackColor = true;
            this.checkBoxIsUse.Click += new System.EventHandler(this.checkBoxUseSysdate_Click);
            // 
            // checkBoxIsAutoSave
            // 
            this.checkBoxIsAutoSave.AutoSize = true;
            this.checkBoxIsAutoSave.Location = new System.Drawing.Point(175, 20);
            this.checkBoxIsAutoSave.Name = "checkBoxIsAutoSave";
            this.checkBoxIsAutoSave.Size = new System.Drawing.Size(144, 16);
            this.checkBoxIsAutoSave.TabIndex = 10;
            this.checkBoxIsAutoSave.Text = "单据保存是否自动审核";
            this.checkBoxIsAutoSave.UseVisualStyleBackColor = true;
            this.checkBoxIsAutoSave.Click += new System.EventHandler(this.checkBoxUseSysdate_Click);
            // 
            // checkBoxIsInput
            // 
            this.checkBoxIsInput.AutoSize = true;
            this.checkBoxIsInput.Location = new System.Drawing.Point(16, 20);
            this.checkBoxIsInput.Name = "checkBoxIsInput";
            this.checkBoxIsInput.Size = new System.Drawing.Size(144, 16);
            this.checkBoxIsInput.TabIndex = 9;
            this.checkBoxIsInput.Text = "单据编码是否允许编辑";
            this.checkBoxIsInput.UseVisualStyleBackColor = true;
            this.checkBoxIsInput.Click += new System.EventHandler(this.checkBoxUseSysdate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(421, 164);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(330, 164);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormBillConfigEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 197);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormBillConfigEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "单据设置信息编辑";
            this.Load += new System.EventHandler(this.FormBillConfigEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFront;
        private System.Windows.Forms.CheckBox checkBoxUseSysdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxIsUse;
        private System.Windows.Forms.CheckBox checkBoxIsAutoSave;
        private System.Windows.Forms.CheckBox checkBoxIsInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.NumericUpDown numericUpDownNum;
    }
}