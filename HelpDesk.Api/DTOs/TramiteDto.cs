namespace HelpDesk.Services.Api.DTOs
{
    public class TramiteDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public string NomeUsuarioGerador { get; set; }
        public Guid IdChamado { get; set; }
        public ChamadoDto Chamado { get; set; }
    }
}
