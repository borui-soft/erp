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
using System.Reflection;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormPurchaseApply : Form
    {
        private int m_applyStaffPkey = -1;
        private string m_billNumber = "";
        private readonly int BillTypeNumber = 18;
        private readonly int DateGridVeiwListDataListRowCount = FormMain.DATA_GRID_VIEW_DEFAULT_ROW_COUNT;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isInit = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        DataGridViewExtend m_dateGridVeiwListDataCount = new DataGridViewExtend();
        PurchaseApplyOrderTable m_purchaseOrder = new PurchaseApplyOrderTable();

        // 根据项目跟踪状况，直接形成采购申请单
        public SortedDictionary<int, ArrayList> m_proInfoList = new SortedDictionary<int, ArrayList>();

        public enum DataGridColumnName
        {
            RowNum,
            MatetielNumber,
            MatetielName,
            Brand,          //品牌
            Model,
            Parameter,
            Unit,
            Price,
            Value,
            Turnover,
            OtherCost,
            SumTurnover
        };

        public FormPurchaseApply(string billNumber = "", SortedDictionary<int, ArrayList> proInfoList = null)
        {
            InitializeComponent();
            m_billNumber = billNumber;

            if (proInfoList != null)
            {
                m_proInfoList = proInfoList;
            }
        }

        private void FormPurchaseApply_Load(object sender, EventArgs e)
        {
            // DataGridView初始化
            dataGridViewInit();

            if (m_proInfoList.Count > 0)
            {
                readProInfoListToUI();
                return;
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

            // 权限判断
            setPageActionEnable();
        }

        private void dataGridViewInit()
        {
            // 物料资料初始化
            m_dateGridVeiwListDataList.addDataGridViewColumn("行号", 55, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\\编码(*)", 100, true, false);

            if (DateGridVeiwListDataListRowCount > 12)
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 244, true, true);
            }
            else
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 261, true, true);
            }

            m_dateGridVeiwListDataList.addDataGridViewColumn("品牌", 61, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("型号", 61, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("参数", 61, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 基本\n 单位", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("参考单价", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 80, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("金额", 80, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("其他费用", 80, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("总金额", 100, true, true);

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

        #region 申请人
        private void panelPurchaseName_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            if (!this.textBoxPApplyName.Visible)
            {
                this.labelPurchaseName.Visible = false; 
                this.textBoxPApplyName.Visible = true;
                this.textBoxPApplyName.Focus();
            }
        }
        private void panelPurchaseName_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxPApplyName.Visible)
            {
                this.labelPurchaseName.Visible = false; 
                this.textBoxPApplyName.Visible = true;
            }
            else
            {
                FormBaseStaff fbs = new FormBaseStaff(true);
                fbs.ShowDialog();

                m_applyStaffPkey = fbs.getSelectRecordPkey();
                StaffTable record = Staff.getInctance().getStaffInfoFromPkey(m_applyStaffPkey);

                this.textBoxPApplyName.Text = record.name;
                this.textBoxPApplyName.Visible = true;
            }
        }
        private void textBoxPurchaseName_Leave(object sender, EventArgs e)
        {
            this.textBoxPApplyName.Visible = false;
            this.labelPurchaseName.Text = this.textBoxPApplyName.Text.ToString();
            this.labelPurchaseName.Visible = this.textBoxPApplyName.Text.Length > 0;
        }
        #endregion

        #region 申请日期
        private void panelDateTime_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
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

        #region 期望交货日期
        private void panelDeliveryDate_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            if (this.labelDeliveryDate.Visible)
            {
                this.labelDeliveryDate.Visible = false;
            }

            if (!this.dateTimePickerDeliveryDate.Visible)
            {
                this.panelDeliveryDate.Visible = false;
                this.dateTimePickerDeliveryDate.Visible = true;
                this.dateTimePickerDeliveryDate.Focus();
            }
        }
        private void dateTimePickerDeliveryDate_Leave(object sender, EventArgs e)
        {
            this.panelDeliveryDate.Visible = true;
            this.labelDeliveryDate.Visible = true;
            this.dateTimePickerDeliveryDate.Visible = false;
            this.labelDeliveryDate.Text = this.dateTimePickerDeliveryDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 摘要
        private void panelSummary_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            this.panelSummary.Visible = false;
            this.labelSummary.Visible = false;
            this.textBoxSummary.Visible = true;
            this.textBoxSummary.Text = this.labelSummary.Text;
            this.textBoxSummary.Focus();
        }

        private void textBoxSummary_Leave(object sender, EventArgs e)
        {
            this.textBoxSummary.Visible = false;
            this.panelSummary.Visible = true;
            this.labelSummary.Text = this.textBoxSummary.Text.ToString();
            this.labelSummary.Visible = this.textBoxSummary.Text.Length > 0;
        }
        #endregion

        #region 项目编号
        private void panelProjectNum_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            this.panelProjectNum.Visible = false;
            this.labelProject.Visible = false;
            this.textBoxProject.Visible = true;
            this.textBoxProject.Text = this.labelProject.Text;
            this.textBoxProject.Text = this.textBoxProject.Text.Replace("-", "_");
            this.textBoxProject.Text = this.textBoxProject.Text.Replace("-", "_");
            this.textBoxProject.Text = this.textBoxProject.Text.Replace("—", "_");
            this.textBoxProject.Focus();
        }

        private void textBoxProject_Leave(object sender, EventArgs e)
        {
            this.textBoxProject.Visible = false;
            this.panelProjectNum.Visible = true;
            this.labelProject.Text = this.textBoxProject.Text.ToString();
            this.textBoxProject.Text = this.textBoxProject.Text.Replace("-", "_");
            this.textBoxProject.Text = this.textBoxProject.Text.Replace("-", "_");
            this.textBoxProject.Text = this.textBoxProject.Text.Replace("—", "_");
            this.labelProject.Visible = this.textBoxProject.Text.Length > 0;
        }
        #endregion

        private void save_Click(object sender, EventArgs e)
        {
            // 得到详细的采购信息
            ArrayList dataList = getPurchaseOrderDetailsValue();

            if (dataList.Count > 0)
            {
                // 采购订单表头和表尾信息
                PurchaseApplyOrderTable record = getPurchaseApplyOrderValue();
                if (purchaseOrderIsFull(record) && purchaseOrderDetailsIsFull(dataList))
                {
                    PurchaseApplyOrder.getInctance().insert(record, false);
                    PurchaseApplyOrderDetails.getInctance().insert(dataList);
                    BillNumber.getInctance().inserBillNumber(BillTypeNumber, this.labelTradingDate.Text, this.labelBillNumber.Text.ToString());

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

        private PurchaseApplyOrderTable getPurchaseApplyOrderValue()
        {
            PurchaseApplyOrderTable record = new PurchaseApplyOrderTable();

            record.applyId = m_applyStaffPkey;
            record.tradingDate = this.labelTradingDate.Text;
            record.billNumber = this.labelBillNumber.Text;

            record.srcOrderNum = this.labelProject.Text;
            record.paymentDate = this.labelDeliveryDate.Text;
            record.exchangesUnit = this.labelSummary.Text;

            record.sumValue = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Value].Value.ToString();
            record.sumMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Turnover].Value.ToString();
            record.sumOtherCost = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.OtherCost].Value.ToString();
            record.totalMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.SumTurnover].Value.ToString();


            if (m_billNumber.Length == 0)
            {
                record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();
            }
            else
            {
                record.makeOrderStaff = m_purchaseOrder.makeOrderStaff;
            }

            return record;
        }

        private bool purchaseOrderIsFull(PurchaseApplyOrderTable record)
        {
            if (record.applyId == -1)
            {
                MessageBoxExtend.messageWarning("申请人信息不完整，单据保存失败");
                return false;
            }

            if (record.tradingDate.Length == 0)
            {
                MessageBoxExtend.messageWarning("日期不完整，单据保存失败");
                return false;
            }

            if (record.billNumber.Length == 0)
            {
                MessageBoxExtend.messageWarning("单据号信息不完整，单据保存失败");
                return false;
            }

            if (record.srcOrderNum.Length > 60)
            {
                MessageBoxExtend.messageWarning("项目编号信息最大长度为60字符,目前输入的项目编号信息太长，请重新输入");
                return false;
            }

            return true;
        }

        private ArrayList getPurchaseOrderDetailsValue()
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
                    PurchaseApplyOrderDetailsTable record = new PurchaseApplyOrderDetailsTable();

                    record.rowNumber = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString();
                    record.materielID = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                    record.price = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                    record.value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    record.otherCost = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
                    record.billNumber = this.labelBillNumber.Text;

                    list.Add(record);
                }
            }

            return list;
        }

        private bool purchaseOrderDetailsIsFull(ArrayList list)
        {
            bool isRet = true;

            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                PurchaseApplyOrderDetailsTable record = new PurchaseApplyOrderDetailsTable();
                record = (PurchaseApplyOrderDetailsTable)list[rowIndex];

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
                PurchaseApplyOrder.getInctance().billReview(m_billNumber);
                MessageBoxExtend.messageOK("单据审核成功");
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError(exp.ToString());
            }
        }

        private void printDisplay_Click(object sender, EventArgs e)
        {
            // PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, true);
            if (m_billNumber.Length > 0)
            {
                FormOrderPrint fop = new FormOrderPrint(BillTypeNumber, m_billNumber, this.dataGridViewDataList);
                fop.ShowDialog();
            }
            else
            {
                MessageBoxExtend.messageWarning("请先保存数据再打印");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, false);
        }

        private void selectSourceOrder_Click(object sender, EventArgs e)
        {
            if (m_rowIndex != -1 && m_columnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                FormBaseMateriel fbm = new FormBaseMateriel(true);
                fbm.ShowDialog();
                this.dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].Value = Convert.ToString(fbm.getSelectRecordPkey());
                this.dataGridViewDataList.CurrentCell = this.dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Price];
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
                /* 如果是物料编码列，需要判断该物料编码是否存在
                 * 如果存在读取相应的值填充DataGridView中对应的其他列，如果不存在该物料编码，则清空该行
                 * */
                if (dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString().Length > 0)
                {
                    setMatetielInfoToDataGridView(dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString(), m_rowIndex);
                }
            }
            else if (e.ColumnIndex == (int)DataGridColumnName.Price || e.ColumnIndex == (int)DataGridColumnName.Value)
            {
                setTurnoverInfoDataGridView(m_rowIndex);
            }
            else if (e.ColumnIndex == (int)DataGridColumnName.OtherCost)
            {
                setSumTurnoverInfoDataGridView();
            }
        }
        
        private void setMatetielInfoToDataGridView(string id, int rowIndex)
        {
            //使用这个输入的值，匹配物料编号
            MaterielTable record = Materiel.getInctance().getMaterielInfoFromNum(Convert.ToString(id));

            if (id != record.num || record.pkey == 0)
            {
                try
                {
                    //使用这个输入的值，匹配物料key
                    double pkey = Convert.ToDouble(id.ToString());

                    record = Materiel.getInctance().getMaterielInfoFromPkey((int)pkey);

                    if (pkey != record.pkey || record.pkey == 0)
                    {
                        MessageBoxExtend.messageWarning("[" + dataGridViewDataList.Rows[rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString() +
                            "]不存在，请重新输入或选择");
                        m_dateGridVeiwListDataList.clearDataGridViewRow(rowIndex);

                        return;
                    }
                }
                catch
                {
                    MessageBoxExtend.messageWarning("[" + dataGridViewDataList.Rows[rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString() +
                        "]不存在，请重新输入或选择");
                    m_dateGridVeiwListDataList.clearDataGridViewRow(rowIndex);

                    return;
                }
            }

            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.pkey;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.name;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.model;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value =
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitPurchase);
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = "0";
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = "0";
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = "0";
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = "0";
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = "0";
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

                if (dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString().Length > 0)
                {
                    double otherCost = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
                    double sumTurnover = turnover + otherCost;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = Convert.ToString(sumTurnover);
                }
            }
        }

        private void setSumTurnoverInfoDataGridView()
        {
            double turnover = 0, transportationCost = 0, otherCost = 0;

            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Turnover].Value.ToString().Length > 0)
            {
                turnover = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Turnover].Value.ToString());
            }
            
            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString().Length > 0)
            {
                otherCost = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
            }

            double sumTurnover = turnover + transportationCost + otherCost;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = Convert.ToString(sumTurnover);

            // 当本次采购发生了运输费用或者其他费用时，需要把费用平均到每件货物单价上
            //double value = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
            //double price = sumTurnover / value;
            //price = (double)(Math.Round(price * 100)) / 100;
            //dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Price].Value = Convert.ToString(price);
        }

        private void dataGridViewDataList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; // 绑定到事件
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_columnIndex != (int)DataGridColumnName.MatetielNumber)
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
                if (e.ColumnIndex == (int)DataGridColumnName.Value ||
                               e.ColumnIndex == (int)DataGridColumnName.Turnover ||
                               e.ColumnIndex == (int)DataGridColumnName.OtherCost ||
                               e.ColumnIndex == (int)DataGridColumnName.SumTurnover)
                {
                    ArrayList columns = new ArrayList();

                    columns.Add((int)DataGridColumnName.Value);
                    columns.Add((int)DataGridColumnName.Turnover);
                    columns.Add((int)DataGridColumnName.OtherCost);
                    columns.Add((int)DataGridColumnName.SumTurnover);

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

        private void labelPurchaseName_TextChanged(object sender, EventArgs e)
        {
            string summary = "";
            summary += this.dateTimePickerTradingDate.Text.ToString();
            summary += this.labelPurchaseName.Text;
            summary += "采购申请单";

            this.labelSummary.Text = summary;
            this.labelSummary.Visible = true;
        }

        private void readBillInfoToUI()
        {
            // 单据表头表尾信息
            m_purchaseOrder = PurchaseApplyOrder.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            m_applyStaffPkey = m_purchaseOrder.applyId;

            this.labelPurchaseName.Visible = true;
            this.labelTradingDate.Visible = true;
            this.labelBillNumber.Visible = true;
            this.labelProject.Visible = true;
            this.labelDeliveryDate.Visible = true;
            this.labelSummary.Visible = true;
            this.labelMakeBillStaff.Visible = true;
            this.labelReviewBillStaff.Visible = true;
            this.labelReviewDate.Visible = true;

            this.labelPurchaseName.Text = m_purchaseOrder.applyName;
            this.labelTradingDate.Text = m_purchaseOrder.tradingDate;
            this.labelBillNumber.Text = m_purchaseOrder.billNumber;
            this.labelDeliveryDate.Text = m_purchaseOrder.paymentDate;
            this.labelProject.Text = m_purchaseOrder.srcOrderNum;
            this.labelSummary.Text = m_purchaseOrder.exchangesUnit;
            this.labelMakeBillStaff.Text = m_purchaseOrder.makeOrderStaffName;

            // DataGridView 赋值
            SortedDictionary<int, PurchaseApplyOrderDetailsTable> purchaseOrderDetails = 
                PurchaseApplyOrderDetails.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            foreach (KeyValuePair<int, PurchaseApplyOrderDetailsTable> index in purchaseOrderDetails)
            {
                PurchaseApplyOrderDetailsTable record = new PurchaseApplyOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.parameter;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.otherCost;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
            }

            // 如果单据已审核，则禁用页面所有控件
            if (m_purchaseOrder.isReview == "1")
            {
                this.labelReviewBillStaff.Text = m_purchaseOrder.orderrReviewName;
                this.labelReviewDate.Text = m_purchaseOrder.reviewDate;
                this.panelIsReview.Visible = true;

                this.save.Enabled = false;
                this.toolStripButtonReview.Enabled = false;
                this.dataGridViewDataList.ReadOnly = true;
                this.dataGridViewDataCount.ReadOnly = true;

                this.panelPurchaseName.Visible = false;
                this.panelTradingDate.Visible = false;
                this.panelProjectNum.Visible = false;
                this.panelDeliveryDate.Visible = false;
                this.panelSummary.Visible = false;

                this.textBoxPApplyName.Visible = false;
                this.textBoxProject.Visible = false;
                this.dateTimePickerTradingDate.Visible = false;
                this.dateTimePickerDeliveryDate.Visible = false;
                this.textBoxSummary.Visible = false;
            }
            else
            {
                this.labelReviewBillStaff.Visible = false;
                this.labelReviewDate.Visible = false;
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(106);

            foreach (KeyValuePair<int, ActionTable> index in list)
            {
                object activeObject = this.GetType().GetField(index.Value.uiActionName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                bool isEnable = AccessAuthorization.getInctance().isAccessAuthorization(index.Value.pkey,
                    Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));

                if (activeObject != null)
                {
                    UserInterfaceActonState.setUserInterfaceActonState(activeObject,
                        ((System.Reflection.MemberInfo)(activeObject.GetType())).Name.ToString(), isEnable);
                }
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


                    this.labelProject.Visible = true;
                    this.labelProject.Text = record[0].ToString();
                    this.panelProjectNum.Visible = false;
                    this.textBoxProject.Visible = false;
                    this.textBoxProject.ReadOnly = true;

                    this.panelDeliveryDate.Visible = true;
                    this.labelDeliveryDate.Visible = true;
                    this.dateTimePickerDeliveryDate.Visible = false;
                    this.labelDeliveryDate.Text = this.dateTimePickerDeliveryDate.Value.ToString("yyyy-MM-dd");

                    m_purchaseOrder.isReview = "1";
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

        private void ToolStripMenuItemCheckDetailed_Click(object sender, EventArgs e)
        {
            if (dataGridViewDataList.Rows[m_rowIndex].Cells[1].Value.ToString().Length > 0)
            {
                int materielID = Convert.ToInt32(dataGridViewDataList.Rows[m_rowIndex].Cells[1].Value.ToString());

                if (this.labelProject.Text.Length <= 0)
                {
                    if (!MessageBoxExtend.messageQuestion("材料表编号为空, 可能无法显示出材料表中的相关数据, 是否继续查看?"))
                    {
                        return;
                    }
                }

                FormMaterielDetailed fmd = new FormMaterielDetailed(materielID, this.labelProject.Text);
                fmd.ShowDialog();
            }
            else 
            {
                MessageBoxExtend.messageWarning("选择行的物料ID为空, 请重新选择");
            }
        }

        private void dataGridViewDataList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < DateGridVeiwListDataListRowCount))
            {
                m_rowIndex = e.RowIndex;

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }
    }
}