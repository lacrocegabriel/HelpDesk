using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.DTOs
{
    public class UsuarioDto : PessoaDto
    {
        public string Login { get; set; }
        
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [StringLength(100,ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
        
        [Compare("Password", ErrorMessage ="As senhas não conferem.")]
        public string ConfirmaSenha { get; set; }
        public Guid IdSetor { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ClienteDto> Clientes { get; set; }
        public IEnumerable<GerenciadorDto> Gerenciadores { get; set; }

    }

    public class UsuarioDtoLogin
    {
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
    }
}
