namespace HelpDesk.Domain.Entities
{
    public class Gerenciador : Pessoa
    {

        // EF Relations
        public Pessoa Pessoa { get; set; }
        public IEnumerable<UsuarioXGerenciador> UsuarioXGerenciador { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Chamado> Chamados { get; set; }
        public IEnumerable<Setor> Setores{ get; set; }

    }
}
