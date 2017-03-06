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
    public partial class FormPurchaseOrderSequence : Form
    {
        public enum OrderType
        {
            // 采购申请单序时簿
            PurchaseApplyOrder,

            // 采购订单序时簿
            PurchaseOrder,

            // 采购入库单序时簿
            PurchaseIn,

            // 采购发票序时簿
            PurchaseInvoice,

            // 采购订单执行情况
            PurchaseOrderExcute,

            // 采购入库单付款情况
            PurchaseInOrderExcute,

            // 产品入库序时簿
            StorageProductIn,

            // 盘盈入库
            StorageInCheck,

            // 其他入库
            StorageInOther,
        };

        private int m_dataGridRecordCount = 0;
        private OrderType m_orderType;
        private string m_billNumber = "";
        private string m_projectNum = "";
        private bool m_isSelectOrderNumber;

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();

        public FormPurchaseOrderSequence(OrderType orderType, bool isSelectOrderNumber = false)
        {
            InitializeComponent();

            m_orderType = orderType;

            if (m_orderType == OrderType.PurchaseOrder)
            {
                this.Text = "采购订单序时薄";
            }
            else if (m_orderType == OrderType.PurchaseApplyOrder)
            {
                this.Text = "采购申请单序时薄";
            }
            else if (m_orderType == OrderType.PurchaseIn)
            {
                this.Text = "采购入库单序时薄";
            }
            else if (m_orderType == OrderType.PurchaseInvoice)
            {
                this.Text = "采购发票序时薄";
            }
            else if (m_orderType == OrderType.PurchaseOrderExcute)
            {
                this.Text = "采购订单执行情况";
            }
            else if (m_orderType == OrderType.PurchaseInOrderExcute)
            {
                this.Text = "采购入库付款情况";
            }
            else if (m_orderType == OrderType.StorageProductIn)
            {
                // 仓存管理-产品入库
                this.Text = "产品入库序时薄";
            }
            else if (m_orderType == OrderType.StorageInCheck)
            {
                // 仓存管理-盘盈入库
                this.Text = "盘盈入库序时薄";
            }
            else if (m_orderType == OrderType.StorageInOther)
            {
                // 仓存管理-其他入库
                this.Text = "其他入库序时薄";
            }

            m_isSelectOrderNumber = isSelectOrderNumber;
        }

        private void FormPurchaseOrderSequence_Load(object sender, EventArgs e)
        {
            if (m_orderType == OrderType.PurchaseOrder)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("供应商", 150);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("项目编号", 120);
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
            else if (m_orderType == OrderType.PurchaseApplyOrder)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("申请人", 100);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("项目编号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("期望到货日期", 140);
                m_dateGridViewExtend.addDataGridViewColumn("总金额", 100);
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
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 150);
                m_dateGridViewExtend.addDataGridViewColumn("交易类型", 120);
                m_dateGridViewExtend.addDataGridViewColumn("项目编号", 120);
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
            else if (m_orderType == OrderType.PurchaseInvoice)
            {
                MessageBoxExtend.messageWarning("暂时不支持采购发票序时薄类型");
                return;
            }
            else if (m_orderType == OrderType.PurchaseOrderExcute)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("供应商", 150);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("约定到货日期", 150);
                m_dateGridViewExtend.addDataGridViewColumn("订单物料总数量", 150);
                m_dateGridViewExtend.addDataGridViewColumn("是否已入库", 150);
                m_dateGridViewExtend.addDataGridViewColumn("实际入库数量", 150);
                m_dateGridViewExtend.addDataGridViewColumn("采购员", 100);
            }
            else if (m_orderType == OrderType.PurchaseInOrderExcute)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("供应商", 150);
                m_dateGridViewExtend.addDataGridViewColumn("交易日期", 100);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("采购类型", 100);
                m_dateGridViewExtend.addDataGridViewColumn("约定付款日期", 110);
                m_dateGridViewExtend.addDataGridViewColumn("费用总金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("已付款金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("未付款金额", 100);
                m_dateGridViewExtend.addDataGridViewColumn("采购员", 100);
            }
            else if (m_orderType == OrderType.StorageProductIn)
            {
                // 仓存管理-产品入库
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("领料部门", 140);
                m_dateGridViewExtend.addDataGridViewColumn("日期", 80);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("数量", 80);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 80);
                m_dateGridViewExtend.addDataGridViewColumn("验收人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("制单人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 80);
            }
            else if (m_orderType == OrderType.StorageInCheck)
            {
                // 仓存管理-盘盈入库
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("日期", 80);
                m_dateGridViewExtend.addDataGridViewColumn("领料部门", 140, false);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("数量", 80);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 80);
                m_dateGridViewExtend.addDataGridViewColumn("验收人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("制单人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 80);
            }
            else if (m_orderType == OrderType.StorageInOther)
            {
                // 仓存管理-其他入库
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("日期", 80);
                m_dateGridViewExtend.addDataGridViewColumn("领料部门", 140, false);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("数量", 80);
                m_dateGridViewExtend.addDataGridViewColumn("金额", 80);
                m_dateGridViewExtend.addDataGridViewColumn("验收人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("制单人", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核人", 80);
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

            if (m_orderType == OrderType.PurchaseOrder)
            {
                SortedDictionary<int, PurchaseOrderTable> list = new SortedDictionary<int, PurchaseOrderTable>();
                list = PurchaseOrder.getInctance().getAllPurchaseOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    PurchaseOrderTable record = new PurchaseOrderTable();
                    record = (PurchaseOrderTable)list[index];

                    if (m_filter.startDate == null ||(record.tradingDate.CompareTo(m_filter.startDate) >= 0 && record.tradingDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.supplierName);
                        temp.Add(record.tradingDate);
                        temp.Add(record.billNumber);
                        temp.Add(record.projectNum);
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
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.PurchaseApplyOrder)
            {
                SortedDictionary<int, PurchaseApplyOrderTable> list = new SortedDictionary<int, PurchaseApplyOrderTable>();
                list = PurchaseApplyOrder.getInctance().getAllPurchaseOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();
                    record = (PurchaseApplyOrderTable)list[index];

                    if (m_filter.startDate == null || (record.tradingDate.CompareTo(m_filter.startDate) >= 0 && record.tradingDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.applyName);
                        temp.Add(record.tradingDate);
                        temp.Add(record.billNumber);
                        temp.Add(record.proNum);
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
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.PurchaseIn)
            {
                SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();
                list = PurchaseInOrder.getInctance().getAllPurchaseInOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    PurchaseInOrderTable record = new PurchaseInOrderTable();
                    record = (PurchaseInOrderTable)list[index];

                    if (m_filter.startDate == null || (record.tradingDate.CompareTo(m_filter.startDate) >= 0 && record.tradingDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.supplierName);
                        temp.Add(record.tradingDate);
                        temp.Add(record.billNumber);
                        temp.Add(record.purchaseType);
                        temp.Add(record.contractNum);
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
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.PurchaseInvoice)
            {
            }
            else if (m_orderType == OrderType.PurchaseOrderExcute)
            {
                SortedDictionary<int, PurchaseOrderTable> list = new SortedDictionary<int, PurchaseOrderTable>();
                list = PurchaseOrder.getInctance().getAllPurchaseOrderInfo();

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
                    temp.Add(record.deliveryDate);
                    temp.Add(record.sumValue);

                    if (record.isInStorage == "0")
                    {
                        temp.Add("否");
                    }
                    else
                    {
                        temp.Add("是");
                    }

                    temp.Add(record.actualValue);
                    temp.Add(record.businessPeopleName);

                    sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.PurchaseInOrderExcute)
            {
                SortedDictionary<int, PurchaseInOrderTable> list = new SortedDictionary<int, PurchaseInOrderTable>();
                list = PurchaseInOrder.getInctance().getAllPurchaseInOrderInfo();

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
                    temp.Add(record.paymentDate);
                    temp.Add(record.totalMoney);
                    temp.Add(record.paymentOk);
                    temp.Add(record.paymentNoOk);
                    temp.Add(record.businessPeopleName);

                    sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.StorageProductIn)
            {
                // 仓存管理-产品入库
                SortedDictionary<int, MaterielInOrderTable> list = new SortedDictionary<int, MaterielInOrderTable>();
                list = MaterielInOrder.getInctance().getAllMaterielInOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    bool isDisplayRecord = false;
                    MaterielInOrderTable record = new MaterielInOrderTable();
                    record = (MaterielInOrderTable)list[index];

                    if (m_filter.startDate == null || (record.tradingDate.CompareTo(m_filter.startDate) >= 0 && record.tradingDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        // 等于0代表只显示已审核单据
                        if (m_filter.allReview == "0")
                        {
                            if (record.isReview == "1")
                            {
                                if (m_filter.billColor == "0")  // 需要显示蓝字单据
                                {
                                    if (record.isRedBill == 0)
                                    {
                                        isDisplayRecord = true;
                                    }
                                }
                                else if (m_filter.billColor == "1")  // 需要显示红字单据
                                {
                                    if (record.isRedBill == 1)
                                    {
                                        isDisplayRecord = true;
                                    }
                                }
                                else                                // 需要显示全部颜色单据
                                {
                                    isDisplayRecord = true;
                                }
                            }
                        }
                        else
                        {
                            if (m_filter.billColor == "0")  // 需要显示蓝字单据
                            {
                                if (record.isRedBill == 0)
                                {
                                    isDisplayRecord = true;
                                }
                            }
                            else if (m_filter.billColor == "1")  // 需要显示红字单据
                            {
                                if (record.isRedBill == 1)
                                {
                                    isDisplayRecord = true;
                                }
                            }
                            else                                // 需要显示全部颜色单据
                            {
                                isDisplayRecord = true;
                            }
                        }
                    }

                    if (isDisplayRecord)
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.departmentName);
                        temp.Add(record.tradingDate);
                        temp.Add(record.billNumber);
                        temp.Add(record.sumValue);
                        temp.Add(record.sumMoney);
                        temp.Add(record.orderReviewStaffName);
                        temp.Add(record.makeOrderStaffName);
                        temp.Add(record.orderrReviewName);
                        temp.Add(record.reviewDate);

                        sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                    }
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.StorageInCheck)
            {
                // 仓存管理-盘盈入库
                SortedDictionary<int, MaterielInEarningsOrderTable> list = new SortedDictionary<int, MaterielInEarningsOrderTable>();
                list = MaterielInEarningsOrder.getInctance().getAllMaterielInEarningsOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    bool isDisplayRecord = false;
                    MaterielInEarningsOrderTable record = new MaterielInEarningsOrderTable();
                    record = (MaterielInEarningsOrderTable)list[index];

                    if (m_filter.startDate == null || (record.tradingDate.CompareTo(m_filter.startDate) >= 0 && record.tradingDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        // 等于0代表只显示已审核单据
                        if (m_filter.allReview == "0")
                        {
                            if (record.isReview == "1")
                            {
                                if (m_filter.billColor == "0")  // 需要显示蓝字单据
                                {
                                    if (record.isRedBill == 0)
                                    {
                                        isDisplayRecord = true;
                                    }
                                }
                                else if (m_filter.billColor == "1")  // 需要显示红字单据
                                {
                                    if (record.isRedBill == 1)
                                    {
                                        isDisplayRecord = true;
                                    }
                                }
                                else                                // 需要显示全部颜色单据
                                {
                                    isDisplayRecord = true;
                                }
                            }
                        }
                        else
                        {
                            if (m_filter.billColor == "0")  // 需要显示蓝字单据
                            {
                                if (record.isRedBill == 0)
                                {
                                    isDisplayRecord = true;
                                }
                            }
                            else if (m_filter.billColor == "1")  // 需要显示红字单据
                            {
                                if (record.isRedBill == 1)
                                {
                                    isDisplayRecord = true;
                                }
                            }
                            else                                // 需要显示全部颜色单据
                            {
                                isDisplayRecord = true;
                            }
                        }
                    }

                    if (isDisplayRecord)
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.tradingDate);
                        temp.Add("");
                        temp.Add(record.billNumber);
                        temp.Add(record.sumValue);
                        temp.Add(record.sumMoney);
                        temp.Add(record.orderReviewStaffName);
                        temp.Add(record.makeOrderStaffName);
                        temp.Add(record.orderrReviewName);
                        temp.Add(record.reviewDate);

                        sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                    }
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else if (m_orderType == OrderType.StorageInOther)
            {
                // 仓存管理-其他入库
                SortedDictionary<int, MaterielInOtherOrderTable> list = new SortedDictionary<int, MaterielInOtherOrderTable>();
                list = MaterielInOtherOrder.getInctance().getAllMaterielInOtherOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    bool isDisplayRecord = false;
                    MaterielInOtherOrderTable record = new MaterielInOtherOrderTable();
                    record = (MaterielInOtherOrderTable)list[index];

                    if (m_filter.startDate == null || (record.tradingDate.CompareTo(m_filter.startDate) >= 0 && record.tradingDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        // 等于0代表只显示已审核单据
                        if (m_filter.allReview == "0")
                        {
                            if (record.isReview == "1")
                            {
                                if (m_filter.billColor == "0")  // 需要显示蓝字单据
                                {
                                    if (record.isRedBill == 0)
                                    {
                                        isDisplayRecord = true;
                                    }
                                }
                                else if (m_filter.billColor == "1")  // 需要显示红字单据
                                {
                                    if (record.isRedBill == 1)
                                    {
                                        isDisplayRecord = true;
                                    }
                                }
                                else                                // 需要显示全部颜色单据
                                {
                                    isDisplayRecord = true;
                                }
                            }
                        }
                        else
                        {
                            if (m_filter.billColor == "0")  // 需要显示蓝字单据
                            {
                                if (record.isRedBill == 0)
                                {
                                    isDisplayRecord = true;
                                }
                            }
                            else if (m_filter.billColor == "1")  // 需要显示红字单据
                            {
                                if (record.isRedBill == 1)
                                {
                                    isDisplayRecord = true;
                                }
                            }
                            else                                // 需要显示全部颜色单据
                            {
                                isDisplayRecord = true;
                            }
                        }
                    }

                    if (isDisplayRecord)
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.tradingDate);
                        temp.Add("");
                        temp.Add(record.billNumber);
                        temp.Add(record.sumValue);
                        temp.Add(record.sumMoney);
                        temp.Add(record.orderReviewStaffName);
                        temp.Add(record.makeOrderStaffName);
                        temp.Add(record.orderrReviewName);
                        temp.Add(record.reviewDate);

                        sortedDictionaryList.Add(sortedDictionaryList.Count, temp);
                    }
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
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
                                m_projectNum = dataGridViewList.Rows[i].Cells[4].Value.ToString();
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
            if (m_isSelectOrderNumber)
            {
                this.Close();
                return;
            }
            // checkAccountBillDetaile函数需要完成弹出一个新的窗口，用来显示单据编号关联的具体单据

            if (m_billNumber.Length > 0)
            {
                if (m_orderType == OrderType.PurchaseOrder || m_orderType == OrderType.PurchaseOrderExcute)
                {
                    FormPurchaseOrder fpo = new FormPurchaseOrder(m_billNumber);
                    fpo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.PurchaseIn || m_orderType == OrderType.PurchaseInOrderExcute)
                {
                    FormPurchaseInOrder fpo = new FormPurchaseInOrder(m_billNumber);
                    fpo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.PurchaseInvoice)
                {
                    //采购发票序时薄
                }
                else if (m_orderType == OrderType.StorageProductIn)
                {
                    FormMaterielInOrder fmoo = new FormMaterielInOrder(m_billNumber);
                    fmoo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.StorageInCheck)
                {
                    FormMaterielInEarningsOrder fmoo = new FormMaterielInEarningsOrder(m_billNumber);
                    fmoo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.StorageInOther)
                {
                    FormMaterielInOtherOrder fmoo = new FormMaterielInOtherOrder(m_billNumber);
                    fmoo.ShowDialog();
                    updateDataGridView();
                }
                else if (m_orderType == OrderType.PurchaseApplyOrder)
                {
                    FormPurchaseApply fpa = new FormPurchaseApply(m_billNumber);
                    fpa.ShowDialog();
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
            return m_projectNum;
        }

        public void setDataFilter(FormStorageSequenceFilterValue filter)
        {
            m_filter = filter;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            // 刷新按钮逻辑
            if (m_orderType == OrderType.PurchaseOrder)
            {
                PurchaseOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == OrderType.PurchaseIn)
            {
                PurchaseInOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == OrderType.PurchaseInvoice)
            {
            }
            else if (m_orderType == OrderType.PurchaseOrderExcute)
            {
                PurchaseOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == OrderType.PurchaseInOrderExcute)
            {
                PurchaseInOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == OrderType.StorageProductIn)
            {
                // 仓存管理-产品入库
                MaterielInOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == OrderType.StorageInCheck)
            {
                // 仓存管理-盘盈入库
                MaterielInEarningsOrder.getInctance().refreshRecord();
            }
            else if (m_orderType == OrderType.StorageInOther)
            {
                // 仓存管理-其他入库
                MaterielInOtherOrder.getInctance().refreshRecord();
            }

            updateDataGridView();
        }
    }
}