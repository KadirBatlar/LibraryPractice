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
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}