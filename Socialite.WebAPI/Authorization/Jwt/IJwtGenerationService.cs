using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Socialite.WebAPI.Authorization.Jwt
{
    public interface IJwtGenerationService
    {
        string GenerateToken(string subject, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string emailAddress, string id);

        string GenerateJwt(ClaimsIdentity claimsIdentity, string userEmail, JsonSerializerSettings jsonSerializerSettings);
    }
}