using Microsoft.IdentityModel.Tokens;
using panitab_backend.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace panitab_backend.Services
{
    public class AuditService : IAuditService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuditService(
            IHttpContextAccessor httpContextAccessor,
            TokenValidationParameters tokenValidationParameters)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenValidationParameters = tokenValidationParameters;
        }

        // esta funcion es para obtener el id del usuario que esta haciendo la peticion
        public string GetUserId()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // si el token es nulo o vacio, se retorna nulo
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var claimsPrincipal = handler.ValidateToken(token, _tokenValidationParameters, out _);
                var userClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId");

                return userClaim?.Value;
            }
            catch
            {
                //opcional: loggear el error
                return null;
            }

        }
    }
}
