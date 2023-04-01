namespace HelpDesk.Domain.Entities
{
    public class UsuarioXCliente
    {
        public Guid IdUsuario { get; set; }
        public Guid IdCliente { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }

    }


}
