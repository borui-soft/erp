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
    public partial class FormPurchaseInOrder : Form
    {
        private int m_supplierPkey = -1;
        private int m_staffPkey = -1;
        private int m_staffSavePkey = -1;
        private int m_staffCheckPkey = -1;
        private string m_billNumber = "";
        private readonly int BillTypeNumber = 2;
        private readonly int DateGridVeiwListDataListRowCount = FormMain.DATA_GRID_VIEW_DEFAULT_ROW_COUNT;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isInit = false;
        private bool m_isRedBill = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        DataGridViewExtend m_dateGridVeiwListDataCount = new DataGridViewExtend();
        PurchaseInOrderTable m_purchaseInOrder = new PurchaseInOrderTable();

        public enum DataGridColumnName
        {
            RowNum,
            MatetielNumber,
            MatetielName,
            ContractMatetielName,
            Model,
            Parameter,
            Brand,
            Unit,
            CZ,
            Price,
            Value,
            Turnover,
            TransportationCost,
            OtherCost,
            SumTurnover,
        };

        public FormPurchaseInOrder(string billNumber = "")
        {
            InitializeComponent();
            m_billNumber = billNumber;
        }

        private void FormPurchaseInOrder_Load(object sender, EventArgs e)
        {
            // DataGridView初始化
            dataGridViewInit();

            // 采购方式初始化
            this.labelPurchaseType.Visible = true;
            this.comboBoxPurchaseType.Items.Add("现购");
            this.comboBoxPurchaseType.Items.Add("赊购");
            this.comboBoxPurchaseType.SelectedIndex = 0;


            // 源单类型初始化
            this.labelSourceOrderType.Visible = true;
            this.comboBoxSourceOrderType.Items.Add("采购申请单");
            this.comboBoxSourceOrderType.Items.Add("采购订单");
            this.comboBoxSourceOrderType.Items.Add("采购发票");
            this.comboBoxSourceOrderType.SelectedIndex = 0;

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

            setPageActionEnable();
        }

        private void dataGridViewInit()
        {
            // 物料资料初始化
            m_dateGridVeiwListDataList.addDataGridViewColumn("行号", 55, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\\编码(*)", 70, true, false);

            if (DateGridVeiwListDataListRowCount > 12)
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 104, true, true);
                m_dateGridVeiwListDataList.addDataGridViewColumn("合同\n物料名称", 110, true, false); 
            }
            else
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 117, true, true);
                m_dateGridVeiwListDataList.addDataGridViewColumn("合同\n物料名称", 113, true, false); 
            }
            m_dateGridVeiwListDataList.addDataGridViewColumn("型号", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("参数", 63, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("品牌", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("单位", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("材质", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("单价(*)", 70, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 70, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("金额", 70, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("应计\n成本费用", 75, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("不计\n成本费用", 75, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("总金额", 80, true, true);

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
            if (m_purchaseInOrder.isReview == "1")
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
            if (m_purchaseInOrder.isReview == "1")
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
            this.labelPaymentDate.Visible = true;

            this.dateTimePickerTradingDate.Visible = false;
            this.labelTradingDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
            this.labelPaymentDate.Text = this.dateTimePickerTradingDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 约定付款日期
        private void panelPaymentDate_Click(object sender, EventArgs e)
        {
            if (m_purchaseInOrder.isReview == "1")
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
            if (m_purchaseInOrder.isReview == "1")
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

        #region 源单类型
        private void panelSourceOrderType_Click(object sender, EventArgs e)
        {
            if (m_purchaseInOrder.isReview == "1")
            {
                return;
            }

            if (!this.comboBoxSourceOrderType.Visible)
            {
                this.panelSourceOrderType.Visible = false;
                this.labelSourceOrderType.Visible = false;
                this.comboBoxSourceOrderType.Visible = true;
                this.comboBoxSourceOrderType.Focus();
            }
        }

        private void comboBoxSourceOrderType_Leave(object sender, EventArgs e)
        {
            this.comboBoxSourceOrderType.Visible = false;
            this.panelSourceOrderType.Visible = true;
            this.labelSourceOrderType.Text = this.comboBoxSourceOrderType.Text.ToString();
            this.labelSourceOrderType.Visible = this.comboBoxSourceOrderType.Text.Length > 0;
        }
        #endregion

        #region 源单据号
        private void panelSourceOrderNumber_Click(object sender, EventArgs e)
        {
            if (m_purchaseInOrder.isReview == "1")
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

        private void panelSourceOrderNumber_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxSourceOrderNumber.Visible)
            {
                this.labelSourceOrderNumber.Visible = false;
                this.textBoxSourceOrderNumber.Visible = true;
            }
            else
            {
                if (comboBoxSourceOrderType.SelectedIndex == 0)
                {
                    FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseApplyOrder, true);
                    fpos.ShowDialog();

                    string sourceBillNumber = fpos.getSelectOrderNumber();

                    // 自动填充总源单据号
                    this.textBoxSourceOrderNumber.Text = sourceBillNumber;
                    this.textBoxSourceOrderNumber.Visible = true;
                    
                    // 自动填充DataGridView区域
                    writeBillDetailsInfoFromBillNumber(sourceBillNumber);

                    // 自动填写总材料表编号
                    this.labelContractNum.Visible = true;
                    PurchaseApplyOrderTable tmp = PurchaseApplyOrder.getInctance().getPurchaseInfoFromBillNumber(sourceBillNumber);
                    this.labelContractNum.Text = tmp.srcOrderNum;
                }
                else if (comboBoxSourceOrderType.SelectedIndex == 1)
                {
                    FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseOrder, true);
                    fpos.ShowDialog();

                    string sourceBillNumber = fpos.getSelectOrderNumber();

                    // 自动填充总源单据号
                    this.textBoxSourceOrderNumber.Text = sourceBillNumber;
                    this.textBoxSourceOrderNumber.Visible = true;

                    // 自动填充DataGridView区域
                    writeBillDetailsInfoFromBillNumber(sourceBillNumber);

                    // 自动填写总材料表编号
                    this.labelContractNum.Visible = true;
                    PurchaseOrderTable tmp = PurchaseOrder.getInctance().getPurchaseInfoFromBillNumber(sourceBillNumber);
                    this.labelContractNum.Text = tmp.srcOrderNum;
                }
            }
        }

        private void textBoxSourceOrderNumber_Leave(object sender, EventArgs e)
        {
            this.textBoxSourceOrderNumber.Visible = false;
            this.labelSourceOrderNumber.Text = this.textBoxSourceOrderNumber.Text.ToString();
            this.labelSourceOrderNumber.Visible = this.textBoxSourceOrderNumber.Text.Length > 0;
        }
        #endregion

        #region 摘要
        private void panelSummary_Click(object sender, EventArgs e)
        {
            if (m_purchaseInOrder.isReview == "1")
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

            if (panelRed.Visible)
            {
                this.labelSummary.Text += "(红色单据)";
            }
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

                m_staffSavePkey = fbs.getSelectRecordPkey();
                StaffTable record = Staff.getInctance().getStaffInfoFromPkey(m_staffSavePkey);
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

        #region 验收员
        private void panelVerify_Click(object sender, EventArgs e)
        {
            if (!this.textBoxVerify.Visible)
            {
                this.labelVerify.Visible = false;
                this.textBoxVerify.Visible = true;
                this.textBoxVerify.Focus();
            }
        }

        private void panelVerify_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxVerify.Visible)
            {
                this.labelVerify.Visible = false;
                this.textBoxVerify.Visible = true;
            }
            else
            {
                FormBaseStaff fbs = new FormBaseStaff(true);
                fbs.ShowDialog();

                m_staffCheckPkey = fbs.getSelectRecordPkey();
                StaffTable record = Staff.getInctance().getStaffInfoFromPkey(m_staffCheckPkey);
                this.textBoxVerify.Text = record.name;
                this.textBoxVerify.Visible = true;
            }
        }

        private void textBoxVerify_Leave(object sender, EventArgs e)
        {
            this.textBoxVerify.Visible = false;
            this.labelVerify.Text = this.textBoxVerify.Text.ToString();
            this.labelVerify.Visible = this.textBoxVerify.Text.Length > 0;
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
            // 得到详细的采购信息
            ArrayList dataList = getPurchaseInOrderDetailsValue();

            if (dataList.Count > 0)
            {
                // 采购订单表头和表尾信息
                PurchaseInOrderTable record = getPurchaseInOrderValue();
                if (purchaseOrderIsFull(record) && purchaseOrderDetailsIsFull(dataList))
                {
                    PurchaseInOrder.getInctance().insert(record, false);
                    PurchaseInOrderDetails.getInctance().insert(dataList);
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

        private PurchaseInOrderTable getPurchaseInOrderValue()
        {
            PurchaseInOrderTable record = new PurchaseInOrderTable();

            record.supplierId = m_supplierPkey;
            record.tradingDate = this.labelTradingDate.Text;
            record.billNumber = this.labelBillNumber.Text;
            record.purchaseType = this.labelPurchaseType.Text;
            record.paymentDate = this.labelPaymentDate.Text;
            record.exchangesUnit = this.labelSummary.Text;
            record.sourceBillType = this.labelSourceOrderType.Text;
            record.sourceBillNumber = this.labelSourceOrderNumber.Text;
            record.srcOrderNum = this.labelContractNum.Text;
            record.purchaseNum = this.labelPurchaseNum.Text;

            record.sumValue = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Value].Value.ToString();
            record.sumMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.Turnover].Value.ToString();

            record.sumTransportationCost = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString();
            record.sumOtherCost = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.OtherCost].Value.ToString();
            record.totalMoney = this.dataGridViewDataCount.Rows[0].Cells[(int)DataGridColumnName.SumTurnover].Value.ToString();

            record.staffSaveId = m_staffSavePkey;
            record.staffCheckId = m_staffCheckPkey;
            
            record.businessPeopleId = m_staffPkey;

            if (m_billNumber.Length == 0)
            {
                record.makeOrderStaff = DbPublic.getInctance().getCurrentLoginUserID();
            }
            else 
            {
                record.makeOrderStaff = m_purchaseInOrder.makeOrderStaff;
            }

            if (m_isRedBill)
            {
                record.isRedBill = 1;
            }
            else
            {
                record.isRedBill = 0;
            }

            return record;
        }

        private bool purchaseOrderIsFull(PurchaseInOrderTable record)
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

            if (record.srcOrderNum.Length > 60)
            {
                MessageBoxExtend.messageWarning("项目编号信息最大长度为60字符,目前输入的项目编号信息太长，请重新输入");
                return false;
            }

            return true;
        }

        private ArrayList getPurchaseInOrderDetailsValue()
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
                    PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();

                    record.rowNumber = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString();
                    record.materielID = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());

                    record.contractMaterielName = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.ContractMatetielName].Value.ToString();

                    record.price = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                    record.value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    record.costApportionments = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString());
                    record.noCostApportionments = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
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
                PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();
                record = (PurchaseInOrderDetailsTable)list[rowIndex];

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
                PurchaseInOrder.getInctance().billReview(m_billNumber);
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError(exp.ToString());
            }
        }

        // 记账按钮事件
        private void registerInLedger_Click(object sender, EventArgs e)
        {
            try
            {
                save_Click(sender, e);
                PurchaseInOrder.getInctance().registerInLedger(m_billNumber, m_isRedBill);
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError(exp.ToString());
            }
        }

        // 蓝字按钮事件
        private void blueBill_Click(object sender, EventArgs e)
        {
            m_isRedBill = false;
            this.panelRed.Enabled = false;
            this.panelRed.Visible = false;

            this.redBill.ForeColor = Color.Black;
            this.redBill.CheckState = CheckState.Unchecked;
            this.blueBill.CheckState = CheckState.Checked;
        }

        // 红字按钮事件
        private void redBill_Click(object sender, EventArgs e)
        {
            m_isRedBill = true;
            this.panelRed.Enabled = true;
            this.panelRed.Visible = true;

            this.redBill.ForeColor = Color.Red;
            this.redBill.CheckState = CheckState.Checked;
            this.blueBill.CheckState = CheckState.Unchecked;
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
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Unit].Value =
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitPurchase);
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.CZ].Value = record.CZ;
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
            if (m_columnIndex != (int)DataGridColumnName.MatetielNumber && m_columnIndex != (int)DataGridColumnName.ContractMatetielName)
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
            summary += "采购入库单(" + this.labelPurchaseType.Text + ")";

            this.labelSummary.Text = summary;
            this.labelSummary.Visible = true;
        }

        private void readBillInfoToUI()
        {
            // 单据表头表尾信息
            m_purchaseInOrder = PurchaseInOrder.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            m_supplierPkey = m_purchaseInOrder.supplierId;
            m_staffSavePkey = m_purchaseInOrder.staffSaveId;
            m_staffCheckPkey = m_purchaseInOrder.staffCheckId;

            this.labelPurchaseName.Visible = true;
            this.labelTradingDate.Visible = true;
            this.labelBillNumber.Visible = true;
            this.labelPurchaseType.Visible = true;
            this.labelPaymentDate.Visible = true;
            this.labelSummary.Visible = true;
            this.labelBusinessPeople.Visible = true;
            this.labelMakeBillStaff.Visible = true;
            this.labelReviewBillStaff.Visible = true;
            this.labelReviewDate.Visible = true;
            this.labelSourceOrderType.Visible = true;
            this.labelSourceOrderNumber.Visible = true;
            this.labelSave.Visible = true;
            this.labelVerify.Visible = true;
            this.labelContractNum.Visible = true;
            this.labelPurchaseNum.Visible = true;
            
            this.labelPurchaseName.Text = m_purchaseInOrder.supplierName;
            this.labelTradingDate.Text = m_purchaseInOrder.tradingDate;
            this.labelBillNumber.Text = m_purchaseInOrder.billNumber;
            this.labelContractNum.Text = m_purchaseInOrder.srcOrderNum;
            this.labelPurchaseType.Text = m_purchaseInOrder.purchaseType;
            this.labelPaymentDate.Text = m_purchaseInOrder.paymentDate;
            this.labelSummary.Text = m_purchaseInOrder.exchangesUnit;

            m_staffPkey = m_purchaseInOrder.businessPeopleId;
            this.labelBusinessPeople.Text = m_purchaseInOrder.businessPeopleName;
            this.labelMakeBillStaff.Text = m_purchaseInOrder.makeOrderStaffName;

            this.labelSourceOrderType.Text = m_purchaseInOrder.sourceBillType;
            this.labelSourceOrderNumber.Text = m_purchaseInOrder.sourceBillNumber;
            this.labelSave.Text = m_purchaseInOrder.staffSaveName;
            this.labelVerify.Text = m_purchaseInOrder.staffCheckName;
            this.labelContractNum.Text = m_purchaseInOrder.srcOrderNum;
            this.labelPurchaseNum.Text = m_purchaseInOrder.purchaseNum;
            

            // DataGridView 赋值
            SortedDictionary<int, PurchaseInOrderDetailsTable> purchaseOrderDetails = 
                PurchaseInOrderDetails.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            foreach (KeyValuePair<int, PurchaseInOrderDetailsTable> index in purchaseOrderDetails)
            {
                PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;
                MaterielTable materielInfo = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.ContractMatetielName].Value = record.contractMaterielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.parameter;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.CZ].Value = materielInfo.CZ;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = record.costApportionments;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.noCostApportionments;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
            }

            // 如果单据已审核，则禁用页面所有控件
            if (m_purchaseInOrder.isReview == "1")
            {
                this.labelReviewBillStaff.Text = m_purchaseInOrder.orderrReviewName;
                this.labelReviewDate.Text = m_purchaseInOrder.reviewDate;
                this.panelIsReview.Visible = true;

                this.save.Enabled = false;
                this.toolStripButtonReview.Enabled = false;

                this.panelPurchaseName.Visible = false;
                this.panelTradingDate.Visible = false;
                this.panelPurchaseType.Visible = false;
                this.panelPaymentDate.Visible = false;
                this.panelSummary.Visible = false;

                this.textBoxPurchaseName.Visible = false;
                this.dateTimePickerTradingDate.Visible = false;
                this.comboBoxPurchaseType.Visible = false;
                this.dateTimePickerPaymentDate.Visible = false;
                this.textBoxSummary.Visible = false;

                this.panelBusinessPeople.Visible = false;

                this.registerInLedger.Enabled = true;
                this.redBill.Enabled = false;
                this.blueBill.Enabled = false;
            }
            else
            {
                this.labelReviewBillStaff.Visible = false;
                this.labelReviewDate.Visible = false;
                this.registerInLedger.Enabled = false;
            }

            // 如果单据已经记账，则出来红字已记账提示
            if (m_purchaseInOrder.isInLedger == 1)
            {
                this.panelInLedger.Visible = true;
                this.registerInLedger.Enabled = false;
                this.dataGridViewDataList.ReadOnly = true;
                this.dataGridViewDataCount.ReadOnly = true;
            }
            else
            {
                this.dataGridViewDataList.Enabled = true;
                this.dataGridViewDataCount.Enabled = true;
            }

            if (m_purchaseInOrder.isRedBill == 1)
            {
                m_isRedBill = true;
                this.panelRed.Visible = true;
            }
        }

        private void writeBillDetailsInfoFromBillNumber(string billNumber)
        {
            if (comboBoxSourceOrderType.SelectedIndex == 0)
            {
                // DataGridView 赋值
                SortedDictionary<int, PurchaseApplyOrderDetailsTable> purchaseOrderDetails =
                    PurchaseApplyOrderDetails.getInctance().getPurchaseInfoFromBillNumber(billNumber);

                foreach (KeyValuePair<int, PurchaseApplyOrderDetailsTable> index in purchaseOrderDetails)
                {
                    PurchaseApplyOrderDetailsTable record = new PurchaseApplyOrderDetailsTable();
                    record = index.Value;

                    int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;
                    MaterielTable materielInfo = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);

                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.CZ].Value = materielInfo.CZ;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = "0";
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.otherCost;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
                }
            }
            else if (comboBoxSourceOrderType.SelectedIndex == 1)
            {
                // DataGridView 赋值
                SortedDictionary<int, PurchaseOrderDetailsTable> purchaseOrderDetails =
                    PurchaseOrderDetails.getInctance().getPurchaseInfoFromBillNumber(billNumber);

                foreach (KeyValuePair<int, PurchaseOrderDetailsTable> index in purchaseOrderDetails)
                {
                    PurchaseOrderDetailsTable record = new PurchaseOrderDetailsTable();
                    record = index.Value;

                    int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;
                    MaterielTable materielInfo = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);

                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.CZ].Value = materielInfo.CZ;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = record.transportationCost;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.otherCost;
                    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
                }
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(102);

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

        private void panel8_Click(object sender, EventArgs e)
        {
            if (m_purchaseInOrder.isReview == "1")
            {
                return;
            }

            this.labelContractNum.Visible = false;
            this.textBoxContractNum.Visible = true;

            this.textBoxContractNum.Text = this.labelContractNum.Text;
            this.textBoxContractNum.Focus();
        }

        private void panel8_Leave(object sender, EventArgs e)
        {
            this.textBoxContractNum.Visible = false;
            this.labelContractNum.Text = this.textBoxContractNum.Text.ToString();
            this.labelContractNum.Visible = this.textBoxContractNum.Text.Length > 0;

            this.labelContractNum.Text = this.labelContractNum.Text.Replace("-", "_");
            this.labelContractNum.Text = this.labelContractNum.Text.Replace("-", "_");
            this.labelContractNum.Text = this.labelContractNum.Text.Replace("—", "_");
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

                if (this.labelContractNum.Text.Length <= 0)
                {
                    if (!MessageBoxExtend.messageQuestion("材料表编号为空, 可能无法显示出材料表中的相关数据, 是否继续查看?"))
                    {
                        return;
                    }
                }

                FormMaterielDetailed fmd = new FormMaterielDetailed(materielID, this.labelContractNum.Text);
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

        private void textBoxPurchaseNum_Click(object sender, EventArgs e)
        {
            if (m_purchaseInOrder.isReview == "1")
            {
                return;
            }

            this.labelPurchaseNum.Visible = false;
            this.textBoxPurchaseNum.Visible = true;

            this.textBoxPurchaseNum.Text = this.labelPurchaseNum.Text;
            this.textBoxPurchaseNum.Focus();
        }

        private void textBoxPurchaseNum_Leave(object sender, EventArgs e)
        {
            this.textBoxPurchaseNum.Visible = false;
            this.labelPurchaseNum.Text = this.textBoxPurchaseNum.Text.ToString();
            this.labelPurchaseNum.Visible = this.textBoxPurchaseNum.Text.Length > 0;
        }
    }
}