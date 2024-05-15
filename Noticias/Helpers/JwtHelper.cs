using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Noticias.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(string username, string role, List<Claim> claims)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var issuer = _configuration.GetSection("JwtBearer").GetValue<string>("Issuer")??"";
            var audience = _configuration.GetSection("JwtBearer").GetValue<string>("Audience") ?? "";
            var secret = _configuration.GetSection("JwtBearer").GetValue<string>("Secret") ?? "";

            List<Claim> bas=new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Iss,issuer),
                new Claim(JwtRegisteredClaimNames.Aud,audience),
               
            };
            bas.AddRange(claims);
            
            JwtSecurityToken jwt = new JwtSecurityToken(issuer,audience,bas,DateTime.Now,DateTime.Now.AddMinutes(20), new SigningCredentials
                (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret??"")),SecurityAlgorithms.HmacSha256));
            return handler.WriteToken(jwt);
        }
    }
}
