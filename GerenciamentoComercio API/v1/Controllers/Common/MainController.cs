using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        public MainController(IUserApp userApp, INotifier notifier)
        {
            UserCode = userApp.GetUserCode();
            UserEmail = userApp.GetUserEmail();
            UserAuthenticated = userApp.IsAuthenticated();
            UserName = userApp.GetUserName();
            IsAdmin = userApp.GetIfUserIsAdmin();
            _notifier = notifier;
        }

        protected int UserCode { get; set; }
        protected string UserName { get; set; }
        protected string UserEmail { get; set; }
        protected bool UserAuthenticated { get; set; }
        protected string IsAdmin { get; set; }

        protected IActionResult ModelStateBadRequest(ModelStateDictionary modelState)
        {
            string modelStateErrors = modelState.SelectMany(m =>
            m.Value.Errors.Select(e =>
                (e.Exception == null ? e.ErrorMessage : e.Exception.Message)))
                    .FirstOrDefault();

            return BadRequest(modelStateErrors);
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotifications();
        }
        protected ActionResult CustomReturn(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    sucess = true
                });
            }

            return BadRequest(_notifier.GetNotifications().Select(n => n.Mensagem));
        }
        protected ActionResult CustomReturn(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) GetErrors(modelState);

            return CustomReturn();
        }
        protected void GetErrors(ModelStateDictionary modelState)
        {
            var errors = ModelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage :
                    error.Exception.Message;
                ReportErro(errorMsg);
            }
        }
        protected void ReportErro(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
