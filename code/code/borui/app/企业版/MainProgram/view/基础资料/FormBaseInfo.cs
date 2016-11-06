using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; 
using TIV.Core.DatabaseAccess;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public partial class FormBaseInfo : Form
    {
        private SortedDictionary<string, Label> m_allPanelLableList = new SortedDictionary<string, Label>();

        public FormBaseInfo()
        {
            InitializeComponent();
        }

        private void FormBaseInfo_Load(object sender, EventArgs e)
        {
            // 本页面所有panel和对应的lable，做相应的关联
            m_allPanelLableList.Add("panelSystemInfo", this.labelSystemInfo);
            m_allPanelLableList.Add("panelUserManager", this.labelUserManager);
            m_allPanelLableList.Add("panelAuxiliaryMaterial", this.labelAuxiliaryMaterial);
            m_allPanelLableList.Add("panelSupplier", this.labelSupplier);
            m_allPanelLableList.Add("panelCustomer", this.labelCustomer);
            m_allPanelLableList.Add("panelMateriel", this.labelMateriel);
            m_allPanelLableList.Add("panelBillConfig", this.labelBillConfig);
            m_allPanelLableList.Add("panelSettlmentWay", this.labelSettlmentWay);
            m_allPanelLableList.Add("panelDBBackConfig", this.labelDBBackConfig);
            m_allPanelLableList.Add("panelDBback", this.labelDBback);
            m_allPanelLableList.Add("panelDBRoll", this.labelDBRoll);
            m_allPanelLableList.Add("panelSystemLogin", this.labelSystemLogin);

            setPageActionEnable();
        }

        private void panelSystemInfo_Click(object sender, EventArgs e)
        {
            FormBaseSystemInfo fbsi = new FormBaseSystemInfo();
            fbsi.ShowDialog();
        }

        private void panelUserManager_Click(object sender, EventArgs e)
        {
            FormBaseUser fbu = new FormBaseUser();
            fbu.ShowDialog();
        }

        private void panelAuxiliaryMaterial_Click(object sender, EventArgs e)
        {
            FormBaseAuxiliaryMaterial fbam = new FormBaseAuxiliaryMaterial();
            fbam.ShowDialog();
        }

        private void panelSupplier_Click(object sender, EventArgs e)
        {
            FormBaseSupplier fbs = new FormBaseSupplier();
            fbs.ShowDialog();
        }

        private void panelCustomer_Click(object sender, EventArgs e)
        {
            FormBaseCustomer fbc = new FormBaseCustomer();
            fbc.ShowDialog();
        }

        private void panelMateriel_Click(object sender, EventArgs e)
        {
            FormBaseMateriel fbm = new FormBaseMateriel();
            fbm.ShowDialog();
        }

        private void panelBillConfig_Click(object sender, EventArgs e)
        {
            FormBaseBillConfig fbbc = new FormBaseBillConfig();
            fbbc.ShowDialog();
        }

        private void panelSettlmentWay_Click(object sender, EventArgs e)
        {
            FormBaseSettlmentWay fbsw = new FormBaseSettlmentWay();
            fbsw.ShowDialog();
        }

        private void panelDBBackConfig_Click(object sender, EventArgs e)
        {
            string parameter = "0";

            if (DbBackConfig.getInctance().getRecordCount() > 0)
            {
                parameter = "1";
            }

            // 启动
            Process.Start(@"DBBackupUserInterface.exe", parameter);
        }

        private void panelDBback_Click(object sender, EventArgs e)
        {
            FormBaseDbBackupRecord fbdbr = new FormBaseDbBackupRecord();
            fbdbr.ShowDialog();
        }

        private void panelDBRoll_Click(object sender, EventArgs e)
        {
            FormBaseDbRollbackRecord fbdrr = new FormBaseDbRollbackRecord();
            fbdrr.ShowDialog();
        }

        private void panelSystemLogin_Click(object sender, EventArgs e)
        {
            FormBaseOperatorLog fbol = new FormBaseOperatorLog();
            fbol.ShowDialog();
        }

        private void setPageActionEnable()
        {
            this.panelDBBackConfig.Enabled = AccessAuthorization.getInctance().isAccessAuthorization(111,
                    Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));
        }
        #region Panel鼠标滑过事件
        private void panelSupplier_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSupplier);
        }

        private void panelSystemInfo_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSystemInfo);
        }

        private void panelUserManager_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelUserManager);
        }

        private void panelAuxiliaryMaterial_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelAuxiliaryMaterial);
        }

        private void panelCustomer_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCustomer);
        }

        private void panelMateriel_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMateriel);
        }

        private void panelBillConfig_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelBillConfig);
        }

        private void panelSettlmentWay_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSettlmentWay);
        }

        private void panelDBBackConfig_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelDBBackConfig);
        }

        private void panelDBback_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelDBback);
        }

        private void panelDBRoll_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelDBRoll);
        }

        private void panelSystemLogin_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSystemLogin);
        }
        #endregion
    }
}
