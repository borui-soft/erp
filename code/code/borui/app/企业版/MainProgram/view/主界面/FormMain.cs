using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormMain : Form
    {
        private bool m_isExitQuestion = true;
        static public string DB_NAME = "DB-ERP";
        static public string DB_NAME_MASTER = "MASTER";

        static public int DATA_GRID_VIEW_DEFAULT_ROW_COUNT = 12;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //检测系统是否已经注册为正版
            if (!DbPublic.getInctance().isGenuineSoftware())
            {
                this.Text = this.Text+"(试用版)";
            }
            else 
            {
                // 如果已注册为正版，检测当初注册时硬盘序列号和本次启动机器硬盘序列号是否相同，如果相同则视为正常，否则视同一序列号多人使用
                if (DbPublic.getInctance().getRegisterSoftwareKey() != SettlmentWay.getInctance().getSoftwareKey())
                {
                    MessageBoxExtend.messageWarning("同一软件序列号涉嫌多单位使用，程序将立即退出。\n如需继续使用该系统，请联系软件供应商。");
                    CurrentLoginUser.getInctance().delete();
                    Process.GetCurrentProcess().Kill();
                }
            }

            // 当系统未被初始化时，单据菜单、序时簿菜单不能显示
            this.menuBill.Visible = InitSubSystemSign.getInctance().isFinancialSystemInit() &&
                InitSubSystemSign.getInctance().isBusinessSystemInit();
            this.menuSquence.Visible = InitSubSystemSign.getInctance().isFinancialSystemInit() &&
                InitSubSystemSign.getInctance().isBusinessSystemInit();

            // 左侧功能按钮初始化
            LoadFunctionZone();
            this.WindowState = FormWindowState.Maximized;

            // 更新状态栏信息
            string statusBarText = DbPublic.getInctance().getCurrentDateStage();
            statusBarText += "                              ";
            statusBarText += "用户：" + DbPublic.getInctance().getCurrentLoginUserName();
            this.toolStripStatusLabel.Text = statusBarText;
        }

        private void LoadFunctionZone()
        {
            Form frm = new FunctionZont(this);
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;

            this.panelFunctionZone.Controls.Add(frm);
            frm.Show();
            frm.Activate();
            frm.WindowState = FormWindowState.Normal;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_isExitQuestion)
            {
                if (MessageBoxExtend.messageQuestion("确定退出系统?"))
                {
                    // 更新BASE_SYSTEM_CURRENT_LOGIN_USER表
                    CurrentLoginUser.getInctance().delete();
                    e.Cancel = false;
                }
                else 
                {
                    e.Cancel = true;
                }
            }
            else 
            {
                // 更新BASE_SYSTEM_CURRENT_LOGIN_USER表
                CurrentLoginUser.getInctance().delete();
            }
        }

        #region 系统菜单项
        private void menuSystemLogin_Click(object sender, EventArgs e)
        {
            CurrentLoginUser.getInctance().delete();
            Process.Start(@"MainProgram.exe");
            m_isExitQuestion = false;
            Application.Exit();
        }

        private void menuSystemExit_Click(object sender, EventArgs e)
        {
            if (MessageBoxExtend.messageQuestion("确定退出系统?"))
            {
                // 更新BASE_SYSTEM_CURRENT_LOGIN_USER表
                CurrentLoginUser.getInctance().delete();
                m_isExitQuestion = false;
                Application.Exit();
            }
        }
        #endregion

        #region 单据菜单项
        private void menuBillPurchase_Click(object sender, EventArgs e)
        {
            FormPurchaseOrder fpo = new FormPurchaseOrder();
            fpo.ShowDialog();
        }

        private void menuBillPurchaseIn_Click(object sender, EventArgs e)
        {
            FormPurchaseInOrder fpio = new FormPurchaseInOrder();
            fpio.ShowDialog();
        }

        private void menuBillProductIn_Click(object sender, EventArgs e)
        {
            FormMaterielInOrder fmoo = new FormMaterielInOrder();
            fmoo.ShowDialog();
        }

        private void menuBillEarningsIn_Click(object sender, EventArgs e)
        {
            FormMaterielInEarningsOrder fmieo = new FormMaterielInEarningsOrder();
            fmieo.ShowDialog();
        }

        private void menuBillOtherIn_Click(object sender, EventArgs e)
        {
            FormMaterielInOtherOrder fmieo = new FormMaterielInOtherOrder();
            fmieo.ShowDialog();
        }

        private void menuBillSale_Click(object sender, EventArgs e)
        {
            FormSaleOrder fso = new FormSaleOrder();
            fso.ShowDialog();
        }

        private void menuBillSaleOut_Click(object sender, EventArgs e)
        {
            FormSaleOutOrder fsoo = new FormSaleOutOrder();
            fsoo.ShowDialog();
        }

        private void menuBillMaterielOut_Click(object sender, EventArgs e)
        {
            FormMaterielOutOrder fmoo = new FormMaterielOutOrder();
            fmoo.ShowDialog();
        }

        private void menuBillEarningsOut_Click(object sender, EventArgs e)
        {
            FormMaterielOutEarningsOrder fmoeo = new FormMaterielOutEarningsOrder();
            fmoeo.ShowDialog();
        }

        private void menuBillOtherOut_Click(object sender, EventArgs e)
        {
            FormMaterielOutOtherOrder fmooo = new FormMaterielOutOtherOrder();
            fmooo.ShowDialog();
        }

        private void menuBillReceive_Click(object sender, EventArgs e)
        {
            FormReceivableOrder fro = new FormReceivableOrder();
            fro.ShowDialog();
        }

        private void menuBillPayment_Click(object sender, EventArgs e)
        {
            FormPaymentOrder fpo = new FormPaymentOrder();
            fpo.ShowDialog();
        }
        #endregion

        #region 序时簿菜单项
        private void menuSquencePurchase_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseOrder);
            fpos.ShowDialog();
        }

        private void menuSquencePurchaseIn_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseIn);
            fpos.ShowDialog();
        }

        private void menuSquenceQuotation_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleQuotation);
            fphpc.ShowDialog();
        }

        private void menuSquenceSale_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOrder);
            fphpc.ShowDialog();
        }

        private void menuSquenceSaleOut_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(FormSaleOrderSequence.OrderType.SaleOut);
            fphpc.ShowDialog();
        }

        private void menuSquenceReceive_Click(object sender, EventArgs e)
        {
            FormReceivablerAndPaymentOrder frap = new FormReceivablerAndPaymentOrder(true);
            frap.ShowDialog();
        }

        private void menuSquencePayment_Click(object sender, EventArgs e)
        {
            FormReceivablerAndPaymentOrder frap = new FormReceivablerAndPaymentOrder(false);
            frap.ShowDialog();
        }
        #endregion

        #region 基础资料菜单项
        private void menuBaseInfoSystemInfo_Click(object sender, EventArgs e)
        {
            FormBaseSystemInfo fbsi = new FormBaseSystemInfo();
            fbsi.ShowDialog();
        }

        private void menuBaseInfoUserManager_Click(object sender, EventArgs e)
        {
            FormBaseUser fbu = new FormBaseUser();
            fbu.ShowDialog();
        }

        private void menuBaseInfoFuzhu_Click(object sender, EventArgs e)
        {
            FormBaseAuxiliaryMaterial fbam = new FormBaseAuxiliaryMaterial();
            fbam.ShowDialog();
        }

        private void menuBaseInfoSupplier_Click(object sender, EventArgs e)
        {
            FormBaseSupplier fbs = new FormBaseSupplier();
            fbs.ShowDialog();
        }

        private void menuBaseInfoCustomer_Click(object sender, EventArgs e)
        {
            FormBaseCustomer fbc = new FormBaseCustomer();
            fbc.ShowDialog();
        }

        private void menuBaseInfoMareriel_Click(object sender, EventArgs e)
        {
            FormBaseMateriel fbm = new FormBaseMateriel();
            fbm.ShowDialog();
        }

        private void menuBaseInfoBillSet_Click(object sender, EventArgs e)
        {
            FormBaseBillConfig fbbc = new FormBaseBillConfig();
            fbbc.ShowDialog();
        }

        private void menuBaseInfoSettlmentWay_Click(object sender, EventArgs e)
        {
            FormBaseSettlmentWay fbsw = new FormBaseSettlmentWay();
            fbsw.ShowDialog();
        }

        private void menuBaseInfoDbBack_Click(object sender, EventArgs e)
        {
            FormBaseDbBackupRecord fbdbr = new FormBaseDbBackupRecord();
            fbdbr.ShowDialog();
        }

        private void menuBaseInfoDbRoolBack_Click(object sender, EventArgs e)
        {
            FormBaseDbRollbackRecord fbdrr = new FormBaseDbRollbackRecord();
            fbdrr.ShowDialog();
        }

        private void menuBaseInfoOperLog_Click(object sender, EventArgs e)
        {
            FormBaseOperatorLog fbol = new FormBaseOperatorLog();
            fbol.ShowDialog();
        }
        #endregion

        #region 帮助菜单项
        private void menuHelpDocument_Click(object sender, EventArgs e)
        {
            string helpFileName = "";
            if (InitSubSystemSign.getInctance().isBusinessSystemInit() && InitSubSystemSign.getInctance().isFinancialSystemInit())
            {
                helpFileName = "用户使用手册.chm";
            }
            else
            {
                helpFileName = "系统初始化手册.chm";
            }

            try
            {
                // 打开帮助使用手册
                string path = Application.ExecutablePath;
                helpFileName = path.Substring(0, path.LastIndexOf("\\") + 1) + helpFileName;
                Process.Start(helpFileName);
            }
            catch (Exception)
            {
                MessageBoxExtend.messageWarning("帮助文件[" + helpFileName + "]不存在。");
            }
        }

        private void menuHelpQuestion_Click(object sender, EventArgs e)
        {
            FormSendQuestion fsq = new FormSendQuestion();
            fsq.ShowDialog();
        }

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            FormAbout fsq = new FormAbout();
            fsq.ShowDialog();
        }
        #endregion

        private void pictureBoxHelp_MouseEnter(object sender, EventArgs e)
        {
            if (labelHelp.ForeColor == System.Drawing.Color.Red)
            {
                labelHelp.ForeColor = System.Drawing.Color.White;
                labelHelp.Font = new Font(labelHelp.Font.Name, labelHelp.Font.Size);
            }
            else
            {
                labelHelp.ForeColor = System.Drawing.Color.Red;
                labelHelp.Font = new Font(labelHelp.Font.Name, labelHelp.Font.Size, FontStyle.Underline);
            }
        }

        private void pictureBoxQuestion_MouseEnter(object sender, EventArgs e)
        {
            if (labelQuestion.ForeColor == System.Drawing.Color.Red)
            {
                labelQuestion.ForeColor = System.Drawing.Color.White;
                labelQuestion.Font = new Font(labelHelp.Font.Name, labelQuestion.Font.Size);
            }
            else
            {
                labelQuestion.ForeColor = System.Drawing.Color.Red;
                labelQuestion.Font = new Font(labelQuestion.Font.Name, labelQuestion.Font.Size, FontStyle.Underline);
            }
        }

        private void pictureBoxQuestion_Click(object sender, EventArgs e)
        {
            FormSendQuestion fsq = new FormSendQuestion();
            fsq.ShowDialog();
        }

        private void menuHelpRegister_Click(object sender, EventArgs e)
        {
            FormRegister fr = new FormRegister();
            fr.ShowDialog();
        }
        private void menuModifyPassword_Click(object sender, EventArgs e)
        {
            // 2011-11-24新增修改个人密码功能
            FormModifyPassword fmp = new FormModifyPassword();
            fmp.ShowDialog();
        }
    }
}