using System;
using AutoMapper;
using pindogramApp.Dtos;
using pindogramApp.Entities;


namespace pindogramApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Meme, MemeDto>()
                .ForMember(d => d.Image, o => o.MapFrom(s => Convert.ToBase64String(s.Image)));
            CreateMap<MemeDto, Meme>()
                .ForMember(d => d.Image, o => o.MapFrom(s => Convert.FromBase64String(s.Image)));
        }
    }
}
