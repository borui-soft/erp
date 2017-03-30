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
    public partial class FormProjectMaterielChangeOrder : Form
    {
        private int m_staffPkey = -1;
        private string m_billNumber = "";
        private string m_projectNum = "";

        private int m_tablesType;
        private readonly int BillTypeNumber = 20;
        private readonly int DateGridVeiwListDataListRowCount = 12;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isSaveSuccessFul = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        BillDataGridViewExtend m_dateGridVeiwListDataListChange = new BillDataGridViewExtend();
        FormProjectMaterielChangeTable m_currentOrderInfo = new FormProjectMaterielChangeTable();

        SortedDictionary<int, ProjectManagerDetailsTable> m_purchaseOrderDetails = new SortedDictionary<int, ProjectManagerDetailsTable>();

        private enum DataGridColumnName
        {
            RowNum,         //行号
            MatetielNumber, //物料编码
            Brand,          //品牌
            MatetielName,   //物料名称

            Model,          //规格型号
            Parameter,      //参数
            Size,           //尺寸
            Unit,           //单位

            Value,          //数量
            MakeType,       //制造类型
            Note            //备注
        };

        public FormProjectMaterielChangeOrder(string billNumber)
        {
            InitializeComponent();

            //注：页面因素中数据表类型(1：设备总材料表；2：电器总材料表；3：工程总材料表)
            m_currentOrderInfo = FormProjectInfoChange.getInctance().getProjectInfoFromBillNumber(billNumber);
            m_tablesType = m_currentOrderInfo.dataType;

            if (m_tablesType == 1)
            {
                this.labelName.Text = "设备总材料变更申请表";
                this.Text = "设备总材料变更申请表";
            }
            else if (m_tablesType == 2)
            {
                this.labelName.Text = "电器总材料变更申请表";
                this.Text = "电器总材料变更申请表";
            }
            else if (m_tablesType == 3)
            {
                this.labelName.Text = "工程总材料变更申请表";
                this.Text = "工程总材料变更申请表";
            }
            else
            {
                m_tablesType = 1;
                this.labelName.Text = "设备总材料变更申请表";
                this.Text = "设备总材料变更申请表";
            }
            m_billNumber = billNumber;
        }

        public FormProjectMaterielChangeOrder(int tablesType, string billNumber, string projectNum)
        {
            InitializeComponent();

            //注：页面因素中数据表类型(1：设备总材料表；2：电器总材料表；3：工程总材料表)
            m_tablesType = tablesType;

            if (m_tablesType == 1)
            {
                this.labelName.Text = "设备总材料变更申请表";
                this.Text = "设备总材料变更申请表";
            }
            else if (m_tablesType == 2)
            {
                this.labelName.Text = "电器总材料变更申请表";
                this.Text = "电器总材料变更申请表";
            }
            else if (m_tablesType == 3)
            {
                this.labelName.Text = "工程总材料变更申请表";
                this.Text = "工程总材料变更申请表";
            }
            else
            {
                m_tablesType = 1;
                this.labelName.Text = "设备总材料变更申请表";
                this.Text = "设备总材料变更申请表";
            }

            this.labelSrcOrderBillNum.Text = billNumber;
            m_currentOrderInfo.dataType = tablesType;

            m_projectNum = projectNum;
        }

        private void FormProjectMaterielOrder_Load(object sender, EventArgs e)
        {
            // DataGridView初始化
            dataGridViewInit();

            if (m_billNumber.Length == 0)
            {
                // 单据号
                this.labelBillNumber.Text = BillNumber.getInctance().getNewBillNumber(BillTypeNumber, DateTime.Now.ToString("yyyy-MM-dd"));

                // 制单人初始化
                this.labelMakeBillStaff.Visible = true;
                this.labelMakeBillStaff.Text = DbPublic.getInctance().getCurrentLoginUserName();

                // 制单日期初始化
                this.labelMakeDate.Visible = true;
                this.labelMakeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            // 变更前单据初始化
            m_dateGridVeiwListDataList.addDataGridViewColumn("行号", 55, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\\编码(*)", 100, true, false);

            m_dateGridVeiwListDataList.addDataGridViewColumn("品牌", 80, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称(*)", 151, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 规格\n 型号", 90, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("参数", 80, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("尺寸", 80, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 基本\n 单位", 80, true, true);

            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 80, true, false);
            m_dateGridVeiwListDataList.addDataGridViewColumn("制作方式", 80, true, false);

            m_dateGridVeiwListDataList.initDataGridViewColumn(this.dataGridViewDataList);
            m_dateGridVeiwListDataList.initDataGridViewData(DateGridVeiwListDataListRowCount);


            // 变更后单据初始化
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("行号", 55, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("物料ID\\编码(*)", 100, true, false);

            m_dateGridVeiwListDataListChange.addDataGridViewColumn("品牌", 80, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("物料名称(*)", 151, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn(" 规格\n 型号", 90, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("参数", 80, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("尺寸", 80, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn(" 基本\n 单位", 80, true, true);

            m_dateGridVeiwListDataListChange.addDataGridViewColumn("数量(*)", 80, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("制作方式", 80, true, true);

            m_dateGridVeiwListDataListChange.initDataGridViewColumn(this.dataGridViewDataListChangeOfter);
            m_dateGridVeiwListDataListChange.initDataGridViewData(DateGridVeiwListDataListRowCount);

            // 根据单据编号，得到详细数据
            m_purchaseOrderDetails = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(this.labelSrcOrderBillNum.Text);
        }

        #region 摘要
        private void panelSummary_Click(object sender, EventArgs e)
        {
            if (m_currentOrderInfo.isReview == "1")
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
            // 得到详细的采购信息
            ArrayList dataList = getOrderDetailsValue();

            if (dataList.Count > 0)
            {
                // 采购订单表头和表尾信息
                geTableHadeAndEndValue();

                if (purchaseOrderIsFull(m_currentOrderInfo) && purchaseOrderDetailsIsFull(dataList))
                {
                    FormProjectInfoChange.getInctance().insert(m_currentOrderInfo, false);
                    ProjectManagerDetails.getInctance().insert(dataList);

                    if (m_billNumber.Length == 0)
                    {
                        MessageBoxExtend.messageOK("数据保存成功");
                    }

                    m_isSaveSuccessFul = true;

                    this.Close();
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("此单据不包含任何交易信息，单据保存失败.");
            }
        }

        private void geTableHadeAndEndValue()
        {
            m_currentOrderInfo.projectNum = m_projectNum;
            m_currentOrderInfo.srcBillNumber = this.labelSrcOrderBillNum.Text;

            if (m_billNumber.Length == 0)
            {
                m_currentOrderInfo.designStaffID = m_staffPkey;
            }
            else
            {
                m_currentOrderInfo.designStaffID = m_currentOrderInfo.designStaffID;
            }

            m_currentOrderInfo.billNumber = this.labelBillNumber.Text;
            m_currentOrderInfo.changeReason = this.labelSummary.Text;


            m_currentOrderInfo.makeOrderDate = this.labelMakeDate.Text;
            m_currentOrderInfo.makeOrderStaffID = DbPublic.getInctance().getCurrentLoginUserID();

            m_currentOrderInfo.materielIDs = "";
            for (int rowIndex = 0; rowIndex < dataGridViewDataListChangeOfter.Rows.Count; rowIndex++)
            {
                if (this.dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }
                else
                {
                    m_currentOrderInfo.materielIDs += dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString();
                    m_currentOrderInfo.materielIDs += "#";
                }
            }
        }

        private bool purchaseOrderIsFull(FormProjectMaterielChangeTable record)
        {
            if (record.changeReason.Length <= 0)
            {
                MessageBoxExtend.messageWarning("变更原因不得为空，请重新输入");
                return false;
            }

            if (this.textBoxBusinessPeople.Text.Length <= 0)
            {
                MessageBoxExtend.messageWarning("设计人员信息不完整，单据保存失败");
                return false;
            }

            return true;
        }

        private ArrayList getOrderDetailsValue()
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
                    ProjectManagerDetailsTable currentRowInfo = new ProjectManagerDetailsTable();

                    currentRowInfo.billNumber = this.labelBillNumber.Text;
                    currentRowInfo.rowNumber = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString());
                    currentRowInfo.materielID = Convert.ToInt32(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                    currentRowInfo.value = Convert.ToDouble(dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    currentRowInfo.makeType = dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeType].Value.ToString();
                    currentRowInfo.materielNote = "";

                    list.Add(currentRowInfo);
                }
            }

            return list;
        }

        private bool purchaseOrderDetailsIsFull(ArrayList list)
        {
            bool isRet = true;

            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();
                record = (ProjectManagerDetailsTable)list[rowIndex];

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
                if (m_billNumber.Length > 0)
                {
                    save_Click(sender, e);

                    if (m_isSaveSuccessFul)
                    {
                        FormProjectInfoChange.getInctance().billReview(this.labelBillNumber.Text);
                        MessageBoxExtend.messageOK("单据审核成功");
                    }

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
            PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, false);
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
        }

        private void setMatetielInfoToDataGridView(string id)
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
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.model;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Size].Value = "";
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Unit].Value =
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitPurchase);
            dataGridViewDataList.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value].Value = "0";
        }

        private void dataGridViewDataList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; // 绑定到事件
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_columnIndex != (int)DataGridColumnName.MakeType && m_columnIndex != (int)DataGridColumnName.Note && m_columnIndex != (int)DataGridColumnName.MatetielNumber)
            {
                e.Handled = m_dateGridVeiwListDataList.isValidDataGridViewCellValue(e.KeyChar,
                    this.dataGridViewDataList.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString());
            }
            else
            {
                e.Handled = false;
            }
        }

        private void readBillInfoToUI()
        {
            m_projectNum = m_currentOrderInfo.projectNum;

            this.labelSrcOrderBillNum.Visible = true;
            this.labelBillNumber.Visible = true;
            this.labelSummary.Visible = true;

            this.labelMakeBillStaff.Visible = true;
            this.labelMakeDate.Visible = true;
            this.labelBusinessPeople.Visible = true;
            this.labelReviewBillStaff.Visible = true;
            this.labelReviewDate.Visible = true;

            this.labelSrcOrderBillNum.Text = m_currentOrderInfo.srcBillNumber;
            this.labelBillNumber.Text = m_currentOrderInfo.billNumber;
            this.labelSummary.Text = m_currentOrderInfo.changeReason;

            this.labelMakeBillStaff.Text = m_currentOrderInfo.makeOrderStaffName;
            this.labelMakeDate.Text = m_currentOrderInfo.makeOrderDate;
            this.labelBusinessPeople.Text = m_currentOrderInfo.designStaffName;
            this.textBoxBusinessPeople.Text = m_currentOrderInfo.designStaffName;
            this.labelReviewBillStaff.Text = m_currentOrderInfo.orderrReviewName;
            this.labelReviewDate.Text = m_currentOrderInfo.reviewDate;

            // 变更前DataGridView 赋值
            string matetiels = m_currentOrderInfo.materielIDs;
            matetiels = matetiels.Replace("##", "#");

            string[] sArray = matetiels.Split('#');
            int rowNum = 0;
            foreach (string index in sArray)
            {
                if (index.ToString().Length > 0)
                {
                    setMatetielInfo(rowNum, index.ToString());
                    rowNum++;
                }
            }

            // 变更后DataGridView 赋值
            SortedDictionary<int, ProjectManagerDetailsTable> purchaseOrderDetails =
                ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in purchaseOrderDetails)
            {
                ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();
                record = index.Value;

                int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.materielBrand;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;

                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Size].Value = record.materielSize;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnit;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeType].Value = record.makeType;
            }

            // 如果单据已审核，则禁用页面所有控件
            if (m_currentOrderInfo.isReview == "1")
            {
                this.labelReviewBillStaff.Text = m_currentOrderInfo.orderrReviewName;
                this.labelReviewDate.Text = m_currentOrderInfo.reviewDate;
                this.panelIsReview.Visible = true;

                this.save.Enabled = false;
                this.toolStripButtonReview.Enabled = false;
                this.dataGridViewDataList.ReadOnly = true;

                this.panelSummary.Visible = false;

                this.textBoxSummary.Visible = false;

                this.panelBusinessPeople.Visible = false;
            }
            else
            {
                this.labelReviewBillStaff.Visible = false;
                this.labelReviewDate.Visible = false;
            }
        }

        private void writeBillDetailsInfoFromBillNumber(string billNumber)
        {
            //// DataGridView 赋值
            //SortedDictionary<int, PurchaseApplyOrderDetailsTable> purchaseOrderDetails =
            //    PurchaseApplyOrderDetails.getInctance().getPurchaseInfoFromBillNumber(billNumber);

            //foreach (KeyValuePair<int, PurchaseApplyOrderDetailsTable> index in purchaseOrderDetails)
            //{
            //    PurchaseApplyOrderDetailsTable record = new PurchaseApplyOrderDetailsTable();
            //    record = index.Value;

            //    int rowIndex = Convert.ToInt32(record.rowNumber.ToString()) - 1;

            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnitPurchase;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Price].Value = record.price;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Turnover].Value = record.sumMoney;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.TransportationCost].Value = 0;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.OtherCost].Value = record.otherCost;
            //    dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.SumTurnover].Value = record.totalMoney;
            //}
        }

        private void setPageActionEnable()
        {
            int authID = 806;

            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(authID);

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

        private void dataGridViewDataListChangeOfter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                if (dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().Length > 0)
                {
                    bool isRet = false;
                    string mateielID = dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString();

                    foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_purchaseOrderDetails)
                    {
                        ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();
                        record = index.Value;

                        if (index.Value.materielID == Convert.ToInt32(mateielID))
                        {
                            isRet = true;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.materielBrand;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Size].Value = record.materielSize;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnit;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.MakeType].Value = record.makeType;

                            break;
                        }
                    }

                    if (!isRet)
                    {
                        MessageBoxExtend.messageWarning("物料[" + dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString() +
                            "]不属于原始单据，请重新输入或选择");
                        m_dateGridVeiwListDataListChange.clearDataGridViewRow(e.RowIndex);
                    }

                }
            }
        }

        private void dataGridViewDataListChangeOfter_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == (int)DataGridColumnName.MatetielNumber)
            {
                //验证DataGridView是否又空的行
                int nullRowNumber = m_dateGridVeiwListDataListChange.getExistNullRow(e.RowIndex);

                if (nullRowNumber != -1)
                {
                    MessageBoxExtend.messageWarning("行号[" + Convert.ToString(nullRowNumber + 1) + "]数据为空，请现在空行中输入");
                    dataGridViewDataListChangeOfter.CurrentCell = this.dataGridViewDataListChangeOfter.Rows[nullRowNumber].Cells[(int)DataGridColumnName.MatetielNumber];

                    return;
                }
            }
        }

        private void setMatetielInfo(int rowIndex, string id)
        {
            // 根据单据编号，得到详细数据
            m_purchaseOrderDetails = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(this.labelSrcOrderBillNum.Text);

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_purchaseOrderDetails)
            {
                ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();
                record = index.Value;

                if (index.Value.materielID == Convert.ToInt32(id))
                {
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.materielBrand;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Size].Value = record.materielSize;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnit;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeType].Value = record.makeType;

                    break;
                }
            }
        }
    }
}