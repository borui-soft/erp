using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using Excel = Microsoft.Office.Interop.Excel;
using MainProgram.bus;
using TIV.Core.TivLogger;

namespace MainProgram
{
    public partial class FormOrderPrint : Form
    {
        string m_billNubmber = "";
        int m_orderType = -1;
        string m_exportExcelName = "";

        public FormOrderPrint(int orderType, string billNubmber)
        {
            InitializeComponent();

            m_orderType = orderType;
            m_billNubmber = billNubmber;
        }

        private void FormOrderPrint_Load(object sender, EventArgs e)
        {
            // axFramerControl1.Open(m_fileName);
            this.radioButton2007.Checked = true;
            this.radioButton2003.Checked = false;

            getPrintDevInfo();

            toolStripButtonTry_Click(null, null);
        }

        private void getPrintDevInfo()
        {
            
            this.labelStatus.Text = "就绪";

            PrintDocument prtdoc = new PrintDocument();

            this.labelPrintDevName.Text = prtdoc.PrinterSettings.PrinterName;//获取默认的打印机名

            this.labelPageSize.Text = Convert.ToString(prtdoc.PrinterSettings.DefaultPageSettings.PaperSize.Width) + " * ";
            this.labelPageSize.Text += Convert.ToString(prtdoc.PrinterSettings.DefaultPageSettings.PaperSize.Height);

            if (prtdoc.PrinterSettings.DefaultPageSettings.Landscape)
            {
                this.labelPage.Text = "横向";
            }
            else
            {
                this.labelPage.Text = "纵向";
            }
        }

        private bool dataExport()
        {
            bool isRet = false;

            try
            {
                Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open("e:\\tdmb.xlsx");
                Excel.Worksheet excelSheet = (Excel.Worksheet)excelWorkbook.Sheets["采购入库单"];

                if (excelApp == null)
                {
                    MessageBoxExtend.messageWarning("数据导出失败, Excel安装进程存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                    return isRet;
                }

                if (excelWorkbook == null)
                {
                    MessageBoxExtend.messageWarning("数据导出失败, Excel工作表存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                    return isRet;
                }

                if (excelSheet == null)
                {
                    MessageBoxExtend.messageWarning("数据导出失败, Excel sheet表存在异常，请关闭到其他已经打开的Excel文件后重试一次.");
                    return isRet;
                }

                excelApp.Visible = false;

                try
                {
                    excelApp.Cells[1, 2] = "123                 456                  789\r\n234                       567                  678";

                    //for (int row = 0; row < m_dataGridView.RowCount; row++)
                    //{
                    //    displayColumnsCount = 1;
                    //    for (int col = 0; col < m_dataGridView.ColumnCount; col++)
                    //    {
                    //        if (m_dataGridView.Rows[row].Cells[col].Visible)
                    //        {
                    //            excelApp.Cells[row + 2, displayColumnsCount] = m_dataGridView.Rows[row].Cells[col].Value.ToString().Trim();
                    //            displayColumnsCount++;
                    //        }
                    //    }
                    //}

                    m_exportExcelName = System.IO.Path.GetTempPath() + m_billNubmber;

                    if (radioButton2007.Checked)
                    {
                        m_exportExcelName += "xlsc";
                    }
                    else
                    {
                        m_exportExcelName += "xls";
                    }

                    if (File.Exists(m_exportExcelName))
                    {
                        File.Delete(m_exportExcelName);
                    }

                    excelWorkbook.SaveAs(m_exportExcelName);

                    isRet = true;
                }
                catch (Exception error)
                {
                    MessageBoxExtend.messageWarning(error.Message);
                    return isRet;
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

                //MessageBoxExtend.messageOK("导出成功\n\n" + outPath);
            }
            catch (Exception ex)
            {
                MessageBoxExtend.messageError(ex.Message);
            }

            return isRet;
        }

        private void print_Click(object sender, EventArgs e)
        {
            // axFramerControl1.PrintOut();
        }

        private void save_Click(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
            getPrintDevInfo();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.printDialog1.ShowDialog();
            getPrintDevInfo();
        }

        private void printDisplay_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
            getPrintDevInfo();
        }

        private void toolStripButtonTry_Click(object sender, EventArgs e)
        {
            if (dataExport())
            {
                this.labelExportStatus.Text = "成功";
                this.open.Enabled = true;
                this.toolStripButtonTry.Enabled = false;

                if (m_exportExcelName.Length > 0)
                {
                    this.toolStripButtonPageSet.Enabled = true;
                    this.printDisplay.Enabled = true;

                    PrintDocument pd = new PrintDocument();
                    pd.DocumentName = m_exportExcelName;
                    this.pageSetupDialog1.Document = pd;
                    this.printDialog1.Document = pd;
                }
                else
                {
                    this.toolStripButtonPageSet.Enabled = false;
                    this.printDisplay.Enabled = false;
                }
            }
            else
            {
                this.labelExportStatus.Text = "失败";
                this.open.Enabled = false;
                this.toolStripButtonPageSet.Enabled = false;
                this.printDisplay.Enabled = false;

                this.toolStripButtonTry.Enabled = true;
            }
        }
    }
}
