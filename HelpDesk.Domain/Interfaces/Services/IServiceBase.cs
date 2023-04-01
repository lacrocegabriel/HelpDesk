using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : Entity
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
