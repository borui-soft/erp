using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; 
using MainProgram.model;
using System.Net;
using MainProgram.bus;

namespace MainProgram
{
    public partial class DBBackup : Form
    {
        private bool m_isBackup;
        private string m_filePath = "";

        public DBBackup(bool isBackup = true)
        {
            InitializeComponent();
            m_isBackup = isBackup;
        }

        private void DBBackup_Load(object sender, EventArgs e)
        {
            m_filePath = System.IO.Directory.GetCurrentDirectory() + "\\Back\\";

            if (!Directory.Exists(m_filePath))
            {
                Directory.CreateDirectory(m_filePath);
            }

            if (m_isBackup)
            {
                this.Text = "数据库备份";
                this.labelPath.Text = "备份文件存放路径";
                this.labelReason.Text = "数据库备份原因";
                this.buttonSave.Text = "备份";

                this.textBoxFileDirectory.Text = m_filePath;
            }
            else
            {
                this.Text = "数据库恢复";
                this.labelPath.Text = "数据库恢复文件名";
                this.labelReason.Text = "数据库恢复原因";
                this.buttonSave.Text = "恢复";
            }
        }

        private void buttonSeleteDirectory_Click(object sender, EventArgs e)
        {
            if (m_isBackup)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = m_filePath; 

                if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.textBoxFileDirectory.Text = folderBrowserDialog.SelectedPath;
                }
            }
            else 
            {
                OpenFileDialog saveFileDialog = new OpenFileDialog();
                saveFileDialog.InitialDirectory = m_filePath;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxFileDirectory.Text = saveFileDialog.FileName;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.textBoxFileDirectory.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning(this.labelPath.Text + "不能为空，请填写");
                return;
            }

            if (this.textBoxReason.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning(this.labelReason.Text + "不能为空，请填写");
                return;
            }

            if (m_isBackup)
            {
                DbBackupRecordTable record = new DbBackupRecordTable();

                record.name = DbPublic.getInctance().getCurrentLoginUserName();
                record.hostName = Dns.GetHostName();
                record.backType = 0;
                record.reason = this.textBoxReason.Text;
                record.note = this.textBoxNode.Text;
                record.savePath1 = this.textBoxFileDirectory.Text;

                //DbBackupRecord.getInctance().insert(record);

                //string parameter = record.savePath1 + " #";

                //// 启动DBBackup.exe
                //Process.Start(@"DBBackup.exe", parameter);

                if (DbBackupRecord.getInctance().databaseBack(record.savePath1))
                {
                    DbBackupRecord.getInctance().insert(record);
                }
            }
            else
            {
                DbRollbackRecordTable record = new DbRollbackRecordTable();

                record.name = DbPublic.getInctance().getCurrentLoginUserName();
                record.hostName = Dns.GetHostName();
                record.reason = this.textBoxReason.Text;
                record.note = this.textBoxNode.Text;
                record.fileName = this.textBoxFileDirectory.Text;

                record.reason = this.textBoxReason.Text;
                record.note = this.textBoxNode.Text;

                //DbRollbackRecord.getInctance().insert(record);
                //string newRecordID = Convert.ToString(DbPublic.getInctance().getTableMaxPkey("BASE_DB_ROLLBACK_RECORD") + 1);
                //string parameter = record.fileName + " " + newRecordID;

                //// 启动DBBackup.exe
                //Process.Start(@"DBRollback.exe", parameter);

                if (DbRollbackRecord.getInctance().databaseRollback(record.fileName))
                {
                    DbRollbackRecord.getInctance().insert(record);
                }
            }

            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}