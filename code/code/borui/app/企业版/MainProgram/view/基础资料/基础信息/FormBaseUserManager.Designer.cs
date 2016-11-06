namespace MainProgram
{
    partial class FormBaseUser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseUser));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.staffManager = new System.Windows.Forms.ToolStripButton();
            this.staffAdd = new System.Windows.Forms.ToolStripButton();
            this.staffRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.departmentManager = new System.Windows.Forms.ToolStripButton();
            this.departmentAdd = new System.Windows.Forms.ToolStripButton();
            this.departmentRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkDuty = new System.Windows.Forms.ToolStripButton();
            this.DutyManger = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuppierGroupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupModify = new System.Windows.Forms.ToolStripMenuItem();
            this.SuppierGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewUserOrg = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staffManager,
            this.staffAdd,
            this.staffRemove,
            this.toolStripSeparator1,
            this.departmentManager,
            this.departmentAdd,
            this.departmentRemove,
            this.toolStripSeparator2,
            this.checkDuty,
            this.DutyManger,
            this.toolStripSeparator4,
            this.close,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(705, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // staffManager
            // 
            this.staffManager.Image = global::MainProgram.Properties.Resources.用户管理;
            this.staffManager.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.staffManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.staffManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.staffManager.Name = "staffManager";
            this.staffManager.Size = new System.Drawing.Size(60, 42);
            this.staffManager.Text = "员工管理";
            this.staffManager.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.staffManager.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.staffManager.Click += new System.EventHandler(this.staffManager_Click);
            // 
            // staffAdd
            // 
            this.staffAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.staffAdd.Image = global::MainProgram.Properties.Resources.add;
            this.staffAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.staffAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.staffAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.staffAdd.Name = "staffAdd";
            this.staffAdd.Size = new System.Drawing.Size(60, 42);
            this.staffAdd.Text = "新建用户";
            this.staffAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.staffAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.staffAdd.Click += new System.EventHandler(this.staffAdd_Click);
            // 
            // staffRemove
            // 
            this.staffRemove.Image = global::MainProgram.Properties.Resources.delete;
            this.staffRemove.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.staffRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.staffRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.staffRemove.Name = "staffRemove";
            this.staffRemove.Size = new System.Drawing.Size(60, 42);
            this.staffRemove.Text = "移除用户";
            this.staffRemove.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.staffRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.staffRemove.Click += new System.EventHandler(this.staffRemove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // departmentManager
            // 
            this.departmentManager.Image = global::MainProgram.Properties.Resources.部门管理;
            this.departmentManager.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.departmentManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.departmentManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.departmentManager.Name = "departmentManager";
            this.departmentManager.Size = new System.Drawing.Size(60, 42);
            this.departmentManager.Text = "部门管理";
            this.departmentManager.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.departmentManager.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.departmentManager.Click += new System.EventHandler(this.departmentManager_Click);
            // 
            // departmentAdd
            // 
            this.departmentAdd.Image = global::MainProgram.Properties.Resources.add;
            this.departmentAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.departmentAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.departmentAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.departmentAdd.Name = "departmentAdd";
            this.departmentAdd.Size = new System.Drawing.Size(60, 42);
            this.departmentAdd.Text = "添加部门";
            this.departmentAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.departmentAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.departmentAdd.Click += new System.EventHandler(this.departmentAdd_Click);
            // 
            // departmentRemove
            // 
            this.departmentRemove.Image = global::MainProgram.Properties.Resources.delete;
            this.departmentRemove.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.departmentRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.departmentRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.departmentRemove.Name = "departmentRemove";
            this.departmentRemove.Size = new System.Drawing.Size(60, 42);
            this.departmentRemove.Text = "移除部门";
            this.departmentRemove.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.departmentRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.departmentRemove.Click += new System.EventHandler(this.departmentRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // checkDuty
            // 
            this.checkDuty.Image = global::MainProgram.Properties.Resources.search;
            this.checkDuty.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkDuty.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.checkDuty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkDuty.Name = "checkDuty";
            this.checkDuty.Size = new System.Drawing.Size(60, 42);
            this.checkDuty.Text = "权限查看";
            this.checkDuty.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.checkDuty.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.checkDuty.Click += new System.EventHandler(this.checkDuty_Click);
            // 
            // DutyManger
            // 
            this.DutyManger.Image = global::MainProgram.Properties.Resources.授权;
            this.DutyManger.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.DutyManger.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DutyManger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DutyManger.Name = "DutyManger";
            this.DutyManger.Size = new System.Drawing.Size(44, 42);
            this.DutyManger.Text = " 授权 ";
            this.DutyManger.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DutyManger.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.DutyManger.Click += new System.EventHandler(this.DutyManger_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 45);
            // 
            // close
            // 
            this.close.Image = global::MainProgram.Properties.Resources.close;
            this.close.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.close.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(36, 42);
            this.close.Text = "关闭";
            this.close.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.close.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 45);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "_CLOSE.GIF");
            this.imageList.Images.SetKeyName(1, "_OPEN.GIF");
            this.imageList.Images.SetKeyName(2, "noOnLine.png");
            this.imageList.Images.SetKeyName(3, "OnLine.png");
            // 
            // SuppierGroupAdd
            // 
            this.SuppierGroupAdd.Name = "SuppierGroupAdd";
            this.SuppierGroupAdd.Size = new System.Drawing.Size(32, 19);
            // 
            // SuppierGroupModify
            // 
            this.SuppierGroupModify.Name = "SuppierGroupModify";
            this.SuppierGroupModify.Size = new System.Drawing.Size(32, 19);
            // 
            // SuppierGroupDelete
            // 
            this.SuppierGroupDelete.Name = "SuppierGroupDelete";
            this.SuppierGroupDelete.Size = new System.Drawing.Size(32, 19);
            // 
            // treeViewUserOrg
            // 
            this.treeViewUserOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewUserOrg.ImageIndex = 0;
            this.treeViewUserOrg.ImageList = this.imageList;
            this.treeViewUserOrg.Location = new System.Drawing.Point(3, 48);
            this.treeViewUserOrg.Name = "treeViewUserOrg";
            this.treeViewUserOrg.SelectedImageIndex = 0;
            this.treeViewUserOrg.Size = new System.Drawing.Size(702, 440);
            this.treeViewUserOrg.TabIndex = 2;
            this.treeViewUserOrg.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewUserOrg_BeforeSelect);
            this.treeViewUserOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewUserOrg_AfterSelect);
            // 
            // FormBaseUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(705, 488);
            this.Controls.Add(this.treeViewUserOrg);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaseUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FormBaseUser_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton staffAdd;
        private System.Windows.Forms.ToolStripButton staffRemove;
        private System.Windows.Forms.ToolStripButton departmentAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton departmentRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton checkDuty;
        private System.Windows.Forms.ToolStripButton DutyManger;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupAdd;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupModify;
        private System.Windows.Forms.ToolStripMenuItem SuppierGroupDelete;
        private System.Windows.Forms.TreeView treeViewUserOrg;
        private System.Windows.Forms.ToolStripButton staffManager;
        private System.Windows.Forms.ToolStripButton departmentManager;
    }
}