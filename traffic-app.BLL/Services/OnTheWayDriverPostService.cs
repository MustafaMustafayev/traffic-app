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
        public OnTheWayDriverPostService(IOnTheWayDriverPostRepository onTheWayDriverPostRepository, IMapper mapper)
        {
            _onTheWayDriverPostRepository = onTheWayDriverPostRepository;
            _mapper = mapper;
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
    }
}
