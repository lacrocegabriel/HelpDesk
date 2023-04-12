using System.Runtime.InteropServices.JavaScript;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Infrastructure.Data.Context;
using HelpDesk.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(HelpDeskContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosPorPermissao(Usuario usuario, int skip, int take)
        {
            var (idGerenciadores, idClientes) = await ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await Db.Usuarios.AsNoTracking()
                .Where(u => u.UsuariosXGerenciadores.Any(ug => idGerenciadores.Contains(ug.IdGerenciador))
                    && u.UsuariosXClientes.Any(uc => idClientes.Contains(uc.IdCliente)))
                .Include(u => u.Setor)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Usuario> ObterUsuarioGerenciadoresClientes(Guid id)
        {
            return await Db.Usuarios.AsNoTracking()
                    .Include(x => x.UsuariosXGerenciadores)
                    .Include(x => x.UsuariosXClientes)
                    .Include(u => u.Setor)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

        }

        public async Task<(List<Guid> IdGerenciadores,List<Guid> IdClientes)> ObterGerenciadoresClientesPermitidos(Guid idUsuario)
        {
            var usuario = await Db.Usuarios.AsNoTracking()
                            .Include(x => x.UsuariosXGerenciadores)
                            .Include(x => x.UsuariosXClientes)
                            .Where(x => x.Id == idUsuario)
                            .FirstOrDefaultAsync();
           
            var idGerenciadores = new List<Guid>();

            foreach (var g in usuario.UsuariosXGerenciadores)
            {
                idGerenciadores.Add(g.IdGerenciador);
            }

            var idClientes = new List<Guid>();

            foreach (var g in usuario.UsuariosXClientes)
            {
                idClientes.Add(g.IdCliente);
            }

            return (idGerenciadores, idClientes);
        
        }
        
        public async Task<IEnumerable<Usuario>> ObterUsuariosPorCliente(Guid idCliente)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(u => u.Clientes.Where(g => g.Id == idCliente))
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosPorGerenciador(Guid idGerenciador)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(u => u.Gerenciadores.Where(g => g.Id == idGerenciador))
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosPorSetor(Guid idSetor)
        {
            return await Db.Usuarios.AsNoTracking()
                .Where(u => u.IdSetor == idSetor)
                .ToListAsync();
        }

       public async Task AdicionarUsuario(Usuario usuario)
        {
            foreach (var g in usuario.Gerenciadores)
            {
                Db.Gerenciadores.Attach(g);
            }
            
            foreach (var c in usuario.Clientes)
            {
                Db.Clientes.Attach(c);
            }

            Db.Add(usuario);

            await SaveChanges();
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            var newUsuario = Db.Usuarios
                               .AsNoTracking()
                               .Include(x => x.UsuariosXGerenciadores)
                               .Include(x => x.UsuariosXClientes)
                               .FirstOrDefault(x => x.Id == usuario.Id);

            if (newUsuario != null)
            {
                Db.TryUpdateManyToMany(newUsuario.UsuariosXGerenciadores, usuario.Gerenciadores
                  .Select(x => new UsuarioXGerenciador
                  {
                      IdGerenciador = x.Id,
                      IdUsuario = usuario.Id,
                  }), x => x.IdGerenciador);

                Db.TryUpdateManyToMany(newUsuario.UsuariosXClientes, usuario.Clientes
                  .Select(x => new UsuarioXCliente
                  {
                      IdCliente = x.Id,
                      IdUsuario = usuario.Id,
                  }), x => x.IdCliente);

                Db.Entry(newUsuario).CurrentValues.SetValues(usuario);
                Db.Usuarios.Update(newUsuario);
                await SaveChanges();
            }
        }
    }
}
