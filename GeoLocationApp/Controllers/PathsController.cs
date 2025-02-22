using GeoLocationApp.Models.Dtos;
using GeoLocationApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace GeoLocationApp.Controllers;

[Route("/v1/public/paths")]
public class PathsController(TempDatabaseService tempDatabaseService): Controller
{
    [HttpGet]
    public async Task<IActionResult> GetPaths()
    {
        return Ok(new GetPathsResponse
        {
            Paths = await tempDatabaseService.GetPathsAsync()
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> PostPath([FromBody] PostPathsRequest request)
    {
        await tempDatabaseService.CreatePathAsync(request.PathId);
        return Ok();
    }
    
    [HttpDelete("{path}")]
    public async Task<IActionResult> DeletePath(string path)
    {
        await tempDatabaseService.DeletePathAsync(path);
        return Ok();
    }
}