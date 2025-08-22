// File: src/BrahmsLab.DataAccess/Repositories/GenericRepository.cs
using BrahmsLab.Core.Interfaces;
using BrahmsLab.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BrahmsLab.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly BrahmsLabDbContext _context;

    public GenericRepository(BrahmsLabDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}