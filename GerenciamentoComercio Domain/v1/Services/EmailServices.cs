using GerenciamentoComercio_Domain.Utils.EmailSender;
using GerenciamentoComercio_Domain.Utils.Security;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IEmailSender _emailSender;

        public EmailServices(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void SendEmailRecoverPassword(Employee user)
        {
            string emailContent = Message("LaTech - ERP - Recuperação de senha",
                user.FullName, Security.EncryptString(user.Id.ToString()));

            _emailSender.Send(user.Email, "LaTech - ERP - Recuperação de senha", emailContent);
        }

        private static string Message(string title, string user, string token)
        {
            var sMensagem = "";

            sMensagem = "<link href=\"http://fmf.com.br/css/master.css\" rel=\"stylesheet\" type=\"text/css\" />";
            sMensagem += "<center><table style=\"width: 80%;\" cellpadding=\"10\" cellspacing=\"0\"><tr><td colspan=\"3\" align=\"center\">";
            sMensagem += "<p>&nbsp;</p></td></tr><tr><td bgcolor=\"#CCCCCC\" style=\"width: 50%\" colspan=\"3\" align='center'><br /><p>";
            sMensagem += "<b>" + title + "</b><br /><br />";
            sMensagem += $"Foi aberto uma solicitação para a recuperação de senha do usuário {user}";
            sMensagem += $"<h1 style=\"padding-left: 50px; font-family: 'Lato'; font-size: 12px; color: #414A54;\">Abaixo segue o link para você recuperar sua senha, caso não tenha sido você que abriu a solicitação, gentilza desconsiderar esse e-mail.:<br /></h1>" + Environment.NewLine;
            sMensagem += $"<a href='http://latech-erp.com/recover-password/{token}'>Link para recuperação de senha.</a>";
            sMensagem += "<td style=\"width: 33%\"></td>";
            sMensagem += "<td style=\"width: 33%\"></td>";
            sMensagem += "<td style=\"width: 33%\"></td>";
            sMensagem += "</tr></table></center>";

            return sMensagem;
        }
    }
}
