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

            builder.HasMany(c => c.UsuarioXGerenciador)
                    .WithOne(uc => uc.Gerenciador)
                    .HasForeignKey(uc => uc.IdGerenciador);

            builder.HasMany(c => c.Usuarios)
                   .WithMany(u => u.Gerenciadores);

            builder.ToTable("Gerenciadores");
        }
    }
}
