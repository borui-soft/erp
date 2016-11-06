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

namespace MainProgram
{
    public partial class FormDepartmentEdit : Form
    {
        private bool m_isManager = false;
        private ArrayList m_forbidCustomerList = new ArrayList();
        

        public FormDepartmentEdit(ArrayList forbidCustomers, string winText, bool isManager)
        {
            InitializeComponent();
            m_forbidCustomerList = forbidCustomers;
            this.Text = winText;
            m_isManager = isManager;
        }

        private void FormDepartmentEdit_Load(object sender, EventArgs e)
        {
            this.listViewNoForbidList.Columns.Add("", 40, HorizontalAlignment.Left);
            this.listViewNoForbidList.Columns.Add("ID", 40, HorizontalAlignment.Left);
            this.listViewNoForbidList.Columns.Add("名称", 250, HorizontalAlignment.Left);
            ListViewExtend.setListViewAttribute(listViewNoForbidList, 16);

            for(int i = 0; i < m_forbidCustomerList.Count; i = i + 2)
            {
                ArrayList record = new ArrayList();

                record.Add("");
                record.Add(m_forbidCustomerList[i]);
                record.Add(m_forbidCustomerList[i+1]);

                ListViewExtend.insertDataToListView(this.listViewNoForbidList, record);
            }

            if (!m_isManager)
            {
                this.buttonAdd.Enabled = false;
                this.buttonDelete.Enabled = false;
                this.buttonModify.Enabled = false;
            }
        }

        private void buttonAllSelect_Click(object sender, EventArgs e)
        {
            if (this.buttonAllSelect.Text == "全选")
            {
                for (int i = 0; i < this.listViewNoForbidList.Items.Count; i++)
                {
                    this.listViewNoForbidList.Items[i].Checked = true;
                }

                this.buttonAllSelect.Text = "全清";
            }
            else
            {
                for (int i = 0; i < this.listViewNoForbidList.Items.Count; i++)
                {
                    this.listViewNoForbidList.Items[i].Checked = false;
                }

                this.buttonAllSelect.Text = "全选";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void buttonModify_Click(object sender, EventArgs e)
        {

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}