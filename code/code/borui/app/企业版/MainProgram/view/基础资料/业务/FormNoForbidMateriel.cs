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
    public partial class FormNoForbidMateriel : Form
    {
        private ArrayList m_forbidMaterielList = new ArrayList();
        

        public FormNoForbidMateriel(ArrayList forbidMateriels, string winText)
        {
            InitializeComponent();
            m_forbidMaterielList = forbidMateriels;
            this.Text = winText;
        }

        private void FormNoForbidMateriel_Load(object sender, EventArgs e)
        {
            this.listViewNoForbidList.Columns.Add("", 40, HorizontalAlignment.Left);
            this.listViewNoForbidList.Columns.Add("ID", 40, HorizontalAlignment.Left);
            this.listViewNoForbidList.Columns.Add("名称", 250, HorizontalAlignment.Left);
            ListViewExtend.setListViewAttribute(listViewNoForbidList, 16);

            for(int i = 0; i < m_forbidMaterielList.Count; i = i + 2)
            {
                ArrayList record = new ArrayList();

                record.Add("");
                record.Add(m_forbidMaterielList[i]);
                record.Add(m_forbidMaterielList[i+1]);

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
                    Materiel.getInctance().noForbidMateriel(Convert.ToInt32(listViewNoForbidList.Items[i].SubItems[1].Text.ToString()));
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