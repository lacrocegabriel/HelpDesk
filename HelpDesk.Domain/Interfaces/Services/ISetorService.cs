﻿using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface ISetorService : IServiceBase<Setor>
    {
        Task Adicionar(Setor setor);
        Task Atualizar(Setor setor);
        Task Remover(Guid id);
        Task<IEnumerable<Setor>> ObterTodos(int skip, int take);
        Task<Setor?> ObterPorId(Guid id);

    }
}
