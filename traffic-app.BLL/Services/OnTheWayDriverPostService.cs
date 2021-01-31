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
    public class OnTheWayDriverPostService : IOnTheWayDriverPostService
    {
        private readonly IOnTheWayDriverPostRepository _onTheWayDriverPostRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public OnTheWayDriverPostService(IOnTheWayDriverPostRepository onTheWayDriverPostRepository, IMapper mapper, IUserRepository userRepository)
        { 
            _onTheWayDriverPostRepository = onTheWayDriverPostRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task AddDriverPost(OnTheWayDriverPostToAddDTO onTheWayDriverPostToAddDTO, string filePath, int userId)
        {
            OnTheWayDriverPost onTheWayDriverPost = _mapper.Map<OnTheWayDriverPost>(onTheWayDriverPostToAddDTO);
            onTheWayDriverPost.UserId = userId;
            onTheWayDriverPost.CarImageUrl = filePath;
            onTheWayDriverPost.CreatedAt = DateTime.Now;
            onTheWayDriverPost.UpdatedAt = DateTime.Now;
            await _onTheWayDriverPostRepository.Add(onTheWayDriverPost);
        }

        public async Task DeleteDriverPost(int postId)
        {
            OnTheWayDriverPost onTheWayDriverPost = await _onTheWayDriverPostRepository.Get(m => m.OnTheWayDriverPostId == postId);
            onTheWayDriverPost.DeletedAt = DateTime.Now;
            onTheWayDriverPost.UpdatedAt = DateTime.Now;
            onTheWayDriverPost.IsDeleted = true;
            await _onTheWayDriverPostRepository.Update(onTheWayDriverPost);
        }

        public async Task<List<OnTheWayDriverPostToListDTO>> FilterDriverPosts(OnTheWayDriverPostToFilterDTO onTheWayDriverPostToFilterDTO, PaginationDTO paginationDTO, int userId)
        {
            List<OnTheWayDriverPost> driverPosts = await _onTheWayDriverPostRepository.FilterDriverPosts(onTheWayDriverPostToFilterDTO, paginationDTO);
            List<OnTheWayDriverPostToListDTO> onTheWayDriverPostToListDTOs = _mapper.Map<List<OnTheWayDriverPostToListDTO>>(driverPosts);

            foreach (OnTheWayDriverPostToListDTO post in onTheWayDriverPostToListDTOs)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.UserId));
                if (post.UserId == userId)
                {
                    post.IsOwner = true;
                }
            }
            return onTheWayDriverPostToListDTOs;
        }

        public async Task<List<OnTheWayDriverPostToListDTO>> GetDriverOwnPosts(PaginationDTO paginationDTO, int userId)
        {
            List<OnTheWayDriverPost> driverPosts = await _onTheWayDriverPostRepository.GetDriverOwnPosts(paginationDTO, userId);
            List<OnTheWayDriverPostToListDTO> onTheWayDriverPostToListDTOs = _mapper.Map<List<OnTheWayDriverPostToListDTO>>(driverPosts);
            foreach (OnTheWayDriverPostToListDTO post in onTheWayDriverPostToListDTOs)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.UserId));
                if (post.UserId == userId)
                {
                    post.IsOwner = true;
                }
            }
            return onTheWayDriverPostToListDTOs;
        }

        public async Task<OnTheWayDriverPostToListDTO> GetDriverPostById(int postId)
        {
            OnTheWayDriverPost onTheWayDriverPost = await _onTheWayDriverPostRepository.Get(m => m.OnTheWayDriverPostId == postId);
            return _mapper.Map<OnTheWayDriverPostToListDTO>(onTheWayDriverPost);
        }

        public async Task<List<OnTheWayDriverPostToListDTO>> GetDriverPosts(PaginationDTO paginationDTO, int userId)
        {
            List<OnTheWayDriverPost> driverPosts = await _onTheWayDriverPostRepository.GetDriverPosts(paginationDTO);
            List<OnTheWayDriverPostToListDTO> onTheWayDriverPostToListDTOs = _mapper.Map<List<OnTheWayDriverPostToListDTO>>(driverPosts);

            foreach (OnTheWayDriverPostToListDTO post in onTheWayDriverPostToListDTOs)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.UserId));
                if (post.UserId == userId)
                {
                    post.IsOwner = true;
                }
            }

            return onTheWayDriverPostToListDTOs;
        }

        public async Task UpdateDriverPost(OnTheWayDriverPostToUpdateDTO onTheWayDriverPostToUpdateDTO, string filePath, int userId)
        {
            OnTheWayDriverPost onTheWayDriverPost = _mapper.Map<OnTheWayDriverPost>(onTheWayDriverPostToUpdateDTO);
            onTheWayDriverPost.UserId = userId;
            onTheWayDriverPost.CarImageUrl = filePath;
            onTheWayDriverPost.UpdatedAt = DateTime.Now;
            await _onTheWayDriverPostRepository.Update(onTheWayDriverPost);
        }
    }
}
