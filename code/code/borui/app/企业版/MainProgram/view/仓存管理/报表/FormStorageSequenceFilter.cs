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
    public partial class FormStorageSequenceFilter : Form
    {
        private FormStorageSequenceFilterValue m_formStorageSequenceFilterValue = new FormStorageSequenceFilterValue();
        private bool m_isStorageOut = false;

        public FormStorageSequenceFilter(bool isStorageOut)
        {
            InitializeComponent();
            m_isStorageOut = isStorageOut;
        }

        private void FormStorageSequenceFilter_Load(object sender, EventArgs e)
        {
            // 初始审核标志下拉列表框
            if (m_isStorageOut)
            {
                this.comboBoxSequenceType.Items.Add("销售出库序时簿");
                this.comboBoxSequenceType.Items.Add("生产领料序时簿");
                this.comboBoxSequenceType.Items.Add("盘亏毁损序时簿");
                this.comboBoxSequenceType.Items.Add("其他出库序时簿");
            }
            else
            {
                this.comboBoxSequenceType.Items.Add("外购入库序时簿");
                this.comboBoxSequenceType.Items.Add("产品入库序时簿");
                this.comboBoxSequenceType.Items.Add("盘盈入库序时簿");
                this.comboBoxSequenceType.Items.Add("其他入库序时簿");
            }
            this.comboBoxSequenceType.SelectedIndex = 0;

            // 开始日期，结束日期控件初始化，默认开始日期为本月第一天，默认结束日期为本月最后一天日期
            DateTime nowDate = DateTime.Now;
            DateTime currentMonthFirstDay = new DateTime(nowDate.Year, nowDate.Month, 1);
            DateTime currentMonthLastDay = currentMonthFirstDay.AddMonths(1).AddDays(-1);

            this.dateTimePickerStartDate.Value = currentMonthFirstDay;
            this.dateTimePickerEndDate.Value = currentMonthLastDay;

            // 初始审核标志下拉列表框
            this.comboBoxReview.Items.Add("已审核单据");
            this.comboBoxReview.Items.Add("全部单据");
            this.comboBoxReview.SelectedIndex = 1;

            // 红蓝标志下拉框初始化
            this.comboBoxColor.Items.Add("蓝字");
            this.comboBoxColor.Items.Add("红字");
            this.comboBoxColor.Items.Add("全部");
            this.comboBoxColor.SelectedIndex = 2;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_formStorageSequenceFilterValue.startDate = this.dateTimePickerStartDate.Value.ToString("yyyy-MM-dd");
            m_formStorageSequenceFilterValue.endDate = this.dateTimePickerEndDate.Value.ToString("yyyy-MM-dd");

            m_formStorageSequenceFilterValue.allReview = Convert.ToString(this.comboBoxReview.SelectedIndex);
            m_formStorageSequenceFilterValue.sequenceType = Convert.ToString(this.comboBoxSequenceType.SelectedIndex);
            m_formStorageSequenceFilterValue.billColor = Convert.ToString(this.comboBoxColor.SelectedIndex);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public FormStorageSequenceFilterValue getFilterUIValue()
        {
            return m_formStorageSequenceFilterValue;
        }
    }

    public class FormStorageSequenceFilterValue
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string allReview { get; set; }
        public string sequenceType { get; set; }
        public string billColor { get; set; }
    }
}
