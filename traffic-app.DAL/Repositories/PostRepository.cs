using Microsoft.EntityFrameworkCore;
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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly TrafficDbContext _trafficDbContext;
        public PostRepository(TrafficDbContext trafficDbContext) : base(trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }

        public async Task<List<Post>> GetPostList()
        {
            return await _trafficDbContext.Posts.ToListAsync();
        }

        public async Task<List<Post>> GetUserPostList(int userId)
        {
            return await _trafficDbContext.Posts.Where(m => m.Owner == userId).ToListAsync();
        }
    }
}
