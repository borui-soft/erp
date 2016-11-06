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
    public partial class FormAccountReceivablerDetail : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_customerOrSupplierID = 0;
        private bool m_isAccountReceivable;
        private string m_billNumber = "";
        private string m_billTypeName = "";

        public FormAccountReceivablerDetail(string winText, bool isAccountReceivable = true, int customerOrSupplierID = 0)
        {
            InitializeComponent();
            m_isAccountReceivable = isAccountReceivable;
            m_customerOrSupplierID = customerOrSupplierID;

            this.Text = winText;
        }

        private void FormAccountReceivablerDetail_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("往来单位名称", 150);
            m_dateGridViewExtend.addDataGridViewColumn("单据类型", 150);
            m_dateGridViewExtend.addDataGridViewColumn("本次交易额", 130);
            m_dateGridViewExtend.addDataGridViewColumn("实际欠款额", 130);
            m_dateGridViewExtend.addDataGridViewColumn("交易日期", 120);
            m_dateGridViewExtend.addDataGridViewColumn("单据编号", 150);
            m_dateGridViewExtend.addDataGridViewColumn("职员", 100);
            m_dateGridViewExtend.addDataGridViewColumn("备注", 100);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewList);
            updateDataGridView();
        }

        private void updateDataGridView()
        {
            SortedDictionary<int, CashAccountReceivableDetailTable> dateList = new SortedDictionary<int, CashAccountReceivableDetailTable>();

            if (m_isAccountReceivable)
            {
                dateList = CashAccountReceivableDetail.getInctance().getAccountReceivableDetailFromCustomerID(m_customerOrSupplierID);
            }
            else
            {
                dateList = CashAccountPayableDetail.getInctance().getAccountPayableDetailFromSupplierID(m_customerOrSupplierID);
            }

            m_dataGridRecordCount = dateList.Count;

            SortedDictionary<int, ArrayList> sortedDictionaryList = new SortedDictionary<int, ArrayList>();

            int recordCount = 0;
            for (recordCount = 0; recordCount < dateList.Count; recordCount++)
            {
                CashAccountReceivableDetailTable record = new CashAccountReceivableDetailTable();
                record = (CashAccountReceivableDetailTable)dateList[recordCount];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.name);
                temp.Add(record.billTypeName);
                temp.Add(record.turnover);
                temp.Add(record.balance);
                temp.Add(record.tradingDate);
                temp.Add(record.billNumber);
                temp.Add(record.staffName);
                temp.Add(record.note);

                sortedDictionaryList.Add(recordCount, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(sortedDictionaryList, 2);
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
                                m_billNumber = dataGridViewList.Rows[i].Cells[6].Value.ToString();
                                m_billTypeName = dataGridViewList.Rows[i].Cells[2].Value.ToString();
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
                                m_billNumber = dataGridViewList.Rows[i].Cells[6].Value.ToString();
                                m_billTypeName = dataGridViewList.Rows[i].Cells[2].Value.ToString();
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
                if (m_billTypeName.IndexOf("采购") != -1)
                {
                    FormPurchaseInOrder fpo = new FormPurchaseInOrder(m_billNumber);
                    fpo.ShowDialog();
                }
                else if (m_billTypeName.IndexOf("销售") != -1)
                {
                    FormSaleOutOrder fsoo = new FormSaleOutOrder(m_billNumber);
                    fsoo.ShowDialog();
                }
                else
                {
                    bool isBankBill = BankCashsubLedger.getInctance().checkBillIsExist(m_billNumber);
                    if (m_isAccountReceivable)
                    {
                        FormReceivableOrder fro = new FormReceivableOrder(m_billNumber, isBankBill);
                        fro.ShowDialog();
                    }
                    else
                    {
                        FormPaymentOrder fpo = new FormPaymentOrder(m_billNumber, isBankBill);
                        fpo.ShowDialog();
                    }
                }
            }
        }
    }
}