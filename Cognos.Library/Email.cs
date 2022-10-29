using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library
{
    public class Email 
    {
        static Email() 
        { }

        #region Send with output error msg

        public static bool Send(out string errMsg, string subject, string body, params string[] to)
        {
            return Send(out errMsg, subject, body, true, null, to);
        }

        public static bool Send(out string errMsg, string subject, string body, bool isHtml = true, Attachment[] attachment = null, params string[] to)
        {
            MailMessage email = new MailMessage()
            {
                Subject = subject,
                IsBodyHtml = isHtml,
                Body = body
            };
            foreach (string item in to.Where(x => x.Contains("@")))
            {
                email.To.Add(item);
            }
            if (attachment != null)
            {
                foreach (Attachment item in attachment)
                {
                    email.Attachments.Add(item);
                }
            }

            return Send(out errMsg, email);
        }

        public static bool Send(out string errMsg, MailMessage email)
        {
            errMsg = "";
            try
            {
                Task emailTask = Task.Run(() => {
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.Send(email);
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region Send without output error

        public static bool Send(string subject, string body, params string[] to)
        {
            string errMsg;
            return Send(out errMsg, subject, body, true, null, to);
        }

        public static bool Send(string subject, string body, bool isHtml = true, Attachment[] attachment = null, params string[] to)
        {
            string errMsg;
            return Send(out errMsg, subject, body, isHtml, attachment, to);
        }

        public static bool Send(MailMessage email)
        {
            string errMsg;
            return Send(out errMsg, email);
        }

        #endregion

    }
}
