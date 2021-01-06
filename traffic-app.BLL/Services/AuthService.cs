using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUtil _util;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, IMapper mapper, IUtil util, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _util = util;
            _configuration = configuration;
        }

        public bool IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:SecretKey").Value);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                // return true from JWT token if validation successful
                return true;
            }
            catch (Exception ex)
            {
                // return false if validation fails
                return false;
            }
        }

        public async Task<UserToListDTO> Login(LoginDTO loginDTO)
        {
            loginDTO.Password = _util.GetHash(loginDTO.Password);
            User user = await _userRepository.Get(m => m.PhoneNumber == loginDTO.PhoneNumber && m.Password == loginDTO.Password);
            return _mapper.Map<UserToListDTO>(user);
        }
    }
}
