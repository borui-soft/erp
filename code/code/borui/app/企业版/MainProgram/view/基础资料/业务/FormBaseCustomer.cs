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

    public partial class FormBaseCustomer : Form
    {
        private int m_customerRecordCount = 0;

        private int m_customerGroupPkey = 0;
        private string m_customerGroupName = "";

        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordCompanyName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        private bool m_isAddRecordMode;

        public FormBaseCustomer(bool isAddRecordMode = false)
        {
            InitializeComponent();
            m_isAddRecordMode = isAddRecordMode;
        }

        private void FormBaseCustomer_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dataGridViewExtend.addDataGridViewColumn("客户组", 200, false);
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

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewCustomerList);
            updateDataGridView(Customer.getInctance().getAllCustomerInfo());

            setPageActionEnable();
        }

        private void updateDataGridView(SortedDictionary<int, CustomerTable> customerList)
        {
            m_customerRecordCount = customerList.Count;
            this.labelCustomerGroupName.Text = "[" + m_customerGroupName + "]客户共计[" + Convert.ToString(m_customerRecordCount) + "]条记录";

            SortedDictionary<int, ArrayList> customers = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < customerList.Count; i++)
            {
                CustomerTable customer = new CustomerTable();
                customer = (CustomerTable)customerList[i];

                ArrayList temp = new ArrayList();

                temp.Add(customer.pkey);
                temp.Add(customer.customerType);
                temp.Add(customer.name);
                temp.Add(customer.nameShort);
                temp.Add(customer.mnemonicCode);
                temp.Add(customer.area);
                temp.Add(customer.credit);
                temp.Add(customer.varRate);
                temp.Add(customer.contact);
                temp.Add(customer.tel);
                temp.Add(customer.fax);
                temp.Add(customer.mobilePhone);
                temp.Add(customer.email);
                temp.Add(customer.homePage);
                temp.Add(customer.address);
                temp.Add(customer.zipCode);
                temp.Add(customer.bankName);
                temp.Add(customer.bankAccount);
                temp.Add(customer.taxAccount);
                temp.Add(customer.note);

                customers.Add(i, temp);
            }

            m_dataGridViewExtend.initDataGridViewData(customers, 3);
        }

        private void refreshTreeView()
        {
            if (m_tree !=null && treeViewCustomerOrg.Nodes.Count > 0)
            {
                treeViewCustomerOrg.Nodes.Clear();
            }

            m_tree = new TreeViewExtend(this.treeViewCustomerOrg);
            int rootNodePkey = CustomerOrgStruct.getInctance().getRootNodePkey();

            int rooNodeValue = CustomerOrgStruct.getInctance().getNoteValueFromPkey(rootNodePkey);
            string rootName = CustomerType.getInctance().getCustomerTypeNameFromPkey(rooNodeValue);

            m_rootNode = this.treeViewCustomerOrg.Nodes.Add(Convert.ToString(rooNodeValue), rootName);

            processDutyOrgNode(rootNodePkey, m_rootNode);
            m_rootNode.Expand();
        }

        private void processDutyOrgNode(int parentID, TreeNode node)
        {
            string nodeName = "";
            TreeNode currentNode;
            ArrayList nodeList = CustomerOrgStruct.getInctance().getNodesFormParentID(parentID);

            for (int i = 0; i < nodeList.Count; i++)
            {
                CustomerOrgStructTable record = (CustomerOrgStructTable)nodeList[i];

                nodeName = CustomerType.getInctance().getCustomerTypeNameFromPkey(record.value);

                currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.value));
                processDutyOrgNode(record.pkey, currentNode);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form fse = new FormCustomerEdit(true, m_customerGroupPkey);
            fse.ShowDialog();

            refreshTreeView();
            updateDataGridView(getCurrentNodeAllChildNodesCustomer());
        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                Form fse = new FormCustomerEdit(false, m_customerGroupPkey, true, m_currentDataGridViedRecordPkey);
                fse.ShowDialog();

                updateDataGridView(getCurrentNodeAllChildNodesCustomer());
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Customer.getInctance().delete(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesCustomer());
                }
            }
        }

        private void forbid_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定禁用[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Customer.getInctance().forbidCustomer(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesCustomer());
                }
            }
        }

        private void noForbid_Click(object sender, EventArgs e)
        {
            SortedDictionary<int, CustomerTable> forbidCustomerList = Customer.getInctance().getAllForbidCustomerInfo();
            ArrayList forbidCustomers = new ArrayList();

            foreach (KeyValuePair<int, CustomerTable> index in forbidCustomerList)
            {
                CustomerTable customer = new CustomerTable();
                customer = index.Value;

                forbidCustomers.Add(Convert.ToString(customer.pkey));
                forbidCustomers.Add(Convert.ToString(customer.name));
            }

            FormNoForbid fnfs = new FormNoForbid(forbidCustomers, "客户反禁用", ForbidDataType.Customer);
            fnfs.ShowDialog();
            updateDataGridView(getCurrentNodeAllChildNodesCustomer());
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_customerRecordCount > 0)
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
            m_dataGridViewExtend.printDataGridView(m_customerGroupName + "组客户资料");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewCustomerOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewCustomerOrg.SelectedNode != null && this.treeViewCustomerOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewCustomerOrg.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                m_customerGroupPkey = Convert.ToInt32(this.treeViewCustomerOrg.SelectedNode.Name.ToString());
                m_customerGroupName = this.treeViewCustomerOrg.SelectedNode.Text.ToString();

                updateDataGridView(getCurrentNodeAllChildNodesCustomer());
            }
            else 
            {
                m_customerGroupPkey = 0;
            }
        }

        private void treeViewCustomerOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewCustomerOrg.SelectedNode != null && this.treeViewCustomerOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewCustomerOrg.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void dataGridViewCustomerList_Click(object sender, EventArgs e)
        {
            if (m_customerRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewCustomerList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewCustomerList.ColumnCount; j++)
                    {
                        if (dataGridViewCustomerList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewCustomerList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewCustomerList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewCustomerList.Rows[i].Cells[2].Value.ToString();
                            return;
                        }
                    }
                }
            }
        }
        
        private void dataGridViewCustomerList_DoubleClick(object sender, EventArgs e)
        {
            if (m_customerRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewCustomerList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewCustomerList.ColumnCount; j++)
                    {
                        if (dataGridViewCustomerList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewCustomerList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewCustomerList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewCustomerList.Rows[i].Cells[2].Value.ToString();
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
            Form fse = new FormCustomerEdit(false, m_customerGroupPkey, false, 0);
            fse.ShowDialog();
            refreshTreeView();
        }

        private void SuppierGroupDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("确定删除[" + m_customerGroupName + "]分组信息吗?"))
            {
                if (m_customerRecordCount > 0)
                {
                    MessageBoxExtend.messageWarning("[" + m_customerGroupName + "] 删除失败,请先删除客户记录然后重试.");
                }
                else
                {
                    CustomerType.getInctance().delete(m_customerGroupPkey);
                    CustomerOrgStruct.getInctance().delete(CustomerOrgStruct.getInctance().getPkeyFromValue(m_customerGroupPkey));
                    refreshTreeView();
                }
            }
        }

        private SortedDictionary<int, CustomerTable> getCurrentNodeAllChildNodesCustomer()
        {
            SortedDictionary<int, CustomerTable> customerList = new SortedDictionary<int, CustomerTable>();
            SortedDictionary<int, int> childNodeValues = CustomerOrgStruct.getInctance().getAllChildNodeValue(m_customerGroupPkey);

            if (!childNodeValues.ContainsKey(m_customerGroupPkey))
            {
                childNodeValues.Add(m_customerGroupPkey, m_customerGroupPkey);
            }

            foreach (KeyValuePair<int, int> index in childNodeValues)
            {
                SortedDictionary<int, CustomerTable> temp = Customer.getInctance().getCustomerInfoFromCustomerType(index.Value);

                foreach (KeyValuePair<int, CustomerTable> i in temp)
                {
                    CustomerTable customer = new CustomerTable();
                    customerList.Add(customerList.Count, (CustomerTable)i.Value);
                }
            }

            return customerList;
        }

        private void dataGridViewCustomerList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_customerRecordCount))
            {
                for (int i = 0; i < this.dataGridViewCustomerList.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dataGridViewCustomerList.ColumnCount; j++)
                    {
                        this.dataGridViewCustomerList.Rows[i].Cells[j].Selected = false;
                    }
                }

                dataGridViewCustomerList.Rows[e.RowIndex].Selected = true;
                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewCustomerList.Rows[e.RowIndex].Cells[0].Value.ToString());
                m_currentDataGridViedRecordCompanyName = dataGridViewCustomerList.Rows[e.RowIndex].Cells[2].Value.ToString();
                
                contextMenuStripCustomerGroup.Show(MousePosition.X, MousePosition.Y);
            }
        }

        public int getSelectRecordPkey()
        {
            return m_currentDataGridViedRecordPkey;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(706);

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

        private void toolStripMenuItemChange_Click(object sender, EventArgs e)
        {
            FormMaterielTypeModify fmtm = new FormMaterielTypeModify(2);
            fmtm.ShowDialog();

            if (fmtm.getIsSave())
            {
                int pkey = fmtm.getSelectRecordPkey();
                string typeName = fmtm.getSelectTypeName();

                if (m_currentDataGridViedRecordCompanyName.Length > 0)
                {
                    if (MessageBoxExtend.messageQuestion("确定调整客户[" + m_currentDataGridViedRecordCompanyName + "]至[" + typeName + "]分组吗?"))
                    {
                        if (Customer.getInctance().modifyMaterielType(pkey, m_currentDataGridViedRecordPkey))
                        {
                            MessageBoxExtend.messageOK("客户分组调整成功");
                            updateDataGridView(getCurrentNodeAllChildNodesCustomer());
                        }
                    }
                }
            }
        }
    }
}