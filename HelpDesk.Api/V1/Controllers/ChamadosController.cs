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
    [Route("helpdesk/v{version:apiVersion}/chamados")]
    public class ChamadosController : MainController
    {
        private readonly IChamadoAppService _chamadoAppService;
        private readonly IMapper _mapper;

        public ChamadosController(IChamadoAppService chamadoAppService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _chamadoAppService = chamadoAppService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Chamados", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<ChamadoDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<ChamadoDto>>(await _chamadoAppService.ObterTodos(skip, take)); 
        }

        [ClaimsAuthorize("Chamados", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChamadoDto>> ObterPorId(Guid id)
        {
            var chamado = _mapper.Map<ChamadoDto>(await _chamadoAppService.ObterPorId(id));

            if (chamado == null)
            {
                return CustomResponse();
            }
            return chamado;

        }

        [ClaimsAuthorize("Chamados","C")]
        [HttpPost]
        public async Task<ActionResult<ChamadoDto>> Adicionar(ChamadoDto chamadoDto)
        {
            if (chamadoDto == null)
            {
                NotificateError("Não foi enviado um chamado válido. Verifique as informações e tente novamente!");
                return CustomResponse();
            }
            await _chamadoAppService.Adicionar(_mapper.Map<Chamado>(chamadoDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Chamados", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ChamadoDto>> Atualizar(Guid id, ChamadoDto chamadoDto)
        {
            if (chamadoDto == null)
            {
                NotificateError("Não foi enviado um chamado válido. Verifique as informações e tente novamente!");
                return CustomResponse();
            }
            if (id != chamadoDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no chamado. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (_chamadoAppService.ObterPorId(chamadoDto.Id).Result == null)
            {
                NotificateError("O chamado não se encontra cadastrado ou o usuário não possui permissão para editá-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _chamadoAppService.Atualizar(_mapper.Map<Chamado>(chamadoDto));

            return CustomResponse(chamadoDto);

        }
    }
}


