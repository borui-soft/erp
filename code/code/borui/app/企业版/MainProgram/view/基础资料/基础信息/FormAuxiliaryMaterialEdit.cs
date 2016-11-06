using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormAuxiliaryMaterialEdit : Form
    {
        private int m_recordKey;
        private bool m_isAdd;
        private string m_name;
        private string m_desc;
        private string m_tableName;


        public FormAuxiliaryMaterialEdit(string tableName, int recordKey, string winText = "", bool isAdd = true, string name = "", string desc = "")
        {
            InitializeComponent();
            this.Text = winText;

            m_isAdd = isAdd;
            m_name = name;
            m_desc = desc;
            m_tableName = tableName;
            m_recordKey = recordKey;
        }

        private void FormAuxiliaryMaterialEdit_Load(object sender, EventArgs e)
        {
            if (!m_isAdd)
            {
                this.textBoxName.Text = m_name;
                this.textBoxDesc.Text = m_desc;
                this.buttonSave.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            AuxiliaryMaterialDataTable record = new AuxiliaryMaterialDataTable();
            record.name = this.textBoxName.Text;
            record.desc = this.textBoxDesc.Text;

            if (m_isAdd)
            {
                AuxiliaryMaterial.getInctance().insert(m_tableName, record);
            }
            else 
            {
                AuxiliaryMaterial.getInctance().update(m_recordKey, m_tableName, record.name, record.desc);
            }

            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxName.Text != m_name || this.textBoxDesc.Text != m_desc)
            {
                this.buttonSave.Enabled = true;
            }
            else
            {
                this.buttonSave.Enabled = false;
            }
        }
    }
}
