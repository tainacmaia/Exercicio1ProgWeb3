using Microsoft.AspNetCore.Mvc;
using Exercicio1ProgWeb3;

namespace Exercicio1ProgWeb3.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                Cpf = Random.Shared.NextInt64(00000000000, 99999999999),
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                Nascimento = data.AddDays(geraData.Next((DateTime.Today - data).Days))
            })
            .ToList();
        }

        [HttpGet]
        public List<CadastroCliente> Consultar(int index)
        {
            return cadastros;
        }

        [HttpPost]
        public CadastroCliente Inserir(CadastroCliente cadastro)
        {
            cadastros.Add(cadastro);
            return cadastro;
        }

        [HttpPut]
        public CadastroCliente Atualizar(int index, CadastroCliente cadastro)
        {
            cadastros[index] = cadastro;
            return cadastros[index];
        }

        [HttpDelete]
        public List<CadastroCliente> Deletar(int index)
        {
            cadastros.RemoveAt(index);
            return cadastros;
        }
    }
}