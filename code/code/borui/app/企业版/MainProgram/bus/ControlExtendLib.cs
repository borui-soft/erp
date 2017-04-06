using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;
using MainProgram.model;
using TIV.Core.TivLogger;

namespace MainProgram.bus
{
    class IControlLibrary : Object
    {

    }

    class TreeViewExtend : IControlLibrary
    {
        private TreeView m_treeView;

        public TreeViewExtend(TreeView treeView)
        {
            m_treeView = treeView;
        }

        public TreeNode addNode(TreeNode parentNode, string text, int imageIndex, int selectImageIndex, string key)
        {
            return parentNode.Nodes.Add(key, text, imageIndex, selectImageIndex);
        }

        public TreeNode addNodeToCurrentNode(string text, int imageIndex, int selectImageIndex, string key)
        {
            return m_treeView.SelectedNode.Nodes.Add(key, text, imageIndex, selectImageIndex);
        }

        public void addLeafageNode(TreeNode parentNode, string text, int imageIndex, int selectImageIndex, string key)
        {
            parentNode.Nodes.Add(key, text, imageIndex, selectImageIndex);
        }

        public void addLeafageNodeToCurrentNode(string text, int imageIndex, int selectImageIndex, string key)
        {
            m_treeView.SelectedNode.Nodes.Add(key, text, imageIndex, selectImageIndex);
        }
    }

    class ListViewExtend : IControlLibrary
    {
        public ListViewExtend()
        {
        }

        public static void insertDataToListView(ListView listView, ArrayList values)
        {
            int count = listView.Items.Count;

            if (values.Count == listView.Columns.Count)
            {
                ListViewItem item = new ListViewItem();

                if (values.Count > 0)
                {
                    item.SubItems[0].Text = (string)values[0];
                    for (int i = 1; i < values.Count; i++)
                    {
                        item.SubItems.Add((string)values[i]);
                    }
                }

                listView.Items.Add(item);

                if (listView.Items.Count % 2 == 0)
                {
                    listView.Items[count].BackColor = System.Drawing.SystemColors.Info;
                }
            }
            else
            {
                // 出错
            }
        }

