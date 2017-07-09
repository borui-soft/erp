namespace MainProgram
{
    partial class FormStorageStockDetailsFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStorageStockDetailsFilter));
            this.buttonEnter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonMateriel = new System.Windows.Forms.RadioButton();
            this.radioButtonAllMateriel = new System.Windows.Forms.RadioButton();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxReview = new System.Windows.Forms.ComboBox();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonInterregional = new System.Windows.Forms.RadioButton();
            this.textBoxStartID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEndID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(269, 208);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 0;
            this.buttonEnter.Text = "确定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxEndID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxStartID);
            this.groupBox1.Controls.Add(this.radioButtonInterregional);
            this.groupBox1.Controls.Add(this.radioButtonMateriel);
            this.groupBox1.Controls.Add(this.radioButtonAllMateriel);
            this.groupBox1.Controls.Add(this.buttonSelect);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.comboBoxReview);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox1.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 185);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件";
            // 
            // radioButtonMateriel
            // 
            this.radioButtonMateriel.AutoSize = true;
            this.radioButtonMateriel.Location = new System.Drawing.Point(9, 85);
            this.radioButtonMateriel.Name = "radioButtonMateriel";
            this.radioButtonMateriel.Size = new System.Drawing.Size(71, 16);
            this.radioButtonMateriel.TabIndex = 10;
            this.radioButtonMateriel.TabStop = true;
            this.radioButtonMateriel.Text = "单一物料";
            this.radioButtonMateriel.UseVisualStyleBackColor = true;
            this.radioButtonMateriel.Click += new System.EventHandler(this.radioButtonAllMateriel_Click);
            // 
            // radioButtonAllMateriel
            // 
            this.radioButtonAllMateriel.AutoSize = true;
            this.radioButtonAllMateriel.Location = new System.Drawing.Point(9, 20);
            this.radioButtonAllMateriel.Name = "radioButtonAllMateriel";
            this.radioButtonAllMateriel.Size = new System.Drawing.Size(71, 16);
            this.radioButtonAllMateriel.TabIndex = 9;
            this.radioButtonAllMateriel.TabStop = true;
            this.radioButtonAllMateriel.Text = "所有物料";
            this.radioButtonAllMateriel.UseVisualStyleBackColor = true;
            this.radioButtonAllMateriel.Click += new System.EventHandler(this.radioButtonAllMateriel_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(216, 81);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(61, 23);
            this.buttonSelect.TabIndex = 7;
            this.buttonSelect.Text = "物料选择";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(99, 83);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(108, 21);
            this.textBoxName.TabIndex = 8;
            // 
            // comboBoxReview
            // 
            this.comboBoxReview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReview.FormattingEnabled = true;
            this.comboBoxReview.Location = new System.Drawing.Point(97, 154);
            this.comboBoxReview.Name = "comboBoxReview";
            this.comboBoxReview.Size = new System.Drawing.Size(228, 20);
            this.comboBoxReview.TabIndex = 5;
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(218, 119);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(105, 21);
            this.dateTimePickerEndDate.TabIndex = 4;
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(97, 119);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(105, 21);
            this.dateTimePickerStartDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "审核标志";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "起止日期";
            // 
            // radioButtonInterregional
            // 
            this.radioButtonInterregional.AutoSize = true;
            this.radioButtonInterregional.Location = new System.Drawing.Point(9, 53);
            this.radioButtonInterregional.Name = "radioButtonInterregional";
            this.radioButtonInterregional.Size = new System.Drawing.Size(83, 16);
            this.radioButtonInterregional.TabIndex = 11;
            this.radioButtonInterregional.TabStop = true;
            this.radioButtonInterregional.Text = "物料ID区间";
            this.radioButtonInterregional.UseVisualStyleBackColor = true;
            this.radioButtonInterregional.Click += new System.EventHandler(this.radioButtonAllMateriel_Click);
            // 
            // textBoxStartID
            // 
            this.textBoxStartID.Enabled = false;
            this.textBoxStartID.Location = new System.Drawing.Point(99, 48);
            this.textBoxStartID.Name = "textBoxStartID";
            this.textBoxStartID.Size = new System.Drawing.Size(61, 21);
            this.textBoxStartID.TabIndex = 12;
            this.textBoxStartID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxStartID_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "-";
            // 
            // textBoxEndID
            // 
            this.textBoxEndID.Enabled = false;
            this.textBoxEndID.Location = new System.Drawing.Point(179, 48);
            this.textBoxEndID.Name = "textBoxEndID";
            this.textBoxEndID.Size = new System.Drawing.Size(61, 21);
            this.textBoxEndID.TabIndex = 14;
            this.textBoxEndID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxStartID_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "输入查询物料ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "区间，如1-999";
            // 
            // FormStorageStockDetailsFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(356, 240);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonEnter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStorageStockDetailsFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据过滤器";
            this.Load += new System.EventHandler(this.FormPurchaseInfoCountFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.ComboBox comboBoxReview;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.RadioButton radioButtonMateriel;
        private System.Windows.Forms.RadioButton radioButtonAllMateriel;
        private System.Windows.Forms.TextBox textBoxEndID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxStartID;
        private System.Windows.Forms.RadioButton radioButtonInterregional;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}