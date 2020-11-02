using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DTO;

namespace traffic_app.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IUserService userService, IConfiguration configuration)
        {
            _authService = authService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                UserToListDTO userToListDTO = await _authService.Login(loginDTO);
                if(userToListDTO == null)
                {
                    return BadRequest(Messages.LoginFailed);
                }
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userToListDTO.UserId.ToString()));

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:SecretKey").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenValue = tokenHandler.WriteToken(token);

                LoginResponseDTO loginResponse = new LoginResponseDTO()
                {
                    User = userToListDTO,
                    Token = tokenValue
                };
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToAddDTO userToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }   
                if(await _userService.IsUserExist(userToAddDTO.CarNumber, userToAddDTO.PhoneNumber, null))
                {
                    return BadRequest(Messages.UserIsExist);
                }
                return Ok(await _userService.AddUser(userToAddDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
