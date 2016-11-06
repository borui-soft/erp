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
    public partial class FormBankCashDetail : Form
    {
        private int m_dataGridRecordCount = 0;
        private bool m_isCashDetail;
        private string m_billNumber = "";

        private string m_billName = "";

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();

        public FormBankCashDetail(string winText, bool isCashDetail = true)
        {
            InitializeComponent();
            m_isCashDetail = isCashDetail;

            this.Text = winText;
        }

        private void FormBankCashDetail_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("日期", 100);
            m_dateGridViewExtend.addDataGridViewColumn("单据名称", 100);
            m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
            m_dateGridViewExtend.addDataGridViewColumn("类型", 120);
            m_dateGridViewExtend.addDataGridViewColumn("往来单位", 150);
            m_dateGridViewExtend.addDataGridViewColumn("交易金额", 100);

            if (m_isCashDetail)
            {
                m_dateGridViewExtend.addDataGridViewColumn("现金余额", 100);
            }
            else
            {
                m_dateGridViewExtend.addDataGridViewColumn("银行账户", 150);
                m_dateGridViewExtend.addDataGridViewColumn("账户余额", 100);
            }

            m_dateGridViewExtend.addDataGridViewColumn("制单人", 100);
            m_dateGridViewExtend.addDataGridViewColumn("审核人", 100);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView(true);
        }

        private void updateDataGridView(bool isALLTableData)
        {
            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();

            if (m_isCashDetail)
            {
                SortedDictionary<int, CashCashsubLedgerTable> cashDetail = new SortedDictionary<int, CashCashsubLedgerTable>();

                if (isALLTableData)
                {
                    cashDetail = CashCashsubLedger.getInctance().getAllCashCashsubLedgerInfo();
                }
                else 
                {
                    cashDetail = CashCashsubLedger.getInctance().getAllReviewCashCashsubLedgerInfo();
                }

                m_dataGridRecordCount = cashDetail.Count;

                for (int index = 0; index < cashDetail.Count; index++)
                {
                    CashCashsubLedgerTable record = new CashCashsubLedgerTable();
                    record = (CashCashsubLedgerTable)cashDetail[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.pkey);
                    temp.Add(record.tradingDate);
                    temp.Add(record.billName);
                    temp.Add(record.billNumber);
                    temp.Add(record.billTypeName);
                    temp.Add(record.exchangesUnitName);
                    temp.Add(record.turnover);
                    temp.Add(record.balance);
                    temp.Add(record.makeOrderStaffName);
                    temp.Add(record.orderReviewName);

                    sortedDictionaryList.Add(index, temp);
                }

                m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 3);
            }
            else
            {
                SortedDictionary<int, BankCashsubLedgerTable> bankDetail = new SortedDictionary<int, BankCashsubLedgerTable>();

                if (isALLTableData)
                {
                    bankDetail = BankCashsubLedger.getInctance().getAllBankCashsubLedgerInfo();
                }
                else
                {
                    bankDetail = BankCashsubLedger.getInctance().getAllReviewBankCashsubLedgerInfo();
                }

                m_dataGridRecordCount = bankDetail.Count;

                for (int index = 0; index < bankDetail.Count; index++)
                {
                    BankCashsubLedgerTable record = new BankCashsubLedgerTable();
                    record = (BankCashsubLedgerTable)bankDetail[index];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.pkey);
                    temp.Add(record.tradingDate);
                    temp.Add(record.billName);
                    temp.Add(record.billNumber);
                    temp.Add(record.billTypeName);
                    temp.Add(record.exchangesUnitName);
                    temp.Add(record.turnover);
                    temp.Add(record.bankName);
                    temp.Add(record.balance);
                    temp.Add(record.makeOrderStaffName);
                    temp.Add(record.orderReviewName);

                    sortedDictionaryList.Add(index, temp);
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
                                m_billName = dataGridViewList.Rows[i].Cells[2].Value.ToString();
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
                                m_billName = dataGridViewList.Rows[i].Cells[2].Value.ToString();
                                m_billNumber = dataGridViewList.Rows[i].Cells[3].Value.ToString();
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
            // checkAccountBillDetaile函数需要完成弹出一个新的窗口，用来显示单据编号关联的具体单据
            if (m_billNumber.Length > 0)
            {
                if (m_billName == "付款单")
                {
                    FormPaymentOrder fpo = new FormPaymentOrder(m_billNumber, !m_isCashDetail);
                    fpo.ShowDialog();
                    updateDataGridView(true);
                }
                else
                {
                    FormReceivableOrder fro = new FormReceivableOrder(m_billNumber, !m_isCashDetail);
                    fro.ShowDialog();
                    updateDataGridView(true);
                }
            }
        }
    }
}
