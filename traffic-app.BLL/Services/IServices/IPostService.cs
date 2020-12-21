using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;

namespace traffic_app.BLL.Services.IServices
{
    public interface IPostService
    {
        Task<int> CreatePost(PostToAddDTO postToAddDTO, int ownerUserId);
        Task UpdatePost(PostToUpdateDTO postToUpdateDTO);
        Task<int> DeletePost(int postId);
        Task<List<PostToListDTO>> GetPostList(int userId);
        Task<List<PostToListDTO>> GetUserPostList(int userId);
    }
}
