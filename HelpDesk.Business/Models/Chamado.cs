using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Business.Models
{
    public class Chamado : Entity
    {
        public string Titulo { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Numero { get; set; }
        public string Descricao { get; set; }
        public long IdSituacaoChamado { get; set; }
        public Guid IdGerenciador { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public Guid IdUsuarioResponsavel { get; set; }

        // EF Relations
        public SituacaoChamado SituacaoChamado { get; set; }
        public Gerenciador Gerenciador { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario UsuarioGerador { get; set; }
        public Usuario UsuarioResponsavel { get; set; }
    }
}
