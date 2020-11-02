using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.BLL.Services.IServices;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserToListDTO> Login(LoginDTO loginDTO)
        {
            User user = await _userRepository.Get(m => m.CarNumber == loginDTO.CarNumber && m.PhoneNumber == loginDTO.PhoneNumber);
            return _mapper.Map<UserToListDTO>(user);
        }
    }
}
