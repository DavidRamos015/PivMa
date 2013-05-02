using System;
using System.Net.Mail;
using System.Net.Mime;
using MiniAmazon.Domain;
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

        private static void SendEmail(MailMessage mailMessage, Boolean async)
        {

            var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential(Utility.AdminEmail, Utility.AdminPassWord),
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

        public static bool SendEmail(IRepository repository, string emailtoSend, string displayName, string body, string subject, MailOperationType mailOperationType, bool async)
        {
            try
            {
                var mailMessage = new MailMessage
                    {
                        From = new MailAddress(Utility.AdminEmail, "Pivma - Online Shopping")
                    };

                mailMessage.To.Add(new MailAddress(emailtoSend, displayName));

                mailMessage.Subject = "Pivma - " + subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<br> <br>" + body;

                mailMessage.Body += "<br> <br> <br>";
                mailMessage.Body += "Correo generado automaticamente. <br>" +
                                    "Favor no responda, no brindaremos soporte a ningun mensaje enviado a este correo <br> <br> <br>";


                //FileStream fs = new FileStream("E:\\TestFolder\\test.pdf",
                //                   FileMode.Open, FileAccess.Read);
                //Attachment a = new Attachment(fs, "test.pdf",
                //                   MediaTypeNames.Application.Octet);
                //m.Attachments.Add(a);

                var str = "<html><body><h5>" + mailMessage.Body + "</h5><br/><img" +
                          "src=\"cid:image1\"></body></html>";
                var av = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

                var lr = new LinkedResource(@"D:\Cloud Computing\dropbox\Dropbox\unitec\prog4\proyecto\pivma resource\OnlineStore.jpg",
                                            MediaTypeNames.Image.Jpeg) { ContentId = "image1" };

                av.LinkedResources.Add(lr);
                mailMessage.AlternateViews.Add(av);

                SendEmail(mailMessage, async);

                SaveRegisterEmailSent(repository, "enviado", mailOperationType, true);
                return true;

            }
            catch (Exception ex)
            {
                SaveRegisterEmailSent(repository, ex.Message, mailOperationType, false);
                return false;
            }
        }


        private static ulong HashCodeConfirmation()
        {
            var when = DateTime.Now;
            var kind = (ulong)(int)when.Kind;
            return (kind << 62) | (ulong)when.Ticks;
        }

        public static void SaveRegisterEmailSent(IRepository repository, string description, MailOperationType mailOperationId, bool successfully)
        {
            var item = new EmailsSent
                {
                    CreateDateTime = DateTime.Now,
                    Description = description,
                    MailOperationId = (int)mailOperationId,
                    Successfully = successfully
                };

            repository.Create(item);
        }

        public static string SaveRegisterEmailConfirmationOperation(IRepository repository, string controllerToRedirect, string viewToRedirect, MailOperationType mailOperationTypeId, long objectID)
        {
            var codeToConfirm = HashCodeConfirmation().ToString();

            var item = new EmailsConfirmationOperation()
            {
                CreateDateTime = DateTime.Now,
                CodeToConfirm = codeToConfirm,
                ControllerToRedirect = controllerToRedirect,
                ViewToRedirect = viewToRedirect,
                MailOperationTypeId = (int)mailOperationTypeId,
                ObjectID = objectID,
            };

            repository.Create(item);

            return codeToConfirm;
        }
    }
}