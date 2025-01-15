﻿using AutoMapper;
using NzWalks.API.Dtos.Region;
using NzWalks.MODEL;

namespace NzWalks.API.Utils
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionToCreateDto, Region>().ReverseMap();
            CreateMap<Region, RegionToDisplayDto>().ReverseMap();
            CreateMap<RegionToUpdateDto, Region>().ReverseMap();
        }
    }
}
