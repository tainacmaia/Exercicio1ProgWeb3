using System.ComponentModel.DataAnnotations;
using Microsoft.Win32.SafeHandles;

namespace APICliente.Core.Model
{
    public class Cliente
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "CPF obrigatório")]
        [RegularExpression("([0-9]+)", ErrorMessage = "O CPF deve conter apenas números")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 números")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime? DataNascimento { get; set; }

        [Range (18, int.MaxValue, ErrorMessage = "Você deve ter mais de 18 anos para se cadastrar")]
        public int? Idade { get { return CalcularIdade(); } }

        public int CalcularIdade()
        {
            int idade = DataNascimento.HasValue ? DateTime.Now.Year - DataNascimento.Value.Year : 0;

            if (DataNascimento.HasValue &&
                DateTime.Now.DayOfYear < DataNascimento.Value.DayOfYear)
                idade--;
            return idade;
        }
    }
}