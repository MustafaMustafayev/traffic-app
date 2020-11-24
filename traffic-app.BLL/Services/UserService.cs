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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserElasticsearchService _userElasticsearchService;
        private readonly IUtil _util;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IUserElasticsearchService userElasticsearchService, IMapper mapper, IUtil util)
        {
            _userRepository = userRepository;
            _userElasticsearchService = userElasticsearchService;
            _util = util;
            _mapper = mapper;
        }

        public async Task<UserToListDTO> AddUser(UserToAddDTO userToAddDTO)
        {
            User user = _mapper.Map<User>(userToAddDTO);
            user.CreatedAt = DateTime.Now;
            user.Password = _util.GetHash(userToAddDTO.Password);
            UserToListDTO userToListDTO = _mapper.Map<UserToListDTO>(await _userRepository.Add(user));
            UserElasticsearchDTO userElasticsearchDTO = new UserElasticsearchDTO()
            {
                Id = userToListDTO.UserId,
                Name = userToListDTO.Name,
                Surname = userToListDTO.Surname,
                PhoneNumber = userToListDTO.PhoneNumber,
                CarNumber = userToListDTO.CarNumber,
                UserMail = userToListDTO.UserMail
            };
            _userElasticsearchService.AddDataToIndices(userElasticsearchDTO);
            return userToListDTO;
        }

        public async Task<bool> ChangePassword(UserChangePasswordDTO userChangePasswordDTO)
        {
            string errorMessage = string.Empty;
            try
            {
                User user =  await _userRepository.Get(m => m.UserId == userChangePasswordDTO.UserId);
                if (await IsUserExist(user.PhoneNumber, _util.GetHash(userChangePasswordDTO.Password), user.UserId))
                {
                    errorMessage = ErrorCodes.UserIsExist;
                    throw new Exception(ErrorCodes.UserIsExist);
                }
                user.Password = _util.GetHash(userChangePasswordDTO.Password);
                user.UpdatedAt = DateTime.Now;
                await _userRepository.Update(user);
                return true;
            }
            catch
            {
                throw new Exception(errorMessage);
            }
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

        public Task<bool> IsUserExist(string phoneNumber, string password, int? userId)
        {
           // password = _util.GetHash(password);
            return _userRepository.IsUserExist(phoneNumber, password, userId);
        }

        public async Task<UserToListDTO> UpdateUser(UserToUpdateDTO userToUpdateDTO)
        {
            User user = await _userRepository.Get(m => m.UserId == userToUpdateDTO.UserId);
            if (await IsUserExist(userToUpdateDTO.PhoneNumber, user.Password, userToUpdateDTO.UserId))
            {
                throw new Exception(ErrorCodes.UserIsExist);
            }
            user = _mapper.Map<User>(userToUpdateDTO);
            User updatedUser = await _userRepository.UpdateUser(user);
            UserElasticsearchDTO userElasticsearchDTO = new UserElasticsearchDTO()
            {
                Id = updatedUser.UserId,
                Name = updatedUser.Name,
                Surname = updatedUser.Surname,
                PhoneNumber = updatedUser.PhoneNumber,
                CarNumber = updatedUser.CarNumber,
                UserMail = updatedUser.UserMail
            };
            _userElasticsearchService.UpdateIndicesData(userElasticsearchDTO);
            return _mapper.Map<UserToListDTO>(updatedUser);
        }
    }
}
