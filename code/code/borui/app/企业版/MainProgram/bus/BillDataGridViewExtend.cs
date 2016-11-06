using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;

namespace MainProgram.bus
{
    class BillDataGridViewExtend : DataGridViewExtend
    {
        public BillDataGridViewExtend()
        {
        }

        new public void initDataGridViewColumn(DataGridView dataGridView)
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
                dataGridView.Columns[i].ReadOnly = column.isReadOnly;

                // 禁止点击列头自动排序功能
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (column.isReadOnly)
                {
                    dataGridView.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
                }
            }

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
        }

        public void initDataGridViewData(int dataGridViewRowCount)
        {
            m_dataGridView.RowCount = dataGridViewRowCount;

            for (int i = 0; i < dataGridViewRowCount; i++)
            {
                m_dataGridView.Rows[i].Cells[0].Value = Convert.ToString(i + 1);

                for (int j = 1; j < m_columnsInfo.Count; j++)
                {
                    m_dataGridView.Rows[i].Cells[j].Value = "";
                }

                m_dataGridView.Rows[i].Height = 19;
            }
        }

        public void addDataGridViewColumn(string headerText, int Width, bool isVisiable = true, bool isReadOnly = false)
        {
            DataGridViewColumnInfoStruct column = new DataGridViewColumnInfoStruct();

            column.headerText = headerText;
            column.Width = Width;
            column.isVisiable = isVisiable;
            column.isReadOnly = isReadOnly;

            m_columnsInfo.Add(m_columnsInfo.Count, column);
        }

        private void setDataGridViewStyle()
        {
            // 系统所有DataGridView统一风格

            // 白色背景
            m_dataGridView.BackgroundColor = System.Drawing.Color.White;

            // 灰色网格线
            m_dataGridView.GridColor = System.Drawing.Color.Silver;

            // 不能同时选中多行
            m_dataGridView.MultiSelect = false;

            // 禁止用户往DataGridView尾部添加新行
            m_dataGridView.AllowUserToAddRows = false;

            // 行头是否显示
            m_dataGridView.RowHeadersVisible = false;

            // 禁止用户用鼠标拖动DataGridView行高
            m_dataGridView.EnableHeadersVisualStyles = false;
            m_dataGridView.AllowUserToResizeRows = false;
        }

        public SortedDictionary<int, DataGridViewColumnInfoStruct> getDataGridViewColumns()
        {
            return m_columnsInfo;
        }

        public bool isValidDataGridViewCellValue(char keyChar, string dataGridViewCellValue)
        {
            bool isRet = false;

            if ((Convert.ToInt32(keyChar) < 48 || Convert.ToInt32(keyChar) > 57) &&
                Convert.ToInt32(keyChar) != 46 && Convert.ToInt32(keyChar) != 8 &&  Convert.ToInt32(keyChar) != 13)
            {
                isRet = true;  // 输入非法就屏蔽
            }
            else
            {
                if ((keyChar >= '0' && keyChar <= '9') || (keyChar == '\b'))
                {
                    isRet = false;
                }

                if (keyChar == '.')
                {
                    if (dataGridViewCellValue.IndexOf(".") != -1)
                    {
                        isRet = true;
                    }
                    else
                    {
                        isRet = false;
                    }
                }
            }

            return isRet;
        }

        // 验证是否存在空行，如果存在返回空行行号，否则返回-1
        public int getExistNullRow(int currentRow)
        {
            int nullRowNumber = -1;

            for (int i = 0; i < currentRow; i++)
            {
                if (m_dataGridView.Rows[i].Cells[1].Value.ToString().Length == 0)
                {
                    nullRowNumber = i;
                    break;
                }
            }

            return nullRowNumber;
        }

        public void clearDataGridViewRow(int rowIndex)
        {
            for (int i = 1; i < m_columnsInfo.Count; i++)
            {
                m_dataGridView.Rows[rowIndex].Cells[i].Value = "";
            }
        }
    }
}