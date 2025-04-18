﻿using Application.DTOs;
using AutoMapper;
using Core.Models;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyCreateDto, Currency>();

            CreateMap<City, CityDto>();
            CreateMap<CityCreateDto, City>();

            CreateMap<District, DistrictDto>();
            CreateMap<DistrictCreateDto, District>();

            CreateMap<Branch, BranchDto>();
            CreateMap<BranchCreateDto, Branch>();
        }
    }
}
