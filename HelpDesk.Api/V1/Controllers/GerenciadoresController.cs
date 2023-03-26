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
    [Route("helpdesk/v{version:apiVersion}/gerenciadores")]
    public class GerenciadoresController : MainController
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IGerenciadorService _gerenciadorService;
        private readonly IMapper _mapper;

        public GerenciadoresController(IGerenciadorRepository gerenciadorRepository,
                                       IGerenciadorService gerenciadorService,
                                       IMapper mapper,
                                       INotificador notificador,
                                       IUser user) : base(notificador, user)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _gerenciadorService = gerenciadorService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Gerenciadores", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<GerenciadorDto>> ObterTodos([FromRoute] int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<GerenciadorDto>>(await _gerenciadorRepository.ObterTodos(skip,take));

        }

        [ClaimsAuthorize("Gerenciadores", "R")]
        [HttpGet("{id:guid}")]
        public async Task<GerenciadorDto> ObterPorId(Guid id)
        {
            return _mapper.Map<GerenciadorDto>(await _gerenciadorRepository.ObterPorId(id));

        }

        [ClaimsAuthorize("Gerenciadores", "C")]
        [HttpPost]
        public async Task<ActionResult<GerenciadorDto>> Adicionar(GerenciadorDto gerenciadorDto)
        {
            await _gerenciadorService.Adicionar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Gerenciadores", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GerenciadorDto>> Atualizar(Guid id, GerenciadorDto gerenciadorDto)
        {
            if(id != gerenciadorDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no gerenciador. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            }
            if (_gerenciadorRepository.ObterPorId(gerenciadorDto.Id).Result == null)
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
            if (await _gerenciadorRepository.ObterPorId(id) == null) return NotFound();

            await _gerenciadorService.Remover(id);

            return CustomResponse(); 
        }
    }
}


