using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Diagnostics;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram
{
    public partial class FormMaterielDetailed : Form
    {
        private int m_materielID = -1;
        private string m_xxMaterielTablebillNubmber = "";

        public FormMaterielDetailed(int materielID, string xxMaterielTablebillNubmber)
        {
            InitializeComponent();

            m_materielID = materielID;
            m_xxMaterielTablebillNubmber = xxMaterielTablebillNubmber;
        }

        private void FormMaterielDetailed_Load(object sender, EventArgs e)
        {
            // 物料基本信息
            this.labelMaterielID.Text = Convert.ToString(m_materielID);
            MaterielTable record = Materiel.getInctance().getMaterielInfoFromPkey(m_materielID);
            this.labelMaterielName.Text = record.name;

            // 实际库存+预占
            InitMaterielTable MaterielCountdata = InitMateriel.getInctance().getMaterielInfoFromMaterielID(m_materielID);
            this.labelCount.Text = Convert.ToString(MaterielCountdata.value);
            this.labelProCount.Text = Convert.ToString(MaterielProOccupiedOrderDetails.getInctance().getMaterielProCountInfoFromProject(m_materielID));

            // 材料表情况
            if(m_xxMaterielTablebillNubmber.Length > 0)
            {
                this.labelxxMaterielTableNum.Text = m_xxMaterielTablebillNubmber;

                FormProjectMaterielTable projectInfo = FormProject.getInctance().getProjectInfoFromBillNumber(m_xxMaterielTablebillNubmber);
                this.labelProjectNum.Text = projectInfo.projectNum;
                this.labelProName.Text = projectInfo.projectName;
                this.labelDevMode.Text = projectInfo.deviceMode;

                SortedDictionary<int, ProjectManagerDetailsTable> list = 
                    ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(m_xxMaterielTablebillNubmber);


                foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in list)
                {
                    if (index.Value.materielID == m_materielID)
                    {
                        this.labelSub.Text = index.Value.deviceName;

                        this.labelNum.Text = index.Value.no;
                        this.labelSquence.Text = index.Value.sequence;

                        this.label1Value.Text = Convert.ToString(index.Value.value);
                        this.labelUseDate.Text = index.Value.useDate;

                        break;
                    }
                }
            }
            else
            {
                this.labelxxMaterielTableNum.Text = "     ";
                this.labelProjectNum.Text = "     ";
                this.labelProName.Text = "     ";
                this.labelDevMode.Text = "     ";
                this.labelSub.Text = "     ";
                this.label1Value.Text = "     ";
                this.labelUseDate.Text = "     ";
                this.labelNum.Text = "     ";
                this.labelSquence.Text = "     ";
                this.labelUseDate.Text = "     ";
            }
        }
    }
}
