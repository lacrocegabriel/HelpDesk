using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using HelpDesk.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioRepository(HelpDeskContext db, UserManager<IdentityUser> userManager) : base(db)
        {
            _userManager = userManager;
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

        public async Task<(List<string> ,bool)> AdicionarUsuario(Usuario usuario)
        {
            var errors = new List<string>();

            foreach (var g in usuario.Gerenciadores)
            {
                Db.Gerenciadores.Attach(g);
            }
            
            foreach (var c in usuario.Clientes)
            {
                Db.Clientes.Attach(c);
            }

            var user = new IdentityUser
            {
                UserName = usuario.Login,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            Db.Database.BeginTransaction();

            Db.Add(usuario);

            await SaveChanges();

            var result = await _userManager.CreateAsync(user, usuario.Senha);

            if(result.Succeeded)
            {
                Db.Database.CommitTransaction();
                
                return (new List<string>(), true);
            }

            Db.Database.RollbackTransaction();
            
            foreach(var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return (errors, false);

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
