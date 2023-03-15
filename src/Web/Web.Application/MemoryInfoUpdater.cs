using System.Diagnostics;

namespace Web.Application;

public class MemoryInfoUpdater
{
    public List<double> MemoryUsage { get; set; } = new();
    public int MemCount { get; private set; }
    
    public void Update()
    {
        var all = Process.GetProcesses();
        double javaMem = 0;
        
        while (true)
        {
            var restMem = 0D;
            
            foreach (var proc in all)
            {
                if (proc.ProcessName.ToLower() == "java")
                {
                    //javaMem += Math.Round((double)proc.GetProcessMemorySize(), 1);
                }

                restMem += proc.GetProcessMemorySize();
            }

            restMem = DiagnosticHelper.ParsePagedMemorySizeToGb(restMem);
            
            MemoryUsage.Add(restMem);
            
            Thread.Sleep(10000);
        }
        
    }
}