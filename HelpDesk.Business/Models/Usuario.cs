namespace HelpDesk.Business.Models
{
    public class Usuario : Pessoa
    {
        public long Login { get; set; }
        public Guid IdSetor { get; set; }
        
        // EF Relations
        public Setor Setor { get; set; }
        public IEnumerable<Chamado> ChamadosGerador { get; set; }
        public IEnumerable<Chamado> ChamadosResponsavel { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Gerenciador> Gerenciadores { get; set; }
    }
}