        public static void setListViewAttribute(ListView ListViewName, int rowHeight)
        {
            ListViewName.GridLines = true;
            ListViewName.FullRowSelect = true;
            ListViewName.View = View.Details;
            ListViewName.Scrollable = true;
            ListViewName.MultiSelect = false;
            ListViewName.HeaderStyle = ColumnHeaderStyle.Clickable;

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, rowHeight);
            ListViewName.SmallImageList = imgList;
        }
    }

    class MessageBoxExtend : IControlLibrary
    {
        public MessageBoxExtend()
        {
        }

        public static bool messageQuestion(string msg)
        {
            bool isRet = false;

            if (MessageBox.Show(msg, " ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).ToString() == "Yes")
            {
                isRet = true;
            }

            return isRet;
        }

        public static void messageOK(string msg)
        {
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK);
        }

        public static void messageError(string msg)
        {
            MessageBox.Show(msg, "错误 ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void messageWarning(string msg)
        {
            MessageBox.Show(msg, "警告 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    class PanelExtend : IControlLibrary
    {
        public PanelExtend()
        {
        }

        public static void setLableControlStyle(Label lableControlName)
        {
            if (lableControlName.ForeColor == System.Drawing.Color.Red)
            {
                lableControlName.ForeColor = System.Drawing.Color.Black;
                lableControlName.Font = new Font(lableControlName.Font.Name, lableControlName.Font.Size);
            }
            else
            {
                lableControlName.ForeColor = System.Drawing.Color.Red;
                lableControlName.Font = new Font(lableControlName.Font.Name, lableControlName.Font.Size, FontStyle.Underline);
            }
        }
    }

    class DataGridViewExtend : IControlLibrary
    {
        protected DataGridView m_dataGridView;

        protected SortedDictionary<int, DataGridViewColumnInfoStruct> m_columnsInfo = new SortedDictionary<int, DataGridViewColumnInfoStruct>();

        public DataGridViewExtend()
        {
        }

        public void initDataGridViewColumn(DataGridView dataGridView)
        {
            m_dataGridView = dataGridView;
            dataGridView.ColumnCount = m_columnsInfo.Count;
            setDataGridViewStyle();

            for (int i = 0; i < m_columnsInfo.Count; i++)
            {
                DataGridViewColumnInfoStruct column = new DataGridViewColumnInfoStruct();
                column = (DataGridViewColumnInfoStruct)m_columnsInfo[i];

                dataGridView.Columns[i].Width = column.Width;
                dataGridView.Columns[i].HeaderText = column.headerText;
                dataGridView.Columns[i].Visible = column.isVisiable;
            }
        }

        public void initDataGridViewData(SortedDictionary<int, ArrayList> data, int columnFrozenCount = 0)
        {
            if (data.Count > 0)
            {
                m_dataGridView.RowCount = data.Count;
                ArrayList temp = new ArrayList();
                for (int i = 0; i < data.Count; i++)
                {
                    temp = (ArrayList)data[i];

                    for (int j = 0; j < temp.Count; j++)
                    {
                        m_dataGridView.Rows[i].Cells[j].Value = temp[j];
                    }
                }

                if (columnFrozenCount != 0)
                {
                    m_dataGridView.Columns[columnFrozenCount].Frozen = true;
                }
            }
            else
            {
                m_dataGridView.Rows.Clear();
            }
        }

        public void addDataGridViewColumn(string headerText, int Width, bool isVisiable = true)
        {
            DataGridViewColumnInfoStruct column = new DataGridViewColumnInfoStruct();

            column.headerText = headerText;
            column.Width = Width;
            column.isVisiable = isVisiable;

            m_columnsInfo.Add(m_columnsInfo.Count, column);
        }

        public void clearDataGridViewColumn()
        {
            m_columnsInfo.Clear();
        }

        public void dataGridViewExportToExecl(string outPath)
        {
            try
            {
                Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                Excel.Worksheet excelSheet = (Excel.Worksheet)excelWorkbook.Sheets["Sheet1"];

                if (excelApp == null)
                {
                    MessageBoxExtend.messageWarning("数据导出失败, Excel安装进程存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                    return;
                }

                if (excelWorkbook == null)
                {
                    MessageBoxExtend.messageWarning("数据导出失败, Excel工作表存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                    return;
                }

                if (excelSheet == null)
                {
                    MessageBoxExtend.messageWarning("数据导出失败, Excel sheet表存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                    return;
                }

                excelApp.Visible = false;

                try
                {
                    //向Excel中写入表格的表头
                    int displayColumnsCount = 1;
                    for (int i = 0; i <= m_dataGridView.ColumnCount - 1; i++)
                    {
                        if(m_dataGridView.Columns[i].Visible)
                        {
                            excelApp.Cells[1, displayColumnsCount] = m_dataGridView.Columns[i].HeaderText.Trim();
                            ((Excel.Range)excelSheet.Columns[displayColumnsCount, System.Type.Missing]).ColumnWidth = m_dataGridView.Columns[i].Width * 1.0 / 8;
                            displayColumnsCount++;
                        }
                    }

                    for (int row = 0; row < m_dataGridView.RowCount; row++)
                    {
                        displayColumnsCount = 1;
                        for (int col = 0; col < m_dataGridView.ColumnCount; col++)
                        {
                            if (m_dataGridView.Rows[row].Cells[col].Visible)
                            {
                                excelApp.Cells[row + 2, displayColumnsCount] = m_dataGridView.Rows[row].Cells[col].Value.ToString().Trim();
                                displayColumnsCount++;
                            }
                        }
                    }

                    excelWorkbook.SaveAs(outPath);
                }
                catch (Exception error)
                {
                    MessageBoxExtend.messageWarning(error.Message);
                    return;
                }
                finally
                {
                    //关闭Excel应用    
                    if (excelWorkbook != null)
                    {
                        excelWorkbook.Close();
                    }

                    if (excelApp.Workbooks != null)
                    {
                        excelApp.Workbooks.Close();
                    }

                    if (excelApp != null)
                    {
                        excelApp.Quit();
                    }

                    excelWorkbook = null;
                    excelApp = null;
                }

                MessageBoxExtend.messageOK("导出成功\n\n" + outPath);
            }
            catch (Exception ex)
            {
                MessageBoxExtend.messageError(ex.Message);
            }
        }

        public void printDataGridView(string docTitle = "")
        {
            PrintDataGridView.Print_DataGridView(m_dataGridView, docTitle);
        }

        private void setDataGridViewStyle()
        {
            // 系统所有DataGridView统一风格

            // 白色背景
            m_dataGridView.BackgroundColor = System.Drawing.Color.White;

            // 灰色网格线
            m_dataGridView.GridColor = System.Drawing.Color.Silver;
            
            // 数据为只读
            m_dataGridView.ReadOnly = true;

            // 不能同时选中多行
            m_dataGridView.MultiSelect = false;

            // 禁止用户往DataGridView尾部添加新行
            m_dataGridView.AllowUserToAddRows = false;

            // 行头是否显示
            m_dataGridView.RowHeadersVisible = false;

            // 禁止用户用鼠标拖动DataGridView行高
            m_dataGridView.EnableHeadersVisualStyles = false;
            m_dataGridView.AllowUserToResizeRows = false;

            // 行高统一为18
            m_dataGridView.RowTemplate.Height = 18;
        }
    }

    class DataGridViewColumnInfoStruct
    {
        public string headerText { get; set; }
        public int Width { get; set; }
        public bool isVisiable { get; set; }
        public bool isReadOnly { get; set; }
    }

    class ComboBoxExtend : IControlLibrary
    {
        public ComboBoxExtend()
        {
        }
        
        public static void initComboBox(ComboBox comboBox, string tableName, bool isDefaultValue = false)
        {
            SortedDictionary<int, AuxiliaryMaterialDataTable> data = AuxiliaryMaterial.getInctance().getAllAuxiliaryMaterialData(tableName);

            foreach (KeyValuePair<int, AuxiliaryMaterialDataTable> index in data)
            {
                AuxiliaryMaterialDataTable area = new AuxiliaryMaterialDataTable();
                area = index.Value;
                comboBox.Items.Add(area.name);
            }

            if (isDefaultValue && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
    }

    class ConvertExtend : IControlLibrary
    {
        //public static string toString(int value)
        //{
        //    string strValue = "";
            
        //    if (value != null)
        //    {
        //        strValue = Convert.ToString(value);
        //    }

        //    return strValue;
        //}

        public static int toInt32(string value)
        {
            int iValue = -1;

            if (value != null && value.Length > 0)
            {
                iValue = Convert.ToInt32(value.ToString());
            }

            return iValue;
        }
    }
}