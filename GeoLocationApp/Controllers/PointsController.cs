using GeoLocationApp.Models.Dtos;
using GeoLocationApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace GeoLocationApp.Controllers;

[Route("/v1/public/paths")]
public class PointsController(TempDatabaseService tempDatabaseService): Controller
{
    [HttpGet("{pathId}/points")]
    public async Task<IActionResult> GetPoints(string pathId)
    {
        var result = await tempDatabaseService.GetPointsAsync(pathId);

        if (result == null)
        {
            return NotFound("No path found with {pathId} ID");
        }
        
        return Ok(new GetPointsResponse
        {
            Points = result
        });
    }
    
    [HttpDelete("{pathId}/points")]
    public async Task<IActionResult> DeletePoints(string pathId)
    {
        await tempDatabaseService.DeletePointsAsync(pathId);
        return Ok();
    }
    
    [HttpPut("{pathId}/points")]
    public async Task<IActionResult> PutPoints(string pathId, [FromBody] PutPointsRequest request)
    {
        var points = await tempDatabaseService.AddOrUpdatePointsAsync(pathId, request.Points);
        return Ok(new GetPointsResponse
        {
            Points = points
        });
    }
}