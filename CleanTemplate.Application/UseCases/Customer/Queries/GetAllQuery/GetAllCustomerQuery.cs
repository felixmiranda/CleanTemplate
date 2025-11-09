using System;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Dtos.Customer;
using MediatR;

namespace CleanTemplate.Application.UseCases.Customer.Queries.GetAllQuery;

public class GetAllCustomerQuery: BaseFilters, IRequest<BaseResponse<IEnumerable<CustomerResponseDto>>>
{

}
