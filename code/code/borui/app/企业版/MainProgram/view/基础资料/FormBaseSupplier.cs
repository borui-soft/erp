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


namespace MainProgram
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public partial class FormBaseSupplier : Form
    {
        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;
        private int m_supplierGroupPkey = 0;

        private int m_rootNodePkey;

        public FormBaseSupplier()
        {
            InitializeComponent();
        }

        private void FormBaseSupplier_Load(object sender, EventArgs e)
        {
            m_tree = new TreeViewExtend(this.treeViewSupplierOrg);
            m_rootNodePkey = SupplierOrgStruct.getInctance().getRootNodePkey();

            int rooNodeValue = SupplierOrgStruct.getInctance().getNoteValueFromPkey(m_rootNodePkey);
            string rootName = SupplierType.getInctance().getSupplierTypeNameFromPkey(rooNodeValue);

            m_rootNode = this.treeViewSupplierOrg.Nodes.Add(Convert.ToString(m_rootNodePkey), rootName);

            refreshTreeView();
        }

        private void refreshTreeView()
        {
            processDutyOrgNode(m_rootNodePkey, m_rootNode);
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

                currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.pkey));
                processDutyOrgNode(record.pkey, currentNode);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if(this.treeViewSupplierOrg.SelectedNode.Nodes.Count > 0)
            {
                m_supplierGroupPkey = Convert.ToInt32(this.treeViewSupplierOrg.SelectedNode.Name.ToString());
            }
            else
            {
                m_supplierGroupPkey = 0;
            }

            Form fse = new FormSupplierEdit(m_supplierGroupPkey);
            fse.ShowDialog();
        }

        private void modify_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {

        }

        private void forbid_Click(object sender, EventArgs e)
        {

        }

        private void noForbid_Click(object sender, EventArgs e)
        {

        }

        private void export_Click(object sender, EventArgs e)
        {

        }

        private void printDisplay_Click(object sender, EventArgs e)
        {

        }

        private void print_Click(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {

        }

        private void treeViewSupplierOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            setButtonState(false);
        }

        private void treeViewSupplierOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewSupplierOrg.SelectedNode != null && this.treeViewSupplierOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewSupplierOrg.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void setButtonState(bool isSetBackColor = true)
        {
            if (this.treeViewSupplierOrg.SelectedNode != null && this.treeViewSupplierOrg.SelectedNode.Nodes.Count >= 0)
            {
                if (isSetBackColor)
                {
                    this.treeViewSupplierOrg.SelectedNode.BackColor = Color.LightSteelBlue;
                }
            }
        }

    }
}