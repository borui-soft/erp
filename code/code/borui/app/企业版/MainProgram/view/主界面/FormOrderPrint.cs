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
        DataGridView m_dataGridViewOfter = new DataGridView();

        Excel.Application m_excelApp = null;
        Excel.Workbook m_excelWorkbook = null;
        Excel.Worksheet m_excelSheet = null;

        public FormOrderPrint(int orderType, string billNubmber, DataGridView dataGridView, DataGridView dataGridView2 = null)
        {
            InitializeComponent();
            m_excelIsOpen = false;
            m_orderType = orderType;
            m_billNubmber = billNubmber;
            m_dataGridView = dataGridView;
            m_dataGridViewOfter = dataGridView2;

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
                else if (m_orderType == 0)
                {
                    // 采购申请单
                    exportPurchaseApplyData();
                }
                else if (m_orderType == 9)
                {
                    // 盘盈入库单
                    exportEarningsOrderData();
                }
                else if (m_orderType == 10)
                {
                    //其他入库单模板
                    exportOtherInData();
                }
                else if (m_orderType == 15)
                {
                    //盘亏毁损单模板
                    exportEarningsOutData();
                }
                else if (m_orderType == 16)
                {
                    // 其他出库单模板
                    exportOtherOutData();
                }
                else if (m_orderType == 51 || m_orderType == 52 || m_orderType == 53)
                {
                    // 总材料表模板
                    exportProjectInfoData();
                }
                else if (m_orderType == 54)
                {
                    //材料表变更模板
                    exportProjectInfoChangeData();
                }

                else if (m_orderType == 8)
                {
                    // 产品入库单模板
                    exportproductInData();
                }
                else if (m_orderType == 6)
                {
                    // 销售出库单模板
                    exportproductOutData();
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

                ProjectManagerDetailsTable tmp = new ProjectManagerDetailsTable();

                string xxMatetielTableRowNum = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.xxMatetielTableRowNum].Value.ToString();

                if (xxMatetielTableRowNum.Length > 0)
                {
                    tmp = ProjectManagerDetails.getInctance().getMaterielInfoFromRowNum(table.srcOrderNum, Convert.ToInt32(xxMatetielTableRowNum));
                }
                else 
                {
                    tmp = ProjectManagerDetails.getInctance().getMaterielInfoFromBillNumber(table.srcOrderNum, materielID);
                }

                m_excelApp.Cells[row + 6, 1] = tmp.no;
                m_excelApp.Cells[row + 6, 2] = tmp.sequence;
                m_excelApp.Cells[row + 6, 3] = record.brand;
                m_excelApp.Cells[row + 6, 4] = record.name;
                m_excelApp.Cells[row + 6, 5] = record.model;

                m_excelApp.Cells[row + 6, 6] = tmp.cl;
                m_excelApp.Cells[row + 6, 7] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.Unit].Value.ToString().Trim();

                m_excelApp.Cells[row + 6, 8] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOrder.DataGridColumnName.Value].Value.ToString().Trim();
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

                if (table.isRedBill == 1)
                {
                    m_excelApp.Cells[row + 6, 8] = Convert.ToDouble(m_dataGridView.Rows[row].Cells[9].Value.ToString().Trim()) * -1;
                    m_excelApp.Cells[row + 6, 9] = Convert.ToDouble(m_dataGridView.Rows[row].Cells[10].Value.ToString().Trim()) * -1;
                    m_excelApp.Cells[row + 6, 10] = Convert.ToDouble(m_dataGridView.Rows[row].Cells[11].Value.ToString().Trim()) * -1;
                    m_excelApp.Cells[row + 6, 11] = Convert.ToDouble(m_dataGridView.Rows[row].Cells[13].Value.ToString().Trim()) * -1;
                }
                else
                {
                    m_excelApp.Cells[row + 6, 8] = m_dataGridView.Rows[row].Cells[9].Value.ToString().Trim();
                    m_excelApp.Cells[row + 6, 9] = m_dataGridView.Rows[row].Cells[10].Value.ToString().Trim();
                    m_excelApp.Cells[row + 6, 10] = m_dataGridView.Rows[row].Cells[11].Value.ToString().Trim();
                    m_excelApp.Cells[row + 6, 11] = m_dataGridView.Rows[row].Cells[13].Value.ToString().Trim();
                }
            }


            if (table.isRedBill == 1)
            {
                stringReplace(Convert.ToString(sum1 * -1), "[5]");
                stringReplace(Convert.ToString(sum2 * -1), "[6]");
                stringReplace(Convert.ToString(sum3 * -1), "[7]");
                stringReplace(Convert.ToString(sum4 * -1), "[8]");
            }
            else
            {
                stringReplace(Convert.ToString(sum1), "[5]");
                stringReplace(Convert.ToString(sum2), "[6]");
                stringReplace(Convert.ToString(sum3), "[7]");
                stringReplace(Convert.ToString(sum4), "[8]");
            }
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
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormPurchaseApply.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 5] = "";
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

        private void exportEarningsOrderData()
        {
            int startRowIndex = 6;
            // 采购入库单数据导出
            MaterielInEarningsOrderTable table = new MaterielInEarningsOrderTable();
            table = MaterielInEarningsOrder.getInctance().getMaterielInEarningsOrderInfoFromBillNumber(m_billNubmber);

            stringReplace(table.billNumber, "[1]");

            if (table.isRedBill == 1)
            {
                stringReplace("(红字单据)", "[2]");
            }
            else
            {
                stringReplace("", "[2]");
            }

            stringReplace(table.makeOrderStaffName, "[3]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.MatetielNumber].Value.ToString().Trim());
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);

                m_excelApp.Cells[row + startRowIndex, 2] = materielID;
                m_excelApp.Cells[row + startRowIndex, 3] = record.brand;
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();

                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 7] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.Note].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormMaterielInEarningsOrder.DataGridColumnName.Value].Value.ToString().Trim());
            }

            stringReplace(Convert.ToString(sum), "[4]");
        }

        private void exportOtherInData()
        {
            int startRowIndex = 6;
            // 采购入库单数据导出
            MaterielInOtherOrderTable table = new MaterielInOtherOrderTable();
            table = MaterielInOtherOrder.getInctance().getMaterielInOtherOrderInfoFromBillNumber(m_billNubmber);

            stringReplace(table.billNumber, "[1]");
            stringReplace(table.exchangesUnit, "[2]");
            stringReplace(table.makeOrderStaffName, "[3]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.MatetielNumber].Value.ToString().Trim());
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);

                m_excelApp.Cells[row + startRowIndex, 2] = materielID;
                m_excelApp.Cells[row + startRowIndex, 3] = record.brand;
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();

                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 7] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.Note].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormMaterielInOtherOrder.DataGridColumnName.Value].Value.ToString().Trim());
            }

            stringReplace(Convert.ToString(sum), "[4]");
        }

        private void exportEarningsOutData()
        {
            int startRowIndex = 6;
            // 采购入库单数据导出
            MaterielOutEarningsOrderTable table = new MaterielOutEarningsOrderTable();
            table = MaterielOutEarningsOrder.getInctance().getMaterielOutEarningsOrderInfoFromBillNumber(m_billNubmber);

            stringReplace(table.billNumber, "[1]");

            if (table.isRedBill == 1)
            {
                stringReplace("(红字单据)", "[2]");
            }
            else
            {
                stringReplace("", "[2]");
            }

            stringReplace(table.makeOrderStaffName, "[3]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.MatetielNumber].Value.ToString().Trim());
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);

                m_excelApp.Cells[row + startRowIndex, 2] = materielID;
                m_excelApp.Cells[row + startRowIndex, 3] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 5] = record.brand;
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 7] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.Note].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormMaterielOutEarningsOrder.DataGridColumnName.Value].Value.ToString().Trim());
            }

            stringReplace(Convert.ToString(sum), "[4]");
        }

        private void exportOtherOutData()
        {
            int startRowIndex = 6;
            // 采购入库单数据导出
            MaterielOutOtherOrderTable table = new MaterielOutOtherOrderTable();
            table = MaterielOutOtherOrder.getInctance().getMaterielOutOtherOrderInfoFromBillNumber(m_billNubmber);

            stringReplace(table.billNumber, "[1]");
            stringReplace(table.exchangesUnit, "[2]");
            stringReplace(table.makeOrderStaffName, "[3]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                int materielID = Convert.ToInt32(m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.MatetielNumber].Value.ToString().Trim());
                MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);

                m_excelApp.Cells[row + startRowIndex, 2] = materielID; 
                m_excelApp.Cells[row + startRowIndex, 3] = record.brand;
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 7] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 9] = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_STORAGE_LIST", record.storage);
                m_excelApp.Cells[row + startRowIndex, 10] = m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.Note].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormMaterielOutOtherOrder.DataGridColumnName.Value].Value.ToString().Trim());
            }

            stringReplace(Convert.ToString(sum), "[4]");
        }

        private void exportProjectInfoData()
        {
            int startRowIndex = 6;
            // 总材料表套打
            FormProjectMaterielTable projectInfo = FormProject.getInctance().getProjectInfoFromBillNumber(m_billNubmber);

            int dataType = FormProject.getInctance().getOrderTypeFromBillNumber(m_billNubmber);
            string type = "设备";

            if (dataType == 1)
            {
                type = "设备";
            }
            else if (dataType == 2)
            {
                type = "电器";
            }
            else if (dataType == 3)
            {
                type = "工程";
            }

            stringReplace(type, "[1]");
            stringReplace(projectInfo.billNumber, "[2]");
            stringReplace(projectInfo.deviceMode, "[3]");
            stringReplace(projectInfo.subName, "[4]");
            stringReplace(projectInfo.projectName, "[5]");
            stringReplace(projectInfo.projectNum, "[6]");
            stringReplace(projectInfo.makeNum, "[7]");
            stringReplace(projectInfo.note, "[8]");
            stringReplace(projectInfo.makeOrderStaffName, "[10]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0 || row > 40)
                {
                    break;
                }

                m_excelApp.Cells[row + startRowIndex, 1] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Num].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 2] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Sequence].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 3] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.DeviceName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Brand].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 7] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Size].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.CL].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 9] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Parameter].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 10] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 11] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 12] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.MakeType].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 13] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.UseDate].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 14] = m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Note].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormProjectMaterielOrder.DataGridColumnName.Value].Value.ToString().Trim());
            }

            stringReplace(Convert.ToString(sum), "[9]");
        }

        private void exportProjectInfoChangeData()
        {
            int startRowIndex1 = 8;
            int startRowIndex2 = 22;

            // 总材料表套打
            FormProjectMaterielChangeTable projectInfoChange = FormProjectInfoChange.getInctance().getProjectInfoFromBillNumber(m_billNubmber);
            FormProjectMaterielTable projectInfo = FormProject.getInctance().getProjectInfoFromBillNumber(projectInfoChange.srcBillNumber);

            int dataType = FormProject.getInctance().getOrderTypeFromBillNumber(projectInfoChange.srcBillNumber);
            string type = "设备";

            if (dataType == 1)
            {
                type = "设备";
            }
            else if (dataType == 2)
            {
                type = "电器";
            }
            else if (dataType == 3)
            {
                type = "工程";
            }

            stringReplace(type, "[1]");
            stringReplace(projectInfo.billNumber, "[2]");
            stringReplace(projectInfoChange.billNumber, "[3]");
            stringReplace(projectInfo.projectName, "[4]");
            stringReplace(projectInfo.projectNum, "[5]");
            stringReplace(projectInfo.makeNum, "[6]");
            stringReplace(projectInfo.deviceMode, "[7]");
            stringReplace(projectInfoChange.makeOrderDate, "[8]");
            stringReplace(projectInfoChange.makeOrderStaffName, "[9]");
            stringReplace(projectInfo.subName, "[10]");

            // 变更后数据
            for (int row2 = 0; row2 < m_dataGridView.RowCount; row2++)
            {
                if (m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                m_excelApp.Cells[row2 + startRowIndex2, 1] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Num].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 2] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Sequence].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 3] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.DeviceName].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 4] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 5] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.CL].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 6] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 7] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Size].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 8] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 9] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 10] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Brand].Value.ToString().Trim();
                m_excelApp.Cells[row2 + startRowIndex2, 11] = m_dataGridView.Rows[row2].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Note].Value.ToString().Trim();
            }

            // 变更前数据
            if (m_dataGridViewOfter == null)
            {
                return;
            }

            for (int row = 0; row < m_dataGridViewOfter.RowCount; row++)
            {
                if (m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                m_excelApp.Cells[row + startRowIndex1, 1] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Num].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 2] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Sequence].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 3] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.DeviceName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 4] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 5] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.CL].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 6] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 7] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Size].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 8] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 9] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 10] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Brand].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex1, 11] = m_dataGridViewOfter.Rows[row].Cells[(int)FormProjectMaterielChangeOrder.DataGridColumnName.Note].Value.ToString().Trim();
            }
        }

        private void exportproductInData()
        {
            int startRowIndex = 6;

            // 产品入库单数据导出
            MaterielInOrderTable table = new MaterielInOrderTable();
            table = MaterielInOrder.getInctance().getMaterielInOrderInfoFromBillNumber(m_billNubmber);

            stringReplace(table.billNumber, "[1]");
            stringReplace(table.exchangesUnit, "[2]");
            stringReplace(table.tradingDate, "[3]");
            stringReplace(table.makeOrderStaffName, "[5]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                m_excelApp.Cells[row + startRowIndex, 2] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 3] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 8] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.Note].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.MakeNum].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormMaterielInOrder.DataGridColumnName.Value].Value.ToString().Trim());
            }

            stringReplace(Convert.ToString(sum), "[4]");
        }

        private void exportproductOutData()
        {
            int startRowIndex = 6;
            // 销售出库单单数据导出
            SaleOutOrderTable table = new SaleOutOrderTable();
            table = SaleOutOrder.getInctance().getSaleInfoFromBillNumber(m_billNubmber);

            stringReplace(table.billNumber, "[1]");
            stringReplace(table.exchangesUnit, "[2]");
            stringReplace(table.tradingDate, "[3]");
            stringReplace(table.makeOrderStaffName, "[5]");

            double sum = 0.0;
            for (int row = 0; row < m_dataGridView.RowCount; row++)
            {
                if (m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.MatetielNumber].Value.ToString().Length == 0)
                {
                    break;
                }

                m_excelApp.Cells[row + startRowIndex, 2] = m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.MatetielName].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 3] = m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.Model].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 4] = m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.Unit].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 5] = m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.Value].Value.ToString().Trim();
                m_excelApp.Cells[row + startRowIndex, 6] = m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.MakeNum].Value.ToString().Trim();

                sum += Convert.ToDouble(m_dataGridView.Rows[row].Cells[(int)FormSaleOutOrder.DataGridColumnName.Value].Value.ToString().Trim());
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
