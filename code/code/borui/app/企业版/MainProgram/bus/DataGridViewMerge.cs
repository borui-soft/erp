/// 对DataGridView进行合并操作的类
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace MainProgram.bus
{
    public class DataGridViewCellMerge
    {
        private static SortedList rowSpan = new SortedList();//取得需要重新绘制的单元格
        private static string rowValue = "";//重新绘制的文本框内容

        /// <summary>
        /// 
        /// DataGridView合并单元格(横向)
        /// </summary>
        /// <param name="dgv">绘制的DataGridview </param>
        /// <param name="cellArgs">绘制单元格的参数（DataGridview的CellPainting事件中参数）</param>
        /// <param name="minColIndex">起始单元格在DataGridView中的索引号</param>
        /// <param name="maxColIndex">结束单元格在DataGridView中的索引号</param>
        public static void MerageRowSpan(DataGridView dgv, DataGridViewCellPaintingEventArgs cellArgs, int minColIndex, int maxColIndex)
        {
            if (cellArgs.ColumnIndex < minColIndex || cellArgs.ColumnIndex > maxColIndex) return;

            Rectangle rect=new Rectangle();
            using (Brush gridBrush = new SolidBrush(dgv.GridColor),
                backColorBrush = new SolidBrush(cellArgs.CellStyle.BackColor))
            {
                //抹去原来的cell背景
                cellArgs.Graphics.FillRectangle(backColorBrush, cellArgs.CellBounds);
            }
            cellArgs.Handled = true;

            if (rowSpan[cellArgs.ColumnIndex] == null)
            {
                //首先判断当前单元格是不是需要重绘的单元格
                //保留此单元格的信息，并抹去此单元格的背景
                rect.X = cellArgs.CellBounds.X;
                rect.Y = cellArgs.CellBounds.Y;
                rect.Width = cellArgs.CellBounds.Width;
                rect.Height = cellArgs.CellBounds.Height;
                
                rowValue += cellArgs.Value.ToString();
                rowSpan.Add(cellArgs.ColumnIndex, rect);
                if (cellArgs.ColumnIndex != maxColIndex)
                    return;
                MeragePrint(dgv, cellArgs, minColIndex, maxColIndex); 
            }
            else
            {
                IsPostMerage(dgv, cellArgs, minColIndex, maxColIndex);
            }
        }

        public static void IsPostMerage(DataGridView dgv, DataGridViewCellPaintingEventArgs cellArgs, int minColIndex, int maxColIndex)
        {
            //比较单元是否有变化
            Rectangle rectArgs = (Rectangle)rowSpan[cellArgs.ColumnIndex];
            if (rectArgs.X != cellArgs.CellBounds.X || rectArgs.Y != cellArgs.CellBounds.Y
                || rectArgs.Width != cellArgs.CellBounds.Width || rectArgs.Height != cellArgs.CellBounds.Height)
            {
                rectArgs.X = cellArgs.CellBounds.X;
                rectArgs.Y = cellArgs.CellBounds.Y;
                rectArgs.Width = cellArgs.CellBounds.Width;
                rectArgs.Height = cellArgs.CellBounds.Height;
                rowSpan[cellArgs.ColumnIndex] = rectArgs;
            }

            MeragePrint(dgv,cellArgs,minColIndex,maxColIndex);
        }

        //画制单元格
        private static void MeragePrint(DataGridView dgv, DataGridViewCellPaintingEventArgs cellArgs, int minColIndex, int maxColIndex)
        {
            int width = 0;//合并后单元格总宽度
            int height = cellArgs.CellBounds.Height;//合并后单元格总高度
                
            for (int i = minColIndex; i <= maxColIndex;i++ )
            {
                width += ((Rectangle)rowSpan[i]).Width;
            }

            Rectangle rectBegin = (Rectangle)rowSpan[minColIndex];//合并第一个单元格的位置信息
            Rectangle rectEnd = (Rectangle)rowSpan[maxColIndex];//合并最后一个单元格的位置信息
                
            //合并单元格的位置信息
            Rectangle reBounds = new Rectangle();
            reBounds.X = rectBegin.X;
            reBounds.Y = rectBegin.Y;
            reBounds.Width = width - 1;
            reBounds.Height = height - 1;

            using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(cellArgs.CellStyle.BackColor))
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    // 画出上下两条边线，左右边线无
                    Point blPoint = new Point(rectBegin.Left, rectBegin.Bottom - 1);//底线左边位置
                    Point brPoint = new Point(rectEnd.Right - 1, rectEnd.Bottom - 1);//底线右边位置
                    cellArgs.Graphics.DrawLine(gridLinePen, blPoint, brPoint);//下边线 

                    Point tlPoint = new Point(rectBegin.Left, rectBegin.Top);//上边线左边位置
                    Point trPoint = new Point(rectEnd.Right - 1, rectEnd.Top);//上边线右边位置
                    cellArgs.Graphics.DrawLine(gridLinePen, tlPoint, trPoint); //上边线

                    Point ltPoint = new Point(rectBegin.Left, rectBegin.Top);//左边线顶部位置
                    Point lbPoint = new Point(rectBegin.Left, rectBegin.Bottom - 1);//左边线底部位置
                    cellArgs.Graphics.DrawLine(gridLinePen, ltPoint, lbPoint); //左边线

                    Point rtPoint = new Point(rectEnd.Right - 1, rectEnd.Top);//右边线顶部位置
                    Point rbPoint = new Point(rectEnd.Right - 1, rectEnd.Bottom - 1);//右边线底部位置
                    cellArgs.Graphics.DrawLine(gridLinePen, rtPoint, rbPoint); //右边线

                    //计算绘制字符串的位置
                    SizeF sf = cellArgs.Graphics.MeasureString(rowValue, cellArgs.CellStyle.Font);
                    float lstr = (width - sf.Width) / 2;
                    float rstr = (height - sf.Height) / 2;

                    //画出文本框
                    if (rowValue != "")
                    {
                        cellArgs.Graphics.DrawString(rowValue, cellArgs.CellStyle.Font,
                                                    new SolidBrush(cellArgs.CellStyle.ForeColor),
                                                        rectBegin.Left + lstr,
                                                        rectBegin.Top + rstr,
                                                        StringFormat.GenericDefault);
                    }
                }
                cellArgs.Handled = true;
            }
        }
    }
}