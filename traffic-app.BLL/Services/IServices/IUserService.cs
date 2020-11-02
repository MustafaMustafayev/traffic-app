using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;

namespace traffic_app.BLL.Services.IServices
{
    public interface IUserService
    {
        public Task<bool> IsUserExist(string carNumber, string phoneNumber, int? userId);
        public Task<UserToListDTO> AddUser(UserToAddDTO userToAddDTO);
        public Task<UserToListDTO> UpdateUser(UserToUpdateDTO userToUpdateDTO);
        public Task<UserToListDTO> DeleteUser(int userId);
        public Task<UserToListDTO> GetUserById(int userId);
    }
}
