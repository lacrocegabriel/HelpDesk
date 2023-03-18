namespace HelpDesk.Api.DTOs
{
    public class UsuarioDto : PessoaDto
    {
        public long Login { get; set; }
        public Guid IdSetor { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ClienteDto> Clientes { get; set; }
        public IEnumerable<GerenciadorDto> Gerenciadores { get; set; }

    }
}
