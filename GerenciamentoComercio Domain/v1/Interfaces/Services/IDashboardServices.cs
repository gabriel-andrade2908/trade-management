using GerenciamentoComercio_Domain.DTOs.Dashboard;
using GerenciamentoComercio_Domain.Utils.APIMessage;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IDashboardServices
    {
        APIMessage GetDashboard(int numberOfDays);
    }
}
