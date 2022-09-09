using APICliente.Core.Interface;
using APICliente.Core.Model;

namespace APICliente.Core.Service
{
    public class ClienteService : IClienteService
    {
        public IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Cliente ConsultarCliente(long id)
        {
            return _clienteRepository.ConsultarCliente(id);
        }
        public List<Cliente> ConsultarCliente()
        {
           return _clienteRepository.ConsultarCliente();
        }

        public Cliente ConsultarCliente(string cpf)
        {
            return _clienteRepository.ConsultarCliente(cpf);
        }

        public bool InserirCliente(Cliente cliente)
        {
            return _clienteRepository.InserirCliente(cliente);
        }
        public bool AtualizarCliente(long id, Cliente cliente)
        {
            //try
            //{
            //    cliente = null;
            //    cliente.Id = id;
            //}
            //catch (Exception ex)
            //{
            //    var tipoExcecao = ex.GetType().Name;
            //    var mensagem = ex.Message;
            //    var caminho = ex.InnerException.StackTrace;

            //    Console.WriteLine($"Tipo da Exceção: {tipoExcecao}, Mensagem: {mensagem}, Stack Trace: {caminho}");
            //    return false;
            //}
            cliente.Id = id;
            return _clienteRepository.AtualizarCliente(id, cliente);
        }
        public bool DeletarCliente(long id)
        {
            return _clienteRepository.DeletarCliente(id);
        }
    }
}