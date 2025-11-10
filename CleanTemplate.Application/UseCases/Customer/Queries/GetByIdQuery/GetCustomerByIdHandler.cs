using System;
using AutoMapper;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Dtos.Customer;
using CleanTemplate.Application.Interfaces.Services;
using MediatR;

namespace CleanTemplate.Application.UseCases.Customer.Queries.GetByIdQuery;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetCustomerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BaseResponse<CustomerByIdResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CustomerByIdResponseDto>();
        try
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
            if (customer is null)
            {
                response.IsSuccess = false;
                response.Message = "Customer not found";
                return response;
            }

            var data = _mapper.Map<CustomerByIdResponseDto>(customer);
            response.IsSuccess = true;
            response.Data = data;
            response.Message = "Customer retrieved successfully";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"An error occurred while retrieving the customer: {ex.Message}";
        }
        return response;
    }
}
