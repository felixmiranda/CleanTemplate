using System;
using AutoMapper;
using CleanTemplate.Application.Dtos.Customer;
using CleanTemplate.Application.UseCases.Customer.Commands;
using CleanTemplate.Application.UseCases.Customer.Commands.UpdateCommand;
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

        CreateMap<Customer, CustomerByIdResponseDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
    }
}
