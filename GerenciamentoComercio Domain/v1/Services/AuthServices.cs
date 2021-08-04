using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.JWTSettings;
using GerenciamentoComercio_Domain.Utils.PasswordGenerator;
using GerenciamentoComercio_Domain.Utils.Security;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using Incidentes.Business.DTOs.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Business.v1.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        public AuthServices(IOptions<JwtSettings> jwtSettings,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _jwtSettings = jwtSettings.Value;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public APIMessage Login(LoginRequest request)
        {
            Employee user = _employeeRepository.CheckLogin(request);

            if (user == null)
            {
                return new APIMessage(HttpStatusCode
                    .Unauthorized, new List<string> { "E-mail ou senha inválidos." });
            }
            ClaimsIdentity userClaims = GetClaimsUser(user);

            string token = EncodeToken(userClaims, _jwtSettings);

            return new APIMessage(HttpStatusCode
                .OK, GetResponseToken(token, user, userClaims.Claims, _jwtSettings));
        }

        public APIMessage GetTokenRecoverPassword(string userEmail)
        {
            Employee user = _employeeRepository.GetUserByEmail(userEmail);

            if (user == null) return new APIMessage(HttpStatusCode
                .NotFound, new List<string> { "Nenhum usuário encontrado." });

            return new APIMessage(HttpStatusCode.OK, user);
        }

        public async Task<APIMessage> ReadTokenRecoverPassword(string token)
        {
            int userId = Convert.ToInt32(Security.DecryptString(token));

            Employee user = await _employeeRepository.GetById(userId);

            if (user == null) return new APIMessage(HttpStatusCode
                .NotFound, new List<string> { "Não foi encontrado nenhum usuário com esse token." });

            return new APIMessage(HttpStatusCode.OK, new TokenRecoverPasswordResponse
            {
                UserCode = user.Id,
                UserEmail = user.Email,
                UserName = user.FullName
            });
        }

        public async Task<APIMessage> RecoverPassword(string token, string newPassword)
        {
            int userId = Convert.ToInt32(Security.DecryptString(token));

            Employee user = await _employeeRepository.GetById(userId);

            if (user == null) return new APIMessage(HttpStatusCode
                .NotFound, "Não foi encontrado nenhum usuário com esse token.");

            if (!PasswordGenerator.ValidatePassword(newPassword, 6, 1, 0, 1, 1, 1))
                return new APIMessage(HttpStatusCode
                    .BadRequest, "A nova senha deve conter 1 letra maiúscula, 1 letra minúscula," +
                    " 1 caractere especial, 1 número e no mínimo 6 caracteres.");

            user.Password = Security.EncryptString(newPassword);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, "Senha alterada com sucesso.");
        }

        private ClaimsIdentity GetClaimsUser(Employee user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim("UserName", user.FullName));
            claims.Add(new Claim("UserCode", user.Id.ToString()));

            ClaimsIdentity identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims, JwtSettings jwtSetings)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler()
            {
                SetDefaultTimesOnTokenCreation = false
            };

            byte[] key = Encoding.ASCII.GetBytes(jwtSetings.Key ?? "MEUSEGREDOSUPERSECRETO");

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = jwtSetings.Issuer,
                Audience = jwtSetings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddDays(9999),
                NotBefore = DateTime.UtcNow.AddDays(-10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginResponse GetResponseToken(string encodedToken, Employee user, IEnumerable<Claim> claims, JwtSettings jwtSettings)
        {
            return new LoginResponse
            {
                AccessToken = encodedToken,
                UserToken = new UserTokenResponse
                {
                    Claims = claims.Select(c => new UserClaimsResponse { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}

