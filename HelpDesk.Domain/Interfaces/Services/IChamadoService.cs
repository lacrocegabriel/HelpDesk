﻿using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface IChamadoService : IServiceBase<Chamado>
    {
        Task Adicionar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task<IEnumerable<Chamado>> ObterTodos(int skip, int take);
        Task<Chamado?> ObterPorId(Guid id);
    }
}
