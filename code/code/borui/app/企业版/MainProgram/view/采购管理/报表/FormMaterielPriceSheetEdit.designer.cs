namespace MainProgram
{
    partial class FormMaterielPriceSheetEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaterielPriceSheetEdit));
            this.buttonEnter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.textBoxSupplierName = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.textBoxOrnmToValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxOrnmFromValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(130, 228);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 4;
            this.buttonEnter.Text = "确定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "(*)";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(80, 109);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(198, 21);
            this.textBoxPrice.TabIndex = 2;
            this.textBoxPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrice_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "单价";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(237, 228);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Location = new System.Drawing.Point(21, 228);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 8;
            this.buttonEdit.Text = "编辑";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Location = new System.Drawing.Point(13, 17);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(41, 12);
            this.labelSupplier.TabIndex = 3;
            this.labelSupplier.Text = "供应商";
            // 
            // textBoxSupplierName
            // 
            this.textBoxSupplierName.Location = new System.Drawing.Point(80, 8);
            this.textBoxSupplierName.Name = "textBoxSupplierName";
            this.textBoxSupplierName.ReadOnly = true;
            this.textBoxSupplierName.Size = new System.Drawing.Size(198, 21);
            this.textBoxSupplierName.TabIndex = 4;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Image = global::MainProgram.Properties.Resources._OPEN;
            this.buttonSelect.Location = new System.Drawing.Point(286, 6);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(23, 23);
            this.buttonSelect.TabIndex = 4;
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxOrnmToValue
            // 
            this.textBoxOrnmToValue.Location = new System.Drawing.Point(80, 75);
            this.textBoxOrnmToValue.Name = "textBoxOrnmToValue";
            this.textBoxOrnmToValue.Size = new System.Drawing.Size(198, 21);
            this.textBoxOrnmToValue.TabIndex = 10;
            this.textBoxOrnmToValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrice_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "订货数量到";
            // 
            // textBoxOrnmFromValue
            // 
            this.textBoxOrnmFromValue.Location = new System.Drawing.Point(80, 41);
            this.textBoxOrnmFromValue.Name = "textBoxOrnmFromValue";
            this.textBoxOrnmFromValue.Size = new System.Drawing.Size(198, 21);
            this.textBoxOrnmFromValue.TabIndex = 13;
            this.textBoxOrnmFromValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrice_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "订货数量从";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "备注";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(80, 140);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(229, 80);
            this.textBoxNote.TabIndex = 16;
            // 
            // FormMaterielPriceSheetEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 266);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxOrnmFromValue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxOrnmToValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSupplierName);
            this.Controls.Add(this.labelSupplier);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMaterielPriceSheetEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "供应商报价编辑";
            this.Load += new System.EventHandler(this.FormSupplierPriceSheetEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.TextBox textBoxSupplierName;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.TextBox textBoxOrnmToValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxOrnmFromValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxNote;
    }
}