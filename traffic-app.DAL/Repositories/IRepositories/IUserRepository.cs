using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> IsUserExist(string phoneNumber, string password, int? userId);
        Task<User> UpdateUser(User user);
    }
}
