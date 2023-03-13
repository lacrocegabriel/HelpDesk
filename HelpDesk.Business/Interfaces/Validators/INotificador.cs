using HelpDesk.Business.Validator.Notificacoes;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
