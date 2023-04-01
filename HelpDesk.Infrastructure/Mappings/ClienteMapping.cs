using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasOne(c => c.Pessoa)
               .WithMany()
               .HasForeignKey(c => c.Id);

            builder.HasOne(c => c.Gerenciador)
                .WithMany(g => g.Clientes)
                .HasForeignKey(c => c.IdGerenciador);

            builder.HasMany(c => c.Usuarios)
                   .WithMany(u => u.Clientes);

            builder.ToTable("Clientes");
        }
    }
}
