using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class JsonContext
{
    private readonly string _filePath;
    private List<object> _inMemoryData; 
    private bool _isDirty; 

    public JsonContext(string filePath)
    {
        _filePath = filePath;
        _inMemoryData = new List<object>();
        _isDirty = false;
    }


    public Task AddAsync<T>(T item)
    {
        _inMemoryData.Add(item);
        _isDirty = true;
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        if (_inMemoryData.Count == 0)
            return;

        var json = JsonSerializer.Serialize(_inMemoryData, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);

        _inMemoryData.Clear();
        _isDirty = false;
    }
    public async Task<List<T>> LoadDataAsync<T>()
    {
        if (_inMemoryData.Count > 0)
        {
            return _inMemoryData.Cast<T>().ToList();
        }

        if (File.Exists(_filePath))
        {
            var json = await File.ReadAllTextAsync(_filePath);
            var loadedData = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();

       
            _inMemoryData = loadedData.Cast<object>().ToList();

            return loadedData;
        }

        return new List<T>(); 
    }

    public async Task UpdateAsync<T>(T entity)
    {
        var data = await LoadDataAsync<T>();
        var index = data.FindIndex(item => item.Equals(entity));

        if (index != -1)
        {
            _inMemoryData[index] = entity;
        }
    }

    public async Task DeleteAsync<T>(T entity)
    {
        var data = await LoadDataAsync<T>();
        var index = data.FindIndex(item => item.Equals(entity));
        _inMemoryData.Remove(entity);
    }

    public bool HasChanges()
    {
        return _isDirty;
    }
}
