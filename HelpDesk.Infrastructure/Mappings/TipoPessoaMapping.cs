using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class TipoPessoaMapping : IEntityTypeConfiguration<TipoPessoa>
    {
        public void Configure(EntityTypeBuilder<TipoPessoa> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Tipo)
                .IsRequired()
                .HasColumnType("Varchar(50)");

            builder.ToTable("TipoPessoa");
        }
    }
}
