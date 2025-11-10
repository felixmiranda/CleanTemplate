using System;
using CleanTemplate.Application.Commons.Bases;
using MediatR;

namespace CleanTemplate.Application.UseCases.Customer.Commands.UpdateCommand;

public class UpdateCustomerCommand : IRequest<BaseResponse<bool>>
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Address { get; set; } 
    public string? City { get; set; } 
}
