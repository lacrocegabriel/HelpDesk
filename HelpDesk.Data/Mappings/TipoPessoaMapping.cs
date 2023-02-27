using HelpDesk.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HelpDesk.Data.Mappings
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
