using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormInitMaterielEdit : Form
    {
        private bool m_isAddToInitMaterielList = false;
        private bool m_isEditMaterielData = false;
        private int m_materielPkey = 0;
        private bool m_isCheckSucceful = false;
        private int m_storageStockTablePkey = 0;
        private string m_materielName = "";

        public FormInitMaterielEdit(string winText, bool isEditMaterielData = true, int pkey = 0)
        {
            InitializeComponent();
            m_isEditMaterielData = isEditMaterielData;
            this.Text = winText;
            m_storageStockTablePkey = pkey;
        }

        private void FormInitMaterielEdit_Load(object sender, EventArgs e)
        {
            if (m_isEditMaterielData)
            {
                this.buttonEdit.Enabled = true;
                this.buttonSelect.Enabled = false;
                this.textBoxPrice.Enabled = false;
                this.textBoxValue.Enabled = false;
                this.buttonEnter.Enabled = false;

                MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(m_storageStockTablePkey);
                this.textBoxName.Text = Convert.ToString(materiel.pkey) + "-" + materiel.name;
                m_materielName = materiel.name;
                m_materielPkey = m_storageStockTablePkey;

                if (InitMateriel.getInctance().checkMaterielIsExist(m_storageStockTablePkey))
                {
                    m_isCheckSucceful = true;

                    InitMaterielTable materielStock = InitMateriel.getInctance().getMaterielInfoFromPkey(m_storageStockTablePkey);
                    m_materielPkey = materielStock.materielID;

                    this.textBoxValue.Text = Convert.ToString(materielStock.value);
                    this.textBoxPrice.Text = Convert.ToString(materielStock.price);
                }
                else 
                {
                    m_isCheckSucceful = false;

                    this.textBoxValue.Text = "0";
                    this.textBoxPrice.Text = "0";
                }
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FormBaseMateriel fbs = new FormBaseMateriel(true);
            fbs.ShowDialog();

            m_materielPkey = fbs.getSelectRecordPkey();
            MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(m_materielPkey);
            this.textBoxName.Text = Convert.ToString(materiel.pkey) + "-" + materiel.name;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxPrice.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("单价不能为空");
                return;
            }

            if (textBoxPrice.Text.Length > 20)
            {
                MessageBoxExtend.messageWarning("单价最大长度不能超过10");
                textBoxPrice.Text = "";
                return;
            }

            if (textBoxValue.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("数量不能为空");
                return;
            }

            if (m_isEditMaterielData && InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                if (MessageBoxExtend.messageQuestion("请确认是否要对[" + m_materielName + "]的成本进行调整，这将会影响到此物料实时库存的加权单价?"))
                {
                    // 第一步 插入插入到存货明细表（STORAGE_STOCK_DETAIL），已解决实际库存和历史库存信息可能不对应的问题
                    StorageStockDetailTable storageStockDetailRecord = new StorageStockDetailTable();
                    storageStockDetailRecord.materielID = m_materielPkey;
                    storageStockDetailRecord.tradingDate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                    storageStockDetailRecord.billNumber = BillNumber.getInctance().getNewBillNumber(20, DateTime.Now.ToString("yyyy-MM-dd"));
                    storageStockDetailRecord.thingsType = "期初成本调整";
                    storageStockDetailRecord.isIn = 3;
                    storageStockDetailRecord.value = 0;
                    storageStockDetailRecord.price = 0;

                    // 交易完毕后数量和单价
                    InitMaterielTable materielStorageData = InitMateriel.getInctance().getMaterielInfoFromMaterielID(m_materielPkey);
                    storageStockDetailRecord.storageValue = materielStorageData.value;
                    storageStockDetailRecord.storagePrice = Convert.ToDouble(this.textBoxPrice.Text.ToString());
                    StorageStockDetail.getInctance().insert(storageStockDetailRecord);


                    // 第二步 更新实时库存表（INIT_STORAGE_STOCK）
                    InitMaterielTable record = new InitMaterielTable();
                    record.materielID = m_materielPkey;
                    record.price = Convert.ToDouble(this.textBoxPrice.Text.ToString());
                    record.value = Convert.ToDouble(this.textBoxValue.Text.ToString());

                    if (!m_isEditMaterielData)
                    {
                        InitMateriel.getInctance().insert(record);
                    }
                    else
                    {
                        if (m_isCheckSucceful)
                        {
                            InitMateriel.getInctance().update(m_storageStockTablePkey, record);
                        }
                        else 
                        {
                            InitMateriel.getInctance().insert(record);
                        }
                    }

                    m_isAddToInitMaterielList = true;
                    this.Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            m_isAddToInitMaterielList = false;
            this.Close();
        }

        public int getSelectRecordPkey()
        {
            return m_materielPkey;
        }

        public bool isAddToInitMaterielList()
        {
            return m_isAddToInitMaterielList;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.buttonEdit.Enabled = false;

            // 2016-11-5 当业务系统启动后，当对物料库存信息进行编辑时，只能修改物料单价，不能修改物料数量
            if (!InitSubSystemSign.getInctance().isBusinessSystemInit())
            {
                this.textBoxPrice.Enabled = true;
            }
            else
            {
                this.textBoxValue.Enabled = false;
            }

            this.textBoxPrice.Enabled = true;
            this.buttonEnter.Enabled = true;
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 单价只能只能输入数字或小数点或退格键
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '.' && this.textBoxPrice.Text.IndexOf(".") == -1)
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 物料数量只能输入数字或退格键
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }
    }
}
