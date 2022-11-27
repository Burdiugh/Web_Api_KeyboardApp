
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
       
            CreateMap<IdentityRole, string>().ConvertUsing(r=>r.Name);

            CreateMap<AppUser, AppUserDTO>()
                //.ForMember(dest => dest.Scores,
                //opt=>opt.MapFrom(src => src.Scores))
                .ReverseMap();

            CreateMap<AppScore, ScoreDTO>()
               //.ForMember(dest => dest.Scores,
               //opt=>opt.MapFrom(src => src.Scores))
               .ReverseMap();

            CreateMap<AppText, AppTextDTO>()
               .ForMember(dest => dest.LevelName,
                           opt => opt.MapFrom(src => src.Level.Name))
               .ForMember(dest => dest.LanguageName,
                           opt => opt.MapFrom(src => src.Language.Name));

           



        }
    }
}
