using MeetupAPI.Data;
using MeetupAPI.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _userDbContext;

        public JWTTokenController(IConfiguration configuration, UserDbContext context)
        {
            this._configuration = configuration;
            this._userDbContext = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UserModel userModel)
        {
            if (userModel != null && !string.IsNullOrEmpty(userModel.Password) && !string.IsNullOrEmpty(userModel.UserName))
            {
                var userData = await GetUser(userModel.UserName, userModel.Password);
                var jwt = this._configuration.GetSection("Jwt").Get<Jwt>();

                var claims = new[]
                {
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", userModel.UserId.ToString()),
                    new Claim("UserName", userModel.UserName),
                    new Claim("Password", userModel.Password),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signin
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<UserModel> GetUser(string username, string password)
        {
            return await this._userDbContext.UserModels.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
