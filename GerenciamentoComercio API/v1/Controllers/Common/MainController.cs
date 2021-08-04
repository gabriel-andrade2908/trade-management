using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [ValidateModelState]
    [ApiController]
    public class MainController : ControllerBase
    {
        public MainController(IUserApp userApp)
        {
            UserCode = userApp.GetUserCode();
            UserEmail = userApp.GetUserEmail();
            UserAuthenticated = userApp.IsAuthenticated();
            UserName = userApp.GetUserName();
        }

        protected int UserCode { get; set; }
        protected string UserName { get; set; }
        protected string UserEmail { get; set; }
        protected bool UserAuthenticated { get; set; }
    }
}
