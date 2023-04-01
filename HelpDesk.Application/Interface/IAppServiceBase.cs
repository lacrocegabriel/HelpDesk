using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface IAppServiceBase<TEntity> : IDisposable where TEntity : Entity
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
