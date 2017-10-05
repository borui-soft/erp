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
        private int m_selectedTextboxIndex = 0;

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

            this.buttonSelect.Enabled = false;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_startDate = this.dateTimePickerStartDate.Value.ToString("yyyyMMdd");
            m_endDate = this.dateTimePickerEndDate.Value.ToString("yyyyMMdd");
            m_allReview = Convert.ToString(this.comboBoxReview.SelectedIndex);

            if(radioButtonMateriel.Checked)
            {
                if (this.textBoxName.Text.Length <= 0)
                {
                    m_materielPkey = -1;
                    MessageBoxExtend.messageWarning("输出的物料名称为空, 查询不到对应的物料信息, 请重新输出或点击物料选择按钮");
                    this.textBoxName.Text = "";

                    return;
                }
            }
            else if (radioButtonInterregional.Checked)
            {
                m_materielPkey = -1;

                if (this.textBoxStartID.Text.Length <= 0)
                {
                    MessageBoxExtend.messageWarning("输出的物料开始ID为空, 请重新输出");
                    this.textBoxStartID.Text = "";

                    return;
                }

                if (this.textBoxEndID.Text.Length <= 0)
                {
                    MessageBoxExtend.messageWarning("输出的物料结束ID为空, 请重新输出");
                    this.textBoxEndID.Text = "";

                    return;
                }
            }

            this.Close();
        }

        public int getSelectMode()
        {
            int mode = 0;

            if (radioButtonAllMateriel.Checked)
            {
                mode = 0;
            }
            else if (radioButtonInterregional.Checked)
            {
                mode = 1;
            }
            else if (radioButtonMateriel.Checked)
            {
                mode = 2;
            }
            else
            {
                mode = 0;
            }

            return mode;
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

        public string getFilterStartID()
        {
            return this.textBoxStartID.Text.ToString();
        }

        public string getFilterEndID()
        {
            return this.textBoxEndID.Text.ToString();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (m_selectedTextboxIndex <= 0 || m_selectedTextboxIndex > 3)
            {
                MessageBoxExtend.messageWarning("请将光标移动到指定输入框中后再重新选择物料");
                this.buttonSelect.Enabled = false;
                return;
            }

            FormBaseMateriel fbs = new FormBaseMateriel(true);
            fbs.ShowDialog();

            m_materielPkey = fbs.getSelectRecordPkey();

            if(m_materielPkey != 0 )
            {
                if (m_selectedTextboxIndex == 1)
                {
                    this.textBoxStartID.Text = Convert.ToString(m_materielPkey);
                }
                else if (m_selectedTextboxIndex == 2)
                {
                    this.textBoxEndID.Text = Convert.ToString(m_materielPkey);
                }
                else
                {
                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(m_materielPkey);
                    this.textBoxName.Text = materiel.name;
                }
            }

            this.buttonSelect.Enabled = false;
            m_selectedTextboxIndex = 0;
        }

        private void radioButtonAllMateriel_Click(object sender, EventArgs e)
        {
            this.textBoxName.Enabled = true;

            if (radioButtonAllMateriel.Checked)
            {
                m_materielPkey = -1;

                this.textBoxName.Enabled = false;
                this.textBoxName.Text = "";

                this.textBoxStartID.Enabled = false;
                this.textBoxEndID.Enabled = false;
                this.textBoxStartID.Text = "";
                this.textBoxEndID.Text = "";
            }
            else if (radioButtonInterregional.Checked)
            {
                this.textBoxName.Enabled = false;
                this.textBoxName.Text = "";

                this.textBoxStartID.Enabled = true;
                this.textBoxEndID.Enabled = true;

                if (m_selectedTextboxIndex != 1 && m_selectedTextboxIndex != 2)
                {
                    this.textBoxStartID.Text = "";
                    this.textBoxEndID.Text = "";
                }
            }
            else
            {
                this.textBoxStartID.Enabled = false;
                this.textBoxEndID.Enabled = false;
                this.textBoxStartID.Text = "";
                this.textBoxEndID.Text = "";

                this.textBoxName.Enabled = true;

                if (m_selectedTextboxIndex != 3)
                {
                    this.textBoxName.Text = "";
                }

                this.buttonSelect.Enabled = false;
            }
        }

        private void textBoxStartID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 单价只能只能输入数字或小数点或退格键
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

        private void textBoxStartID_Enter(object sender, EventArgs e)
        {
            this.buttonSelect.Enabled = true;
            m_selectedTextboxIndex = 1;
        }

        private void textBoxEndID_Enter(object sender, EventArgs e)
        {
            this.buttonSelect.Enabled = true;
            m_selectedTextboxIndex = 2;
        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            this.buttonSelect.Enabled = true;
            m_selectedTextboxIndex = 3;
        }
    }
}
