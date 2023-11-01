﻿using Microsoft.EntityFrameworkCore;
using ProjectTemplate.Infra.Resilience.Database.Mssql.Policies;
using System.Linq.Expressions;

namespace ProjectTemplate.Infra.Data.EF.Mssql.Extension;

public static class MssqlEFExtension
{
    public static async Task<int> SaveChangesAsyncWtithRetry(this DbContext context)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        return await DatabasePolicy.asyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await context.SaveChangesAsync();
        });
    }

    public static async Task<TEntity?> GetByIdAsyncWithRetry<TEntity, KeyType>(this DbContext context, KeyType id) where TEntity : class
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        return await DatabasePolicy.asyncRetryPolicy.ExecuteAsync(async () =>
        {
            return await context.Set<TEntity>().FindAsync(id);
        });
    }

    public static async Task<IEnumerable<TEntity>> FindAsyncWith<TEntity>(this DbContext context,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includes) where TEntity : class
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        return await DatabasePolicy.asyncRetryPolicy.ExecuteAsync(async () =>
        {
            var query = context.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            query = query.Where(predicate);

            return await query.ToListAsync();
        });
    }
}