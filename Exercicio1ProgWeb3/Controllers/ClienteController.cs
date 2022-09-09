using Microsoft.AspNetCore.Mvc;
using APICliente.Core.Interface;
using APICliente.Core.Model;
using APICliente.Filters;

namespace APICliente.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(LogResourceFilter))]

    public class ClienteController : ControllerBase
    {
        public IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            Console.WriteLine("Instanciando Cliente Controller");
            _clienteService = clienteService;
        }

        [HttpGet("/cadastros/consultarcpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> ConsultarCliente(string cpf)
        {
            Console.WriteLine("Iniciando");
            if (_clienteService.ConsultarCliente(cpf) == null)
                return NotFound("Cliente não encontrado");
            return Ok(_clienteService.ConsultarCliente(_clienteService.ConsultarCliente(cpf).Cpf));
        }

        [HttpGet("/cadastros/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <List<Cliente>> ConsultarCliente()
        {
            Console.WriteLine("Iniciando");
            if (_clienteService.ConsultarCliente() == null)
                return NotFound("Não há clientes cadastrados.");
            return Ok(_clienteService.ConsultarCliente());
        }

        [HttpPost("/cadastros/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ServiceFilter(typeof(ClienteExisteActionFilter))]
        public ActionResult<Cliente> Inserir(Cliente cliente)
        {
            if (!_clienteService.InserirCliente(cliente))
                return BadRequest();

            return CreatedAtAction(nameof(Inserir), cliente);
        }

        [HttpPut("/cadastros/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ClienteExisteActionFilter))]
        public IActionResult Atualizar(long id, Cliente cliente)
        {
            Console.WriteLine("Iniciando");
            if (!_clienteService.AtualizarCliente(id, cliente))
                return NotFound("Cliente não encontrado.");

            _clienteService.AtualizarCliente(id, cliente);
            return NoContent();
        }

        [HttpDelete("/cadastros/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> Deletar(long id)
        {
            if (_clienteService.DeletarCliente(id))
                return NoContent();
            return NotFound();
        }
    }
}