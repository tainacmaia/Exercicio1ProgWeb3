using APICliente.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICliente.Filters
{
    public class ClienteExisteActionFilter : ActionFilterAttribute
    {
        public IClienteService _clienteService;

        public ClienteExisteActionFilter(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        public override void OnActionExecuting (ActionExecutingContext context)
        {
            if (!HttpMethods.IsPost(context.HttpContext.Request.Method) && 
                _clienteService.ConsultarCliente((long)context.ActionArguments["id"]) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            else if (HttpMethods.IsPost(context.HttpContext.Request.Method) &&
                _clienteService.ConsultarCliente((string)context.ActionArguments["cpf"]) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }
    }
}
