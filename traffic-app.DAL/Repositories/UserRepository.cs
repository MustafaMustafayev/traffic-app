using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly TrafficDbContext _trafficDbContext;
        public UserRepository(TrafficDbContext trafficDbContext) : base(trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }

        public async Task<bool> IsUserExist(string carNumber, string phoneNumber, int? userId)
        {
            return await _trafficDbContext.Users.AnyAsync(m => m.CarNumber == carNumber && m.PhoneNumber == phoneNumber && m.UserId != userId);
        }
    }
}
