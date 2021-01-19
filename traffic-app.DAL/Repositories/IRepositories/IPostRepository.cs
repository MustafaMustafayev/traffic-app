using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostList(PaginationDTO paginationDTO);
        Task<List<Post>> GetUserPostList(int userId, PaginationDTO paginationDTO);
        Task<List<Post>> GetSearchedPostList(string carNumber, PaginationDTO paginationDTO);
    }
}
