using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Services;
using HelpDesk.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelpDesk.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Api.Controllers
{
    [Authorize]
    [Route("api/chamados")]
    public class ChamadosController : MainController
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoService _chamadoService;
        private readonly IMapper _mapper;

        public ChamadosController(IChamadoRepository chamadoRepository,
                                  IChamadoService chamadoService,
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador) 
        {
            _chamadoRepository = chamadoRepository;
            _chamadoService = chamadoService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Chamados", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<ChamadoDto>> ObterTodos([FromRoute] int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<ChamadoDto>>(await _chamadoRepository.ObterTodos(skip,take));

        }

        [ClaimsAuthorize("Chamados", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ChamadoDto> ObterPorId(Guid id)
        {
            return _mapper.Map<ChamadoDto>(await _chamadoRepository.ObterPorId(id));

        }

        [ClaimsAuthorize("Chamados","C")]
        [HttpPost]
        public async Task<ActionResult<ChamadoDto>> Adicionar(ChamadoDto chamadoDto)
        {
            await _chamadoService.Adicionar(_mapper.Map<Chamado>(chamadoDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Chamados", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ChamadoDto>> Atualizar(Guid id, ChamadoDto chamadoDto)
        {
            if (id != chamadoDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no chamado. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (_chamadoRepository.ObterPorId(chamadoDto.Id).Result == null)
            {
                NotificateError("O chamado não se encontra cadastrado! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _chamadoService.Atualizar(_mapper.Map<Chamado>(chamadoDto));

            return CustomResponse(chamadoDto);

        }
    }
}


