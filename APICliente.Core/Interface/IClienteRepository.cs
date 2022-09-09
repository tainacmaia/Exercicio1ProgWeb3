using APICliente.Core.Model;

namespace APICliente.Core.Interface
{
    public interface IClienteRepository
    {
        List<Cliente> ConsultarCliente();
        Cliente ConsultarCliente(string cpf);
        Cliente ConsultarCliente(long id);
        public bool InserirCliente(Cliente cliente);
        public bool AtualizarCliente(long id, Cliente cliente);
        public bool DeletarCliente(long id);
    }
}
