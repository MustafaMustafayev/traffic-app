using System;
using System.Collections.Generic;
using System.Text;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly TrafficDbContext _trafficDbContext;
        public MessageRepository(TrafficDbContext trafficDbContext) : base(trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }
    }
}
