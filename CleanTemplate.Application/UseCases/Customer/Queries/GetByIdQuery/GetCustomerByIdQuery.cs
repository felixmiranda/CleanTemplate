using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Dtos.Customer;
using MediatR;

namespace CleanTemplate.Application.UseCases.Customer.Queries.GetByIdQuery;

public class GetCustomerByIdQuery : IRequest<BaseResponse<CustomerByIdResponseDto>>
{
    public int CustomerId { get; set; }
}
