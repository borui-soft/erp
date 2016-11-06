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
    public partial class FormPurchaseInfoCount : Form
    {
        public enum CountType
        {
            Materiel,
            People,
            Supplier
        };

        private CountType m_orderType;
        private bool m_isCountAllBill = false;
        private int m_dataGridRecordCount = 0;
        private string m_countStartDate, m_countEndDate;

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();

        public FormPurchaseInfoCount(CountType orderType)
        {
            InitializeComponent();

            m_orderType = orderType;

            if (m_orderType == CountType.Materiel)
            {
                this.Text = "采购金额统计-按物料类型";
            }
            else if (m_orderType == CountType.People)
            {
                this.Text = "采购金额统计-按采购员名称";
            }
            else if (m_orderType == CountType.Supplier)
            {
                this.Text = "采购金额统计-按供应商名称";
            }

            // 默认查询得本月第一天开始到本月最后一天的数据
            DateTime nowDate = DateTime.Now;
            DateTime currentMonthFirstDay = new DateTime(nowDate.Year, nowDate.Month, 1);
            DateTime currentMonthLastDay = currentMonthFirstDay.AddMonths(1).AddDays(-1);

            m_countStartDate = currentMonthFirstDay.ToString("yyyyMMdd");
            m_countEndDate = currentMonthLastDay.ToString("yyyyMMdd");
        }

        private void FormPurchaseInfoCount_Load(object sender, EventArgs e)
        {
            if (m_orderType == CountType.Materiel)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("物料编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("采购金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.People)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("采购员ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("采购员名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("采购金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.Supplier)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("供应商ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("供应商名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("采购金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            double sumTotleMoney = 0;
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            int index = 0;

            if (m_orderType == CountType.Materiel)
            {
                SortedDictionary<int, PurchaseInOrderDetailsTable> list = new SortedDictionary<int, PurchaseInOrderDetailsTable>();

                list = PurchaseInOrderDetails.getInctance().getPurchaseInOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill);

                m_dataGridRecordCount = list.Count;
                sumTotleMoney = getSumTotleMoney(list);

                for (index = 0; index < list.Count; index++)
                {
                    PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();
                    record = (PurchaseInOrderDetailsTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(index + 1);
                    temp.Add(record.materielID);
                    temp.Add(record.materielName);
                    temp.Add(record.materielModel);
                    temp.Add(record.totalMoney);
                    temp.Add(getPercentValue(record.totalMoney, sumTotleMoney));

                    sortedDictionaryList.Add(index, temp);
                }
            }
            else if (m_orderType == CountType.People)
            {
                SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

                list = PurchaseInOrder.getInctance().getPurchaseInOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill, 2);

                m_dataGridRecordCount = list.Count;
                sumTotleMoney = getSumTotleMoney(list);

                for (index = 0; index < list.Count; index++)
                {
                    PurchaseInOrderTable record = new PurchaseInOrderTable();
                    record = (PurchaseInOrderTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(index + 1);
                    temp.Add(record.businessPeopleId);
                    temp.Add(record.businessPeopleName);
                    temp.Add(record.totalMoney);
                    temp.Add(getPercentValue(Convert.ToDouble(record.totalMoney.ToString()), sumTotleMoney));

                    sortedDictionaryList.Add(index, temp);
                }
            }
            else if (m_orderType == CountType.Supplier)
            {
                SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

                list = PurchaseInOrder.getInctance().getPurchaseInOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill, 1);

                m_dataGridRecordCount = list.Count;
                sumTotleMoney = getSumTotleMoney(list);

                for (index = 0; index < list.Count; index++)
                {
                    PurchaseInOrderTable record = new PurchaseInOrderTable();
                    record = (PurchaseInOrderTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(index + 1);
                    temp.Add(record.supplierId);
                    temp.Add(record.supplierName);
                    temp.Add(record.totalMoney);
                    temp.Add(getPercentValue(Convert.ToDouble(record.totalMoney.ToString()), sumTotleMoney));
                    
                    sortedDictionaryList.Add(index, temp);
                }
            }

            // 添加合计行
            ArrayList sumRow = new ArrayList();
            sumRow.Add(index + 1);
            sumRow.Add("合计");
            sumRow.Add("");

            if (m_orderType == CountType.Materiel)
            {
                sumRow.Add("");
            }

            sumRow.Add(sumTotleMoney);

            sumRow.Add("100%");
            sortedDictionaryList.Add(index, sumRow);

            m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList);
            dataGridViewList.Rows[index].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;


            // 更新状态栏信息
            updateStatusLable();
        }

        private void updateStatusLable()
        {
            string countInfoToStatusLable = "当前数据过滤条件：  ";
            string startDate = "开始日期: ";
            string endDate = "结束日期: ";
            string isReview = "是否审核: ";

            startDate += m_countStartDate.Substring(0, 4) + "年";
            startDate += m_countStartDate.Substring(4, 2) + "月";
            startDate += m_countStartDate.Substring(6) + "日     ";

            endDate += m_countEndDate.Substring(0, 4) + "年";
            endDate += m_countEndDate.Substring(4, 2) + "月";
            endDate += m_countEndDate.Substring(6) + "日     ";


            if (m_isCountAllBill)
            {
                isReview += "全部单据";
            }
            else
            {
                isReview += "已审核单据";
            }

            countInfoToStatusLable += startDate;
            countInfoToStatusLable += endDate;
            countInfoToStatusLable += isReview;

            this.toolStripStatusLabelCountInfo.Text = countInfoToStatusLable;
        }

        private double getSumTotleMoney(SortedDictionary<int, PurchaseInOrderDetailsTable> list)
        {
            double sumTotleMoney = 0.0;

            for (int index = 0; index < list.Count; index++)
            {
                PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();
                record = (PurchaseInOrderDetailsTable)list[index];
                sumTotleMoney += record.totalMoney;
            }
            return sumTotleMoney;
        }

        private double getSumTotleMoney(SortedDictionary<int, PurchaseInOrderTable> list)
        {
            double sumTotleMoney = 0.0;

            for (int index = 0; index < list.Count; index++)
            {
                PurchaseInOrderTable record = new PurchaseInOrderTable();
                record = (PurchaseInOrderTable)list[index];
                sumTotleMoney += Convert.ToDouble(record.totalMoney.ToString());
            }

            return sumTotleMoney;
        }

        private string getPercentValue(double value, double sumValue)
        {
            double Percent = 0 ;

            // 保留2位小数儿
            Percent = value / sumValue;
            Percent = (double)(Math.Round(Percent * 100));

            return Convert.ToString(Percent) + "%";
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

        #region Panle控件鼠标滑过事件
        private void billDetail_MouseEnter(object sender, EventArgs e)
        {
            this.billDetail.CheckState = CheckState.Checked;
        }

        private void billDetail_MouseLeave(object sender, EventArgs e)
        {
            this.billDetail.CheckState = CheckState.Unchecked;
        }

        private void panelExport_MouseEnter(object sender, EventArgs e)
        {
            this.export.CheckState = CheckState.Checked;
        }

        private void panelExport_MouseLeave(object sender, EventArgs e)
        {
            this.export.CheckState = CheckState.Unchecked;
        }

        private void panelPrintDisplay_MouseEnter(object sender, EventArgs e)
        {
            this.printDisplay.CheckState = CheckState.Checked;
        }

        private void panelPrintDisplay_MouseLeave(object sender, EventArgs e)
        {
            this.printDisplay.CheckState = CheckState.Unchecked;
        }

        private void panelPrint_MouseEnter(object sender, EventArgs e)
        {
            this.print.CheckState = CheckState.Checked;
        }

        private void panelPrint_MouseLeave(object sender, EventArgs e)
        {
            this.print.CheckState = CheckState.Unchecked;
        }

        private void panelImageExit_MouseEnter(object sender, EventArgs e)
        {
            this.close.CheckState = CheckState.Checked;
        }

        private void panelImageExit_MouseLeave(object sender, EventArgs e)
        {
            this.close.CheckState = CheckState.Unchecked;
        }

        #endregion
    }
}