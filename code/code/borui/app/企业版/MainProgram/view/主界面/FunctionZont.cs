using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using Glass;
using System.Threading;
using MainProgram.model;

namespace MainProgram
{
    public partial class FunctionZont : Form
    {
        private FormMain m_mainPagePoint = null;
        private string m_defaultPanelFunctonPageName = "";
        private string m_defaultPanelReportPageName = "";
        private string CONFIG_FILE_NAME = "FunctionZone.xml";
        private string m_exeFileFullPath = "", m_exeFileRelativePath = "";

        private GlassButton m_selectButton = null;

        private Color BUTTON_INIT_BACK_COLOR = Color.CornflowerBlue;
        private Color BUTTON_SELECT_BACK_COLOR = Color.GhostWhite;

        private Color BUTTON_INIT_FONT_BACK_COLOR = Color.White;
        private Color BUTTON_INIT_SELECT_FONT_BACK_COLOR = Color.DarkBlue;

        private FunctionZontBaseInfo m_functionZontBaseInfo = new FunctionZontBaseInfo();
        private SortedDictionary<string, FunctionZontButtonInfo> m_functionZoneButtonList = new SortedDictionary<string, FunctionZontButtonInfo>();
        private SortedDictionary<string, GlassButton> m_buttonList = new SortedDictionary<string, GlassButton>();

        public FunctionZont(FormMain mainPagePoint)
        {
            InitializeComponent();
            m_mainPagePoint = mainPagePoint;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 读配置文件
            readConfigFile();

            // 设置窗体背景图片
            this.BackgroundImage = Image.FromFile(m_exeFileRelativePath + m_functionZontBaseInfo.backgroundImage);

            // 初始化按钮列表
            int index = 0;
            foreach (KeyValuePair<string, FunctionZontButtonInfo> button in m_functionZoneButtonList)
            {
                /* 财务系统或业务系统未初始化时，左侧功能区对应按钮是不显示的
                 * button isVisable属性代表按钮是否需要显示
                 * */
                if (button.Value.isVisable)
                {
                    createAndSetButtonStyle(button.Value, m_functionZontBaseInfo.buttonStartPosX,
                      index++ * (m_functionZontBaseInfo.buttonHeigth + m_functionZontBaseInfo.buttonInterval) + m_functionZontBaseInfo.buttonStartPosY);

                    // m_defaultPanelFunctonPageName代表系统启动后，默认需要显示在主功能区的from的名称
                    // m_defaultPanelReportPageName代表系统启动后，默认需要显示在报表功能区的from的名称
                    if (m_defaultPanelFunctonPageName.Length == 0 && m_defaultPanelReportPageName.Length == 0)
                    {
                        m_defaultPanelFunctonPageName = button.Value.panelPageName;
                        m_defaultPanelReportPageName = button.Value.reportPanelPageName;
                    }
                }
            }

            if (m_defaultPanelFunctonPageName.Length > 0)
            {
                ShowOneFormToPanelFuntionArea(FormFactory.getInctance().getFormObjectFromName(m_defaultPanelFunctonPageName));
            }

            if (m_defaultPanelReportPageName.Length > 0)
            {
                ShowOneFormToPanelReportArea(FormFactory.getInctance().getFormObjectFromName(m_defaultPanelReportPageName));
            }
        }

        private void readConfigFile()
        {
            XmlDocument XMLDoc = new XmlDocument();
            m_exeFileFullPath = Application.ExecutablePath;
            m_exeFileRelativePath = m_exeFileFullPath.Substring(0, m_exeFileFullPath.LastIndexOf("\\") + 1);
            string fileFullPath = m_exeFileRelativePath + CONFIG_FILE_NAME;

            try
            {
                XMLDoc.Load(fileFullPath);

                XmlNodeList notes = XMLDoc.DocumentElement.ChildNodes;
                XmlElement baseInfo = (XmlElement)notes[0];
                XmlElement functonZoneInfo = (XmlElement)notes[1];
                XmlNodeList functionZoneButtons = (XmlNodeList)notes[1].ChildNodes;

                m_functionZontBaseInfo.backgroundImage = baseInfo.GetAttribute("backgroundImage");
                m_functionZontBaseInfo.buttonWidth = Convert.ToInt32(baseInfo.GetAttribute("buttonWidth").ToString());
                m_functionZontBaseInfo.buttonHeigth = Convert.ToInt32(baseInfo.GetAttribute("buttonHeigth").ToString());
                m_functionZontBaseInfo.buttonStartPosX = Convert.ToInt32(baseInfo.GetAttribute("buttonStartPosX").ToString());
                m_functionZontBaseInfo.buttonStartPosY = Convert.ToInt32(baseInfo.GetAttribute("buttonStartPosY").ToString());
                m_functionZontBaseInfo.buttonInterval = Convert.ToInt32(baseInfo.GetAttribute("buttonInterval").ToString());

                for (int i = 0; i < functionZoneButtons.Count; i++)
                {
                    XmlElement button = (XmlElement)functionZoneButtons[i];
                    FunctionZontButtonInfo buttonInfo = new FunctionZontButtonInfo();

                    buttonInfo.name = button.GetAttribute("name");
                    buttonInfo.text = button.GetAttribute("text");
                    buttonInfo.sequence = button.GetAttribute("sequence");
                    buttonInfo.image = button.GetAttribute("image");
                    buttonInfo.mouseClick = button.GetAttribute("mouseClick");
                    buttonInfo.panelPageName = button.GetAttribute("panelPageName");
                    buttonInfo.reportPanelPageName = button.GetAttribute("reportPanelPageName");
                    buttonInfo.isVisable = isEnable(buttonInfo.text);

                    m_functionZoneButtonList.Add(buttonInfo.name, buttonInfo);
                }
            }
            catch (Exception)
            {
                // .. error log
            }
        }

