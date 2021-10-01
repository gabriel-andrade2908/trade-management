using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Net;

namespace GerenciamentoComercio_Domain.Utils.EmailSender.EmailConfig
{
    public class EmailConfig
    {
        public static string SendEmail(string from, string to, string subject, string message, string smtpServer, string smtpPort, string validUser, string validPassword, string attachment, bool SSL)
        {
            WebProxy nProxy;
            string sServidorProxy = "webmail.latech-erp.com";
            string sUsuarioProxy = "noreply@latech-erp.com";
            string sSenhaProxy = "Lealdrade123@";

            if (Strings.Trim(sServidorProxy) != string.Empty)
            {
                nProxy = new WebProxy(sServidorProxy);
                if (Strings.Trim(sUsuarioProxy) != string.Empty)
                    nProxy.Credentials = new NetworkCredential(sUsuarioProxy, sSenhaProxy);
                WebRequest.DefaultWebProxy = nProxy;
            }

            message += "<br /><br /><div style='font-size:xx-small; color:gray; font-family: verdana;'><hr>Esta mensagem, incluindo seus eventuais anexos, pode conter informações confidenciais, de uso restrito e/ou legalmente protegidas. Se você recebeu esta mensagem por engano, não deve usar, copiar, divulgar, distribuir ou tomar qualquer atitude com base nestas informações. Solicitamos que você elimine a mensagem imediatamente de seu sistema e avise-nos, enviando uma mensagem diretamente para o remetente e para <a href='mailto:" + from + "'>" + from + "</a>. Todas as opiniões, conclusões ou informações contidas nesta mensagem somente serão consideradas como provenientes da CATALDE BESSA ou de suas subsidiárias quando efetivamente confirmadas, formalmente, por um de seus representantes legais, devidamente autorizados para tanto.</div>";

            string sNaoEnviado = string.Empty;
            using (System.Net.Mail.MailMessage objectoEmail = new System.Net.Mail.MailMessage())
            {
                string[] Destinatario = Strings.Replace(Strings.Replace(to, Constants.vbCrLf, string.Empty), Constants.vbCr, string.Empty).Split(";");
                foreach (var sTemp in Destinatario)
                {
                    if (Strings.Trim(sTemp) != string.Empty)
                    {
                        try
                        {
                            objectoEmail.To.Add(new System.Net.Mail.MailAddress(Strings.Trim(sTemp), Strings.Trim(sTemp)));
                        }
                        catch (Exception ex)
                        {
                            sNaoEnviado += Interaction.IIf(sNaoEnviado == string.Empty, string.Empty, ";") + sTemp;
                        }
                    }
                }

                objectoEmail.ReplyTo = new System.Net.Mail.MailAddress(from);

                objectoEmail.Sender = new System.Net.Mail.MailAddress(from);

                objectoEmail.From = new System.Net.Mail.MailAddress(from);

                objectoEmail.IsBodyHtml = true;

                objectoEmail.Priority = System.Net.Mail.MailPriority.Normal;

                objectoEmail.Subject = subject;

                objectoEmail.Body = message;

                objectoEmail.IsBodyHtml = true;

                if (Strings.Trim(attachment) != string.Empty)
                {
                    foreach (var attach in Strings.Split(attachment, ";"))
                        objectoEmail.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(File.ReadAllBytes(attach)), new FileInfo(attach).Name));
                }
                using (System.Net.Mail.SmtpClient smtpSend = new System.Net.Mail.SmtpClient("webmail.latech-erp.com", 587))
                {
                    NetworkCredential nCredent = new NetworkCredential(validUser, validPassword);

                    smtpSend.Host = smtpServer;
                    if (Strings.Trim(smtpPort) != string.Empty)
                        smtpSend.Port = Convert.ToInt32(smtpPort);

                    if (Convert.ToBoolean(SSL) == true)
                        smtpSend.EnableSsl = true;
                    else
                        smtpSend.EnableSsl = false;

                    smtpSend.UseDefaultCredentials = false;

                    smtpSend.Credentials = nCredent;

                    smtpSend.Send(objectoEmail);
                }
            }

            return "Sua Mensagem foi enviada com sucesso para o(s) destinatário(s) de e-mail." + Interaction.IIf(sNaoEnviado == string.Empty, string.Empty, "<br>Exceto para: <b>" + sNaoEnviado + "</b>.");
        }
    }
}
