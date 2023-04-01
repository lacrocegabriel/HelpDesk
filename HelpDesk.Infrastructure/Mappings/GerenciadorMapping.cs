using HelpDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class GerenciadorMapping : IEntityTypeConfiguration<Gerenciador>
    {
        public void Configure(EntityTypeBuilder<Gerenciador> builder)
        {
            builder.HasOne(g => g.Pessoa)
               .WithMany()
               .HasForeignKey(g => g.Id);

            builder.HasMany(c => c.Usuarios)
                   .WithMany(u => u.Gerenciadores);

            builder.ToTable("Gerenciadores");
        }
    }
}
