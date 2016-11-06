namespace MainProgram
{
    partial class DBBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBBackup));
            this.labelReason = new System.Windows.Forms.Label();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.textBoxNode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxFileDirectory = new System.Windows.Forms.TextBox();
            this.buttonSeleteDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.Location = new System.Drawing.Point(12, 74);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(53, 12);
            this.labelReason.TabIndex = 0;
            this.labelReason.Text = "备份原因";
            // 
            // textBoxReason
            // 
            this.textBoxReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReason.Location = new System.Drawing.Point(119, 45);
            this.textBoxReason.Multiline = true;
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.Size = new System.Drawing.Size(333, 71);
            this.textBoxReason.TabIndex = 1;
            // 
            // textBoxNode
            // 
            this.textBoxNode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNode.Location = new System.Drawing.Point(119, 128);
            this.textBoxNode.Multiline = true;
            this.textBoxNode.Name = "textBoxNode";
            this.textBoxNode.Size = new System.Drawing.Size(333, 137);
            this.textBoxNode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "备注";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(271, 272);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(377, 271);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 16);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(101, 12);
            this.labelPath.TabIndex = 6;
            this.labelPath.Text = "备份文件存放路径";
            // 
            // textBoxFileDirectory
            // 
            this.textBoxFileDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileDirectory.Location = new System.Drawing.Point(119, 11);
            this.textBoxFileDirectory.Name = "textBoxFileDirectory";
            this.textBoxFileDirectory.ReadOnly = true;
            this.textBoxFileDirectory.Size = new System.Drawing.Size(300, 21);
            this.textBoxFileDirectory.TabIndex = 7;
            // 
            // buttonSeleteDirectory
            // 
            this.buttonSeleteDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeleteDirectory.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSeleteDirectory.Location = new System.Drawing.Point(425, 9);
            this.buttonSeleteDirectory.Name = "buttonSeleteDirectory";
            this.buttonSeleteDirectory.Size = new System.Drawing.Size(27, 23);
            this.buttonSeleteDirectory.TabIndex = 8;
            this.buttonSeleteDirectory.Text = "...";
            this.buttonSeleteDirectory.UseVisualStyleBackColor = true;
            this.buttonSeleteDirectory.Click += new System.EventHandler(this.buttonSeleteDirectory_Click);
            // 
            // DBBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 307);
            this.Controls.Add(this.buttonSeleteDirectory);
            this.Controls.Add(this.textBoxFileDirectory);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxNode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxReason);
            this.Controls.Add(this.labelReason);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.Name = "DBBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库备份\\恢复";
            this.Load += new System.EventHandler(this.DBBackup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.TextBox textBoxNode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.TextBox textBoxFileDirectory;
        private System.Windows.Forms.Button buttonSeleteDirectory;
    }
}