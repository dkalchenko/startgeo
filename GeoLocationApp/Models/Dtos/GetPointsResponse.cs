using GeoLocationApp.Service;

namespace GeoLocationApp.Models.Dtos;

public class GetPointsResponse
{
    public IEnumerable<Point> Points { get; set; }
}