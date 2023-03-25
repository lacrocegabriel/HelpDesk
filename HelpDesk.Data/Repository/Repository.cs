using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly HelpDeskContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(HelpDeskContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        public async Task<TEntity> BuscarUnico(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<TEntity>> ObterListaId(IEnumerable<Guid> idEntity)
        {
            List<TEntity> entity = new List<TEntity>();

            foreach(var id in idEntity)
            {
                entity.Add(await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id));
            }

            return entity;
        }

        public virtual async Task<List<TEntity>> ObterTodos(int skip, int take)
        {
            return await DbSet.AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            Db.Database.BeginTransaction();
        }
        public void Commit()
        {
            Db.Database.CommitTransaction();
        }

        public void Rollback()
        {
            Db.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        
    }
}
