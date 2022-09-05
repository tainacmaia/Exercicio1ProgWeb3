using Microsoft.AspNetCore.Mvc;
using APICliente.Core.Interface;
using APICliente.Core.Model;

namespace APICliente.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        public IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("/cadastros/consultarcpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> ConsultarCliente(string cpf)
        {
            var cliente = _clienteService.ConsultarCliente(cpf);
            if (cliente == null)
                return NotFound("Cliente não encontrado");
            return Ok(_clienteService.ConsultarCliente(cliente.Cpf));
        }

        [HttpGet("/cadastros/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <List<Cliente>> ConsultarCliente()
        {
            var cliente = _clienteService.ConsultarCliente();
            if (cliente == null)
                return NotFound("Não há clientes cadastrados.");
            return Ok(_clienteService.ConsultarCliente());
        }

        [HttpPost("/cadastros/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Cliente> Inserir(Cliente cliente)
        {
            var clienteExiste = _clienteService.ConsultarCliente(cliente.Cpf);
            if (clienteExiste != null)
                return Conflict("Já existe um cliente com esse CPF.");

            if (!_clienteService.InserirCliente(cliente))
                return BadRequest();

            return CreatedAtAction(nameof(Inserir), cliente);
        }

        [HttpPut("/cadastros/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Atualizar(long id, Cliente cliente)
        {
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
                return NotFound();
            return NoContent();
        }
    }
}