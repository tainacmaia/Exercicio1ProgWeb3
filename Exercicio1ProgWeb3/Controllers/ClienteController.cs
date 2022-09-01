using Exercicio1ProgWeb3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Exercicio1ProgWeb3;

namespace Exercicio1ProgWeb3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        public List<Cliente> ClienteList { get; set; }
        public ClienteRepository _repositoryCliente;

        public ClienteController(IConfiguration configuration)
        {
            ClienteList = new List<Cliente>();
            _repositoryCliente = new ClienteRepository(configuration);
        }


        [HttpGet("/cadastros/consultarcpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> ConsultarCpf(string cpf)
        {
            var cliente = _repositoryCliente.GetCliente(cpf);
            if (cliente == null)
                return NotFound("Cliente não encontrado");
            return Ok(_repositoryCliente.GetCliente(cliente.Cpf));
        }

        [HttpGet("/cadastros/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <List<Cliente>> Consultar()
        {
            var clientes = ClienteList;
            if (clientes == null)
                return NotFound("Não há clientes cadastrados.");
            return Ok(_repositoryCliente.GetCliente());
        }

        [HttpPost("/cadastros/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Cliente> Inserir(Cliente cliente)
        {
            var clienteExiste = _repositoryCliente.GetCliente(cliente.Cpf);
            if (clienteExiste != null)
                return Conflict("Já existe um cliente com esse CPF.");

            if (!_repositoryCliente.InsertCliente(cliente))
                return BadRequest();

            return CreatedAtAction(nameof(Inserir), cliente);
        }

        [HttpPut("/cadastros/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Atualizar(long id, Cliente cliente)
        {
            if (!_repositoryCliente.UpdateCliente(id, cliente))
                return NotFound("Cliente não encontrado.");

            _repositoryCliente.UpdateCliente(id, cliente);
            return NoContent();
        }

        [HttpDelete("/cadastros/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> Deletar(long id)
        {
            if (_repositoryCliente.DeleteCliente(id))
                return NotFound();
            return NoContent();
        }
    }
}