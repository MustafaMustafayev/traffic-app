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

        public async Task<bool> IsUserExist(string phoneNumber, string password, int? userId)
        {
            return await _trafficDbContext.Users.AnyAsync(m => m.PhoneNumber == phoneNumber && m.Password == password && m.UserId != userId);
        }

        public async Task<User> UpdateUser(User user)
        {
            var userTracker = _trafficDbContext.Set<User>().Local.FirstOrDefault(entry => entry.UserId.Equals(user.UserId));
            if (userTracker != null)
            {
                _trafficDbContext.Entry(userTracker).State = EntityState.Detached;
            }
            user.UpdatedAt = DateTime.Now;
            _trafficDbContext.Entry(user).State = EntityState.Modified;
            _trafficDbContext.Entry(user).Property("Password").IsModified = false;
            _trafficDbContext.Entry(user).Property("CreatedAt").IsModified = false;
            await _trafficDbContext.SaveChangesAsync();
            return user;
        }
    }
}
