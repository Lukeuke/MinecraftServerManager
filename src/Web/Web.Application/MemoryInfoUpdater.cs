using System.Diagnostics;

namespace Web.Application;

public class MemoryInfoUpdater
{
    public List<double> MemoryUsage { get; set; } = new();

    public void Update()
    {
        var all = Process.GetProcesses();
        double javaMem = 0;
        
        while (true)
        {
            foreach (var proc in all)
            {
                if (proc.ProcessName.ToLower() == "java")
                {
                    javaMem += Math.Round((double)proc.GetProcessMemorySize(), 1);
                }
                
                MemoryUsage.Add(Math.Round((double)proc.GetProcessMemorySize(), 1));
            }

            Thread.Sleep(10000);

            Console.WriteLine(MemoryUsage.Sum());
        }
        
    }
}