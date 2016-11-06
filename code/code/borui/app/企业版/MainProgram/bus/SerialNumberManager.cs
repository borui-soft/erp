using System;
using System.Data;
using System.Configuration;
using LumiSoft.Net.POP3.Client;
using LumiSoft.Net.Mime;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace MainProgram.bus
{
    public class serialNumberManager
    {
        #region 序列号有限性验证(是否是由本企业序列号生成器生成)
        // 数据源
        static private string[] g_sourceDateArr = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", 
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
            "U", "V", "W", "X", "Y", "Z"
        };

        // 序列号加密规则
        static private int[] g_ruleArr = 
        {
            3, 6, 10, 15, 21,
            2, 5, 8, 11, 14,
            7, 9, 2, 16, 13,
            19, 13, 2, 15, 1,
            28, 35, 22, 37, 73
        };

        static public bool checkKey(string sourceGroupValue, string nextSourceGroupValue, int ruleArrFirstPos)
        {
            bool isRet = false;

            // 验证第二组key是否正确
            string firstGroupKey1 = sourceGroupValue.Substring(0, 1);
            string firstGroupKey2 = sourceGroupValue.Substring(1, 1);
            string firstGroupKey3 = sourceGroupValue.Substring(2, 1);
            string firstGroupKey4 = sourceGroupValue.Substring(3, 1);
            string firstGroupKey5 = sourceGroupValue.Substring(4, 1);

            string secordGroupKey1 = nextSourceGroupValue.Substring(0, 1);
            string secordGroupKey2 = nextSourceGroupValue.Substring(1, 1);
            string secordGroupKey3 = nextSourceGroupValue.Substring(2, 1);
            string secordGroupKey4 = nextSourceGroupValue.Substring(3, 1);
            string secordGroupKey5 = nextSourceGroupValue.Substring(4, 1);

            int newPos1 = getNumPosInSourceDateArr(firstGroupKey1) + g_ruleArr[ruleArrFirstPos];
            int newPos2 = getNumPosInSourceDateArr(firstGroupKey2) + g_ruleArr[ruleArrFirstPos + 1];
            int newPos3 = getNumPosInSourceDateArr(firstGroupKey3) + g_ruleArr[ruleArrFirstPos + 2];
            int newPos4 = getNumPosInSourceDateArr(firstGroupKey4) + g_ruleArr[ruleArrFirstPos + 3];
            int newPos5 = getNumPosInSourceDateArr(firstGroupKey5) + g_ruleArr[ruleArrFirstPos + 4];

            newPos1 = newPos1 - ((newPos1 / 36) * 36);
            newPos2 = newPos2 - ((newPos2 / 36) * 36);
            newPos3 = newPos3 - ((newPos3 / 36) * 36);
            newPos4 = newPos4 - ((newPos4 / 36) * 36);
            newPos5 = newPos5 - ((newPos5 / 36) * 36);

            if (secordGroupKey1 == g_sourceDateArr[newPos1] &&
                secordGroupKey2 == g_sourceDateArr[newPos2] &&
                secordGroupKey3 == g_sourceDateArr[newPos3] &&
                secordGroupKey4 == g_sourceDateArr[newPos4] &&
                secordGroupKey5 == g_sourceDateArr[newPos5])
            {
                isRet = true;
            }

            return isRet;
        }

        static private int getNumPosInSourceDateArr(string key)
        {
            int pos = 0;

            for (int i = 0; i < 36; i++)
            {
                if (g_sourceDateArr[i] == key)
                {
                    pos = i;
                    break;
                }
            }

            return pos;
        }

        #endregion

        #region 序列号是否已经被注册
        static public bool serialNumberIsExist(string serialNumber)
        {
            bool isRet = false;

            List<Mime> eMailList = POP3.getEmails();
            foreach (Mime eMail in eMailList)
            {
                if (eMail.BodyText != null)
                {
                    if (eMail.BodyText.IndexOf(serialNumber) != -1)
                    {
                        isRet = true;
                        break;
                    }
                }

                if (eMail.BodyHtml != null)
                {
                    if (eMail.BodyHtml.IndexOf(serialNumber) != -1)
                    {
                        isRet = true;
                        break;
                    }
                }
            }

            return isRet;
        }
        #endregion

        #region 发送序列号
        static public void sendSerialNumberEmail(string titleText, string emailContent)
        {
            //string content = "软件序列号：" + emailContent + "\n";
            //content += "注册日期：" + DateTime.Now.ToString("yyyyMMdd");
            SMTP.sendEmail(titleText, emailContent, "borui_key@163.com");
        }
        #endregion
    }

    public class POP3
    {
        static public List<Mime> getEmails()
        {
            List<Mime> result = new List<Mime>();

            using (POP3_Client pop3 = new POP3_Client())
            {
                try
                {
                    pop3.Connect("pop3.163.com", 110, false);
                    pop3.Authenticate("borui_key@163.com", "6728924", false);

                    //获取邮件信息列表
                    POP3_ClientMessageCollection infos = pop3.Messages;

                    foreach (POP3_ClientMessage info in infos)
                    {
                        byte[] bytes = info.MessageToByte();

                        //解析从Pop3服务器发送过来的邮件信息
                        Mime mime = Mime.Parse(bytes);

                        result.Add(mime);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return result;
        }
    }

    public class SMTP
    {
        static public void sendEmail(string titleText, string emailContent, string eMailAddress = "borui_erp@163.com")
        {
            MailMessage message = new MailMessage(eMailAddress, eMailAddress, titleText, emailContent);
            message.IsBodyHtml = true;
            SmtpClient sc = new SmtpClient("SMTP.163.com");
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential(eMailAddress, "6728924");
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.Send(message);
        }
    }
}