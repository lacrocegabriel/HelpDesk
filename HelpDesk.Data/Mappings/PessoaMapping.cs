using HelpDesk.Business.Models;
using HelpDesk.Business.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HelpDesk.Data.Mappings
{
    public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("VARCHAR(14)");

            builder.Property(p => p.DataNascimentoConstituicao)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("Varchar(50)");

            builder.HasOne(p => p.TipoPessoa)
            .WithMany()
            .HasForeignKey(p => p.IdTipoPessoa)
            .IsRequired();

            builder.HasOne(p => p.Endereco)
                .WithOne(e => e.Pessoa)
                .HasForeignKey<Pessoa>(e => e.IdEndereco);

            builder.ToTable("Pessoas");
        }
    }
}
