namespace GerenciamentoComercio_Domain.Utils.ModelStateValidation
{
    public class Notification
    {
        public Notification(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; }
    }
}
