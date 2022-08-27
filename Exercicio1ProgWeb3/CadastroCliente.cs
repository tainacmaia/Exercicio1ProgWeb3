namespace Exercicio1ProgWeb3
{
    public class CadastroCliente
    {
        public string? Nome { get; set; }

        public long Cpf { get; set; }

        public DateTime Nascimento { get; set; }

        public int Idade { get { return CalculaIdade(); } }

        public int CalculaIdade()
        {
            int idade = DateTime.Now.Year - Nascimento.Year;
            if (DateTime.Now.DayOfYear < Nascimento.DayOfYear)
            {
                idade--;
            }
            return idade;
        }
    }
}