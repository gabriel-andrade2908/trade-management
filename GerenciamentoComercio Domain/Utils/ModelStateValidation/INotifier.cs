using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.Utils.ModelStateValidation
{
    public interface INotifier
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
