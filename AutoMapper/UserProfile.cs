using AutoMapper;
using WeeklyProgram.Dtos;
using WeeklyProgram.Models;
namespace SonmezERP.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto,AppUser>().ReverseMap();
            CreateMap<AppUser,UserAddDto>().ReverseMap();
        }
    }
}