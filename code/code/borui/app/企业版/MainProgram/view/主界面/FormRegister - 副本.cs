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
using TIV.Core.TivLogger;

namespace MainProgram
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxTel.Text.Length == 0)
            {
                MessageBoxExtend.messageWarning("联系电话不能为空");
                return;
            }

            // 6MY5O-8R6G2-F08WF-YDABG-QCWCH
            string keyGroup1 = this.textBoxKey1.Text;
            string keyGroup2 = this.textBoxKey2.Text;
            string keyGroup3 = this.textBoxKey3.Text;
            string keyGroup4 = this.textBoxKey4.Text;
            string keyGroup5 = this.textBoxKey5.Text;

            /* 注册函数逻辑
             * 1、判断是否系统已经被注册为正版软件
             *      如果是，提示用户无需要重复注册。
             *      如果不是，进行步骤2
             * 2、验证序列号格式：判断下每组序列号长度是否为5
             *      如果不是，则说明序列号格式错误
             *      如果是，则进行步骤3
             * 3、验证序列号是否是正版序列号
             *      如果不是，则提示用户 序列号错误，请联系软件供应商，索取正版序列号。
             *      如果是，则进行步骤4
             * 4、查询邮件服务器，看是否次序列号已经被其他单位或企业注册过
             *      如果已被注册过，说明客户拿到的序列号不是软件供应商提供的全新序列号，是从其他单位借的，或其他
             *      如果没有注册过，说明次序列号为全新序列号，执行步骤5
             * 5、把序列号信息保存到数据库,分别保存到INIT_SUB_SYSTEM_SIGN表和BASE_SETTLMENT_WAY表，确保不太容易被破解
             * 6、发送邮件：邮件标题为：序列号。正为是序列号 + 当前注册日期
             * */

            if (keyGroup1.Length != 5 || keyGroup2.Length != 5 || keyGroup3.Length != 5 || keyGroup4.Length != 5 || keyGroup5.Length != 5)
            {
                MessageBoxExtend.messageWarning("序列号错误，请联系软件供应商，索取正版序列号。");
            }
            else
            {
                if (DbPublic.getInctance().isGenuineSoftware())
                {
                    MessageBoxExtend.messageWarning("软件已注册为正版，不需要重复注册");
                }
                else 
                {
                    TivLog.Logger.Info("开始验证是否是正版序列号...");
                    if (serialNumberManager.checkKey(keyGroup1, keyGroup2, 5) &&
                    serialNumberManager.checkKey(keyGroup2, keyGroup3, 10) &&
                    serialNumberManager.checkKey(keyGroup3, keyGroup4, 15) &&
                    serialNumberManager.checkKey(keyGroup4, keyGroup5, 20))
                    {
                        TivLog.Logger.Info("序列号验证通过，开始查找是否已经被注册过");
                        try
                        {
                            string serialNumber = keyGroup1 + keyGroup2 + keyGroup3 + keyGroup4 + keyGroup5;
                            if (serialNumberManager.serialNumberIsExist(serialNumber))
                            {
                                MessageBoxExtend.messageWarning("注册失败！此序列号已被使用，请联系软件供应商，重新索取序列号。");
                            }
                            else
                            {
                                TivLog.Logger.Info("序列号验未被使用，开始注册工作...");
                                serialNumberManager.sendSerialNumberEmail(serialNumber, serialNumber + this.textBoxTel.Text);
                                TivLog.Logger.Info("邮件发送成功，开始写数据库");

                                // 插入到INIT_SUB_SYSTEM_SIGN
                                InitSubSystemSign.getInctance().register(serialNumber);

                                // 插入到BASE_SETTLMENT_WAY
                                SettlmentWayTable record = new SettlmentWayTable();
                                record.name = serialNumber;
                                record.subjectID = "#####";
                                SettlmentWay.getInctance().insert(record, false);

                                TivLog.Logger.Info("数据库更新成功，序列号注册成功");
                                MessageBoxExtend.messageOK("感谢您注册并激活产品");
                            }
                        }
                        catch (Exception exp)
                        {
                            TivLog.Logger.Error(exp.ToString());
                            MessageBoxExtend.messageWarning("注册失败！可能因为网络未联通或网络堵塞等原因造成，请稍后重新");
                        }
                    }
                    else
                    {
                        MessageBoxExtend.messageError("序列号错误，请联系软件供应商，索取正版序列号。");
                    }
                }
                
                this.Close();
            }
        }

        private void textBoxTel_KeyPress(object sender, KeyPressEventArgs e)
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
