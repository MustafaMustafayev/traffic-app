using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.Core.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserToListDTO>()
                     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")))
                     .ReverseMap();

            CreateMap<User, UserToAddDTO>().ReverseMap();
            CreateMap<User, UserToUpdateDTO>().ReverseMap();
            CreateMap<User, PostedByDTO>().ReverseMap();

            CreateMap<Post, PostToAddDTO>().ReverseMap();
            CreateMap<PostImage, PostImageToAddDTO>().ReverseMap();
            CreateMap<PostImage, PostImageToListDTO>().ReverseMap();
            CreateMap<Post, PostToListDTO>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")))
                .ReverseMap();

            CreateMap<Message, MessageToAddDTO>().ReverseMap();

            CreateMap<OnTheWayDriverPost, OnTheWayDriverPostToAddDTO>().ReverseMap();
            CreateMap<OnTheWayDriverPost, OnTheWayDriverPostToUpdateDTO>().ReverseMap();

            CreateMap<OnTheWayDriverPost, OnTheWayDriverPostToListDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy HH:mm:ss")))
                .ReverseMap();

            CreateMap<OnTheWayPassengerPost, OnTheWayPassengerPostToAddDTO>().ReverseMap();
            CreateMap<OnTheWayPassengerPost, OnTheWayPassengerPostToUpdateDTO>().ReverseMap();

            CreateMap<OnTheWayPassengerPost, OnTheWayPassengerPostToListDTO>()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy HH:mm:ss")))
               .ReverseMap();

            CreateMap<OnTheWayPassengerPost, OnTheWayPassengerPostToListDTO>()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy HH:mm:ss")))
               .ReverseMap();
        }
    }
}
