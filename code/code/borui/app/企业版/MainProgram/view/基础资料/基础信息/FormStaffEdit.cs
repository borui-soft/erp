using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainProgram.model;
using MainProgram.bus;

namespace MainProgram
{
    public partial class FormStaffEdit : Form
    {
        private StaffTable m_staff;

        private bool m_isAdd = true;
        private int m_staffPkey = 0;
        private int m_staffGroupPkey = 0;
        
        private string ROLE = "BASE_ROLE";
        private string STAFF_TYPE = "BASE_STAFF_TYPE";

        private bool m_isInit = false;

        public FormStaffEdit(bool isAdd, int staffGroupPkey, int pkey = 0)
        {
            m_isAdd = isAdd;
            m_staffGroupPkey = staffGroupPkey;
            m_staffPkey = pkey;

            InitializeComponent();
        }

        private void FormStaffEdit_Load(object sender, EventArgs e)
        {
            // 性别下拉框初始化
            comboBoxSex.Items.Add("男");
            comboBoxSex.Items.Add("女");
            comboBoxSex.SelectedIndex = 0;

            //学历下拉框初始化
            comboBoxEdu.Items.Add("小学");
            comboBoxEdu.Items.Add("中学");
            comboBoxEdu.Items.Add("高中");
            comboBoxEdu.Items.Add("专科");
            comboBoxEdu.Items.Add("本科");
            comboBoxEdu.Items.Add("硕士");
            comboBoxEdu.Items.Add("博士");
            comboBoxEdu.Items.Add("其他");
            comboBoxEdu.SelectedIndex = 4;

            // 初始化下拉框值
            ComboBoxExtend.initComboBox(this.comboBoxProfile, ROLE, true);
            ComboBoxExtend.initComboBox(this.comboBoxType, STAFF_TYPE, true);

            if (!m_isAdd)
            {
                this.buttonAdd.Enabled = false;

                m_staff = Staff.getInctance().getStaffInfoFromPkey(m_staffPkey);

                comboBoxSex.SelectedIndex = 0;
                comboBoxEdu.SelectedIndex = 0;
                setPageActiveValue();
            }

            m_isInit = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(m_isAdd)
            {
                addStaff();
            }
            else
            {
                modifyStaff();
            }

            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setPageActiveValue()
        {
            this.textBoxNum.Text = m_staff.number;
            this.textBoxName.Text = m_staff.name;
            this.comboBoxSex.Text = m_staff.sex;
            this.textBoxTel.Text = m_staff.tel;
            this.textBoxEmail.Text = m_staff.email;
            this.dateTimePickerDate.Text = m_staff.enterDate;
            this.comboBoxProfile.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(ROLE, m_staff.prifileID);
            this.comboBoxType.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(STAFF_TYPE, m_staff.staffType);
            textBoxNO.Text = m_staff.NO;
            comboBoxEdu.Text = m_staff.eduBackground;
            textBoxAddress.Text = m_staff.address;
            textBoxNote.Text = m_staff.remarks;
        }

        private void addStaff()
        {
            StaffTable staff = getPageStaffActiveData();

            if (Staff.getInctance().isExistStaffName(staff.name))
            {
                MessageBoxExtend.messageWarning("已存在[" + staff.name + "]用户，请适量修改用户名后重新保存");
            }
            else
            {
                Staff.getInctance().insert(staff);
            }
        }

        private StaffTable getPageStaffActiveData()
        {
            StaffTable staff = new StaffTable();
            staff.pkey = m_staffPkey;
            staff.departmentID = m_staffGroupPkey;
            staff.number = this.textBoxNum.Text;
            staff.name = this.textBoxName.Text;
            staff.sex = this.comboBoxSex.Text;
            staff.tel = this.textBoxTel.Text;
            staff.email = this.textBoxEmail.Text;
            staff.enterDate = this.dateTimePickerDate.Value.ToString("yyyyMMdd");
            staff.prifileID = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(ROLE, this.comboBoxProfile.Text);
            staff.staffType = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(STAFF_TYPE, this.comboBoxType.Text);
            staff.NO = this.textBoxNO.Text;
            staff.eduBackground = this.comboBoxEdu.Text;
            staff.address = this.textBoxAddress.Text;
            staff.remarks = this.textBoxNote.Text;

            return staff;
        }

        private void modifyStaff()
        {
            StaffTable staff = getPageStaffActiveData();
            Staff.getInctance().update(m_staffPkey, staff);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (m_isInit && !m_isAdd && m_staff != null)
            {
                if (m_staff.name.CompareTo(this.textBoxName.Text) != 0 ||
                    m_staff.number.CompareTo(this.textBoxNum.Text) != 0 ||
                    m_staff.sex.CompareTo(this.comboBoxSex.Text) != 0 ||

                    m_staff.email.CompareTo(this.textBoxEmail.Text) != 0 ||
                    m_staff.tel.CompareTo(this.textBoxTel.Text) != 0 ||
                    m_staff.NO.CompareTo(this.textBoxNO.Text) != 0 ||
                    m_staff.eduBackground.CompareTo(this.comboBoxEdu.Text) != 0 ||
                    m_staff.address.CompareTo(this.textBoxAddress.Text) != 0 ||
                    m_staff.enterDate.CompareTo(this.dateTimePickerDate.Value.ToString("yyyyMMdd")) != 0 ||
                    m_staff.prifileID != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(ROLE, this.comboBoxProfile.Text) ||
                    m_staff.staffType != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(STAFF_TYPE, this.comboBoxType.Text) ||
                    m_staff.remarks.CompareTo(this.textBoxNote.Text) != 0)
                {
                    this.buttonAdd.Enabled = true;
                }
                else
                {
                    this.buttonAdd.Enabled = false;
                }
            }
        }

        private void textBoxMAX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }
    }
}