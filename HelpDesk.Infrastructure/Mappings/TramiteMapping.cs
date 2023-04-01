using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Data.Mappings
{
    public class TramiteMapping : IEntityTypeConfiguration<Tramite>
    {
        public void Configure(EntityTypeBuilder<Tramite> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.HasOne(t => t.Chamado)
                .WithMany(c => c.Tramites)
                .HasForeignKey(t => t.IdChamado);

            builder.HasOne(c => c.UsuarioGerador)
                .WithMany(x => x.Tramites)
                .HasForeignKey(c => c.IdUsuarioGerador);

            builder.ToTable("Tramites");
        }
    }
}
