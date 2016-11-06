namespace MainProgram
{
    partial class FormSupplierEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSupplierEdit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonSupplierGroup = new System.Windows.Forms.ToolStripButton();
            this.buttonExit = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBaseInfo = new System.Windows.Forms.TabPage();
            this.groupBoxBaseInfo = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxCredit = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxShortName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVatRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMnemonicCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBoxBankInfo = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxTaxAccount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxBankAccount = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxBankName = new System.Windows.Forms.TextBox();
            this.tabPageContact = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxZipCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxHomePage = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMobilePhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxFax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxArea = new System.Windows.Forms.ComboBox();
            this.textBoxContact = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxSupplierGroup = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxSupplierGroupName = new System.Windows.Forms.TextBox();
            this.textBoxSupplierGroupDesc = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageBaseInfo.SuspendLayout();
            this.groupBoxBaseInfo.SuspendLayout();
            this.groupBoxBankInfo.SuspendLayout();
            this.tabPageContact.SuspendLayout();
            this.groupBoxSupplierGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAdd,
            this.buttonSupplierGroup,
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
            this.buttonAdd.Size = new System.Drawing.Size(44, 42);
            this.buttonAdd.Text = " 保存 ";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSupplierGroup
            // 
            this.buttonSupplierGroup.BackColor = System.Drawing.Color.Transparent;
            this.buttonSupplierGroup.Image = global::MainProgram.Properties.Resources.SupplierGroup;
            this.buttonSupplierGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSupplierGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSupplierGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSupplierGroup.Name = "buttonSupplierGroup";
            this.buttonSupplierGroup.Size = new System.Drawing.Size(72, 42);
            this.buttonSupplierGroup.Text = "供应商分组";
            this.buttonSupplierGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSupplierGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.buttonSupplierGroup.Click += new System.EventHandler(this.buttonSupplierGroup_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Image = global::MainProgram.Properties.Resources.close;
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(44, 42);
            this.buttonExit.Text = " 退出 ";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageBaseInfo);
            this.tabControl1.Controls.Add(this.tabPageContact);
            this.tabControl1.Location = new System.Drawing.Point(13, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 345);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPageBaseInfo
            // 
            this.tabPageBaseInfo.Controls.Add(this.groupBoxBaseInfo);
            this.tabPageBaseInfo.Controls.Add(this.groupBoxBankInfo);
            this.tabPageBaseInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageBaseInfo.Name = "tabPageBaseInfo";
            this.tabPageBaseInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBaseInfo.Size = new System.Drawing.Size(601, 319);
            this.tabPageBaseInfo.TabIndex = 0;
            this.tabPageBaseInfo.Text = "基本信息";
            this.tabPageBaseInfo.UseVisualStyleBackColor = true;
            // 
            // groupBoxBaseInfo
            // 
            this.groupBoxBaseInfo.Controls.Add(this.label20);
            this.groupBoxBaseInfo.Controls.Add(this.label2);
            this.groupBoxBaseInfo.Controls.Add(this.label18);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxCredit);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxName);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxShortName);
            this.groupBoxBaseInfo.Controls.Add(this.label1);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxVatRate);
            this.groupBoxBaseInfo.Controls.Add(this.label3);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxMnemonicCode);
            this.groupBoxBaseInfo.Controls.Add(this.label17);
            this.groupBoxBaseInfo.Location = new System.Drawing.Point(4, 6);
            this.groupBoxBaseInfo.Name = "groupBoxBaseInfo";
            this.groupBoxBaseInfo.Size = new System.Drawing.Size(592, 157);
            this.groupBoxBaseInfo.TabIndex = 35;
            this.groupBoxBaseInfo.TabStop = false;
            this.groupBoxBaseInfo.Text = "基础信息";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(568, 129);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 34;
            this.label20.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "简称";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 131);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 28;
            this.label18.Text = "信用额度";
            // 
            // textBoxCredit
            // 
            this.textBoxCredit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCredit.Location = new System.Drawing.Point(63, 124);
            this.textBoxCredit.Name = "textBoxCredit";
            this.textBoxCredit.Size = new System.Drawing.Size(296, 21);
            this.textBoxCredit.TabIndex = 29;
            this.textBoxCredit.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(61, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(522, 21);
            this.textBoxName.TabIndex = 17;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShortName.Location = new System.Drawing.Point(62, 57);
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(303, 21);
            this.textBoxShortName.TabIndex = 19;
            this.textBoxShortName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "名称";
            // 
            // textBoxVatRate
            // 
            this.textBoxVatRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVatRate.Location = new System.Drawing.Point(457, 124);
            this.textBoxVatRate.Name = "textBoxVatRate";
            this.textBoxVatRate.Size = new System.Drawing.Size(108, 21);
            this.textBoxVatRate.TabIndex = 31;
            this.textBoxVatRate.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "助记码";
            // 
            // textBoxMnemonicCode
            // 
            this.textBoxMnemonicCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMnemonicCode.Location = new System.Drawing.Point(64, 89);
            this.textBoxMnemonicCode.Name = "textBoxMnemonicCode";
            this.textBoxMnemonicCode.Size = new System.Drawing.Size(128, 21);
            this.textBoxMnemonicCode.TabIndex = 21;
            this.textBoxMnemonicCode.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(387, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 30;
            this.label17.Text = "增值税税率";
            // 
            // groupBoxBankInfo
            // 
            this.groupBoxBankInfo.Controls.Add(this.label14);
            this.groupBoxBankInfo.Controls.Add(this.textBoxTaxAccount);
            this.groupBoxBankInfo.Controls.Add(this.label15);
            this.groupBoxBankInfo.Controls.Add(this.textBoxBankAccount);
            this.groupBoxBankInfo.Controls.Add(this.label16);
            this.groupBoxBankInfo.Controls.Add(this.textBoxBankName);
            this.groupBoxBankInfo.Location = new System.Drawing.Point(4, 176);
            this.groupBoxBankInfo.Name = "groupBoxBankInfo";
            this.groupBoxBankInfo.Size = new System.Drawing.Size(591, 125);
            this.groupBoxBankInfo.TabIndex = 34;
            this.groupBoxBankInfo.TabStop = false;
            this.groupBoxBankInfo.Text = "银行信息";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 22;
            this.label14.Text = "单位税号";
            // 
            // textBoxTaxAccount
            // 
            this.textBoxTaxAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTaxAccount.Location = new System.Drawing.Point(61, 95);
            this.textBoxTaxAccount.Name = "textBoxTaxAccount";
            this.textBoxTaxAccount.Size = new System.Drawing.Size(515, 21);
            this.textBoxTaxAccount.TabIndex = 23;
            this.textBoxTaxAccount.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 24;
            this.label15.Text = "银行账户";
            // 
            // textBoxBankAccount
            // 
            this.textBoxBankAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBankAccount.Location = new System.Drawing.Point(61, 58);
            this.textBoxBankAccount.Name = "textBoxBankAccount";
            this.textBoxBankAccount.Size = new System.Drawing.Size(514, 21);
            this.textBoxBankAccount.TabIndex = 25;
            this.textBoxBankAccount.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 26;
            this.label16.Text = "开户银行";
            // 
            // textBoxBankName
            // 
            this.textBoxBankName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBankName.Location = new System.Drawing.Point(61, 20);
            this.textBoxBankName.Name = "textBoxBankName";
            this.textBoxBankName.Size = new System.Drawing.Size(513, 21);
            this.textBoxBankName.TabIndex = 27;
            this.textBoxBankName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // tabPageContact
            // 
            this.tabPageContact.Controls.Add(this.label13);
            this.tabPageContact.Controls.Add(this.textBoxNote);
            this.tabPageContact.Controls.Add(this.label12);
            this.tabPageContact.Controls.Add(this.textBoxZipCode);
            this.tabPageContact.Controls.Add(this.label11);
            this.tabPageContact.Controls.Add(this.textBoxAddress);
            this.tabPageContact.Controls.Add(this.label9);
            this.tabPageContact.Controls.Add(this.textBoxEmail);
            this.tabPageContact.Controls.Add(this.label10);
            this.tabPageContact.Controls.Add(this.textBoxHomePage);
            this.tabPageContact.Controls.Add(this.label8);
            this.tabPageContact.Controls.Add(this.textBoxMobilePhone);
            this.tabPageContact.Controls.Add(this.label7);
            this.tabPageContact.Controls.Add(this.textBoxFax);
            this.tabPageContact.Controls.Add(this.label6);
            this.tabPageContact.Controls.Add(this.textBoxTel);
            this.tabPageContact.Controls.Add(this.label5);
            this.tabPageContact.Controls.Add(this.comboBoxArea);
            this.tabPageContact.Controls.Add(this.textBoxContact);
            this.tabPageContact.Controls.Add(this.label4);
            this.tabPageContact.Location = new System.Drawing.Point(4, 22);
            this.tabPageContact.Name = "tabPageContact";
            this.tabPageContact.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContact.Size = new System.Drawing.Size(601, 319);
            this.tabPageContact.TabIndex = 1;
            this.tabPageContact.Text = "联系方式";
            this.tabPageContact.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 224);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 37;
            this.label13.Text = "备注";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.Location = new System.Drawing.Point(67, 221);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(513, 86);
            this.textBoxNote.TabIndex = 36;
            this.textBoxNote.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 35;
            this.label12.Text = "邮编";
            // 
            // textBoxZipCode
            // 
            this.textBoxZipCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxZipCode.Location = new System.Drawing.Point(65, 192);
            this.textBoxZipCode.Name = "textBoxZipCode";
            this.textBoxZipCode.Size = new System.Drawing.Size(132, 21);
            this.textBoxZipCode.TabIndex = 34;
            this.textBoxZipCode.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 33;
            this.label11.Text = "详细地址";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress.Location = new System.Drawing.Point(65, 161);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(515, 21);
            this.textBoxAddress.TabIndex = 32;
            this.textBoxAddress.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 31;
            this.label9.Text = "电子邮件";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmail.Location = new System.Drawing.Point(65, 134);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(132, 21);
            this.textBoxEmail.TabIndex = 30;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "公司主页";
            // 
            // textBoxHomePage
            // 
            this.textBoxHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHomePage.Location = new System.Drawing.Point(65, 103);
            this.textBoxHomePage.Name = "textBoxHomePage";
            this.textBoxHomePage.Size = new System.Drawing.Size(317, 21);
            this.textBoxHomePage.TabIndex = 28;
            this.textBoxHomePage.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(406, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "手机";
            // 
            // textBoxMobilePhone
            // 
            this.textBoxMobilePhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMobilePhone.Location = new System.Drawing.Point(448, 74);
            this.textBoxMobilePhone.Name = "textBoxMobilePhone";
            this.textBoxMobilePhone.Size = new System.Drawing.Size(132, 21);
            this.textBoxMobilePhone.TabIndex = 26;
            this.textBoxMobilePhone.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(219, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "传真";
            // 
            // textBoxFax
            // 
            this.textBoxFax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFax.Location = new System.Drawing.Point(261, 74);
            this.textBoxFax.Name = "textBoxFax";
            this.textBoxFax.Size = new System.Drawing.Size(121, 21);
            this.textBoxFax.TabIndex = 24;
            this.textBoxFax.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "电话";
            // 
            // textBoxTel
            // 
            this.textBoxTel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTel.Location = new System.Drawing.Point(65, 74);
            this.textBoxTel.Name = "textBoxTel";
            this.textBoxTel.Size = new System.Drawing.Size(121, 21);
            this.textBoxTel.TabIndex = 22;
            this.textBoxTel.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "联系人";
            // 
            // comboBoxArea
            // 
            this.comboBoxArea.FormattingEnabled = true;
            this.comboBoxArea.Location = new System.Drawing.Point(65, 13);
            this.comboBoxArea.Name = "comboBoxArea";
            this.comboBoxArea.Size = new System.Drawing.Size(121, 20);
            this.comboBoxArea.TabIndex = 20;
            this.comboBoxArea.SelectedIndexChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxContact
            // 
            this.textBoxContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContact.Location = new System.Drawing.Point(65, 43);
            this.textBoxContact.Name = "textBoxContact";
            this.textBoxContact.Size = new System.Drawing.Size(175, 21);
            this.textBoxContact.TabIndex = 19;
            this.textBoxContact.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "区域";
            // 
            // groupBoxSupplierGroup
            // 
            this.groupBoxSupplierGroup.Controls.Add(this.label21);
            this.groupBoxSupplierGroup.Controls.Add(this.label19);
            this.groupBoxSupplierGroup.Controls.Add(this.textBoxSupplierGroupName);
            this.groupBoxSupplierGroup.Controls.Add(this.textBoxSupplierGroupDesc);
            this.groupBoxSupplierGroup.Controls.Add(this.label23);
            this.groupBoxSupplierGroup.Location = new System.Drawing.Point(12, 53);
            this.groupBoxSupplierGroup.Name = "groupBoxSupplierGroup";
            this.groupBoxSupplierGroup.Size = new System.Drawing.Size(606, 88);
            this.groupBoxSupplierGroup.TabIndex = 36;
            this.groupBoxSupplierGroup.TabStop = false;
            this.groupBoxSupplierGroup.Text = "基础信息";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 61);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 35;
            this.label21.Text = "描述";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(582, 129);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 34;
            this.label19.Text = "%";
            // 
            // textBoxSupplierGroupName
            // 
            this.textBoxSupplierGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSupplierGroupName.Location = new System.Drawing.Point(61, 24);
            this.textBoxSupplierGroupName.Name = "textBoxSupplierGroupName";
            this.textBoxSupplierGroupName.Size = new System.Drawing.Size(536, 21);
            this.textBoxSupplierGroupName.TabIndex = 17;
            this.textBoxSupplierGroupName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxSupplierGroupDesc
            // 
            this.textBoxSupplierGroupDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSupplierGroupDesc.Location = new System.Drawing.Point(62, 57);
            this.textBoxSupplierGroupDesc.Name = "textBoxSupplierGroupDesc";
            this.textBoxSupplierGroupDesc.Size = new System.Drawing.Size(535, 21);
            this.textBoxSupplierGroupDesc.TabIndex = 19;
            this.textBoxSupplierGroupDesc.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 26);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 16;
            this.label23.Text = "组名称";
            // 
            // FormSupplierEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 398);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBoxSupplierGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FormSupplierEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "供应商编辑";
            this.Load += new System.EventHandler(this.FormSupplierEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageBaseInfo.ResumeLayout(false);
            this.groupBoxBaseInfo.ResumeLayout(false);
            this.groupBoxBaseInfo.PerformLayout();
            this.groupBoxBankInfo.ResumeLayout(false);
            this.groupBoxBankInfo.PerformLayout();
            this.tabPageContact.ResumeLayout(false);
            this.tabPageContact.PerformLayout();
            this.groupBoxSupplierGroup.ResumeLayout(false);
            this.groupBoxSupplierGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonSupplierGroup;
        private System.Windows.Forms.ToolStripButton buttonExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageBaseInfo;
        private System.Windows.Forms.TabPage tabPageContact;
        private System.Windows.Forms.TextBox textBoxMnemonicCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxShortName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContact;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxArea;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMobilePhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxFax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxHomePage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxZipCode;
        private System.Windows.Forms.TextBox textBoxBankName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxBankAccount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxTaxAccount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxVatRate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxCredit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBoxBankInfo;
        private System.Windows.Forms.GroupBox groupBoxBaseInfo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBoxSupplierGroup;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxSupplierGroupName;
        private System.Windows.Forms.TextBox textBoxSupplierGroupDesc;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
    }
}