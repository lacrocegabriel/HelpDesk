using AutoMapper;
using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Services.Api.Controllers;
using HelpDesk.Services.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelpDesk.Services.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Services.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/tramites")]
    public class TramitesController : MainController
    {
        private readonly ITramiteAppService _tramiteAppService;
        private readonly IMapper _mapper;

        public TramitesController(ITramiteAppService tramiteAppService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _tramiteAppService = tramiteAppService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Tramites", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<TramiteDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<TramiteDto>>(await _tramiteAppService.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Tramites", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TramiteDto>> ObterPorId(Guid id)
        {
            var tramite = _mapper.Map<TramiteDto>(await _tramiteAppService.ObterPorId(id));

            if (tramite == null)
            {
                NotificateError("O usuário não possui permissão para visualizar o tramite selecionado! Verifique as informações e tente novamente");

                return CustomResponse();
            }
            return tramite;
            

        }

        [ClaimsAuthorize("Tramites", "C")]
        [HttpPost]
        public async Task<ActionResult<TramiteDto>> Adicionar(TramiteDto tramiteDto)
        {
            if (tramiteDto == null)
            {
                NotificateError("Não foi enviado um trâmite válido. Verifique as informações e tente novamente!");
                return CustomResponse();
            }
            await _tramiteAppService.Adicionar(_mapper.Map<Tramite>(tramiteDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Tramites", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TramiteDto>> Atualizar(Guid id, TramiteDto tramiteDto)
        {
            if (tramiteDto == null)
            {
                NotificateError("Não foi enviado um trâmite válido. Verifique as informações e tente novamente!");
                return CustomResponse();
            }
            if (id != tramiteDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no tramite. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (_tramiteAppService.ObterPorId(tramiteDto.Id).Result == null)
            {
                NotificateError("O trâmite não se encontra cadastrado ou o usuário não possui permissão para editá-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _tramiteAppService.Atualizar(_mapper.Map<Tramite>(tramiteDto));

            return CustomResponse(tramiteDto);

        }
    }
}
