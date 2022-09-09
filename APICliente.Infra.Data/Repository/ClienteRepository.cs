using Microsoft.Data.SqlClient;
using Dapper;
using APICliente.Core.Model;
using Microsoft.Extensions.Configuration;
using APICliente.Core.Interface;

namespace APICliente.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Cliente ConsultarCliente(long id)
        {
            var query = "SELECT * FROM clientes WHERE id = @id";

            var parameters = new DynamicParameters(new
            {
                id
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Cliente>(query, parameters);
        }
        public Cliente ConsultarCliente(string cpf)
        {
            var query = "SELECT * FROM clientes WHERE cpf = @cpf";

            var parameters = new DynamicParameters(new
            {
                cpf
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Cliente>(query, parameters);
        }

        public List<Cliente> ConsultarCliente()
        {
            var query = "SELECT*FROM clientes";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Cliente>(query).ToList();
        }

        public bool InserirCliente(Cliente cliente)
        {
            var query = "INSERT INTO clientes VALUES(@cpf, @nome, @dataNascimento, @idade)";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cliente.Cpf);
            parameters.Add("nome", cliente.Nome);
            parameters.Add("dataNascimento", cliente.DataNascimento);
            parameters.Add("idade", cliente.Idade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeletarCliente(long id)
        {
            var query = "DELETE FROM clientes WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool AtualizarCliente(long id, Cliente cliente)
        {
            var query = "UPDATE clientes SET nome = @nome, dataNascimento = @dataNascimento," +
                " idade = @idade, cpf = @cpf WHERE id = @id";

            var parameters = new DynamicParameters(cliente);

            parameters.Add("id", cliente.Id);
            parameters.Add("cpf", cliente.Cpf);
            parameters.Add("nome", cliente.Nome);
            parameters.Add("dataNascimento", cliente.DataNascimento);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
