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
using Users.Core.Interfaces;

public abstract class JsonFileRepository<T> : IGenericRepository<T> where T : class, IAddable
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
        return data.FirstOrDefault(item => item.Id == id);
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
}
