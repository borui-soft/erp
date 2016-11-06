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

    public partial class FormBaseUser : Form
    {
        private int m_pkey = 0;
        private string m_name = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();
        
        public FormBaseUser()
        {
            InitializeComponent();
        }

        private void FormBaseUser_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();
            setButtonState();

            setPageActionEnable();
        }

        private void refreshTreeView()
        {
            if (m_tree != null && treeViewUserOrg.Nodes.Count > 0)
            {
                treeViewUserOrg.Nodes.Clear();
            }

            m_tree = new TreeViewExtend(this.treeViewUserOrg);

            m_rootNode = this.treeViewUserOrg.Nodes.Add(Convert.ToString(0), "系统用户");

            processDutyOrgNode(0, m_rootNode);
            m_rootNode.Expand();
        }

        private void processDutyOrgNode(int parentID, TreeNode node)
        {
            string nodeName = "";
            TreeNode currentNode;
            ArrayList nodeList = UserOrgStruct.getInctance().getNodesFormParentID(parentID);

            for (int i = 0; i < nodeList.Count; i++)
            {
                UserOrgStructTable record = (UserOrgStructTable)nodeList[i];

                if (record.departmentOrStaff == 1)
                {
                    nodeName = Staff.getInctance().getStaffNameFromPkey(record.value);

                    // 用户在线或者不在线,使用不同的图片
                    if (Staff.getInctance().isOnline(record.value))
                    {
                        m_tree.addNode(node, nodeName, 3, 3, Convert.ToString(record.pkey));
                    }
                    else
                    {
                        m_tree.addNode(node, nodeName, 2, 2, Convert.ToString(record.pkey));
                    }
                }
                else
                {
                    nodeName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_DEPARTMENT_LIST", record.value);
                    currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.pkey));
                    processDutyOrgNode(record.pkey, currentNode);
                }
            }
        }

        private void treeViewUserOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewUserOrg.SelectedNode != null && this.treeViewUserOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewUserOrg.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                m_pkey = Convert.ToInt32(this.treeViewUserOrg.SelectedNode.Name.ToString());
                m_name = this.treeViewUserOrg.SelectedNode.Text.ToString();
            }
            else
            {
                m_pkey = 0;
            }

            setButtonState(false);
        }

        private void treeViewUserOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewUserOrg.SelectedNode != null && this.treeViewUserOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewUserOrg.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void staffManager_Click(object sender, EventArgs e)
        {
            FormBaseStaff fbs = new FormBaseStaff();
            fbs.ShowDialog();
        }

        private void staffAdd_Click(object sender, EventArgs e)
        {
            FormCreateUser fcu = new FormCreateUser();
            fcu.ShowDialog();
            if (fcu.isAddStaff())
            {
                UserOrgStructTable userOrgStructData = new UserOrgStructTable();
                userOrgStructData.value = fcu.getSelectRecordPkey();
                userOrgStructData.departmentOrStaff = 1;        // 0:部门  1:员工
                userOrgStructData.parentPkey = m_pkey;

                if (userOrgStructData.value != 0)
                {
                    UserOrgStruct.getInctance().insert(userOrgStructData);
                    refreshTreeView();
                }
            }
        }

        private void staffRemove_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("确定移除[" + m_name + "]吗?"))
            {
                UserOrgStruct.getInctance().delete(m_pkey);
                refreshTreeView();
            }
        }

        private void departmentManager_Click(object sender, EventArgs e)
        {
            ArrayList tables = new ArrayList();
            tables.Add("BASE_DEPARTMENT_LIST");

            FormBaseAuxiliaryMaterial fbam = new FormBaseAuxiliaryMaterial(tables, "部门信息管理", false);
            fbam.ShowDialog();
        }

        private void departmentAdd_Click(object sender, EventArgs e)
        {
            ArrayList tables = new ArrayList();
            tables.Add("BASE_DEPARTMENT_LIST");

            FormBaseAuxiliaryMaterial fbam = new FormBaseAuxiliaryMaterial(tables, "部门信息管理", true);
            fbam.ShowDialog();

            UserOrgStructTable userOrgStructData = new UserOrgStructTable();
            userOrgStructData.value = fbam.getSelectRecordPkey();
            userOrgStructData.departmentOrStaff = 0;        // 0:部门  1:员工
            userOrgStructData.parentPkey = m_pkey;

            if (userOrgStructData.value != 0)
            {
                UserOrgStruct.getInctance().insert(userOrgStructData);
                refreshTreeView();
            }
        }

        private void departmentRemove_Click(object sender, EventArgs e)
        {
            if (UserOrgStruct.getInctance().getChildNodesCount(m_pkey) == 0)
            {
                if (MessageBoxExtend.messageQuestion("确定移除[" + m_name + "]吗?"))
                {
                    UserOrgStruct.getInctance().delete(m_pkey);
                    refreshTreeView();
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("[" + m_name + "] 移除失败,请移除隶属的用户，然后重试.");
            }
        }

        private void checkDuty_Click(object sender, EventArgs e)
        {
            FormBaseDuryManager fbdm = new FormBaseDuryManager(m_name, false);
            fbdm.ShowDialog();
        }

        private void DutyManger_Click(object sender, EventArgs e)
        {
            FormBaseDuryManager fbdm = new FormBaseDuryManager(m_name, true);
            fbdm.ShowDialog();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setButtonState(bool isSetBackColor = true)
        {
            if (this.treeViewUserOrg.SelectedNode != null && this.treeViewUserOrg.SelectedNode.Nodes.Count >= 0)
            {
                if (isSetBackColor)
                {
                    this.treeViewUserOrg.SelectedNode.BackColor = Color.LightSteelBlue;
                }

                if (this.treeViewUserOrg.SelectedNode.ImageIndex == 2 || this.treeViewUserOrg.SelectedNode.ImageIndex == 3)
                {
                    this.staffAdd.Enabled = false;
                    this.staffRemove.Enabled = true;
                    this.departmentAdd.Enabled = false;
                    this.departmentRemove.Enabled = false;
                    this.DutyManger.Enabled = true;
                    this.checkDuty.Enabled = true;
                }
                else
                {
                    this.staffAdd.Enabled = true;
                    this.staffRemove.Enabled = false;
                    this.departmentAdd.Enabled = true;
                    this.departmentRemove.Enabled = true;
                    this.DutyManger.Enabled = false;
                    this.checkDuty.Enabled = false;
                }
            }
            else
            {
                this.staffAdd.Enabled = true;
                this.staffRemove.Enabled = false;
                this.departmentAdd.Enabled = true;
                this.departmentRemove.Enabled = true;
            }

            setPageActionEnable();
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(702);

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