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

    public partial class FormBaseStaff : Form
    {
        private int m_staffRecordCount = 0;

        private int m_staffGroupPkey = 0;
        private string m_staffGroupName = "";

        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordCompanyName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        private bool m_isAddRecordMode;

        public FormBaseStaff(bool isAddRecordMode = false)
        {
            InitializeComponent();
            m_isAddRecordMode = isAddRecordMode;
        }

        private void FormBaseStaff_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();

            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dataGridViewExtend.addDataGridViewColumn("编号", 60, false);
            m_dataGridViewExtend.addDataGridViewColumn("姓名", 100);
            m_dataGridViewExtend.addDataGridViewColumn("性别", 60);
            m_dataGridViewExtend.addDataGridViewColumn("学历", 60);
            m_dataGridViewExtend.addDataGridViewColumn("身份证号", 200);
            m_dataGridViewExtend.addDataGridViewColumn("电话", 100);
            m_dataGridViewExtend.addDataGridViewColumn("住址", 200);
            m_dataGridViewExtend.addDataGridViewColumn("电子邮箱", 100);
            m_dataGridViewExtend.addDataGridViewColumn("入职日期", 100);
            m_dataGridViewExtend.addDataGridViewColumn("职务", 100);
            m_dataGridViewExtend.addDataGridViewColumn("部门", 100, false);
            m_dataGridViewExtend.addDataGridViewColumn("类型", 100);
            m_dataGridViewExtend.addDataGridViewColumn("备注", 300);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewStaffList);
            updateDataGridView(Staff.getInctance().getAllStaffInfo());

            setPageActionEnable();

            ToolStripMenuItemAddStaff.Enabled = this.add.Enabled;
            ToolStripMenuItemModifyStaff.Enabled = this.modify.Enabled;
            ToolStripMenuItemDeleteStaff.Enabled = this.delete.Enabled;
            ToolStripMenuItemForbidStaff.Enabled = this.forbid.Enabled;
            ToolStripMenuItemNoForbidStaff.Enabled = this.noForbid.Enabled;


            this.SuppierGroupAdd.Enabled = this.add.Enabled; ;
            this.SuppierGroupDelete.Enabled = this.delete.Enabled;
        }

        private void updateDataGridView(SortedDictionary<int, StaffTable> staffList)
        {
            m_staffRecordCount = staffList.Count;
            this.labelStaffGroupName.Text = "[" + m_staffGroupName + "]员工共计[" + Convert.ToString(m_staffRecordCount) + "]条记录";

            SortedDictionary<int, ArrayList> staffs = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < staffList.Count; i++)
            {
                StaffTable staff = new StaffTable();
                staff = (StaffTable)staffList[i];

                ArrayList temp = new ArrayList();

                temp.Add(staff.pkey);
                temp.Add(staff.number);
                temp.Add(staff.name);
                temp.Add(staff.sex);
                temp.Add(staff.eduBackground);
                temp.Add(staff.NO);
                temp.Add(staff.tel);
                temp.Add(staff.address);
                temp.Add(staff.email);
                temp.Add(staff.enterDate);

                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_ROLE", staff.prifileID));
                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_DEPARTMENT_LIST", staff.departmentID));
                temp.Add(AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_STAFF_TYPE", staff.staffType));

                temp.Add(staff.remarks);

                staffs.Add(i, temp);
            }

            m_dataGridViewExtend.initDataGridViewData(staffs, 3);
        }

        private void refreshTreeView()
        {
            if (m_tree !=null && treeViewStaffOrg.Nodes.Count > 0)
            {
                treeViewStaffOrg.Nodes.Clear();
            }

            m_tree = new TreeViewExtend(this.treeViewStaffOrg);

            m_rootNode = this.treeViewStaffOrg.Nodes.Add(Convert.ToString(0), "员工组织结构图");

            processDutyOrgNode(0, m_rootNode);
            m_rootNode.Expand();
        }

        private void processDutyOrgNode(int parentID, TreeNode node)
        {
            string nodeName = "";
            TreeNode currentNode;
            ArrayList nodeList = StaffOrgStruct.getInctance().getNodesFormParentID(parentID);

            for (int i = 0; i < nodeList.Count; i++)
            {
                StaffOrgStructTable record = (StaffOrgStructTable)nodeList[i];

                nodeName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_DEPARTMENT_LIST", record.value);

                currentNode = m_tree.addNode(node, nodeName, 0, 1, Convert.ToString(record.value));
                processDutyOrgNode(record.pkey, currentNode);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form fse = new FormStaffEdit(true, m_staffGroupPkey);
            fse.ShowDialog();
            updateDataGridView(getCurrentNodeAllChildNodesStaff());
        }

        private void modify_Click(object sender, EventArgs e)
        {
            Form fse = new FormStaffEdit(false, m_staffGroupPkey, m_currentDataGridViedRecordPkey);
            fse.ShowDialog();
            updateDataGridView(getCurrentNodeAllChildNodesStaff());
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (UserOrgStruct.getInctance().getPkeyFromValue(m_currentDataGridViedRecordPkey) == -1)
            {
                if (m_currentDataGridViedRecordPkey != 0)
                {
                    if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                    {
                        Staff.getInctance().delete(m_currentDataGridViedRecordPkey);
                        updateDataGridView(getCurrentNodeAllChildNodesStaff());
                    }
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("[" + m_currentDataGridViedRecordCompanyName + "]删除失败,该用户已被关联到系统登录用户\n请先在系统登录中移除，然后重试!");
            }
        }

        private void forbid_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定禁用[" + m_currentDataGridViedRecordCompanyName + "]吗?"))
                {
                    Staff.getInctance().forbidStaff(m_currentDataGridViedRecordPkey);
                    updateDataGridView(getCurrentNodeAllChildNodesStaff());
                }
            }
        }

        private void noForbid_Click(object sender, EventArgs e)
        {
            SortedDictionary<int, StaffTable> forbidStaffList = Staff.getInctance().getAllForbidStaffInfo();
            ArrayList forbidStaffs = new ArrayList();

            foreach (KeyValuePair<int, StaffTable> index in forbidStaffList)
            {
                StaffTable staff = new StaffTable();
                staff = index.Value;

                forbidStaffs.Add(Convert.ToString(staff.pkey));
                forbidStaffs.Add(Convert.ToString(staff.name));
            }

            FormNoForbid fnfs = new FormNoForbid(forbidStaffs, "职员反禁用", ForbidDataType.Staff);
            fnfs.ShowDialog();
            updateDataGridView(getCurrentNodeAllChildNodesStaff());
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_staffRecordCount > 0)
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
            if(m_staffRecordCount > 0)
            {
                m_dataGridViewExtend.printDataGridView();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewStaffOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewStaffOrg.SelectedNode != null && this.treeViewStaffOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewStaffOrg.SelectedNode.BackColor = Color.LightSteelBlue;

                // 单击鼠标时，得到当前树节点Pkey及文本值
                m_staffGroupPkey = Convert.ToInt32(this.treeViewStaffOrg.SelectedNode.Name.ToString());
                m_staffGroupName = this.treeViewStaffOrg.SelectedNode.Text.ToString();

                updateDataGridView(getCurrentNodeAllChildNodesStaff());
            }
            else 
            {
                m_staffGroupPkey = 0;
            }
        }

        private void treeViewStaffOrg_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewStaffOrg.SelectedNode != null && this.treeViewStaffOrg.SelectedNode.Nodes.Count >= 0)
            {
                this.treeViewStaffOrg.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void dataGridViewStaffList_Click(object sender, EventArgs e)
        {
            if (m_staffRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewStaffList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewStaffList.ColumnCount; j++)
                    {
                        if (dataGridViewStaffList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewStaffList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewStaffList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewStaffList.Rows[i].Cells[2].Value.ToString();
                        }
                    }
                }
            }
        }

        private void dataGridViewStaffList_DoubleClick(object sender, EventArgs e)
        {
            if (m_staffRecordCount > 0)
            {
                // 当用户双击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewStaffList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewStaffList.ColumnCount; j++)
                    {
                        if (dataGridViewStaffList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewStaffList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewStaffList.Rows[i].Cells[0].Value.ToString());
                            m_currentDataGridViedRecordCompanyName = dataGridViewStaffList.Rows[i].Cells[2].Value.ToString();

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

        private void SuppierGroupDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("确定删除[" + m_staffGroupName + "]部门信息吗?"))
            {
                if (m_staffRecordCount > 0)
                {
                    MessageBoxExtend.messageWarning("[" + m_staffGroupName + "] 删除失败,请先删除下属的员工记录然后重试.");
                }
                else
                {
                    StaffOrgStruct.getInctance().delete(StaffOrgStruct.getInctance().getPkeyFromValue(m_staffGroupPkey));
                    refreshTreeView();
                }
            }
        }

        private SortedDictionary<int, StaffTable> getCurrentNodeAllChildNodesStaff()
        {
            SortedDictionary<int, StaffTable> staffList = new SortedDictionary<int, StaffTable>();
            SortedDictionary<int, int> childNodeValues = StaffOrgStruct.getInctance().getAllChildNodeValue(m_staffGroupPkey);

            if (!childNodeValues.ContainsKey(m_staffGroupPkey))
            {
                childNodeValues.Add(m_staffGroupPkey, m_staffGroupPkey);
            }

            foreach (KeyValuePair<int, int> index in childNodeValues)
            {
                SortedDictionary<int, StaffTable> temp = Staff.getInctance().getStaffInfoFromDepartmentPkey(index.Value);

                foreach (KeyValuePair<int, StaffTable> i in temp)
                {
                    StaffTable staff = new StaffTable();
                    staffList.Add(staffList.Count, (StaffTable)i.Value);
                }
            }

            return staffList;
        }

        private void dataGridViewStaffList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0 && e.RowIndex < m_staffRecordCount))
            {
                for (int i = 0; i < this.dataGridViewStaffList.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dataGridViewStaffList.ColumnCount; j++)
                    {
                        this.dataGridViewStaffList.Rows[i].Cells[j].Selected = false;
                    }
                }

                dataGridViewStaffList.Rows[e.RowIndex].Selected = true;
                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewStaffList.Rows[e.RowIndex].Cells[0].Value.ToString());
                m_currentDataGridViedRecordCompanyName = dataGridViewStaffList.Rows[e.RowIndex].Cells[2].Value.ToString();

                contextMenuStripDataGridView.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void SuppierGroupAdd_Click(object sender, EventArgs e)
        {
            ArrayList tables = new ArrayList();
            tables.Add("BASE_DEPARTMENT_LIST");

            FormBaseAuxiliaryMaterial fbam = new FormBaseAuxiliaryMaterial(tables, "部门信息管理", true);
            fbam.ShowDialog();

            StaffOrgStructTable userOrgStructData = new StaffOrgStructTable();
            userOrgStructData.value = fbam.getSelectRecordPkey();
            userOrgStructData.parentPkey = StaffOrgStruct.getInctance().getPkeyFromValue(m_staffGroupPkey);

            if (userOrgStructData.value != 0)
            {
                StaffOrgStruct.getInctance().insert(userOrgStructData);
                refreshTreeView();
            }
        }

        public int getSelectRecordPkey()
        {
            return m_currentDataGridViedRecordPkey;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(703);

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