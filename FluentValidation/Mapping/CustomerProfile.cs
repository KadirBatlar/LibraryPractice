using AutoMapper;
using FluentValidationApp.DTOs;
using FluentValidationApp.Models;

namespace FluentValidationApp.Mapping
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            //two-way mapping
            CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email))
            .ReverseMap();
        }
    }
}