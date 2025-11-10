using CleanTemplate.Application.Interfaces.Persistence;
using CleanTemplate.Domain.Entities;
using CleanTemplate.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private const int ActiveState = 1;
    private const int InactiveState = 0;
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await _entity
            .Where(x => x.State == ActiveState && x.AuditDeleteUser == null && x.AuditDeleteDate == null)
            .ToListAsync();
        return response;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var response = await _entity
            .SingleOrDefaultAsync(x => x.Id == id && 
                                x.State == ActiveState && 
                                x.AuditDeleteUser == null && 
                                x.AuditDeleteDate == null);
        return response!;
    }

    public async Task CreateAsync(T entity)
    {
        SetCreateAudit(entity);
        await _context.AddAsync(entity);
        
    }

    public void UpdateAsync(T entity)
    {
        SetUpdateAudit(entity);
        _context.Update(entity);
        PreserveCreateAudit(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            throw new ArgumentException($"Entity with id {id} not found.");
            
        SetDeleteAudit(entity);
        _context.Update(entity);

    }

    private static void SetCreateAudit(BaseEntity entity)
    {
        entity.AuditCreateUser = 1; //TODO: Replace with actual user
        entity.AuditCreateDate = DateTime.UtcNow;
        entity.State = ActiveState;
    }

    private static void SetUpdateAudit(BaseEntity entity)
    {
        entity.AuditUpdateUser = 1; //TODO: Replace with actual user
        entity.AuditUpdateDate = DateTime.UtcNow;
    }

    private static void SetDeleteAudit(BaseEntity entity)
    {
        entity.AuditDeleteUser = 1; //TODO: Replace with actual user
        entity.AuditDeleteDate = DateTime.UtcNow;
        entity.State = InactiveState;
    }

    private void PreserveCreateAudit(T entity)
    {
        _context.Entry(entity).Property(x => x.AuditCreateUser).IsModified = false;
        _context.Entry(entity).Property(x => x.AuditCreateDate).IsModified = false;
        _context.Entry(entity).Property(x => x.State).IsModified = false;
    }

    public IQueryable<T> GetAllQueryable()
    {
        var response = _entity
            .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);
        return response;
    }
}
