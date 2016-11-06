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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(269, 185);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 0;
            this.buttonEnter.Text = "确定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(331, 163);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件";
            // 
            // radioButtonMateriel
            // 
            this.radioButtonMateriel.AutoSize = true;
            this.radioButtonMateriel.Location = new System.Drawing.Point(9, 48);
            this.radioButtonMateriel.Name = "radioButtonMateriel";
            this.radioButtonMateriel.Size = new System.Drawing.Size(71, 16);
            this.radioButtonMateriel.TabIndex = 10;
            this.radioButtonMateriel.TabStop = true;
            this.radioButtonMateriel.Text = "物料名称";
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
            this.buttonSelect.Location = new System.Drawing.Point(216, 45);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(61, 23);
            this.buttonSelect.TabIndex = 7;
            this.buttonSelect.Text = "物料选择";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(86, 47);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(108, 21);
            this.textBoxName.TabIndex = 8;
            // 
            // comboBoxReview
            // 
            this.comboBoxReview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReview.FormattingEnabled = true;
            this.comboBoxReview.Location = new System.Drawing.Point(86, 122);
            this.comboBoxReview.Name = "comboBoxReview";
            this.comboBoxReview.Size = new System.Drawing.Size(239, 20);
            this.comboBoxReview.TabIndex = 5;
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(217, 82);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(108, 21);
            this.dateTimePickerEndDate.TabIndex = 4;
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(86, 82);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(108, 21);
            this.dateTimePickerStartDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "审核标志";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "起止日期";
            // 
            // FormStorageStockDetailsFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(356, 216);
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
    }
}