namespace MainProgram
{
    partial class FormNoForbidSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNoForbidSupplier));
            this.listViewNoForbidList = new System.Windows.Forms.ListView();
            this.buttonAllSelect = new System.Windows.Forms.Button();
            this.buttonNoForbid = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewNoForbidList
            // 
            this.listViewNoForbidList.CheckBoxes = true;
            this.listViewNoForbidList.Location = new System.Drawing.Point(13, 13);
            this.listViewNoForbidList.Name = "listViewNoForbidList";
            this.listViewNoForbidList.Size = new System.Drawing.Size(359, 265);
            this.listViewNoForbidList.TabIndex = 0;
            this.listViewNoForbidList.UseCompatibleStateImageBehavior = false;
            // 
            // buttonAllSelect
            // 
            this.buttonAllSelect.Location = new System.Drawing.Point(13, 284);
            this.buttonAllSelect.Name = "buttonAllSelect";
            this.buttonAllSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonAllSelect.TabIndex = 1;
            this.buttonAllSelect.Text = "全选";
            this.buttonAllSelect.UseVisualStyleBackColor = true;
            this.buttonAllSelect.Click += new System.EventHandler(this.buttonAllSelect_Click);
            // 
            // buttonNoForbid
            // 
            this.buttonNoForbid.Location = new System.Drawing.Point(209, 284);
            this.buttonNoForbid.Name = "buttonNoForbid";
            this.buttonNoForbid.Size = new System.Drawing.Size(75, 23);
            this.buttonNoForbid.TabIndex = 2;
            this.buttonNoForbid.Text = "取消禁用";
            this.buttonNoForbid.UseVisualStyleBackColor = true;
            this.buttonNoForbid.Click += new System.EventHandler(this.buttonNoForbid_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(297, 284);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormNoForbidSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 312);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonNoForbid);
            this.Controls.Add(this.buttonAllSelect);
            this.Controls.Add(this.listViewNoForbidList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNoForbidSupplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormNoForbidSupplier";
            this.Load += new System.EventHandler(this.FormNoForbidSupplier_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewNoForbidList;
        private System.Windows.Forms.Button buttonAllSelect;
        private System.Windows.Forms.Button buttonNoForbid;
        private System.Windows.Forms.Button buttonClose;
    }
}