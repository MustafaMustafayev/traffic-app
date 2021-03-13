using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories.IRepositories
{
    public interface IOnTheWayPassengerPostRepository : IGenericRepository<OnTheWayPassengerPost>
    {
        Task<List<OnTheWayPassengerPost>> GetPassengerPosts(PaginationDTO paginationDTO);
        Task<List<OnTheWayPassengerPost>> GetPassengerOwnPosts(PaginationDTO paginationDTO, int userId);
        Task<List<OnTheWayPassengerPost>> FilterPassengerPosts(OnTheWayPassengerPostToFilterDTO onTheWayPassengerPostToFilterDTO, PaginationDTO paginationDTO);
    }
}
