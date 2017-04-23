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
            m_excelIsOpen = false;
            m_orderType = orderType;
            m_billNubmber = billNubmber;
            m_dataGridView = dataGridView;

            m_tempFilePath = Directory.GetCurrentDirectory() + "\\tdmb\\" + DbPublic.getInctance().getOrderNameFromType(orderType) + ".xlsx";
        }

        ~ FormOrderPrint()
        {
        }

        private void FormOrderPrint_Load(object sender, EventArgs e)
        {
            radioButton2007_Click(null, null);
            displayPrintDevInfo();


            if (!File.Exists(m_tempFilePath))
            {
                this.labelExportStatus.Text = "错误(模板文件不存在)";
                return;
            }
            else
            {
                toolStripButtonReExport_Click(null, null);
            }

            this.radioButton2007.Checked = true;
            this.radioButton2003.Checked = false;
            this.radioButton2007.Checked = true;
        }

        #region toolBar功能实现

        private void toolStripButtonReExport_Click(object sender, EventArgs e)
        {
            string exportExcelName = generateExportFileName();

            // 把模板文件移动到导出文件的目录，如果目标文件已经存在，则直接覆盖
            if (File.Exists(exportExcelName))
            {
                if ((File.GetAttributes(exportExcelName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // 如果是将文件的属性设置为Normal
                    File.SetAttributes(exportExcelName, FileAttributes.Normal);
                }

                File.Delete(exportExcelName);
            }

            File.Copy(m_tempFilePath, exportExcelName);
            File.SetAttributes(exportExcelName, FileAttributes.Normal);

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
                proc.Kill();
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
            PrintDocument prtdoc = new PrintDocument();

            this.labelPrintDevName.Text = prtdoc.PrinterSettings.PrinterName;//获取默认的打印机名

            if (this.labelPrintDevName.Text.IndexOf("未设置默认") >= 0)
            {
                this.labelStatus.Text = "未就绪";
                this.labelPageSize.Text = "0";
                this.labelPageSize.Text = "0";
                this.labelPage.Text = "未知";
            }
            else
            {
                this.labelStatus.Text = "就绪";
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
                if (m_orderType == 14)
                {
                    // 领料单
                    exportMaterielOutData();
                }
                else if (m_orderType == 2)
                {
                    // 采购入库单
                    exportPurchaseInData();
                }
                else if (m_orderType == 18)
                {
                    // 采购申请单
                    exportPurchaseApplyData();
                }
                
                m_excelWorkbook.Save();

                isRet = true;
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                release();
            }

            finally
            {
                release();
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
                m_excelSheet = (Excel.Worksheet)m_excelWorkbook.Sheets["单据模板"];

                m_excelApp.Visible = false;

                isRet = true;
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning("输出导出出错:" + error.Message);
                release();
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
            // 生产领料单数据导出
            MaterielOutOrderTable table = new MaterielOutOrderTable();
            table = MaterielOutOrder.getInctance().getMaterielOutOrderInfoFromBillNumber(m_billNubmber);

            FormProjectMaterielTable projectInfo = FormProject.getInctance().getProjectInfoFromBillNumber(table.srcOrderNum);

            stringReplace(projectInfo.projectName, "[1]");
            stringReplace(table.billNumber, "[2]");
            stringReplace(table.srcOrderNum, "[3]");
            stringReplace(table.makeNo, "[4]");
            stringReplace(table.exchangesUnit, "[5]");
            stringReplace(projectInfo.projectNum, "[6]");
            stringReplace(table.makeOrderStaffName, "[7]");
            stringReplace(projectInfo.deviceMode, "[9]");
            stringReplace(projectInfo.subName, "[10]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.MatetielNumber].Value.ToString());
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);

                ProjectManagerDetailsTable tmp = ProjectManagerDetails.getInctance().getMaterielInfoFromBillNumber(table.srcOrderNum, materielID);

                m_excelApp.Cells[row + 6, 1] = tmp.no;
                m_excelApp.Cells[row + 6, 2] = tmp.sequence;
                m_excelApp.Cells[row + 6, 3] = record.brand;
                m_excelApp.Cells[row + 6, 4] = record.name;
                m_excelApp.Cells[row + 6, 5] = record.model;

                m_excelApp.Cells[row + 6, 6] = tmp.cl;
                m_excelApp.Cells[row + 6, 7] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.Unit].Value.ToString().Trim();

                m_excelApp.Cells[row + 6, 8] = tmp.value;
                m_excelApp.Cells[row + 6, 9] = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_STORAGE_LIST", record.storage);
                m_excelApp.Cells[row + 6, 10] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.Note].Value.ToString().Trim();

                sum += tmp.value;

            }

            stringReplace(Convert.ToString(sum), "[8]");
        }

        private void exportPurchaseInData()
        {
            // 采购入库单数据导出
            PurchaseInOrderTable table = new PurchaseInOrderTable();
            table = PurchaseInOrder.getInctance().getPurchaseInfoFromBillNumber(m_billNubmber);

            FormProjectMaterielTable projectInfo = FormProject.getInctance().getProjectInfoFromBillNumber(table.srcOrderNum);

            stringReplace(table.supplierName, "[1]");
            stringReplace(table.billNumber, "[2]");
            stringReplace(table.srcOrderNum, "[3]");
            stringReplace(projectInfo.projectNum, "[4]");
            stringReplace(table.makeOrderStaffName, "[9]");
            stringReplace(projectInfo.projectName, "[10]");
            stringReplace(" ", "[11]");
            stringReplace(table.exchangesUnit, "[12]");

            double sum1 = 0.0, sum2 = 0.0, sum3 = 0.0, sum4 = 0.0;

            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[1].Value.ToString().Length == 0 && m_dataGridView.Rows[row].Cells[2].Value.ToString().Length == 0)
                {
                    break;
                }
                else
                {
                    if (m_dataGridView.Rows[row].Cells[9].Value.ToString().Length > 0)
                    {
                        sum1 += Convert.ToDouble(m_dataGridView.Rows[row].Cells[9].Value.ToString());
                    }

                    if (m_dataGridView.Rows[row].Cells[10].Value.ToString().Length > 0)
                    {
                        sum2 += Convert.ToDouble(m_dataGridView.Rows[row].Cells[10].Value.ToString());
                    }

                    if (m_dataGridView.Rows[row].Cells[11].Value.ToString().Length > 0)
                    {
                        sum3 += Convert.ToDouble(m_dataGridView.Rows[row].Cells[11].Value.ToString());
                    }

                    if (m_dataGridView.Rows[row].Cells[13].Value.ToString().Length > 0)
                    {
                        sum4 += Convert.ToDouble(m_dataGridView.Rows[row].Cells[13].Value.ToString());
                    }
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[1].Value.ToString());
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);
                m_excelApp.Cells[row + 6, 1] = m_dataGridView.Rows[row].Cells[1].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 2] = record.brand;
                m_excelApp.Cells[row + 6, 3] = m_dataGridView.Rows[row].Cells[2].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 4] = m_dataGridView.Rows[row].Cells[3].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 5] = record.model;
                m_excelApp.Cells[row + 6, 6] = m_dataGridView.Rows[row].Cells[7].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 7] = m_dataGridView.Rows[row].Cells[8].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 8] = m_dataGridView.Rows[row].Cells[9].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 9] = m_dataGridView.Rows[row].Cells[10].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 10] = m_dataGridView.Rows[row].Cells[12].Value.ToString().Trim();
                m_excelApp.Cells[row + 6, 11] = m_dataGridView.Rows[row].Cells[13].Value.ToString().Trim();
            }

            stringReplace(Convert.ToString(sum1), "[5]");
            stringReplace(Convert.ToString(sum2), "[6]");
            stringReplace(Convert.ToString(sum3), "[7]");
            stringReplace(Convert.ToString(sum4), "[8]");
        }

        private void exportPurchaseApplyData()
        {
            int startRowIndex = 4;
            // 采购入库单数据导出
            PurchaseApplyOrderTable table = new PurchaseApplyOrderTable();
            table = PurchaseApplyOrder.getInctance().getPurchaseInfoFromBillNumber(m_billNubmber);

            FormProjectMaterielTable projectInfo = FormProject.getInctance().getProjectInfoFromBillNumber(table.srcOrderNum);

            stringReplace(table.billNumber, "[1]");
            stringReplace(table.srcOrderNum, "[2]");
            stringReplace(table.exchangesUnit, "[3]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.MatetielNumber].Value.ToString());
                ProjectManagerDetailsTable tmp = ProjectManagerDetails.getInctance().getMaterielInfoFromBillNumber(table.srcOrderNum, materielID);

                m_excelApp.Cells[row + startRowIndex, 2] = projectInfo.projectName;
                m_excelApp.Cells[row + startRowIndex, 3] = m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.Brand].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 4] = "";
                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 7] = tmp.cl;
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 9] = m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 10] = tmp.useDate;
                m_excelApp.Cells[row + startRowIndex, 11] = "";
                m_excelApp.Cells[row + startRowIndex, 12] = "";

                sum += tmp.value;
            }

            stringReplace(Convert.ToString(sum), "[4]");
        }

        private void release()
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
        #endregion

        private void radioButton2003_Click(object sender, EventArgs e)
        {
            this.radioButton2003.Checked = true;
            this.radioButton2007.Checked = false;
        }

        private void radioButton2007_Click(object sender, EventArgs e)
        {
            this.radioButton2003.Checked = false;
            this.radioButton2007.Checked = true;

        }
    }
}
