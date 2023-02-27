using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> ObterTodos()
        {
            var pessoa = (await _pessoaRepository.ObterTodos());
            return Ok(pessoa);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pessoa>> ObterPoId(Guid id)
        {
            var pessoa = await _pessoaRepository.ObterPorId(id);

            if (pessoa == null) return NotFound();

            return pessoa;
        }
    }
}
