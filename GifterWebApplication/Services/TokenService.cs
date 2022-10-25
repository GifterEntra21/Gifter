using Microsoft.IdentityModel.Tokens;
using Shared.Responses;
using Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthentication.Server.Services
{
    public class TokenService : ITokenService
    {


        public SingleResponse<string> GenerateAccessToken(IEnumerable<Claim> claims)
        {
            try
            {
                DateTime expiryTime = DateTime.Now.AddMinutes(1);
                if (AppSettings.IsDevelopingMode)
                {
                    expiryTime = DateTime.Now.AddHours(8);
                }
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken tokeOptions = new JwtSecurityToken(
                    issuer: "https://gifterserver.azurewebsites.net",
                    audience: "https://gifter-e21.netlify.app",
                    claims: claims,
                    expires: expiryTime,
                    signingCredentials: signinCredentials
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(tokenString);
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<string>(ex);
            }

        }
        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public SingleResponse<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {

                    ValidateAudience = false, //Valida o "consumidor", ou seja, quem faz as requisições
                    ValidateIssuer = false, //Valida o "emissor", ou seja, quem recebe as requisições
                    ValidateIssuerSigningKey = true,//Valida a assinatura do "emissor", ou seja, a criptografia que gerou o acess Token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                    ValidateLifetime = false //nesse caso não importa se o token está ou nao expira, pois para nos so importa os dados que ele contem
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(principal);
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<ClaimsPrincipal>(ex);
            }

        }
    }
}
