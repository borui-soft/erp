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
    public partial class FormInitMateriel : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";

        public FormInitMateriel()
        {
            InitializeComponent();
        }

        private void FormInitMateriel_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("物料编号", 80);
            m_dateGridViewExtend.addDataGridViewColumn("物料名称", 150);
            m_dateGridViewExtend.addDataGridViewColumn("计价单位", 150);
            m_dateGridViewExtend.addDataGridViewColumn("收料仓库", 150);
            m_dateGridViewExtend.addDataGridViewColumn("单价", 100);
            m_dateGridViewExtend.addDataGridViewColumn("数量", 100);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);
            updateDataGridView(InitMateriel.getInctance().getAllInitMaterielInfo());

            setPageActionEnable();
        }

        private void updateDataGridView(SortedDictionary<int, InitMaterielTable> settlmentWayList)
        {
            m_dataGridRecordCount = settlmentWayList.Count;

            SortedDictionary<int, ArrayList> settlmentWayLArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < settlmentWayList.Count; i++)
            {
                InitMaterielTable record = new InitMaterielTable();
                record = (InitMaterielTable)settlmentWayList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.materielID);
                temp.Add(record.name);
                temp.Add(record.unitPurchase);
                temp.Add(record.storage);
                temp.Add(record.price);
                temp.Add(record.value);

                settlmentWayLArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(settlmentWayLArrary);
        }

        private void add_Click(object sender, EventArgs e)
        {
            // shengguang++ 2016-11-05 当业务启动后，增加功能：不能再直接增加物料库存信息
            if (InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                MessageBoxExtend.messageWarning("业务系统已启用，不能再直接添加物料库存信息，请通过物料入库方式完成此操作");
                return;
            }
            // ++shengguang

            FormInitMaterielEdit fime = new FormInitMaterielEdit("添加物料初始资料", false);
            fime.ShowDialog();

            updateDataGridView(InitMateriel.getInctance().getAllInitMaterielInfo());
        }

        private void modify_Click(object sender, EventArgs e)
        {
            FormInitMaterielEdit fime = new FormInitMaterielEdit("编辑物料初始资料", true, m_currentDataGridViedRecordPkey);
            fime.ShowDialog();

            updateDataGridView(InitMateriel.getInctance().getAllInitMaterielInfo());
        }

        private void delete_Click(object sender, EventArgs e)
        {
            // shengguang++ 2016-11-05 当业务启动后，增加功能：不能再直接删除物料库存信息
            if (InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                MessageBoxExtend.messageWarning("业务系统已启用，不能再直接删除物料库存信息，请通过物料出库方式完成此操作");
                return;
            }
            // ++shengguang

            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordName + "]库存信息吗?"))
                {
                    InitMateriel.getInctance().delete(m_currentDataGridViedRecordPkey);
                    updateDataGridView(InitMateriel.getInctance().getAllInitMaterielInfo());
                }
            }
        }

        private void import_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("批量导入数据会清空数据库所有已存在库存记录，是否继续？"))
            {
                // 指定OpenFileDialog控件打开的文件格式
                openFileDialog1.Filter = "导入文件(*.xls)|*.xls";
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    InitMateriel.getInctance().fileImport(openFileDialog1.FileName);
                    updateDataGridView(InitMateriel.getInctance().getAllInitMaterielInfo());
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

        private void dataGridViewBilConfigList_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dataGridRecordCount > 0)
                {
                    // 当单击某个单元格时，自动选择整行
                    for (int i = 0; i < this.dataGridViewMaterielList.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridViewMaterielList.ColumnCount; j++)
                        {
                            if (dataGridViewMaterielList.Rows[i].Cells[j].Selected)
                            {
                                dataGridViewMaterielList.Rows[i].Selected = true;
                                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[0].Value.ToString());
                                m_currentDataGridViedRecordName = dataGridViewMaterielList.Rows[i].Cells[2].Value.ToString();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
 
            }
        }

        private void setPageActionEnable()
        {
            if (InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(401);

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
            else 
            {
                SortedDictionary<int, ActionTable> list2 = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(601);

                foreach (KeyValuePair<int, ActionTable> index in list2)
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
}
