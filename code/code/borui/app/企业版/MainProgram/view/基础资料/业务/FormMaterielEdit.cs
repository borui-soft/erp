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
    public partial class FormMaterielEdit : Form
    {
        private int m_materielGroupPkey = 1;
        private bool m_isMaterielGroup = false;

        private MaterielTable m_materiel;
        private MaterielTypeTable m_materielType;

        private bool m_isAdd = true;
        private bool m_isEditMateriel = true;
        private int m_materielPkey = 0;

        private string UNIT_LIST_TABLE_NAME = "BASE_UNIT_LIST";
        private string STORAGE_LIST_TABLE_NAME = "BASE_STORAGE_LIST";
        private string MATERIEL_ATTRIBUTE_TABLE_NAME = "BASE_MATERIEL_ATTRIBUTE";
        private string VALUATION_TYPE_LIST_TABLE_NAME = "BASE_VALUATION_TYPE_LIST";

        private bool m_isInit = false;

        public FormMaterielEdit(bool isAdd, int materielGroupPkey, bool isEditMateriel = true, int pkey = 0)
        {
            m_isAdd = isAdd;
            m_materielGroupPkey = materielGroupPkey;
            m_isEditMateriel = isEditMateriel;
            m_materielPkey = pkey;

            InitializeComponent();
        }

        private void FormMaterielEdit_Load(object sender, EventArgs e)
        {
            // 初始化下拉框值
            ComboBoxExtend.initComboBox(this.comboBoxMaterielAttribut, MATERIEL_ATTRIBUTE_TABLE_NAME, true);
            ComboBoxExtend.initComboBox(this.comboBoxValuationType, VALUATION_TYPE_LIST_TABLE_NAME, true);
            ComboBoxExtend.initComboBox(this.comboBoxUnit, UNIT_LIST_TABLE_NAME, true);
            ComboBoxExtend.initComboBox(this.comboBoxUnitPurchase, UNIT_LIST_TABLE_NAME, true);
            ComboBoxExtend.initComboBox(this.comboBoxUnitSale, UNIT_LIST_TABLE_NAME, true);
            ComboBoxExtend.initComboBox(this.comboBoxStorage, STORAGE_LIST_TABLE_NAME, true);

            if (!m_isAdd)
            {
                this.buttonAdd.Enabled = false;
                this.buttonMaterielGroup.Enabled = false;

                if (m_isEditMateriel)
                {
                    m_materiel = Materiel.getInctance().getMaterielInfoFromPkey(m_materielPkey);
                }
                else
                {
                    m_materielType = MaterielType.getInctance().getMaterielTypeInfoFromPkey(m_materielGroupPkey);
                    m_isMaterielGroup = true;
                }

                setPageActiveState();
                setPageActiveValue();
            }

            m_isInit = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool isRet = true;

            if(m_isAdd)
            {
                if (!m_isMaterielGroup)
                {
                    isRet = addMateriel();
                }
                else
                {
                    isRet = addMaterielType();
                }
            }
            else
            {
                if (!m_isMaterielGroup)
                {
                    isRet = modifyMateriel();
                }
                else
                {
                    isRet = modifyMaterielType();
                }
            }

            if (isRet)
            {
                this.Close();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMaterielGroup_Click(object sender, EventArgs e)
        {
			if(m_isMaterielGroup)
			{
				m_isMaterielGroup = false;
			}
			else
			{
				m_isMaterielGroup = true;
			}

            setPageActiveState();
        }

        private void setPageActiveState()
        {
            if (m_isMaterielGroup)
            {
                buttonMaterielGroup.BackColor = System.Drawing.Color.LightGray;
                tabControl1.Visible = false;
                groupBoxMaterielGroup.Show();
            }
            else
            {
                buttonMaterielGroup.BackColor = System.Drawing.Color.Transparent;
                tabControl1.Visible = true;
                groupBoxMaterielGroup.Hide();
            }
        }

        private void setPageActiveValue()
        {
            if (m_isMaterielGroup)
            {
                this.textBoxMaterielGroupName.Text = m_materielType.name;
                this.textBoxGroupNum.Text = m_materielType.num;
                this.textBoxMaterielGroupDesc.Text = m_materielType.desc;
            }
            else
            {
                this.textBoxName.Text = m_materiel.name;
                this.textBoxNum.Text = m_materiel.num;
                this.textBoxShortName.Text = m_materiel.nameShort;
                this.textBoxModel.Text = m_materiel.model;
                this.textBoxMnemonicCode.Text = m_materiel.mnemonicCode;
                this.textBoxMIN.Text = Convert.ToString(m_materiel.min);
                this.textBoxMAX.Text = Convert.ToString(m_materiel.max);
                textBoxWarranty.Text = Convert.ToString(m_materiel.warramty);
                comboBoxMaterielAttribut.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(MATERIEL_ATTRIBUTE_TABLE_NAME, m_materiel.materielAttribute);
                comboBoxValuationType.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(VALUATION_TYPE_LIST_TABLE_NAME, m_materiel.valuation);
                comboBoxStorage.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(VALUATION_TYPE_LIST_TABLE_NAME, m_materiel.storage);
                comboBoxUnit.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(UNIT_LIST_TABLE_NAME, m_materiel.unit);
                comboBoxUnitPurchase.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(UNIT_LIST_TABLE_NAME, m_materiel.unitPurchase);
                comboBoxUnitSale.Text = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey(UNIT_LIST_TABLE_NAME, m_materiel.unitSale);
                this.textBoxNote.Text = m_materiel.note;
            }
        }

        private bool addMateriel()
        {
            MaterielTable materiel = getPageMaterielActiveData();

            if (materiel.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("物料信息保存失败, 物料名称不能为空，请重新输入");
                return false;
            }

            if (materiel.num.Length == 0)
            {
                MessageBoxExtend.messageWarning("物料信息保存失败, 物料编号不能为空，请重新输入");
                return false;
            }

            if (materiel.mnemonicCode.Length > 10)
            {
                MessageBoxExtend.messageWarning("物料信息保存失败, 助记码长度应小于10，请重新输入");
                return false;
            }

            Materiel.getInctance().insert(materiel);

            return true;
        }

        private MaterielTable getPageMaterielActiveData()
        {
            MaterielTable materiel = new MaterielTable();

            materiel.materielType = m_materielGroupPkey;
            materiel.name = this.textBoxName.Text.ToString();
            materiel.num = this.textBoxNum.Text.ToString();
            materiel.nameShort = this.textBoxShortName.Text.ToString();
            materiel.model = this.textBoxModel.Text.ToString();
            materiel.mnemonicCode = this.textBoxMnemonicCode.Text.ToString();

            if (this.textBoxMIN.Text.Length > 0)
            {
                materiel.min = Convert.ToInt32(this.textBoxMIN.Text.ToString());
            }
            else
            {
                materiel.min = 0;
            }

            if (this.textBoxMAX.Text.Length > 0)
            {
                materiel.max = Convert.ToInt32(this.textBoxMAX.Text.ToString());
            }
            else
            {
                materiel.max = 0;
            }

            if (this.textBoxWarranty.Text.Length > 0)
            {
                materiel.warramty = Convert.ToInt32(this.textBoxWarranty.Text.ToString());
            }
            else
            {
                materiel.warramty = 0;
            }

            if (this.comboBoxMaterielAttribut.Text.Length > 0)
            {
                materiel.materielAttribute = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(MATERIEL_ATTRIBUTE_TABLE_NAME, this.comboBoxMaterielAttribut.Text);
            }

            if (this.comboBoxValuationType.Text.Length > 0)
            {
                materiel.valuation = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(VALUATION_TYPE_LIST_TABLE_NAME, this.comboBoxValuationType.Text);
            }

            if (this.comboBoxStorage.Text.Length > 0)
            {
                materiel.storage = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(STORAGE_LIST_TABLE_NAME, this.comboBoxStorage.Text);
            }
            
            if (this.comboBoxUnit.Text.Length > 0)
            {
                materiel.unit = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnit.Text);
            }

            if (this.comboBoxUnitPurchase.Text.Length > 0)
            {
                materiel.unitPurchase = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnitPurchase.Text);
            }

            if (this.comboBoxUnitSale.Text.Length > 0)
            {
                materiel.unitSale = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnitSale.Text);
            }

            if (this.comboBoxUnit.Text.Length > 0)
            {
                materiel.unitStorage = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnit.Text);
            }

            materiel.note = this.textBoxNote.Text.ToString();

            return materiel;
        }

        private bool addMaterielType()
        {
            MaterielTypeTable materielType = new MaterielTypeTable();

            materielType.name = this.textBoxMaterielGroupName.Text.ToString();
            materielType.num = this.textBoxGroupNum.Text.ToString();
            materielType.desc = this.textBoxMaterielGroupDesc.Text.ToString();

            if (materielType.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("物料分组保存失败, 组名称不能为空，请重新输入!");
                return false;
            }

            if (materielType.num.Length == 0 || materielType.num.Length > 10)
            {
                MessageBoxExtend.messageWarning("物料分组保存失败, 组编号长度必须为1-10位，请重新输入!");
                return false;
            }

            MaterielType.getInctance().insert(materielType);


            // 物料组织结构
            MaterielOrgStructTable materielOrgInfo = new MaterielOrgStructTable();

            materielOrgInfo.parentPkey = MaterielOrgStruct.getInctance().getPkeyFromValue(m_materielGroupPkey);
            materielOrgInfo.value = MaterielType.getInctance().getMaxPkey();
            MaterielOrgStruct.getInctance().insert(materielOrgInfo);

            return true;
        }

        private bool modifyMateriel()
        {
            MaterielTable materiel = getPageMaterielActiveData();

            if (materiel.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("物料信息保存失败, 物料名称不能为空，请重新输入");
                return false;
            }

            if (materiel.num.Length == 0)
            {
                MessageBoxExtend.messageWarning("物料信息保存失败, 物料编号不能为空，请重新输入");
                return false;
            }

            if (materiel.mnemonicCode.Length > 10)
            {
                MessageBoxExtend.messageWarning("物料信息保存失败, 助记码长度应小于10，请重新输入");
                return false;
            }

            Materiel.getInctance().update(m_materielPkey, materiel);

            return true;
        }

        private bool modifyMaterielType()
        {
            MaterielTypeTable materielType = new MaterielTypeTable();
            materielType.name = this.textBoxMaterielGroupName.Text.ToString();
            materielType.num = this.textBoxGroupNum.Text.ToString();
            materielType.desc = this.textBoxMaterielGroupDesc.Text.ToString();

            if (materielType.name.Length == 0)
            {
                MessageBoxExtend.messageWarning("组名称不能为空，请重新填写!");
                return false;
            }

            if (materielType.num.Length == 0 || materielType.num.Length > 10)
            {
                MessageBoxExtend.messageWarning("物料分组保存失败, 组编号长度必须为1-10位，请重新输入!");
                return false;
            }

            MaterielType.getInctance().update(m_materielGroupPkey, materielType);

            return true;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (m_isInit)
            {
                if (!m_isAdd)
                {
                    if (!m_isMaterielGroup)
                    {
                        if (m_materiel != null)
                        {
                            if (m_materiel.name.CompareTo(this.textBoxName.Text) != 0 ||
                                m_materiel.num.CompareTo(this.textBoxNum.Text) != 0 ||
                                m_materiel.nameShort.CompareTo(this.textBoxShortName.Text) != 0 ||
                                m_materiel.model.CompareTo(this.textBoxModel.Text) != 0 ||
                                m_materiel.mnemonicCode.CompareTo(this.textBoxMnemonicCode.Text) != 0 ||
                                m_materiel.min != ConvertExtend.toInt32(this.textBoxMIN.Text.ToString()) ||
                                m_materiel.max != ConvertExtend.toInt32(this.textBoxMAX.Text.ToString()) ||
                                m_materiel.warramty != ConvertExtend.toInt32(this.textBoxWarranty.Text.ToString()) ||
                                m_materiel.materielAttribute != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(MATERIEL_ATTRIBUTE_TABLE_NAME, this.comboBoxMaterielAttribut.Text) ||
                                m_materiel.warramty != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(VALUATION_TYPE_LIST_TABLE_NAME, this.comboBoxValuationType.Text) ||
                                m_materiel.storage != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(STORAGE_LIST_TABLE_NAME, this.comboBoxStorage.Text) ||
                                m_materiel.unit != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnit.Text) ||
                                m_materiel.unitPurchase != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnitPurchase.Text) ||
                                m_materiel.unitSale != AuxiliaryMaterial.getInctance().getAuxiliaryMaterialPkeyFromName(UNIT_LIST_TABLE_NAME, this.comboBoxUnitSale.Text) ||
                                m_materiel.note.CompareTo(this.textBoxNote.Text) != 0)
                            {
                                this.buttonAdd.Enabled = true;
                            }
                            else
                            {
                                this.buttonAdd.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (m_materielType != null)
                        {
                            if (m_materielType.name.CompareTo(this.textBoxMaterielGroupName.Text) != 0 ||
                                m_materielType.num.CompareTo(this.textBoxGroupNum.Text) != 0 ||
                                m_materielType.desc.CompareTo(this.textBoxMaterielGroupDesc.Text) != 0)
                            {
                                this.buttonAdd.Enabled = true;
                            }
                            else
                            {
                                this.buttonAdd.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        private void textBoxMAX_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 物料上限、下限、保质期控件中只能输入数字和退格键
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