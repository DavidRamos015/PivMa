using System;
using System.Net.Mail;
using System.Net.Mime;
using MiniAmazon.Domain.Entities;

namespace MiniAmazon.Web.Infrastructure
{
    public class EmailUtility
    {
        private delegate void SendEmailDelegate(System.Net.Mail.MailMessage mailMessage);

        private static void SendEmailResponse(IAsyncResult asyncResult)
        {
            var _delegate = (SendEmailDelegate)(asyncResult.AsyncState);

            _delegate.EndInvoke(asyncResult);
        }

        public static void SendEmail(System.Net.Mail.MailMessage mailMessage, Boolean async)
        {

            var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("mformytest@gmail.com", "pwmformytest123"),
                    EnableSsl = true
                };

            smtpClient.Send(mailMessage);

            if (async)
            {
                var sd = new SendEmailDelegate(smtpClient.Send);
                var cb = new AsyncCallback(SendEmailResponse);
                sd.BeginInvoke(mailMessage, cb, sd);
            }
            else
            {
                smtpClient.Send(mailMessage);
            }
        }

        public static bool SendEmail(string emailtoSend, string displayName, string body, string subject, MailOperationType mailOperationType, bool async)
        {
            try
            {
                var mailMessage = new MailMessage
                    {
                        From = new MailAddress("mformytest@gmail.com", "Pivma - Online Shopping")
                    };

                mailMessage.To.Add(new MailAddress(emailtoSend, displayName));

                mailMessage.Subject = "Pivma - " + subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;

                mailMessage.Body += Environment.NewLine + Environment.NewLine + Environment.NewLine;
                mailMessage.Body += "Correo generado automaticamente." + Environment.NewLine +
                                    "Favor no responda, no brindaremos soporte a ningun mensaje enviado a este correo" +
                                    Environment.NewLine + Environment.NewLine + Environment.NewLine;


                //FileStream fs = new FileStream("E:\\TestFolder\\test.pdf",
                //                   FileMode.Open, FileAccess.Read);
                //Attachment a = new Attachment(fs, "test.pdf",
                //                   MediaTypeNames.Application.Octet);
                //m.Attachments.Add(a);

                var str = "<html><body><h4>" + mailMessage.Body + "</h4><br/><img" +
                          "src=\"cid:image1\"></body></html>";
                var av = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

                var lr = new LinkedResource(@"D:\Cloud Computing\dropbox\Dropbox\unitec\prog4\proyecto\pivma resource\OnlineStore.jpg",
                                            MediaTypeNames.Image.Jpeg) { ContentId = "image1" };

                av.LinkedResources.Add(lr);
                mailMessage.AlternateViews.Add(av);

                SendEmail(mailMessage, async);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}