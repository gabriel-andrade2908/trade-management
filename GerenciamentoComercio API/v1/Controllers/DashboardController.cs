using GerenciamentoComercio_Domain.DTOs.Dashboard;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/dashboard/")]
    [ApiVersion("1.0")]
    public class DashboardController : MainController
    {
        private readonly IDashboardServices _dashboardServices;

        public DashboardController(IDashboardServices dashboardServices,
            IUserApp userApp, INotifier notifier) : base(userApp, notifier)
        {
            _dashboardServices = dashboardServices;
        }

        [HttpGet("{numberOfDays}")]
        [SwaggerOperation("Shows the dashboard")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetDashboardResponse))]
        public IActionResult GetDashboard(int numberOfDays)
        {
            APIMessage response = _dashboardServices.GetDashboard(numberOfDays);

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }
    }
}
