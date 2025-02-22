using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeoLocationApp.Models;
using GeoLocationApp.Service;

namespace GeoLocationApp.Controllers;

public class HomeController(TempDatabaseService tempDatabaseService) : Controller
{

    public async Task<IActionResult> Index()
    {
        var data = await tempDatabaseService.GetPathsAsync();
        ViewData["paths"] = data;
        return View();
    }

    [Route("/paths/{id}")]
    public async Task<IActionResult> Path(string id)
    {
        var data = await tempDatabaseService.GetPointsAsync(id);
        ViewData["points"] = data.Select(x => new List<decimal>{x.X, x.Y});
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}