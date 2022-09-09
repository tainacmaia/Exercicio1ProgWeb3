using APICliente.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICliente.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {

        public IClienteService _clienteService;

        public LogActionFilter(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //fiz um filtro específico
            //var cpf = (string)context.ActionArguments["cpf"];

            //if (_clienteService.ConsultarCliente().Find(x => x.Cpf == cpf) != null)
            //    context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            //return Conflict("Já existe um cliente com esse CPF.");
        }
    }
}
