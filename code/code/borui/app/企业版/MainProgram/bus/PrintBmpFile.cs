using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing.Printing;

namespace MainProgram.bus
{
    class PrintBmpFile
    {
        private string m_docName = "";
        static private PrintBmpFile m_instance = null;

        public void printCurrentWin(int width, int height, int locationX, int locationY, bool isPrintPreview = false)
        {
            Bitmap bmp = new Bitmap(width - 10, height - 90);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(locationX + 6, locationY + 80, 0, 0, new Size(width - 10, height - 90), CopyPixelOperation.SourceCopy);
            g.Dispose();

            SaveFile(bmp);

            printBmpFile(isPrintPreview);
        }

        private void printBmpFile(bool isPrintPreview)
        {
            PrintDocument pd = new PrintDocument();

            //…Ë÷√±ﬂæ‡
            Margins margin = new Margins(0, 0, 80, 80);
            pd.DefaultPageSettings.Margins = margin;
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            try
            {
                PageSetupDialog psd = new PageSetupDialog();
                psd.Document = pd;
                psd.PageSettings.Landscape = true;

                if (isPrintPreview)
                {
                    //¥Ú”°‘§¿¿
                    PrintPreviewDialog ppd = new PrintPreviewDialog();
                    ppd.Document = pd;

                    if (DialogResult.OK == ppd.ShowDialog())
                    {
                        pd.Print();
                    }
                }
                else
                {
                    pd.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "¥Ú”°≥ˆ¥Ì", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }
        }

        private void SaveFile(Bitmap bmSave)
        {
            m_docName = getTempFileName();
            bmSave.Save(m_docName, System.Drawing.Imaging.ImageFormat.Bmp);
            bmSave.Dispose();
        }

        private PrintBmpFile()
        {
        }

        static public PrintBmpFile getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new PrintBmpFile();
            }

            return m_instance;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image temp = Image.FromFile(m_docName);
            Rectangle destRect = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y, temp.Width, temp.Height);
            e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
        }

        private string getTempFileName()
        {
            string name = System.Environment.GetEnvironmentVariable("TEMP") + "\\";

            name += DateTime.Now.ToString("yyyyMMddHHmmss");
            name += ".bmp";

            return name;
        }
    }
}