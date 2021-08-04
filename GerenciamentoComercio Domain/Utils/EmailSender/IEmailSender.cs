namespace GerenciamentoComercio_Domain.Utils.EmailSender
{
    public interface IEmailSender
    {
        void Send(string email, string subject, string message);
        void Send(string[] emails, string subject, string message);
    }
}