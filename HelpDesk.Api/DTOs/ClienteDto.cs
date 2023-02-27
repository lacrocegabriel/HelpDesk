using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.DTOs
{
    public class ClienteDto : PessoaDto
    {
        public Guid IdGerenciador { get; set; }
    }
}
