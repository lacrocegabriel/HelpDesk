using HelpDesk.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data.Context
{
    public class HelpDeskContext : DbContext
    {
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Gerenciador> Gerenciadores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<TipoPessoa> TipoPessoas{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HelpDeskContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Entity<TipoPessoa>()
                .HasData(Enum.GetValues(typeof(Business.Models.Enums.TipoPessoa))
                .Cast<Business.Models.Enums.TipoPessoa>().Select(p => new TipoPessoa { Id = ((long)p), Tipo = p.ToString()}));

            base.OnModelCreating(modelBuilder);
        }
    }
}
