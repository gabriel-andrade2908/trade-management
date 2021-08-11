using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.Utils.ModelStateValidation
{
    public class Notifier : INotifier
    {
        private List<Notification> _notificacoes;

        public Notifier()
        {
            _notificacoes = new List<Notification>();
        }

        public void Handle(Notification notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notification> GetNotifications()
        {
            return _notificacoes;
        }

        public bool HasNotifications()
        {
            return _notificacoes.Any();
        }
    }
}
