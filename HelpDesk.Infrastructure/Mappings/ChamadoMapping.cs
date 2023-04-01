using HelpDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class ChamadoMapping : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Numero)
                .IsRequired()
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.HasOne(p => p.SituacaoChamado)
            .WithMany()
            .HasForeignKey(p => p.IdSituacaoChamado)
            .IsRequired();

            builder.HasOne(c => c.Gerenciador)
                .WithMany(g => g.Chamados)
                .HasForeignKey(c => c.IdGerenciador);
        
            builder.HasOne(c => c.Cliente)
                .WithMany(x => x.Chamados)
                .HasForeignKey(c => c.IdCliente);

            builder.ToTable("Chamados");
        }
    }
}
