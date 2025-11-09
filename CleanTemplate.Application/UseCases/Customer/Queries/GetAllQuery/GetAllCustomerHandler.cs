using System;
using AutoMapper;
using CleanTemplate.Application.Commons.Bases;
using CleanTemplate.Application.Dtos.Customer;
using CleanTemplate.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Application.UseCases.Customer.Queries.GetAllQuery;

public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, BaseResponse<IEnumerable<CustomerResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _orderingQuery;
    public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderingQuery = orderingQuery;
    }
    public async Task<BaseResponse<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CustomerResponseDto>>();
        try
        {
            var customers = _unitOfWork.Customers.GetAllQueryable();
            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        customers = customers.Where(c => c.Name.Contains(request.TextFilter!));
                        break;
                    case 2:
                        customers = customers.Where(c => c.LastName!.Contains(request.TextFilter!));
                        break;
                }
            }
            if (request.StateFilter is not null)
            {
                customers = customers.Where(c => c.State == request.StateFilter);
            }
            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                if (DateTime.TryParse(request.StartDate, out DateTime startDate) && DateTime.TryParse(request.EndDate, out DateTime endDate))
                {
                    customers = customers.Where(c => c.AuditCreateDate.Date >= startDate.Date && c.AuditCreateDate.Date <= endDate.Date.AddDays(1).Date);
                }
            }

            request.Sort ??= "Id";

            var items = _orderingQuery.Ordering(request, customers).ToListAsync(cancellationToken);

            var data = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
            response.IsSuccess = true;
            response.Data = data;
            response.TotalRecords = await customers.CountAsync(cancellationToken); //data.Count();
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
