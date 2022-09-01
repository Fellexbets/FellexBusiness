
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Igor_AIS_Proj.Auxiliary
{
    public class AuthTools : IAuthTools
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
        public int? ValidateJwt1(string token)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var IdToken = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);
                return IdToken;
            }
            catch
            {
                return null;
            }
        }


        //public Session GenerateToken(Session session, User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        //                    new Claim("id", user.UserId.ToString()),
        //                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
        //                    new Claim(ClaimTypes.Sid, session.Id.ToString())

        //    }),
        //        Expires = DateTime.UtcNow.AddMinutes(5),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    session.UserToken = tokenHandler.WriteToken(token);
        //    return session;
        //}
        public User GenerateTokenOld()
        {
            User user = new User();
            var tokenHandler =  new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

            var tokenDescriptor =  new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim("id", user.UserId.ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Name, user.Username)
                    //new Claim(ClaimTypes.Sid session.Id.ToString())

            }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            user.UserToken =  tokenHandler.WriteToken(token);
            return user;
        }

        public string GetClaim(string userToken, string claimType)
        {
            userToken = userToken.Replace("Bearer ", ""); // remove Bearer prefix
            JwtSecurityToken token = new JwtSecurityToken(userToken);

            return token.Claims.FirstOrDefault(claim => claim.Type == claimType).Value;
        }

        public bool ValidateJwt(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = GetTokenValidation();
            Task<TokenValidationResult> tokenValidationResult = tokenHandler.ValidateTokenAsync(authToken, validationParameters);
            if (tokenValidationResult.Result.IsValid)
            {
                return true;
            }
            return false;
        }
        private TokenValidationParameters GetTokenValidation()
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(configuration["Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            return tokenValidationParameters;
        }
        public Session GenerateRefreshToken(Session session)
        {
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                session.RefreshToken = Convert.ToBase64String(randomNumber);
                session.RefreshTokenExpiresAt = DateTime.UtcNow.AddMinutes(10);
                return session;
            }
        }
    }
}
