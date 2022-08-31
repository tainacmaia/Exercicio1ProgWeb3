using Microsoft.AspNetCore.Mvc;
using Exercicio1ProgWeb3;

namespace Exercicio1ProgWeb3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CadastroClienteController : ControllerBase
    {
        private static readonly string[] Nomes = new[]
        {
        "Taina", "Sergio", "Viviane", "Thayssa", "Lucas", "Nathan", "Sathya"
        };
        private readonly ILogger<CadastroClienteController> _logger;

        private readonly Random geraData = new Random();
        private readonly DateTime data = new DateTime(1950, 1, 1);

        public List<CadastroCliente> cadastros { get; set; }

        public CadastroClienteController(ILogger<CadastroClienteController> logger)
        {
            _logger = logger;
            cadastros = Enumerable.Range(1, 5).Select(index => new CadastroCliente
            {
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                Cpf = $"{Random.Shared.NextInt64(00000000000, 99999999999)}",
                Nascimento = data.AddDays(geraData.Next((DateTime.Today - data).Days)).Date
            })
            .ToList();
        }

        [HttpGet("/cadastros/{index}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <List<CadastroCliente>> Consultar(int index)
        {
            if (index > 4 )
            {
                return NotFound("Não encontrado.");
            }
            return Ok(cadastros[index]);
        }

        [HttpPost("/cadastros/{index}/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CadastroCliente> Inserir([FromBody]CadastroCliente cadastro)
        {
            if (!cadastros.Any(x => x.Equals(cadastro.Cpf) || 
            cadastro.Nascimento.Equals(new DateTime())))
            {
                cadastros.Add(cadastro);
                return StatusCode(201, cadastro);
            }
            return BadRequest("Verifique se os dados estão corretos.");
        }

        [HttpPut("/cadastros/{index}/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int index, CadastroCliente cadastro)
        {
            if (index >= cadastros.Count || index < 0)
            {
                return NotFound();
            }
            cadastros[index] = cadastro;
            return NoContent();
        }

        [HttpDelete("/cadastros/{index}/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Deletar(int index)
        {
            if (index >= cadastros.Count || index < 0)
            {
                return NotFound();
            }
            cadastros.RemoveAt(index);
            return NoContent();
        }
    }
}