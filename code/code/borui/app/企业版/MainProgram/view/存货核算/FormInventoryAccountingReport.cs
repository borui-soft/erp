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
    public partial class FormInventoryAccountingReport : Form
    {
        FormStorageSequenceFilterValue m_filter = new FormStorageSequenceFilterValue();
        public FormInventoryAccountingReport()
        {
            InitializeComponent();
        }

        private void FormInventoryAccountingReport_Load(object sender, EventArgs e)
        {
            DateTime nowDate = DateTime.Now;
            DateTime currentMonthFirstDay = new DateTime(nowDate.Year, nowDate.Month, 1);
            DateTime currentMonthLastDay = currentMonthFirstDay.AddMonths(1).AddDays(-1);

            // 显示当前会计期间，已审核的所有单据
            m_filter.startDate = currentMonthFirstDay.ToString("yyyy-MM-dd");
            m_filter.endDate = currentMonthLastDay.ToString("yyyy-MM-dd");
            m_filter.allReview = "0";
            m_filter.billColor = "2";

            setPageActionEnable();
        }

        // 期初成本调价
        private void labelCost_Click(object sender, EventArgs e)
        {
            FormInitMateriel fim = new FormInitMateriel();
            fim.ShowDialog();
        }
        
        // 存货明细
        private void labelInventoryDetails_Click(object sender, EventArgs e)
        {
            FormStorageStockDetails fssd = new FormStorageStockDetails();
            fssd.ShowDialog();
        }

        // 采购成本汇总
        private void labelPurchaseCostCount_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.PurchaseMateriel);
            fpic.ShowDialog();
        }

        // 采购成本明细表
        private void labelPurchaseCostDetails_Click(object sender, EventArgs e)
        {
            FormPurchaseOrderSequence fpos = new FormPurchaseOrderSequence(FormPurchaseOrderSequence.OrderType.PurchaseIn);
            fpos.ShowDialog();
        }

        // 生产领料汇总
        private void labelProductionOutCount_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.StorageMaterielOut);
            fpic.ShowDialog();
        }

        // 生产领料明细
        private void labelProductionOutDetails_Click(object sender, EventArgs e)
        {
            FormSaleOrderSequence fphpc = new FormSaleOrderSequence(
                FormSaleOrderSequence.OrderType.StorageMaterielOut);
            fphpc.setDataFilter(m_filter);

            fphpc.ShowDialog();
        }

        // 销售成本汇总
        private void labelSaleCostCount_Click(object sender, EventArgs e)
        {
            FormSaleInfoCount fsic = new FormSaleInfoCount(FormSaleInfoCount.OrderType.SaleCostCount);
            fsic.ShowDialog();
        }

        // 销售成本明细
        private void labelSaleCostDetails_Click(object sender, EventArgs e)
        {
            FormSaleInfoCount fsic = new FormSaleInfoCount(FormSaleInfoCount.OrderType.SaleCostDetails);
            fsic.ShowDialog();
        }

        // 销售毛利润汇总
        private void labelSaleProfitCount_Click(object sender, EventArgs e)
        {
            FormSaleInfoCount fsic = new FormSaleInfoCount(FormSaleInfoCount.OrderType.SaleProfitCount);
            fsic.ShowDialog();
        }

        // 销售毛利润明细
        private void labelSaleProfitDetails_Click(object sender, EventArgs e)
        {
            FormSaleInfoCount fsic = new FormSaleInfoCount(FormSaleInfoCount.OrderType.SaleProfitDetails);
            fsic.ShowDialog();
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(402);

            foreach (KeyValuePair<int, ActionTable> index in list)
            {
                object activeObject = this.GetType().GetField(index.Value.uiActionName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                bool isEnable = AccessAuthorization.getInctance().isAccessAuthorization(index.Value.pkey,
                    Convert.ToString(DbPublic.getInctance().getCurrentLoginUserID()));

                if (activeObject != null)
                {
                    UserInterfaceActonState.setUserInterfaceActonState(activeObject,
                        ((System.Reflection.MemberInfo)(activeObject.GetType())).Name.ToString(), isEnable);
                }
            }
        }
        #region 鼠标滑过事件
        private void labelCost_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelCost);
        }

        private void labelInventoryDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryDetails);
        }

        private void labelPurchaseCostCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseCostCount);
        }

        private void labelPurchaseCostDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelPurchaseCostDetails);
        }

        private void labelInventoryAppraisalCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelProductionOutCount);
        }

        private void labelInventoryAppraisalDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelProductionOutDetails);
        }

        private void labelProductionOutCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleCostCount);
        }

        private void labelProductionOutDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleCostDetails);
        }

        private void labelSaleCostCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleProfitCount);
        }

        private void labelSaleCostDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelSaleProfitDetails);
        }
        #endregion

        private void labelMaterielInOutCount_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(
                FormDisplayCountInfoFromSQL.CountType.MaterielInOutCount);
            fpic.ShowDialog();
        }

        private void labelMaterielInOutCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMaterielInOutCount);
        }
    }
}
