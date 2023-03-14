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

        _logger.LogInformation("======LIST OF ALL PROCESSES======");
        var all = Process.GetProcesses().GroupBy(x => x.ProcessName);

        double sumOfAllProcesses = 0;
        double javaMem = 0;
        foreach (var processes in all)
        {
            var procKey = processes.Key;

            foreach (var proc in processes)
            {
                if (procKey.ToLower() == "java")
                {

                    javaMem += proc.GetProcessMemorySize();

                    ViewData["JavaMem"] = Math.Round(DiagnosticHelper.ParsePagedMemorySizeToGb(javaMem), 1);
                }

                Console.WriteLine(
                    $"{proc.ProcessName} | {Math.Round(DiagnosticHelper.ParsePagedMemorySizeToMb(proc.GetProcessMemorySize()), 1)} Mb");

                sumOfAllProcesses += proc.GetProcessMemorySize();
            }

        }

        ViewData["TotalAllocatedMemory"] = Math.Round(DiagnosticHelper.ParsePagedMemorySizeToGb(sumOfAllProcesses), 1);

        _logger.LogInformation(@$"======JAVA MEMORY USAGE======
{Math.Round(DiagnosticHelper.ParsePagedMemorySizeToGb(javaMem), 1)} Gb
        ");

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