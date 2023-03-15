using System.Text;

namespace Web.Application;

public static class MemoryUsageGraphHelper
{
    public static (string, string) DataLabels(List<double> memDict)
    {
        var data = new StringBuilder();
        var labels = new StringBuilder();

        data.Append('[');
        
        foreach (var value in memDict)
        {
            var mem = Math.Round(value, 1).ToString().Replace(',', '.');

            Console.WriteLine(mem);
            
            data.Append($"{mem},");
        }
        
        data.Append(']');

        return (data.ToString(), labels.ToString());
    }
}