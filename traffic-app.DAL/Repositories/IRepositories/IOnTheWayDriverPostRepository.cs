using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IOnTheWayDriverPostRepository : IGenericRepository<OnTheWayDriverPost>
    {
        Task<List<OnTheWayDriverPost>> GetDriverPosts(PaginationDTO paginationDTO);
        Task<List<OnTheWayDriverPost>> GetDriverOwnPosts(PaginationDTO paginationDTO, int userId);
        Task<List<OnTheWayDriverPost>> FilterDriverPosts(OnTheWayDriverPostToFilterDTO onTheWayDriverPostToFilterDTO, PaginationDTO paginationDTO);
    }
}
