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
    public partial class FormDisplayCountInfoFromSQL : Form
    {
        public enum CountType
        {
            // 采购管理相关单据
            PurchaseMateriel,
            PurchasePeople,
            PurchaseSupplier,
            PurchaseHistoryPrice,

            // 销售管理相关单据
            SaleMateriel,
            SalePeople,
            SaleCustomer,
            SaleHistoryPrice,

            // 仓存管理
            StorageManagerMaterielCount,
            StorageManagerProduceIn,

            // 存货核算相关报表
            MaterielInOutCount,
            StorageMaterielOut
        };

        private CountType m_orderType;
        private bool m_isCountAllBill = false;
        private int m_dataGridRecordCount = 0;
        private string m_countStartDate, m_countEndDate;
        private int m_materielID = -1;

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();

        public FormDisplayCountInfoFromSQL(CountType orderType, int materielID = -1)
        {
            InitializeComponent();

            m_orderType = orderType;
            m_materielID = materielID;

            if (m_orderType == CountType.PurchaseMateriel)
            {
                this.Text = "采购金额统计-按物料类型";
            }
            else if (m_orderType == CountType.PurchasePeople)
            {
                this.Text = "采购金额统计-按采购员名称";
            }
            else if (m_orderType == CountType.PurchaseSupplier)
            {
                this.Text = "采购金额统计-按供应商名称";
            }
            else if (m_orderType == CountType.PurchaseHistoryPrice || m_orderType == CountType.SaleHistoryPrice)
            {
                // 用于采购管理->报表->历史采购价格查询->历史采购价格 button click 事件
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(m_materielID);
                this.Text = "物料 [" + record.name + "] 历史采购价格";
            }
            else if (m_orderType == CountType.SaleMateriel)
            {
                this.Text = "销售金额统计-按商品类型";
            }
            else if (m_orderType == CountType.SalePeople)
            {
                this.Text = "销售金额统计-按业务员名称";
            }
            else if (m_orderType == CountType.SaleCustomer)
            {
                this.Text = "销售金额统计-按客户名称";
            }
            else if (m_orderType == CountType.StorageMaterielOut)
            {
                this.Text = "生产领料统计";
            }
            else if (m_orderType == CountType.StorageManagerMaterielCount)
            {
                this.Text = "物料出入库明细";
            }
            else if (m_orderType == CountType.MaterielInOutCount)
            {
                this.Text = "物料出入库核算";
            }
            else if (m_orderType == CountType.StorageManagerProduceIn)
            {
                this.Text = "产品入库汇总";
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
            if (m_orderType == CountType.PurchaseMateriel)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("物料编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("采购金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.PurchasePeople)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("采购员ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("采购员名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("采购金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.PurchaseSupplier)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("供应商ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("供应商名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("采购金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.PurchaseHistoryPrice)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("供应商ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("供应商名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易数量", 100);
            }
            else if (m_orderType == CountType.SaleMateriel)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("商品编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("商品名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("销售金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.SalePeople)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("业务员ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("业务员名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("销售金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.SaleCustomer)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("客户ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("客户名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("销售金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.SaleHistoryPrice)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("客户ID", 100);
                m_dateGridViewExtend.addDataGridViewColumn("客户名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易数量", 100);
            }
            else if (m_orderType == CountType.StorageMaterielOut)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("物料编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("百分比", 100);
            }
            else if (m_orderType == CountType.StorageManagerMaterielCount)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("物料编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("事务类型", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易数量", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易单价", 100, false);
                m_dateGridViewExtend.addDataGridViewColumn("结存数量", 100);
                m_dateGridViewExtend.addDataGridViewColumn("结存单价", 100, false);
            }
            else if (m_orderType == CountType.MaterielInOutCount)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("物料编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("事务类型", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单价", 100);
                m_dateGridViewExtend.addDataGridViewColumn("数量", 100);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 100);
            }
            else if (m_orderType == CountType.StorageManagerProduceIn)
            {
                m_dateGridViewExtend.addDataGridViewColumn("序号", 60);
                m_dateGridViewExtend.addDataGridViewColumn("产品编码", 100);
                m_dateGridViewExtend.addDataGridViewColumn("产品名称", 200);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("数量", 100);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 100, false);
            }

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            this.labelMaterieInOutlCountInfo.Visible = false;

            double sumTotleMoney = 0;
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();

            if (m_orderType == CountType.PurchaseMateriel)
            {
                sortedDictionaryList = getPurchaseMaterielData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.PurchasePeople)
            {
                sortedDictionaryList = getPurchasePeopleData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.PurchaseSupplier)
            {
                sortedDictionaryList = getPurchaseSupplierData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.PurchaseHistoryPrice)
            {
                sortedDictionaryList = getPurchaseHistoryPriceData();
            }
            else if (m_orderType == CountType.SaleMateriel)
            {
                sortedDictionaryList = getSaleMaterielData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.SalePeople)
            {
                sortedDictionaryList = getSalePeopleData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.SaleCustomer)
            {
                sortedDictionaryList = getSaleCustomerData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.SaleHistoryPrice)
            {
                sortedDictionaryList = getSaleHistoryPriceData();
            }
            else if (m_orderType == CountType.StorageMaterielOut)
            {
                sortedDictionaryList = getStorageMaterielOutData(out sumTotleMoney);
            }
            else if (m_orderType == CountType.StorageManagerMaterielCount)
            {
                sortedDictionaryList = getStorageManagerMaterielCountData();
            }
            else if (m_orderType == CountType.MaterielInOutCount)
            {
                sortedDictionaryList = getMaterieInOutlCountData();
                this.labelMaterieInOutlCountInfo.Visible = true;
            }
            else if (m_orderType == CountType.StorageManagerProduceIn)
            {
                sortedDictionaryList = getStorageManagerProduceInData();
            }

            if (m_orderType == CountType.PurchaseHistoryPrice ||
                m_orderType == CountType.SaleHistoryPrice ||
                m_orderType == CountType.StorageManagerMaterielCount ||
                m_orderType == CountType.MaterielInOutCount ||
                m_orderType == CountType.StorageManagerProduceIn)
            {
                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList);
            }
            else
            {
                // 添加合计行
                ArrayList sumRow = new ArrayList();
                sumRow.Add(sortedDictionaryList.Count + 1);
                sumRow.Add("合计");
                sumRow.Add("");

                if (m_orderType == CountType.PurchaseMateriel || 
                    m_orderType == CountType.SaleMateriel ||
                    m_orderType == CountType.StorageMaterielOut
                    )
                {
                    sumRow.Add("");
                }

                sumRow.Add(sumTotleMoney);

                sumRow.Add("100%");
                sortedDictionaryList.Add(sortedDictionaryList.Count, sumRow);

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList);
                dataGridViewList.Rows[sortedDictionaryList.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
            }

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

        private double getSumTotleMoney(SortedDictionary<int, SaleOutOrderDetailsTable> list)
        {
            double sumTotleMoney = 0.0;

            for (int index = 0; index < list.Count; index++)
            {
                SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();
                record = (SaleOutOrderDetailsTable)list[index];
                sumTotleMoney += record.totalMoney;
            }
            return sumTotleMoney;
        }

        private double getSumTotleMoney(SortedDictionary<int, SaleOutOrderTable> list)
        {
            double sumTotleMoney = 0.0;

            for (int index = 0; index < list.Count; index++)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();
                record = (SaleOutOrderTable)list[index];
                sumTotleMoney += Convert.ToDouble(record.totalMoney.ToString());
            }

            return sumTotleMoney;
        }

        private double getSumTotleMoney(SortedDictionary<int, MaterielOutOrderDetailsTable> list)
        {
            double sumTotleMoney = 0.0;

            for (int index = 0; index < list.Count; index++)
            {
                MaterielOutOrderDetailsTable record = new MaterielOutOrderDetailsTable();
                record = (MaterielOutOrderDetailsTable)list[index];
                sumTotleMoney += record.sumMoney;
            }
            return sumTotleMoney;
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

        private SortedDictionary<int, ArrayList> getPurchaseMaterielData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, PurchaseInOrderDetailsTable> list = new SortedDictionary<int, PurchaseInOrderDetailsTable>();

            list = PurchaseInOrderDetails.getInctance().getPurchaseInOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
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

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getPurchasePeopleData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

            list = PurchaseInOrder.getInctance().getPurchaseInOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill, 2);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
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

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getPurchaseSupplierData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

            list = PurchaseInOrder.getInctance().getPurchaseInOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill, 1);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
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

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getPurchaseHistoryPriceData()
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

            list = PurchaseInOrderDetails.getInctance().getPurchasePriceCountInfo(m_materielID, m_countStartDate, m_countEndDate);

            m_dataGridRecordCount = list.Count;

            for (int index = 0; index < list.Count; index++)
            {
                PurchaseInOrderTable record = new PurchaseInOrderTable();
                record = (PurchaseInOrderTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.billNumber);
                temp.Add(record.supplierId);
                temp.Add(record.supplierName);
                temp.Add(record.tradingDate);
                temp.Add(record.totalMoney);
                temp.Add(record.sumValue);

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSaleMaterielData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleOutOrderDetailsTable> list = new SortedDictionary<int, SaleOutOrderDetailsTable>();

            list = SaleOutOrderDetails.getInctance().getSaleOutOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
            {
                SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();
                record = (SaleOutOrderDetailsTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielModel);
                temp.Add(record.totalMoney);
                temp.Add(getPercentValue(record.totalMoney, sumTotleMoney));

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSalePeopleData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleOutOrderTable> list = new SortedDictionary<int, SaleOutOrderTable>();

            list = SaleOutOrder.getInctance().getSaleOutOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill, 2);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();
                record = (SaleOutOrderTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.businessPeopleId);
                temp.Add(record.businessPeopleName);
                temp.Add(record.totalMoney);
                temp.Add(getPercentValue(Convert.ToDouble(record.totalMoney.ToString()), sumTotleMoney));

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSaleCustomerData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleOutOrderTable> list = new SortedDictionary<int, SaleOutOrderTable>();

            list = SaleOutOrder.getInctance().getSaleOutOrderCountInfo(m_countStartDate, m_countEndDate, m_isCountAllBill, 1);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();
                record = (SaleOutOrderTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.customerId);
                temp.Add(record.customerName);
                temp.Add(record.totalMoney);
                temp.Add(getPercentValue(Convert.ToDouble(record.totalMoney.ToString()), sumTotleMoney));

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getSaleHistoryPriceData()
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, SaleOutOrderTable> list = new SortedDictionary<int, SaleOutOrderTable>();

            list = SaleOutOrderDetails.getInctance().getSalePriceCountInfo(m_materielID, m_countStartDate, m_countEndDate);

            m_dataGridRecordCount = list.Count;

            for (int index = 0; index < list.Count; index++)
            {
                SaleOutOrderTable record = new SaleOutOrderTable();
                record = (SaleOutOrderTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.billNumber);
                temp.Add(record.customerId);
                temp.Add(record.customerName);
                temp.Add(record.tradingDate);
                temp.Add(record.totalMoney);
                temp.Add(record.sumValue);

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private SortedDictionary<int, ArrayList> getStorageMaterielOutData(out double sumTotleMoney)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, MaterielOutOrderDetailsTable> list = new SortedDictionary<int, MaterielOutOrderDetailsTable>();

            list = MaterielOutOrderDetails.getInctance().getMaterielOutOrderCountInfo2(m_countStartDate, m_countEndDate, m_isCountAllBill);

            m_dataGridRecordCount = list.Count;
            sumTotleMoney = getSumTotleMoney(list);

            for (int index = 0; index < list.Count; index++)
            {
                MaterielOutOrderDetailsTable record = new MaterielOutOrderDetailsTable();
                record = (MaterielOutOrderDetailsTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielModel);
                temp.Add(record.sumMoney);
                temp.Add(getPercentValue(record.sumMoney, sumTotleMoney));

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        // 物料收发明细
        private SortedDictionary<int, ArrayList> getStorageManagerMaterielCountData()
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, StorageStockDetailTable> list = new SortedDictionary<int, StorageStockDetailTable>();

            list = StorageStockDetail.getInctance().getAllStorageStockDetailInfo(m_countStartDate, m_countEndDate);

            m_dataGridRecordCount = list.Count;

            for (int index = 0; index < list.Count; index++)
            {
                StorageStockDetailTable record = new StorageStockDetailTable();
                record = (StorageStockDetailTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.model);
                temp.Add(record.tradingDate);
                temp.Add(record.thingsType);
                temp.Add(record.value);
                temp.Add(record.price);
                temp.Add(record.storageValue);
                temp.Add(record.storagePrice);

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        // 物料出入库核算
        private SortedDictionary<int, ArrayList> getMaterieInOutlCountData()
        {
            double inCount = 0, outCount = 0;
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, StorageStockDetailTable> list = new SortedDictionary<int, StorageStockDetailTable>();

            list = StorageStockDetail.getInctance().getAllStorageStockDetailInfo(m_countStartDate, m_countEndDate);

            m_dataGridRecordCount = list.Count;

            for (int index = 0; index < list.Count; index++)
            {
                StorageStockDetailTable record = new StorageStockDetailTable();
                record = (StorageStockDetailTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.model);
                temp.Add(record.tradingDate);
                temp.Add(record.thingsType);
                temp.Add(record.price);
                temp.Add(record.value);
                temp.Add(record.value * record.price);

                if (record.thingsType.IndexOf("入库") > 0)
                {
                    inCount += record.sumMoney;
                }
                else
                {
                    outCount += record.sumMoney;
                }

                sortedDictionaryList.Add(index, temp);
            }

            string countInfoToStatusLable = "当前会计期间:累计入库金额 ";
            inCount = (double)(Math.Round(inCount * 10000)) / 10000;
            countInfoToStatusLable += Convert.ToString(inCount);

            countInfoToStatusLable += ", 累计出库金额 ";
            outCount = (double)(Math.Round(outCount * 10000)) / 10000;
            countInfoToStatusLable += Convert.ToString(outCount);

            this.labelMaterieInOutlCountInfo.Text = countInfoToStatusLable;

            return sortedDictionaryList;
        }

        // 产品入库汇总
        private SortedDictionary<int, ArrayList> getStorageManagerProduceInData()
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, MaterielInOrderDetailsTable> list = new SortedDictionary<int, MaterielInOrderDetailsTable>();

            list = MaterielInOrderDetails.getInctance().getMaterielInOrderCountInfo(m_countStartDate, m_countEndDate);

            m_dataGridRecordCount = list.Count;

            for (int index = 0; index < list.Count; index++)
            {
                MaterielInOrderDetailsTable record = new MaterielInOrderDetailsTable();
                record = (MaterielInOrderDetailsTable)list[index];

                ArrayList temp = new ArrayList();

                temp.Add(index + 1);
                temp.Add(record.materielID);
                temp.Add(record.materielName);
                temp.Add(record.materielModel);
                temp.Add(record.value);
                temp.Add(record.sumMoney);

                sortedDictionaryList.Add(index, temp);
            }

            return sortedDictionaryList;
        }

        private void toolStripButtonRefreshHS_Click(object sender, EventArgs e)
        {
            if (m_orderType == CountType.PurchaseMateriel)
            {
                PurchaseInOrderDetails.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.PurchasePeople)
            {
                PurchaseInOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.PurchaseSupplier)
            {
                PurchaseInOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.PurchaseHistoryPrice)
            {
                PurchaseInOrderDetails.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.SaleMateriel)
            {
                SaleOutOrderDetails.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.SalePeople)
            {
                SaleOutOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.SaleCustomer)
            {
                SaleOutOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.SaleHistoryPrice)
            {
                SaleOutOrderDetails.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.StorageMaterielOut)
            {
                MaterielOutOrderDetails.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.StorageManagerMaterielCount)
            {
                StorageStockDetail.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.MaterielInOutCount)
            {
                StorageStockDetail.getInctance().refreshRecord();
            }
            else if (m_orderType == CountType.StorageManagerProduceIn)
            {
                MaterielInOrderDetails.getInctance().refreshRecord();
            }

            updateDataGridView();
        }
    }
}