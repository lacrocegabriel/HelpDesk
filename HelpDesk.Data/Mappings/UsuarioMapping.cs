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
                .HasColumnType("Varchar(14)");

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
                   .UsingEntity<UsuarioXCliente>(
                       j => j
                           .HasOne(uc => uc.Cliente)
                           .WithMany(c => c.UsuarioXClientes)
                           .HasForeignKey(uc => uc.IdCliente),
                       
                       j => j
                           .HasOne(uc => uc.Usuario)
                           .WithMany(u => u.UsuariosXClientes)
                           .HasForeignKey(uc => uc.IdUsuario)
                   );
            builder.HasMany(u => u.Gerenciadores)
                    .WithMany(c => c.Usuarios)
                    .UsingEntity<UsuarioXGerenciador>(
                        j => j
                            .HasOne(uc => uc.Gerenciador)
                            .WithMany(g => g.UsuarioXGerenciador)
                            .HasForeignKey(uc => uc.IdGerenciador),

                        j => j
                            .HasOne(uc => uc.Usuario)
                            .WithMany(g => g.UsuariosXGerenciadores)
                            .HasForeignKey(uc => uc.IdUsuario)
                 );
            builder.ToTable("Usuarios");
        }
    }
}
