namespace MainProgram
{
    partial class FormStaffEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStaffEdit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonExit = new System.Windows.Forms.ToolStripButton();
            this.groupBoxBaseInfo = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxEdu = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxTel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNum = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNO = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.groupBoxBaseInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAdd,
            this.buttonExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(635, 45);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonAdd.Image = global::MainProgram.Properties.Resources.Save;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(45, 42);
            this.buttonAdd.Text = " 保存 ";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Image = global::MainProgram.Properties.Resources.close;
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(45, 42);
            this.buttonExit.Text = " 退出 ";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupBoxBaseInfo
            // 
            this.groupBoxBaseInfo.Controls.Add(this.label6);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxEmail);
            this.groupBoxBaseInfo.Controls.Add(this.dateTimePickerDate);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxSex);
            this.groupBoxBaseInfo.Controls.Add(this.label5);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxEdu);
            this.groupBoxBaseInfo.Controls.Add(this.label4);
            this.groupBoxBaseInfo.Controls.Add(this.label26);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxNote);
            this.groupBoxBaseInfo.Controls.Add(this.label24);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxAddress);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxType);
            this.groupBoxBaseInfo.Controls.Add(this.label18);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxProfile);
            this.groupBoxBaseInfo.Controls.Add(this.label16);
            this.groupBoxBaseInfo.Controls.Add(this.label14);
            this.groupBoxBaseInfo.Controls.Add(this.label22);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxTel);
            this.groupBoxBaseInfo.Controls.Add(this.label2);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxName);
            this.groupBoxBaseInfo.Controls.Add(this.label3);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxNO);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxNum);
            this.groupBoxBaseInfo.Controls.Add(this.label1);
            this.groupBoxBaseInfo.Location = new System.Drawing.Point(11, 53);
            this.groupBoxBaseInfo.Name = "groupBoxBaseInfo";
            this.groupBoxBaseInfo.Size = new System.Drawing.Size(612, 338);
            this.groupBoxBaseInfo.TabIndex = 35;
            this.groupBoxBaseInfo.TabStop = false;
            this.groupBoxBaseInfo.Text = "员工信息";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(238, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 58;
            this.label6.Text = "电子邮箱";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmail.Location = new System.Drawing.Point(299, 74);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(107, 21);
            this.textBoxEmail.TabIndex = 59;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(63, 74);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(158, 21);
            this.dateTimePickerDate.TabIndex = 57;
            this.dateTimePickerDate.ValueChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSex.FormattingEnabled = true;
            this.comboBoxSex.Location = new System.Drawing.Point(63, 44);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(102, 20);
            this.comboBoxSex.TabIndex = 56;
            this.comboBoxSex.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 55;
            this.label5.Text = "性别";
            // 
            // comboBoxEdu
            // 
            this.comboBoxEdu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEdu.FormattingEnabled = true;
            this.comboBoxEdu.Location = new System.Drawing.Point(489, 106);
            this.comboBoxEdu.Name = "comboBoxEdu";
            this.comboBoxEdu.Size = new System.Drawing.Size(105, 20);
            this.comboBoxEdu.TabIndex = 54;
            this.comboBoxEdu.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "学历";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 180);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 52;
            this.label26.Text = "备注";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.Location = new System.Drawing.Point(63, 177);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(531, 155);
            this.textBoxNote.TabIndex = 51;
            this.textBoxNote.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 143);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 12);
            this.label24.TabIndex = 47;
            this.label24.Text = "住址";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress.Location = new System.Drawing.Point(63, 141);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(531, 21);
            this.textBoxAddress.TabIndex = 48;
            this.textBoxAddress.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(489, 44);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(105, 20);
            this.comboBoxType.TabIndex = 44;
            this.comboBoxType.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(427, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 43;
            this.label18.Text = "职工类型";
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Location = new System.Drawing.Point(299, 44);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(107, 20);
            this.comboBoxProfile.TabIndex = 42;
            this.comboBoxProfile.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(238, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 41;
            this.label16.Text = "职务";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 37;
            this.label14.Text = "入职日期";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(427, 77);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 35;
            this.label22.Text = "联系电话";
            // 
            // textBoxTel
            // 
            this.textBoxTel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTel.Location = new System.Drawing.Point(489, 74);
            this.textBoxTel.Name = "textBoxTel";
            this.textBoxTel.Size = new System.Drawing.Size(105, 21);
            this.textBoxTel.TabIndex = 36;
            this.textBoxTel.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMAX_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "姓名";
            // 
            // textBoxNum
            // 
            this.textBoxNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNum.Location = new System.Drawing.Point(63, 15);
            this.textBoxNum.Name = "textBoxNum";
            this.textBoxNum.Size = new System.Drawing.Size(53, 21);
            this.textBoxNum.TabIndex = 17;
            this.textBoxNum.Visible = false;
            this.textBoxNum.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMAX_KeyPress);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(63, 15);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(531, 21);
            this.textBoxName.TabIndex = 19;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "编号";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "身份证号";
            // 
            // textBoxNO
            // 
            this.textBoxNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNO.Location = new System.Drawing.Point(63, 106);
            this.textBoxNO.Name = "textBoxNO";
            this.textBoxNO.Size = new System.Drawing.Size(343, 21);
            this.textBoxNO.TabIndex = 21;
            this.textBoxNO.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMAX_KeyPress);
            // 
            // FormStaffEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 398);
            this.Controls.Add(this.groupBoxBaseInfo);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStaffEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "员工信息";
            this.Load += new System.EventHandler(this.FormStaffEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxBaseInfo.ResumeLayout(false);
            this.groupBoxBaseInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonExit;
        private System.Windows.Forms.GroupBox groupBoxBaseInfo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxTel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNum;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNO;
        private System.Windows.Forms.ComboBox comboBoxEdu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxEmail;
    }
}