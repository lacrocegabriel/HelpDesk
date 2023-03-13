using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Business.Models;

namespace HelpDesk.Data.Mappings
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
