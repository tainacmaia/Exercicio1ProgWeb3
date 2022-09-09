using APICliente.Core.Interface;
using APICliente.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

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
            var cliente = context.ActionArguments["cliente"] as Cliente;

            if (HttpMethods.IsPut(context.HttpContext.Request.Method) &&
                _clienteService.ConsultarCliente(cliente.Id) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            else if (HttpMethods.IsPost(context.HttpContext.Request.Method) &&
                _clienteService.ConsultarCliente(cliente.Cpf) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
                Console.WriteLine("Esse CPF já existe.");
            }
        }
    }
}
