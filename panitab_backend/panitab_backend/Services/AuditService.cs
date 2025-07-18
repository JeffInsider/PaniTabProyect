using Microsoft.IdentityModel.Tokens;
using panitab_backend.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace panitab_backend.Services
{
    public class AuditService : IAuditService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ILogger<AuditService> _logger;

        public AuditService(
            IHttpContextAccessor httpContextAccessor,
            TokenValidationParameters tokenValidationParameters,
            ILogger<AuditService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenValidationParameters = tokenValidationParameters;
            _logger = logger;
        }

        public string GetUserId()
        {
            try
            {
                // 1. Obtener el token del header
                var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    _logger.LogWarning("No se encontró token de autenticación");
                    return "system"; // Valor por defecto
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();

                // 2. Validar y decodificar el token
                var handler = new JwtSecurityTokenHandler();
                var claimsPrincipal = handler.ValidateToken(token, _tokenValidationParameters, out _);

                // 3. Buscar el UserId en los claims (prueba con diferentes nombres de claim)
                var userId = claimsPrincipal.Claims
                    .FirstOrDefault(c => c.Type == "UserId" ||
                                       c.Type == ClaimTypes.NameIdentifier ||
                                       c.Type == JwtRegisteredClaimNames.Sub)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("Token válido pero no contiene claim UserId");
                    return "system";
                }

                return userId;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning("Token expirado: {Message}", ex.Message);
                return "system";
            }
            catch (SecurityTokenValidationException ex)
            {
                _logger.LogWarning("Token inválido: {Message}", ex.Message);
                return "system";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener UserId");
                return "system"; // Siempre retorna un valor por defecto
            }
        }
    }
}

//using Microsoft.IdentityModel.Tokens;
//using panitab_backend.Services.Interfaces;
//using System.IdentityModel.Tokens.Jwt;

//namespace panitab_backend.Services
//{
//    public class AuditService : IAuditService
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly TokenValidationParameters _tokenValidationParameters;

//        public AuditService(
//            IHttpContextAccessor httpContextAccessor,
//            TokenValidationParameters tokenValidationParameters)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _tokenValidationParameters = tokenValidationParameters;
//        }

//        // esta funcion es para obtener el id del usuario que esta haciendo la peticion
//        public string GetUserId()
//        {
//            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

//            // si el token es nulo o vacio, se retorna nulo
//            if (string.IsNullOrEmpty(token))
//                return null;

//            var handler = new JwtSecurityTokenHandler();
//            try
//            {
//                var claimsPrincipal = handler.ValidateToken(token, _tokenValidationParameters, out _);
//                var userClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId");

//                return userClaim?.Value;
//            }
//            catch
//            {
//                //opcional: loggear el error
//                return null;
//            }

//        }
//    }
//}
