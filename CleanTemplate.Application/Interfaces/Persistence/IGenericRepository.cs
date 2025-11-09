using System;
using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T: BaseEntity
{
    IQueryable<T> GetAllQueryable();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task <bool> CreateAsync(T entity);
    Task <bool> UpdateAsync(T entity);
    Task <bool> DeleteAsync(int id);
}
