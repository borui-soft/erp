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
    public partial class FormBaseBillConfig : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";

        public FormBaseBillConfig()
        {
            InitializeComponent();
        }

        private void FormBaseBillConfig_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("单据类型", 100, false);
            m_dateGridViewExtend.addDataGridViewColumn("单据名称", 130);
            m_dateGridViewExtend.addDataGridViewColumn("编码规则", 150);
            m_dateGridViewExtend.addDataGridViewColumn("是否允许输入", 110);
            m_dateGridViewExtend.addDataGridViewColumn("保存时自动审核", 120);
            m_dateGridViewExtend.addDataGridViewColumn("是否使用编码规则", 130);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewBilConfigList);
            updateDataGridView(BillConfig.getInctance().getAllBillConfigInfo());

            setPageActionEnable();
        }

        private void updateDataGridView(SortedDictionary<int, BillConfigTable> billConfigList)
        {
            m_dataGridRecordCount = billConfigList.Count;

            SortedDictionary<int, ArrayList> billConfigLArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < billConfigList.Count; i++)
            {
                BillConfigTable record = new BillConfigTable();
                record = (BillConfigTable)billConfigList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.billpType);
                temp.Add(record.name);
                temp.Add(record.code);

                temp.Add(toChiness(record.isInput));
                temp.Add(toChiness(record.isAutoSave));
                temp.Add(toChiness(record.isUseRules));

                billConfigLArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(billConfigLArrary, 3);
        }

        private void modify_Click(object sender, EventArgs e)
        {
            FormBillConfigEdit fbce = new FormBillConfigEdit(m_currentDataGridViedRecordName + "编辑", m_currentDataGridViedRecordPkey);
            fbce.ShowDialog();

            updateDataGridView(BillConfig.getInctance().getAllBillConfigInfo());
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
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(708);

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
