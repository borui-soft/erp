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
    public partial class FormStorageStockDetailsFilter : Form
    {
        private int m_materielPkey = -1;
        private string m_startDate;
        private string m_endDate;
        private string m_allReview;

        public FormStorageStockDetailsFilter()
        {
            InitializeComponent();
        }

        private void FormPurchaseInfoCountFilter_Load(object sender, EventArgs e)
        {
            DateTime nowDate = DateTime.Now;
            DateTime currentMonthFirstDay = new DateTime(nowDate.Year, nowDate.Month, 1);
            DateTime currentMonthLastDay = currentMonthFirstDay.AddMonths(1).AddDays(-1);

            this.dateTimePickerStartDate.Value = currentMonthFirstDay;
            this.dateTimePickerEndDate.Value = currentMonthLastDay;

            // 初始审核标志下拉列表框
            this.comboBoxReview.Items.Add("已审核单据");
            this.comboBoxReview.Items.Add("全部单据");
            this.comboBoxReview.SelectedIndex = 1;

            radioButtonAllMateriel.Checked = false;
            radioButtonMateriel.Checked = true;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_startDate = this.dateTimePickerStartDate.Value.ToString("yyyyMMdd");
            m_endDate = this.dateTimePickerEndDate.Value.ToString("yyyyMMdd");
            m_allReview = Convert.ToString(this.comboBoxReview.SelectedIndex);

            if(radioButtonMateriel.Checked)
            {
                string materielName = this.textBoxName.Text;
                MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromMaterielName(materielName);

                if (materiel != null)
                {
                    m_materielPkey = materiel.pkey;
                    this.Close();
                }
                else 
                {
                    m_materielPkey = -1;
                    MessageBoxExtend.messageWarning("输出的物料名称: " + materielName + ", 查询不到对应的物料信息, 请重新输出或点击物料选择按钮");
                    this.textBoxName.Text = "";
                }
            }
        }

        public string getFilterStartDate()
        {
            return m_startDate;
        }

        public string getFilterEndDate()
        {
            return m_endDate;
        }

        public string getIsReviewSign()
        {
            return m_allReview;
        }

        public int getMaterielPkey()
        {
            return m_materielPkey;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FormBaseMateriel fbs = new FormBaseMateriel(true);
            fbs.ShowDialog();

            m_materielPkey = fbs.getSelectRecordPkey();
            MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(m_materielPkey);
            this.textBoxName.Text = materiel.name;
        }

        private void radioButtonAllMateriel_Click(object sender, EventArgs e)
        {
            if (radioButtonAllMateriel.Checked)
            {
                m_materielPkey = -1;
                this.buttonSelect.Enabled = false;
                this.textBoxName.Text = "";
            }
            else 
            {
                this.buttonSelect.Enabled = true;
            }
        }
    }
}
