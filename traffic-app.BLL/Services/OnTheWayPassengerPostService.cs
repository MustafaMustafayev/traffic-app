using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using traffic_app.BLL.Services.IServices;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.BLL.Services
{
    public class OnTheWayPassengerPostService : IOnTheWayPassengerPostService
    {
        private readonly IOnTheWayPassengerPostRepository _onTheWayPassengerPostRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public OnTheWayPassengerPostService(IOnTheWayPassengerPostRepository onTheWayPassengerPostRepository,
                                            IMapper mapper,
                                            IUserRepository userRepository)
        {
            _onTheWayPassengerPostRepository = onTheWayPassengerPostRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task AddPassengerPost(OnTheWayPassengerPostToAddDTO onTheWayPassengerPostToAddDTO, int userId)
        {
            OnTheWayPassengerPost onTheWayPassengerPost = _mapper.Map<OnTheWayPassengerPost>(onTheWayPassengerPostToAddDTO);
            onTheWayPassengerPost.UserId = userId;
            onTheWayPassengerPost.CreatedAt = DateTime.Now;
            onTheWayPassengerPost.UpdatedAt = DateTime.Now;
            await _onTheWayPassengerPostRepository.Add(onTheWayPassengerPost);
        }

        public async Task DeletePassengerPost(int postId)
        {
            OnTheWayPassengerPost onTheWayPassengerPost = await _onTheWayPassengerPostRepository.Get(m => m.OnTheWayPassengerPostId == postId);
            onTheWayPassengerPost.DeletedAt = DateTime.Now;
            onTheWayPassengerPost.UpdatedAt = DateTime.Now;
            onTheWayPassengerPost.IsDeleted = true;
            await _onTheWayPassengerPostRepository.Update(onTheWayPassengerPost);
        }

        public async Task<List<OnTheWayPassengerPostToListDTO>> FilterPassengerPosts(OnTheWayPassengerPostToFilterDTO onTheWayPassengerPostToFilterDTO, PaginationDTO paginationDTO, int userId)
        {
            List<OnTheWayPassengerPost> passengerPosts = await _onTheWayPassengerPostRepository.FilterPassengerPosts(onTheWayPassengerPostToFilterDTO, paginationDTO);
            List<OnTheWayPassengerPostToListDTO> onTheWayPassengerPostToListDTOs = _mapper.Map<List<OnTheWayPassengerPostToListDTO>>(passengerPosts);

            foreach (OnTheWayPassengerPostToListDTO post in onTheWayPassengerPostToListDTOs)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.UserId));
                if (post.UserId == userId)
                {
                    post.IsOwner = true;
                }
            }
            return onTheWayPassengerPostToListDTOs;
        }

        public async Task<List<OnTheWayPassengerPostToListDTO>> GetPassengerOwnPosts(PaginationDTO paginationDTO, int userId)
        {
            List<OnTheWayPassengerPost> passengerPosts = await _onTheWayPassengerPostRepository.GetPassengerOwnPosts(paginationDTO, userId);
            List<OnTheWayPassengerPostToListDTO> onTheWayPassengerPostToListDTOs = _mapper.Map<List<OnTheWayPassengerPostToListDTO>>(passengerPosts);

            foreach (OnTheWayPassengerPostToListDTO post in onTheWayPassengerPostToListDTOs)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.UserId));
                if (post.UserId == userId)
                {
                    post.IsOwner = true;
                }
            }
            return onTheWayPassengerPostToListDTOs;
        }

        public async Task<OnTheWayPassengerPostToListDTO> GetPassengerPostById(int postId)
        {
            OnTheWayPassengerPost onTheWayPassengerPost = await _onTheWayPassengerPostRepository.Get(m => m.OnTheWayPassengerPostId == postId);
            return _mapper.Map<OnTheWayPassengerPostToListDTO>(onTheWayPassengerPost);
        }

        public async Task<List<OnTheWayPassengerPostToListDTO>> GetPassengerPosts(PaginationDTO paginationDTO, int userId)
        {
            List<OnTheWayPassengerPost> passengerPosts = await _onTheWayPassengerPostRepository.GetPassengerPosts(paginationDTO);
            List<OnTheWayPassengerPostToListDTO> onTheWayPassengerPostToListDTOs = _mapper.Map<List<OnTheWayPassengerPostToListDTO>>(passengerPosts);

            foreach (OnTheWayPassengerPostToListDTO post in onTheWayPassengerPostToListDTOs)
            {
                post.PostedBy = _mapper.Map<PostedByDTO>(await _userRepository.Get(m => m.UserId == post.UserId));
                if (post.UserId == userId)
                {
                    post.IsOwner = true;
                }
            }
            return onTheWayPassengerPostToListDTOs;
        }

        public async Task UpdatePasssengerPost(OnTheWayPassengerPostToUpdateDTO onTheWayPassengerPostToUpdateDTO, int userId)
        {
            OnTheWayPassengerPost onTheWayPassengerPost = _mapper.Map<OnTheWayPassengerPost>(onTheWayPassengerPostToUpdateDTO);
            onTheWayPassengerPost.UserId = userId;
            onTheWayPassengerPost.UpdatedAt = DateTime.Now;
            await _onTheWayPassengerPostRepository.Update(onTheWayPassengerPost);
        }
    }
}
