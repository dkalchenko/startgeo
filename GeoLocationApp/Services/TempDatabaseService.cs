using System.Collections.Concurrent;

namespace GeoLocationApp.Service;

public class TempDatabaseService
{
    private ConcurrentDictionary<string, List<Point>> _database = new();

    public async Task<IEnumerable<string>> GetPathsAsync()
    {
        return _database.Keys.ToList();
    }
    
    public async Task DeletePathAsync(string key)
    {
        _database.Remove(key, out _);
    }
    
    public async Task DeletePointsAsync(string key)
    {
        var exists = _database.TryGetValue(key, out var result);

        if (!exists)
        {
            return;
        }
        
        _database.TryUpdate(key, new List<Point>(), result);
    }
    
    public async Task CreatePathAsync(string key)
    {
        _database.AddOrUpdate(key, new List<Point>(), (s, points) => points);
    }
    
    public async Task<IEnumerable<Point>> AddOrUpdatePointsAsync(string key, IEnumerable<Point> points)
    {
        var result = _database.AddOrUpdate(key, points.ToList(), (s, p) =>
        {
            p.AddRange(points);
            return p;
        });
        
        return result;
    }
    
    public async Task<IEnumerable<Point>> GetPointsAsync(string key)
    {
        var exists = _database.TryGetValue(key, out var result);
        if (!exists || result == null)
        {
            return null;
        }

        return result;
    }
}

public class Point
{
    public decimal X { get; set; }
    public decimal Y { get; set; }
}