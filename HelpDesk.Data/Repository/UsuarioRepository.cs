using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using HelpDesk.Data.Extensions;
using HelpDesk.Data.Migrations;
using Microsoft.EntityFrameworkCore;


namespace HelpDesk.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(HelpDeskContext db) : base(db)
        {
        }

        public async Task<Usuario> ObterUsuarioPorAutenticacao(Guid idUsuarioAutenticado)
        {
            return await Db.Usuarios.AsNoTracking()
                    .Include(x => x.UsuariosXGerenciadores)
                    .Include(x => x.UsuariosXClientes)
                    .Where(x => x.Id == idUsuarioAutenticado)
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

        public async Task<IEnumerable<Usuario>> ObterTodosChamadosUsuario(Guid idUsuario)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(u => u.ChamadosGerador)
                .Include(u => u.ChamadosResponsavel)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterChamadosGeradorUsuario(Guid idUsuario)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(u => u.ChamadosGerador)
                .Where(u => u.Id == idUsuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterChamadosResponsavelUsuario(Guid idUsuario)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(u => u.ChamadosResponsavel)
                .Where(u => u.Id == idUsuario)
                .ToListAsync();
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
