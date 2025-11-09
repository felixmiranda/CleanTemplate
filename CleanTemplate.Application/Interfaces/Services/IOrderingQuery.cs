using System;
using CleanTemplate.Application.Commons.Bases;

namespace CleanTemplate.Application.Interfaces.Services;

public interface IOrderingQuery
{
    IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class;
}
