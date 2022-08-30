using System.ComponentModel.DataAnnotations;

namespace Exercicio1ProgWeb3
{
    public class CadastroCliente
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        public string? Nome { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "O CPF deve conter apenas números")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 números")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigatória")] //não ta funcionando
        [DataType(DataType.Date, ErrorMessage = "Digite uma data no formato aaaa-mm-dd")]
        public DateTime Nascimento { get; set; }

        [Range (18, int.MaxValue, ErrorMessage = "Você deve ter mais de 18 anos para se cadastrar")]
        public int Idade { get { return CalcularIdade(); } }

        public int CalcularIdade()
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