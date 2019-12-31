using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Socialite.WebAPI.IdentityServer;

namespace Socialite.WebAPI.Authorization.Jwt
{
    public struct JwtClaimIdentifiers
    {
        public static string Id = "id";

        public static string Rol = "rol";
    }

    public class JwtGenerationService : IJwtGenerationService
    {
        public readonly DateTime EpochDateTime = new DateTime(1970, 1, 1);

        public readonly IOptions<JwtIssuerOptions> jwtIssuerOptions;

        public JwtGenerationService(IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            this.jwtIssuerOptions = jwtIssuerOptions;
        }

        public string GenerateJwt(ClaimsIdentity claimsIdentity, string userEmail, JsonSerializerSettings jsonSerializerSettings)
        {
            var response = new
            {
                id = claimsIdentity.Claims.Single(c => c.Type == JwtClaimIdentifiers.Id).Value,
                authToken = GenerateToken(userEmail, claimsIdentity),
                expiresIn = (int)jwtIssuerOptions.Value.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, jsonSerializerSettings);
        }

        public string GenerateToken(string subject, ClaimsIdentity identity)
        {
            var claims = GetClaims(subject);

            var jwt = GetJwtSecurityToken(claims);

            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encryptedToken;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string emailAddress, string id)
        {
            var claims = new[]
            {
                new Claim(JwtClaimIdentifiers.Id, id),
                new Claim(JwtClaimIdentifiers.Rol, "api"),
            };

            return new ClaimsIdentity(new GenericIdentity(emailAddress, "Token"), claims);
        }

        private Claim[] GetClaims(string subject)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    (DateTime.Now - EpochDateTime).TotalSeconds.ToString(),
                    ClaimValueTypes.Integer64
                )
            };
        }

        private JwtSecurityToken GetJwtSecurityToken(Claim[] claims)
        {
            return new JwtSecurityToken(
                issuer: jwtIssuerOptions.Value.Issuer,
                audience: jwtIssuerOptions.Value.Audience,
                claims: claims,
                notBefore: jwtIssuerOptions.Value.NotBefore,
                expires: jwtIssuerOptions.Value.Expiration,
                signingCredentials: jwtIssuerOptions.Value.SigningCredentials
            );
        }
    }
}