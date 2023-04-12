using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Services.Api.DTOs
{
    public class SetorDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdGerenciador { get; set; }
        public string NomeGerenciador { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo Descrição precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
    }
}
