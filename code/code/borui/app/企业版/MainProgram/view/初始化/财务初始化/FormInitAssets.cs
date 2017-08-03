using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormInitAssets : Form
    {
        private int m_dataGridRecordCount = 0;
        private bool m_isInitBankAssets = false;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();

        public FormInitAssets(bool isInitBankAssets)
        {
            InitializeComponent();
            m_isInitBankAssets = isInitBankAssets;
        }

        private void FormInitAssets_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("科目名称", 300);
            m_dateGridViewExtend.addDataGridViewColumn("余额", 170);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewAssetsList);

            if (m_isInitBankAssets)
            {
                this.Text = "银行存款期数余额初始化";
                updateDataGridViewBank(InitBankBalance.getInctance().getAllInitBankBalanceInfo());
            }
            else
            {
                this.Text = "库存现金期数余额初始化";
                updateDataGridViewCash(InitCashBalance.getInctance().getAllInitCashBalanceInfo());
            }

            setPageActionEnable();
        }

        private void updateDataGridViewBank(SortedDictionary<int, InitBankBalanceTable> bankList)
        {
            m_dataGridRecordCount = bankList.Count;

            SortedDictionary<int, ArrayList> bankRecordArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < bankList.Count; i++)
            {
                InitBankBalanceTable record = new InitBankBalanceTable();
                record = (InitBankBalanceTable)bankList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.bankName);
                temp.Add(record.accountBalanec);

                bankRecordArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(bankRecordArrary);
        }

        private void updateDataGridViewCash(SortedDictionary<int, InitCashBalanceTable> bankList)
        {
            m_dataGridRecordCount = bankList.Count;

            SortedDictionary<int, ArrayList> bankRecordArrary = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < bankList.Count; i++)
            {
                InitCashBalanceTable record = new InitCashBalanceTable();
                record = (InitCashBalanceTable)bankList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.objectName);
                temp.Add(record.accountBalanec);

                bankRecordArrary.Add(i, temp);
            }

            m_dateGridViewExtend.initDataGridViewData(bankRecordArrary);
        }

        private void save_Click(object sender, EventArgs e)
        {
            this.ActiveControl = this.toolStrip1;

            if (this.save.Text == "修改")
            {
                this.save.Text = "保存";
                this.dataGridViewAssetsList.ReadOnly = false;
            }
            else 
            {
                this.save.Text = "修改";
                saveAssetsData();
                this.dataGridViewAssetsList.ReadOnly = true;
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_dataGridRecordCount > 0)
            {
                this.saveFileDialog1.Filter = "Excel 2007格式 (*.xlsx)|*.xlsx|Excel 2003格式 (*.xls)|*.xls";
                this.saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_dateGridViewExtend.dataGridViewExportToExecl(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据为空，无数据可导出!");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            m_dateGridViewExtend.printDataGridView();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAssetsData()
        {
            for (int i = 0; i < m_dataGridRecordCount; i++)
            {
                try
                {
                    int pkey = Convert.ToInt32(this.dataGridViewAssetsList.Rows[i].Cells[0].Value.ToString());
                    double balance = Convert.ToDouble(this.dataGridViewAssetsList.Rows[i].Cells[2].EditedFormattedValue.ToString());

                    if (m_isInitBankAssets)
                    {
                        InitBankBalance.getInctance().update(pkey, balance);
                    }
                    else
                    {
                        InitCashBalance.getInctance().update(pkey, balance);
                    }
                }
                catch (Exception)
                {
                    MessageBoxExtend.messageError("期初余额应该为数字形式，请仔细核对各账户余额格式，然后重试");
                }
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(604);

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

            SortedDictionary<int, ActionTable> list2 = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(605);

            foreach (KeyValuePair<int, ActionTable> index in list2)
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
    }
}
