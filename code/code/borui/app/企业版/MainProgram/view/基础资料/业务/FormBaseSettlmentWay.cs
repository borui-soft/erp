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
    public partial class FormBaseSettlmentWay : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";

        public FormBaseSettlmentWay()
        {
            InitializeComponent();
        }

        private void FormBaseSettlmentWay_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("名称", 150);
            m_dateGridViewExtend.addDataGridViewColumn("科目代码", 150);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewBilConfigList);
            updateDataGridView(SettlmentWay.getInctance().getAllSettlmentWayInfo());

            setPageActionEnable();
        }

        private void updateDataGridView(SortedDictionary<int, SettlmentWayTable> settlmentWayList)
        {
            m_dataGridRecordCount = settlmentWayList.Count;

            SortedDictionary<int, ArrayList> settlmentWayLArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < settlmentWayList.Count; i++)
            {
                SettlmentWayTable record = new SettlmentWayTable();
                record = (SettlmentWayTable)settlmentWayList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.name);
                temp.Add(record.subjectID);

                settlmentWayLArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(settlmentWayLArrary);
        }

        private void add_Click(object sender, EventArgs e)
        {
            FormSettlmentWayEdit fswe = new FormSettlmentWayEdit("结算方式添加", true);
            fswe.ShowDialog();

            updateDataGridView(SettlmentWay.getInctance().getAllSettlmentWayInfo());
        }

        private void modify_Click(object sender, EventArgs e)
        {
            FormSettlmentWayEdit fswe = new FormSettlmentWayEdit("结算方式修改", false, m_currentDataGridViedRecordPkey);
            fswe.ShowDialog();

            updateDataGridView(SettlmentWay.getInctance().getAllSettlmentWayInfo());
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordName + "]吗?"))
                {
                    SettlmentWay.getInctance().delete(m_currentDataGridViedRecordPkey);
                    updateDataGridView(SettlmentWay.getInctance().getAllSettlmentWayInfo());
                }
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

        private string toChiness(int value)
        {
            string str = "是";

            if(value == 0)
            {
                str = "否";
            }

            return str;
        }

        private void dataGridViewBilConfigList_Click(object sender, EventArgs e)
        {
            if (m_dataGridRecordCount > 0)
            {
                // 当单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewBilConfigList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewBilConfigList.ColumnCount; j++)
                    {
                        if (dataGridViewBilConfigList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewBilConfigList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewBilConfigList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordName = dataGridViewBilConfigList.Rows[i].Cells[2].Value.ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(709);

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
    }
}
