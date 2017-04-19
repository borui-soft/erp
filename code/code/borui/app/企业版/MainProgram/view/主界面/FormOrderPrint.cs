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
using System.Diagnostics;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormOrderPrint : Form
    {
        string m_billNubmber = "";
        int m_orderType = -1;
        string m_exportExcelName = "";
        string m_tempFilePath = "";
        bool m_excelIsOpen = false;
        DataGridView m_dataGridView = new DataGridView();

        Excel.Application m_excelApp = null;
        Excel.Workbook m_excelWorkbook = null;
        Excel.Worksheet m_excelSheet = null;

        public FormOrderPrint(int orderType, string billNubmber, DataGridView dataGridView)
        {
            InitializeComponent();

            m_orderType = orderType;
            m_billNubmber = billNubmber;
            m_dataGridView = dataGridView;

            m_tempFilePath = Directory.GetCurrentDirectory() + "\\tdmb\\" + DbPublic.getInctance().getOrderNameFromType(orderType) + ".xlsx";
        }

        private void FormOrderPrint_Load(object sender, EventArgs e)
        {
            displayPrintDevInfo();

            this.radioButton2007.Checked = true;
            this.radioButton2003.Checked = false;

            if (!File.Exists(m_tempFilePath))
            {
                this.labelExportStatus.Text = "错误(模板文件不存在)";
                return;
            }
            else
            {
                toolStripButtonReExport_Click(null, null);
            }
        }

        #region toolBar功能实现

        private void toolStripButtonReExport_Click(object sender, EventArgs e)
        {
            string exportExcelName = generateExportFileName();

            // 把模板文件移动到导出文件的目录，如果目标文件已经存在，则直接覆盖
            File.Copy(m_tempFilePath, exportExcelName, true);

            m_excelIsOpen = openExcelFile(exportExcelName);

            if (m_excelIsOpen && dataExportToExcel())
            {
                this.labelExportStatus.Text = "成功";
                this.open.Enabled = true;
                this.toolStripButtonTry.Enabled = false;

                m_exportExcelName = exportExcelName;

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
                this.open.Enabled = false;
                this.toolStripButtonPageSet.Enabled = false;
                this.printDisplay.Enabled = false;

                this.toolStripButtonTry.Enabled = true;
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            Process proc = null;

            try
            {
                proc = new Process();
                proc.StartInfo.FileName = m_exportExcelName;
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }

        private void printReview_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
            displayPrintDevInfo();
        }

        private void toolStripButtonPateSet_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
            displayPrintDevInfo();
        }

        private void toolStripButtonPrintDevSet_Click(object sender, EventArgs e)
        {
            this.printDialog1.ShowDialog();
            displayPrintDevInfo();
        }

        private void print_Click(object sender, EventArgs e)
        {
            // axFramerControl1.PrintOut();
        }

        private void displayPrintDevInfo()
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

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private string generateExportFileName()
        {
            string exportExcelName = System.IO.Path.GetTempPath() + m_billNubmber;

            if (radioButton2007.Checked)
            {
                exportExcelName += ".xlsx";
            }
            else
            {
                exportExcelName += ".xls";
            }

            return exportExcelName;
        }

        #region excel文件的操作

        private bool dataExportToExcel()
        {
            bool isRet = false;

            try
            {
                try
                {
                    if (m_orderType == 14)
                    {
                        // 领料单
                        exportMaterielOutData();
                    }
                    
                    m_excelWorkbook.Save();

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
                    if (m_excelWorkbook != null)
                    {
                        m_excelWorkbook.Close();
                    }

                    if (m_excelApp.Workbooks != null)
                    {
                        m_excelApp.Workbooks.Close();
                    }

                    if (m_excelApp != null)
                    {
                        m_excelApp.Quit();
                    }

                    m_excelWorkbook = null;
                    m_excelApp = null;
                }
            }
            catch (Exception ex)
            {
                MessageBoxExtend.messageError(ex.Message);
            }

            return isRet;
        }

        private bool openExcelFile(string fileName)
        {
            bool isRet = false;

            try
            {
                m_excelApp = new Microsoft.Office.Interop.Excel.Application();
                m_excelWorkbook = m_excelApp.Workbooks.Open(fileName);
                m_excelSheet = (Excel.Worksheet)m_excelWorkbook.Sheets["采购入库单"];

                m_excelApp.Visible = false;

                isRet = true;
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning("输出导出出错:" + error.Message);

                //关闭Excel应用    
                if (m_excelWorkbook != null)
                {
                    m_excelWorkbook.Close();
                }

                if (m_excelApp.Workbooks != null)
                {
                    m_excelApp.Workbooks.Close();
                }

                if (m_excelApp != null)
                {
                    m_excelApp.Quit();
                }

                m_excelWorkbook = null;
                m_excelApp = null;
            }

            return isRet;
        }

        private void stringReplace(string desc, string src)
        {
            Excel.Range oRange = null;
            oRange = ((Excel.Range)m_excelSheet.UsedRange).Find(src);
            if (oRange != null && oRange.Cells.Rows.Count >= 1 && oRange.Cells.Columns.Count >= 1)
            {
                oRange.Replace(src, desc);
            }
        }

        #endregion

        #region 单据详细情况导出
        private void exportMaterielOutData()
        {
            MaterielOutOrderTable table = new MaterielOutOrderTable();
            table = MaterielOutOrder.getInctance().getMaterielOutOrderInfoFromBillNumber(m_billNubmber);

            string projectNum = FormProject.getInctance().getProjectNumFromBillNumber(m_billNubmber);

            stringReplace(table.departmentName, "[1]");
            stringReplace(table.billNumber, "[2]");
            stringReplace(table.srcOrderNum, "[3]");
            stringReplace(table.makeNo, "[4]");
            stringReplace(table.exchangesUnit, "[5]");
            stringReplace(projectNum, "[6]");
            stringReplace(table.makeOrderStaffName, "[7]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                m_excelApp.Cells[row + 6, 2] = m_dataGridView.Rows[row].Cells[2].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 3] = m_dataGridView.Rows[row].Cells[3].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 4] = m_dataGridView.Rows[row].Cells[2].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 5] = m_dataGridView.Rows[row].Cells[5].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 6] = m_dataGridView.Rows[row].Cells[6].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 7] = m_dataGridView.Rows[row].Cells[8].Value.ToString().Trim();

                if(m_dataGridView.Rows[row].Cells[2].Value.ToString().Length == 0 && m_dataGridView.Rows[row].Cells[5].Value.ToString().Length == 0)
                {
                    break;
                }
                else
                {
                    sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[5].Value.ToString());
                }
            }

            stringReplace(Convert.ToString(sum), "[8]");
        }
        #endregion
    }
}
