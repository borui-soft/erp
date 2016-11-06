namespace MainProgram
{
    partial class FormAdjustAmount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdjustAmount));
            this.labelAmount = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonAdjustAmount = new System.Windows.Forms.Button();
            this.buttonEnter2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(15, 124);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(35, 14);
            this.labelAmount.TabIndex = 1;
            this.labelAmount.Text = "金额";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 149);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 23);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // buttonAdjustAmount
            // 
            this.buttonAdjustAmount.Location = new System.Drawing.Point(134, 186);
            this.buttonAdjustAmount.Name = "buttonAdjustAmount";
            this.buttonAdjustAmount.Size = new System.Drawing.Size(87, 27);
            this.buttonAdjustAmount.TabIndex = 3;
            this.buttonAdjustAmount.Text = "金额调整";
            this.buttonAdjustAmount.UseVisualStyleBackColor = true;
            this.buttonAdjustAmount.Click += new System.EventHandler(this.buttonAdjustAmount_Click);
            // 
            // buttonEnter2
            // 
            this.buttonEnter2.Location = new System.Drawing.Point(234, 186);
            this.buttonEnter2.Name = "buttonEnter2";
            this.buttonEnter2.Size = new System.Drawing.Size(87, 27);
            this.buttonEnter2.TabIndex = 4;
            this.buttonEnter2.Text = "确定";
            this.buttonEnter2.UseVisualStyleBackColor = true;
            this.buttonEnter2.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "(只能输入数字或小数点)";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(12, 12);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.Size = new System.Drawing.Size(307, 99);
            this.textBoxMessage.TabIndex = 6;
            // 
            // FormAdjustAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 221);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonEnter2);
            this.Controls.Add(this.buttonAdjustAmount);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelAmount);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdjustAmount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "应收\\应付金额调整";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAdjustAmount_FormClosed);
            this.Load += new System.EventHandler(this.FormAdjustAmount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAdjustAmount;
        private System.Windows.Forms.Button buttonEnter2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMessage;
    }
}