using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using traffic_app.DTO;

namespace traffic_app.BLL.Services.IServices
{
    public interface IOnTheWayPassengerPostService
    {
        Task<List<OnTheWayPassengerPostToListDTO>> GetPassengerPosts(PaginationDTO paginationDTO, int userId);
        Task<List<OnTheWayPassengerPostToListDTO>> FilterPassengerPosts(OnTheWayPassengerPostToFilterDTO onTheWayPassengerPostToFilterDTO, PaginationDTO paginationDTO, int userId);
        Task<List<OnTheWayPassengerPostToListDTO>> GetPassengerOwnPosts(PaginationDTO paginationDTO, int userId);
        Task<OnTheWayPassengerPostToListDTO> GetPassengerPostById(int postId);
        Task AddPassengerPost(OnTheWayPassengerPostToAddDTO onTheWayPassengerPostToAddDTO, int userId);
        Task UpdatePasssengerPost(OnTheWayPassengerPostToUpdateDTO onTheWayPassengerPostToUpdateDTO, int userId);
        Task DeletePassengerPost(int postId);
    }
}
