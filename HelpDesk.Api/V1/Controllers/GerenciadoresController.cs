using AutoMapper;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Models;
using HelpDesk.Services.Api.Controllers;
using HelpDesk.Services.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelpDesk.Services.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Services.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/gerenciadores")]
    public class GerenciadoresController : MainController
    {
        private readonly IGerenciadorService _gerenciadorService;
        private readonly IMapper _mapper;

        public GerenciadoresController(IGerenciadorService gerenciadorService,
                                       IMapper mapper,
                                       INotificador notificador,
                                       IUser user) : base(notificador, user)
        {
            _gerenciadorService = gerenciadorService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Gerenciadores", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<GerenciadorDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<GerenciadorDto>>(await _gerenciadorService.ObterTodos(skip,take));

        }

        [ClaimsAuthorize("Gerenciadores", "R")]
        [HttpGet("{id:guid}")]
        public async Task<GerenciadorDto> ObterPorId(Guid id)
        {
            return _mapper.Map<GerenciadorDto>(await _gerenciadorService.ObterPorId(id));

        }

        [ClaimsAuthorize("Gerenciadores", "C")]
        [HttpPost]
        public async Task<ActionResult<GerenciadorDto>> Adicionar(GerenciadorDto gerenciadorDto)
        {
            if (gerenciadorDto == null)
            {
                NotificateError("Não foi enviado um gerenciador válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }

            await _gerenciadorService.Adicionar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Gerenciadores", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GerenciadorDto>> Atualizar(Guid id, GerenciadorDto gerenciadorDto)
        {
            if (gerenciadorDto == null)
            {
                NotificateError("Não foi enviado um gerenciador válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }

            if (id != gerenciadorDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no gerenciador. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            }
            if (_gerenciadorService.ObterPorId(gerenciadorDto.Id).Result == null)
            {
                NotificateError("O gerenciador não se encontra cadastrado! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _gerenciadorService.Atualizar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Gerenciadores", "U")]
        [HttpPut("endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, GerenciadorDto gerenciadorDto)
        {
            if (gerenciadorDto == null)
            {
                NotificateError("Não foi enviado um gerenciador válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }
            if (id != gerenciadorDto.IdEndereco || id != gerenciadorDto.Endereco.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no endereço. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };

            await _gerenciadorService.AtualizarEndereco(_mapper.Map<Endereco>(gerenciadorDto.Endereco));

            return CustomResponse();

        }

        [ClaimsAuthorize("Gerenciadores", "D")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _gerenciadorService.ObterPorId(id) == null) return NotFound();

            await _gerenciadorService.Remover(id);

            return CustomResponse(); 
        }
    }
}


