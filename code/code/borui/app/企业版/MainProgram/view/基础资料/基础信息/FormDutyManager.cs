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

    public partial class FormBaseDuryManager : Form
    {
        private bool m_isManagerMode = false;
        private int m_dataGridViewActionRowCount = 0;
        private int m_dataGridViewActionColumnCount = 0;

        private int m_groupPkey = 0;
        private string m_groupName = "";

        private TreeNode m_rootNode;
        private TreeViewExtend m_tree;

        private string m_userName = "";

        public FormBaseDuryManager(string userName, bool isManagerMode = true)
        {
            InitializeComponent();

            m_userName = userName;

            if(userName.Length != 0)
            {
                this.Text = "用户[" + userName + "]权限查看";
            }

            m_isManagerMode = isManagerMode;
        }

        private void FormBaseDuryManager_Load(object sender, EventArgs e)
        {
            // 树控件初始化
            refreshTreeView();
            updateDataGridView(SystemModule.getInctance().getAllSystemModuleInfo());

            if(!m_isManagerMode)
            {
                this.toolStripButtonSave.Visible = false;
                this.dataGridViewActionList.ReadOnly = true;
            }
        }

        private void updateDataGridView(SortedDictionary<int, SystemModuleTable> systemModuleList)
        {
            // dataGridViewModule更新
            SortedDictionary<int, ArrayList> modules = new SortedDictionary<int, ArrayList>();
            SortedDictionary<int, ArrayList> actions = new SortedDictionary<int, ArrayList>();

            // dataGridViewAction更新
            int moduleActionMax = 0;

            dataGridViewActionList.Rows.Clear();
            dataGridViewActionList.Columns.Clear();

            foreach (KeyValuePair<int, SystemModuleTable> index in systemModuleList)
            {
                // 获得所有模块信息
                ArrayList arrModule = new ArrayList();
                arrModule.Add(index.Value.name);
                modules.Add(modules.Count, arrModule);

                // 获得模块对应的action
                ArrayList arrAction = new ArrayList();
                SortedDictionary<int, ActionTable> actionList = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(index.Value.id);

                if (actionList.Count > 0)
                {
                    foreach (KeyValuePair<int, ActionTable> i in actionList)
                    {
                        arrAction.Add(i.Value.actionName);
                    }
                }
                else
                {
                    arrAction.Add("");
                }

                if (actionList.Count > moduleActionMax)
                {
                    moduleActionMax = actionList.Count;
                }

                actions.Add(actions.Count, arrAction);
            }

            m_dataGridViewActionRowCount = actions.Count;
            m_dataGridViewActionColumnCount = moduleActionMax + 1;

            if (m_dataGridViewActionRowCount > 0)
            {
                this.dataGridViewActionList.RowCount = m_dataGridViewActionRowCount;

                initDataGridViewColumn(dataGridViewActionList, m_dataGridViewActionColumnCount);
                updateDataGridViewModule(modules);
                updateDataGridViewAction(actions);

                this.dataGridViewActionList.ColumnCount = this.dataGridViewActionList.ColumnCount - 1;
            }
        }

        private void refreshTreeView()
        {
            m_tree = new TreeViewExtend(this.treeView);
            m_rootNode = this.treeView.Nodes.Add(Convert.ToString(0), "子系统");

            SortedDictionary<int, SubSystemTable> records = SubSystem.getInctance().getAllSubSystemInfo();

            foreach (KeyValuePair<int, SubSystemTable> index in records)
            {
                SubSystemTable record = new SubSystemTable();
                record = index.Value;

                m_tree.addNode(m_rootNode, record.name, 0, 1, Convert.ToString(record.id));
            }

            m_rootNode.Expand();
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (this.dataGridViewActionList.RowCount > 0)
            {
                this.saveFileDialog1.Filter = "Excel 2007格式 (*.xlsx)|*.xlsx|Excel 2003格式 (*.xls)|*.xls";
                this.saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // m_dataGridViewExtendAction.dataGridViewExportToExecl(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据为空，无数据可导出!");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            // m_dataGridViewExtendAction.printDataGridView(m_groupName + "资料");
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

                if (m_groupPkey != 0)
                {
                    updateDataGridView(SystemModule.getInctance().getSystemModuleInfo(m_groupPkey));
                }
                else
                {
                    updateDataGridView(SystemModule.getInctance().getAllSystemModuleInfo());
                }
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

        private void initDataGridViewColumn(DataGridView dataGridView, int dataGridViewColumnCount = 1, string headerText = "", string name = "")
        {
            DataGridViewTextBoxColumn textc = new DataGridViewTextBoxColumn();
            textc.HeaderText = "模块名称";
            textc.ReadOnly = true;
            dataGridView.Columns.Insert(0, textc);

            for (int columnIndex = 1; columnIndex < dataGridViewColumnCount; columnIndex++)
            {
                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();

                column.HeaderText = headerText;
                column.Name = name;
                column.ReadOnly = false;
                column.FlatStyle = FlatStyle.Standard;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dataGridView.Columns.Insert(columnIndex, column);
            }
        }

        private void updateDataGridViewModule(SortedDictionary<int, ArrayList> data)
        {
            if (data.Count > 0)
            {
                for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                {
                    ArrayList temp = new ArrayList();
                    temp = (ArrayList)data[rowIndex];

                    for (int i = 0; i < temp.Count; i++)
                    {
                        dataGridViewActionList.Rows[rowIndex].Cells[0].Value = temp[i];
                        dataGridViewActionList.Rows[rowIndex].Height = 19;
                    }
                }
            }
        }

        private void updateDataGridViewAction(SortedDictionary<int, ArrayList> data)
        {
            // 数据初始化
            for (int rowIndex = 0; rowIndex < m_dataGridViewActionRowCount; rowIndex++)
            {
                ArrayList temp = new ArrayList();
                temp = (ArrayList)data[rowIndex];

                for (int columnIndex = 0; columnIndex < temp.Count; columnIndex++)
                {
                    dataGridViewActionList.Rows[rowIndex].Cells[columnIndex + 1] = new DataGridViewCheckBoxCellExtend(temp[columnIndex].ToString());
                    dataGridViewActionList.Rows[rowIndex].Height = 19;
                }

                for(int i = temp.Count; i < m_dataGridViewActionColumnCount; i ++)
                {
                    dataGridViewActionList.Rows[rowIndex].Cells[i + 1] = new DataGridViewCheckBoxCellExtend("");
                    dataGridViewActionList.Rows[rowIndex].Height = 19;
                }
            }

            // 查询数据库，把该用户已有的权限响应的复选框中打上勾
            for (int rowIndex = 0; rowIndex < m_dataGridViewActionRowCount; rowIndex++)
            {
                string moduleText = this.dataGridViewActionList.Rows[rowIndex].Cells[0].Value.ToString();

                for (int columnIndex = 1; columnIndex < m_dataGridViewActionColumnCount; columnIndex++)
                {
                    DataGridViewCheckBoxCellExtend dataGridViewCheckBoxCell;
                    dataGridViewCheckBoxCell = (DataGridViewCheckBoxCellExtend)this.dataGridViewActionList.Rows[rowIndex].Cells[columnIndex];

                    string checkBoxText = dataGridViewCheckBoxCell.getCheckText();

                    if (checkBoxText.Length > 0)
                    {
                        int actionID = MainProgram.model.Action.getInctance().getActionPkey(moduleText, checkBoxText);
                        if (ActionAndStaff.getInctance().isAccess(Staff.getInctance().getStaffPkeyFromName(m_userName), actionID))
                        {
                            this.dataGridViewActionList.Rows[rowIndex].Cells[columnIndex].Value = true;
                        }
                    }
                }
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int rowIndex = 0; rowIndex < m_dataGridViewActionRowCount; rowIndex++)
                {
                    string moduleText = this.dataGridViewActionList.Rows[rowIndex].Cells[0].Value.ToString();

                    // 删除该用户此module对应的所有权限
                    deleteUserDutyFromModuleText(moduleText);

                    for (int columnIndex = 1; columnIndex < m_dataGridViewActionColumnCount; columnIndex++)
                    {
                        insertActionAndStaffTableData(moduleText, rowIndex, columnIndex);
                    }
                }

                MessageBoxExtend.messageOK("数据保存成功");
            }
            catch (System.Exception)
            {
            	
            }
        }

        private void insertActionAndStaffTableData(string moduleText, int rowIndex, int columnIndex)
        {
            string checkBoxText;
            DataGridViewCheckBoxCellExtend dataGridViewCheckBoxCell;
            dataGridViewCheckBoxCell = (DataGridViewCheckBoxCellExtend)this.dataGridViewActionList.Rows[rowIndex].Cells[columnIndex];

            checkBoxText = dataGridViewCheckBoxCell.getCheckText();

            if (checkBoxText.Length > 0)
            {
                int actionID = MainProgram.model.Action.getInctance().getActionPkey(moduleText, checkBoxText);

                if (this.dataGridViewActionList.Rows[rowIndex].Cells[columnIndex].EditedFormattedValue.ToString().CompareTo("True") == 0)
                {
                    AtionAndStaffTable record = new AtionAndStaffTable();
                    record.actionID = actionID;
                    record.staffID = Staff.getInctance().getStaffPkeyFromName(m_userName);

                    ActionAndStaff.getInctance().insert(record);
                }
            }
        }

        private void deleteUserDutyFromModuleText(string moduleName)
        {
            /* 删除该用户此module对应的所有权限
            * 1、根据moduleText得到moduleID
            * 2、得到ModuleID对应的所有Action的ID
            * 3、根据当前用户ID和acitonID，删除BASE_ACTION_STAFF对应的记录
            */
            int moduleID = SystemModule.getInctance().getModuleIDFromModuleName(moduleName);
            ArrayList actions = MainProgram.model.Action.getInctance().getAllActionPkeyFromModuleID(moduleID);

            for(int i = 0; i < actions.Count; i++)
            {
                ActionAndStaff.getInctance().delete(Staff.getInctance().getStaffPkeyFromName(m_userName), (int)actions[i]);
            }
        }

        private void dataGridViewActionList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(this.dataGridViewActionList.Rows[0].Cells[0].EditedFormattedValue.ToString());
            //MessageBox.Show(this.dataGridViewActionList.Rows[0].Cells[5].Value.ToString());

            if (e.RowIndex == m_dataGridViewActionRowCount - 1)
            {
                return;
            }
        }

        private void dataGridViewActionList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == m_dataGridViewActionRowCount - 1)
            {
                return;
            }
        }
    }

    public class DataGridViewCheckBoxCellExtend : DataGridViewCheckBoxCell
    {
        private string m_checkBoxText;

        public DataGridViewCheckBoxCellExtend(string text)
        {
            m_checkBoxText = text;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, m_checkBoxText, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            Rectangle contentBounds = this.GetContentBounds(rowIndex);

            Point stringLocation = new Point();

            stringLocation.Y = cellBounds.Y + 5;
            stringLocation.X = cellBounds.X + contentBounds.Right + 5;

            graphics.DrawString(m_checkBoxText, Control.DefaultFont, System.Drawing.Brushes.Black, stringLocation);
        }

        public string getCheckText()
        {
            return m_checkBoxText;
        }
    }
}