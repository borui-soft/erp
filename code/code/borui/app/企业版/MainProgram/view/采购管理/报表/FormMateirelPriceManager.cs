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

    public partial class FormMateirelPriceManager : Form
    {
        private int m_materielRecordCount = 0;
        private int m_materielGroupPkey = 0;
        private int m_currentDataGridViedRecordPkey = 0;
        private bool m_isQueryHistoryPrice = true;
        private bool m_isQueryMaterielPurchasePrice = true;

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        public FormMateirelPriceManager(bool isQueryHistoryPrice = true, bool isQueryMaterielPurchasePrice = true)
        {
            InitializeComponent();
            m_isQueryHistoryPrice = isQueryHistoryPrice;
            m_isQueryMaterielPurchasePrice = isQueryMaterielPurchasePrice;

            if (m_isQueryMaterielPurchasePrice)
            {
                if (m_isQueryHistoryPrice)
                {
                    this.Text = "供应商历史价格";
                    this.historyPrice.Text = "历史交易价";
                }
                else
                {
                    this.Text = "采购价格参照表";
                    this.historyPrice.Text = "供应商报价";
                }
            }
            else
            {
                if (m_isQueryHistoryPrice)
                {
                    this.Text = "历史销售价格";
                    this.historyPrice.Text = "历史交易价";
                }
                else
                {
                    this.Text = "商品销售指导价格";
                    this.historyPrice.Text = "商品报价";
                }
            }
        }

        private void FormPurchaseHistoryPriceCount_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dataGridViewExtend.addDataGridViewColumn("物料名称", 200);
            m_dataGridViewExtend.addDataGridViewColumn("简称", 80);
            m_dataGridViewExtend.addDataGridViewColumn("规格型号", 80);
            m_dataGridViewExtend.addDataGridViewColumn("存货数量", 80, false);
            m_dataGridViewExtend.addDataGridViewColumn("加权单价", 80, false);
            m_dataGridViewExtend.addDataGridViewColumn("基本单位", 80);
            m_dataGridViewExtend.addDataGridViewColumn("存货上限", 80, false);
            m_dataGridViewExtend.addDataGridViewColumn("存货下限", 80, false);
            m_dataGridViewExtend.addDataGridViewColumn("保质期", 80, false);
            m_dataGridViewExtend.addDataGridViewColumn("收料仓库", 80);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);
            updateDataGridView(Materiel.getInctance().getAllMaterielInfo());
        }

        private void updateDataGridView(SortedDictionary<int, MaterielTable> materielList)
        {
            m_materielRecordCount = materielList.Count;

            SortedDictionary<int, ArrayList> materiels = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < materielList.Count; i++)
            {
                MaterielTable materiel = new MaterielTable();
                materiel = (MaterielTable)materielList[i];

                ArrayList temp = new ArrayList();

                temp.Add(materiel.pkey);
                temp.Add(materiel.name);
                temp.Add(materiel.nameShort);
                temp.Add(materiel.model);

                InitMaterielTable MaterielCountdata = InitMateriel.getInctance().getMaterielInfoFromMaterielID(materiel.pkey);
                temp.Add(MaterielCountdata.value);
                temp.Add(MaterielCountdata.price);
                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitStorage));
                temp.Add(materiel.max);
                temp.Add(materiel.min);
                temp.Add(materiel.warramty);
                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_STORAGE_LIST", materiel.storage));

                materiels.Add(i, temp);
            }

            m_dataGridViewExtend.initDataGridViewData(materiels, 5);
            this.dataGridViewMaterielList.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewMaterielList.Columns[4].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewMaterielList.Columns[5].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
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

        private void historyPrice_Click(object sender, EventArgs e)
        {
            if (m_isQueryMaterielPurchasePrice)
            {
                if (m_isQueryHistoryPrice)
                {
                    FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.PurchaseHistoryPrice, m_currentDataGridViedRecordPkey);
                    fpic.ShowDialog();
                }
                else
                {
                    FormMaterielPriceSheet fsps = new FormMaterielPriceSheet(m_currentDataGridViedRecordPkey);
                    fsps.ShowDialog();
                }
            }
            else 
            {
                if (m_isQueryHistoryPrice)
                {
                    //FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.PurchaseHistoryPrice, m_currentDataGridViedRecordPkey);
                    //fpic.ShowDialog();
                }
                else
                {
                    FormMaterielPriceSheet fsps = new FormMaterielPriceSheet(m_currentDataGridViedRecordPkey, false);
                    fsps.ShowDialog();
                }
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
    }
}