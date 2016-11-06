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
    public partial class FormCompanyProfit : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();

        public FormCompanyProfit()
        {
            InitializeComponent();
        }

        private void FormCompanyProfit_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("会计期间", 100);
            m_dateGridViewExtend.addDataGridViewColumn("结转日期", 100);
            m_dateGridViewExtend.addDataGridViewColumn("数据来源", 100);
            m_dateGridViewExtend.addDataGridViewColumn("库存现金", 100);
            m_dateGridViewExtend.addDataGridViewColumn("银行存款", 100);
            m_dateGridViewExtend.addDataGridViewColumn("库存货物金额", 120);
            m_dateGridViewExtend.addDataGridViewColumn("应收账款合计", 120);
            m_dateGridViewExtend.addDataGridViewColumn("应付账款合计", 120);
            m_dateGridViewExtend.addDataGridViewColumn("所有者权益", 120);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            SortedDictionary<int, CompanyProfitTable> dateList = new SortedDictionary<int, CompanyProfitTable>();
            SortedDictionary<int, ArrayList> companyProfitDataArrary = new SortedDictionary<int, ArrayList>();
            dateList = CompanyProfit.getInctance().getAllCompanyProfitData();

            m_dataGridRecordCount = dateList.Count;

            for (int recordCount = 0; recordCount < dateList.Count; recordCount++)
            {
                CompanyProfitTable record = new CompanyProfitTable();
                record = (CompanyProfitTable)dateList[recordCount];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.date.Substring(0, 4) + "-" + record.date.Substring(5, 2));
                temp.Add(record.date);
                temp.Add(record.dataSource);
                temp.Add(record.cashBalance);
                temp.Add(record.bankBalance);
                temp.Add(record.sumStorageStock);
                temp.Add(record.sumReceivable);
                temp.Add(record.sumPayable);

                temp.Add(record.cashBalance + record.bankBalance + record.sumStorageStock + record.sumReceivable - record.sumPayable);

                companyProfitDataArrary.Add(recordCount, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(companyProfitDataArrary, 2);
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

        private void dataGridViewList_Click(object sender, EventArgs e)
        {
            if (m_dataGridRecordCount > 0)
            {
                // 当单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewList.ColumnCount; j++)
                    {
                        if (dataGridViewList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewList.Rows[i].Selected = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}