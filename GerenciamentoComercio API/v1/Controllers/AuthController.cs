using GerenciamentoComercio_API.v1.Controllers;
using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Sistema_Incidentes.v1.Controllers
{
    [Route("v{version:apiVersion}/auth/")]
    [ApiVersion("1.0")]
    public class AuthController : MainController
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices,
            IUserApp userApp) : base(userApp)
        {
            _authServices = authServices;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation("Authenticate user")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(LoginResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
        public IActionResult Login(LoginRequest request)
        {
            APIMessage response = _authServices.Login(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }
    }
}
