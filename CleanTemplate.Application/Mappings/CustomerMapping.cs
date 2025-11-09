using System;
using AutoMapper;
using CleanTemplate.Application.Dtos.Customer;
using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Mappings;

public class CustomerMapping : Profile
{
    public CustomerMapping()
    {
        CreateMap<Customer, CustomerResponseDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StateCustomer, opt => opt.MapFrom(src => src.State == 1 ? "Active" : "Inactive"))
            .ReverseMap();
    }
}
