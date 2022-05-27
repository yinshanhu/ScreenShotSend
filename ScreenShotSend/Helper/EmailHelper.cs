using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotSend.Helper
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="smtpServer">smtp服务器</param>
        /// <param name="port">端口</param>
        /// <param name="mailFrom">发件人邮箱</param>
        /// <param name="userPassword">密码</param>
        /// <param name="mailTo">收件人</param>
        /// <param name="strcc">抄送人</param>
        /// <param name="strBcc">密送</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="mailContent">内容</param>
        /// <param name="strs">附件</param>
        /// <returns>发送成功返回true否则false</returns>
        public static bool SendEmail(string smtpServer, int port, string mailFrom, string userPassword,
            string mailTo, string strcc, string strBcc, string mailSubject, string mailContent, string strs)
        {
            try
            {
                // 设置发送方的邮件信息
                // 邮件服务设置
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
                smtpClient.Host = smtpServer; //指定SMTP服务器
                smtpClient.Port = port;//端口
                smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//验证用户名和密码
                smtpClient.EnableSsl = true; //使用SSL 
                // 发送邮件设置       
                MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人

                mailMessage.Subject = mailSubject;//主题
                mailMessage.Body = mailContent;//内容
                mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                mailMessage.IsBodyHtml = true;//设置为HTML格式
                mailMessage.Priority = MailPriority.Normal;//优先级
                                                           //抄送人
                if (!string.IsNullOrEmpty(strcc))
                    mailMessage.CC.Add(strcc);
                //密送
                if (!string.IsNullOrEmpty(strBcc))
                    mailMessage.Bcc.Add(strBcc);
                //附件
                if (!string.IsNullOrEmpty(strs))
                {
                    List<string> paths = new List<string>();
                    if (strs.Contains(","))
                    {
                        paths = strs.Split(',').ToList();
                    }
                    else
                    {
                        paths.Add(strs);
                    }
                    foreach (var path in paths)
                    {
                        mailMessage.Attachments.Add(new Attachment(strs));
                    }
                }
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="smtpServer">smtp服务器</param>
        /// <param name="port">端口</param>
        /// <param name="mailFrom">发件人邮箱</param>
        /// <param name="userPassword">密码</param>
        /// <param name="mailTo">收件人</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="mailContent">内容</param>
        /// <param name="strs">附件</param>
        /// <returns>发送成功返回true否则false</returns>
        public static bool SendEmail(string smtpServer, int port, string mailFrom, string userPassword,
            string mailTo, string mailSubject, string mailContent, string strs)
        {
            return SendEmail(smtpServer, port, mailFrom, userPassword, mailTo, string.Empty, string.Empty, mailSubject, mailContent, strs);
        }
    }
}
