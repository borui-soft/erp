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
    public partial class FormProjectInfoTrackFilter : Form
    {
        private FormProjectInfoTrackFilterValue m_formStorageSequenceFilterValue = new FormProjectInfoTrackFilterValue();
        private bool m_isStorageOut = false;

        public FormProjectInfoTrackFilter(bool isStorageOut)
        {
            InitializeComponent();
            m_isStorageOut = isStorageOut;
        }

        private void FormProjectInfoTrackFilter_Load(object sender, EventArgs e)
        {
            // 初始审核标志下拉列表框
            this.comboBoxReview.Items.Add("已审核单据");
            this.comboBoxReview.Items.Add("全部单据");
            this.comboBoxReview.SelectedIndex = 1;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (this.textBoxProjectNum.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("项目编号不能为空, 请重新输入");
                return;
            }

            m_formStorageSequenceFilterValue.projectNum = this.textBoxProjectNum.Text;

            if (this.comboBoxReview.SelectedIndex == 1)
            {
                m_formStorageSequenceFilterValue.allReview = "1";
            }
            else
            {
                m_formStorageSequenceFilterValue.allReview = "0";
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public FormProjectInfoTrackFilterValue getFilterUIValue()
        {
            return m_formStorageSequenceFilterValue;
        }
    }

    public class FormProjectInfoTrackFilterValue
    {
        public string projectNum { get; set; }
        public string allReview { get; set; }
    }
}
