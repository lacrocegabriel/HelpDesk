namespace HelpDesk.Api.DTOs
{
    public class UsuarioDto : PessoaDto
    {
        public long Login { get; set; }
        public Guid IdSetor { get; set; }
        public IEnumerable<Guid> IdClientes { get; set; }
        public IEnumerable<Guid> IdGerenciadores { get; set; }

    }
}
