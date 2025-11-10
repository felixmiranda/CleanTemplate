using System;
using AutoMapper;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanTemplate.Application.UseCases.Customer.Commands.UpdateCommand;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BaseResponse<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var customer = _mapper.Map<Domain.Entities.Customer>(request);
            customer.Id = request.CustomerId;
            _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
                        
            response.IsSuccess = true;
            response.Message = "Customer updated successfully";

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = false;
            response.Message = $"An error occurred while updating the customer: {ex.Message}";
        }

        return response;
    }
}
