﻿using System.Linq.Expressions;
using ProjectTemplate.Domain.Interfaces.Repositories;
using ProjectTemplate.Domain.Interfaces.Services;

namespace ProjectTemplate.Domain.Services;

public abstract class DomainService<TEntity, TKeyType> : IDomainService<TEntity, TKeyType>
    where TEntity : class
{
    private readonly IRepositoryBase<TEntity> _repository;

    protected DomainService(IRepositoryBase<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity?> AddAsync(TEntity obj)
    {
        return await _repository.AddAsync(obj);
    }

    public async Task<bool> RemoveAsync(TEntity obj)
    {
        return await _repository.RemoveAsync(obj);
    }

    public async Task<bool> RemoveByIdAsync(TKeyType Id)
    {
        return await _repository.RemoveByIdAsync(Id);
    }

    public async Task<IEnumerable<TEntity>>
        FindAsync(Expression<Func<TEntity, bool>>? predicate = null,
            bool pagination = false,
            int pageNumber = 1,
            int pageSize = 10,
            params Expression<Func<TEntity, object>>[] includes)
    {
        return await _repository.FindAsync(predicate, pagination, pageNumber, pageSize, includes);
    }

    public async Task<TEntity?> GetByIdAsync(TKeyType Id)
    {
        return await _repository.GetByIdAsync(Id);
    }

    public async Task<TEntity?> UpdateAsync(TEntity obj)
    {
        return await _repository.UpdateAsync(obj);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return await _repository.CountAsync(predicate);
    }
}