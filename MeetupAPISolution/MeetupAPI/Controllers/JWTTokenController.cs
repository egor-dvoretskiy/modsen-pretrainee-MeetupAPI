using AutoMapper;
using MeetupAPI.Data;
using MeetupAPI.Data.Repositories.Interfaces;
using MeetupAPI.DTOs.Account;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public JWTTokenController(IConfiguration configuration, IMapper mapper, IUserRepository repository)
        {
            this._configuration = configuration;
            this._userRepository = repository;
            this._mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UserDTO userDTO)
        {
            if (userDTO != null && !string.IsNullOrEmpty(userDTO.Password) && !string.IsNullOrEmpty(userDTO.UserName))
            {
                var userModel = (await this._userRepository.GetAll()).FirstOrDefault(u => u.UserName == userDTO.UserName && u.Password == userDTO.Password) ?? new UserModel();
                
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<UserDTO> GetUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new UserDTO();
            }
            
            var userModel = (await this._userRepository.GetAll()).FirstOrDefault(u => u.UserName == username && u.Password == password);
            var userDTO  = this._mapper.Map<UserDTO>(userModel);

            return userDTO;
        }
    }
}
