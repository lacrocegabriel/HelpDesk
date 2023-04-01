using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class SituacaoChamadoMapping : IEntityTypeConfiguration<SituacaoChamado>
    {
        public void Configure(EntityTypeBuilder<SituacaoChamado> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Situacao)
                .IsRequired()
                .HasColumnType("Varchar(50)");

            builder.ToTable("SituacaoChamado");
        }
    }
}
