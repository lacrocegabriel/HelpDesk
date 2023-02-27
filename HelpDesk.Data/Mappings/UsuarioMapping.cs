using HelpDesk.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Login)
                .IsRequired()
                .HasColumnType("BIGINT");
            
            builder.HasOne(u => u.Setor)
                .WithMany(s => s.Usuarios)
                .HasForeignKey(u => u.IdSetor);

            builder.HasMany(u => u.ChamadosResponsavel)
                .WithOne(c => c.UsuarioResponsavel)
                .HasForeignKey(c => c.IdUsuarioResponsavel);
            
            builder.HasMany(u => u.ChamadosGerador)
                .WithOne(c => c.UsuarioGerador)
                .HasForeignKey(c => c.IdUsuarioGerador);
            
            builder.HasMany(u => u.Clientes)
                .WithMany(c => c.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                       "UsuarioXCliente",
                     j => j
                        .HasOne<Cliente>()
                        .WithMany()
                        .HasForeignKey("IdCliente"),
                     j => j
                    .HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey("IdUsuario")
                );

            builder.HasMany(u => u.Gerenciadores)
                .WithMany(g => g.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                       "UsuarioXGerenciador",
                     j => j
                        .HasOne<Gerenciador>()
                        .WithMany()
                        .HasForeignKey("IdGerenciador"),
                     j => j
                    .HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey("IdUsuario")
                );

            builder.ToTable("Usuarios");
        }
    }
}
