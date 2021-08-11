using GerenciamentoComercio_API.v1.Controllers;
using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using Incidentes.Business.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace Sistema_Incidentes.v1.Controllers
{
    [AllowAnonymous]
    [Route("v{version:apiVersion}/auth/")]
    [ApiVersion("1.0")]
    public class AuthController : MainController
    {
        private readonly IAuthServices _authServices;
        private readonly IEmailServices _emailServices;

        public AuthController(IAuthServices authServices,
            IUserApp userApp, INotifier notifier,
            IEmailServices emailServices) : base(userApp, notifier)
        {
            _authServices = authServices;
            _emailServices = emailServices;
        }

        [HttpPost("login")]
        [SwaggerOperation("Authenticate user")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(LoginResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = _authServices.Login(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("get-token-recover-password/{userEmail}")]
        [SwaggerOperation("Send an email to the user to retrieve the password")]
        [SwaggerResponse(StatusCodes.Status200OK, "Email send successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User email not found", typeof(string))]
        public IActionResult GetTokenRecoverPassword(string userEmail)
        {
            var response = _authServices.GetTokenRecoverPassword(userEmail);

            if (response.StatusCode != HttpStatusCode.OK)
                return StatusCode((int)response.StatusCode, response.Content);

            _emailServices.SendEmailRecoverPassword((Employee)response.ContentObj);

            return Ok("Email para recuperação de senha enviado com sucesso.");
        }

        [HttpGet("read-recover-token")]
        [SwaggerOperation("Returns the token user informations")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(TokenRecoverPasswordResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User token not found", typeof(string))]
        public async Task<IActionResult> TokenRecoverPassword(string token)
        {
            APIMessage tokenRecover = await _authServices.ReadTokenRecoverPassword(token);

            return StatusCode((int)tokenRecover.StatusCode, tokenRecover.Content);
        }

        [HttpPut("recover-password")]
        [SwaggerOperation("User sets a new password")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User token not found", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Password is in an incorrect format", typeof(string))]
        public async Task<IActionResult> RecoverPassword(string token, string newPassword)
        {
            APIMessage recover = await _authServices.RecoverPassword(token, newPassword);

            return StatusCode((int)recover.StatusCode, recover.Content);
        }
    }
}
