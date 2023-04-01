namespace HelpDesk.Domain.Entities
{
    public class UsuarioXGerenciador
    {
        public Guid IdUsuario { get; set; }
        public Guid IdGerenciador { get; set; }
        public Usuario Usuario { get; set; }
        public Gerenciador Gerenciador { get; set; }
    }
}
