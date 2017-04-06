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
using TIV.Core.TivLogger;

namespace MainProgram
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public partial class FormBaseMateriel : Form
    {
        private int m_materielRecordCount = 0;

        private int m_materielGroupPkey = 1;
        private string m_materielGroupName = "";

        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordCompanyName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        private bool m_isAddRecordMode;

        public FormBaseMateriel(bool isAddRecordMode = false)
        {
            InitializeComponent();
            m_isAddRecordMode = isAddRecordMode;
        }

        private void FormBaseMateriel_Load(object sender, EventArgs e)
        {
            // 树控件初始化 
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dataGridViewExtend.addDataGridViewColumn("物料组", 200, false);
            m_dataGridViewExtend.addDataGridViewColumn("物料名称", 200);
            m_dataGridViewExtend.addDataGridViewColumn("物料编号", 100);
            m_dataGridViewExtend.addDataGridViewColumn("物料简称", 100);
            m_dataGridViewExtend.addDataGridViewColumn("助记码", 100);
            m_dataGridViewExtend.addDataGridViewColumn("规格型号", 100);
            m_dataGridViewExtend.addDataGridViewColumn("品牌", 60);
            m_dataGridViewExtend.addDataGridViewColumn("参数", 60);
            m_dataGridViewExtend.addDataGridViewColumn("收料仓库", 100);
            //m_dataGridViewExtend.addDataGridViewColumn("物料属性", 100);
            m_dataGridViewExtend.addDataGridViewColumn("计价方式", 100);
            m_dataGridViewExtend.addDataGridViewColumn("基本单位", 100);
            m_dataGridViewExtend.addDataGridViewColumn("存货上限", 100);
            m_dataGridViewExtend.addDataGridViewColumn("存货下限", 100);
            m_dataGridViewExtend.addDataGridViewColumn("保质期", 100);
            m_dataGridViewExtend.addDataGridViewColumn("备注", 100);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);

            updateDataGridView(Materiel.getInctance().getAllMaterielInfo());
            setPageActionEnable();

            ToolStripMenuItemAddMateriel.Enabled = this.add.Enabled;
            ToolStripMenuItemModifyMateriel.Enabled = this.modify.Enabled;
            ToolStripMenuItemDeleteMateriel.Enabled = this.delete.Enabled;
            ToolStripMenuItemForbidMateriel.Enabled = this.forbid.Enabled;
            ToolStripMenuItemNoForbidMateriel.Enabled = this.noForbid.Enabled;
        }

        private void updateDataGridView(SortedDictionary<int, MaterielTable> materielList)
        {
            m_materielRecordCount = materielList.Count;
            this.labelMaterielGroupName.Text = "[" + m_materielGroupName + "]物料共计[" + Convert.ToString(m_materielRecordCount) + "]条记录";

            SortedDictionary<int, ArrayList> materiels = new SortedDictionary<int, ArrayList>();


            SortedDictionary<int, AuxiliaryMaterialDataTable> AuxiliaryMaterialList =
                AuxiliaryMaterial.getInctance().getAuxiliaryListFromTableName("BASE_UNIT_LIST");

            SortedDictionary<int, AuxiliaryMaterialDataTable> AuxiliaryStorelList =
                AuxiliaryMaterial.getInctance().getAuxiliaryListFromTableName("BASE_STORAGE_LIST");

            for (int i = 0; i < materielList.Count; i++)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = (MaterielTable)materielList[i];

                ArrayList temp = new ArrayList();
                temp.Add(materiel.pkey);
                temp.Add(materiel.materielType);
                temp.Add(materiel.name);
                temp.Add(materiel.num);
                temp.Add(materiel.nameShort);
                temp.Add(materiel.mnemonicCode);
                temp.Add(materiel.model);
                temp.Add(materiel.brand);
                temp.Add(materiel.materielParameter);

                if (AuxiliaryStorelList.ContainsKey(materiel.storage))
                {
                    temp.Add(AuxiliaryStorelList[materiel.storage].name);
                }
                else
                {
                    temp.Add("");
                }

                temp.Add("移动加权平均");

                if (AuxiliaryMaterialList.ContainsKey(materiel.unit))
                {
                    temp.Add(AuxiliaryMaterialList[materiel.unit].name);
                }
                else
                {
                    temp.Add("");
                }
                temp.Add(materiel.max);
                temp.Add(materiel.min);
                temp.Add(materiel.warramty);
                temp.Add(materiel.note);
                materiels.Add(materiels.Count, temp);
            }

            m_dataGridViewExtend.initDataGridViewData(materiels, 4);
        }

        private void refreshTreeView()
        {
            if (m_tree !=null && treeViewMaterielOrg.Nodes.Count > 0)
            {
                treeViewMaterielOrg.Nodes.Clear();
            }

            m_tree = new TreeViewExtend(this.treeViewMaterielOrg);
            int rootNodePkey = MaterielOrgStruct.getInctance().getRootNodePkey();

            int rooNodeValue = MaterielOrgStruct.getInctance().getNoteValueFromPkey(rootNodePkey);
            string rootName = MaterielType.getInctance().getMaterielTypeNameFromPkey(rooNodeValue);

            m_rootNode = this.treeViewMaterielOrg.Nodes.Add(Convert.ToString(rooNodeValue), rootName);

            processDutyOrgNode(rootNodePkey, m_rootNode);
            m_rootNode.Expand();
        }

        private void processDutyOrgNode(int parentID, TreeNode node)
        {
            string nodeName = "";
            TreeNode currentNode;
            ArrayList nodeList = MaterielOrgStruct.getInctance().getNodesFormParentID(parentID);

            for (int i = 0; i < nodeList.Count; i++)
            {
                MaterielOrgStructTable record = (MaterielOrgStructTable)nodeList[i];

                nodeName = MaterielType.getInctance().getMaterielTypeNameFromPkey(record.value);

                string groupNum = MaterielType.getInctance().getMaterielTypeNumFromPkey(record.value);

                if (groupNum.Length > 0)
                {
                    nodeName += "(" + groupNum + ")";
                }

                currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.value));
                processDutyOrgNode(record.pkey, currentNode);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form fse = new FormMaterielEdit(true, m_materielGroupPkey);
            fse.ShowDialog();

            refreshTreeView();
            updateDataGridView(getCurrentNodeAllChildNodesMateriel());
        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                Form fse = new FormMaterielEdit(false, m_materielGroupPkey, true, m_currentDataGridViedRecordPkey);
                fse.ShowDialog();

                updateDataGridView(getCurrentNodeAllChildNodesMateriel());
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Materiel.getInctance().delete(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesMateriel());
                }
            }
        }

        private void forbid_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定禁用[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Materiel.getInctance().forbidMateriel(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesMateriel());
                }
            }
        }

        private void noForbid_Click(object sender, EventArgs e)
        {
            SortedDictionary<int, MaterielTable> forbidMaterielList = Materiel.getInctance().getAllForbidMaterielInfo();
            ArrayList forbidMateriels = new ArrayList();

            foreach (KeyValuePair<int, MaterielTable> index in forbidMaterielList)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = index.Value;

                forbidMateriels.Add(Convert.ToString(materiel.pkey));
                forbidMateriels.Add(Convert.ToString(materiel.name));
            }

            FormNoForbid fnfs = new FormNoForbid(forbidMateriels, "物料反禁用", ForbidDataType.Materiel);
            fnfs.ShowDialog();
            updateDataGridView(getCurrentNodeAllChildNodesMateriel());
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_materielRecordCount > 0)
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
            if(m_materielRecordCount > 0)
            {
                m_dataGridViewExtend.printDataGridView();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            // for test
            //for (int i = 0; i < 1000; i++)
            //{
            //    addMaterielTypeTest(1);
            //}

            //for (int i = 0; i < 1000; i++)
            //{
            //    addMaterielTypeTest(7 + i);
            //}

            //for (int i = 0; i < 1000; i++)
            //{
            //    addMateriel(7 + i);
            //    addMateriel(7 + i);
            //    addMateriel(7 + i);
            //    addMateriel(7 + i);
            //    addMateriel(7 + i);
            //}

            this.Close();
        }

        private void treeViewMaterielOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewMaterielOrg.SelectedNode != null && this.treeViewMaterielOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewMaterielOrg.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                if (Convert.ToInt32(this.treeViewMaterielOrg.SelectedNode.Name.ToString()) != m_materielGroupPkey)
                {
                    m_materielGroupPkey = Convert.ToInt32(this.treeViewMaterielOrg.SelectedNode.Name.ToString());

                    updateDataGridView(getCurrentNodeAllChildNodesMateriel());
                }
            }
            else 
            {
                m_materielGroupPkey = 0;
            }
        }

        private void treeViewMaterielOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewMaterielOrg.SelectedNode != null && this.treeViewMaterielOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewMaterielOrg.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void dataGridViewMaterielList_Click(object sender, EventArgs e)
        {
            if (m_materielRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewMaterielList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewMaterielList.ColumnCount; j++)
                    {
                        if (dataGridViewMaterielList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewMaterielList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewMaterielList.Rows[i].Cells[2].Value.ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void dataGridViewMaterielList_DoubleClick(object sender, EventArgs e)
        {
            if (m_materielRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewMaterielList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewMaterielList.ColumnCount; j++)
                    {
                        if (dataGridViewMaterielList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewMaterielList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewMaterielList.Rows[i].Cells[2].Value.ToString();
                            
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

        public int getSelectRecordPkey()
        {
            return m_currentDataGridViedRecordPkey;
        }

        private void SuppierGroupModify_Click(object sender, EventArgs e)
        {
            Form fse = new FormMaterielEdit(false, m_materielGroupPkey, false, 0);
            fse.ShowDialog();
            refreshTreeView();
        }

        private void SuppierGroupDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("确定删除[" + m_materielGroupName + "]分组信息吗?"))
            {
                if (m_materielRecordCount > 0)
                {
                    MessageBoxExtend.messageWarning("[" + m_materielGroupName + "] 删除失败,请先删除物料记录然后重试.");
                }
                else
                {
                    MaterielType.getInctance().delete(m_materielGroupPkey);
                    MaterielOrgStruct.getInctance().delete(MaterielOrgStruct.getInctance().getPkeyFromValue(m_materielGroupPkey));
                    refreshTreeView();
                }
            }
        }

        private SortedDictionary<int, MaterielTable> getCurrentNodeAllChildNodesMateriel()
        {
            SortedDictionary<int, MaterielTable> materielList = new SortedDictionary<int, MaterielTable>();
            SortedDictionary<int, int> childNodeValues = MaterielOrgStruct.getInctance().getAllChildNodeValue(m_materielGroupPkey);

            if (!childNodeValues.ContainsKey(m_materielGroupPkey))
            {
                childNodeValues.Add(m_materielGroupPkey, m_materielGroupPkey);
            }

            foreach (KeyValuePair<int, int> index in childNodeValues)
            {
                SortedDictionary<int, MaterielTable> temp = Materiel.getInctance().getMaterielInfoFromMaterielType(index.Value);

                foreach (KeyValuePair<int, MaterielTable> i in temp)
                {
                    MaterielTable materiel = new MaterielTable();
                    materielList.Add(materielList.Count, (MaterielTable)i.Value);
                }
            }

            return materielList;
        }

        private void dataGridViewMaterielList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_materielRecordCount))
            {
                for (int i = 0; i < this.dataGridViewMaterielList.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dataGridViewMaterielList.ColumnCount; j++)
                    {
                        this.dataGridViewMaterielList.Rows[i].Cells[j].Selected = false;
                    }
                }

                dataGridViewMaterielList.Rows[e.RowIndex].Selected = true;
                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[e.RowIndex].Cells[0].Value.ToString());
                m_currentDataGridViedRecordCompanyName = dataGridViewMaterielList.Rows[e.RowIndex].Cells[2].Value.ToString();

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(707);

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

        private void textBoxSerach_Click(object sender, EventArgs e)
        {
            this.textBoxSerach.Text = "";
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.MenuText;
        }

        private void textBoxSerach_Click(object sender, MouseEventArgs e)
        {
            this.textBoxSerach.Text = "";
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.MenuText;
        }

        private void textBoxSerach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateDataGridView(Materiel.getInctance().getMaterielInfoFromSerachTerm(this.textBoxSerach.Text));
                this.labelMaterielGroupName.Text = "[" + this.textBoxSerach.Text + "]物料共计[" + Convert.ToString(m_materielRecordCount) + "]条记录";

                this.textBoxSerach.ForeColor = System.Drawing.SystemColors.ScrollBar;
                this.textBoxSerach.Text = "输入物料名称或编码或助记码，按回车键实现快速查找";

                this.labelMaterielGroupName.Focus();
            }
        }

        private void toolStripMenuItemChange_Click(object sender, EventArgs e)
        {
            FormMaterielTypeModify fmtm = new FormMaterielTypeModify();
            fmtm.ShowDialog();

            if (fmtm.getIsSave())
            {
                int pkey = fmtm.getSelectRecordPkey();
                string typeName = fmtm.getSelectTypeName();

                if (m_currentDataGridViedRecordCompanyName.Length > 0)
                {
                    if (MessageBoxExtend.messageQuestion("确定调整物料[" + m_currentDataGridViedRecordCompanyName + "]至[" + typeName + "]分组吗?"))
                    {
                        if (Materiel.getInctance().modifyMaterielType(pkey, m_currentDataGridViedRecordPkey))
                        {
                            MessageBoxExtend.messageOK("物料分组调整成功");
                            updateDataGridView(getCurrentNodeAllChildNodesMateriel());
                        }
                    }
                }
            }
        }


        /*
         * 各种测试代码如下
         * 
         * */
        private bool addMaterielTypeTest(int materielGroupPkey)
        {
            MaterielTypeTable materielType = new MaterielTypeTable();

            materielType.name = "addMaterielTypeTest";
            materielType.num = "0";
            materielType.desc = "addMaterielTypeTest desc";

            MaterielType.getInctance().insert(materielType, false);


            // 物料组织结构
            MaterielOrgStructTable materielOrgInfo = new MaterielOrgStructTable();

            materielOrgInfo.parentPkey = MaterielOrgStruct.getInctance().getPkeyFromValue(materielGroupPkey);
            materielOrgInfo.value = MaterielType.getInctance().getMaxPkey();
            MaterielOrgStruct.getInctance().insert(materielOrgInfo,false);

            return true;
        }

        private bool addMateriel(int materielGroupPkey)
        {
            MaterielTable materiel = new MaterielTable();

            materiel.materielType = materielGroupPkey;
            materiel.name = "name-" + Convert.ToString(materielGroupPkey);

            materiel.num = "1";
            materiel.nameShort = "2";
            materiel.model = "3";
            materiel.mnemonicCode = "4";
            materiel.brand = "5";
            materiel.materielParameter = "6";

            Materiel.getInctance().insert(materiel, false);

            return true;
        }
    }
}