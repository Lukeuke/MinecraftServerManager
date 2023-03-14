using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Application;
using Web.Presentation.Models;

namespace Web.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["TotalAvailableMemory"] = Math.Round(DiagnosticHelper.GetRamAmount(), 1);

        var all = Process.GetProcesses().GroupBy(x => x.ProcessName);

        double sumOfAllProcesses = 0;
        foreach (var processes in all)
        {
            var procKey = processes.Key;

            foreach (var proc in processes)
            {
                if (procKey.ToLower() == "java")
                {
                }

                sumOfAllProcesses += proc.GetProcessPagedMemorySize();
            }
            
        }

        ViewData["TotalAllocatedMemory"] = Math.Round(DiagnosticHelper.ParsePagedMemorySizeToGb(sumOfAllProcesses), 1);
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}