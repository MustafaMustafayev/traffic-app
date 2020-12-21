using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostList();
        Task<List<Post>> GetUserPostList(int userId);
    }
}
