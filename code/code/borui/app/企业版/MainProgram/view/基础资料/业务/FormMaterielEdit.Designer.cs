namespace MainProgram
{
    partial class FormMaterielEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaterielEdit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonMaterielGroup = new System.Windows.Forms.ToolStripButton();
            this.buttonExit = new System.Windows.Forms.ToolStripButton();
            this.groupBoxMaterielGroup = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxMaterielGroupName = new System.Windows.Forms.TextBox();
            this.textBoxMaterielGroupDesc = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tabPageBaseInfo = new System.Windows.Forms.TabPage();
            this.groupBoxBaseInfo = new System.Windows.Forms.GroupBox();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxValuationType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxMIN = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxMAX = new System.Windows.Forms.TextBox();
            this.comboBoxUnitSale = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxUnitPurchase = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxUnit = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxWarranty = new System.Windows.Forms.TextBox();
            this.comboBoxMaterielAttribut = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxMnemonicCode = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxShortName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1.SuspendLayout();
            this.groupBoxMaterielGroup.SuspendLayout();
            this.tabPageBaseInfo.SuspendLayout();
            this.groupBoxBaseInfo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAdd,
            this.buttonMaterielGroup,
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
            // buttonMaterielGroup
            // 
            this.buttonMaterielGroup.BackColor = System.Drawing.Color.Transparent;
            this.buttonMaterielGroup.Image = global::MainProgram.Properties.Resources.SupplierGroup;
            this.buttonMaterielGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonMaterielGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonMaterielGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMaterielGroup.Name = "buttonMaterielGroup";
            this.buttonMaterielGroup.Size = new System.Drawing.Size(60, 42);
            this.buttonMaterielGroup.Text = "物料分组";
            this.buttonMaterielGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonMaterielGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.buttonMaterielGroup.Click += new System.EventHandler(this.buttonMaterielGroup_Click);
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
            // groupBoxMaterielGroup
            // 
            this.groupBoxMaterielGroup.Controls.Add(this.label21);
            this.groupBoxMaterielGroup.Controls.Add(this.label19);
            this.groupBoxMaterielGroup.Controls.Add(this.textBoxMaterielGroupName);
            this.groupBoxMaterielGroup.Controls.Add(this.textBoxMaterielGroupDesc);
            this.groupBoxMaterielGroup.Controls.Add(this.label23);
            this.groupBoxMaterielGroup.Location = new System.Drawing.Point(12, 53);
            this.groupBoxMaterielGroup.Name = "groupBoxMaterielGroup";
            this.groupBoxMaterielGroup.Size = new System.Drawing.Size(606, 88);
            this.groupBoxMaterielGroup.TabIndex = 36;
            this.groupBoxMaterielGroup.TabStop = false;
            this.groupBoxMaterielGroup.Text = "基础信息";
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
            // textBoxMaterielGroupName
            // 
            this.textBoxMaterielGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMaterielGroupName.Location = new System.Drawing.Point(61, 24);
            this.textBoxMaterielGroupName.Name = "textBoxMaterielGroupName";
            this.textBoxMaterielGroupName.Size = new System.Drawing.Size(536, 21);
            this.textBoxMaterielGroupName.TabIndex = 17;
            this.textBoxMaterielGroupName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxMaterielGroupDesc
            // 
            this.textBoxMaterielGroupDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMaterielGroupDesc.Location = new System.Drawing.Point(62, 57);
            this.textBoxMaterielGroupDesc.Name = "textBoxMaterielGroupDesc";
            this.textBoxMaterielGroupDesc.Size = new System.Drawing.Size(535, 21);
            this.textBoxMaterielGroupDesc.TabIndex = 19;
            this.textBoxMaterielGroupDesc.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
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
            // tabPageBaseInfo
            // 
            this.tabPageBaseInfo.Controls.Add(this.groupBoxBaseInfo);
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
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxStorage);
            this.groupBoxBaseInfo.Controls.Add(this.label5);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxValuationType);
            this.groupBoxBaseInfo.Controls.Add(this.label4);
            this.groupBoxBaseInfo.Controls.Add(this.label26);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxNote);
            this.groupBoxBaseInfo.Controls.Add(this.label25);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxMIN);
            this.groupBoxBaseInfo.Controls.Add(this.label24);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxMAX);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxUnitSale);
            this.groupBoxBaseInfo.Controls.Add(this.label17);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxUnitPurchase);
            this.groupBoxBaseInfo.Controls.Add(this.label18);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxUnit);
            this.groupBoxBaseInfo.Controls.Add(this.label16);
            this.groupBoxBaseInfo.Controls.Add(this.label15);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxWarranty);
            this.groupBoxBaseInfo.Controls.Add(this.comboBoxMaterielAttribut);
            this.groupBoxBaseInfo.Controls.Add(this.label14);
            this.groupBoxBaseInfo.Controls.Add(this.label22);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxMnemonicCode);
            this.groupBoxBaseInfo.Controls.Add(this.label20);
            this.groupBoxBaseInfo.Controls.Add(this.label2);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxName);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxShortName);
            this.groupBoxBaseInfo.Controls.Add(this.label1);
            this.groupBoxBaseInfo.Controls.Add(this.label3);
            this.groupBoxBaseInfo.Controls.Add(this.textBoxModel);
            this.groupBoxBaseInfo.Location = new System.Drawing.Point(4, 6);
            this.groupBoxBaseInfo.Name = "groupBoxBaseInfo";
            this.groupBoxBaseInfo.Size = new System.Drawing.Size(592, 305);
            this.groupBoxBaseInfo.TabIndex = 35;
            this.groupBoxBaseInfo.TabStop = false;
            this.groupBoxBaseInfo.Text = "基础信息";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(471, 89);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(106, 20);
            this.comboBoxStorage.TabIndex = 56;
            this.comboBoxStorage.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(391, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 55;
            this.label5.Text = "收料仓库";
            // 
            // comboBoxValuationType
            // 
            this.comboBoxValuationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxValuationType.FormattingEnabled = true;
            this.comboBoxValuationType.Location = new System.Drawing.Point(472, 120);
            this.comboBoxValuationType.Name = "comboBoxValuationType";
            this.comboBoxValuationType.Size = new System.Drawing.Size(106, 20);
            this.comboBoxValuationType.TabIndex = 54;
            this.comboBoxValuationType.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(392, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "计价方式";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(2, 218);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 52;
            this.label26.Text = "备注";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.Location = new System.Drawing.Point(85, 218);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(494, 79);
            this.textBoxNote.TabIndex = 51;
            this.textBoxNote.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(195, 188);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 49;
            this.label25.Text = "存货下限";
            // 
            // textBoxMIN
            // 
            this.textBoxMIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMIN.Location = new System.Drawing.Point(279, 185);
            this.textBoxMIN.Name = "textBoxMIN";
            this.textBoxMIN.Size = new System.Drawing.Size(87, 21);
            this.textBoxMIN.TabIndex = 50;
            this.textBoxMIN.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxMIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMAX_KeyPress);
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 187);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 47;
            this.label24.Text = "存货上限";
            // 
            // textBoxMAX
            // 
            this.textBoxMAX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMAX.Location = new System.Drawing.Point(86, 185);
            this.textBoxMAX.Name = "textBoxMAX";
            this.textBoxMAX.Size = new System.Drawing.Size(88, 21);
            this.textBoxMAX.TabIndex = 48;
            this.textBoxMAX.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxMAX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMAX_KeyPress);
            // 
            // comboBoxUnitSale
            // 
            this.comboBoxUnitSale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnitSale.FormattingEnabled = true;
            this.comboBoxUnitSale.Location = new System.Drawing.Point(472, 152);
            this.comboBoxUnitSale.Name = "comboBoxUnitSale";
            this.comboBoxUnitSale.Size = new System.Drawing.Size(107, 20);
            this.comboBoxUnitSale.TabIndex = 46;
            this.comboBoxUnitSale.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(392, 155);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 12);
            this.label17.TabIndex = 45;
            this.label17.Text = "销售计量单位";
            // 
            // comboBoxUnitPurchase
            // 
            this.comboBoxUnitPurchase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnitPurchase.FormattingEnabled = true;
            this.comboBoxUnitPurchase.Location = new System.Drawing.Point(273, 153);
            this.comboBoxUnitPurchase.Name = "comboBoxUnitPurchase";
            this.comboBoxUnitPurchase.Size = new System.Drawing.Size(90, 20);
            this.comboBoxUnitPurchase.TabIndex = 44;
            this.comboBoxUnitPurchase.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(194, 156);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 12);
            this.label18.TabIndex = 43;
            this.label18.Text = "采购计量单位";
            // 
            // comboBoxUnit
            // 
            this.comboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnit.FormattingEnabled = true;
            this.comboBoxUnit.Location = new System.Drawing.Point(85, 153);
            this.comboBoxUnit.Name = "comboBoxUnit";
            this.comboBoxUnit.Size = new System.Drawing.Size(90, 20);
            this.comboBoxUnit.TabIndex = 42;
            this.comboBoxUnit.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 155);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 41;
            this.label16.Text = "基本计量单位";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(394, 190);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 39;
            this.label15.Text = "保质期";
            // 
            // textBoxWarranty
            // 
            this.textBoxWarranty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWarranty.Location = new System.Drawing.Point(473, 185);
            this.textBoxWarranty.Name = "textBoxWarranty";
            this.textBoxWarranty.Size = new System.Drawing.Size(81, 21);
            this.textBoxWarranty.TabIndex = 40;
            this.textBoxWarranty.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxWarranty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMAX_KeyPress);
            // 
            // comboBoxMaterielAttribut
            // 
            this.comboBoxMaterielAttribut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterielAttribut.FormattingEnabled = true;
            this.comboBoxMaterielAttribut.Location = new System.Drawing.Point(85, 121);
            this.comboBoxMaterielAttribut.Name = "comboBoxMaterielAttribut";
            this.comboBoxMaterielAttribut.Size = new System.Drawing.Size(281, 20);
            this.comboBoxMaterielAttribut.TabIndex = 38;
            this.comboBoxMaterielAttribut.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 124);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 37;
            this.label14.Text = "物料属性";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(402, 61);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 35;
            this.label22.Text = "助记码";
            // 
            // textBoxMnemonicCode
            // 
            this.textBoxMnemonicCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMnemonicCode.Location = new System.Drawing.Point(451, 57);
            this.textBoxMnemonicCode.Name = "textBoxMnemonicCode";
            this.textBoxMnemonicCode.Size = new System.Drawing.Size(128, 21);
            this.textBoxMnemonicCode.TabIndex = 36;
            this.textBoxMnemonicCode.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(563, 192);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 12);
            this.label20.TabIndex = 34;
            this.label20.Text = "天";
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
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(85, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(494, 21);
            this.textBoxName.TabIndex = 17;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShortName.Location = new System.Drawing.Point(85, 57);
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(280, 21);
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
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "规格型号";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxModel.Location = new System.Drawing.Point(85, 89);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(278, 21);
            this.textBoxModel.TabIndex = 21;
            this.textBoxModel.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageBaseInfo);
            this.tabControl1.Location = new System.Drawing.Point(13, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 345);
            this.tabControl1.TabIndex = 16;
            // 
            // FormMaterielEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 398);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBoxMaterielGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FormMaterielEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "物料编辑";
            this.Load += new System.EventHandler(this.FormMaterielEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxMaterielGroup.ResumeLayout(false);
            this.groupBoxMaterielGroup.PerformLayout();
            this.tabPageBaseInfo.ResumeLayout(false);
            this.groupBoxBaseInfo.ResumeLayout(false);
            this.groupBoxBaseInfo.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonMaterielGroup;
        private System.Windows.Forms.ToolStripButton buttonExit;
        private System.Windows.Forms.GroupBox groupBoxMaterielGroup;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxMaterielGroupName;
        private System.Windows.Forms.TextBox textBoxMaterielGroupDesc;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabPage tabPageBaseInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBoxBaseInfo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBoxMIN;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxMAX;
        private System.Windows.Forms.ComboBox comboBoxUnitSale;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxUnitPurchase;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBoxUnit;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxWarranty;
        private System.Windows.Forms.ComboBox comboBoxMaterielAttribut;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxMnemonicCode;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxShortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.ComboBox comboBoxValuationType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.Label label5;
    }
}