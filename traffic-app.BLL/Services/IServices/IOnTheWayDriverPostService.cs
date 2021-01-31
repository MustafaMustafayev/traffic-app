using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;

namespace traffic_app.BLL.Services.IServices
{
    public interface IOnTheWayDriverPostService
    {
        Task<List<OnTheWayDriverPostToListDTO>> GetDriverPosts(PaginationDTO paginationDTO, int userId);
        Task<List<OnTheWayDriverPostToListDTO>> FilterDriverPosts(OnTheWayDriverPostToFilterDTO onTheWayDriverPostToFilterDTO, PaginationDTO paginationDTO, int userId);
        Task<List<OnTheWayDriverPostToListDTO>> GetDriverOwnPosts(PaginationDTO paginationDTO, int userId);
        Task<OnTheWayDriverPostToListDTO> GetDriverPostById(int postId);
        Task AddDriverPost(OnTheWayDriverPostToAddDTO onTheWayDriverPostToAddDTO, string filePath, int userId);
        Task UpdateDriverPost(OnTheWayDriverPostToUpdateDTO onTheWayDriverPostToUpdateDTO, string filePath, int userId);
        Task DeleteDriverPost(int postId);
    }
}
