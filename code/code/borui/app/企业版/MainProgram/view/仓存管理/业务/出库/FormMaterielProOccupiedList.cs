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
    public partial class FormMaterielProOccupiedList : Form
    {
        public enum OrderType
        {
            // 仓存管理-库存预占
            MaterielProOccupied
        };

        private int m_dataGridRecordCount = 0;
        private OrderType m_orderType;
        private string m_pkey = "";
        private string m_billNumber = "";
        private string m_currentSelectRecordName = "";
        private bool m_isSelectOrderNumber;

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();

        public FormMaterielProOccupiedList(OrderType orderType, bool isSelectOrderNumber = false)
        {
            InitializeComponent();

            m_orderType = orderType;
            m_isSelectOrderNumber = isSelectOrderNumber;

            this.Text = "库存预占单序时薄";
        }

        private void FormMaterielProOccupiedList_Load(object sender, EventArgs e)
        {
            if (m_orderType == OrderType.MaterielProOccupied)
            {
                // 其他出库单序时薄
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("名称", 120);
                m_dateGridViewExtend.addDataGridViewColumn("型号", 70);
                m_dateGridViewExtend.addDataGridViewColumn("申请人", 70);
                m_dateGridViewExtend.addDataGridViewColumn("申请日期", 80);
                m_dateGridViewExtend.addDataGridViewColumn("申请数量", 80);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 140);
                m_dateGridViewExtend.addDataGridViewColumn("是否已审核", 140);
                m_dateGridViewExtend.addDataGridViewColumn("审核人", 100);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 100);
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
            
            if (m_orderType == OrderType.MaterielProOccupied)
            {
                SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> list = new SortedDictionary<int, MaterielProOccupiedOrderDetailsTable>();
                list = MaterielProOccupiedOrderDetails.getInctance().getAllMaterielProOccupiedOrderInfo();

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    MaterielProOccupiedOrderDetailsTable record = new MaterielProOccupiedOrderDetailsTable();
                    record = (MaterielProOccupiedOrderDetailsTable)list[index];

                    MaterielProOccupiedOrderTable orderInof = MaterielProOccupiedOrder.getInctance().getMaterielProOccupiedOrderInfoFromBillNumber(record.billNumber);

                    if (orderInof != null)
                    {
                        if (record.isCancel == "1")
                        {
                            continue;
                        }

                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.materielName);
                        temp.Add(record.materielModel);

                        temp.Add(orderInof.applyStaffName);
                        temp.Add(orderInof.tradingDate);
                        temp.Add(record.value);

                        temp.Add(orderInof.billNumber);

                        if (orderInof.isReview.Length > 0)
                        {
                            temp.Add("已审核");
                            temp.Add(orderInof.orderrReviewName);
                            temp.Add(orderInof.reviewDate);
                        }
                        else
                        {
                            temp.Add("未审核");
                            temp.Add("");
                            temp.Add("");
                        }

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
            // 此处需要添加导出DataGridViewer数据到Excel的功能
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
                                m_pkey = dataGridViewList.Rows[i].Cells[0].Value.ToString();
                                m_currentSelectRecordName = dataGridViewList.Rows[i].Cells[1].Value.ToString();
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
                                m_pkey = dataGridViewList.Rows[i].Cells[0].Value.ToString();
                                m_currentSelectRecordName = dataGridViewList.Rows[i].Cells[1].Value.ToString();
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
                if (m_orderType == OrderType.MaterielProOccupied)
                {
                    FormMaterielProOccupied fmoo = new FormMaterielProOccupied(m_billNumber);
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

        private string getWindowText(OrderType orderType)
        {
            string winText = "";

            if (m_orderType == OrderType.MaterielProOccupied)
            {
                winText = "库存预占单序时薄";
            }
            
            return winText;
        }

        public void setDataFilter(FormStorageSequenceFilterValue filter)
        {
            m_filter = filter;
        }

        private void billDelProOccupied_Click(object sender, EventArgs e)
        {
            if (m_currentSelectRecordName.Length == 0)
            {
                MessageBoxExtend.messageWarning("物料名称不能为空!");
                return;
            }

            if(MessageBoxExtend.messageQuestion("确认要撤销对" + m_currentSelectRecordName + "的库存占用吗？"))
            {
                MaterielProOccupiedOrderDetails.getInctance().delProOccupied(m_pkey);
            }

            updateDataGridView();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            MaterielProOccupiedOrder.getInctance().refreshRecord();
            MaterielProOccupiedOrderDetails.getInctance().refreshRecord();

            updateDataGridView();
        }
    }
}