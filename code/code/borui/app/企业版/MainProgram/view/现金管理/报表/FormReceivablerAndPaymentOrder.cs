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
    public partial class FormReceivablerAndPaymentOrder : Form
    {
        private int m_dataGridRecordCount = 0;
        private bool m_isReceivablerOrder;
        private string m_billNumber;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private SortedDictionary<string, ReceivablerAndPaymentOrderTable> m_receivablerAndPaymentOrderList =
                                         new SortedDictionary<string, ReceivablerAndPaymentOrderTable>();
        

        public FormReceivablerAndPaymentOrder(bool isReceivablerOrder)
        {
            InitializeComponent();
            m_isReceivablerOrder = isReceivablerOrder;

            if (m_isReceivablerOrder)
            {
                this.Text = "收款单明细薄";
            }
            else
            {
                this.Text = "付款单明细薄";
            }
        }

        private void FormReceivablerAndPaymentOrder_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("交易日期", 120);
            m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
            m_dateGridViewExtend.addDataGridViewColumn("类型", 120);
            m_dateGridViewExtend.addDataGridViewColumn("交易金额", 120);
            m_dateGridViewExtend.addDataGridViewColumn("制单人", 130);
            m_dateGridViewExtend.addDataGridViewColumn("审核人", 130);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            if (m_isReceivablerOrder)
            {
                m_receivablerAndPaymentOrderList = getAllOrderFromOrderName("收款单");
            }
            else
            {
                m_receivablerAndPaymentOrderList = getAllOrderFromOrderName("付款单");
            }

            m_dataGridRecordCount = m_receivablerAndPaymentOrderList.Count;

            SortedDictionary<int, ArrayList> arrayData = new SortedDictionary<int, ArrayList>();


            foreach (KeyValuePair<string, ReceivablerAndPaymentOrderTable> index in m_receivablerAndPaymentOrderList)
            {
                ArrayList temp = new ArrayList();

                temp.Add(index.Value.pkey);
                temp.Add(index.Value.tradingDate);
                temp.Add(index.Value.billNumber);
                temp.Add(index.Value.billTypeName);
                temp.Add(index.Value.turnover);
                temp.Add(index.Value.makeOrderStaffName);
                temp.Add(index.Value.orderReviewName);

                arrayData.Add(arrayData.Count, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(arrayData, 2);
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
                            m_billNumber = dataGridViewList.Rows[i].Cells[2].Value.ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void dataGridViewMaterielList_DoubleClick(object sender, EventArgs e)
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
                            openBillFromBillNumber(m_billNumber);
                            return;
                        }
                    }
                }
            }
        }

        private SortedDictionary<string, ReceivablerAndPaymentOrderTable> getAllOrderFromOrderName(string orderName)
        {
            SortedDictionary<string, ReceivablerAndPaymentOrderTable> receivablerAndPaymentOrderList =
                new SortedDictionary<string, ReceivablerAndPaymentOrderTable>();

            // 查询现金账中所有指定类型单据
            SortedDictionary<int, CashCashsubLedgerTable> cashCashsubLedgerList =
                CashCashsubLedger.getInctance().getAllReviewCashCashsubLedgerInfo();
            
            foreach (KeyValuePair<int, CashCashsubLedgerTable> index in cashCashsubLedgerList)
            {
                if (index.Value.billName == orderName)
                {
                    ReceivablerAndPaymentOrderTable record = new ReceivablerAndPaymentOrderTable();

                    record.pkey = index.Value.pkey;
                    record.tradingDate = index.Value.tradingDate;
                    record.makeOrderStaffName = index.Value.makeOrderStaffName;
                    record.orderReviewName = index.Value.orderReviewName;
                    record.turnover = index.Value.turnover;
                    record.billNumber = index.Value.billNumber;
                    record.billTypeName = index.Value.billTypeName;

                    receivablerAndPaymentOrderList.Add(record.billNumber, record);
                }
            }

            // 查询所有的银行账中所有指定类型的单据
            SortedDictionary<int, BankCashsubLedgerTable> bankCashsubLedgerList = 
                BankCashsubLedger.getInctance().getAllReviewBankCashsubLedgerInfo();

            foreach (KeyValuePair<int, BankCashsubLedgerTable> indexbank in bankCashsubLedgerList)
            {
                if (indexbank.Value.billName == orderName)
                {
                    ReceivablerAndPaymentOrderTable record = new ReceivablerAndPaymentOrderTable();

                    record.pkey = indexbank.Value.pkey;
                    record.tradingDate = indexbank.Value.tradingDate;
                    record.makeOrderStaffName = indexbank.Value.makeOrderStaffName;
                    record.orderReviewName = indexbank.Value.orderReviewName;
                    record.turnover = indexbank.Value.turnover;
                    record.billNumber = indexbank.Value.billNumber;
                    record.billTypeName = indexbank.Value.billTypeName;

                    receivablerAndPaymentOrderList.Add(record.billNumber, record);
                }
            }

            return receivablerAndPaymentOrderList;
        }


        private void billDetail_Click(object sender, EventArgs e)
        {
            openBillFromBillNumber(m_billNumber);
        }

        private void openBillFromBillNumber(string billNumber)
        {
            if (m_billNumber.Length > 0)
            {
                bool isCashBill = CashCashsubLedger.getInctance().checkBillIsExist(m_billNumber);

                if (m_isReceivablerOrder)
                {
                    FormReceivableOrder fro = new FormReceivableOrder(m_billNumber, !isCashBill);
                    fro.ShowDialog();
                }
                else
                {
                    FormPaymentOrder fpo = new FormPaymentOrder(m_billNumber, !isCashBill);
                    fpo.ShowDialog();
                }
            }
        }
    }

    public class ReceivablerAndPaymentOrderTable
    {
        public int pkey { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }
        public string billTypeName { get; set; }
        public double turnover { get; set; }
        public string makeOrderStaffName { get; set; }
        public string orderReviewName { get; set; }
    }
}