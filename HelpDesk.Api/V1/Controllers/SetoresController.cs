﻿using AutoMapper;
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
    [Route("helpdesk/v{version:apiVersion}/setores")]
    public class SetoresController : MainController
    {
        private readonly ISetorRepository _setorRepository;
        private readonly ISetorService _setorService;
        private readonly IMapper _mapper;

        public SetoresController(ISetorRepository setorRepository,
                                  ISetorService setorService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _setorRepository = setorRepository;
            _setorService = setorService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Setores", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<SetorDto>> ObterTodos([FromRoute] int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<SetorDto>>(await _setorRepository.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Setores", "R")]
        [HttpGet("{id:guid}")]
        public async Task<SetorDto> ObterPorId(Guid id)
        {
            return _mapper.Map<SetorDto>(await _setorRepository.ObterPorId(id));

        }

        [ClaimsAuthorize("Setores", "C")]
        [HttpPost]
        public async Task<ActionResult<SetorDto>> Adicionar(SetorDto setorDto)
        {
            await _setorService.Adicionar(_mapper.Map<Setor>(setorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Setores", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SetorDto>> Atualizar(Guid id, SetorDto setorDto)
        {
            if (id != setorDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no setor. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (_setorRepository.ObterPorId(setorDto.Id).Result == null)
            {
                NotificateError("O setor não se encontra cadastrado! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _setorService.Atualizar(_mapper.Map<Setor>(setorDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Setores", "D")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _setorRepository.ObterPorId(id) == null) return NotFound();

            await _setorService.Remover(id);

            return CustomResponse();
        }

    }
}