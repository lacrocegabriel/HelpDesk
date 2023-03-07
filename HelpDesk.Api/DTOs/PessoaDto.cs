using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.DTOs
{
    public class PessoaDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimentoConstituicao { get; set; }
        public long IdTipoPessoa { get; set; }
        public Guid IdEndereco { get; set; }
        public EnderecoDto? Endereco { get; set; }

    }
}
