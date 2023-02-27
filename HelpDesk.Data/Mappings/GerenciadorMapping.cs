using HelpDesk.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Mappings
{
    public class GerenciadorMapping : IEntityTypeConfiguration<Gerenciador>
    {
        public void Configure(EntityTypeBuilder<Gerenciador> builder)
        {
            builder.HasOne(g => g.Pessoa)
               .WithMany()
               .HasForeignKey(g => g.Id);

            builder.HasMany(g => g.Usuarios)
                .WithMany(u => u.Gerenciadores)
                .UsingEntity<Dictionary<string, object>>(
                       "UsuarioXGerenciador",
                     j => j
                        .HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("IdUsuario"),
                     j => j
                    .HasOne<Gerenciador>()
                    .WithMany()
                    .HasForeignKey("IdGerenciador")
                );

            builder.ToTable("Gerenciadores");
        }
    }
}
