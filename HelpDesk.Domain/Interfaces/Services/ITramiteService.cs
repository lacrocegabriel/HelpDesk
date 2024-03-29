﻿using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface ITramiteService : IServiceBase<Tramite>
    {
        Task Adicionar(Tramite tramite);
        Task Atualizar(Tramite tramite);
        Task<IEnumerable<Tramite>> ObterTodos(int skip, int take);
        Task<Tramite?> ObterPorId(Guid id);
    }
}
