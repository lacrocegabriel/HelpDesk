using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : Entity, new()
    {
        private readonly IServiceBase<TEntity> _serviceBase;
        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }
        public void BeginTransaction()
        {
            _serviceBase.BeginTransaction();
        }

        public void Commit()
        {
            _serviceBase.Commit();
        }

        public void Rollback()
        {
            _serviceBase.Rollback();
        }
        public void Dispose()
        {
            _serviceBase?.Dispose();
        }
    }
}
