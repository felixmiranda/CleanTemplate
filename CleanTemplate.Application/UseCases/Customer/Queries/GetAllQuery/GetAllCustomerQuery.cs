using System;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Dtos.Customer;
using MediatR;

namespace CleanTemplate.Application.UseCases.Customer.Queries.GetAllQuery;

public class GetAllCustomerQuery: IRequest<BaseResponse<IEnumerable<CustomerResponseDto>>>
{

}
