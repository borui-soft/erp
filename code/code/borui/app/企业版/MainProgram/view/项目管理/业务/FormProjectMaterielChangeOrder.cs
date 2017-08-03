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
        private readonly int BillTypeNumber = 54;
        private readonly int DateGridVeiwListDataListRowCount = 12;
        private int m_rowIndex = -1, m_columnIndex = -1;
        private bool m_isSaveSuccessFul = false;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();
        BillDataGridViewExtend m_dateGridVeiwListDataListChange = new BillDataGridViewExtend();
        FormProjectMaterielChangeTable m_currentOrderInfo = new FormProjectMaterielChangeTable();

        SortedDictionary<int, ProjectManagerDetailsTable> m_purchaseOrderDetails = new SortedDictionary<int, ProjectManagerDetailsTable>();

        public enum DataGridColumnName
        {
            RowNum,         //行号
            MatetielNumber, //物料编码

            Num,             //序号
            Sequence,        //序列号

            DeviceName,     //所属部件
            Brand,          //品牌
            MatetielName,   //物料名称

            Model,          //规格型号
            Size,           //尺寸
            CL,             //材料
            Parameter,      //参数
            Unit,           //单位

            Value,          //数量
            MakeType,       //制造类型
            UseDate,        //使用日期
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

                FormProjectMaterielTable tmp = FormProject.getInctance().getProjectInfoFromBillNumber(this.labelSrcOrderBillNum.Text);
                this.labelProjectName.Text = tmp.projectName;
                this.labelProjectNo.Text = tmp.projectNum;
                this.labelMakeNo.Text = tmp.makeNum;
                this.labelDevMode.Text = tmp.deviceMode;
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
            m_dateGridVeiwListDataList.addDataGridViewColumn("行号", 40, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\\编码(*)", 70, true, false);

            if (m_tablesType == 1)
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("序号(*)", 40, true, false);
                m_dateGridVeiwListDataList.addDataGridViewColumn("序列号(*)", 50, true, false);
                m_dateGridVeiwListDataList.addDataGridViewColumn("所属部件(*)", 95, true, true);
            }
            else
            {
                m_dateGridVeiwListDataList.addDataGridViewColumn("序号(*)", 40, true, false);
                m_dateGridVeiwListDataList.addDataGridViewColumn("序列号(*)", 50, false, false);
                m_dateGridVeiwListDataList.addDataGridViewColumn("所属部件(*)", 145, true, true);
            }

            m_dateGridVeiwListDataList.addDataGridViewColumn("品牌", 40, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称(*)", 85, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 规格\n 型号", 50, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("尺寸", 40, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("材料", 40, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("参数", 40, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" 基本\n 单位", 50, true, true);

            m_dateGridVeiwListDataList.addDataGridViewColumn("数量(*)", 50, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("制作方式", 70, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("使用日期(*)", 70, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("备注", 50, true, true);

            m_dateGridVeiwListDataList.initDataGridViewColumn(this.dataGridViewDataListChangeOfter);
            m_dateGridVeiwListDataList.initDataGridViewData(DateGridVeiwListDataListRowCount);


            // 变更后单据初始化
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("行号", 40, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("物料ID\\编码(*)", 70, true, false);

            if (m_tablesType == 1)
            {
                m_dateGridVeiwListDataListChange.addDataGridViewColumn("序号(*)", 40, true, false);
                m_dateGridVeiwListDataListChange.addDataGridViewColumn("序列号(*)", 50, true, false);
                m_dateGridVeiwListDataListChange.addDataGridViewColumn("所属部件(*)", 95, true, false);
            }
            else
            {
                m_dateGridVeiwListDataListChange.addDataGridViewColumn("序号(*)", 40, true, false);
                m_dateGridVeiwListDataListChange.addDataGridViewColumn("序列号(*)", 50, false, false);
                m_dateGridVeiwListDataListChange.addDataGridViewColumn("所属部件(*)", 145, true, false);
            }

            m_dateGridVeiwListDataListChange.addDataGridViewColumn("品牌", 40, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("物料名称(*)", 85, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn(" 规格\n 型号", 50, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("尺寸", 40, true, false);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("材料", 40, true, false);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("参数", 40, true, true);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn(" 基本\n 单位", 50, true, true);

            m_dateGridVeiwListDataListChange.addDataGridViewColumn("数量(*)", 50, true, false);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("制作方式", 70, true, false);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("使用日期(*)", 70, true, false);
            m_dateGridVeiwListDataListChange.addDataGridViewColumn("变更原因", 100, true, false);

            m_dateGridVeiwListDataListChange.initDataGridViewColumn(this.dataGridViewDataListChange);
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
            this.ActiveControl = this.toolStrip1;

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
                    BillNumber.getInctance().inserBillNumber(BillTypeNumber, this.labelMakeDate.Text, this.labelBillNumber.Text.ToString());

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
                    m_currentOrderInfo.materielIDs += dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString();
                    m_currentOrderInfo.materielIDs += ",";
                    m_currentOrderInfo.materielIDs += dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Num].Value.ToString();
                    m_currentOrderInfo.materielIDs += ",";
                    m_currentOrderInfo.materielIDs += dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Sequence].Value.ToString();
                    m_currentOrderInfo.materielIDs += "#";
                }
            }
        }

        private bool purchaseOrderIsFull(FormProjectMaterielChangeTable record)
        {
            //if (record.changeReason.Length <= 0)
            //{
            //    MessageBoxExtend.messageWarning("变更原因不得为空，请重新输入");
            //    return false;
            //}

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
                if (this.dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }
                else
                {
                    ProjectManagerDetailsTable currentRowInfo = new ProjectManagerDetailsTable();

                    currentRowInfo.billNumber = this.labelBillNumber.Text;
                    currentRowInfo.rowNumber = Convert.ToInt32(dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.RowNum].Value.ToString());

                    currentRowInfo.no = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Num].Value.ToString();
                    currentRowInfo.sequence = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Sequence].Value.ToString();
                    currentRowInfo.useDate = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.UseDate].Value.ToString();
                    currentRowInfo.deviceName = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.DeviceName].Value.ToString();
                    currentRowInfo.materielSize = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Size].Value.ToString();
                    currentRowInfo.cl = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.CL].Value.ToString();

                    currentRowInfo.materielID = Convert.ToInt32(dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value.ToString());
                    currentRowInfo.value = Convert.ToDouble(dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value.ToString());
                    currentRowInfo.makeType = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeType].Value.ToString();
                    currentRowInfo.materielNote = dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Note].Value.ToString();

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

                if (record.no.Length == 0)
                {
                    MessageBoxExtend.messageWarning("第[" + record.rowNumber + "]信息中物料序号不能为空");
                    isRet = false;
                    break;
                }

                if (m_tablesType == 1 && record.sequence.Length == 0)
                {
                    MessageBoxExtend.messageWarning("第[" + record.rowNumber + "]信息中物料序列号不能为空");
                    isRet = false;
                    break;
                }

                if (record.useDate.Length == 0)
                {
                    MessageBoxExtend.messageWarning("第[" + record.rowNumber + "]信息中物料使用日期不能为空");
                    isRet = false;
                    break;
                }

                if (record.deviceName.Length == 0)
                {
                    MessageBoxExtend.messageWarning("第[" + record.rowNumber + "]信息中物料所属部件不能为空");
                    isRet = false;
                    break;
                }

                if (record.value == 0)
                {
                    if (!MessageBoxExtend.messageQuestion("第[" + record.rowNumber + "]行信息中物料数量为0, 确实变更？"))
                    {
                        isRet = false;
                        break;
                    }
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
            // PrintBmpFile.getInctance().printCurrentWin(Width, Height, this.Location.X, this.Location.Y, true);
            if (m_billNumber.Length > 0)
            {
                FormOrderPrint fop = new FormOrderPrint(BillTypeNumber, m_billNumber, this.dataGridViewDataListChange, this.dataGridViewDataListChangeOfter);
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
                int nullRowNumber = m_dateGridVeiwListDataListChange.getExistNullRow(e.RowIndex);

                if (nullRowNumber != -1)
                {
                    MessageBoxExtend.messageWarning("行号[" + Convert.ToString(nullRowNumber + 1) + "]数据为空，请现在空行中输入");
                    dataGridViewDataListChange.CurrentCell = this.dataGridViewDataListChange.Rows[nullRowNumber].Cells[(int)DataGridColumnName.MatetielNumber];

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
                if (dataGridViewDataListChange.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString().Length > 0)
                {
                    setMatetielInfoToDataGridView(dataGridViewDataListChange.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString());
                }
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
                        MessageBoxExtend.messageWarning("[" + dataGridViewDataListChange.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString() +
                            "]不存在，请重新输入或选择");
                        m_dateGridVeiwListDataList.clearDataGridViewRow(m_rowIndex);

                        return;
                    }
                }
                catch
                {
                    MessageBoxExtend.messageWarning("[" + dataGridViewDataListChange.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString() +
                        "]不存在，请重新输入或选择");
                    m_dateGridVeiwListDataList.clearDataGridViewRow(m_rowIndex);

                    return;
                }
            }

            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.pkey;
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.name;
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.brand;
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.model;
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Size].Value = "";
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Unit].Value =
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", record.unitPurchase);
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.Value].Value = "0";
            dataGridViewDataListChange.Rows[m_rowIndex].Cells[(int)DataGridColumnName.UseDate].Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void dataGridViewDataList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; // 绑定到事件
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_columnIndex != (int)DataGridColumnName.MakeType &&
                m_columnIndex != (int)DataGridColumnName.Note &&
                m_columnIndex != (int)DataGridColumnName.CL &&
                m_columnIndex != (int)DataGridColumnName.Size &&
                m_columnIndex != (int)DataGridColumnName.UseDate &&
                m_columnIndex != (int)DataGridColumnName.DeviceName && 
                m_columnIndex != (int)DataGridColumnName.MatetielNumber)
            {
                e.Handled = m_dateGridVeiwListDataList.isValidDataGridViewCellValue(e.KeyChar,
                    this.dataGridViewDataListChange.Rows[m_rowIndex].Cells[m_columnIndex].EditedFormattedValue.ToString());
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

            FormProjectMaterielTable tmp = FormProject.getInctance().getProjectInfoFromBillNumber(m_currentOrderInfo.srcBillNumber);
            this.labelProjectName.Text = tmp.projectName;
            this.labelProjectNo.Text = tmp.projectNum;
            this.labelMakeNo.Text = tmp.makeNum;
            this.labelDevMode.Text = tmp.deviceMode;

            this.labelSummary.Text = m_currentOrderInfo.changeReason;

            this.labelMakeBillStaff.Text = m_currentOrderInfo.makeOrderStaffName;
            this.labelMakeDate.Text = m_currentOrderInfo.makeOrderDate;
            this.labelBusinessPeople.Text = m_currentOrderInfo.designStaffName;
            this.textBoxBusinessPeople.Text = m_currentOrderInfo.designStaffName;
            this.labelReviewBillStaff.Text = m_currentOrderInfo.orderrReviewName;
            this.labelReviewDate.Text = m_currentOrderInfo.reviewDate;

            // 变更前DataGridView 赋值
            string matetiels = m_currentOrderInfo.materielIDs;
            string[] sArray = matetiels.Split('#');
            int rowNum = 0;
            foreach (string index in sArray)
            {
                if (index.ToString().Length > 0)
                {
                    string[] list = index.Split(',');

                    if (list.Length > 1)
                    {
                        setMatetielInfo(rowNum, list[0], list[1], list[2]);
                    }
                    else
                    {
                        setMatetielInfo(rowNum, list[0], "", "");
                    }

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

                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.materielBrand;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;

                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Size].Value = record.materielSize;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnit;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeType].Value = record.makeType;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Note].Value = record.materielNote;


                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Num].Value = record.no;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.Sequence].Value = record.sequence;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.UseDate].Value = record.useDate;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.DeviceName].Value = record.deviceName;
                dataGridViewDataListChange.Rows[rowIndex].Cells[(int)DataGridColumnName.CL].Value = record.cl;
            }

            // 如果单据已审核，则禁用页面所有控件
            if (m_currentOrderInfo.isReview == "1")
            {
                this.labelReviewBillStaff.Text = m_currentOrderInfo.orderrReviewName;
                this.labelReviewDate.Text = m_currentOrderInfo.reviewDate;
                this.panelIsReview.Visible = true;

                this.save.Enabled = false;
                this.toolStripButtonReview.Enabled = false;
                this.dataGridViewDataListChange.ReadOnly = true;

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
            if (e.ColumnIndex == (int)DataGridColumnName.MatetielNumber ||
                e.ColumnIndex == (int)DataGridColumnName.Num ||
                e.ColumnIndex == (int)DataGridColumnName.Sequence )
            {

                string mateielID = dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.MatetielNumber].EditedFormattedValue.ToString();
                string num = dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Num].EditedFormattedValue.ToString();
                string sequence = dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Sequence].EditedFormattedValue.ToString();

                if (mateielID.Length > 0)
                {
                    bool isRet = false;

                    foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_purchaseOrderDetails)
                    {
                        ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();
                        record = index.Value;

                        if (index.Value.materielID == Convert.ToInt32(mateielID))
                        {
                            if (e.ColumnIndex == (int)DataGridColumnName.Num && num.Length > 0 && index.Value.no != num)
                            {
                                continue;
                            }

                            if (e.ColumnIndex == (int)DataGridColumnName.Sequence && sequence.Length > 0 && index.Value.sequence != sequence)
                            {
                                continue;
                            }

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
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Note].Value = record.materielNote;


                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Num].Value = record.no;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Sequence].Value = record.sequence;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.UseDate].Value = record.useDate;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.DeviceName].Value = record.deviceName;
                            dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[(int)DataGridColumnName.CL].Value = record.cl;

                            break;
                        }
                    }

                    if (!isRet)
                    {
                        MessageBoxExtend.messageWarning("物料[" + dataGridViewDataListChangeOfter.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString() +
                            "]不属于原始单据数据，请重新输入或选择");
                        m_dateGridVeiwListDataList.clearDataGridViewRow(e.RowIndex);
                    }

                }
            }
        }

        private void setMatetielInfo(int rowIndex, string id, string num, string sequence)
        {
            // 根据单据编号，得到详细数据
            m_purchaseOrderDetails = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(this.labelSrcOrderBillNum.Text);

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_purchaseOrderDetails)
            {
                ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();
                record = index.Value;

                if (index.Value.materielID == Convert.ToInt32(id))
                {
                    if (sequence.Length > 0 && index.Value.sequence != sequence)
                    {
                        continue;
                    }

                    if (num.Length > 0 && index.Value.no != num)
                    {
                        continue;
                    }

                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielNumber].Value = record.materielID;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Brand].Value = record.materielBrand;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MatetielName].Value = record.materielName;

                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.materielModel;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Size].Value = record.materielSize;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Unit].Value = record.materielUnit;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Value].Value = record.value;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.MakeType].Value = record.makeType;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Note].Value = record.materielNote;

                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Num].Value = record.no;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.Sequence].Value = record.sequence;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.UseDate].Value = record.useDate;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.DeviceName].Value = record.deviceName;
                    dataGridViewDataListChangeOfter.Rows[rowIndex].Cells[(int)DataGridColumnName.CL].Value = record.cl;

                    break;
                }
            }
        }
    }
}