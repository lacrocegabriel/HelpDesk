namespace HelpDesk.Business.Models
{
    public class Usuario : Pessoa
    {
        public long Login { get; set; }
        public Guid IdSetor { get; set; }
        public bool Ativo { get; set; }

        // EF Relations
        public Setor Setor { get; set; }
        public IEnumerable<UsuarioXGerenciador> UsuarioXGerenciador { get; set; }
        public IEnumerable<UsuarioXCliente> UsuarioXClientes { get; set; }
        public IEnumerable<Chamado> ChamadosGerador { get; set; }
        public IEnumerable<Chamado> ChamadosResponsavel { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Gerenciador> Gerenciadores { get; set; }
    }
}
