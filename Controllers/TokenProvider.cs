using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using System.Security.Claims;
using PoolGameAPI.modules;

namespace PoolGameAPI.Controllers
{
    public sealed class TokenProvider(IConfiguration configuration)
    {

        public string Create(Users user)
        {

            string secretKey = configuration["Jwt:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDesctiptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.username.ToString()),




                ]),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]

            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDesctiptor);
            return token;
        }

     public   string getUsernameFromToken(string token) {
            var handler = new JsonWebTokenHandler();
            var jwtToken= handler.ReadJsonWebToken(token);

            var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier || claim.Type == "sub");
        
        return usernameClaim.Value;
        }


    }
}
