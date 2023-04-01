using AutoMapper;
using HelpDesk.Api.Controllers;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelpDesk.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/setores")]
    public class SetoresController : MainController
    {
        private readonly ISetorService _setorService;
        private readonly IMapper _mapper;

        public SetoresController(ISetorRepository setorRepository,
                                  ISetorService setorService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _setorService = setorService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Setores", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<SetorDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<SetorDto>>(await _setorService.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Setores", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SetorDto>> ObterPorId(Guid id)
        {
            var setor = _mapper.Map<SetorDto>(await _setorService.ObterPorId(id));

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
            await _setorService.Adicionar(_mapper.Map<Setor>(setorDto));

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
            if (_setorService.ObterPorId(setorDto.Id).Result == null)
            {
                NotificateError("O setor não se encontra cadastrado ou o usuário não possui permissão para editá-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _setorService.Atualizar(_mapper.Map<Setor>(setorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Setores", "D")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _setorService.ObterPorId(id) == null) return NotFound();

            await _setorService.Remover(id);

            return CustomResponse();
        }

    }
}
