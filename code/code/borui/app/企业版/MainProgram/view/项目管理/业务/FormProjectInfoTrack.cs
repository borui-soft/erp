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
    public partial class FormProjectInfoTrack : Form
    {
        public enum OrderType
        {
            // 设备总材料表
            DevMaterielInfo,
            EleMaterielInfo,
            EngMaterielInfo,
            ALL
        };

        private int m_dataGridRecordCount = 0;
        private OrderType m_orderType;
        private string m_billNumber = "";
        private string m_projectNum = "";
        private bool m_isSelectOrderNumber;

        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();

        public FormProjectInfoTrack(OrderType orderType, bool isSelectOrderNumber = false)
        {
            InitializeComponent();

            m_orderType = orderType;

            if (m_orderType == OrderType.DevMaterielInfo)
            {
                this.Text = "设备总材料表跟踪情况";
            }
            else if (m_orderType == OrderType.EleMaterielInfo)
            {
                this.Text = "电器总材料表跟踪情况";
            }
            else if (m_orderType == OrderType.EngMaterielInfo)
            {
                this.Text = "工程总材料表跟踪情况";
            }
            else if (m_orderType == OrderType.ALL)
            {
                this.Text = "项目整体情况跟踪情况";
            }

            m_isSelectOrderNumber = isSelectOrderNumber;
        }

        private void FormProjectInfoTrack_Load(object sender, EventArgs e)
        {
            if (m_orderType == OrderType.DevMaterielInfo || 
                m_orderType == OrderType.EleMaterielInfo || 
                m_orderType == OrderType.EngMaterielInfo)
            {
                m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dateGridViewExtend.addDataGridViewColumn("设备型号", 100);
                m_dateGridViewExtend.addDataGridViewColumn("制表日期", 80);
                m_dateGridViewExtend.addDataGridViewColumn("单据号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("项目编号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("生产编号", 120);
                m_dateGridViewExtend.addDataGridViewColumn("所属部件", 100);
                m_dateGridViewExtend.addDataGridViewColumn("制单员", 80);
                m_dateGridViewExtend.addDataGridViewColumn("设计", 80);
                m_dateGridViewExtend.addDataGridViewColumn("是否审核", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核员", 80);
                m_dateGridViewExtend.addDataGridViewColumn("审核日期", 80);
            }
            else if (m_orderType == OrderType.ALL)
            {
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

            if (m_orderType == OrderType.DevMaterielInfo 
                || m_orderType == OrderType.EleMaterielInfo 
                || m_orderType == OrderType.EngMaterielInfo)
            {
                int dataType = 1;
                if (m_orderType == OrderType.DevMaterielInfo)
                {
                    dataType = 1;
                }
                else if (m_orderType == OrderType.EleMaterielInfo)
                {
                    dataType = 2;
                }
                else if (m_orderType == OrderType.EngMaterielInfo)
                {
                    dataType = 3;
                }

                SortedDictionary<int, FormProjectMaterielTable> list = new SortedDictionary<int, FormProjectMaterielTable>();
                list = FormProject.getInctance().getAllPurchaseOrderInfo(dataType);

                m_dataGridRecordCount = list.Count;

                for (int index = 0; index < list.Count; index++)
                {
                    FormProjectMaterielTable record = new FormProjectMaterielTable();
                    record = (FormProjectMaterielTable)list[index];

                    if (m_filter.startDate == null || (record.makeDate.CompareTo(m_filter.startDate) >= 0 && record.makeDate.CompareTo(m_filter.endDate) <= 0))
                    {
                        ArrayList temp = new ArrayList();

                        temp.Add(record.pkey);
                        temp.Add(record.deviceMode);
                        temp.Add(record.makeDate);
                        temp.Add(record.billNumber);
                        temp.Add(record.projectNum);
                        temp.Add(record.makeNum);
                        temp.Add(record.deviceName);
                        temp.Add(record.makeOrderStaffName);
                        temp.Add(record.designStaffName);

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
            else if (m_orderType == OrderType.ALL)
            {
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
            if (m_orderType == OrderType.DevMaterielInfo || m_orderType == OrderType.EleMaterielInfo || m_orderType == OrderType.EngMaterielInfo)
            {
                int dataType = 1;
                if (m_orderType == OrderType.DevMaterielInfo)
                {
                    dataType = 1;
                }
                else if (m_orderType == OrderType.EleMaterielInfo)
                {
                    dataType = 2;
                }
                else if (m_orderType == OrderType.EngMaterielInfo)
                {
                    dataType = 3;
                }

                FormProjectMaterielOrder fpmo = new FormProjectMaterielOrder(dataType, m_billNumber);
                fpmo.ShowDialog();
                updateDataGridView();
            }
            else
            {
                MessageBoxExtend.messageWarning("暂时不支持的序时薄类型");
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
            //// 刷新按钮逻辑
            //if (m_orderType == OrderType.PurchaseOrder)
            //{
            //    PurchaseOrder.getInctance().refreshRecord();
            //}
            //else if (m_orderType == OrderType.PurchaseIn)
            //{
            //    PurchaseInOrder.getInctance().refreshRecord();
            //}
            //else if (m_orderType == OrderType.PurchaseInvoice)
            //{
            //}
            //else if (m_orderType == OrderType.PurchaseOrderExcute)
            //{
            //    PurchaseOrder.getInctance().refreshRecord();
            //}
            //else if (m_orderType == OrderType.PurchaseInOrderExcute)
            //{
            //    PurchaseInOrder.getInctance().refreshRecord();
            //}
            //else if (m_orderType == OrderType.StorageProductIn)
            //{
            //    // 仓存管理-产品入库
            //    MaterielInOrder.getInctance().refreshRecord();
            //}
            //else if (m_orderType == OrderType.StorageInCheck)
            //{
            //    // 仓存管理-盘盈入库
            //    MaterielInEarningsOrder.getInctance().refreshRecord();
            //}
            //else if (m_orderType == OrderType.StorageInOther)
            //{
            //    // 仓存管理-其他入库
            //    MaterielInOtherOrder.getInctance().refreshRecord();
            //}

            updateDataGridView();
        }
    }
}