        private void createAndSetButtonStyle(FunctionZontButtonInfo button, int posX, int posY)
        {
            GlassButton glassButton = new GlassButton();
            glassButton.Location = new Point(posX, posY);
            glassButton.BackColor = Color.CornflowerBlue;
            glassButton.ShineColor = Color.CornflowerBlue;
            glassButton.GlowColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            glassButton.Height = m_functionZontBaseInfo.buttonHeigth;
            glassButton.Width = m_functionZontBaseInfo.buttonWidth;
            glassButton.Text = button.text;
            glassButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            glassButton.TextAlign = ContentAlignment.MiddleRight;
            glassButton.Cursor = Cursors.Hand;
            glassButton.FadeOnFocus = true;
            glassButton.InnerBorderColor = Color.Transparent;
            glassButton.Name = button.name;
            glassButton.OuterBorderColor = Color.Transparent;
            glassButton.TabIndex = Convert.ToInt32(button.sequence);
            glassButton.Image = Image.FromFile(m_exeFileRelativePath + button.image);
            glassButton.ImageAlign = ContentAlignment.MiddleLeft;
            glassButton.Click += new System.EventHandler(this.ButtonFrom_Click);

            this.Controls.Add(glassButton);
            this.ResumeLayout(false);

            m_buttonList.Add(button.name, glassButton);
        }

        private void ButtonFrom_Click(object sender, EventArgs e)
        {
            bool pictureBoxEnabled = true;
            string buttonName = ((GlassButton)sender).Name;
            SetSelectButtonState(m_buttonList[buttonName]);

            ShowOneFormToPanelFuntionArea(FormFactory.getInctance().getFormObjectFromName(m_functionZoneButtonList[buttonName].panelPageName));

            if (m_functionZoneButtonList[buttonName].reportPanelPageName.Length == 0)
            {
                // 当单击按钮对应的panel没有相应报表展示的时候，自动吧PanelFunction和PanelReport之间的分割线隐藏
                pictureBoxEnabled = false;
            }

            ShowOneFormToPanelReportArea(FormFactory.getInctance().getFormObjectFromName(m_functionZoneButtonList[buttonName].reportPanelPageName),
                pictureBoxEnabled);
            
        }

        private void SetSelectButtonState(GlassButton button)
        {
            if (m_selectButton != null)
            {
                m_selectButton.BackColor = BUTTON_INIT_BACK_COLOR;
                m_selectButton.ShineColor = BUTTON_INIT_BACK_COLOR;
                m_selectButton.ForeColor = BUTTON_INIT_FONT_BACK_COLOR;
                m_selectButton.Font = new System.Drawing.Font("宋体", 12F);
            }

            button.BackColor = BUTTON_SELECT_BACK_COLOR;
            button.ShineColor = BUTTON_SELECT_BACK_COLOR;
            button.ForeColor = BUTTON_INIT_SELECT_FONT_BACK_COLOR;
            button.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);

            m_selectButton = button;
        }

        private void ShowOneFormToPanelFuntionArea(Form child)
        {
            if (child != null)
            {
                try
                {
                    m_mainPagePoint.panelFunction.Controls.Clear();
                    child.FormBorderStyle = FormBorderStyle.None;
                    child.TopLevel = false;
                    child.Dock = DockStyle.Fill;
                    child.BackColor = Color.White;

                    m_mainPagePoint.panelFunction.Controls.Add(child);
                    child.Show();
                    child.Activate();
                    child.WindowState = FormWindowState.Normal;
                }
                catch (Exception)
                {

                }
            }
        }

        private void ShowOneFormToPanelReportArea(Form child, bool pictureBoxEnabled = true)
        {
            if (pictureBoxEnabled)
            {
                m_mainPagePoint.pictureBox.Show();

                if (child != null)
                {
                    try
                    {
                        m_mainPagePoint.panelReport.Controls.Clear();
                        child.FormBorderStyle = FormBorderStyle.None;
                        child.TopLevel = false;
                        child.Dock = DockStyle.Fill;
                        child.BackColor = Color.White;

                        m_mainPagePoint.panelReport.Controls.Add(child);
                        child.Show();
                        child.Activate();
                        child.WindowState = FormWindowState.Normal;
                    }
                    catch (Exception)
                    {
 
                    }
                }
            }
            else
            {
                // 如果不需要显示PanelFunction和PanelReport之间的分割线，说明此时PanelReport没有对应的form需要展示
                // 此时需要隐藏分割线且把PanelReport区域清空
                m_mainPagePoint.pictureBox.Hide();
                m_mainPagePoint.panelReport.Controls.Clear();
            }
        }

        bool isEnable(string buttonText)
        {
            bool isRet;

            if (buttonText.IndexOf("初始化") != -1 || buttonText.IndexOf("基础") != -1)
            {
                isRet = true;
            }
            else if (buttonText.IndexOf("现金") != -1 || buttonText.IndexOf("银行") != -1 || buttonText.IndexOf("财务") != -1)
            {
                isRet = InitSubSystemSign.getInctance().isFinancialSystemInit();
            }
            else
            {
                isRet = InitSubSystemSign.getInctance().isBusinessSystemInit();
            }

            return isRet;
        }
    }

    public class FunctionZontBaseInfo
    {
        public string backgroundImage;
        public int buttonWidth;
        public int buttonHeigth;
        public int buttonStartPosX;
        public int buttonStartPosY;
        public int buttonInterval;
    }

    public class FunctionZontButtonInfo
    {
        public string name;
        public string text;
        public string sequence;
        public string image;
        public string mouseClick;
        public string panelPageName;
        public string reportPanelPageName;
        public bool isVisable;
    }
}