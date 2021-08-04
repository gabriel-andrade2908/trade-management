namespace GerenciamentoComercio_Domain.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public UserTokenResponse UserToken { get; set; }
    }
}
