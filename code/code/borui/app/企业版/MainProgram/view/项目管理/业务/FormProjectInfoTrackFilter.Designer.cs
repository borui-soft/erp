namespace MainProgram
{
    partial class FormProjectInfoTrackFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProjectInfoTrackFilter));
            this.buttonEnter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxProjectNum = new System.Windows.Forms.TextBox();
            this.labelProjectNum = new System.Windows.Forms.Label();
            this.comboBoxReview = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewProList = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(371, 125);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 0;
            this.buttonEnter.Text = "确定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewProList);
            this.groupBox1.Controls.Add(this.textBoxProjectNum);
            this.groupBox1.Controls.Add(this.labelProjectNum);
            this.groupBox1.Controls.Add(this.comboBoxReview);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 94);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件";
            // 
            // textBoxProjectNum
            // 
            this.textBoxProjectNum.Location = new System.Drawing.Point(79, 21);
            this.textBoxProjectNum.Name = "textBoxProjectNum";
            this.textBoxProjectNum.Size = new System.Drawing.Size(216, 21);
            this.textBoxProjectNum.TabIndex = 0;
            this.textBoxProjectNum.TextChanged += new System.EventHandler(this.textBoxProjectNum_TextChanged);
            // 
            // labelProjectNum
            // 
            this.labelProjectNum.AutoSize = true;
            this.labelProjectNum.Location = new System.Drawing.Point(7, 25);
            this.labelProjectNum.Name = "labelProjectNum";
            this.labelProjectNum.Size = new System.Drawing.Size(71, 12);
            this.labelProjectNum.TabIndex = 6;
            this.labelProjectNum.Text = "项目编号(*)";
            // 
            // comboBoxReview
            // 
            this.comboBoxReview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReview.FormattingEnabled = true;
            this.comboBoxReview.Location = new System.Drawing.Point(79, 59);
            this.comboBoxReview.Name = "comboBoxReview";
            this.comboBoxReview.Size = new System.Drawing.Size(108, 20);
            this.comboBoxReview.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "审核标志";
            // 
            // listViewProList
            // 
            this.listViewProList.Location = new System.Drawing.Point(291, 21);
            this.listViewProList.Name = "listViewProList";
            this.listViewProList.Size = new System.Drawing.Size(137, 85);
            this.listViewProList.TabIndex = 7;
            this.listViewProList.UseCompatibleStateImageBehavior = false;
            this.listViewProList.Visible = false;
            this.listViewProList.Leave += new System.EventHandler(this.listViewProList_Leave);
            this.listViewProList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewProList_MouseClick);
            this.listViewProList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewProList_MouseDoubleClick);
            // 
            // FormProjectInfoTrackFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(455, 161);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonEnter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProjectInfoTrackFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据过滤器";
            this.Load += new System.EventHandler(this.FormProjectInfoTrackFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxReview;
        private System.Windows.Forms.Label labelProjectNum;
        private System.Windows.Forms.TextBox textBoxProjectNum;
        private System.Windows.Forms.ListView listViewProList;
    }
}