using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Diagnostics;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormMaterielProOccupied : Form
    {
        private int m_applyStaffPkey = -1;
        private string m_billNumber = "";
        private readonly int BillTypeNumber = 17;
        private int DateGridVeiwListDataListRowCount = FormMain.DATA_GRID_VIEW_DEFAULT_ROW_COUNT;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isInit = false;
        private bool m_isRedBill = false;
        private bool m_isSaveSuccess = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        DataGridViewExtend m_dateGridVeiwListDataCount = new DataGridViewExtend();
        MaterielProOccupiedOrderTable m_materieOutOrder = new MaterielProOccupiedOrderTable();

        // 根据项目跟踪状况，直接形成预占数据
        public SortedDictionary<int, ArrayList> m_proInfoList = new SortedDictionary<int, ArrayList>();

        private enum DataGridColumnName
        {
            RowNum,
            MatetielNumber,
            MatetielName,
            Model,
            Unit,
            Value,
            Price,
            Turnover,
            Note
        };

        public FormMaterielProOccupied(string billNumber = "", SortedDictionary<int, ArrayList> proInfoList = null)
        {
            InitializeComponent();
            m_billNumber = billNumber;

            if (proInfoList != null)
            {
                m_proInfoList = proInfoList;
            }
        }

        private void FormMaterielProOccupied_Load(object sender, EventArgs e)
        {
            if (m_proInfoList.Count > 0)
            {
                DateGridVeiwListDataListRowCount = m_proInfoList.Count;

                // DataGridView初始化
                dataGridViewInit();

                readProInfoListToUI();
                return;
            }
            else
            {
                // DataGridView初始化
                dataGridViewInit();
            }

            if (m_billNumber.Length == 0)
            {
                // 单据号
                this.labelBillNumber.Text = BillNumber.getInctance().getNewBillNumber(BillTypeNumber, DateTime.Now.ToString("yyyy-MM-dd"));
                
                // 制单人初始化
                this.labelMakeBillStaff.Visible = true;
                this.labelMakeBillStaff.Text = DbPublic.getInctance().getCurrentLoginUserName();
            }
            else
            {
                readBillInfoToUI();
            }
        }

        private void dataGridViewInit()
        {
            // 物料资料初始化
            m_dateGridVeiwListDataList.addDataGridViewColumn("行号", 55, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\\编码(*)", 100, true, false);

            if (DateGridVeiwListDataListRowCount > 12)
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 144, true, true);
            }
            else
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 161, true, true);
            }

            m_dateGridVeiwListDataList.addDataGridViewColumn("型号", 83, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("基本\n单位", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("单价", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("金额", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("备注", 203, true, false);

            m_dateGridVeiwListDataList.initDataGridViewColumn(this.dataGridViewDataList);
            m_dateGridVeiwListDataList.initDataGridViewData(DateGridVeiwListDataListRowCount);

            // 合计行DataGridView初始化
            SortedDictionary<int, DataGridViewColumnInfoStruct> columnsInfo = m_dateGridVeiwListDataList.getDataGridViewColumns();
            for (int i = 0; i < columnsInfo.Count; i++)
            {
                DataGridViewColumnInfoStruct column = new DataGridViewColumnInfoStruct();
                column = (DataGridViewColumnInfoStruct)columnsInfo[i];
                m_dateGridVeiwListDataCount.addDataGridViewColumn(column.headerText, column.Width, column.isVisiable);
            }

            SortedDictionary<int, ArrayList> dataList = new SortedDictionary<int, ArrayList>();

            ArrayList record = new ArrayList();
            record.Add("合计");
            dataList.Add(dataList.Count, record);

            m_dateGridVeiwListDataCount.initDataGridViewColumn(this.dataGridViewDataCount);
            m_dateGridVeiwListDataCount.initDataGridViewData(dataList);

            this.dataGridViewDataCount.ColumnHeadersVisible = false;
            this.dataGridViewDataCount.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightSkyBlue;

            // 初始化完毕
            m_isInit = true;
        }

        #region 供应日期
        private void panelDateTime_Click(object sender, EventArgs e)
        {
            if (m_materieOutOrder.isReview == "1")
            {
                return;
            }

            if (!this.dateTimePickerTradingDate.Visible)
            {
                this.panelTradingDate.Visible = false;
                this.labelTradingDate.Visible = false;
                this.dateTimePickerTradingDate.Visible = true;
                this.dateTimePickerTradingDate.Focus();
            }
        }
        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            this.panelTradingDate.Visible = true;

            this.labelTradingDate.Visible = true;

            this.dateTimePickerTradingDate.Visible = false;
            this.labelTradingDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 用途
        private void panelSummary_Click(object sender, EventArgs e)
        {
            if (m_materieOutOrder.isReview == "1")
            {
                return;
            }
            this.labelSummary.Visible = false;
            this.textBoxSummary.Visible = true;
            this.textBoxSummary.Focus();
        }

        private void textBoxSummary_Leave(object sender, EventArgs e)
        {
            this.textBoxSummary.Visible = false;
            this.labelSummary.Visible = true;
            this.labelSummary.Text = this.textBoxSummary.Text;
            //this.textBoxSummary.Text = "";
        }
        #endregion

        #region 保管员
        private void panelSave_Click(object sender, EventArgs e)
        {
            if (!this.textBoxSave.Visible)
            {
                this.labelSave.Visible = false;
                this.textBoxSave.Visible = true;
                this.textBoxSave.Focus();
            }
        }

        private void panelSave_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxSave.Visible)
            {
                this.labelSave.Visible = false;
                this.textBoxSave.Visible = true;
            }
            else
            {
                FormBaseStaff fbs = new FormBaseStaff(true);
                fbs.ShowDialog();

                m_applyStaffPkey = fbs.getSelectRecordPkey();
                StaffTable record = Staff.getInctance().getStaffInfoFromPkey(m_applyStaffPkey);
                this.textBoxSave.Text = record.name;
                this.textBoxSave.Visible = true;
            }
        }

        private void textBoxSave_Leave(object sender, EventArgs e)
        {
            this.textBoxSave.Visible = false;
            this.labelSave.Text = this.textBoxSave.Text.ToString();
            this.labelSave.Visible = this.textBoxSave.Text.Length > 0;
        }
        #endregion

        private void save_Click(object sender, EventArgs e)
        {
            m_isSaveSuccess = false;

            if ((sender.ToString() == "保存" || sender.ToString() == "审核") &&
                MaterielProOccupiedOrder.getInctance().checkBillIsReview(this.labelBillNumber.Text.ToString()))
            {
                MessageBoxExtend.messageWarning("单据已被审核，所有数据无法进行更改，无法重复保存或审核\r\n请重新登录或手动刷新后查看单据详情");
                return;
            }

            this.ActiveControl = this.toolStrip1;

            // 得到详细的销售信息
            ArrayList dataList = getMaterielProOccupiedDetailsValue();

            if (dataList.Count > 0)
            {
                // 销售订单表头和表尾信息
                MaterielProOccupiedOrderTable record = getMaterielProOccupiedOrderValue();
                if (orderInfoIsFull(record) && orderDetailsIsFull(dataList))
                {
                    MaterielProOccupiedOrder.getInctance().insert(record, false);
                    MaterielProOccupiedOrderDetails.getInctance().insert(dataList);
                    BillNumber.getInctance().inserBillNumber(BillTypeNumber, this.labelTradingDate.Text, this.labelBillNumber.Text.ToString());

                    m_isSaveSuccess = true;

                    if (m_billNumber.Length == 0)
                    {
                        MessageBoxExtend.messageOK("数据保存成功");
                    }

                    this.Close();
                }
            }
            else 
            {
                MessageBoxExtend.messageWarning("此单据不包含任何交易信息，单据保存失败.");
            }
        }

        private MaterielProOccupiedOrderTable getMaterielProOccupiedOrderValue()
        {
            MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();

            record.tradingDate = this.labelTradingDate.Text;
            record.billNumber = this.labelBillNumber.Text;
            record.srcOrderNum = this.labelSummary.Text;

            record.sumValue = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Value].Value.ToString();
            record.sumMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Turnover].Value.ToString();

            record.applyStaffId = m_applyStaffPkey;

            if (m_billNumber.Length == 0)
            {
                record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();
            }
            else 
            {
                record.makeOrderStaff = m_materieOutOrder.makeOrderStaff;
            }

            return record;
        }

        private ArrayList getMaterielProOccupiedDetailsValue()
        {
            ArrayList list = new ArrayList();

            for (int rowIndex = 0; rowIndex < DateGridVeiwListDataListRowCount; rowIndex++)
            {
                if (this.dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }
                else 
                {
                    MaterielProOccupiedOrderDetailsTable record = new MaterielProOccupiedOrderDetailsTable();

                    record.billNumber = this.labelBillNumber.Text;
                    record.rowNumber = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString();
                    record.materielID = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                    record.price = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                    record.value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    record.note = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Note].Value.ToString();

                    list.Add(record);
                }
            }

            return list;
        }

        private bool orderInfoIsFull(MaterielProOccupiedOrderTable record)
        {
            if (record.tradingDate.Length == 0)
            {
                MessageBoxExtend.messageWarning("日期不能为空，单据保存失败");
                return false;
            }

            if (record.srcOrderNum.Length == 0)
            {
                MessageBoxExtend.messageWarning("用途信息不能为空，单据保存失败");
                return false;
            }

            if (record.applyStaffId == -1)
            {
                MessageBoxExtend.messageWarning("申请人信息不完整，单据保存失败");
                return false;
            }

            return true;
        }

        private bool orderDetailsIsFull(ArrayList list)
        {
            bool isRet = true;

            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                MaterielProOccupiedOrderDetailsTable record = new MaterielProOccupiedOrderDetailsTable();
                record = (MaterielProOccupiedOrderDetailsTable)list[rowIndex];

                if (record.price == 0)
                {
                    MessageBoxExtend.messageWarning("第[" + record.rowNumber + "]信息中物料单价不能为空");
                    isRet = false;
                    break;
                }

                if (record.value == 0)
                {
                    MessageBoxExtend.messageWarning("第[" + record.rowNumber + "]信息中物料数量不能为空");
                    isRet = false;
                    break;
                }
            }

            return isRet;
        }

        private void toolStripButtonReview_Click(object sender, EventArgs e)
        {
            try
            {
                save_Click(sender, e);

                if (m_isSaveSuccess)
                {
                    MaterielProOccupiedOrder.getInctance().billReview(m_billNumber, m_isRedBill);
                }
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError(exp.ToString());
            }
        }

        private void printDisplay_Click(object sender, EventArgs e)
        {
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, true);
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y);
        }

        private void selectSourceOrder_Click(object sender, EventArgs e)
        {
            if (m_rowIndex != -1 && m_columnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                FormBaseMateriel fbm = new FormBaseMateriel(true);
                fbm.ShowDialog();
                this.dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].Value = Convert.ToString(fbm.getSelectRecordPkey());
                this.dataGridViewDataList.CurrentCell = this.dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value];
            }
        }

        private void calculator_Click(object sender, EventArgs e)
        {
            Process.Start("Calc");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewDataList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                //验证DataGridView是否又空的行
                int nullRowNumber = m_dateGridVeiwListDataList.getExistNullRow(e.RowIndex);

                if (nullRowNumber != -1)
                {
                    MessageBoxExtend.messageWarning("行号[" + Convert.ToString(nullRowNumber + 1) + "]数据为空，请现在空行中输入");
                    dataGridViewDataList.CurrentCell = this.dataGridViewDataList.Rows[nullRowNumber].Cells[(int)DataGridColumnName.MatetielNumber];

                    m_rowIndex = nullRowNumber;
                    m_columnIndex = e.ColumnIndex;

                    return;
                }
            }

            m_rowIndex = e.RowIndex;
            m_columnIndex = e.ColumnIndex;
        }

        private void dataGridViewDataList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (m_proInfoList.Count > 0 && e.ColumnIndex == (int)DataGridColumnName.Value)
            {
                // 当value被改变后，
                if (Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].Value.ToString()) >
                    getMaxValue(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString()))
                {
                    MessageBoxExtend.messageWarning("物料数量以超过允许的最大值" + getMaxValue(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString()) + 
                        ", 已强制修改为默认最大值");
                    dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].Value = getMaxValue(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                }
            }

            if (e.ColumnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                // 当用户选择的相应物料后，自动对DataGridView行某些值进行赋值
                if (dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString().Length > 0)
                {
                    setMatetielInfoToDataGridView(dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString(), m_rowIndex);
                }
            }
            else if (e.ColumnIndex == (int)DataGridColumnName.Price || e.ColumnIndex == (int)DataGridColumnName.Value)
            {
                // 当单价和数量有变化时，自动计算物料金额
                setTurnoverInfoDataGridView(m_rowIndex);
            }
        }
        
        private void setMatetielInfoToDataGridView(string id, int rowIndex)
        {
            /* 如果是物料编码列，需要判断该物料编码是否存在
            * 如果存在读取相应的值填充DataGridView中对应的其他列，如果不存在该物料编码，则清空该行
            * */
            //使用这个输入的值，匹配物料编号
            double pkey = 0;
            MaterielTable record = Materiel.getInctance().getMaterielInfoFromNum(Convert.ToString(id));

            if (record == null || id.ToLower() != record.num.ToLower() || record.pkey == 0)
            {
                try
                {
                    //使用这个输入的值，匹配物料key
                    pkey = Convert.ToDouble(id.ToString());

                    record = Materiel.getInctance().getMaterielInfoFromPkey((int)pkey);

                    if (pkey != record.pkey || record.pkey == 0)
                    {
                        MessageBoxExtend.messageWarning("[" + dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString() +
                            "]不存在，请重新输入或选择");
                        m_dateGridVeiwListDataList.clearDataGridViewRow(m_rowIndex);

                        return;
                    }
                }
                catch
                {
                    MessageBoxExtend.messageWarning("[" + dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString() +
                        "]不存在，请重新输入或选择");
                    m_dateGridVeiwListDataList.clearDataGridViewRow(m_rowIndex);

                    return;
                }
            }

            InitMaterielTable MaterielCountdata = InitMateriel.getInctance().getMaterielInfoFromMaterielID((int)pkey);

            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.pkey;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.name;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.model;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value =
            AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitSale);
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = Convert.ToString(MaterielCountdata.price);
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = "0";
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = "0";
        }

        private void setTurnoverInfoDataGridView(int rowIndex)
        {
            if (dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString().Length > 0 &&
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString().Length > 0)
            {
                double price = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                double value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                double turnover = price * value;

                // 金额信息保留2位小数儿
                turnover = (double)(Math.Round(turnover * 100)) / 100;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = Convert.ToString(turnover);
            }
        }

        private void dataGridViewDataList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; // 绑定到事件
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_columnIndex != (int)DataGridColumnName.Note && m_columnIndex != (int)DataGridColumnName.MatetielNumber)
            {
                e.Handled = m_dateGridVeiwListDataList.isValidDataGridViewCellValue(e.KeyChar,
                    this.dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString());
            }
            else 
            {
                e.Handled = false;
            }
        }

        private void dataGridViewDataList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_isInit)
            {
                if (e.ColumnIndex == (int)DataGridColumnName.Value || e.ColumnIndex == (int)DataGridColumnName.Turnover)
                {
                    ArrayList columns = new ArrayList();

                    columns.Add((int)DataGridColumnName.Value);
                    columns.Add((int)DataGridColumnName.Turnover);

                    for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                    {
                        int columnNumber = (int)columns[columnIndex];
                        double sum = 0;

                        for (int rowIndex = 0; rowIndex < DateGridVeiwListDataListRowCount; rowIndex++)
                        {
                            if (this.dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString().Length > 0)
                            {
                                if (this.dataGridViewDataList.Rows[rowIndex].Cells[columnNumber].Value.ToString().Length > 0)
                                {
                                    sum += Convert.ToDouble(this.dataGridViewDataList.Rows[rowIndex].Cells[columnNumber].Value.ToString());
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        this.dataGridViewDataCount.Rows[0].Cells[columnNumber].Value = Convert.ToString(sum);
                    }
                }
            }
        }

        private void readBillInfoToUI()
        {
            // 单据表头表尾信息
            m_materieOutOrder = MaterielProOccupiedOrder.getInctance().getMaterielProOccupiedOrderInfoFromBillNumber(m_billNumber);

            m_applyStaffPkey = m_materieOutOrder.applyStaffId;

            this.labelTradingDate.Visible = true;
            this.labelBillNumber.Visible = true;
            this.labelMakeBillStaff.Visible = true;
            this.labelReviewBillStaff.Visible = true;
            this.labelReviewDate.Visible = true;
            this.labelSave.Visible = true;
            this.labelSummary.Visible = true;
            
            this.labelTradingDate.Text = m_materieOutOrder.tradingDate;
            this.labelBillNumber.Text = m_materieOutOrder.billNumber;
            this.labelMakeBillStaff.Text = m_materieOutOrder.makeOrderStaffName;
            this.labelSave.Text = m_materieOutOrder.applyStaffName;
            this.labelSummary.Text = m_materieOutOrder.srcOrderNum;

            // DataGridView 赋值
            writeBillDetailsInfoFromBillNumber(m_billNumber);

            // 如果单据已审核，则禁用页面所有控件
            if (m_materieOutOrder.isReview == "1")
            {
                this.labelReviewBillStaff.Text = m_materieOutOrder.orderrReviewName;
                this.labelReviewDate.Text = m_materieOutOrder.reviewDate;
                this.panelIsReview.Visible = true;

                this.save.Enabled = false;
                this.toolStripButtonReview.Enabled = false;

                this.panelTradingDate.Visible = false;
                this.dateTimePickerTradingDate.Visible = false;
                this.textBoxSummary.Visible = false;

                this.dataGridViewDataList.ReadOnly = true;
                this.dataGridViewDataCount.ReadOnly = true;
            }
            else
            {
                this.labelReviewBillStaff.Visible = false;
                this.labelReviewDate.Visible = false;
            }
        }

        private void readProInfoListToUI()
        {
            for (int index = 0; index < m_proInfoList.Count; index++)
            {
                ArrayList record = new ArrayList();

                record = m_proInfoList[index];

                if (index == 0)
                {
                    this.labelTradingDate.Visible = true;
                    this.labelTradingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    // 单据号
                    this.labelBillNumber.Text = BillNumber.getInctance().getNewBillNumber(BillTypeNumber, DateTime.Now.ToString("yyyy-MM-dd"));

                    // 制单人初始化
                    this.labelMakeBillStaff.Visible = true;
                    this.labelMakeBillStaff.Text = DbPublic.getInctance().getCurrentLoginUserName();

                    this.labelSummary.Visible = true;
                    this.labelSummary.Text = record[0].ToString();

                    m_materieOutOrder.isReview = "1";
                }

                setMatetielInfoToDataGridView(Convert.ToString(record[1]), index);
                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.Value].Value = Convert.ToDouble(record[2]) - Convert.ToDouble(record[3]);

                setTurnoverInfoDataGridView(index);
            }
        }

        private double getMaxValue(string materielID)
        {
            double value = 0.0;

            for (int index = 0; index < m_proInfoList.Count; index++)
            {
                ArrayList record = new ArrayList();

                record = m_proInfoList[index];

                if (record[1].ToString() == materielID)
                {
                    value = Convert.ToDouble(record[2]) - Convert.ToDouble(record[3]);
                    break;
                }
            }
            return value;
        }

        private void writeBillDetailsInfoFromBillNumber(string billNumber)
        {
            // DataGridView 赋值
            SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> orderDetails =
                MaterielProOccupiedOrderDetails.getInctance().getMaterielProOccupiedInfoFromBillNumber(billNumber);

            if (orderDetails.Count > DateGridVeiwListDataListRowCount)
            {
                m_dateGridVeiwListDataList.initDataGridViewData(orderDetails.Count + 1);
            }

            foreach (KeyValuePair<int, MaterielProOccupiedOrderDetailsTable> index in orderDetails)
            {
                MaterielProOccupiedOrderDetailsTable record = new MaterielProOccupiedOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitSale;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Note].Value = record.note;
            }
        }
    }
}