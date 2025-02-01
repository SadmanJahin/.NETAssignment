using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text.Json;
using Users.DataAccess.Interfaces;
using Users.DataAccess.Data;
using Users.Core.Entities;

public abstract class JsonFileRepository<T> : IGenericRepository<T> where T : class
{
    private readonly JsonContext _jsonContext;
    private List<T> data;
    public JsonFileRepository(JsonContext jsonContext)
    {
        _jsonContext = jsonContext;
    }

    public async Task<T> GetAsync(long id)
    {
        var data = await _jsonContext.LoadDataAsync<T>();
        return data.FirstOrDefault(item => item.GetHashCode() == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _jsonContext.LoadDataAsync<T>();
    }

    public async Task SaveAsync(T entity)
    {
        await _jsonContext.AddAsync<T>(entity);
    }

    public async Task UpdateAsync(T entity)
    {
       await _jsonContext.UpdateAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
       await _jsonContext.DeleteAsync(entity);
    }

    public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
    {
        var data = await _jsonContext.LoadDataAsync<T>();
        return data.AsQueryable().Where(predicate).ToList();
    }

    public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, bool sortDescending = true)
    {
        var data = await _jsonContext.LoadDataAsync<T>();
        var query = data.AsQueryable().Where(predicate);

        query = sortDescending
            ? query.OrderByDescending(orderBy)
            : query.OrderBy(orderBy);

        return query.ToList();
    }

    public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var data = await _jsonContext.LoadDataAsync<T>();
        return data.AsQueryable().Where(predicate).ToList();
    }

    public async Task<long> Count(Expression<Func<T, bool>> predicate)
    {
        var data = await _jsonContext.LoadDataAsync<T>();
        return data.AsQueryable().Count(predicate);
    }
}
