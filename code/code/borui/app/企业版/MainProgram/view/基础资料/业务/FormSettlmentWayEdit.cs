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
    public partial class FormSettlmentWayEdit : Form
    {
        private bool m_isAdd, m_initFinshed = false;
        private int m_recordPkey;
        SettlmentWayTable m_settlmentWay = new SettlmentWayTable();

        public FormSettlmentWayEdit(string winText, bool isAdd = true, int recordPkey = 0)
        {
            InitializeComponent();
            this.Text = winText;
            m_isAdd = isAdd;
            m_recordPkey = recordPkey;
        }

        private void FormSettlmentWayEdit_Load(object sender, EventArgs e)
        {
            if(!m_isAdd)
            {
                m_settlmentWay = SettlmentWay.getInctance().getSettlmentWayInfoFromPeky(m_recordPkey);
                this.buttonSave.Enabled = false;
                this.textBoxName.Text = m_settlmentWay.name;
                this.comboBoxSubject.Text = m_settlmentWay.subjectID;
            }

            m_initFinshed = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SettlmentWayTable record = new SettlmentWayTable();

            record.name = this.textBoxName.Text;
            record.subjectID = this.comboBoxSubject.Text;

            if (m_isAdd)
            {
                SettlmentWay.getInctance().insert(record);
            }
            else
            {
                record.pkey = m_recordPkey;
                SettlmentWay.getInctance().update(m_recordPkey, record);
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!m_isAdd && m_initFinshed)
            {
                if (this.textBoxName.Text != m_settlmentWay.name || this.comboBoxSubject.Text != m_settlmentWay.subjectID)
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
}
