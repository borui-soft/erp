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
    public partial class FormBaseOperatorLog : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";

        public FormBaseOperatorLog()
        {
            InitializeComponent();
        }

        private void FormBaseOperatorLog_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("操作者", 100);
            m_dateGridViewExtend.addDataGridViewColumn("操作时间", 150);
            m_dateGridViewExtend.addDataGridViewColumn("模块名称", 200);
            m_dateGridViewExtend.addDataGridViewColumn("内容描述", 400);
            m_dateGridViewExtend.addDataGridViewColumn("主机名", 100);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewBilConfigList);
            updateDataGridView(OperatorLog.getInctance().getAllRecord());
        }

        private void updateDataGridView(SortedDictionary<int, OperatorLogTable> backupRecordList)
        {
            m_dataGridRecordCount = backupRecordList.Count;

            SortedDictionary<int, ArrayList> backupRecordLArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < backupRecordList.Count; i++)
            {
                OperatorLogTable record = new OperatorLogTable();
                record = (OperatorLogTable)backupRecordList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.userName);
                temp.Add(record.operTime);
                temp.Add(record.moduleName);
                temp.Add(record.operDesc);
                temp.Add(record.hostName);

                backupRecordLArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(backupRecordLArrary);
        }

        private void backup_Click(object sender, EventArgs e)
        {
            OperatorLogSerach ols = new OperatorLogSerach();
            ols.ShowDialog();

            if (ols.getWhereSql().Length > 0)
            {
                updateDataGridView(OperatorLog.getInctance().getRecordFromQuerySql(ols.getWhereSql()));
            }
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
    }
}
