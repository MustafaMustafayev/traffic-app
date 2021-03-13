using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories
{
    public class OnTheWayPassengerPostRepository : GenericRepository<OnTheWayPassengerPost>, IOnTheWayPassengerPostRepository
    {
        private readonly TrafficDbContext _trafficDbContext;
        public OnTheWayPassengerPostRepository(TrafficDbContext trafficDbContext) : base(trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }

        public async Task<List<OnTheWayPassengerPost>> FilterPassengerPosts(OnTheWayPassengerPostToFilterDTO onTheWayPassengerPostToFilterDTO, PaginationDTO paginationDTO)
        {
            return await _trafficDbContext.OnTheWayPassengerPosts.Where(m =>
                                                                    (!string.IsNullOrEmpty(onTheWayPassengerPostToFilterDTO.FromPlace) ? onTheWayPassengerPostToFilterDTO.FromPlace.ToLower().Contains(m.FromPlace.ToLower()) : true) &&
                                                                    (!string.IsNullOrEmpty(onTheWayPassengerPostToFilterDTO.ToPlace) ? onTheWayPassengerPostToFilterDTO.ToPlace.ToLower().Contains(m.ToPlace.ToLower()) : true) &&
                                                                    (onTheWayPassengerPostToFilterDTO.StartDate != null ? onTheWayPassengerPostToFilterDTO.StartDate <= m.StartDate : true))
                                                                   .OrderByDescending(m => m.UpdatedAt).Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
                                                                   .Take(paginationDTO.PageSize).ToListAsync();
        }

        public async Task<List<OnTheWayPassengerPost>> GetPassengerOwnPosts(PaginationDTO paginationDTO, int userId)
        {
            return await _trafficDbContext.OnTheWayPassengerPosts.Where(m =>
                                                                    m.UserId == userId).OrderByDescending(m => m.UpdatedAt).Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
                                                                   .Take(paginationDTO.PageSize).ToListAsync();
        }

        public async Task<List<OnTheWayPassengerPost>> GetPassengerPosts(PaginationDTO paginationDTO)
        {
            return await _trafficDbContext.OnTheWayPassengerPosts.OrderByDescending(m => m.UpdatedAt).Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
                                                        .Take(paginationDTO.PageSize).ToListAsync();
        }
    }
}
