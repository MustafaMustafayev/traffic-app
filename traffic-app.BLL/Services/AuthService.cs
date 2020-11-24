using AutoMapper;
using System;
using System.Collections.Generic;
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
        public AuthService(IUserRepository userRepository, IMapper mapper, IUtil util)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _util = util;
        }

        public async Task<UserToListDTO> Login(LoginDTO loginDTO)
        {
            loginDTO.Password = _util.GetHash(loginDTO.Password);
            User user = await _userRepository.Get(m => m.PhoneNumber == loginDTO.PhoneNumber && m.Password == loginDTO.Password);
            return _mapper.Map<UserToListDTO>(user);
        }
    }
}
