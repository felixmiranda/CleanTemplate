using System;
using AutoMapper;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Dtos.Customer;
using CleanTemplate.Application.Interfaces.Services;
using MediatR;

namespace CleanTemplate.Application.UseCases.Customer.Queries.GetAllQuery;

public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, BaseResponse<IEnumerable<CustomerResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BaseResponse<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CustomerResponseDto>>();
        try
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            if (customers is null)
            {
                response.IsSuccess = false;
                response.Message = "No customers found";
                return response;
            }

            var data = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
            response.IsSuccess = true;
            response.Data = data;
            response.TotalRecords = data.Count();
            response.Message = "Customers retrieved successfully";

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            return response;
        }
        return response;
    }
}
