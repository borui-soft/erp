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
    public enum ForbidDataType
    {
        Supplier,
        Customer,
        Materiel,
        Staff
    }
    public partial class FormNoForbid : Form
    {

        private ArrayList m_forbidDataList = new ArrayList();
        private ForbidDataType m_forbidDataType;

        public FormNoForbid(ArrayList forbidList, string winText, ForbidDataType type)
        {
            InitializeComponent();
            m_forbidDataList = forbidList;
            m_forbidDataType = type;
            this.Text = winText;
        }

        private void FormNoForbid_Load(object sender, EventArgs e)
        {
            this.listViewNoForbidList.Columns.Add("", 40, HorizontalAlignment.Left);
            this.listViewNoForbidList.Columns.Add("ID", 40, HorizontalAlignment.Left);
            this.listViewNoForbidList.Columns.Add("名称", 250, HorizontalAlignment.Left);
            ListViewExtend.setListViewAttribute(listViewNoForbidList, 16);

            for(int i = 0; i < m_forbidDataList.Count; i = i + 2)
            {
                ArrayList record = new ArrayList();

                record.Add("");
                record.Add(m_forbidDataList[i]);
                record.Add(m_forbidDataList[i+1]);

                ListViewExtend.insertDataToListView(this.listViewNoForbidList, record);
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

        private void buttonNoForbid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewNoForbidList.Items.Count; i++)
            {
                if(this.listViewNoForbidList.Items[i].Checked)
                {
                    int pkey = Convert.ToInt32(listViewNoForbidList.Items[i].SubItems[1].Text.ToString());

                    if (m_forbidDataType == ForbidDataType.Supplier)
                    {
                        Supplier.getInctance().noForbidSupplier(pkey);
                    }
                    else if (m_forbidDataType == ForbidDataType.Customer)
                    {
                        Customer.getInctance().noForbidCustomer(pkey);
                    }
                    else if (m_forbidDataType == ForbidDataType.Materiel)
                    {
                        Materiel.getInctance().noForbidMateriel(pkey);
                    }
                    else if (m_forbidDataType == ForbidDataType.Staff)
                    {
                        Staff.getInctance().noForbidStaff(pkey);
                    }
                    else 
                    {
                        // 咱不支持的数据类型
                    }

                    this.listViewNoForbidList.Items[i].Remove();
                    i--;
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}