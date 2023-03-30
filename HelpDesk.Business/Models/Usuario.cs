using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Business.Models
{
    public class Usuario : Pessoa
    {
        public string Login { get; set; }
        [NotMapped]
        public string Senha { get; set; }
        public Guid IdSetor { get; set; }
        
        // EF Relations
        public Setor Setor { get; set; }
        public IEnumerable<UsuarioXGerenciador> UsuariosXGerenciadores { get; set; }
        public IEnumerable<UsuarioXCliente> UsuariosXClientes { get; set; }
        public IEnumerable<Chamado> ChamadosGerador { get; set; }
        public IEnumerable<Chamado> ChamadosResponsavel { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Gerenciador> Gerenciadores { get; set; }
        public IEnumerable<Tramite> Tramites { get; set; }
    }
}
