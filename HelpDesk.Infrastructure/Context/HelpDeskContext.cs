using HelpDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Context
{
    public class HelpDeskContext : DbContext
    {
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Gerenciador> Gerenciadores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioXGerenciador> UsuariosXGerenciadores { get; set; }
        public DbSet<UsuarioXCliente> UsuariosXClientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Tramite> Tramites { get; set; }
        public DbSet<TipoPessoa> TipoPessoas{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HelpDeskContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Entity<TipoPessoa>()
                .HasData(Enum.GetValues(typeof(Domain.Entities.Enums.TipoPessoa))
                .Cast<Domain.Entities.Enums.TipoPessoa>().Select(p => new TipoPessoa { Id = ((long)p), Tipo = p.ToString()}));

            modelBuilder.Entity<SituacaoChamado>()
                .HasData(Enum.GetValues(typeof(Domain.Entities.Enums.SituacaoChamado))
                .Cast<Domain.Entities.Enums.SituacaoChamado>().Select(p => new SituacaoChamado { Id = ((long)p), Situacao = p.ToString() }));

            base.OnModelCreating(modelBuilder);

        }

        
    }
    
}
