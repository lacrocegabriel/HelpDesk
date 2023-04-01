using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : Entity, new()
    {
        private readonly IRepository<TEntity> _repository;
        public ServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public void BeginTransaction()
        {
            _repository.BeginTransaction();
        }

        public void Commit()
        {
            _repository.Commit();
        }

        public void Rollback()
        {
            _repository.Rollback();
        }

        public void Dispose()
        {
            _repository?.Dispose(); 
        }
    }
}
