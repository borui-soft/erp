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
    public partial class FormInitAccountReceivabler : Form
    {
        private int m_dataGridRecordCount = 0;
        private DataGridViewExtend m_dateGridViewExtend = new DataGridViewExtend();
        private int m_currentDataGridViedRecordPkey = 0;
        private string m_currentDataGridViedRecordName = "";
        private bool m_isAccountReceivable;
        private bool m_isAddRecordMode;
        private int m_customerOrSupplierID;

        public FormInitAccountReceivabler(bool isAccountReceivable = true, bool isAddRecordMode = false)
        {
            InitializeComponent();
            m_isAccountReceivable = isAccountReceivable;
            m_isAddRecordMode = isAddRecordMode;

            if (m_isAccountReceivable)
            {
                this.Text = "应收账款期初数据";
            }
            else
            {
                this.Text = "应付账款期初数据";
            }
        }

        private void FormInitAccountReceivabler_Load(object sender, EventArgs e)
        {
            m_dateGridViewExtend.addDataGridViewColumn("ID", 30);
            m_dateGridViewExtend.addDataGridViewColumn("客户ID", 250, false);
            m_dateGridViewExtend.addDataGridViewColumn("客户名称", 250);
            m_dateGridViewExtend.addDataGridViewColumn("欠款金额", 150);
            m_dateGridViewExtend.addDataGridViewColumn("交易日期", 150);
            m_dateGridViewExtend.addDataGridViewColumn("信用额度", 150);

            m_dateGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);
            updateDataGridView();

            setPageActionEnable();
        }

        private void updateDataGridView()
        {
            SortedDictionary<int, InitAccountReceivableTable> dateList = new SortedDictionary<int, InitAccountReceivableTable>();

            if (m_isAccountReceivable)
            {
                dateList = InitAccountReceivable.getInctance().getAllInitAccountReceivableInfo();
            }
            else
            {
                dateList = InitAccountPayable.getInctance().getAllInitAccountPayableInfo();
            }

            m_dataGridRecordCount = dateList.Count;

            SortedDictionary<int, ArrayList> settlmentWayLArrary = new SortedDictionary<int, ArrayList>();

            double balanceSum = 0.0, creditSum = 0.0;
            int recordCount = 0;
            for (recordCount = 0; recordCount < dateList.Count; recordCount++)
            {
                InitAccountReceivableTable record = new InitAccountReceivableTable();
                record = (InitAccountReceivableTable)dateList[recordCount];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.customerOrSupplierID);
                temp.Add(record.name);
                temp.Add(record.balance);
                temp.Add(record.tradingDate);
                temp.Add(record.credit);

                settlmentWayLArrary.Add(recordCount, temp);

                // 应收或应付账款金额合计
                balanceSum += record.balance;
                creditSum += record.credit;
            }

            m_dateGridViewExtend.initDataGridViewData(settlmentWayLArrary);
        }

        private void add_Click(object sender, EventArgs e)
        {
            userInterfaceValue uiValue = new userInterfaceValue();
            uiValue.winText = this.Text + "新增";
            uiValue.isAccountReceivable = m_isAccountReceivable;
            uiValue.isEditDate = false;

            FormAccountReceivablerEdit fare = new FormAccountReceivablerEdit(uiValue);
            fare.ShowDialog();

            updateDataGridView();
        }

        private void modify_Click(object sender, EventArgs e)
        {
            userInterfaceValue uiValue = new userInterfaceValue();
            uiValue.winText = this.Text + "编辑";
            uiValue.isAccountReceivable = m_isAccountReceivable;
            uiValue.isEditDate = true;
            uiValue.textBoxText = m_currentDataGridViedRecordName;
            uiValue.pkey = m_currentDataGridViedRecordPkey;

            InitAccountReceivableTable data = new InitAccountReceivableTable();
            if(m_isAccountReceivable)
            {
                data = InitAccountReceivable.getInctance().getAccountReceivableInfoFromPkey(m_currentDataGridViedRecordPkey);
            }
            else
            {
                data = InitAccountPayable.getInctance().getAccountPayableInfoFromPkey(m_currentDataGridViedRecordPkey);
            }

            uiValue.textBoxBalance = Convert.ToString(data.balance);
            uiValue.dateTimePickerDateText = data.tradingDate;
            uiValue.customerOrSupplierID = data.customerOrSupplierID;

            FormAccountReceivablerEdit fare = new FormAccountReceivablerEdit(uiValue);
            fare.ShowDialog();

            updateDataGridView();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除[" + m_currentDataGridViedRecordName + "]欠款信息吗?"))
                {
                    if (m_isAccountReceivable)
                    {
                        InitAccountReceivable.getInctance().delete(m_currentDataGridViedRecordPkey);
                    }
                    else
                    {
                        InitAccountPayable.getInctance().delete(m_currentDataGridViedRecordPkey);
                    }

                    updateDataGridView();
                }
            }
        }

        private void import_Click(object sender, EventArgs e)
        {
            string msg;
            
            if(m_isAccountReceivable)
            {
                msg = "批量导入数据会清空数据库所有已存在应收账款记录，是否继续？";
            }
            else
            {
                msg = "批量导入数据会清空数据库所有已存在应付账款记录，是否继续？";
            }

            if (MessageBoxExtend.messageQuestion(msg))
            {
                // 指定OpenFileDialog控件打开的文件格式
                openFileDialog1.Filter = "导入文件(*.xls)|*.xls";
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    if (m_isAccountReceivable)
                    {
                        InitAccountReceivable.getInctance().fileImport(openFileDialog1.FileName);
                    }
                    else
                    {
                        InitAccountPayable.getInctance().fileImport(openFileDialog1.FileName);
                    }
                    updateDataGridView();
                }
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

        private void dataGridViewBilConfigList_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dataGridRecordCount > 0)
                {
                    // 当单击某个单元格时，自动选择整行
                    for (int i = 0; i < this.dataGridViewMaterielList.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridViewMaterielList.ColumnCount; j++)
                        {
                            if (dataGridViewMaterielList.Rows[i].Cells[j].Selected)
                            {
                                dataGridViewMaterielList.Rows[i].Selected = true;
                                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[0].Value.ToString());
                                m_customerOrSupplierID = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[1].Value.ToString());
                                m_currentDataGridViedRecordName = dataGridViewMaterielList.Rows[i].Cells[2].Value.ToString();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
 
            }
        }

        private void dataGridViewMaterielList_DoubleClick(object sender, EventArgs e)
        {
            
            try
            {
                if (m_dataGridRecordCount > 0)
                {
                    // 当单击某个单元格时，自动选择整行
                    for (int i = 0; i < this.dataGridViewMaterielList.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridViewMaterielList.ColumnCount; j++)
                        {
                            if (dataGridViewMaterielList.Rows[i].Cells[j].Selected)
                            {
                                dataGridViewMaterielList.Rows[i].Selected = true;

                                m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[0].Value.ToString());
                                m_customerOrSupplierID = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[1].Value.ToString());
                                m_currentDataGridViedRecordName = dataGridViewMaterielList.Rows[i].Cells[2].Value.ToString();

                                if (m_isAddRecordMode)
                                {
                                    this.Close();
                                }

                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
 
            }
            
        }

        public int getCustomerOrSupplierID()
        {
            return m_customerOrSupplierID;
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(602);

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

            SortedDictionary<int, ActionTable> list2 = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(603);

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
