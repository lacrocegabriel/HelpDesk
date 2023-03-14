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

            builder.Property(u => u.Ativo)
                .IsRequired();

            builder.HasOne(u => u.Setor)
                .WithMany(s => s.Usuarios)
                .HasForeignKey(u => u.IdSetor);

            builder.HasMany(u => u.ChamadosResponsavel)
                .WithOne(c => c.UsuarioResponsavel)
                .HasForeignKey(c => c.IdUsuarioResponsavel);
            
            builder.HasMany(u => u.ChamadosGerador)
                .WithOne(c => c.UsuarioGerador)
                .HasForeignKey(c => c.IdUsuarioGerador);

         builder.HasMany(u => u.UsuarioXClientes)
                .WithOne(uc => uc.Usuario)
                .HasForeignKey(uc => uc.IdUsuario);
       
        builder.HasMany(u => u.Clientes) // mapeia a nova propriedade
                .WithMany(c => c.Usuarios) // sem propriedade de navegação inversa
                .UsingEntity<UsuarioXCliente>(
                    j => j
                        .HasOne(uc => uc.Cliente)
                        .WithMany(c => c.UsuarioXClientes)
                        .HasForeignKey(uc => uc.IdCliente),
                    
                    j => j
                        .HasOne(uc => uc.Usuario)
                        .WithMany(u => u.UsuarioXClientes)
                        .HasForeignKey(uc => uc.IdUsuario),
                    
                    j =>
                    {
                        j.Property(uc => uc.VinculoAtivo).IsRequired().HasDefaultValue(true);
                    }
                );
            builder.HasMany(u => u.Gerenciadores) // mapeia a nova propriedade
                    .WithMany(c => c.Usuarios) // sem propriedade de navegação inversa
                    .UsingEntity<UsuarioXGerenciador>(
                        j => j
                            .HasOne(uc => uc.Gerenciador)
                            .WithMany(g => g.UsuarioXGerenciador)
                            .HasForeignKey(uc => uc.IdGerenciador),

                        j => j
                            .HasOne(uc => uc.Usuario)
                            .WithMany(g => g.UsuarioXGerenciador)
                            .HasForeignKey(uc => uc.IdUsuario),

                        j =>
                        {
                            j.Property(uc => uc.VinculoAtivo).IsRequired().HasDefaultValue(true);
                        }
                    );
            builder.ToTable("Usuarios");
        }
    }
}
