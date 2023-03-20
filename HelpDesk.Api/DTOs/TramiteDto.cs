namespace HelpDesk.Api.DTOs
{
    public class TramiteDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public Guid IdChamado { get; set; }
    }
}
