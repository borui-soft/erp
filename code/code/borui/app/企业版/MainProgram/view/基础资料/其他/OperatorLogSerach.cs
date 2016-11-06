using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.model;
using System.Collections;

namespace MainProgram
{
    public partial class OperatorLogSerach : Form
    {
        private string m_whereSQL = "";
        public OperatorLogSerach()
        {
            InitializeComponent();
        }

        private void radioButtonDate_Click(object sender, EventArgs e)
        {
            if(this.radioButtonDate.Checked)
            {
                this.dateTimePickerStartDate.Enabled = true;
                this.dateTimePickerEndDate.Enabled = true;
            }
            else
            {
                this.dateTimePickerStartDate.Enabled = false;
                this.dateTimePickerEndDate.Enabled = false;
            }
        }

        private void OperatorLogSerach_Load(object sender, EventArgs e)
        {
            this.radioButtonAllDate.Checked = true;
            this.comboBoxUserName.Items.Add("所有用户");
            this.comboBoxModuleName.Items.Add("所有模块");
            this.comboBoxHoseName.Items.Add("所有主机");

            // 初始化用户名下拉列表
            ArrayList userNameValues = OperatorLog.getInctance().getDistinecRecord("USER_NAME");
            ArrayList moduleNameValues = OperatorLog.getInctance().getDistinecRecord("MODULE_NAME");
            ArrayList hostNameValues = OperatorLog.getInctance().getDistinecRecord("HOST_NAME");

            for(int i = 0; i < userNameValues.Count; i++)
            {
                this.comboBoxUserName.Items.Add((string)userNameValues[i]);
            }

            for (int i = 0; i < moduleNameValues.Count; i++)
            {
                this.comboBoxModuleName.Items.Add((string)moduleNameValues[i]);
            }

            for (int i = 0; i < hostNameValues.Count; i++)
            {
                this.comboBoxHoseName.Items.Add((string)hostNameValues[i]);
            }

            this.comboBoxUserName.SelectedIndex = 0;
            this.comboBoxModuleName.SelectedIndex = 0;
            this.comboBoxHoseName.SelectedIndex = 0;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            m_whereSQL = "WHERE 1 = 1 ";

            if( this.comboBoxUserName.SelectedIndex != 0)
            {
                m_whereSQL += " AND USER_NAME = '" + this.comboBoxUserName.SelectedItem.ToString() + "'";
            }

            if (this.comboBoxModuleName.SelectedIndex != 0)
            {
                m_whereSQL += " AND MODULE_NAME = '" + this.comboBoxModuleName.SelectedItem.ToString() + "'";
            }

            if (this.comboBoxHoseName.SelectedIndex != 0)
            {
                m_whereSQL += " AND HOST_NAME = '" + this.comboBoxHoseName.SelectedItem.ToString() + "'";
            }

            if (this.radioButtonDate.Checked)
            {
                m_whereSQL += " AND OPER_TIME >= '" + this.dateTimePickerStartDate.Value.ToString("yyyyMMdd") + "'";
                m_whereSQL += " AND OPER_TIME <= '" + this.dateTimePickerEndDate.Value.ToString("yyyyMMdd") + "'";
            }

            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string getWhereSql()
        {
            return m_whereSQL;
        }
    }
}
