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
using Excel = Microsoft.Office.Interop.Excel;

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
        private int m_orderType;
        private OrderType m_orderTypeEnum;
        private string m_billNumber = "";
        private int m_materielPKey = -1;
        private bool m_isSelectOrderNumber;

        private RowMergeView m_dateGridViewExtend = new RowMergeView();
        private FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();
        private FormProjectInfoTrackFilterValue m_projectInfoTrackFilter = new FormProjectInfoTrackFilterValue();
        private SortedDictionary<int, FormProjectMaterielTable> m_dataList = new SortedDictionary<int, FormProjectMaterielTable>();
        private SortedDictionary<int, DataGridViewColumnInfoStruct> m_columnsInfo = new SortedDictionary<int, DataGridViewColumnInfoStruct>();

        // m_materielProlist用于存放库存预占信息
        SortedDictionary<int, MaterielProOccupiedInfo> m_materielProlist = new SortedDictionary<int, MaterielProOccupiedInfo>();

        public FormProjectInfoTrack(OrderType orderType, bool isSelectOrderNumber = false)
        {
            InitializeComponent();
            m_orderTypeEnum = orderType;
            string orderTypeText = "";

            if (m_orderTypeEnum == OrderType.DevMaterielInfo)
            {
                this.Text = "设备总材料表跟踪情况";
                m_orderType = 1;
                orderTypeText = "设备总材料表";
            }
            else if (m_orderTypeEnum == OrderType.EleMaterielInfo)
            {
                this.Text = "电器总材料表跟踪情况";
                m_orderType = 2;
                orderTypeText = "电器总材料表";
            }
            else if (m_orderTypeEnum == OrderType.EngMaterielInfo)
            {
                this.Text = "工程总材料表跟踪情况";
                m_orderType = 3;
                orderTypeText = "工程总材料";
            }
            else if (m_orderTypeEnum == OrderType.ALL)
            {
                this.Text = "项目整体情况跟踪情况";
                orderTypeText = "项目总材料表";
                m_orderType = 4;
            }

            m_isSelectOrderNumber = isSelectOrderNumber;

            FormProjectInfoTrackFilter fssf = new FormProjectInfoTrackFilter(false);

            if (fssf.ShowDialog() == DialogResult.OK)
            {
                m_projectInfoTrackFilter = fssf.getFilterUIValue();
            }

            this.toolStripStatusLabel.Text = "材料表类型：" + orderTypeText + ",     项目编号：" + m_projectInfoTrackFilter.projectNum + ",     单据类型:" + m_projectInfoTrackFilter.allReview;
        }

        private void FormProjectInfoTrack_Load(object sender, EventArgs e)
        {
            addDataGridViewColumn("单据编号", 135);
            addDataGridViewColumn("设备型号", 100);
            addDataGridViewColumn("所属部件", 100);

            addDataGridViewColumn("ID", 60, false);
            addDataGridViewColumn("物料名称", 100);
            addDataGridViewColumn("物料编码", 60);
            addDataGridViewColumn("型号", 60);
            addDataGridViewColumn("参数", 60);
            addDataGridViewColumn("数量", 60);

            addDataGridViewColumn("实际库存", 100);
            addDataGridViewColumn("预占库存", 100);
            addDataGridViewColumn("可用库存", 100);

            addDataGridViewColumn("转采购申请\n数量", 100);
            addDataGridViewColumn("采购(订单)\n数量", 100);
            addDataGridViewColumn("采购入库\n数量", 100);
            addDataGridViewColumn("生产领料\n数量", 100);

            initDataGridViewColumn();

            this.projectRowMergeView.ColumnHeadersHeight = 40;
            this.projectRowMergeView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.projectRowMergeView.MergeColumnNames.Add("单据编号");
            this.projectRowMergeView.AddSpanHeader(4, 5, "物料需求");
            this.projectRowMergeView.AddSpanHeader(9, 3, "库存情况");

            getMaterielProOccupiedList();
            updateDataGridView();
        }

        private void getMaterielProOccupiedList()
        {
            m_materielProlist = MaterielProOccupiedOrderDetails.getInctance().getMaterielProOccupiedList();
        }

        private void updateDataGridView()
        {
            SortedDictionary<int, ProjectManagerDetailsTable> listDetails = new SortedDictionary<int, ProjectManagerDetailsTable>();
            SortedDictionary<int, ArrayList> projectInfoList = new SortedDictionary<int, ArrayList>();

            SortedDictionary<int, FormProjectMaterielTable> list = new SortedDictionary<int, FormProjectMaterielTable>();
            list = FormProject.getInctance().getAllPurchaseOrderInfo(
                m_projectInfoTrackFilter.projectNum, m_projectInfoTrackFilter.allReview, m_orderType);;

            for (int index = 0; index < list.Count; index++)
            {
                FormProjectMaterielTable record = new FormProjectMaterielTable();
                record = (FormProjectMaterielTable)list[index];

                listDetails.Clear();
                listDetails = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(record.billNumber);

                for (int index2 = 0; index2 < listDetails.Count; index2++)
                {
                    ProjectManagerDetailsTable tmp = new ProjectManagerDetailsTable();
                    tmp = (ProjectManagerDetailsTable)listDetails[index2];

                    ArrayList temp = new ArrayList();

                    temp.Add(record.billNumber);
                    temp.Add(record.deviceMode);
                    temp.Add(record.deviceName);

                    temp.Add(tmp.materielID);
                    temp.Add(tmp.materielName);
                    temp.Add(tmp.num);
                    temp.Add(tmp.materielModel);
                    temp.Add(tmp.materielParameter);
                    temp.Add(tmp.value);

                    // 得到实际库存
                    InitMaterielTable MaterielCountdata = InitMateriel.getInctance().getMaterielInfoFromMaterielID(tmp.pkey);
                    temp.Add(MaterielCountdata.value);

                    // 库存预占情况
                    if (m_materielProlist.ContainsKey(tmp.pkey))
                    {
                        MaterielProOccupiedInfo proOccupiedRecord = new MaterielProOccupiedInfo();
                        proOccupiedRecord = (MaterielProOccupiedInfo)m_materielProlist[tmp.pkey];

                        temp.Add(proOccupiedRecord.sum);
                        temp.Add(MaterielCountdata.value - proOccupiedRecord.sum);
                    }
                    else
                    {
                        temp.Add(0);
                        temp.Add(MaterielCountdata.value);
                    }

                    // 转采购申请单数量
                    SortedDictionary<int, PurchaseApplyOrderTable> listApplyList = new SortedDictionary<int, PurchaseApplyOrderTable>();
                    listApplyList = PurchaseApplyOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_projectInfoTrackFilter.projectNum);

                    double appylyCount = 0;
                    for (int indexApplyList = 0; indexApplyList < listApplyList.Count; indexApplyList++)
                    {
                        PurchaseApplyOrderTable recordlistApply = new PurchaseApplyOrderTable();
                        recordlistApply = (PurchaseApplyOrderTable)listApplyList[indexApplyList];

                        appylyCount += PurchaseApplyOrderDetails.getInctance().getPurchaseValueFromBillNumber(recordlistApply.billNumber, tmp.materielID);
                    }
                    temp.Add(appylyCount);

                    // 采购订单数量
                    SortedDictionary<int, PurchaseOrderTable> listOrderList = new SortedDictionary<int, PurchaseOrderTable>();
                    listOrderList = PurchaseOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_projectInfoTrackFilter.projectNum);

                    double orderCount = 0;
                    for (int indexOrderList = 0; indexOrderList < listOrderList.Count; indexOrderList++)
                    {
                        PurchaseOrderTable recordOrder = new PurchaseOrderTable();
                        recordOrder = (PurchaseOrderTable)listOrderList[indexOrderList];

                        orderCount += PurchaseOrderDetails.getInctance().getPurchaseValueFromBillNumber(recordOrder.billNumber, tmp.materielID);
                    }
                    temp.Add(orderCount);

                    // 采购入库数量
                    SortedDictionary<int, PurchaseInOrderTable> purchaseInOrderList = new SortedDictionary<int, PurchaseInOrderTable>();
                    purchaseInOrderList = PurchaseInOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_projectInfoTrackFilter.projectNum);

                    double purchaseInOrderValueCount = 0;
                    for (int indexOrderList = 0; indexOrderList < purchaseInOrderList.Count; indexOrderList++)
                    {
                        PurchaseInOrderTable recordOrder = new PurchaseInOrderTable();
                        recordOrder = (PurchaseInOrderTable)purchaseInOrderList[indexOrderList];

                        purchaseInOrderValueCount += PurchaseInOrderDetails.getInctance().getPurchaseValueFromBillNumber(recordOrder.billNumber, tmp.materielID);
                    }
                    temp.Add(purchaseInOrderValueCount);

                    // 生产领料数量
                    SortedDictionary<int, MaterielOutOrderTable> materielOutOrderList = new SortedDictionary<int, MaterielOutOrderTable>();
                    materielOutOrderList = MaterielOutOrder.getInctance().getAllPurchaseOrderInfoFromProjectNum(m_projectInfoTrackFilter.projectNum);

                    double materielOutOrderValueCount = 0;
                    for (int indexOrderList = 0; indexOrderList < materielOutOrderList.Count; indexOrderList++)
                    {
                        MaterielOutOrderTable recordOrder = new MaterielOutOrderTable();
                        recordOrder = (MaterielOutOrderTable)materielOutOrderList[indexOrderList];

                        materielOutOrderValueCount += MaterielOutOrderDetails.getInctance().getPurchaseValueFromBillNumber(recordOrder.billNumber, tmp.materielID);
                    }
                    temp.Add(materielOutOrderValueCount);

                    projectInfoList.Add(projectInfoList.Count, temp);
                }
            }

            initDataGridViewData(projectInfoList, 3);

            m_dataGridRecordCount = projectInfoList.Count;
        }

        private void billDetail_Click(object sender, EventArgs e)
        {
            if (m_billNumber.Length > 0)
            {
                checkAccountBillDetaile();
            }
            else 
            {
                MessageBoxExtend.messageWarning("数据为空，无详细数据!");
            }
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
                    for (int i = 0; i < this.projectRowMergeView.RowCount; i++)
                    {
                        for (int j = 0; j < projectRowMergeView.ColumnCount; j++)
                        {
                            if (projectRowMergeView.Rows[i].Cells[j].Selected)
                            {
                                projectRowMergeView.Rows[i].Selected = true;
                                m_billNumber = projectRowMergeView.Rows[i].Cells[3].Value.ToString();
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
                    for (int i = 0; i < this.projectRowMergeView.RowCount; i++)
                    {
                        for (int j = 0; j < projectRowMergeView.ColumnCount; j++)
                        {
                            if (projectRowMergeView.Rows[i].Cells[j].Selected)
                            {
                                projectRowMergeView.Rows[i].Selected = true;
                                m_billNumber = projectRowMergeView.Rows[i].Cells[0].Value.ToString();
                                m_materielPKey = Convert.ToInt32(projectRowMergeView.Rows[i].Cells[3].Value.ToString());
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
            FormProjectMaterielOrder fpmo = new FormProjectMaterielOrder(m_orderType, m_billNumber);
            fpmo.ShowDialog();
        }

        public string getSelectOrderNumber()
        {
            return m_billNumber;
        }

        public void setDataFilter(FormStorageSequenceFilterValue filter)
        {
            m_filter = filter;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            getMaterielProOccupiedList();
            updateDataGridView();
        }

        private void rowMergeView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            projectRowMergeView.Rows[e.RowIndex].Selected = true;

            m_billNumber = projectRowMergeView.Rows[e.RowIndex].Cells[0].Value.ToString();
            m_materielPKey = Convert.ToInt32(projectRowMergeView.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        #region DataGridView自定义方法

        public void addDataGridViewColumn(string headerText, int Width, bool isVisiable = true)
        {
            DataGridViewColumnInfoStruct column = new DataGridViewColumnInfoStruct();

            column.headerText = headerText;
            column.Width = Width;
            column.isVisiable = isVisiable;

            m_columnsInfo.Add(m_columnsInfo.Count, column);
        }

        private void setDataGridViewStyle()
        {
            this.projectRowMergeView.BackgroundColor = System.Drawing.Color.White;

            // 灰色网格线
            this.projectRowMergeView.GridColor = System.Drawing.Color.Silver;

            // 数据为只读
            this.projectRowMergeView.ReadOnly = true;

            // 不能同时选中多行
            this.projectRowMergeView.MultiSelect = false;

            // 禁止用户往DataGridView尾部添加新行
            this.projectRowMergeView.AllowUserToAddRows = false;

            // 行头是否显示
            this.projectRowMergeView.RowHeadersVisible = false;

            // 禁止用户用鼠标拖动DataGridView行高
            this.projectRowMergeView.EnableHeadersVisualStyles = false;
            this.projectRowMergeView.AllowUserToResizeRows = false;
        }

        public void initDataGridViewColumn()
        {
            this.projectRowMergeView.ColumnCount = m_columnsInfo.Count;
            setDataGridViewStyle();

            for (int i = 0; i < m_columnsInfo.Count; i++)
            {
                DataGridViewColumnInfoStruct column = new DataGridViewColumnInfoStruct();
                column = (DataGridViewColumnInfoStruct)m_columnsInfo[i];

                this.projectRowMergeView.Columns[i].Name = column.headerText;
                this.projectRowMergeView.Columns[i].Width = column.Width;
                this.projectRowMergeView.Columns[i].HeaderText = column.headerText;
                this.projectRowMergeView.Columns[i].Visible = column.isVisiable;
            }
        }

        public void initDataGridViewData(SortedDictionary<int, ArrayList> data, int columnFrozenCount = 0)
        {
            if (data.Count > 0)
            {
                this.projectRowMergeView.RowCount = data.Count;
                for (int i = 0; i < data.Count; i++)
                {
                    ArrayList temp = new ArrayList();
                    temp = (ArrayList)data[i];

                    for (int j = 0; j < temp.Count; j++)
                    {
                        this.projectRowMergeView.Rows[i].Cells[j].Value = temp[j];
                        this.projectRowMergeView.Rows[i].Height = 18;
                    }
                }

                if (columnFrozenCount != 0)
                {
                    this.projectRowMergeView.Columns[columnFrozenCount].Frozen = true;
                }
            }
            else
            {
                this.projectRowMergeView.Rows.Clear();
            }
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
                    try
                    {
                        Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                        Excel.Worksheet excelSheet = (Excel.Worksheet)excelWorkbook.Sheets["Sheet1"];

                        if (excelApp == null)
                        {
                            MessageBoxExtend.messageWarning("数据导出失败, Excel安装进程存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                            return;
                        }

                        if (excelWorkbook == null)
                        {
                            MessageBoxExtend.messageWarning("数据导出失败, Excel工作表存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                            return;
                        }

                        if (excelSheet == null)
                        {
                            MessageBoxExtend.messageWarning("数据导出失败, Excel sheet表存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                            return;
                        }

                        excelApp.Visible = false;

                        try
                        {
                            //向Excel中写入表格的表头
                            int displayColumnsCount = 1;
                            for (int i = 0; i <= this.projectRowMergeView.ColumnCount - 1; i++)
                            {
                                if (projectRowMergeView.Columns[i].Visible)
                                {
                                    excelApp.Cells[1, displayColumnsCount] = projectRowMergeView.Columns[i].HeaderText.Trim();
                                    ((Excel.Range)excelSheet.Columns[displayColumnsCount, System.Type.Missing]).ColumnWidth = projectRowMergeView.Columns[i].Width * 1.0 / 8;
                                    displayColumnsCount++;
                                }
                            }

                            for (int row = 0; row < projectRowMergeView.RowCount; row++)
                            {
                                displayColumnsCount = 1;
                                for (int col = 0; col < projectRowMergeView.ColumnCount; col++)
                                {
                                    if (projectRowMergeView.Rows[row].Cells[col].Visible)
                                    {
                                        excelApp.Cells[row + 2, displayColumnsCount] = projectRowMergeView.Rows[row].Cells[col].Value.ToString().Trim();
                                        displayColumnsCount++;
                                    }
                                }
                            }

                            excelWorkbook.SaveAs(saveFileDialog1.FileName);
                        }
                        catch (Exception error)
                        {
                            MessageBoxExtend.messageWarning(error.Message);
                            return;
                        }
                        finally
                        {
                            //关闭Excel应用    
                            if (excelWorkbook != null)
                            {
                                excelWorkbook.Close();
                            }

                            if (excelApp.Workbooks != null)
                            {
                                excelApp.Workbooks.Close();
                            }

                            if (excelApp != null)
                            {
                                excelApp.Quit();
                            }

                            excelWorkbook = null;
                            excelApp = null;
                        }

                        MessageBoxExtend.messageOK("导出成功\n\n" + saveFileDialog1.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxExtend.messageError(ex.Message);
                    }
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据为空，无数据可导出!");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintDataGridView.Print_DataGridView(projectRowMergeView);
        }

        #endregion

        private void projectRowMergeView_DoubleClick(object sender, EventArgs e)
        {
            billDetail_Click(null, null);
        }

        private void projectRowMergeView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_dataGridRecordCount))
            {
                projectRowMergeView.Rows[e.RowIndex].Selected = true;
                m_billNumber = projectRowMergeView.Rows[e.RowIndex].Cells[0].Value.ToString();
                m_materielPKey = Convert.ToInt32(projectRowMergeView.Rows[e.RowIndex].Cells[3].Value.ToString());

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void ToolStripMenuItemToApply_Click(object sender, EventArgs e)
        {
            FormPurchaseApply fpa = new FormPurchaseApply("", m_projectInfoTrackFilter.projectNum);
            fpa.ShowDialog();
        }

        private void ToolStripMenuItemPurchaseApplyOrderInfo_Click(object sender, EventArgs e)
        {
            FormProjectOrderDetail fpod = new FormProjectOrderDetail(FormProjectOrderDetail.OrderType.PurchaseApplyOrder, m_projectInfoTrackFilter.projectNum, m_materielPKey);
            fpod.ShowDialog();
        }

        private void ToolStripMenuItemPurchaseOrderInfo_Click(object sender, EventArgs e)
        {
            FormProjectOrderDetail fpod = new FormProjectOrderDetail(FormProjectOrderDetail.OrderType.PurchaseOrder, m_projectInfoTrackFilter.projectNum, m_materielPKey);
            fpod.ShowDialog();
        }

        private void toolStripMenuItemPurchaseInOrderInfo_Click(object sender, EventArgs e)
        {
            FormProjectOrderDetail fpod = new FormProjectOrderDetail(FormProjectOrderDetail.OrderType.PurchaseIn, m_projectInfoTrackFilter.projectNum, m_materielPKey);
            fpod.ShowDialog();
        }

        private void ToolStripMenuItemMaterielOutOrderInfo_Click(object sender, EventArgs e)
        {
            FormProjectOrderDetail fpod = new FormProjectOrderDetail(FormProjectOrderDetail.OrderType.StorageMaterielOut, m_projectInfoTrackFilter.projectNum, m_materielPKey);
            fpod.ShowDialog();
        }

        // 转生产领料
        private void toolStripMenuItemToProduce_Click(object sender, EventArgs e)
        {
            FormMaterielOutOrder fmoo = new FormMaterielOutOrder("", m_projectInfoTrackFilter.projectNum);
            fmoo.ShowDialog();
        }

        // 转预占库存
        private void toolStripMenuItemProOccupied_Click(object sender, EventArgs e)
        {
            // 存放需要转预占的数据
            SortedDictionary<int, ArrayList> proInfoList = new SortedDictionary<int, ArrayList>();

            // 根据单据,得到单据详细信息
            SortedDictionary<int, ProjectManagerDetailsTable> listDetails = new SortedDictionary<int, ProjectManagerDetailsTable>();
            listDetails = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            for (int index = 0; index < listDetails.Count; index++)
            {
                ArrayList record = new ArrayList();

                ProjectManagerDetailsTable tmp = new ProjectManagerDetailsTable();
                tmp = (ProjectManagerDetailsTable)listDetails[index];

                record.Add(tmp.materielID);
                record.Add(tmp.materielName);
                record.Add(tmp.value);

                // 筛选出，可以转为预占库存的数据(如果预占数量小于项目所需数量，则可以形成预占)
                if (m_materielProlist.ContainsKey(tmp.pkey))
                {
                    MaterielProOccupiedInfo proOccupiedRecord = new MaterielProOccupiedInfo();
                    proOccupiedRecord = (MaterielProOccupiedInfo)m_materielProlist[tmp.pkey];

                    record.Add(proOccupiedRecord.sum);
                }
                else 
                {
                    record.Add(0);
                }


                proInfoList.Add(proInfoList.Count, record);
            }

        }
    }
}