using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.JWTSettings;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Incidentes.Business.v1.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthServices(IOptions<JwtSettings> jwtSettings,
            IEmployeeRepository employeeRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _employeeRepository = employeeRepository;
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

