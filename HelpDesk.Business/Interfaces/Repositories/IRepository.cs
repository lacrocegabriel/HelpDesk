using HelpDesk.Business.Models;
using System.Linq.Expressions;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterListaId(IEnumerable<Guid> id);
        Task<List<TEntity>> ObterTodos(int skip, int take);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> BuscarUnico(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
        void Commit();
        void Rollback();
        void BeginTransaction();
    }
}
