using System.Diagnostics;

namespace Web.Application;

public static class DiagnosticHelper
{
    /// <summary>
    /// This method returns Ram memory in MB
    /// </summary>
    /// <returns>Size of memory in MB</returns>
    public static double GetRamAmount()
    {
        var gcMemoryInfo = GC.GetGCMemoryInfo();
        var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
        
        return installedMemory / 1048576.0 / 1024;
    }

    /// <summary>
    /// Gets pages memory size of running process
    /// </summary>
    /// <param name="proc">Process in System.Diagnostics</param>
    /// <returns>PagedMemorySize64 as long value</returns>
    public static long GetProcessPagedMemorySize(this Process proc)
    {
        return proc.PagedMemorySize64;
    }
    
    public static double ParsePagedMemorySizeToMb(long value)
    {
        return (double) value / 1048576;
    }
    
    public static double ParsePagedMemorySizeToGb(long value)
    {
        return (double) value / 1073741824;
    }
    
    public static double ParsePagedMemorySizeToGb(double value)
    {
        return value / 1073741824;
    }
}