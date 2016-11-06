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
    public partial class FormSaleInfoCount : Form
    {
        public enum OrderType
        {
            // 存货核算-销售成本汇总表
            SaleCostCount,

            // 存货核算-销售成本明细
            SaleCostDetails,

            // 存货核算-销售毛利润汇总表
            SaleProfitCount,

            // 存货核算-销售毛利润明细表
            SaleProfitDetails
        };

        private OrderType m_orderType;
        private bool m_isCountAllBill = false;
        private int m_dataGridRecordCount = 0;
        private string m_countStartDate, m_countEndDate;
        private int m_materielID = -1;

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();

        public FormSaleInfoCount(OrderType orderType, int materielID = -1)
        {
            InitializeComponent();

            m_orderType = orderType;
            m_materielID = materielID;

            if (m_orderType == OrderType.SaleCostCount)
            {
                this.Text = "销售成本汇总表";
            }
            else if (m_orderType == OrderType.SaleCostDetails)
            {
                this.Text = "销售成本明细表";
            }
            else if (m_orderType == OrderType.SaleProfitCount)
            {
                this.Text = "销售毛利润汇总表";
            }
            else if (m_orderType == OrderType.SaleProfitDetails)
            {
                this.Text = "销售毛利润明细表";
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
            if (m_orderType == OrderType.SaleCostCount)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 90);
                m_dateGridViewExtend.addDataGridViewColumn("产品ID", 90);
                m_dateGridViewExtend.addDataGridViewColumn("名称", 160);
                m_dateGridViewExtend.addDataGridViewColumn("销售单位", 80);

                m_dateGridViewExtend.addDataGridViewColumn("总销售数量", 120);
                m_dateGridViewExtend.addDataGridViewColumn("总成本", 120);
            }
            else if (m_orderType == OrderType.SaleCostDetails)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 90);
                m_dateGridViewExtend.addDataGridViewColumn("产品ID", 90);
                m_dateGridViewExtend.addDataGridViewColumn("名称", 160);
                m_dateGridViewExtend.addDataGridViewColumn("销售单位", 80);

                m_dateGridViewExtend.addDataGridViewColumn("成本价", 120);
                m_dateGridViewExtend.addDataGridViewColumn("销售数量", 120);
                m_dateGridViewExtend.addDataGridViewColumn("总成本", 120);
            }
            else if (m_orderType == OrderType.SaleProfitCount)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 90);
                m_dateGridViewExtend.addDataGridViewColumn("产品ID", 90);
                m_dateGridViewExtend.addDataGridViewColumn("名称", 160);
                m_dateGridViewExtend.addDataGridViewColumn("销售单位", 80);

                m_dateGridViewExtend.addDataGridViewColumn("总销售数量", 120);
                m_dateGridViewExtend.addDataGridViewColumn("总成本", 120);
                m_dateGridViewExtend.addDataGridViewColumn("总销售额", 120);
                m_dateGridViewExtend.addDataGridViewColumn("毛利润", 120);
            }
            else if (m_orderType == OrderType.SaleProfitDetails)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 70);
                m_dateGridViewExtend.addDataGridViewColumn("产品ID", 70);
                m_dateGridViewExtend.addDataGridViewColumn("名称", 150);
                m_dateGridViewExtend.addDataGridViewColumn("销售单位", 80);

                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120, false);
                m_dateGridViewExtend.addDataGridViewColumn("成本价", 90);
                m_dateGridViewExtend.addDataGridViewColumn("销售价", 90);
                m_dateGridViewExtend.addDataGridViewColumn("销售数量", 90);
                m_dateGridViewExtend.addDataGridViewColumn("总成本", 90);
                m_dateGridViewExtend.addDataGridViewColumn("总销售额", 90);
                m_dateGridViewExtend.addDataGridViewColumn("毛利润", 90);
            }
            else
            {
                MessageBoxExtend.messageWarning("暂时不支持的序时薄类型");
            }

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            // 合计行
            ArrayList sumRow = new ArrayList();
            double sumTotleValue = 0, sumTotleMoney = 0, sumCost = 0, sumSale = 0;

            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            if (m_orderType == OrderType.SaleCostCount)
            {
                sortedDictionaryList = getSaleCostCountData(out sumTotleValue, out sumTotleMoney);

                sumRow.Add(sortedDictionaryList.Count + 1);
                sumRow.Add("合计");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add(sumTotleValue);
                sumRow.Add(sumTotleMoney);
                sortedDictionaryList.Add(sortedDictionaryList.Count, sumRow);
            }
            else if (m_orderType == OrderType.SaleCostDetails)
            {
                sortedDictionaryList = getSaleCostDetailsData(out sumTotleValue, out sumTotleMoney); 

                sumRow.Add(sortedDictionaryList.Count + 1);
                sumRow.Add("合计");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add(sumTotleValue);
                sumRow.Add(sumTotleMoney);
                sortedDictionaryList.Add(sortedDictionaryList.Count, sumRow);
            }
            else if (m_orderType == OrderType.SaleProfitCount)
            {
                sortedDictionaryList = getSaleProfitCountData(out sumTotleValue, out sumCost, out sumSale, out sumTotleMoney);

                sumRow.Add(sortedDictionaryList.Count + 1);
                sumRow.Add("合计");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add(sumTotleValue);
                sumRow.Add(sumCost);
                sumRow.Add(sumSale);
                sumRow.Add(sumTotleMoney);
                sortedDictionaryList.Add(sortedDictionaryList.Count, sumRow);
            }
            else if (m_orderType == OrderType.SaleProfitDetails)
            {
                sortedDictionaryList = getSaleProfitDetailsData(out sumTotleValue, out sumCost, out sumSale, out sumTotleMoney);

                sumRow.Add(sortedDictionaryList.Count + 1);
                sumRow.Add("合计");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add("");
                sumRow.Add(sumTotleValue);
                sumRow.Add(sumCost);
                sumRow.Add(sumSale);
                sumRow.Add(sumTotleMoney);
                sortedDictionaryList.Add(sortedDictionaryList.Count, sumRow);
            }

            m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList);
            dataGridViewList.Rows[sortedDictionaryList.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;


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

        private string getPercentValue(double value, double sumValue)
        {
            double Percent = 0 ;

            // 保留2位小数儿
            Percent = value / sumValue;
            Percent = (double)(Math.Round(Percent * 100, 2));

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

        private void billDetail_Click(object sender, EventArgs e)
        {
            FormPurchaseInfoCountFilter fpicf = new FormPurchaseInfoCountFilter();
            fpicf.ShowDialog();

            m_countStartDate = fpicf.getFilterStartDate();
            m_countEndDate = fpicf.getFilterEndDate();

            if (m_countStartDate != null && m_countEndDate != null)
            {
                string isReviewSign = fpicf.getIsReviewSign();

                if (isReviewSign == "0")
                {
                    // isReviewSign等于0，代表只查询已审核单据
                    m_isCountAllBill = false;
                }
                else
                {
                    m_isCountAllBill = true;
                }

                updateDataGridView();
            }
        }

        private void dataGridViewList_DoubleClick(object sender, EventArgs e)
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

                                // 当用户查询的是销售毛利润明细信息时，双击记录关联到对应单据
                                if (m_orderType == OrderType.SaleProfitDetails)
                                {
                                    string billNumber = dataGridViewList.Rows[i].Cells[4].Value.ToString();
                                    if (billNumber.Length > 0)
                                    {
                                        FormSaleOutOrder fpo = new FormSaleOutOrder(billNumber);
                                        fpo.ShowDialog();
                                    }
                                }

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

        private SortedDictionary<int, ArrayList> getSaleCostCountData(out double sumTotleValue, out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleProfitCounInfoStruct> list = new SortedDictionary<int, SaleProfitCounInfoStruct>();

            list = SaleOutOrderDetails.getInctance().getSaleOutOrderCountInfo2(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;
            sumTotleValue = 0;
            sumTotleMoney = 0;

            for (int index = 0; index < list.Count; index++)
            {
                SaleProfitCounInfoStruct record = new SaleProfitCounInfoStruct();
                record = (SaleProfitCounInfoStruct)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielUnitSale);
                temp.Add(record.valueSum);
                temp.Add(record.costSum);

                sumTotleValue += record.valueSum;
                sumTotleMoney += record.costSum;

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSaleCostDetailsData(out double sumTotleValue, out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleProfitCounInfoStruct> list = new SortedDictionary<int, SaleProfitCounInfoStruct>();

            list = SaleOutOrderDetails.getInctance().getSaleOutOrderDetailsInfo(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;
            sumTotleValue = 0;
            sumTotleMoney = 0;

            for (int index = 0; index < list.Count; index++)
            {
                SaleProfitCounInfoStruct record = new SaleProfitCounInfoStruct();
                record = (SaleProfitCounInfoStruct)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielUnitSale);
                temp.Add(record.priceCost);
                temp.Add(record.value);
                temp.Add(record.costSum);

                sumTotleValue += record.valueSum;
                sumTotleMoney += record.costSum;

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSaleProfitCountData(out double sumTotleValue, out double sumCost, out double sumSale, out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleProfitCounInfoStruct> list = new SortedDictionary<int, SaleProfitCounInfoStruct>();

            list = SaleOutOrderDetails.getInctance().getSaleOutOrderCountInfo2(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;
            sumTotleValue = 0;
            sumCost = 0;
            sumSale = 0;
            sumTotleMoney = 0;

            for (int index = 0; index < list.Count; index++)
            {
                SaleProfitCounInfoStruct record = new SaleProfitCounInfoStruct();
                record = (SaleProfitCounInfoStruct)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielUnitSale);
                temp.Add(record.valueSum);
                temp.Add(record.costSum);
                temp.Add(record.saleSum);
                temp.Add(record.profitSum);

                sumTotleValue += record.valueSum;
                sumCost += record.costSum;
                sumSale += record.saleSum;
                sumTotleMoney += record.profitSum;

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSaleProfitDetailsData(out double sumTotleValue, out double sumCost, out double sumSale, out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleProfitCounInfoStruct> list = new SortedDictionary<int, SaleProfitCounInfoStruct>();

            list = SaleOutOrderDetails.getInctance().getSaleOutOrderDetailsInfo(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;

            sumTotleValue = 0;
            sumCost = 0;
            sumSale = 0;
            sumTotleMoney = 0;

            for (int index = 0; index < list.Count; index++)
            {
                SaleProfitCounInfoStruct record = new SaleProfitCounInfoStruct();
                record = (SaleProfitCounInfoStruct)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielUnitSale);
                temp.Add(record.billNumber);
                temp.Add(record.priceCost);
                temp.Add(record.priceSale);
                temp.Add(record.value);
                temp.Add(record.costSum);
                temp.Add(record.saleSum);
                temp.Add(record.profitSum);

                sumTotleValue += record.value;
                sumCost += record.costSum;
                sumSale += record.saleSum;
                sumTotleMoney += record.profitSum;

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }
    }
}