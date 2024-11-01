using AutoMapper;
using Datas.DTOs;
using Datas.Entities;

namespace TodoItem.Extensions
{
    public class AppMapper : Profile
    {
        public AppMapper() 
        {
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
