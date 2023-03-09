﻿using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;
using HelpDesk.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/gerenciadores")]
    public class GerenciadoresController : MainController
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IGerenciadorService _gerenciadorService;
        private readonly IMapper _mapper;

        public GerenciadoresController(IGerenciadorRepository gerenciadorRepository,
                                       IGerenciadorService gerenciadorService,
                                       IMapper mapper,
                                       INotificador notificador) : base(notificador)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _gerenciadorService = gerenciadorService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<GerenciadorDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<GerenciadorDto>>(await _gerenciadorRepository.ObterTodos());

        }

        [HttpGet("{id:guid}")]
        public async Task<GerenciadorDto> ObterPorId(Guid id)
        {
            return _mapper.Map<GerenciadorDto>(await _gerenciadorRepository.ObterPorId(id));

        }

        [HttpPost]
        public async Task<ActionResult<GerenciadorDto>> Adicionar(GerenciadorDto gerenciadorDto)
        {
            await _gerenciadorService.Adicionar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return CustomResponse();

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GerenciadorDto>> Atualizar(Guid id, GerenciadorDto gerenciadorDto)
        {
            if(id != gerenciadorDto.Id) return BadRequest();

            await _gerenciadorService.Atualizar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return CustomResponse();

        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, GerenciadorDto gerenciadorDto)
        {
            if (id != gerenciadorDto.IdEndereco || id != gerenciadorDto.Endereco.Id) return BadRequest();

            await _gerenciadorService.AtualizarEndereco(_mapper.Map<Endereco>(gerenciadorDto.Endereco));

            return CustomResponse();

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _gerenciadorRepository.ObterPorId(id) == null) return NotFound();

            await _gerenciadorService.Remover(id);

            return CustomResponse(); 
        }
    }
}

