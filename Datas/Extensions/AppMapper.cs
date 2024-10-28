using AutoMapper;
using Datas.DTOs;
using Datas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Extensions
{
    public class AppMapper : Profile
    {
        public AppMapper() 
        {
            CreateMap<Item, ItemDTO>().ReverseMap();
        }
    }
}
