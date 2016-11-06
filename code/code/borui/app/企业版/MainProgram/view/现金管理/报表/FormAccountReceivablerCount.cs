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
    public partial class FormAccountReceivablerCount : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";
        private bool m_isAccountReceivable;

        public FormAccountReceivablerCount(bool isAccountReceivable = true)
        {
            InitializeComponent();
            m_isAccountReceivable = isAccountReceivable;

            if (m_isAccountReceivable)
            {
                this.Text = "应收账款汇总";
            }
            else
            {
                this.Text = "应付账款汇总";
            }
        }

        private void FormAccountReceivablerCount_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("客户名称", 250);
            m_dateGridViewExtend.addDataGridViewColumn("欠款金额", 150);
            m_dateGridViewExtend.addDataGridViewColumn("交易日期", 150);
            m_dateGridViewExtend.addDataGridViewColumn("信用额度", 150);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            SortedDictionary<int, InitAccountReceivableTable> dateList = new SortedDictionary<int, InitAccountReceivableTable>();

            if (m_isAccountReceivable)
            {
                dateList = InitAccountReceivable.getInctance().getAllInitAccountReceivableInfo();
            }
            else
            {
                dateList = InitAccountPayable.getInctance().getAllInitAccountPayableInfo();
            }

            m_dataGridRecordCount = dateList.Count;

            SortedDictionary<int, ArrayList> settlmentWayLArrary = new SortedDictionary<int, ArrayList>();

            double balanceSum = 0.0, creditSum = 0.0;
            int recordCount = 0;
            for (recordCount = 0; recordCount < dateList.Count; recordCount++)
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();
                record = (InitAccountReceivableTable)dateList[recordCount];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.name);
                temp.Add(record.balance);
                temp.Add(record.tradingDate);
                temp.Add(record.credit);

                settlmentWayLArrary.Add(recordCount, temp);

                // 应收或应付账款金额合计
                balanceSum += record.balance;
                creditSum += record.credit;
            }

            // 需要再DataGridView的最后一行添加合计欠款金额一行
            InitAccountReceivableTable sumBalanceRecord = new InitAccountReceivableTable();
            sumBalanceRecord.pkey = 0;
            sumBalanceRecord.name = "合计";
            sumBalanceRecord.balance = balanceSum;
            sumBalanceRecord.tradingDate = "";
            sumBalanceRecord.credit = creditSum;

            ArrayList sumRecord = new ArrayList();

            sumRecord.Add(sumBalanceRecord.pkey);
            sumRecord.Add(sumBalanceRecord.name);
            sumRecord.Add(sumBalanceRecord.balance);
            sumRecord.Add(sumBalanceRecord.tradingDate);
            sumRecord.Add(sumBalanceRecord.credit);

            settlmentWayLArrary.Add(recordCount, sumRecord);

            m_dateGridViewExtend.initDataGridViewData(settlmentWayLArrary);

            // 设置DataGridView 合计行背景颜色
            dataGridViewList.Rows[dateList.Count].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
        }

        private void billDetail_Click(object sender, EventArgs e)
        {
            checkAccountBillDetaile();
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
            try
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
                                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewList.Rows[i].Cells[0].Value.ToString());
                                m_currentDataGridViedRecordName = dataGridViewList.Rows[i].Cells[1].Value.ToString();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
 
            }
        }

        private void dataGridViewMaterielList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex != -1 &&
            //    e.RowIndex < dataGridViewMaterielList.RowCount - 1 &&
            //    dataGridViewMaterielList.Rows[e.RowIndex].Cells[1].Value.ToString() == "合计")
            //{
            //    e.CellStyle.Font = new Font(dataGridViewMaterielList.DefaultCellStyle.Font, FontStyle.Bold);
            //    e.CellStyle.WrapMode = DataGridViewTriState.True;
            //    DataGridViewCellMerge.MerageRowSpan(dataGridViewMaterielList, e, 0, 1);
            //}
        }

        private void dataGridViewMaterielList_DoubleClick(object sender, EventArgs e)
        {
            try
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
                                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewList.Rows[i].Cells[0].Value.ToString());
                                m_currentDataGridViedRecordName = dataGridViewList.Rows[i].Cells[1].Value.ToString();
                                checkAccountBillDetaile();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void checkAccountBillDetaile()
        {
            // checkAccountBillDetaile函数需要完成弹出一个新的窗口，用来显示公司跟该企业的详细的往来账

            if (m_currentDataGridViedRecordPkey != 0)
            {
                // 根据m_currentDataGridViedRecordPkey得到对应往来单位的pkey值
                int exchangesUnitPkey;
                string winText = ""; ;

                if (m_isAccountReceivable)
                {
                    exchangesUnitPkey = InitAccountReceivable.getInctance().getCustomerOrSupplierIDFromPkey(m_currentDataGridViedRecordPkey);
                    winText = "应收账款 - [" + m_currentDataGridViedRecordName + "]";
                    
                }
                else
                {
                    exchangesUnitPkey = InitAccountPayable.getInctance().getCustomerOrSupplierIDFromPkey(m_currentDataGridViedRecordPkey);
                    winText = "应付账款 - [" + m_currentDataGridViedRecordName + "]";
                }

                if (exchangesUnitPkey != -1)
                {
                    FormAccountReceivablerDetail fard = new FormAccountReceivablerDetail(winText, m_isAccountReceivable, exchangesUnitPkey);
                    fard.ShowDialog();
                }
                else 
                {
                    MessageBoxExtend.messageWarning("该企业不存在.");
                }
            }

        }
    }
}
