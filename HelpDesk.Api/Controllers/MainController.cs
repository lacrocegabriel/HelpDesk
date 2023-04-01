using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Validator.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HelpDesk.Services.Api.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        private readonly INotificador _notificador;
        public readonly IUser AppUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        public MainController(INotificador notificador,
                              IUser appUser)
        {
            _notificador = notificador;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(e => e.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificateErrorInvalidModel(modelState);
            return CustomResponse();
        }

        protected void NotificateErrorInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificateError(errorMsg);
            }
        }

        protected void NotificateError(string message)
        {
            _notificador.Handle(new Notificacao(message));
        }

    }
}
