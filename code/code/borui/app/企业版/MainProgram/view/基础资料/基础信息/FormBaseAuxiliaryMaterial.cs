using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using TIV.Core.DatabaseAccess;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public partial class FormBaseAuxiliaryMaterial : Form
    {
        private int m_recordCount = 0;

        private int m_groupPkey = 0;
        private string m_groupName = "";

        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";
        private string m_currentDataGridViedRecordDesc = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        private ArrayList m_tables = new ArrayList();

        // 双击页面DataGridView,是否关闭窗口，并记录选中记录的信息
        private bool m_isAddRecordMode = false;

        public FormBaseAuxiliaryMaterial(ArrayList tables = null, string winText = "", bool isAddRecordMode = false)
        {
            InitializeComponent();
            m_tables = tables;
            m_isAddRecordMode = isAddRecordMode;

            if(winText.Length != 0)
            {
                this.Text = winText;
            }
        }

        private void FormBaseCustomer_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dataGridViewExtend.addDataGridViewColumn("名称", 150);
            m_dataGridViewExtend.addDataGridViewColumn("描述", 200);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewAuxiliaryMaterialList);
            updateDataGridView(AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData(
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey))
                );

            setPageActionEnable();
        }

        private void updateDataGridView(SortedDictionary<int, AuxiliaryMaterialDataTable> customerList)
        {
            m_recordCount = customerList.Count;
            this.labelCustomerGroupName.Text = "[" + m_groupName + "]资料共计[" + Convert.ToString(m_recordCount) + "]条记录";

            SortedDictionary<int, ArrayList> customers = new SortedDictionary<int, ArrayList>();

            foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index2 in customerList)
           {
                ArrayList temp = new ArrayList();

                temp.Add(index2.Value.pkey);
                temp.Add(index2.Value.name);
                temp.Add(index2.Value.desc);

                customers.Add(customers.Count, temp);
           }

            m_dataGridViewExtend.initDataGridViewData(customers);
        }

        private void refreshTreeView()
        {
            m_tree = new TreeViewExtend(this.treeView);
            m_rootNode = this.treeView.Nodes.Add(Convert.ToString(0), "辅助资料");

            SortedDictionary<int, AuxiliaryMaterialTable> records = AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialInfo();

            foreach (KeyValuePair<int, AuxiliaryMaterialTable> index in records)
            {
                AuxiliaryMaterialTable record = new AuxiliaryMaterialTable();
                record = index.Value;
                if(isNeedDisplay(record.tableName))
                {
                    m_tree.addNode(m_rootNode, record.nodeName, 0, 1, Convert.ToString(record.pkey));
                }
            }

            m_rootNode.Expand();
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form fame = new FormAuxiliaryMaterialEdit(
                    AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey), 
                    m_currentDataGridViedRecordPkey, 
                    m_groupName + "信息编辑", 
                    true);

            fame.ShowDialog();

            updateDataGridView(AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData(
                AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey)));
        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                Form fame = new FormAuxiliaryMaterialEdit(
                    AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey),
                    m_currentDataGridViedRecordPkey, 
                    m_groupName + "信息编辑", 
                    false,
                    m_currentDataGridViedRecordName,
                    m_currentDataGridViedRecordDesc);

                fame.ShowDialog();

                updateDataGridView(AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData(
                    AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey)));
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordName + "]吗?"))
                {
                    string tableName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey);
                    AuxiliaryMaterial.getInctance().delete(m_currentDataGridViedRecordPkey, tableName);

                    updateDataGridView(AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData(
                        AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey)));
                }
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_recordCount > 0)
            {
                this.saveFileDialog1.Filter = "Excel 2007格式 (*.xlsx)|*.xlsx|Excel 2003格式 (*.xls)|*.xls";
                this.saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_dataGridViewExtend.dataGridViewExportToExecl(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据为空，无数据可导出!");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            m_dataGridViewExtend.printDataGridView(m_groupName + "资料");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewCustomerOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Nodes.Count >= 0)
            {
                this.treeView.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                m_groupPkey = Convert.ToInt32(this.treeView.SelectedNode.Name.ToString());
                m_groupName = this.treeView.SelectedNode.Text.ToString();

                updateDataGridView(AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData(
                    AuxiliaryMaterial.getInctance().getAuxiliaryMaterialTableNameFromPkey(m_groupPkey))
                    );
            }
            else
            {
                m_groupPkey = 0;
            }
        }

        private void treeViewCustomerOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Nodes.Count >= 0)
            {
                this.treeView.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void dataGridViewCustomerList_Click(object sender, EventArgs e)
        {
            if (m_recordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewAuxiliaryMaterialList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewAuxiliaryMaterialList.ColumnCount; j++)
                    {
                        if (dataGridViewAuxiliaryMaterialList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewAuxiliaryMaterialList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewAuxiliaryMaterialList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordName = dataGridViewAuxiliaryMaterialList.Rows[i].Cells[1].Value.ToString();
                            m_currentDataGridViedRecordDesc = dataGridViewAuxiliaryMaterialList.Rows[i].Cells[2].Value.ToString();
                        }
                    }
                }
            }
        }

        private void dataGridViewAuxiliaryMaterialList_DoubleClick(object sender, EventArgs e)
        {
            if (m_recordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewAuxiliaryMaterialList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewAuxiliaryMaterialList.ColumnCount; j++)
                    {
                        if (dataGridViewAuxiliaryMaterialList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewAuxiliaryMaterialList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewAuxiliaryMaterialList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordName = dataGridViewAuxiliaryMaterialList.Rows[i].Cells[1].Value.ToString();
                            m_currentDataGridViedRecordDesc = dataGridViewAuxiliaryMaterialList.Rows[i].Cells[2].Value.ToString();

                            // m_isAddRecordMode等于true时，说明窗口处于记录添加模式模式，双击后需要记录选中的信息，并关闭该窗口
                            if (m_isAddRecordMode)
                            {
                                this.Close();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void dataGridViewCustomerList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_recordCount))
            {
                for (int i = 0; i < this.dataGridViewAuxiliaryMaterialList.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dataGridViewAuxiliaryMaterialList.ColumnCount; j++)
                    {
                        this.dataGridViewAuxiliaryMaterialList.Rows[i].Cells[j].Selected = false;
                    }
                }

                dataGridViewAuxiliaryMaterialList.Rows[e.RowIndex].Selected = true;
                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewAuxiliaryMaterialList.Rows[e.RowIndex].Cells[0].Value.ToString());
                m_currentDataGridViedRecordName = dataGridViewAuxiliaryMaterialList.Rows[e.RowIndex].Cells[1].Value.ToString();
                m_currentDataGridViedRecordDesc = dataGridViewAuxiliaryMaterialList.Rows[e.RowIndex].Cells[2].Value.ToString();

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private bool isNeedDisplay(string tableName)
        {
            bool isRet = false;

            if (m_tables == null || m_tables.Count == 0)
            {
                isRet = true;
            }
            else
            {
                isRet = m_tables.Contains((string)tableName);
            } 
            return isRet;
        }

        public int getSelectRecordPkey()
        {
            return m_currentDataGridViedRecordPkey;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(704);

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