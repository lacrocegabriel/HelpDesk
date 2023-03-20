using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/tramites")]
    public class TramitesController : MainController
    {
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITramiteService _tramiteService;
        private readonly IMapper _mapper;

        public TramitesController(ITramiteRepository tramiteRepository,
                                  ITramiteService tramiteService,
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteService = tramiteService;
            _mapper = mapper;

        }

        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<TramiteDto>> ObterTodos([FromRoute] int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<TramiteDto>>(await _tramiteRepository.ObterTodos(skip, take));

        }

        [HttpGet("{id:guid}")]
        public async Task<TramiteDto> ObterPorId(Guid id)
        {
            return _mapper.Map<TramiteDto>(await _tramiteRepository.ObterPorId(id));

        }

        [HttpPost]
        public async Task<ActionResult<TramiteDto>> Adicionar(TramiteDto tramiteDto)
        {
            await _tramiteService.Adicionar(_mapper.Map<Tramite>(tramiteDto));

            return CustomResponse();

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TramiteDto>> Atualizar(Guid id, TramiteDto tramiteDto)
        {
            if (id != tramiteDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no tramite. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };

            await _tramiteService.Atualizar(_mapper.Map<Tramite>(tramiteDto));

            return CustomResponse(tramiteDto);

        }
    }
}
