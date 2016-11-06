using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormInit : Form
    {
        public FormInit()
        {
            InitializeComponent();
        }

        private void FormInit_Load(object sender, EventArgs e)
        {
        }

        // 存货数据初始化
        private void panelMaterielInit_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                FormInitMateriel fim = new FormInitMateriel();
                fim.ShowDialog();
            }
            else 
            {
                MessageBoxExtend.messageWarning("业务系统已启用，无法再录入期初数据");
            }
        }

        // 应收账款初始化
        private void panelAccountReceivable_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                FormInitAccountReceivabler fiar = new FormInitAccountReceivabler(true);
                fiar.ShowDialog();
            }
            else
            {
                MessageBoxExtend.messageWarning("业务系统已启用，无法再录入期初数据");
            }
        }

        // 应付账款初始化
        private void panelBusinessAccountsData_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                FormInitAccountReceivabler fiar = new FormInitAccountReceivabler(false);
                fiar.ShowDialog();
            }
            else
            {
                MessageBoxExtend.messageWarning("业务系统已启用，无法再录入期初数据");
            }
        }

        // 业务系统初始化
        private void panelBusinessInit_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                if (MessageBoxExtend.messageQuestion("确定所有的业务数据均已录入完毕并正式启用业务系统吗?"))
                {
                    try
                    {
                        InitSubSystemSign.getInctance().initBusinessSystem();

                        // 把业务系统相关初始数据写到企业利润表
                        CompanyProfit.getInctance().initBusinessSystemBalanceInfo();

                        MessageBoxExtend.messageOK("业务系统初始化成功.\n重新启动应用程序，业务相关子系统便可使用.");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("业务系统已启用，不要再次启用");
            }
        }

        // 库存现金
        private void panelMoneyData_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isFinancialSystemInit())
            {
                FormInitAssets fia = new FormInitAssets(false);
                fia.ShowDialog();
            }
            else
            {
                MessageBoxExtend.messageWarning("财务系统已启用，无法再录入期初数据");
            }
        }

        // 银行存款
        private void panelBankData_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isFinancialSystemInit())
            {
                FormInitAssets fia = new FormInitAssets(true);
                fia.ShowDialog();
            }
            else
            {
                MessageBoxExtend.messageWarning("财务系统已启用，无法再录入期初数据");
            }
        }

        // 财务系统初始化
        private void panelAccountsInit_Click(object sender, EventArgs e)
        {
            if (!InitSubSystemSign.getInctance().isFinancialSystemInit())
            {
                if (MessageBoxExtend.messageQuestion("确定所有的财务数据均已录入完毕并正式启用财务系统吗?"))
                {
                    try
                    {
                        InitSubSystemSign.getInctance().initFinancialSystem();

                        // 把财务相关初始数据写到企业利润表
                        CompanyProfit.getInctance().initAssetsSystemBalanceInfo();

                        MessageBoxExtend.messageOK("财务系统初始化成功.\n重新启动应用程序，财务相关子系统便可使用.");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("财务系统已启用，不要再次启用");
            }
        }

        #region 鼠标滑过事件
        private void panelMaterielInit_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMaterielData);
        }

        private void panelBusinessAccountsData_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelBusinessAccountsData);
        }

        private void panelBusinessInit_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelBusinessInit);
        }

        private void panelBankData_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelBankData);
        }

        private void panelMoneyData_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMoneyData);
        }

        private void panelAccountsInit_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelAccountsInit);
        }

        private void panelAccountReceivable_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelAccountReceivable);
        }
        #endregion
    }
}