using Microsoft.AspNetCore.Mvc.Filters;

namespace APICliente.Filters
{
    public class LogResultFilter : ResultFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Filtro de Resultado LogResultFilter (APÓS) OnResultExecuted");
        }
    }
}
