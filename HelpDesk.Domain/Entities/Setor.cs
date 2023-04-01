namespace HelpDesk.Domain.Entities
{
    public class Setor : Entity
    {
        public Guid IdGerenciador { get; set; }
        public string Descricao { get; set; }
        
        // EF Relations
        public Gerenciador Gerenciador { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
