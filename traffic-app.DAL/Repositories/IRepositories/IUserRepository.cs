using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> IsUserExist(string carNumber, string phoneNumber, int? userId);
    }
}
