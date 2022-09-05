using System.ComponentModel.DataAnnotations;
using Microsoft.Win32.SafeHandles;

namespace APICliente.Core.Model
{
    public class Cliente
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Nome obrigat�rio")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "CPF obrigat�rio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "O CPF deve conter apenas n�meros")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 n�meros")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento � obrigat�ria")]
        public DateTime? DataNascimento { get; set; }

        [Range (18, int.MaxValue, ErrorMessage = "Voc� deve ter mais de 18 anos para se cadastrar")]
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