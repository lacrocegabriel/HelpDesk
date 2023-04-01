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
    [Route("helpdesk/v{version:apiVersion}/setores")]
    public class SetoresController : MainController
    {
        private readonly ISetorAppService _setorAppService;
        private readonly IMapper _mapper;

        public SetoresController(ISetorAppService setorAppService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _setorAppService = setorAppService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Setores", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<SetorDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<SetorDto>>(await _setorAppService.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Setores", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SetorDto>> ObterPorId(Guid id)
        {
            var setor = _mapper.Map<SetorDto>(await _setorAppService.ObterPorId(id));

            if (setor == null)
            {
                return CustomResponse();
            }
            return setor;

        }

        [ClaimsAuthorize("Setores", "C")]
        [HttpPost]
        public async Task<ActionResult<SetorDto>> Adicionar(SetorDto setorDto)
        {
            if (setorDto == null)
            {
                NotificateError("Não foi enviado um setor válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }
            await _setorAppService.Adicionar(_mapper.Map<Setor>(setorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Setores", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SetorDto>> Atualizar(Guid id, SetorDto setorDto)
        {
            if (setorDto == null)
            {
                NotificateError("Não foi enviado um setor válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }
            if (id != setorDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no setor. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (_setorAppService.ObterPorId(setorDto.Id).Result == null)
            {
                NotificateError("O setor não se encontra cadastrado ou o usuário não possui permissão para editá-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _setorAppService.Atualizar(_mapper.Map<Setor>(setorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Setores", "D")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _setorAppService.ObterPorId(id) == null) return NotFound();

            await _setorAppService.Remover(id);

            return CustomResponse();
        }

    }
}
