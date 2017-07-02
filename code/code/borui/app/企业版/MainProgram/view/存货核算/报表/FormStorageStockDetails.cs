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
    public partial class FormStorageStockDetails : Form
    {
        private int m_currentRecordIndex = 0;
        private int m_dataGridRecordCount = 0;
        private string m_countStartDate, m_countEndDate;
        private string m_billNumber = "", m_billType = "";

        private BillDataGridViewExtend m_dateGridViewExtend = new BillDataGridViewExtend();
        private SortedDictionary<int, MaterielTable> m_materielList = new SortedDictionary<int, MaterielTable>();
        private PurchaseInOrderTable m_purchaseInOrder = new PurchaseInOrderTable();

        private bool m_isDisplayJG = false;
        private int m_moduleID = 403;

        public FormStorageStockDetails()
        {
            InitializeComponent();

            // 默认查询得本月第一天开始到本月最后一天的数据
            DateTime nowDate = DateTime.Now;
            DateTime currentMonthFirstDay = new DateTime(nowDate.Year, nowDate.Month, 1);
            DateTime currentMonthLastDay = currentMonthFirstDay.AddMonths(1).AddDays(-1);

            m_countStartDate = currentMonthFirstDay.ToString("yyyyMMdd");
            m_countEndDate = currentMonthLastDay.ToString("yyyyMMdd");

            m_materielList = Materiel.getInctance().getAllMaterielInfo();

            if (m_materielList.Count > 0)
            {
                m_currentRecordIndex = 0;
            }

            // 判断下是否有查看单价的权限
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(m_moduleID);

            foreach (KeyValuePair<int, ActionTable> index in list)
            {
                if (index.Value.uiActionName == "dispaly")
                {
                    m_isDisplayJG = AccessAuthorization.getInctance().isAccessAuthorization(index.Value.pkey,
                                 Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));
                }
            }

        }

        private void FormPurchaseInfoCount_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("会计期间", 80);
            m_dateGridViewExtend.addDataGridViewColumn("日期", 80);
            m_dateGridViewExtend.addDataGridViewColumn("单据号", 130);
            m_dateGridViewExtend.addDataGridViewColumn("事务类型", 80);

            m_dateGridViewExtend.addDataGridViewColumn("数量\n(入库)", 65);
            if (m_isDisplayJG)
            {
                m_dateGridViewExtend.addDataGridViewColumn("单价\n(入库)", 65);
                m_dateGridViewExtend.addDataGridViewColumn("金额\n(入库)", 65);
            }
            m_dateGridViewExtend.addDataGridViewColumn("供应商\n(入库)", 100);


            m_dateGridViewExtend.addDataGridViewColumn("数量\n(出库)", 65);
            if (m_isDisplayJG)
            {
                m_dateGridViewExtend.addDataGridViewColumn("单价\n(出库)", 65);
                m_dateGridViewExtend.addDataGridViewColumn("金额\n(出库)", 65);
            }
            m_dateGridViewExtend.addDataGridViewColumn("项目编号\n(出库)", 65);
            m_dateGridViewExtend.addDataGridViewColumn("生成编号\n(出库)", 65);


            m_dateGridViewExtend.addDataGridViewColumn("结存数量", 65);
            if (m_isDisplayJG)
            {
                m_dateGridViewExtend.addDataGridViewColumn("结存单价", 65);
                m_dateGridViewExtend.addDataGridViewColumn("结存金额", 65);
            }

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);

            updateDataGridView();
        }

        private void updateDataGridView(int materielID = -1)
        {
            if (materielID == -1)
            {
                this.upRecord.Enabled = true;
                this.nextRecord.Enabled = true;

                if (m_currentRecordIndex < 0)
                {
                    MessageBoxExtend.messageOK("已是首条记录");
                    return;
                }
                else if (m_currentRecordIndex >= m_materielList.Count)
                {
                    MessageBoxExtend.messageOK("已是尾条记录");
                    return;
                }
                else
                {
                    materielID = ((MaterielTable)m_materielList[m_currentRecordIndex]).pkey;
                }
            }
            else 
            {
                this.upRecord.Enabled = false;
                this.nextRecord.Enabled = false;
            }

            // 期初余额
            ArrayList firstRow = new ArrayList();
            ArrayList sumRow = new ArrayList();
            ArrayList lastRow = new ArrayList();

            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, StorageStockDetailTable> list = new SortedDictionary<int, StorageStockDetailTable>();

            #region 期初余额记录行
            StorageStockDetailTable firstRecord = StorageStockDetail.getInctance().getMaterielStorageStockDetailInfo(materielID, m_countStartDate);

            firstRow.Add(formatStringToMonth(m_countStartDate, 4));
            firstRow.Add("");
            firstRow.Add("");
            firstRow.Add("期初结存");
            firstRow.Add("");

            if (m_isDisplayJG)
            {
                firstRow.Add("");
                firstRow.Add("");
            }
            firstRow.Add("");
            firstRow.Add("");

            if (m_isDisplayJG)
            {
                firstRow.Add("");
                firstRow.Add("");
            }

            firstRow.Add("");
            firstRow.Add("");

            if (firstRecord != null)
            {
                firstRow.Add(firstRecord.storageValue);

                if (m_isDisplayJG)
                {
                    firstRow.Add(firstRecord.storagePrice);
                    firstRow.Add((double)(Math.Round(firstRecord.storageMoney * 100)) / 100);
                }
            }
            else
            {
                firstRow.Add("0");

                if (m_isDisplayJG)
                {
                    firstRow.Add("0");
                    firstRow.Add("0");
                }
            }

            sortedDictionaryList.Add(sortedDictionaryList.Count, firstRow);
            #endregion

            #region 当前时间段交易记录
            list = StorageStockDetail.getInctance().getMaterielStorageStockDetailInfo(materielID, m_countStartDate, m_countEndDate);

            m_dataGridRecordCount = list.Count + 3;

            double inSumValue = 0, inSumMoney = 0, outSumValue = 0, outSumMoney = 0, stockSumValue = 0, stockSumPrice = 0, stockSumMoney = 0;

            for (int index = 0; index < list.Count; index++)
            {
                StorageStockDetailTable record = new StorageStockDetailTable();
                record = (StorageStockDetailTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(formatStringToMonth(record.tradingDate, 5));
                temp.Add(record.tradingDate);
                temp.Add(record.billNumber);
                temp.Add(record.thingsType);

                if (record.isIn == 0)
                {
                    // 出库类单据
                    temp.Add("");
                    if (m_isDisplayJG)
                    {
                        temp.Add("");
                        temp.Add("");
                    }
                    temp.Add("");

                    temp.Add(record.value);

                    if (m_isDisplayJG)
                    {
                        temp.Add(record.price);
                        temp.Add(record.value * record.price);
                    }

                    // 未完成：这里的信息需要显示项目编号和生产编号，如何添加
                    string projectNo = "", makeNo = "";
                    if (record.thingsType == "生产领料")
                    {
                        MaterielOutOrderTable materieOutOrder = MaterielOutOrder.getInctance().getMaterielOutOrderInfoFromBillNumber(record.billNumber);
                        projectNo = materieOutOrder.srcOrderNum;
                        makeNo = materieOutOrder.makeNo;
                    }
                    else if (record.thingsType == "其他出库")
                    {
                        MaterielOutOtherOrderTable materieOutOrder = MaterielOutOtherOrder.getInctance().getMaterielOutOtherOrderInfoFromBillNumber(record.billNumber);
                        projectNo = materieOutOrder.projectNo;
                        makeNo = materieOutOrder.makeNo;
                    }

                    temp.Add(projectNo);
                    temp.Add(makeNo);

                    temp.Add(record.storageValue);

                    if (m_isDisplayJG)
                    {
                        temp.Add(record.storagePrice);
                        temp.Add((double)(Math.Round(record.storageMoney * 100)) / 100);
                    }

                    outSumValue += record.value;
                    outSumMoney += record.value * record.price;
                }
                else if (record.isIn == 1)
                {
                    // 入库类单据
                    temp.Add(record.value);

                    if (m_isDisplayJG)
                    {
                        temp.Add(record.price);
                        temp.Add(record.value * record.price);
                    }

                    if (record.thingsType == "采购入库")
                    {
                        m_purchaseInOrder = PurchaseInOrder.getInctance().getPurchaseInfoFromBillNumber(record.billNumber);
                        temp.Add(m_purchaseInOrder.supplierName);
                    }
                    else
                    {
                        temp.Add("");
                    }

                    temp.Add("");

                    if (m_isDisplayJG)
                    {
                        temp.Add("");
                        temp.Add("");
                    }

                    temp.Add("");
                    temp.Add("");

                    temp.Add(record.storageValue);

                    if (m_isDisplayJG)
                    {
                        temp.Add(record.storagePrice);
                        temp.Add((double)(Math.Round(record.storageMoney * 100)) / 100);
                    }

                    inSumValue += record.value;
                    inSumMoney += record.value * record.price;
                }
                else
                {
                    // 其他类型单据
                    temp.Add("");
                    if (m_isDisplayJG)
                    {
                        temp.Add("");
                        temp.Add("");
                    }
                    temp.Add("");
                    temp.Add("");

                    if (m_isDisplayJG)
                    {
                        temp.Add("");
                        temp.Add("");
                    }

                    temp.Add("");
                    temp.Add("");
                    temp.Add(record.storageValue);

                    if (m_isDisplayJG)
                    {
                        temp.Add(record.storagePrice);
                        temp.Add((double)(Math.Round(record.storageMoney * 100)) / 100);
                    }
                }
                if(index == list.Count - 1)
                {
                    stockSumValue = record.storageValue;
                    stockSumPrice = record.storagePrice;
                    stockSumMoney += record.storageMoney;
                }

                sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
            }
            #endregion

            #region 合计
            sumRow.Add("");
            sumRow.Add("");
            sumRow.Add("");
            sumRow.Add("合计");

            sumRow.Add(inSumValue);

            if (m_isDisplayJG)
            {
                sumRow.Add(getPercentValue(inSumMoney, inSumValue));
                sumRow.Add(inSumMoney);
            }

            sumRow.Add("");

            sumRow.Add(outSumValue);
            if (m_isDisplayJG)
            {
                sumRow.Add(getPercentValue(outSumMoney, outSumValue));
                sumRow.Add(outSumMoney);
            }
            sumRow.Add("");
            sumRow.Add("");
            sumRow.Add("");

            if (m_isDisplayJG)
            {
                sumRow.Add("");
                sumRow.Add("");
            }

            sortedDictionaryList.Add(sortedDictionaryList.Count, sumRow);
            #endregion

            #region 期末结转
            lastRow.Add(formatStringToMonth(m_countEndDate, 4));
            lastRow.Add("");
            lastRow.Add("");
            lastRow.Add("期末结存");


            lastRow.Add("");

            if (m_isDisplayJG)
            {
                lastRow.Add("");
                lastRow.Add("");
            }
            lastRow.Add("");
            lastRow.Add("");

            if (m_isDisplayJG)
            {
                lastRow.Add("");
                lastRow.Add("");
            }

            lastRow.Add("");
            lastRow.Add("");


            lastRow.Add(stockSumValue);

            if (m_isDisplayJG)
            {
                lastRow.Add(stockSumPrice);
                lastRow.Add((double)(Math.Round(stockSumMoney * 100)) / 100);
            }

            sortedDictionaryList.Add(sortedDictionaryList.Count, lastRow);
            #endregion

            // 设置m_dateGridViewExtend背景为白色
            m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList);

            for (int i = 0; i < sortedDictionaryList.Count; i++)
            {
                dataGridViewList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }

            dataGridViewList.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewList.Rows[sortedDictionaryList.Count - 2].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewList.Rows[sortedDictionaryList.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;

            // 更新状态栏信息
            updateStatusLable(materielID);
        }

        private string formatStringToMonth(string date, int monthStartPos)
        {
            string str = "";

            if (date.Length > 0)
            {
                str = date.Substring(0, 4) + "." + date.Substring(monthStartPos, 2);
            }

            return str;
        }

        private void updateStatusLable(int ID)
        {
            MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(ID);

            string countInfoToStatusLable = "当前数据过滤条件：  ";
            string materielName = "物料名称：" + record.name;

            string startDate = "     开始日期: ";
            string endDate = "     结束日期: ";
            string isReview = "     是否审核: ";

            startDate += m_countStartDate.Substring(0, 4) + "年";
            startDate += m_countStartDate.Substring(4, 2) + "月";
            startDate += m_countStartDate.Substring(6) + "日";

            endDate += m_countEndDate.Substring(0, 4) + "年";
            endDate += m_countEndDate.Substring(4, 2) + "月";
            endDate += m_countEndDate.Substring(6) + "日";

            isReview += "已审核单据";
            countInfoToStatusLable += materielName;
            countInfoToStatusLable += startDate;
            countInfoToStatusLable += endDate;
            countInfoToStatusLable += isReview;

            this.toolStripStatusLabelCountInfo.Text = countInfoToStatusLable;
        }

        private string getPercentValue(double value, double sumValue)
        {
            double Percent = 0 ;

            // 保留3位小数儿
            Percent = value / sumValue;
            Percent = (double)(Math.Round(Percent, 2));

            return Convert.ToString(Percent);
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
                                m_billNumber = dataGridViewList.Rows[i].Cells[2].ToString();
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
            FormStorageStockDetailsFilter fpicf = new FormStorageStockDetailsFilter();
            fpicf.ShowDialog();

            m_countStartDate = fpicf.getFilterStartDate();
            m_countEndDate = fpicf.getFilterEndDate();

            updateDataGridView(fpicf.getMaterielPkey());
            
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

                                m_billNumber = dataGridViewList.Rows[i].Cells[2].Value.ToString();
                                m_billType = dataGridViewList.Rows[i].Cells[3].Value.ToString();
                                break;
                            }
                        }
                    }
                }

                if (m_billType.Length > 0 && m_billNumber.Length > 0)
                {
                    if (m_billType == "采购入库")
                    {
                        FormPurchaseInOrder fpo = new FormPurchaseInOrder(m_billNumber);
                        fpo.ShowDialog();
                    }
                    else if (m_billType == "盘盈入库")
                    {
                        FormMaterielInEarningsOrder fmoo = new FormMaterielInEarningsOrder(m_billNumber);
                        fmoo.ShowDialog();
                    }
                    else if (m_billType == "产品入库")
                    {
                        FormMaterielInOrder fmoo = new FormMaterielInOrder(m_billNumber);
                        fmoo.ShowDialog();
                    }
                    else if (m_billType == "其他入库")
                    {
                        FormMaterielInOtherOrder fmoo = new FormMaterielInOtherOrder(m_billNumber);
                        fmoo.ShowDialog();
                    }
                    else if (m_billType == "生产领料")
                    {
                        FormMaterielOutOrder fmoo = new FormMaterielOutOrder(m_billNumber);
                        fmoo.ShowDialog();
                    }
                    else if (m_billType == "其他出库")
                    {
                        FormMaterielOutOtherOrder fmoo = new FormMaterielOutOtherOrder(m_billNumber);
                        fmoo.ShowDialog();
                    }
                    else if (m_billType == "盘盈毁损")
                    {
                        FormMaterielOutEarningsOrder fmoo = new FormMaterielOutEarningsOrder(m_billNumber);
                        fmoo.ShowDialog();
                    }
                    else if (m_billType == "销售出库")
                    {
                        FormSaleOutOrder fpo = new FormSaleOutOrder(m_billNumber);
                        fpo.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void upRecord_Click(object sender, EventArgs e)
        {
            m_currentRecordIndex -= 1;
            updateDataGridView();
        }

        private void nextRecord_Click(object sender, EventArgs e)
        {
            m_currentRecordIndex += 1;
            updateDataGridView();
        }

        private void dataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}