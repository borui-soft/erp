using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainProgram
{
    public partial class FormPurchaseInfoCountFilter : Form
    {
        private string m_startDate;
        private string m_endDate;
        private string m_allReview;

        public FormPurchaseInfoCountFilter()
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
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_startDate = this.dateTimePickerStartDate.Value.ToString("yyyyMMdd");
            m_endDate = this.dateTimePickerEndDate.Value.ToString("yyyyMMdd");
            m_allReview = Convert.ToString(this.comboBoxReview.SelectedIndex);

            this.Close();
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
    }
}
