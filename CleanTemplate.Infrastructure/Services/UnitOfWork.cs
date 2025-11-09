using System;
using CleanTemplate.Application.Interfaces.Persistence;
using CleanTemplate.Application.Interfaces.Services;
using CleanTemplate.Domain.Entities;
using CleanTemplate.Infrastructure.Persistence.Context;
using CleanTemplate.Infrastructure.Persistence.Repositories;

namespace CleanTemplate.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IGenericRepository<Customer> _customer = null!;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public IGenericRepository<Customer> Customers => _customer ?? new GenericRepository<Customer>(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    
}
