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
    public partial class FormProjectOrderDetail : Form
    {
        public enum OrderType
        {
            // 采购申请单序时簿
            PurchaseApplyOrder,

            // 采购订单序时簿
            PurchaseOrder,

            // 采购入库单序时簿
            PurchaseIn,

            // 生产领料单
            StorageMaterielOut,

            // 变更申请
            ChangeApply
        };

        private int m_dataGridRecordCount = 0;
        private OrderType m_orderType;
        private string m_billNumber = "";
        private string m_xxMaterielTableNum = "";
        private int m_materielID = -1;
        private string m_srcChangeOrderBillNumber = "";

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();

        public FormProjectOrderDetail(OrderType orderType, string srcBillNumber, int materielID)
        {
            InitializeComponent();

            m_orderType = orderType;

            if (m_orderType == OrderType.PurchaseApplyOrder)
            {
                this.Text = "采购申请详情";
            }
            else if (m_orderType == OrderType.PurchaseOrder)
            {
                this.Text = "采购订单详情";
            }
            else if (m_orderType == OrderType.PurchaseIn)
            {
                this.Text = "采购入库详情";
            }
            else if (m_orderType == OrderType.StorageMaterielOut)
            {
                this.Text = "生产领料详情";
            }
            else if (m_orderType == OrderType.ChangeApply)
            {
                this.Text = "变更申请单详情";
            }

            m_xxMaterielTableNum = srcBillNumber;
            m_materielID = materielID;
        }

        private void FormProjectOrderDetail_Load(object sender, EventArgs e)
        {
            if (m_orderType == OrderType.PurchaseApplyOrder)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("申请人", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("总材料表单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("期望到货日期", 140);
                m_dateGridViewExtend.addDataGridViewColumn("总金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("制单员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("是否审核", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 100);
            }
            else if (m_orderType == OrderType.PurchaseOrder)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("供应商", 150);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("总材料表单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("约定到货日期", 160);
                m_dateGridViewExtend.addDataGridViewColumn("约定付款日期", 160);
                m_dateGridViewExtend.addDataGridViewColumn("金额合计", 100);
                m_dateGridViewExtend.addDataGridViewColumn("运输费用合计", 160);
                m_dateGridViewExtend.addDataGridViewColumn("其他费用合计", 160);
                m_dateGridViewExtend.addDataGridViewColumn("总金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("采购员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("制单员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("是否审核", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 100);
            }
            else if (m_orderType == OrderType.PurchaseIn)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("供应商", 150);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("交易类型", 120);
                m_dateGridViewExtend.addDataGridViewColumn("总材料表单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("约定付款日期", 160);
                m_dateGridViewExtend.addDataGridViewColumn("源单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("金额合计", 100);
                m_dateGridViewExtend.addDataGridViewColumn("计入成本费用", 160);
                m_dateGridViewExtend.addDataGridViewColumn("不计入成本费用", 160);
                m_dateGridViewExtend.addDataGridViewColumn("总金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("保管员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("验收员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("采购员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("制单员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核员", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("记账", 100);
                m_dateGridViewExtend.addDataGridViewColumn("记账日期", 100);
            }
            else if (m_orderType == OrderType.StorageMaterielOut)
            {
                // 生产领料单序时薄
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("领料部门", 140);
                m_dateGridViewExtend.addDataGridViewColumn("日期", 80);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("总材料表单据号", 200);
                m_dateGridViewExtend.addDataGridViewColumn("生产编号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("数量", 80);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 80);
                m_dateGridViewExtend.addDataGridViewColumn("领料人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("制单人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 80);
            }
            else if (m_orderType == OrderType.ChangeApply)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("源单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("设计人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("变更原因", 300);
                m_dateGridViewExtend.addDataGridViewColumn("制单员", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核员", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 80);
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
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();

            if (m_orderType == OrderType.PurchaseApplyOrder)
            {
                SortedDictionary<int, PurchaseApplyOrderTable> list = new SortedDictionary<int, PurchaseApplyOrderTable>();

                SortedDictionary<int, PurchaseApplyOrderTable> listOrderList = new SortedDictionary<int, PurchaseApplyOrderTable>();
                listOrderList = PurchaseApplyOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_xxMaterielTableNum);

                for (int indexOrderList = 0; indexOrderList < listOrderList.Count; indexOrderList++)
                {
                    PurchaseApplyOrderTable recordOrder = new PurchaseApplyOrderTable();
                    recordOrder = (PurchaseApplyOrderTable)listOrderList[indexOrderList];

                    if (PurchaseApplyOrderDetails.getInctance().getPurchaseValueFromMaterielID(recordOrder.billNumber, m_materielID) > 0)
                    {
                        list.Add(list.Count, recordOrder);
                    }
                }

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();
                    record = (PurchaseApplyOrderTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.pkey);
                    temp.Add(record.applyName);
                    temp.Add(record.tradingDate);
                    temp.Add(record.billNumber);
                    temp.Add(record.srcOrderNum);
                    temp.Add(record.paymentDate);
                    temp.Add(record.totalMoney);
                    temp.Add(record.makeOrderStaffName);

                    if (record.isReview == "0")
                    {
                        temp.Add("否");
                    }
                    else
                    {
                        temp.Add("是");
                    }

                    temp.Add(record.orderrReviewName);
                    temp.Add(record.reviewDate);

                    sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.PurchaseOrder)
            {
                SortedDictionary<int, PurchaseOrderTable> list = new SortedDictionary<int, PurchaseOrderTable>();

                SortedDictionary<int, PurchaseOrderTable> listOrderList = new SortedDictionary<int, PurchaseOrderTable>();
                listOrderList = PurchaseOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_xxMaterielTableNum);

                for (int indexOrderList = 0; indexOrderList < listOrderList.Count; indexOrderList++)
                {
                    PurchaseOrderTable recordOrder = new PurchaseOrderTable();
                    recordOrder = (PurchaseOrderTable)listOrderList[indexOrderList];

                    if (PurchaseOrderDetails.getInctance().getPurchaseValueFromBillNumber(recordOrder.billNumber, m_materielID) > 0)
                    {
                        list.Add(list.Count, recordOrder);
                    }
                }

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    PurchaseOrderTable record = new PurchaseOrderTable();
                    record = (PurchaseOrderTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.pkey);
                    temp.Add(record.supplierName);
                    temp.Add(record.tradingDate);
                    temp.Add(record.billNumber);
                    temp.Add(record.xxMaterielTableNum);
                    temp.Add(record.deliveryDate);
                    temp.Add(record.paymentDate);
                    temp.Add(record.sumMoney);
                    temp.Add(record.sumTransportationCost);
                    temp.Add(record.sumOtherCost);
                    temp.Add(record.totalMoney);
                    temp.Add(record.businessPeopleName);
                    temp.Add(record.makeOrderStaffName);

                    if (record.isReview == "0")
                    {
                        temp.Add("否");
                    }
                    else
                    {
                        temp.Add("是");
                    }

                    temp.Add(record.orderrReviewName);
                    temp.Add(record.reviewDate);

                    sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.PurchaseIn)
            {
                SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();

                SortedDictionary<int, PurchaseInOrderTable> listOrderList = new SortedDictionary<int, PurchaseInOrderTable>();
                listOrderList = PurchaseInOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_xxMaterielTableNum);

                for (int indexOrderList = 0; indexOrderList < listOrderList.Count; indexOrderList++)
                {
                    PurchaseInOrderTable recordOrder = new PurchaseInOrderTable();
                    recordOrder = (PurchaseInOrderTable)listOrderList[indexOrderList];

                    if (PurchaseInOrderDetails.getInctance().getPurchaseValueFromBillNumber(recordOrder.billNumber, m_materielID) > 0)
                    {
                        list.Add(list.Count, recordOrder);
                    }
                }

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    PurchaseInOrderTable record = new PurchaseInOrderTable();
                    record = (PurchaseInOrderTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.pkey);
                    temp.Add(record.supplierName);
                    temp.Add(record.tradingDate);
                    temp.Add(record.billNumber);
                    temp.Add(record.purchaseType);
                    temp.Add(record.srcOrderNum);
                    temp.Add(record.paymentDate);
                    temp.Add(record.sourceBillNumber);
                    temp.Add(record.sumMoney);
                    temp.Add(record.sumTransportationCost);
                    temp.Add(record.sumOtherCost);
                    temp.Add(record.totalMoney);
                    temp.Add(record.staffSaveName);
                    temp.Add(record.staffCheckName);
                    temp.Add(record.businessPeopleName);
                    temp.Add(record.makeOrderStaffName);
                    temp.Add(record.orderrReviewName);
                    temp.Add(record.reviewDate);
                    temp.Add(record.orderInLedgerName);
                    temp.Add(record.inLedgerDate);

                    sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.StorageMaterielOut)
            {
                SortedDictionary<int, MaterielOutOrderTable> list = new SortedDictionary<int, MaterielOutOrderTable>();

                SortedDictionary<int, MaterielOutOrderTable> listOrderList = new SortedDictionary<int, MaterielOutOrderTable>();
                listOrderList = MaterielOutOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_xxMaterielTableNum);

                for (int indexOrderList = 0; indexOrderList < listOrderList.Count; indexOrderList++)
                {
                    MaterielOutOrderTable recordOrder = new MaterielOutOrderTable();
                    recordOrder = (MaterielOutOrderTable)listOrderList[indexOrderList];

                    if (MaterielOutOrderDetails.getInctance().getPurchaseValueFromMaterielID(recordOrder.billNumber, m_materielID) > 0)
                    {
                        list.Add(list.Count, recordOrder);
                    }
                }

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    MaterielOutOrderTable record = new MaterielOutOrderTable();
                    record = (MaterielOutOrderTable)list[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.pkey);
                    temp.Add(record.departmentName);
                    temp.Add(record.tradingDate);
                    temp.Add(record.billNumber);
                    temp.Add(record.srcOrderNum);
                    temp.Add(record.makeNo);
                    temp.Add(record.sumValue);
                    temp.Add(record.sumMoney);
                    temp.Add(record.materielOutStaffName);
                    temp.Add(record.makeOrderStaffName);
                    temp.Add(record.orderrReviewName);
                    temp.Add(record.reviewDate);

                    sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.ChangeApply)
            {
                //m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                //m_dateGridViewExtend.addDataGridViewColumn("源单据号", 150);
                //m_dateGridViewExtend.addDataGridViewColumn("设计人", 80);
                //m_dateGridViewExtend.addDataGridViewColumn("单据号", 150);
                //m_dateGridViewExtend.addDataGridViewColumn("变更原因", 300);
                //m_dateGridViewExtend.addDataGridViewColumn("制单员", 80);
                //m_dateGridViewExtend.addDataGridViewColumn("审核员", 80);
                //m_dateGridViewExtend.addDataGridViewColumn("审核日期", 80);


                // 根据源单据号，得到订单详细单据
                if (m_srcChangeOrderBillNumber.Length > 0)
                {
                    SortedDictionary<int, FormProjectMaterielChangeTable> changtList = FormProjectInfoChange.getInctance().getChangeListFromSrcBillNumber(m_srcChangeOrderBillNumber);
                    m_dataGridRecordCount = changtList.Count;

                    for (int index = 0; index < changtList.Count; index++)
                    {
                        FormProjectMaterielChangeTable record = new FormProjectMaterielChangeTable();
                        record = (FormProjectMaterielChangeTable)changtList[index];

                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.srcBillNumber);
                        temp.Add(record.designStaffName);
                        temp.Add(record.billNumber);
                        temp.Add(record.changeReason);
                        temp.Add(record.makeOrderStaffName);
                        temp.Add(record.orderrReviewName);
                        temp.Add(record.reviewDate);

                        sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                    }

                    m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
                }
            }
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
                                m_billNumber = dataGridViewList.Rows[i].Cells[3].Value.ToString();
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
                                m_billNumber = dataGridViewList.Rows[i].Cells[3].Value.ToString();
                                m_xxMaterielTableNum = dataGridViewList.Rows[i].Cells[4].Value.ToString();
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
            if (m_billNumber.Length > 0)
            {
                if (m_orderType == OrderType.PurchaseApplyOrder)
                {
                    FormPurchaseApply fpa = new FormPurchaseApply(m_billNumber);
                    fpa.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.PurchaseOrder)
                {
                    FormPurchaseOrder fpo = new FormPurchaseOrder(m_billNumber);
                    fpo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.PurchaseIn)
                {
                    FormPurchaseInOrder fpo = new FormPurchaseInOrder(m_billNumber);
                    fpo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.StorageMaterielOut)
                {
                    FormMaterielOutOrder fmoo = new FormMaterielOutOrder(m_billNumber);
                    fmoo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.ChangeApply)
                {
                    FormProjectMaterielChangeOrder fmoo = new FormProjectMaterielChangeOrder(m_billNumber);
                    fmoo.ShowDialog();
                    updateDataGridView();
                }
                else
                {
                    MessageBoxExtend.messageWarning("暂时不支持的序时薄类型");
                }
            }
        }

        public string getSelectOrderNumber()
        {
            return m_billNumber;
        }

        public string getSelectOrderProjectNum()
        {
            return m_xxMaterielTableNum;
        }

        public void setDataFilter(FormStorageSequenceFilterValue filter)
        {
            m_filter = filter;
        }

        public void setChangeBillNumber(string srcBillNumber)
        {
            m_srcChangeOrderBillNumber = srcBillNumber;
        }
    }
}