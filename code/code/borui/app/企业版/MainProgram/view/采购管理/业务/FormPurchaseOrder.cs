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
    public partial class FormPurchaseOrder : Form
    {
        private int m_supplierPkey = -1;
        private int m_staffPkey = -1;
        private string m_billNumber = "";
        private readonly int BillTypeNumber = 1;
        private readonly int DateGridVeiwListDataListRowCount = FormMain.DATA_GRID_VIEW_DEFAULT_ROW_COUNT;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isInit = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        DataGridViewExtend m_dateGridVeiwListDataCount = new DataGridViewExtend();
        PurchaseOrderTable m_purchaseOrder = new PurchaseOrderTable();

        private enum DataGridColumnName
        {
            RowNum,
            MatetielNumber,
            MatetielName,
            Model,
            Unit,
            Price,
            Value,
            Turnover,
            TransportationCost,
            OtherCost,
            SumTurnover
        };

        public FormPurchaseOrder(string billNumber = "")
        {
            InitializeComponent();
            m_billNumber = billNumber;
        }

        private void FormPurchaseOrder_Load(object sender, EventArgs e)
        {
            // DataGridView初始化
            dataGridViewInit();

            // 采购方式初始化
            this.labelPurchaseType.Visible = true;
            this.comboBoxPurchaseType.Items.Add("现购");
            this.comboBoxPurchaseType.Items.Add("赊购");
            this.comboBoxPurchaseType.SelectedIndex = 0;

            if (m_billNumber.Length == 0)
            {
                // 单据号
                this.labelBillNumber.Text = BillNumber.getInctance().getNewBillNumber(BillTypeNumber, DateTime.Now.ToString("yyyy-MM-dd"));

                // 采购方式默认值
                this.labelPurchaseType.Text = this.comboBoxPurchaseType.Text;

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
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 144, true, true);
            }
            else
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 161, true, true);
            }

            m_dateGridVeiwListDataList.addDataGridViewColumn("型号", 83, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 基本\n 单位", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("单价(*)", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("金额", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("运输费用", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("其他费用", 100, true, false);
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

        #region 供应商
        private void panelPurchaseName_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            if (!this.textBoxPurchaseName.Visible)
            {
                this.labelPurchaseName.Visible = false; 
                this.textBoxPurchaseName.Visible = true;
                this.textBoxPurchaseName.Focus();
            }
        }
        private void panelPurchaseName_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxPurchaseName.Visible)
            {
                this.labelPurchaseName.Visible = false; 
                this.textBoxPurchaseName.Visible = true;
            }
            else
            {
                FormBaseSupplier fbs = new FormBaseSupplier(true);
                fbs.ShowDialog();

                m_supplierPkey = fbs.getSelectRecordPkey();
                SupplierTable record = Supplier.getInctance().getSupplierInfoFromPkey(m_supplierPkey);
                this.textBoxPurchaseName.Text = record.name;
                this.textBoxPurchaseName.Visible = true;
            }
        }
        private void textBoxPurchaseName_Leave(object sender, EventArgs e)
        {
            this.textBoxPurchaseName.Visible = false;
            this.labelPurchaseName.Text = this.textBoxPurchaseName.Text.ToString();
            this.labelPurchaseName.Visible = this.textBoxPurchaseName.Text.Length > 0;
        }
        #endregion

        #region 供应日期
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
            this.labelDeliveryDate.Visible = true;
            this.labelPaymentDate.Visible = true;

            this.dateTimePickerTradingDate.Visible = false;
            this.labelTradingDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
            this.labelDeliveryDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
            this.labelPaymentDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 约定交货日期
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

        #region 约定付款日期
        private void panelPaymentDate_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            if (this.labelPaymentDate.Visible)
            {
                this.labelPaymentDate.Visible = false;
            }

            if (!this.dateTimePickerPaymentDate.Visible)
            {
                this.panelPaymentDate.Visible = false;
                this.labelPaymentDate.Visible = false;
                this.dateTimePickerPaymentDate.Visible = true;
                this.dateTimePickerPaymentDate.Focus();
            }
        }
        private void dateTimePickerPaymentDate_Leave(object sender, EventArgs e)
        {
            this.panelPaymentDate.Visible = true;
            this.labelPaymentDate.Visible = true;
            this.dateTimePickerPaymentDate.Visible = false;
            this.labelPaymentDate.Text = this.dateTimePickerPaymentDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 采购类型
        private void panelPurchaseType_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            if (!this.comboBoxPurchaseType.Visible)
            {
                this.panelPurchaseType.Visible = false;
                this.labelPurchaseType.Visible = false;
                this.comboBoxPurchaseType.Visible = true;
                this.comboBoxPurchaseType.Focus();
            }
        }

        private void comboBoxPurchaseType_Leave(object sender, EventArgs e)
        {
            this.comboBoxPurchaseType.Visible = false;
            this.panelPurchaseType.Visible = true;
            this.labelPurchaseType.Text = this.comboBoxPurchaseType.Text.ToString();
            this.labelPurchaseType.Visible = this.comboBoxPurchaseType.Text.Length > 0;
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

        #region 采购员
        private void panelBusinessPeople_Click(object sender, EventArgs e)
        {
            if (!this.textBoxBusinessPeople.Visible)
            {
                this.labelBusinessPeople.Visible = false;
                this.textBoxBusinessPeople.Visible = true;
                this.textBoxBusinessPeople.Focus();
            }
        }

        private void panelBusinessPeople_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxBusinessPeople.Visible)
            {
                this.labelBusinessPeople.Visible = false;
                this.textBoxBusinessPeople.Visible = true;
            }
            else
            {
                FormBaseStaff fbs = new FormBaseStaff(true);
                fbs.ShowDialog();

                m_staffPkey = fbs.getSelectRecordPkey();
                StaffTable record = Staff.getInctance().getStaffInfoFromPkey(m_staffPkey);
                this.textBoxBusinessPeople.Text = record.name;
                this.textBoxBusinessPeople.Visible = true;
            }
        }

        private void textBoxBusinessPeople_Leave(object sender, EventArgs e)
        {
            this.textBoxBusinessPeople.Visible = false;
            this.labelBusinessPeople.Text = this.textBoxBusinessPeople.Text.ToString();
            this.labelBusinessPeople.Visible = this.textBoxBusinessPeople.Text.Length > 0;
        }
        #endregion

        private void save_Click(object sender, EventArgs e)
        {
            this.ActiveControl = this.toolStrip1;

            // 得到详细的采购信息
            ArrayList dataList = getPurchaseOrderDetailsValue();

            if (dataList.Count > 0)
            {
                // 采购订单表头和表尾信息
                PurchaseOrderTable record = getPurchaseOrderValue();
                if (purchaseOrderIsFull(record) && purchaseOrderDetailsIsFull(dataList))
                {
                    PurchaseOrder.getInctance().insert(record, false);
                    PurchaseOrderDetails.getInctance().insert(dataList);
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

        private PurchaseOrderTable getPurchaseOrderValue()
        {
            PurchaseOrderTable record = new PurchaseOrderTable();

            record.supplierId = m_supplierPkey;
            record.tradingDate = this.labelTradingDate.Text;
            record.billNumber = this.labelBillNumber.Text;
            record.purchaseType = this.labelPurchaseType.Text;
            record.deliveryDate = this.labelDeliveryDate.Text;
            record.paymentDate = this.labelPaymentDate.Text;
            record.exchangesUnit = this.labelSummary.Text;

            record.xxMaterielTableNum = this.labelContractNum.Text;
            record.srcOrderNum = this.labelSourceOrderNumber.Text;

            record.sumValue = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Value].Value.ToString();
            record.sumMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Turnover].Value.ToString();

            record.sumTransportationCost = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString();
            record.sumOtherCost = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.OtherCost].Value.ToString();
            record.totalMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.SumTurnover].Value.ToString();

            record.businessPeopleId = m_staffPkey;

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

        private bool purchaseOrderIsFull(PurchaseOrderTable record)
        {
            if (record.supplierId == -1)
            {
                MessageBoxExtend.messageWarning("供应商信息不完整，单据保存失败");
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

            if (record.purchaseType.Length == 0)
            {
                MessageBoxExtend.messageWarning("采购信息不完整，单据保存失败");
                return false;
            }

            if (record.businessPeopleId == -1)
            {
                MessageBoxExtend.messageWarning("采购员信息不完整，单据保存失败");
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
                    PurchaseOrderDetailsTable record = new PurchaseOrderDetailsTable();

                    record.rowNumber = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString();
                    record.materielID = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                    record.price = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                    record.value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    record.transportationCost = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString());
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
                PurchaseOrderDetailsTable record = new PurchaseOrderDetailsTable();
                record = (PurchaseOrderDetailsTable)list[rowIndex];

                //string price = Convert.ToString(record.price);
                //string value = Convert.ToString(record.value);

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
                PurchaseOrder.getInctance().billReview(m_billNumber);
                MessageBoxExtend.messageOK("单据审核成功");
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
            if (e.ColumnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                /* 如果是物料编码列，需要判断该物料编码是否存在
                 * 如果存在读取相应的值填充DataGridView中对应的其他列，如果不存在该物料编码，则清空该行
                 * */
                if (dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString().Length > 0)
                {
                    setMatetielInfoToDataGridView(dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString());
                }
            }
            else if (e.ColumnIndex == (int)DataGridColumnName.Price || e.ColumnIndex == (int)DataGridColumnName.Value)
            {
                setTurnoverInfoDataGridView();
            }
            else if (e.ColumnIndex == (int)DataGridColumnName.TransportationCost || e.ColumnIndex == (int)DataGridColumnName.OtherCost)
            {
                setSumTurnoverInfoDataGridView();
            }
        }
        
        private void setMatetielInfoToDataGridView(string id)
        {
            //使用这个输入的值，匹配物料编号
            MaterielTable record = Materiel.getInctance().getMaterielInfoFromNum(Convert.ToString(id));

            if (record == null || id.ToLower() != record.num.ToLower() || record.pkey == 0)
            {
                try
                {
                    //使用这个输入的值，匹配物料key
                    double pkey = Convert.ToDouble(id.ToString());

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

            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.pkey;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.name;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.model;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Unit].Value =
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitPurchase);
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Price].Value = "0";
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value].Value = "0";
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = "0";
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = "0";
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = "0";
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = "0";
        }

        private void setTurnoverInfoDataGridView()
        {
            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString().Length > 0 &&
                dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString().Length > 0)
            {
                double price = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                double value = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                double turnover = price * value;

                // 金额信息保留2位小数儿
                turnover = (double)(Math.Round(turnover * 100)) / 100;

                dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = Convert.ToString(turnover);

                if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString().Length > 0 &&
                    dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString().Length > 0)
                {
                    double transportationCost = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString());
                    double otherCost = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
                    double sumTurnover = turnover + transportationCost + otherCost;
                    dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = Convert.ToString(sumTurnover);
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

            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString().Length > 0)
            {
                transportationCost = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString());
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
                               e.ColumnIndex == (int)DataGridColumnName.TransportationCost ||
                               e.ColumnIndex == (int)DataGridColumnName.OtherCost ||
                               e.ColumnIndex == (int)DataGridColumnName.SumTurnover)
                {
                    ArrayList columns = new ArrayList();

                    columns.Add((int)DataGridColumnName.Value);
                    columns.Add((int)DataGridColumnName.Turnover);
                    columns.Add((int)DataGridColumnName.TransportationCost);
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
            summary += "采购订单(" + this.labelPurchaseType.Text + ")";

            this.labelSummary.Text = summary;
            this.labelSummary.Visible = true;
        }

        private void readBillInfoToUI()
        {
            // 单据表头表尾信息
            m_purchaseOrder = PurchaseOrder.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            m_supplierPkey = m_purchaseOrder.supplierId;

            this.labelPurchaseName.Visible = true;
            this.labelTradingDate.Visible = true;
            this.labelBillNumber.Visible = true;
            this.labelPurchaseType.Visible = true;
            this.labelDeliveryDate.Visible = true;
            this.labelPaymentDate.Visible = true;
            this.labelSummary.Visible = true;
            this.labelBusinessPeople.Visible = true;
            this.labelMakeBillStaff.Visible = true;
            this.labelReviewBillStaff.Visible = true;
            this.labelReviewDate.Visible = true;

            this.labelContractNum.Visible = true;
            this.labelSourceOrderNumber.Visible = true;
            
            this.labelPurchaseName.Text = m_purchaseOrder.supplierName;
            this.labelTradingDate.Text = m_purchaseOrder.tradingDate;
            this.labelBillNumber.Text = m_purchaseOrder.billNumber;
            this.labelPurchaseType.Text = m_purchaseOrder.purchaseType;
            this.labelDeliveryDate.Text = m_purchaseOrder.deliveryDate;
            this.labelPaymentDate.Text = m_purchaseOrder.paymentDate;
            this.labelSummary.Text = m_purchaseOrder.exchangesUnit;

            this.labelContractNum.Text = m_purchaseOrder.xxMaterielTableNum;
            this.labelSourceOrderNumber.Text = m_purchaseOrder.srcOrderNum;

            m_staffPkey = m_purchaseOrder.businessPeopleId;
            this.labelBusinessPeople.Text = m_purchaseOrder.businessPeopleName;
            this.labelMakeBillStaff.Text = m_purchaseOrder.makeOrderStaffName;

            // DataGridView 赋值
            SortedDictionary<int, PurchaseOrderDetailsTable> purchaseOrderDetails = 
                PurchaseOrderDetails.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            foreach (KeyValuePair<int, PurchaseOrderDetailsTable> index in purchaseOrderDetails)
            {
                PurchaseOrderDetailsTable record = new PurchaseOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = record.transportationCost;
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
                this.panelPurchaseType.Visible = false;
                this.panelDeliveryDate.Visible = false;
                this.panelPaymentDate.Visible = false;
                this.panelSummary.Visible = false;

                this.textBoxPurchaseName.Visible = false;
                this.dateTimePickerTradingDate.Visible = false;
                this.comboBoxPurchaseType.Visible = false;
                this.dateTimePickerDeliveryDate.Visible = false;
                this.dateTimePickerPaymentDate.Visible = false;
                this.textBoxSummary.Visible = false;

                this.panelBusinessPeople.Visible = false;
            }
            else
            {
                this.labelReviewBillStaff.Visible = false;
                this.labelReviewDate.Visible = false;
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(101);

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

        private void textBoxContractNum_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            this.labelContractNum.Visible = false;
            this.textBoxContractNum.Visible = true;

            this.textBoxContractNum.Text = this.labelContractNum.Text;

            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("-", "_");
            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("-", "_");
            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("—", "_");

            this.textBoxContractNum.Focus();
        }

        private void panelContractNum_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            this.labelContractNum.Visible = false;
            this.textBoxContractNum.Visible = true;

            this.textBoxContractNum.Text = this.labelContractNum.Text;

            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("-", "_");
            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("-", "_");
            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("—", "_");

            this.textBoxContractNum.Focus();
        }

        private void panelContractNum_Leave(object sender, EventArgs e)
        {
            this.textBoxContractNum.Visible = false;
            this.labelContractNum.Text = this.textBoxContractNum.Text.ToString();

            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("-", "_");
            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("-", "_");
            this.textBoxContractNum.Text = this.textBoxContractNum.Text.Replace("—", "_");

            this.labelContractNum.Visible = this.textBoxContractNum.Text.Length > 0;
        }

        private void panelSourceOrderNumber_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxSourceOrderNumber.Visible)
            {
                this.labelSourceOrderNumber.Visible = false;
                this.textBoxSourceOrderNumber.Visible = true;
            }
            else
            {
                FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseApplyOrder, true);
                fpos.ShowDialog();

                string strSrcApplyOrderNum = fpos.getSelectOrderNumber();
                this.labelContractNum.Text = fpos.getSelectOrderProjectNum();

                this.textBoxSourceOrderNumber.Text = strSrcApplyOrderNum;
                this.textBoxSourceOrderNumber.Visible = true;
                this.labelContractNum.Visible = true;

                writeBillDetailsInfoFromBillNumber(strSrcApplyOrderNum);
            }
        }

        private void panelSourceOrderNumber_Click(object sender, EventArgs e)
        {
            if (m_purchaseOrder.isReview == "1")
            {
                return;
            }

            if (!this.textBoxSourceOrderNumber.Visible)
            {
                this.labelSourceOrderNumber.Visible = false;
                this.textBoxSourceOrderNumber.Visible = true;
                this.textBoxSourceOrderNumber.Focus();
            }
        }

        private void textBoxSourceOrderNumber_Leave(object sender, EventArgs e)
        {
            this.textBoxSourceOrderNumber.Visible = false;
            this.labelSourceOrderNumber.Text = this.textBoxSourceOrderNumber.Text.ToString();
            this.labelSourceOrderNumber.Visible = this.textBoxSourceOrderNumber.Text.Length > 0;
        }

        private void writeBillDetailsInfoFromBillNumber(string billNumber)
        {
            // DataGridView 赋值
            SortedDictionary<int, PurchaseApplyOrderDetailsTable> purchaseOrderDetails =
                PurchaseApplyOrderDetails.getInctance().getPurchaseInfoFromBillNumber(billNumber);

            foreach (KeyValuePair<int, PurchaseApplyOrderDetailsTable> index in purchaseOrderDetails)
            {
                PurchaseApplyOrderDetailsTable record = new PurchaseApplyOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = 0;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.otherCost;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
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

        private void ToolStripMenuItemCheckDetailed_Click(object sender, EventArgs e)
        {
            if (dataGridViewDataList.Rows[m_rowIndex].Cells[1].Value.ToString().Length > 0)
            {
                int materielID = Convert.ToInt32(dataGridViewDataList.Rows[m_rowIndex].Cells[1].Value.ToString());

                if (this.textBoxContractNum.Text.Length <= 0)
                {
                    if (!MessageBoxExtend.messageQuestion("材料表编号为空, 可能无法显示出材料表中的相关数据, 是否继续查看?"))
                    {
                        return;
                    }
                }

                FormMaterielDetailed fmd = new FormMaterielDetailed(materielID, this.textBoxContractNum.Text);
                fmd.ShowDialog();
            }
            else
            {
                MessageBoxExtend.messageWarning("选择行的物料ID为空, 请重新选择");
            }
        }

        private void ToolStripMenuItemDelRow_Click(object sender, EventArgs e)
        {
            if (dataGridViewDataList.Rows[m_rowIndex].Cells[1].Value.ToString().Length > 0)
            {
                string rowNum = dataGridViewDataList.Rows[m_rowIndex].Cells[0].Value.ToString();

                if (MessageBoxExtend.messageQuestion("确认删除第" + rowNum + "行的数据吗？"))
                {
                    m_dateGridVeiwListDataList.delDataGridVewRow(Convert.ToInt32(rowNum), DateGridVeiwListDataListRowCount);
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("选择行的物料ID为空, 请重新选择");
            }
        }
    }
}