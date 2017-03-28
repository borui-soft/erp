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
    public partial class FormTransfer : Form
    {
        private string m_billNumber = "";
        private readonly int DateGridVeiwListDataListRowCount = 100;
        private int m_rowIndex = -1, m_columnIndex = -1;

        private string m_projectNum = "";
        private int m_dataType = -1;

        public DataGridViewTextBoxEditingControl CellEdit = null;
        BillDataGridViewExtend m_dateGridVeiwListDataList = new BillDataGridViewExtend();

        // 根据项目跟踪状况，直接形成采购申请单
        public SortedDictionary<int, ArrayList> m_proInfoList = new SortedDictionary<int, ArrayList>();
        public SortedDictionary<int, ArrayList> m_proInfoListUser = new SortedDictionary<int, ArrayList>();

        private enum DataGridColumnName
        {
            RowNum,
            MatetielNumber,
            MatetielName,
            Model,
            Parameter,

            RequestValue,
            TransferValue,
            OtherValue,
            Select
        };

        public FormTransfer(string billNum, string projectNum, int dataType)
        {
            InitializeComponent();
            this.dataGridViewDataList.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };
            m_dataType = dataType;
            m_billNumber = billNum;
            m_projectNum = projectNum;
        }

        private void FormTransfer_Load(object sender, EventArgs e)
        {
            getProInfoList();

            // DataGridView初始化
            dataGridViewInit();

            this.label1.Text = "单据编号[" + m_billNumber + "] 待转物料信息";

            readProInfoListToUI();
        }

        private void dataGridViewInit()
        {
            // 物料资料初始化
            m_dateGridVeiwListDataList.addDataGridViewColumn("NO.", 25, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料ID\n编码", 100, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("物料名称", 200, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("型号", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("参数", 60, true, true);

            m_dateGridVeiwListDataList.addDataGridViewColumn("项目需求量", 95, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("已转数量", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn("待转数量", 60, true, true);
            m_dateGridVeiwListDataList.addDataGridViewColumn(" ", 25, true, true);

            m_dateGridVeiwListDataList.initDataGridViewColumn(this.dataGridViewDataList);
            m_dateGridVeiwListDataList.initDataGridViewData(m_proInfoList.Count);
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
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Model].Value = record.model;
            dataGridViewDataList.Rows[rowIndex].Cells[(int)DataGridColumnName.Parameter].Value = record.materielParameter;
        }

        private void readProInfoListToUI()
        {

            for (int index = 0; index < m_proInfoList.Count; index++)
            {
                ArrayList record = new ArrayList();

                record = m_proInfoList[index];

                setMatetielInfoToDataGridView(Convert.ToString(record[1]), index);
                
                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.RequestValue].Value = Convert.ToDouble(record[2]);
                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.TransferValue].Value = Convert.ToDouble(record[3]);
                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.OtherValue].Value = Convert.ToDouble(record[2]) - Convert.ToDouble(record[3]);

                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.Select] =  new DataGridViewCheckBoxCell();
            }
        }

        private void getProInfoList()
        {
            m_proInfoList.Clear();

            // 根据单据,得到单据详细信息
            SortedDictionary<int, ProjectManagerDetailsTable> listDetails = new SortedDictionary<int, ProjectManagerDetailsTable>();
            listDetails = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(m_billNumber);

            for (int index = 0; index < listDetails.Count; index++)
            {
                ArrayList record = new ArrayList();

                ProjectManagerDetailsTable tmp = new ProjectManagerDetailsTable();
                tmp = (ProjectManagerDetailsTable)listDetails[index];

                double proValue = 0.0;

                if (m_dataType == 1)
                {
                    proValue = MaterielProOccupiedOrderDetails.getInctance().getMaterielProCountInfoFromProject(tmp.materielID, m_projectNum);
                }
                else if (m_dataType == 2)
                {
                    proValue = PurchaseApplyOrderDetails.getInctance().getPurchaseValueFromProjectNumber(m_projectNum, tmp.materielID);
                }
                else if (m_dataType == 3)
                {
                    proValue = MaterielOutOrderDetails.getInctance().getMaterielCountInfoFromProject(m_projectNum, tmp.materielID);
                }

                if (tmp.value - proValue > 0)
                {
                    record.Add(m_projectNum);
                    record.Add(tmp.materielID);
                    record.Add(tmp.value);
                    record.Add(proValue);

                    m_proInfoList.Add(m_proInfoList.Count, record);
                }
            }

            if (m_proInfoList.Count == 0)
            {
                this.button1.Enabled = false;
            }

            this.label1.Text += ", 共有[" + Convert.ToString(m_proInfoList.Count) + "]条数据";
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

        private void dataGridViewDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)DataGridColumnName.Select)
            {
                if (Convert.ToBoolean(dataGridViewDataList.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Select].Value))
                {
                    dataGridViewDataList.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Select].Value = false;
                }
                else
                {
                    dataGridViewDataList.Rows[e.RowIndex].Cells[(int)DataGridColumnName.Select].Value = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < m_proInfoList.Count; index++)
            {
                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.Select].Value = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < m_proInfoList.Count; index++)
            {
                dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.Select].Value = true;
            }
        }

        // 确定
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_proInfoListUser.Clear();

            for (int index = 0; index < m_proInfoList.Count; index++)
            {
                if (Convert.ToBoolean(dataGridViewDataList.Rows[index].Cells[(int)DataGridColumnName.Select].Value))
                {
                    m_proInfoListUser.Add(m_proInfoListUser.Count, m_proInfoList[index]);
                }
            }

            if (m_proInfoListUser.Count > 12)
            {
                MessageBoxExtend.messageWarning("选择的数据条目不得超过12条, 请重新选择");

                return;
            }
            
            if (m_proInfoListUser.Count == 0)
            {
                MessageBoxExtend.messageWarning("未选择任何数据, 请重新选择");
                return;
            }

            if (m_dataType == 1)
            {
                FormMaterielProOccupied fmpo = new FormMaterielProOccupied("", m_proInfoListUser);
                fmpo.ShowDialog();
            }
            else if (m_dataType == 2)
            {
                FormPurchaseApply fmpo = new FormPurchaseApply("", m_proInfoListUser);
                fmpo.ShowDialog();
            }
            else if (m_dataType == 3)
            {
                FormMaterielOutOrder fmpo = new FormMaterielOutOrder("", m_proInfoListUser);
                fmpo.ShowDialog();
            }

            this.Close();
        }
    }
}