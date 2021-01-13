using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.BLL.Services.IServices;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> CreatePost(PostToAddDTO postToAddDTO, int ownerUserId)
        {
            Post post = _mapper.Map<Post>(postToAddDTO);
            post.PostText = post.PostText.Trim();
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            post.Owner = ownerUserId;
            List<PostUser> postUsers = new List<PostUser>()
                {
                    new PostUser()
                    {
                        UserId = ownerUserId,
                        IsOwner = true
                    }
                };
            post.PostUsers = postUsers;
            await _postRepository.Add(post);
            return post.PostId;
        }

        public async Task<int> DeletePost(int postId)
        {
            Post post = await _postRepository.Get(m => m.PostId == postId);
            post.DeletedAt = DateTime.Now;
            await _postRepository.Update(post);
            return post.PostId;
        }

        public async Task<List<PostToListDTO>> GetPostList(int userId, PaginationDTO paginationDTO)
        {
            List<Post> posts = await _postRepository.GetPostList(paginationDTO);
            List<PostToListDTO> postLists = _mapper.Map<List<PostToListDTO>>(posts);
            foreach(PostToListDTO post in postLists)
            {
                post.PostedBy =  _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.Owner));
                if(post.Owner == userId)
                {
                    post.isOwner = true;
                }
            }
            return postLists;
        }

        public async Task<List<PostToListDTO>> GetUserPostList(int userId, PaginationDTO paginationDTO)
        {
            List<Post> posts = await _postRepository.GetUserPostList(userId, paginationDTO);
            List<PostToListDTO> postLists = _mapper.Map<List<PostToListDTO>>(posts);
            foreach (PostToListDTO post in postLists)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.Owner));
                if (post.Owner == userId)
                {
                    post.isOwner = true;
                }
            }
            return postLists;
        }

        public async Task<List<PostToListDTO>> SearchPostByCarNumber(string carNumber)
        {
            List<Post> posts = await _postRepository.GetList(m => m.PostText.ToLower().Contains(carNumber.ToLower()) == true);
            return _mapper.Map<List<PostToListDTO>>(posts);
        }

        public async Task UpdatePost(PostToUpdateDTO postToUpdateDTO)
        {
            Post post = await _postRepository.Get(m => m.PostId == postToUpdateDTO.PostId);
            post.PostText = postToUpdateDTO.PostText;
            post.UpdatedAt = DateTime.Now;
            await _postRepository.Update(post);
        }
    }
}
