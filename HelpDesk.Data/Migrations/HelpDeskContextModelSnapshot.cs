﻿// <auto-generated />
using System;
using HelpDesk.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    [DbContext(typeof(HelpDeskContext))]
    partial class HelpDeskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HelpDesk.Business.Models.Chamado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdGerenciador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdSituacaoChamado")
                        .HasColumnType("bigint");

                    b.Property<Guid>("IdUsuarioGerador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuarioResponsavel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Numero"));

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdGerenciador");

                    b.HasIndex("IdSituacaoChamado");

                    b.HasIndex("IdUsuarioGerador");

                    b.HasIndex("IdUsuarioResponsavel");

                    b.ToTable("Chamados", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Enderecos", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataNascimentoConstituicao")
                        .HasColumnType("DATE");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.Property<Guid>("IdEndereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdTipoPessoa")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdEndereco")
                        .IsUnique();

                    b.HasIndex("IdTipoPessoa");

                    b.ToTable("Pessoas", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Setor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(300)");

                    b.Property<Guid>("IdGerenciador")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdGerenciador");

                    b.ToTable("Setores", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.SituacaoChamado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SituacaoChamado", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Situacao = "EmAndamento"
                        },
                        new
                        {
                            Id = 2L,
                            Situacao = "Aguardando"
                        },
                        new
                        {
                            Id = 3L,
                            Situacao = "Fechado"
                        },
                        new
                        {
                            Id = 4L,
                            Situacao = "Cancelado"
                        });
                });

            modelBuilder.Entity("HelpDesk.Business.Models.TipoPessoa", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TipoPessoa", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Tipo = "PessoaJuridica"
                        },
                        new
                        {
                            Id = 2L,
                            Tipo = "PessoaFisica"
                        });
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Tramite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<Guid>("IdChamado")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuarioGerador")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdChamado");

                    b.HasIndex("IdUsuarioGerador");

                    b.ToTable("Tramites", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.UsuarioXCliente", b =>
                {
                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdCliente", "IdUsuario");

                    b.HasIndex("IdUsuario");

                    b.ToTable("UsuariosXClientes");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.UsuarioXGerenciador", b =>
                {
                    b.Property<Guid>("IdGerenciador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdGerenciador", "IdUsuario");

                    b.HasIndex("IdUsuario");

                    b.ToTable("UsuariosXGerenciadores");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Cliente", b =>
                {
                    b.HasBaseType("HelpDesk.Business.Models.Pessoa");

                    b.Property<Guid>("IdGerenciador")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("IdGerenciador");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Gerenciador", b =>
                {
                    b.HasBaseType("HelpDesk.Business.Models.Pessoa");

                    b.ToTable("Gerenciadores", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Usuario", b =>
                {
                    b.HasBaseType("HelpDesk.Business.Models.Pessoa");

                    b.Property<Guid>("IdSetor")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Login")
                        .HasColumnType("BIGINT");

                    b.HasIndex("IdSetor");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Chamado", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Cliente", "Cliente")
                        .WithMany("Chamados")
                        .HasForeignKey("IdCliente")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Gerenciador", "Gerenciador")
                        .WithMany("Chamados")
                        .HasForeignKey("IdGerenciador")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.SituacaoChamado", "SituacaoChamado")
                        .WithMany()
                        .HasForeignKey("IdSituacaoChamado")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Usuario", "UsuarioGerador")
                        .WithMany("ChamadosGerador")
                        .HasForeignKey("IdUsuarioGerador")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Usuario", "UsuarioResponsavel")
                        .WithMany("ChamadosResponsavel")
                        .HasForeignKey("IdUsuarioResponsavel")
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Gerenciador");

                    b.Navigation("SituacaoChamado");

                    b.Navigation("UsuarioGerador");

                    b.Navigation("UsuarioResponsavel");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Pessoa", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Endereco", "Endereco")
                        .WithOne("Pessoa")
                        .HasForeignKey("HelpDesk.Business.Models.Pessoa", "IdEndereco")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.TipoPessoa", "TipoPessoa")
                        .WithMany()
                        .HasForeignKey("IdTipoPessoa")
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("TipoPessoa");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Setor", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Gerenciador", "Gerenciador")
                        .WithMany("Setores")
                        .HasForeignKey("IdGerenciador")
                        .IsRequired();

                    b.Navigation("Gerenciador");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Tramite", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Chamado", "Chamado")
                        .WithMany("Tramites")
                        .HasForeignKey("IdChamado")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Usuario", "UsuarioGerador")
                        .WithMany("Tramites")
                        .HasForeignKey("IdUsuarioGerador")
                        .IsRequired();

                    b.Navigation("Chamado");

                    b.Navigation("UsuarioGerador");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.UsuarioXCliente", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Cliente", "Cliente")
                        .WithMany("UsuarioXClientes")
                        .HasForeignKey("IdCliente")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Usuario", "Usuario")
                        .WithMany("UsuariosXClientes")
                        .HasForeignKey("IdUsuario")
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.UsuarioXGerenciador", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Gerenciador", "Gerenciador")
                        .WithMany("UsuarioXGerenciador")
                        .HasForeignKey("IdGerenciador")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Usuario", "Usuario")
                        .WithMany("UsuariosXGerenciadores")
                        .HasForeignKey("IdUsuario")
                        .IsRequired();

                    b.Navigation("Gerenciador");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Cliente", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Gerenciador", "Gerenciador")
                        .WithMany("Clientes")
                        .HasForeignKey("IdGerenciador")
                        .IsRequired();

                    b.Navigation("Gerenciador");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Gerenciador", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Usuario", b =>
                {
                    b.HasOne("HelpDesk.Business.Models.Pessoa", null)
                        .WithOne()
                        .HasForeignKey("HelpDesk.Business.Models.Usuario", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpDesk.Business.Models.Setor", "Setor")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdSetor")
                        .IsRequired();

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Chamado", b =>
                {
                    b.Navigation("Tramites");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Endereco", b =>
                {
                    b.Navigation("Pessoa")
                        .IsRequired();
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Setor", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Cliente", b =>
                {
                    b.Navigation("Chamados");

                    b.Navigation("UsuarioXClientes");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Gerenciador", b =>
                {
                    b.Navigation("Chamados");

                    b.Navigation("Clientes");

                    b.Navigation("Setores");

                    b.Navigation("UsuarioXGerenciador");
                });

            modelBuilder.Entity("HelpDesk.Business.Models.Usuario", b =>
                {
                    b.Navigation("ChamadosGerador");

                    b.Navigation("ChamadosResponsavel");

                    b.Navigation("Tramites");

                    b.Navigation("UsuariosXClientes");

                    b.Navigation("UsuariosXGerenciadores");
                });
#pragma warning restore 612, 618
        }
    }
}
