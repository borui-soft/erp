using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormProjectInfoTrackFilter : Form
    {
        private FormProjectInfoTrackFilterValue m_formStorageSequenceFilterValue = new FormProjectInfoTrackFilterValue();
        private bool m_isStorageOut = false;
        private bool m_isAciveProjectNum = false;

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

            this.textBoxProjectNum.Focus();

            this.listViewProList.Columns.Add("双击可快速选择编号", 140, HorizontalAlignment.Left);
            ListViewExtend.setListViewAttribute(listViewProList, 16);
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

            m_formStorageSequenceFilterValue.billNumber = this.textBoxBillNumber.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public FormProjectInfoTrackFilterValue getFilterUIValue()
        {
            return m_formStorageSequenceFilterValue;
        }

        private void buttonSelectProject_Click(object sender, EventArgs e)
        {
            // SELECT DISTINCT(PROJECT_NUM) FROM [PROJECT_MATERIE_MANAGER]
        }

        private void textBoxProjectNum_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxProjectNum.Text.Length <= 0)
            {
                this.listViewProList.Visible = false;
                return;
            }

            this.listViewProList.Visible = true;
            this.listViewProList.Items.Clear();

            ArrayList list = FormProject.getInctance().getProjectNumList(this.textBoxProjectNum.Text);

            for(int i = 0; i < list.Count; i++)
            {
                ArrayList record = new ArrayList();
                record.Add(list[i].ToString());

                ListViewExtend.insertDataToListView(this.listViewProList, record);
            }

            m_isAciveProjectNum = true;
        }

        private void listViewProList_MouseClick(object sender, MouseEventArgs e)
        {
            string value = this.listViewProList.SelectedItems[0].Text.ToString();

            if (m_isAciveProjectNum)
            {
                this.textBoxProjectNum.Text = value;
            }
            else
            {
                this.textBoxBillNumber.Text = value;
            }

            this.listViewProList.Hide();
        }

        private void listViewProList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listViewProList_MouseClick(sender, e);
        }

        private void listViewProList_Leave(object sender, EventArgs e)
        {
            this.listViewProList.Hide();
        }

        private void textBoxBillNumber_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxBillNumber.Text.Length <= 0)
            {
                this.listViewProList.Visible = false;
                return;
            }

            this.listViewProList.Visible = true;
            this.listViewProList.Items.Clear();

            ArrayList list = FormProject.getInctance().getbillNumberList(this.textBoxProjectNum.Text, this.textBoxBillNumber.Text);
            
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList record = new ArrayList();
                record.Add(list[i].ToString());

                ListViewExtend.insertDataToListView(this.listViewProList, record);
            }

            m_isAciveProjectNum = false;
        }
    }

    public class FormProjectInfoTrackFilterValue
    {
        public string projectNum { get; set; }
        public string billNumber { get; set; }
        public string allReview { get; set; }
    }
}
