using GeoLocationApp.Service;

namespace GeoLocationApp.Models.Dtos;

public class PutPointsRequest
{
    public IEnumerable<Point> Points { get; set; }
}