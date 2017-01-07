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

    public partial class FormBaseSupplier : Form
    {
        private int m_supplierRecordCount = 0;

        private int m_supplierGroupPkey = 0;
        private string m_supplierGroupName = "";

        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordCompanyName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        private bool m_isAddRecordMode;

        public FormBaseSupplier(bool isAddRecordMode = false)
        {
            InitializeComponent();
            m_isAddRecordMode = isAddRecordMode;
        }

        private void FormBaseSupplier_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dataGridViewExtend.addDataGridViewColumn("供应商组", 200, false);
            m_dataGridViewExtend.addDataGridViewColumn("公司全称", 200);
            m_dataGridViewExtend.addDataGridViewColumn("公司简称", 100);
            m_dataGridViewExtend.addDataGridViewColumn("助记码", 100);
            m_dataGridViewExtend.addDataGridViewColumn("区域", 100);
            m_dataGridViewExtend.addDataGridViewColumn("信用额度", 100);
            m_dataGridViewExtend.addDataGridViewColumn("增值税税率", 100);
            m_dataGridViewExtend.addDataGridViewColumn("联系人", 100);
            m_dataGridViewExtend.addDataGridViewColumn("电话", 100);
            m_dataGridViewExtend.addDataGridViewColumn("传真", 100);
            m_dataGridViewExtend.addDataGridViewColumn("手机", 100);
            m_dataGridViewExtend.addDataGridViewColumn("电子邮件", 100);
            m_dataGridViewExtend.addDataGridViewColumn("公司主页", 100);
            m_dataGridViewExtend.addDataGridViewColumn("详细地址", 200);
            m_dataGridViewExtend.addDataGridViewColumn("邮编", 100);
            m_dataGridViewExtend.addDataGridViewColumn("开户银行", 100);
            m_dataGridViewExtend.addDataGridViewColumn("银行账户", 100);
            m_dataGridViewExtend.addDataGridViewColumn("单位税号", 100);
            m_dataGridViewExtend.addDataGridViewColumn("备注", 100);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewSupplierList);
            updateDataGridView(Supplier.getInctance().getAllSupplierInfo());

            setPageActionEnable();

            ToolStripMenuItemAddSupplier.Enabled = this.add.Enabled;
            ToolStripMenuItemModifySupplier.Enabled = this.modify.Enabled;
            ToolStripMenuItemDeleteSupplier.Enabled = this.delete.Enabled;
            ToolStripMenuItemForbidSupplier.Enabled = this.forbid.Enabled;
            ToolStripMenuItemNoForbidSupplier.Enabled = this.noForbid.Enabled;
        }

        private void updateDataGridView(SortedDictionary<int, SupplierTable> supplierList)
        {
            m_supplierRecordCount = supplierList.Count;
            this.labelSupplierGroupName.Text = "[" + m_supplierGroupName + "]供应商共计[" + Convert.ToString(m_supplierRecordCount) + "]条记录";

            SortedDictionary<int, ArrayList> suppliers = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < supplierList.Count; i++)
            {
                SupplierTable supplier = new SupplierTable();
                supplier = (SupplierTable)supplierList[i];

                ArrayList temp = new ArrayList();

                temp.Add(supplier.pkey);
                temp.Add(supplier.supplierType);
                temp.Add(supplier.name);
                temp.Add(supplier.nameShort);
                temp.Add(supplier.mnemonicCode);
                temp.Add(supplier.area);
                temp.Add(supplier.credit);
                temp.Add(supplier.varRate);
                temp.Add(supplier.contact);
                temp.Add(supplier.tel);
                temp.Add(supplier.fax);
                temp.Add(supplier.mobilePhone);
                temp.Add(supplier.email);
                temp.Add(supplier.homePage);
                temp.Add(supplier.address);
                temp.Add(supplier.zipCode);
                temp.Add(supplier.bankName);
                temp.Add(supplier.bankAccount);
                temp.Add(supplier.taxAccount);
                temp.Add(supplier.note);

                suppliers.Add(i, temp);
            }

            m_dataGridViewExtend.initDataGridViewData(suppliers, 3);
        }

        private void refreshTreeView()
        {
            if (m_tree !=null && treeViewSupplierOrg.Nodes.Count > 0)
            {
                treeViewSupplierOrg.Nodes.Clear();
            }

            m_tree = new TreeViewExtend(this.treeViewSupplierOrg);
            int rootNodePkey = SupplierOrgStruct.getInctance().getRootNodePkey();

            int rooNodeValue = SupplierOrgStruct.getInctance().getNoteValueFromPkey(rootNodePkey);
            string rootName = SupplierType.getInctance().getSupplierTypeNameFromPkey(rooNodeValue);

            m_rootNode = this.treeViewSupplierOrg.Nodes.Add(Convert.ToString(rooNodeValue), rootName);

            processDutyOrgNode(rootNodePkey, m_rootNode);
            m_rootNode.Expand();
        }

        private void processDutyOrgNode(int parentID, TreeNode node)
        {
            string nodeName = "";
            TreeNode currentNode;
            ArrayList nodeList = SupplierOrgStruct.getInctance().getNodesFormParentID(parentID);

            for (int i = 0; i < nodeList.Count; i++)
            {
                SupplierOrgStructTable record = (SupplierOrgStructTable)nodeList[i];

                nodeName = SupplierType.getInctance().getSupplierTypeNameFromPkey(record.value);

                currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.value));
                processDutyOrgNode(record.pkey, currentNode);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form fse = new FormSupplierEdit(true, m_supplierGroupPkey);
            fse.ShowDialog();

            refreshTreeView();
            updateDataGridView(getCurrentNodeAllChildNodesSupplier());
        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                Form fse = new FormSupplierEdit(false, m_supplierGroupPkey, true, m_currentDataGridViedRecordPkey);
                fse.ShowDialog();

                updateDataGridView(getCurrentNodeAllChildNodesSupplier());
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Supplier.getInctance().delete(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesSupplier());
                }
            }
        }

        private void forbid_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定禁用[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Supplier.getInctance().forbidSupplier(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesSupplier());
                }
            }
        }

        private void noForbid_Click(object sender, EventArgs e)
        {
            SortedDictionary<int, SupplierTable> forbidSupplierList = Supplier.getInctance().getAllForbidSupplierInfo();
            ArrayList forbidSuppliers = new ArrayList();

            foreach (KeyValuePair<int, SupplierTable> index in forbidSupplierList)
            {
                SupplierTable supplier = new SupplierTable();
                supplier = index.Value;

                forbidSuppliers.Add(Convert.ToString(supplier.pkey));
                forbidSuppliers.Add(Convert.ToString(supplier.name));
            }

            FormNoForbid fnfs = new FormNoForbid(forbidSuppliers, "供应商反禁用", ForbidDataType.Supplier);
            fnfs.ShowDialog();
            updateDataGridView(getCurrentNodeAllChildNodesSupplier());
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_supplierRecordCount > 0)
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
            m_dataGridViewExtend.printDataGridView(m_supplierGroupName + "组供应商资料");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewSupplierOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewSupplierOrg.SelectedNode != null && this.treeViewSupplierOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewSupplierOrg.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                m_supplierGroupPkey = Convert.ToInt32(this.treeViewSupplierOrg.SelectedNode.Name.ToString());
                m_supplierGroupName = this.treeViewSupplierOrg.SelectedNode.Text.ToString();

                updateDataGridView(getCurrentNodeAllChildNodesSupplier());
            }
            else 
            {
                m_supplierGroupPkey = 0;
            }
        }

        private void treeViewSupplierOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewSupplierOrg.SelectedNode != null && this.treeViewSupplierOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewSupplierOrg.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void dataGridViewSupplierList_Click(object sender, EventArgs e)
        {
            if (m_supplierRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewSupplierList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewSupplierList.ColumnCount; j++)
                    {
                        if (dataGridViewSupplierList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewSupplierList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewSupplierList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewSupplierList.Rows[i].Cells[2].Value.ToString();
                            return;
                        }
                    }
                }
            }
        }


        private void dataGridViewSupplierList_DoubleClick(object sender, EventArgs e)
        {
            if (m_supplierRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewSupplierList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewSupplierList.ColumnCount; j++)
                    {
                        if (dataGridViewSupplierList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewSupplierList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewSupplierList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewSupplierList.Rows[i].Cells[2].Value.ToString();
                            
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
        private void SuppierGroupModify_Click(object sender, EventArgs e)
        {
            Form fse = new FormSupplierEdit(false, m_supplierGroupPkey, false, 0);
            fse.ShowDialog();
            refreshTreeView();
        }

        private void SuppierGroupDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("确定删除[" + m_supplierGroupName + "]分组信息吗?"))
            {
                if (m_supplierRecordCount > 0)
                {
                    MessageBoxExtend.messageWarning("[" + m_supplierGroupName + "] 删除失败,请先删除供应商记录然后重试.");
                }
                else
                {
                    SupplierType.getInctance().delete(m_supplierGroupPkey);
                    SupplierOrgStruct.getInctance().delete(SupplierOrgStruct.getInctance().getPkeyFromValue(m_supplierGroupPkey));
                    refreshTreeView();
                }
            }
        }

        private SortedDictionary<int, SupplierTable> getCurrentNodeAllChildNodesSupplier()
        {
            SortedDictionary<int, SupplierTable> supplierList = new SortedDictionary<int, SupplierTable>();
            SortedDictionary<int, int> childNodeValues = SupplierOrgStruct.getInctance().getAllChildNodeValue(m_supplierGroupPkey);

            if (!childNodeValues.ContainsKey(m_supplierGroupPkey))
            {
                childNodeValues.Add(m_supplierGroupPkey, m_supplierGroupPkey);
            }

            foreach (KeyValuePair<int, int> index in childNodeValues)
            {
                SortedDictionary<int, SupplierTable> temp = Supplier.getInctance().getSupplierInfoFromSupplierType(index.Value);

                foreach (KeyValuePair<int, SupplierTable> i in temp)
                {
                    SupplierTable supplier = new SupplierTable();
                    supplierList.Add(supplierList.Count, (SupplierTable)i.Value);
                }
            }

            return supplierList;
        }

        private void dataGridViewSupplierList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_supplierRecordCount))
            {
                for (int i = 0; i < this.dataGridViewSupplierList.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dataGridViewSupplierList.ColumnCount; j++)
                    {
                        this.dataGridViewSupplierList.Rows[i].Cells[j].Selected = false;
                    }
                }

                dataGridViewSupplierList.Rows[e.RowIndex].Selected = true;
                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewSupplierList.Rows[e.RowIndex].Cells[0].Value.ToString());
                m_currentDataGridViedRecordCompanyName = dataGridViewSupplierList.Rows[e.RowIndex].Cells[2].Value.ToString();

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }

        public int getSelectRecordPkey()
        {
            return m_currentDataGridViedRecordPkey;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(705);

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

        private void textBoxSerach_DoubleClick(object sender, EventArgs e)
        {
            this.textBoxSerach.Text = "";
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.MenuText;
        }

        private void textBoxSerach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateDataGridView(Supplier.getInctance().getSupplierInfoFromSerachTerm(this.textBoxSerach.Text));
                this.labelSupplierGroupName.Text = "包含[" + this.textBoxSerach.Text + "]关键字的供应商共计[" + Convert.ToString(m_supplierRecordCount) + "]条记录";

                this.textBoxSerach.ForeColor = System.Drawing.SystemColors.ScrollBar;
                this.textBoxSerach.Text = "输入供应商名称，按回车键实现快速查找";

                this.labelSupplierGroupName.Focus();
            }
        }

        private void textBoxSerach_MouseClick(object sender, MouseEventArgs e)
        {
            this.textBoxSerach.Text = "";
            this.textBoxSerach.ForeColor = System.Drawing.SystemColors.MenuText;
        }
    }
}