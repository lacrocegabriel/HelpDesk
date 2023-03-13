using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(HelpDeskContext db) : base(db)
        {
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
           foreach (var g in usuario.Gerenciadores)
            {
                Db.Gerenciadores.Attach(g);
            }

            foreach (var c in usuario.Clientes)
            {
                Db.Clientes.Attach(c);
            }
            Db.Usuarios.Update(usuario);
            await SaveChanges();

        }
    }
}
