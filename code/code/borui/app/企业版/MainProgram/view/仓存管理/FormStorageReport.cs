using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormStorageReport : Form
    {
        public FormStorageReport()
        {
            InitializeComponent();
        }

        #region 鼠标滑过按钮事件
        private void labelMateriel_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMateriel);
        }

        private void labelOrderIn_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelOrderIn);
        }

        private void labelOrderOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelOrderOut);
        }

        private void labelOrderAllocate_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelOrderMaterielProOccupied);
        }

        private void labelInventory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventory);
        }

        private void labelOrderInventory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelOrderInventory);
        }

        private void labelMaterielCount_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMaterielCount);
        }

        private void labelMaterielDetails_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelMaterielDetails);
        }

        private void labelInventoryAlarm_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAlarm);
        }

        private void labelInventoryAnalysis_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAnalysis);
        }

        private void labelInventoryAge_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryAge);
        }

        private void labelInventoryLife_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryLife);
        }

        private void labelProductionOut_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelProductionOut);
        }

        private void labelInventoryPassAge_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryPassAge);
        }
        #endregion
        private void labelMateriel_Click(object sender, EventArgs e)
        {
            FormBaseMateriel fbm = new FormBaseMateriel();
            fbm.ShowDialog();
        }

        private void labelOrderIn_Click(object sender, EventArgs e)
        {
            FormStorageSequenceFilter fssf = new FormStorageSequenceFilter(false);
            if (fssf.ShowDialog() == DialogResult.OK)
            {
                //得到界面用户选定的值
                FormStorageSequenceFilterValue filter = fssf.getFilterUIValue();
                FormPurchaseOrderSequence.OrderType type = new FormPurchaseOrderSequence.OrderType();

                if (filter.sequenceType == "0")
                {
                    type = FormPurchaseOrderSequence.OrderType.PurchaseIn;
                }
                else if (filter.sequenceType == "1")
                {
                    type = FormPurchaseOrderSequence.OrderType.StorageProductIn;
                }
                else if (filter.sequenceType == "2")
                {
                    type = FormPurchaseOrderSequence.OrderType.StorageInCheck;
                }
                else if (filter.sequenceType == "3")
                {
                    type = FormPurchaseOrderSequence.OrderType.StorageInOther;
                }

                FormPurchaseOrderSequence fphpc = new FormPurchaseOrderSequence(type);
                fphpc.setDataFilter(filter);
                fphpc.ShowDialog();
            }
        }

        private void labelOrderOut_Click(object sender, EventArgs e)
        {
            FormStorageSequenceFilter fssf = new FormStorageSequenceFilter(true);
            if (fssf.ShowDialog() == DialogResult.OK)
            {
                //得到界面用户选定的值
                FormStorageSequenceFilterValue filter = fssf.getFilterUIValue();
                FormSaleOrderSequence.OrderType type = new FormSaleOrderSequence.OrderType();

                if (filter.sequenceType == "0")
                {
                    type = FormSaleOrderSequence.OrderType.SaleOut;
                }
                else if (filter.sequenceType == "1")
                {
                    type = FormSaleOrderSequence.OrderType.StorageMaterielOut;
                }
                else if (filter.sequenceType == "2")
                {
                    type = FormSaleOrderSequence.OrderType.StorageOutCheck;
                }
                else if (filter.sequenceType == "3")
                {
                    type = FormSaleOrderSequence.OrderType.StorageOutOther;
                }

                FormSaleOrderSequence fphpc = new FormSaleOrderSequence(type);
                fphpc.setDataFilter(filter);
                fphpc.ShowDialog();
            }
        }

        private void labelOrderAllocate_Click(object sender, EventArgs e)
        {
            FormMaterielProOccupiedList fmpol = new FormMaterielProOccupiedList(FormMaterielProOccupiedList.OrderType.MaterielProOccupied);
            fmpol.ShowDialog();
        }

        private void labelInventory_Click(object sender, EventArgs e)
        {
            FormMaterielStorageAmountInfo fmc = new FormMaterielStorageAmountInfo((int)FormMaterielStorageAmountInfo.DisplayDataType.All, false);
            fmc.ShowDialog();
        }

        // 物料收发明细
        private void labelOrderInventory_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(
                FormDisplayCountInfoFromSQL.CountType.StorageManagerMaterielCount);
            fpic.ShowDialog();
        }

        // 产品入库汇总表
        private void labelMaterielCount_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(
                FormDisplayCountInfoFromSQL.CountType.StorageManagerProduceIn);
            fpic.ShowDialog();
        }

        // 生产领料汇总表
        private void labelMaterielDetails_Click(object sender, EventArgs e)
        {
            FormDisplayCountInfoFromSQL fpic = new FormDisplayCountInfoFromSQL(FormDisplayCountInfoFromSQL.CountType.StorageMaterielOut);
            fpic.ShowDialog();
        }

        // 安全库存预警分析
        private void labelInventoryAlarm_Click(object sender, EventArgs e)
        {
            FormMaterielCountAlarm fmca = new FormMaterielCountAlarm(this.labelInventoryAlarm.Text);
            fmca.ShowDialog();
        }

        // 超储短缺库存分析
        private void labelInventoryAnalysis_Click(object sender, EventArgs e)
        {
            FormMaterielCountAlarm fmca = new FormMaterielCountAlarm(this.labelInventoryAnalysis.Text, true);
            fmca.ShowDialog();
        }

        // 库存账龄分析
        private void labelInventoryAge_Click(object sender, EventArgs e)
        {
            FormMaterielCountAlarm fmca = new FormMaterielCountAlarm(this.labelInventoryAge.Text, false, true);
            fmca.ShowDialog();
        }

        // 库存呆滞料分析
        private void labelInventoryPassAge_Click(object sender, EventArgs e)
        {

        }

        // 保质期预警
        private void labelInventoryLife_Click(object sender, EventArgs e)
        {

        }

        // 超出保质期物料分析
        private void labelProductionOut_Click(object sender, EventArgs e)
        {

        }

        private void labelInventoryHistory_Click(object sender, EventArgs e)
        {
            FormMaterielStorageAmountHistoryInfo fmc = new FormMaterielStorageAmountHistoryInfo((int)FormMaterielStorageAmountInfo.DisplayDataType.All, false);
            fmc.ShowDialog();
        }

        private void labelInventoryHistory_MouseEnter(object sender, EventArgs e)
        {
            PanelExtend.setLableControlStyle(this.labelInventoryHistory);
        }
    }
}
