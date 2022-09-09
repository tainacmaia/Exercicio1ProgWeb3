using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace APICliente.Filters
{
    public class LogResourceFilter : IResourceFilter
    {
        Stopwatch relogio = new();
        public void OnResourceExecuted(ResourceExecutedContext context) //depois
        {
            relogio.Stop();
            Console.WriteLine($"Tempo da operação: {relogio.ElapsedMilliseconds} ms");
        }

        public void OnResourceExecuting(ResourceExecutingContext context) //antes
        {
            relogio.Start();
        }
    }
}
