using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Net;

namespace Catalde.Tools.Email
{
    public class Correio
    {
        public static string EnviaEmailAnexo(string De, string Para, string Assunto, string Mensagem, string ServidorSmtp, string PortaSMTP, string UsuarioValida, string SenhaValida, string sAnexo, bool SSL)
        {
            try
            {
                WebProxy nProxy;
                string sServidorProxy = "mail.escalapro.com.br";
                string sUsuarioProxy = "noreply@escalapro.com.br";
                string sSenhaProxy = "Mob#1234";

                if (Strings.Trim(sServidorProxy) != string.Empty)
                {
                    nProxy = new WebProxy(sServidorProxy);
                    if (Strings.Trim(sUsuarioProxy) != string.Empty)
                        nProxy.Credentials = new NetworkCredential(sUsuarioProxy, sSenhaProxy);
                    WebRequest.DefaultWebProxy = nProxy;
                }
            }
            catch (Exception ex) { }

            try
            {
                Mensagem += "<br /><br /><div style='font-size:xx-small; color:gray; font-family: verdana;'><hr>Esta mensagem, incluindo seus eventuais anexos, pode conter informações confidenciais, de uso restrito e/ou legalmente protegidas. Se você recebeu esta mensagem por engano, não deve usar, copiar, divulgar, distribuir ou tomar qualquer atitude com base nestas informações. Solicitamos que você elimine a mensagem imediatamente de seu sistema e avise-nos, enviando uma mensagem diretamente para o remetente e para <a href='mailto:" + De + "'>" + De + "</a>. Todas as opiniões, conclusões ou informações contidas nesta mensagem somente serão consideradas como provenientes da CATALDE BESSA ou de suas subsidiárias quando efetivamente confirmadas, formalmente, por um de seus representantes legais, devidamente autorizados para tanto.</div>";

                string sNaoEnviado = string.Empty;
                using (System.Net.Mail.MailMessage objectoEmail = new System.Net.Mail.MailMessage())
                {
                    string[] Destinatario = Strings.Replace(Strings.Replace(Para, Constants.vbCrLf, string.Empty), Constants.vbCr, string.Empty).Split(";");
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

                    objectoEmail.ReplyTo = new System.Net.Mail.MailAddress(De);

                    objectoEmail.Sender = new System.Net.Mail.MailAddress(De);

                    objectoEmail.From = new System.Net.Mail.MailAddress(De);

                    objectoEmail.IsBodyHtml = true;

                    objectoEmail.Priority = System.Net.Mail.MailPriority.Normal;

                    objectoEmail.Subject = Assunto;

                    objectoEmail.Body = Mensagem;

                    objectoEmail.IsBodyHtml = true;

                    if (Strings.Trim(sAnexo) != string.Empty)
                    {
                        foreach (var attach in Strings.Split(sAnexo, ";"))
                            objectoEmail.Attachments.Add(new System.Net.Mail.Attachment(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(attach)), new FileInfo(attach).Name));
                    }
                    using (System.Net.Mail.SmtpClient smtpSend = new System.Net.Mail.SmtpClient("smtplw.com.br", 587))
                    {
                        System.Net.NetworkCredential nCredent = new System.Net.NetworkCredential(UsuarioValida, SenhaValida);

                        smtpSend.Host = ServidorSmtp;
                        if (Strings.Trim(PortaSMTP) != string.Empty)
                            smtpSend.Port = Convert.ToInt32(PortaSMTP);

                        if (System.Convert.ToBoolean(SSL) == true)
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
            catch (Exception ex_basic)
            {
                return "Erro: " + ex_basic.Message;
            }
        }
    }
}
