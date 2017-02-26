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

    public partial class FormMaterielCountAlarm : Form
    {
        private int m_materielRecordCount = 0;

        private int m_materielGroupPkey = 0;
        private string m_materielGroupName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        private bool m_isNeedFilterData = false;
        private int m_filterValue = 10;

        // 库存账龄分析
        private bool m_isStorageAgeCount = false;

        public FormMaterielCountAlarm(string winText, bool isNeedFilterData = false, bool isStorateAgeCount = false)
        {
            InitializeComponent();
            this.Text = winText;

            m_isNeedFilterData = isNeedFilterData;

            m_isStorageAgeCount = isStorateAgeCount;
        }

        private void FormBaseMateriel_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            if (!m_isStorageAgeCount)
            {
                // DataGridView控件初始化
                m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dataGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dataGridViewExtend.addDataGridViewColumn("规格型号", 80);
                m_dataGridViewExtend.addDataGridViewColumn("品牌", 80);
                m_dataGridViewExtend.addDataGridViewColumn("基本单位", 80);
                m_dataGridViewExtend.addDataGridViewColumn("存货上限", 80);
                m_dataGridViewExtend.addDataGridViewColumn("存货下限", 80);
                m_dataGridViewExtend.addDataGridViewColumn("当前库存", 80);
                m_dataGridViewExtend.addDataGridViewColumn("备注", 210, m_isNeedFilterData);
            }
            else 
            {
                // 如果是库存账龄分析，datagridview中需要显示的列
                m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
                m_dataGridViewExtend.addDataGridViewColumn("物料名称", 200);
                m_dataGridViewExtend.addDataGridViewColumn("规格型号", 80);
                m_dataGridViewExtend.addDataGridViewColumn("品牌", 80);
                m_dataGridViewExtend.addDataGridViewColumn("基本单位", 80);
                m_dataGridViewExtend.addDataGridViewColumn("当前库存", 80);
                m_dataGridViewExtend.addDataGridViewColumn("1-30天", 80);
                m_dataGridViewExtend.addDataGridViewColumn("31-60天", 80);
                m_dataGridViewExtend.addDataGridViewColumn("60-90天", 80);
                m_dataGridViewExtend.addDataGridViewColumn("大于90天", 80);
            }

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);
            updateDataGridView(Materiel.getInctance().getAllMaterielInfo());

            this.filter.Visible = m_isNeedFilterData;
        }

        private void updateDataGridView(SortedDictionary<int, MaterielTable> materielList)
        {
            m_materielRecordCount = materielList.Count;
            this.labelMaterielGroupName.Text = "[" + m_materielGroupName + "]物料共计[" + Convert.ToString(m_materielRecordCount) + "]条记录";

            SortedDictionary<int, ArrayList> materiels = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < materielList.Count; i++)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = (MaterielTable)materielList[i];

                if (!m_isStorageAgeCount)
                {
                    ArrayList temp = new ArrayList();

                    temp.Add(materiel.pkey);
                    temp.Add(materiel.name);
                    temp.Add(materiel.model);
                    temp.Add(materiel.brand);

                    temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unit));

                    temp.Add(materiel.max);
                    temp.Add(materiel.min);
                    double materielCurrentStorageValue = InitMateriel.getInctance().getMarerielCountInfoFromMaterielID(materiel.pkey);
                    temp.Add(materielCurrentStorageValue);

                    if (m_isNeedFilterData)
                    {
                        if (materiel.min - materielCurrentStorageValue > m_filterValue)
                        {
                            temp.Add("当前库存与预设的物料下限差值大于" + Convert.ToString(m_filterValue));
                            materiels.Add(materiels.Count, temp);
                        }

                        if (materielCurrentStorageValue - materiel.max > m_filterValue)
                        {
                            temp.Add("当前库存与预设的物料上限差值大于" + Convert.ToString(m_filterValue));
                            materiels.Add(materiels.Count, temp);
                        }
                    }
                    else
                    {
                        materiels.Add(materiels.Count, temp);
                    }

                    m_dataGridViewExtend.initDataGridViewData(materiels, 3);
                }
                else
                {
                    // 库存账龄分析
                    ArrayList temp = new ArrayList();

                    temp.Add(materiel.pkey);
                    temp.Add(materiel.name);
                    temp.Add(materiel.model);
                    temp.Add(materiel.brand);
                    temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unit));

                    // 计算当前物料的账龄
                    double materielCurrentStorageValue = InitMateriel.getInctance().getMarerielCountInfoFromMaterielID(materiel.pkey);
                    ArrayList materielAgeValueList = StorageStockDetail.getInctance().getMaterielAgeValue(materiel.pkey, materielCurrentStorageValue);

                    temp.Add(materielCurrentStorageValue);
                    temp.Add(materielAgeValueList[0]);
                    temp.Add(materielAgeValueList[1]);
                    temp.Add(materielAgeValueList[2]);
                    temp.Add(materielAgeValueList[3]);

                    materiels.Add(materiels.Count, temp);
                    m_dataGridViewExtend.initDataGridViewData(materiels, 4);
                }
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

                currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.value));
                processDutyOrgNode(record.pkey, currentNode);
            }
        }

        private void filter_Click(object sender, EventArgs e)
        {
            FormNewFilterValue fnfv = new FormNewFilterValue(m_filterValue);
            fnfv.ShowDialog();

            m_filterValue = fnfv.getNewFilterValue();
            updateDataGridView(Materiel.getInctance().getAllMaterielInfo());
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
                m_materielGroupName = this.treeViewMaterielOrg.SelectedNode.Text.ToString();

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
    }
}