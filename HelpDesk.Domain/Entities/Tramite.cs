using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Domain.Entities
{
    public class Tramite : Entity
    {
        public string Descricao { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public Guid IdChamado { get; set; }
        [NotMapped]
        public long IdSituacaoChamado { get; set; }
        public Chamado Chamado { get; set; }
        public Usuario UsuarioGerador { get; set; }
    }
}
