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
    public class PostImageService : IPostImageService
    {
        private readonly IPostImageRepository _postImageRepository;
        private readonly IMapper _mapper;
        public PostImageService(IPostImageRepository postImageRepository, IMapper mapper)
        {
            _postImageRepository = postImageRepository;
            _mapper = mapper;
        }

        public async Task AddPostImage(PostImageToAddDTO postImageToAddDTO)
        {
            PostImage postImage = _mapper.Map<PostImage>(postImageToAddDTO);
            postImage.CreatedAt = DateTime.Now;
            await _postImageRepository.Add(postImage);
        }

        public async Task<int> DeletePostImage(int postImageId)
        {
            PostImage postImage = await _postImageRepository.Get(m => m.PostImageId == postImageId);
            postImage.DeletedAt = DateTime.Now;
            await _postImageRepository.Update(postImage);
            return postImageId;
        }
    }
}
