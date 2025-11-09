using System;
using CleanTemplate.Application.Interfaces.Persistence;
using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Interfaces.Services;

public interface IUnitOfWork
{
    IGenericRepository<Customer> Customers { get; }
    Task SaveChangesAsync();
    void Dispose();
}
