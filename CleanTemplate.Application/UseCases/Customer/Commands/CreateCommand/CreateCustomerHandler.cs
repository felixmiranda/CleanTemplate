using System;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Interfaces.Services;
using MediatR;


namespace CleanTemplate.Application.UseCases.Customer.Commands;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BaseResponse<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var customer = _mapper.Map<Domain.Entities.Customer>(request);
            await _unitOfWork.Customers.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;

            response.Message = "Customer created successfully";

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = false;
            response.Message = $"An error occurred while creating the customer: {ex.Message}";
        }
        
         return response;
    }
}
