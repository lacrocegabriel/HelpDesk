using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class SetorMapping : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(300)");

            builder.HasOne(s => s.Gerenciador)
                .WithMany(g => g.Setores)
                .HasForeignKey(s => s.IdGerenciador);

            builder.ToTable("Setores");
        }
    }
}
