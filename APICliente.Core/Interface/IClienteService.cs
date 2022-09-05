using APICliente.Core.Model;

namespace APICliente.Core.Interface
{
    public interface IClienteService
    {
        List<Cliente> ConsultarCliente();
        Cliente ConsultarCliente(string cpf);
        public bool InserirCliente(Cliente cliente);
        public bool AtualizarCliente(long id, Cliente cliente);
        public bool DeletarCliente(long id);
    }
}
