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
    public partial class FormSaleOutOrder : Form
    {
        private int m_customerPkey = -1;
        private int m_staffPkey = -1;
        private int m_staffSavePkey = -1;
        private int m_staffCheckPkey = -1;
        private string m_billNumber = "";
        private readonly int BillTypeNumber = 6;
        private readonly int DateGridVeiwListDataListRowCount = FormMain.DATA_GRID_VIEW_DEFAULT_ROW_COUNT;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isInit = false;
        private bool m_isRedBill = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        DataGridViewExtend m_dateGridVeiwListDataCount = new DataGridViewExtend();
        SaleOutOrderTable m_saleOutOrder = new SaleOutOrderTable();

        public enum DataGridColumnName
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
            SumTurnover,
            MakeNum
        };

        public FormSaleOutOrder(string billNumber = "")
        {
            InitializeComponent();
            m_billNumber = billNumber;
        }

        private void FormSaleOutOrder_Load(object sender, EventArgs e)
        {
            // DataGridView初始化
            dataGridViewInit();

            // 销售方式初始化
            this.labelSaleType.Visible = true;
            this.comboBoxSaleType.Items.Add("现购");
            this.comboBoxSaleType.Items.Add("赊购");
            this.comboBoxSaleType.SelectedIndex = 0;


            // 源单类型初始化
            this.labelSourceOrderType.Visible = true;
            this.comboBoxSourceOrderType.Items.Add("销售订单");
            this.comboBoxSourceOrderType.Items.Add("销售发票");
            this.comboBoxSourceOrderType.SelectedIndex = 0;

            if (m_billNumber.Length == 0)
            {
                // 单据号
                this.labelBillNumber.Text = BillNumber.getInctance().getNewBillNumber(BillTypeNumber, DateTime.Now.ToString("yyyy-MM-dd"));

                // 销售方式默认值
                this.labelSaleType.Text = this.comboBoxSaleType.Text;
                
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
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\\编码(*)", 100, true, false);

            if (DateGridVeiwListDataListRowCount > 12)
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 144, true, true);
            }
            else
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 161, true, true);
            }

            m_dateGridVeiwListDataList.addDataGridViewColumn("型号", 63, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 基本\n 单位", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("单价(*)", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 100, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("金额", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("应缴税\n税率%", 80, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("应缴税金额", 80, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("总金额", 80, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("生成编号", 80, true, false);

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

        #region 客户
        private void panelSaleName_Click(object sender, EventArgs e)
        {
            if (m_saleOutOrder.isReview == "1")
            {
                return;
            }

            if (!this.textBoxSaleName.Visible)
            {
                this.labelSaleName.Visible = false; 
                this.textBoxSaleName.Visible = true;
                this.textBoxSaleName.Focus();
            }
        }
        private void panelSaleName_DoubleClick(object sender, EventArgs e)
        {
            if (!this.textBoxSaleName.Visible)
            {
                this.labelSaleName.Visible = false; 
                this.textBoxSaleName.Visible = true;
            }
            else
            {
                FormBaseCustomer fbs = new FormBaseCustomer(true);
                fbs.ShowDialog();

                m_customerPkey = fbs.getSelectRecordPkey();
                CustomerTable record = Customer.getInctance().getCustomerInfoFromPkey(m_customerPkey);
                this.textBoxSaleName.Text = record.name;
                this.textBoxSaleName.Visible = true;
            }
        }
        private void textBoxSaleName_Leave(object sender, EventArgs e)
        {
            this.textBoxSaleName.Visible = false;
            this.labelSaleName.Text = this.textBoxSaleName.Text.ToString();
            this.labelSaleName.Visible = this.textBoxSaleName.Text.Length > 0;
        }
        #endregion

        #region 供应日期
        private void panelDateTime_Click(object sender, EventArgs e)
        {
            if (m_saleOutOrder.isReview == "1")
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
            if (m_saleOutOrder.isReview == "1")
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

        #region 销售类型
        private void panelSaleType_Click(object sender, EventArgs e)
        {
            if (m_saleOutOrder.isReview == "1")
            {
                return;
            }

            if (!this.comboBoxSaleType.Visible)
            {
                this.panelSaleType.Visible = false;
                this.labelSaleType.Visible = false;
                this.comboBoxSaleType.Visible = true;
                this.comboBoxSaleType.Focus();
            }
        }

        private void comboBoxSaleType_Leave(object sender, EventArgs e)
        {
            this.comboBoxSaleType.Visible = false;
            this.panelSaleType.Visible = true;
            this.labelSaleType.Text = this.comboBoxSaleType.Text.ToString();
            this.labelSaleType.Visible = this.comboBoxSaleType.Text.Length > 0;
        }
        #endregion

        #region 源单类型
        private void panelSourceOrderType_Click(object sender, EventArgs e)
        {
            if (m_saleOutOrder.isReview == "1")
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
            if (m_saleOutOrder.isReview == "1")
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
                    FormSaleOrderSequence fpos = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOrder, true);
                    fpos.ShowDialog();

                    string sourceBillNumber = fpos.getSelectOrderNumber();

                    this.textBoxSourceOrderNumber.Text = sourceBillNumber;
                    this.textBoxSourceOrderNumber.Visible = true;

                    writeBillDetailsInfoFromBillNumber(sourceBillNumber);
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
            if (m_saleOutOrder.isReview == "1")
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

        #region 业务员
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
            // 得到详细的销售信息
            ArrayList dataList = getSaleOutOrderDetailsValue();

            if (dataList.Count > 0)
            {
                // 销售订单表头和表尾信息
                SaleOutOrderTable record = getSaleOutOrderValue();
                if (saleOrderIsFull(record) && saleOrderDetailsIsFull(dataList))
                {
                    SaleOutOrder.getInctance().insert(record, false);
                    SaleOutOrderDetails.getInctance().insert(dataList);
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

        private SaleOutOrderTable getSaleOutOrderValue()
        {
            SaleOutOrderTable record = new SaleOutOrderTable();

            record.customerId = m_customerPkey;
            record.tradingDate = this.labelTradingDate.Text;
            record.billNumber = this.labelBillNumber.Text;
            record.saleType = this.labelSaleType.Text;
            record.paymentDate = this.labelPaymentDate.Text;
            record.exchangesUnit = this.labelSummary.Text;
            record.sourceBillType = this.labelSourceOrderType.Text;
            record.sourceBillNumber = this.labelSourceOrderNumber.Text;

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
                record.makeOrderStaff = m_saleOutOrder.makeOrderStaff;
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

        private bool saleOrderIsFull(SaleOutOrderTable record)
        {
            if (record.customerId == -1)
            {
                MessageBoxExtend.messageWarning("客户信息不完整，单据保存失败");
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

            if (record.saleType.Length == 0)
            {
                MessageBoxExtend.messageWarning("销售信息不完整，单据保存失败");
                return false;
            }

            if (record.businessPeopleId == -1)
            {
                MessageBoxExtend.messageWarning("业务员信息不完整，单据保存失败");
                return false;
            }

            return true;
        }

        private ArrayList getSaleOutOrderDetailsValue()
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
                    SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();

                    record.rowNumber = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString();
                    record.materielID = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                    record.price = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value.ToString());
                    record.value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    record.costApportionments = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString());
                    record.noCostApportionments = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
                    record.makeNum = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeNum].Value.ToString();
                    record.billNumber = this.labelBillNumber.Text;

                    list.Add(record);
                }
            }

            return list;
        }

        private bool saleOrderDetailsIsFull(ArrayList list)
        {
            bool isRet = true;

            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();
                record = (SaleOutOrderDetailsTable)list[rowIndex];

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
                SaleOutOrder.getInctance().billReview(m_billNumber);
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError(exp.ToString());
            }
        }

        private void printDisplay_Click(object sender, EventArgs e)
        {
            //PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, true);
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
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y);
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
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitSale);
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
            double turnover = 0, taxRate = 0, rate = 0;

            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Turnover].Value.ToString().Length > 0)
            {
                turnover = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Turnover].Value.ToString());
            }

            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString().Length > 0)
            {
                taxRate = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value.ToString());
            }
            
            if (dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString().Length > 0)
            {
                rate = Convert.ToDouble(dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value.ToString());
            }

            // 计算应缴税金额
            rate = turnover * taxRate * 0.01;

            // 总金额
            double sumTurnover = rate + turnover;

            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = Convert.ToString(rate);
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = Convert.ToString(sumTurnover);
        }

        private void dataGridViewDataList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; // 绑定到事件
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_columnIndex != (int)DataGridColumnName.MatetielNumber && m_columnIndex != (int)DataGridColumnName.MakeNum)
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

        private void labelSaleName_TextChanged(object sender, EventArgs e)
        {
            string summary = "";
            summary += this.dateTimePickerTradingDate.Text.ToString();
            summary += this.labelSaleName.Text;
            summary += "销售出库单(" + this.labelSaleType.Text + ")";

            this.labelSummary.Text = summary;
            this.labelSummary.Visible = true;
        }

        private void readBillInfoToUI()
        {
            // 单据表头表尾信息
            m_saleOutOrder = SaleOutOrder.getInctance().getSaleInfoFromBillNumber(m_billNumber);

            m_customerPkey = m_saleOutOrder.customerId;
            m_staffSavePkey = m_saleOutOrder.staffSaveId;
            m_staffCheckPkey = m_saleOutOrder.staffCheckId;

            this.labelSaleName.Visible = true;
            this.labelTradingDate.Visible = true;
            this.labelBillNumber.Visible = true;
            this.labelSaleType.Visible = true;
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
            
            this.labelSaleName.Text = m_saleOutOrder.customerName;
            this.labelTradingDate.Text = m_saleOutOrder.tradingDate;
            this.labelBillNumber.Text = m_saleOutOrder.billNumber;
            this.labelSaleType.Text = m_saleOutOrder.saleType;
            this.labelPaymentDate.Text = m_saleOutOrder.paymentDate;
            this.labelSummary.Text = m_saleOutOrder.exchangesUnit;

            m_staffPkey = m_saleOutOrder.businessPeopleId;
            this.labelBusinessPeople.Text = m_saleOutOrder.businessPeopleName;
            this.labelMakeBillStaff.Text = m_saleOutOrder.makeOrderStaffName;

            this.labelSourceOrderType.Text = m_saleOutOrder.sourceBillType;
            this.labelSourceOrderNumber.Text = m_saleOutOrder.sourceBillNumber;
            this.labelSave.Text = m_saleOutOrder.staffSaveName;
            this.labelVerify.Text = m_saleOutOrder.staffCheckName;

            // DataGridView 赋值
            SortedDictionary<int, SaleOutOrderDetailsTable> saleOrderDetails = 
                SaleOutOrderDetails.getInctance().getSaleOutInfoFromBillNumber(m_billNumber);

            foreach (KeyValuePair<int, SaleOutOrderDetailsTable> index in saleOrderDetails)
            {
                SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitSale;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = record.costApportionments;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.noCostApportionments;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeNum].Value = record.makeNum;
            }

            // 如果单据已审核，则禁用页面所有控件
            if (m_saleOutOrder.isReview == "1")
            {
                this.labelReviewBillStaff.Text = m_saleOutOrder.orderrReviewName;
                this.labelReviewDate.Text = m_saleOutOrder.reviewDate;
                this.panelIsReview.Visible = true;

                this.save.Enabled = false;
                this.toolStripButtonReview.Enabled = false;

                this.panelSaleName.Visible = false;
                this.panelTradingDate.Visible = false;
                this.panelSaleType.Visible = false;
                this.panelPaymentDate.Visible = false;
                this.panelSummary.Visible = false;

                this.textBoxSaleName.Visible = false;
                this.dateTimePickerTradingDate.Visible = false;
                this.comboBoxSaleType.Visible = false;
                this.dateTimePickerPaymentDate.Visible = false;
                this.textBoxSummary.Visible = false;

                this.panelBusinessPeople.Visible = false;

                // 如果单据已经审核则入账按钮是好用的
                this.registerInLedger.Enabled = true;
            }
            else
            {
                this.labelReviewBillStaff.Visible = false;
                this.labelReviewDate.Visible = false;
                this.registerInLedger.Enabled = false;
            }

            // 如果单据已经记账，则出来红字已记账提示
            if (m_saleOutOrder.isInLedger == 1)
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

            if (m_saleOutOrder.isRedBill == 1)
            {
                m_isRedBill = true;
                this.panelRed.Visible = true;
            }
        }

        private void writeBillDetailsInfoFromBillNumber(string billNumber)
        {

            // DataGridView 赋值
            SortedDictionary<int, SaleOrderDetailsTable> saleOrderDetails =
                SaleOrderDetails.getInctance().getSaleInfoFromBillNumber(billNumber);

            foreach (KeyValuePair<int, SaleOrderDetailsTable> index in saleOrderDetails)
            {
                SaleOrderDetailsTable record = new SaleOrderDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitSale;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = record.transportationCost;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.otherCost;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
            }
        }

        private void redBill_Click(object sender, EventArgs e)
        {
            m_isRedBill = true;
            this.panelRed.Enabled = true;
            this.panelRed.Visible = true;

            this.redBill.ForeColor = Color.Red;
            this.redBill.CheckState = CheckState.Checked;
            this.blueBill.CheckState = CheckState.Unchecked;
        }

        private void registerInLedger_Click(object sender, EventArgs e)
        {
            try
            {
                save_Click(sender, e);
                SaleOutOrder.getInctance().registerInLedger(m_billNumber, m_isRedBill);
            }
            catch (Exception exp)
            {
                MessageBoxExtend.messageError(exp.ToString());
            }
        }

        private void blueBill_Click(object sender, EventArgs e)
        {
            m_isRedBill = false;
            this.panelRed.Enabled = false;
            this.panelRed.Visible = false;

            this.redBill.ForeColor = Color.Black;
            this.redBill.CheckState = CheckState.Unchecked;
            this.blueBill.CheckState = CheckState.Checked;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(203);

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

        private void dataGridViewDataList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < DateGridVeiwListDataListRowCount))
            {
                m_rowIndex = e.RowIndex;

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
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