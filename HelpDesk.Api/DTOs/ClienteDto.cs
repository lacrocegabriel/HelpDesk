namespace HelpDesk.Services.Api.DTOs
{
    public class ClienteDto : PessoaDto
    {
        public Guid IdGerenciador { get; set; }
        public string NomeGerenciador { get; set; }
    }
}
