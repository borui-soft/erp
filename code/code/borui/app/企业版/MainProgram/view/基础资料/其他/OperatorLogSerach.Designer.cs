namespace MainProgram
{
    partial class OperatorLogSerach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorLogSerach));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxHoseName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxModuleName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxUserName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.radioButtonDate = new System.Windows.Forms.RadioButton();
            this.radioButtonAllDate = new System.Windows.Forms.RadioButton();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxHoseName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxModuleName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件";
            // 
            // comboBoxHoseName
            // 
            this.comboBoxHoseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHoseName.FormattingEnabled = true;
            this.comboBoxHoseName.Location = new System.Drawing.Point(77, 86);
            this.comboBoxHoseName.Name = "comboBoxHoseName";
            this.comboBoxHoseName.Size = new System.Drawing.Size(227, 20);
            this.comboBoxHoseName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "主机名称";
            // 
            // comboBoxModuleName
            // 
            this.comboBoxModuleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModuleName.FormattingEnabled = true;
            this.comboBoxModuleName.Location = new System.Drawing.Point(76, 51);
            this.comboBoxModuleName.Name = "comboBoxModuleName";
            this.comboBoxModuleName.Size = new System.Drawing.Size(227, 20);
            this.comboBoxModuleName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "模块名称";
            // 
            // comboBoxUserName
            // 
            this.comboBoxUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUserName.FormattingEnabled = true;
            this.comboBoxUserName.Location = new System.Drawing.Point(76, 18);
            this.comboBoxUserName.Name = "comboBoxUserName";
            this.comboBoxUserName.Size = new System.Drawing.Size(227, 20);
            this.comboBoxUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox2.Controls.Add(this.radioButtonDate);
            this.groupBox2.Controls.Add(this.radioButtonAllDate);
            this.groupBox2.Location = new System.Drawing.Point(13, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "时间段";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(199, 48);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(103, 21);
            this.dateTimePickerEndDate.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "到";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(61, 48);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(103, 21);
            this.dateTimePickerStartDate.TabIndex = 2;
            // 
            // radioButtonDate
            // 
            this.radioButtonDate.AutoSize = true;
            this.radioButtonDate.Location = new System.Drawing.Point(19, 48);
            this.radioButtonDate.Name = "radioButtonDate";
            this.radioButtonDate.Size = new System.Drawing.Size(35, 16);
            this.radioButtonDate.TabIndex = 1;
            this.radioButtonDate.TabStop = true;
            this.radioButtonDate.Text = "从";
            this.radioButtonDate.UseVisualStyleBackColor = true;
            this.radioButtonDate.Click += new System.EventHandler(this.radioButtonDate_Click);
            // 
            // radioButtonAllDate
            // 
            this.radioButtonAllDate.AutoSize = true;
            this.radioButtonAllDate.Location = new System.Drawing.Point(19, 21);
            this.radioButtonAllDate.Name = "radioButtonAllDate";
            this.radioButtonAllDate.Size = new System.Drawing.Size(71, 16);
            this.radioButtonAllDate.TabIndex = 0;
            this.radioButtonAllDate.TabStop = true;
            this.radioButtonAllDate.Text = "所有时段";
            this.radioButtonAllDate.UseVisualStyleBackColor = true;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(361, 21);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 2;
            this.buttonEnter.Text = "确定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(361, 57);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // OperatorLogSerach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 236);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OperatorLogSerach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "操作日志过滤器";
            this.Load += new System.EventHandler(this.OperatorLogSerach_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxModuleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxHoseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.RadioButton radioButtonDate;
        private System.Windows.Forms.RadioButton radioButtonAllDate;
    }
}