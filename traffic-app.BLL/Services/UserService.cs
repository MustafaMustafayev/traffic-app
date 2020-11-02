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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserToListDTO> AddUser(UserToAddDTO userToAddDTO)
        {
            User user = _mapper.Map<User>(userToAddDTO);
            user.CreatedAt = DateTime.Now;
            return _mapper.Map<UserToListDTO>(await _userRepository.Add(user));
        }

        public async Task<UserToListDTO> DeleteUser(int userId)
        {
            User user = await _userRepository.Get(m => m.UserId == userId);
            user.DeletedAt = DateTime.Now;
            return _mapper.Map<UserToListDTO>(await _userRepository.Update(user));
        }

        public async Task<UserToListDTO> GetUserById(int userId)
        {
            return _mapper.Map<UserToListDTO>(await _userRepository.Get(m => m.UserId == userId));
        }

        public async Task<bool> IsUserExist(string carNumber, string phoneNumber, int? userId)
        {
            return await _userRepository.IsUserExist(carNumber, phoneNumber, userId);
        }

        public async Task<UserToListDTO> UpdateUser(UserToUpdateDTO userToUpdateDTO)
        {
            User user = _mapper.Map<User>(userToUpdateDTO);
            user.UpdatedAt = DateTime.Now;
            return _mapper.Map<UserToListDTO>(await _userRepository.Update(user));
        }
    }
}
