using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using TIV.Core.DatabaseAccess;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;

    public partial class FormMaterielPriceSheet : Form
    {
        private int m_materielID = 0;
        private int m_materielRecordCount = 0;
        private int m_currentDataGridViedRecordPkey = 0;
        private bool m_isQuerySupplierPrice;

        private DataGridViewExtend m_dataGridViewExtend = new DataGridViewExtend();

        public FormMaterielPriceSheet(int materielID, bool isQuerySupplierPrice = true)
        {
            InitializeComponent();
            m_materielID = materielID;
            m_isQuerySupplierPrice = isQuerySupplierPrice;
            MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(materielID);

            if (m_isQuerySupplierPrice)
            {
                this.Text = "物料 [" + record.name + "] 供应商报价信息" ;
            }
            else 
            {
                this.Text = "商品 [" + record.name + "] 销售指导价格";
            }
        }

        private void FormSupplierPriceSheet_Load(object sender, EventArgs e)
        {
            // DataGridView控件初始化
            m_dataGridViewExtend.addDataGridViewColumn("ID", 30, false);
            m_dataGridViewExtend.addDataGridViewColumn("供应商ID", 60, false);
            m_dataGridViewExtend.addDataGridViewColumn("供应商名称", 200, m_isQuerySupplierPrice);
            m_dataGridViewExtend.addDataGridViewColumn("订货数量从", 100);
            m_dataGridViewExtend.addDataGridViewColumn("订货数量到", 100);
            m_dataGridViewExtend.addDataGridViewColumn("报价日期", 80);
            m_dataGridViewExtend.addDataGridViewColumn("物料单价", 80);
            m_dataGridViewExtend.addDataGridViewColumn("联系人", 80, m_isQuerySupplierPrice);
            m_dataGridViewExtend.addDataGridViewColumn("联系电话", 80, m_isQuerySupplierPrice);
            m_dataGridViewExtend.addDataGridViewColumn("备注", 100);

            m_dataGridViewExtend.initDataGridViewColumn(this.dataGridViewMaterielList);
            updateDataGridView();
            setPageActionEnable();
        }

        private void updateDataGridView()
        {
            SortedDictionary<int, SupplierPriceSheetTable> dataList =
                SupplierPriceSheet.getInctance().getAllSupplierPriceSheetInfo(m_materielID);

            m_materielRecordCount = dataList.Count;

            SortedDictionary<int, ArrayList> materiels = new SortedDictionary<int, ArrayList>();

            for (int i = 0; i < dataList.Count; i++)
            {
                SupplierPriceSheetTable record = new SupplierPriceSheetTable();
                record = (SupplierPriceSheetTable)dataList[i];

                ArrayList temp = new ArrayList();

                temp.Add(record.pkey);
                temp.Add(record.supplierId);
                temp.Add(record.supplierName);
                temp.Add(record.ORNMFromValue);
                temp.Add(record.ORNMToValue);
                temp.Add(record.date);
                temp.Add(record.pirce);
                temp.Add(record.contact);
                temp.Add(record.tel);
                temp.Add(record.note);

                if ((m_isQuerySupplierPrice && record.supplierId != -1)||
                    (!m_isQuerySupplierPrice && record.supplierId == -1))
                {
                    materiels.Add(i, temp);
                }
            }

            m_dataGridViewExtend.initDataGridViewData(materiels, 3);
        }

        private void add_Click(object sender, EventArgs e)
        {
            FormMaterielPriceSheetEdit fspse = new FormMaterielPriceSheetEdit(m_materielID, false, m_currentDataGridViedRecordPkey, m_isQuerySupplierPrice);
            fspse.ShowDialog();
            updateDataGridView();
        }

        private void modify_Click(object sender, EventArgs e)
        {
            FormMaterielPriceSheetEdit fspse = new FormMaterielPriceSheetEdit(m_materielID, true, m_currentDataGridViedRecordPkey, m_isQuerySupplierPrice);
            fspse.ShowDialog();
            updateDataGridView();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (m_currentDataGridViedRecordPkey != 0)
            {
                if (MessageBoxExtend.messageQuestion("确定删除此报价信息吗?"))
                {
                    SupplierPriceSheet.getInctance().delete(m_currentDataGridViedRecordPkey);
                    updateDataGridView();
                }
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            // 此处需要添加导入DataGridViewer数据到Excel的功能
            if (m_materielRecordCount > 0)
            {
                this.saveFileDialog1.Filter = "Excel 2007格式 (*.xlsx)|*.xlsx|Excel 2003格式 (*.xls)|*.xls";
                this.saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_dataGridViewExtend.dataGridViewExportToExecl(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBoxExtend.messageWarning("数据为空，无数据可导出!");
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            if(m_materielRecordCount > 0)
            {
                m_dataGridViewExtend.printDataGridView();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewMaterielList_Click(object sender, EventArgs e)
        {
            if (m_materielRecordCount > 0)
            {
                // 当用户单击某个单元格时，自动选择整行
                for (int i = 0; i < this.dataGridViewMaterielList.RowCount; i++)
                {
                    for (int j = 0; j < dataGridViewMaterielList.ColumnCount; j++)
                    {
                        if (dataGridViewMaterielList.Rows[i].Cells[j].Selected)
                        {
                            dataGridViewMaterielList.Rows[i].Selected = true;
                            m_currentDataGridViedRecordPkey = Convert.ToInt32(dataGridViewMaterielList.Rows[i].Cells[0].Value.ToString());
                            return;
                        }
                    }
                }
            }
        }

        private void setPageActionEnable()
        {
            SortedDictionary<int, ActionTable> list = MainProgram.model.Action.getInctance().getActionInfoFromModuleID(104);

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
    }
}