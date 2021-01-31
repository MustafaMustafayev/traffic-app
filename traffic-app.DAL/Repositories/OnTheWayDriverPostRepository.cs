using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.DAL.Repositories
{
    public class OnTheWayDriverPostRepository : GenericRepository<OnTheWayDriverPost>, IOnTheWayDriverPostRepository
    {
        private readonly TrafficDbContext _trafficDbContext;
        public OnTheWayDriverPostRepository(TrafficDbContext trafficDbContext) : base(trafficDbContext)
        {
            _trafficDbContext = trafficDbContext;
        }

        public async Task<List<OnTheWayDriverPost>> FilterDriverPosts(OnTheWayDriverPostToFilterDTO onTheWayDriverPostToFilterDTO, PaginationDTO paginationDTO)
        {
            return await _trafficDbContext.OnTheWayDriverPosts.Where(m => 
                                                                    onTheWayDriverPostToFilterDTO.FromPlace.ToLower().Contains(m.FromPlace.ToLower()) &&
                                                                    onTheWayDriverPostToFilterDTO.ToPlace.ToLower().Contains(m.ToPlace.ToLower()) &&
                                                                    onTheWayDriverPostToFilterDTO.StartDate <= m.StartDate)
                                                                   .OrderByDescending(m => m.UpdatedAt).Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
                                                                   .Take(paginationDTO.PageSize).ToListAsync();
        }

        public async Task<List<OnTheWayDriverPost>> GetDriverOwnPosts(PaginationDTO paginationDTO, int userId)
        {
            return await _trafficDbContext.OnTheWayDriverPosts.Where(m => m.UserId == userId).OrderByDescending(m => m.UpdatedAt).Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
                                                                   .Take(paginationDTO.PageSize).ToListAsync();
        }

        public async Task<List<OnTheWayDriverPost>> GetDriverPosts(PaginationDTO paginationDTO)
        {
            return await _trafficDbContext.OnTheWayDriverPosts.OrderByDescending(m => m.UpdatedAt).Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
                                                        .Take(paginationDTO.PageSize).ToListAsync();
        }

    }
}
