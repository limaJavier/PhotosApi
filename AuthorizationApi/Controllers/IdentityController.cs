using AuthorizationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private const string TokenSecret = "Password should not be here";
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(2);

        [HttpGet(Name = "GenerateToken")]
        public ActionResult<string> GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            //var claims = new List<Claim>
            //{
            //    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new(JwtRegisteredClaimNames.Sub, request.Email),
            //    new(JwtRegisteredClaimNames.Email, request.Email),
            //    new("userid", request.UserId.ToString()),
            //};

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.Add(TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://www.audience.com",
                Issuer = "https://www.issuer.com"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);
        }
    }
}
