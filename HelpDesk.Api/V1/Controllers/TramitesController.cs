using AutoMapper;
using HelpDesk.Api.Controllers;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelpDesk.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/tramites")]
    public class TramitesController : MainController
    {
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITramiteService _tramiteService;
        private readonly IMapper _mapper;

        public TramitesController(ITramiteRepository tramiteRepository,
                                  ITramiteService tramiteService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteService = tramiteService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Tramites", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<TramiteDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<TramiteDto>>(await _tramiteRepository.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Tramites", "R")]
        [HttpGet("{id:guid}")]
        public async Task<TramiteDto> ObterPorId(Guid id)
        {
            return _mapper.Map<TramiteDto>(await _tramiteRepository.ObterPorId(id));

        }

        [ClaimsAuthorize("Tramites", "C")]
        [HttpPost]
        public async Task<ActionResult<TramiteDto>> Adicionar(TramiteDto tramiteDto)
        {
            await _tramiteService.Adicionar(_mapper.Map<Tramite>(tramiteDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Tramites", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TramiteDto>> Atualizar(Guid id, TramiteDto tramiteDto)
        {
            if (id != tramiteDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no tramite. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (_tramiteRepository.ObterPorId(tramiteDto.Id).Result == null)
            {
                NotificateError("O trâmite não se encontra cadastrado! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _tramiteService.Atualizar(_mapper.Map<Tramite>(tramiteDto));

            return CustomResponse(tramiteDto);

        }
    }
}
