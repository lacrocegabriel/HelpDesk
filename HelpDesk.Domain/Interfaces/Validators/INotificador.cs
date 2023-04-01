using HelpDesk.Domain.Validator.Notificacoes;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
