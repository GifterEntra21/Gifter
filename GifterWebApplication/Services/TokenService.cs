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
        private DateTime ExpiryTime { get; set; }

        public SingleResponse<string> GenerateAccessToken(IEnumerable<Claim> claims)
        {
            try
            {
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                if (AppSettings.IsDevelopingMode)
                {
                    ExpiryTime = DateTime.Now.AddMinutes(300);
                }
                else
                {
                    ExpiryTime = DateTime.Now.AddMinutes(1);
                }

                JwtSecurityToken tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7008",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: ExpiryTime,
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
                    ValidateIssuer = true, //Valida o "emissor", ou seja, quem recebe as requisições
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
