using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormBaseDbBackupRecord : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";

        public FormBaseDbBackupRecord()
        {
            InitializeComponent();
        }

        private void FormBaseDbBackupRecord_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("备份方式", 100);
            m_dateGridViewExtend.addDataGridViewColumn("日期时间", 150);
            m_dateGridViewExtend.addDataGridViewColumn("备份人姓名", 120);
            m_dateGridViewExtend.addDataGridViewColumn("主机名", 100);
            m_dateGridViewExtend.addDataGridViewColumn("保存位置1", 300, false);
            m_dateGridViewExtend.addDataGridViewColumn("保存位置2", 300, false);
            m_dateGridViewExtend.addDataGridViewColumn("备份原因", 250);
            m_dateGridViewExtend.addDataGridViewColumn("备注", 150);
            m_dateGridViewExtend.addDataGridViewColumn("是否成功", 80);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewBilConfigList);
            updateDataGridView(DbBackupRecord.getInctance().getAllRecord());

            setPageActionEnable();
        }

        private void updateDataGridView(SortedDictionary<int, DbBackupRecordTable> backupRecordList)
        {
            m_dataGridRecordCount = backupRecordList.Count;

            SortedDictionary<int, ArrayList> backupRecordLArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < backupRecordList.Count; i++)
            {
                DbBackupRecordTable record = new DbBackupRecordTable();
                record = (DbBackupRecordTable)backupRecordList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);

                if (record.backType == 0)
                {
                    temp.Add("人为备份");
                }
                else
                {
                    temp.Add("自动备份");
                }

                temp.Add(record.backupTime);
                temp.Add(record.name);
                temp.Add(record.hostName);
                temp.Add(record.savePath1);
                temp.Add(record.savePath2);
                temp.Add(record.reason);
                temp.Add(record.note);
                temp.Add(record.state);

                backupRecordLArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(backupRecordLArrary);
        }

        private void backup_Click(object sender, EventArgs e)
        {
            DBBackup dbb = new DBBackup();
            dbb.ShowDialog();

            updateDataGridView(DbBackupRecord.getInctance().getAllRecord());
        }
        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_dataGridRecordCount > 0)
            {
                this.saveFileDialog1.Filter = "Excel 2007格式 (*.xlsx)|*.xlsx|Excel 2003格式 (*.xls)|*.xls";
                this.saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_dateGridViewExtend.dataGridViewExportToExecl(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据为空，无数据可导出!");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            m_dateGridViewExtend.printDataGridView();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewBilConfigList_Click(object sender, EventArgs e)
        {
            if (m_dataGridRecordCount > 0)
            {
                // 当单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewBilConfigList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewBilConfigList.ColumnCount; j++)
                    {
                        if (dataGridViewBilConfigList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewBilConfigList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewBilConfigList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordName = dataGridViewBilConfigList.Rows[i].Cells[2].Value.ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void setPageActionEnable()
        {
            this.backup.Enabled = AccessAuthorization.getInctance().isAccessAuthorization(112,
                    Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));
        }
    }
}
