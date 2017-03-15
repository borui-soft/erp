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

    public partial class FormMaterielStorageAmountInfo : Form
    {
        public enum DisplayDataType
        {
            Materiel,
            Product,
            All
        }

        private int m_materielRecordCount = 0;
        private int m_materielGroupPkey = 0;
        private int m_currentDataGridViedRecordPkey = 0;
        private bool m_isDisplayJG = false;
        private string m_materielGroupName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;
        private int m_displayDataType;
        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        // m_materielProlist用于存放库存预占信息
        SortedDictionary<int, MaterielProOccupiedInfo> m_materielProlist = new SortedDictionary<int, MaterielProOccupiedInfo>();

        // 参数isDisplayJG代表实现显示单价列
        public FormMaterielStorageAmountInfo(int displayDataType = -1, bool isDisplayJG = true)
        {
            InitializeComponent();

            if (displayDataType != -1)
            {
                m_displayDataType = displayDataType;
            }
            else 
            {
                m_displayDataType = (int)DisplayDataType.All;
            }

            if (isDisplayJG)
            {
                // 判断下是否有查看单价的权限
                SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(105);

                foreach (KeyValuePair<int, ActionTable> index in list)
                {
                    if (index.Value.uiActionName == "dispaly")
                    {
                        m_isDisplayJG = AccessAuthorization.getInctance().isAccessAuthorization(index.Value.pkey,
                                     Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));
                    }
                }
            } 
            
            if (m_displayDataType == (int)DisplayDataType.Materiel)
            {
                this.Text = "当前库存信息(物料)";
            }
            else if (m_displayDataType == (int)DisplayDataType.Product)
            {
                this.Text = "当前库存信息(商品)";
            }
            else
            {
                this.Text = "当前库存信息";
            }
        }

        private void FormMaterielStorageInfo_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30, false);
            m_dataGridViewExtend.addDataGridViewColumn("物料名称", 170);
            m_dataGridViewExtend.addDataGridViewColumn("物料编号", 100);
            m_dataGridViewExtend.addDataGridViewColumn("型号", 60);
            m_dataGridViewExtend.addDataGridViewColumn("品牌", 60);
            m_dataGridViewExtend.addDataGridViewColumn("实际库存", 78);

            if (m_isDisplayJG)
            {
                m_dataGridViewExtend.addDataGridViewColumn("加权单价", 78);
                m_dataGridViewExtend.addDataGridViewColumn("合计", 60);
            }

            // 2016-11-11++ 增加库存预占信息显示
            m_dataGridViewExtend.addDataGridViewColumn("预占数量", 78);
            m_dataGridViewExtend.addDataGridViewColumn("预占人", 78);
            m_dataGridViewExtend.addDataGridViewColumn("可用数量参考", 110);
            // ++2016-11-11

            m_dataGridViewExtend.addDataGridViewColumn("单位", 60);
            m_dataGridViewExtend.addDataGridViewColumn("存货上限", 78);
            m_dataGridViewExtend.addDataGridViewColumn("存货下限", 78);
            m_dataGridViewExtend.addDataGridViewColumn("保质期", 78);
            m_dataGridViewExtend.addDataGridViewColumn("收料仓库", 78);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);

            getMaterielProOccupiedList();
            updateDataGridView(Materiel.getInctance().getAllMaterielInfo());
        }

        private void updateDataGridView(SortedDictionary<int, MaterielTable> materielList)
        {
            double sum = 0;
            m_materielRecordCount = materielList.Count;

            SortedDictionary<int, ArrayList> materiels = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < materielList.Count; i++)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = (MaterielTable)materielList[i];

                ArrayList temp = new ArrayList();

                temp.Add(materiel.pkey);
                temp.Add(materiel.name);
                temp.Add(materiel.num);
                temp.Add(materiel.model);
                temp.Add(materiel.brand);

                InitMaterielTable MaterielCountdata = InitMateriel.getInctance().getMaterielInfoFromMaterielID(materiel.pkey);
                temp.Add(MaterielCountdata.value);

                if (m_isDisplayJG)
                {
                    temp.Add(MaterielCountdata.price);
                    temp.Add((double)(Math.Round(MaterielCountdata.value * MaterielCountdata.price * 100)) / 100);
                }

                if (m_materielProlist.ContainsKey(materiel.pkey))
                {
                    MaterielProOccupiedInfo proOccupiedRecord = new MaterielProOccupiedInfo();
                    proOccupiedRecord = (MaterielProOccupiedInfo)m_materielProlist[materiel.pkey];

                    temp.Add(proOccupiedRecord.sum);
                    temp.Add(proOccupiedRecord.applyStaffName);
                    temp.Add(MaterielCountdata.value - proOccupiedRecord.sum);
                }
                else
                {
                    temp.Add(0);
                    temp.Add("-");
                    temp.Add(MaterielCountdata.value);
                }

                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitStorage));
                temp.Add(materiel.max);
                temp.Add(materiel.min);
                temp.Add(materiel.warramty);
                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_STORAGE_LIST", materiel.storage));

                if (m_displayDataType == (int)DisplayDataType.Materiel)
                {
                    string materielAttributeName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_MATERIEL_ATTRIBUTE", materiel.materielAttribute);

                    if (materielAttributeName.IndexOf("外购") != -1)
                    {
                        materiels.Add(materiels.Count, temp);
                        sum += MaterielCountdata.value * MaterielCountdata.price;
                    }
                }
                else if (m_displayDataType == (int)DisplayDataType.Product)
                {
                    string materielAttributeName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_MATERIEL_ATTRIBUTE", materiel.materielAttribute);

                    if (materielAttributeName.IndexOf("外购") == -1)
                    {
                        materiels.Add(materiels.Count, temp);
                        sum += MaterielCountdata.value * MaterielCountdata.price;
                    }
                }
                else 
                {
                    materiels.Add(materiels.Count, temp);
                    sum += MaterielCountdata.value * MaterielCountdata.price;
                }
            }

            // 金额信息保留2位小数儿
            sum = (double)(Math.Round(sum * 100)) / 100;

            if (m_isDisplayJG)
            {
                m_dataGridViewExtend.initDataGridViewData(materiels, 9);
            }
            else
            {
                m_dataGridViewExtend.initDataGridViewData(materiels, 7);
            }

            this.dataGridViewMaterielList.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
            this.dataGridViewMaterielList.Columns[5].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;

            if (m_isDisplayJG)
            {
                this.dataGridViewMaterielList.Columns[6].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                this.dataGridViewMaterielList.Columns[7].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                this.dataGridViewMaterielList.Columns[10].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;

                this.labelCountInfo.Text = "[" + m_materielGroupName + "]类材料总计[" + Convert.ToString(materiels.Count) + "]条";
                this.labelCountInfo.Text += "  累计金额[" + Convert.ToString(sum) + "]";
            }
            else 
            {
                this.dataGridViewMaterielList.Columns[8].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                this.labelCountInfo.Text = "[" + m_materielGroupName + "]类材料总计[" + Convert.ToString(materiels.Count) + "]条";
            }
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
            this.Close();
        }

        private void treeViewMaterielOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewMaterielOrg.SelectedNode != null && this.treeViewMaterielOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewMaterielOrg.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                m_materielGroupPkey = Convert.ToInt32(this.treeViewMaterielOrg.SelectedNode.Name.ToString());
                m_materielGroupName = this.treeViewMaterielOrg.SelectedNode.Text;

                updateDataGridView(getCurrentNodeAllChildNodesMateriel());
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
                            return;
                        }
                    }
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

        private void textBoxSerach_Click(object sender, EventArgs e)
        {
            this.textBoxSerach.Text = "";
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.MenuText;
        }

        private void textBoxSerach_MouseClick(object sender, MouseEventArgs e)
        {
            this.textBoxSerach.Text = "";
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.MenuText;
        }

        private void textBoxSerach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateDataGridView(Materiel.getInctance().getMaterielInfoFromSerachTerm(this.textBoxSerach.Text));

                this.textBoxSerach.ForeColor = System.Drawing.SystemColors.ScrollBar;
                this.textBoxSerach.Text = "输入物料名称或编码或助记码，按回车键实现快速查找";

                this.labelCountInfo.Focus();
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            MaterielProOccupiedOrder.getInctance().refrensRecord();
            MaterielProOccupiedOrderDetails.getInctance().refrensRecord();
            getMaterielProOccupiedList();

            refreshTreeView();
            Materiel.getInctance().refrensRecord();
            updateDataGridView(Materiel.getInctance().getAllMaterielInfo());
        }

        private void getMaterielProOccupiedList()
        {
            m_materielProlist = MaterielProOccupiedOrderDetails.getInctance().getMaterielProOccupiedList();
        }

        private void ToolStripMenuItemToApply_Click(object sender, EventArgs e)
        {
            FormPurchaseApply fpa = new FormPurchaseApply("", "");
            fpa.ShowDialog();
        }

        private void dataGridViewMaterielList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_materielRecordCount))
            {
                dataGridViewMaterielList.Rows[e.RowIndex].Selected = true;
                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[e.RowIndex].Cells[0].Value.ToString());

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }
    }
}