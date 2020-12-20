using System;
using System.Collections.Generic;
using System.Text;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories
{
    public class PostImageRepository : GenericRepository<PostImage>, IPostImageRepository
    {
        private readonly TrafficDbContext _trafficDbContext;
        public PostImageRepository(TrafficDbContext trafficDbContext) : base(trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }
    }
}
