using System.Security.Claims;
using AutoMapper;
using HelpDesk.Domain.Entities;
using HelpDesk.Services.Api.DTOs;

namespace HelpDesk.Services.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
           CreateMap<Gerenciador, GerenciadorDto>().ReverseMap();
           CreateMap<ClienteDto, Cliente>();
           CreateMap<Cliente, ClienteDto>()
               .ForMember(dest => dest.NomeGerenciador,
                   opt => opt.MapFrom(src => src.Gerenciador.Nome));
           CreateMap<Pessoa, PessoaDto>().ReverseMap();
           CreateMap<UsuarioDto, Usuario>();
           CreateMap<Usuario, UsuarioDto>().ForMember(dest => dest.Setor,
               opt => opt.MapFrom(src => src.Setor.Descricao));
           CreateMap<Endereco, EnderecoDto>().ReverseMap();
           CreateMap<TramiteDto, Tramite>().ReverseMap();
           CreateMap<Tramite, TramiteDto>()
               .ForMember(dest => dest.NomeUsuarioGerador,
               opt => opt.MapFrom(src => src.UsuarioGerador.Nome));
            CreateMap<SetorDto, Setor>().ReverseMap();          
           CreateMap<Setor, SetorDto>().ForMember(dest => dest.NomeGerenciador,
               opt => opt.MapFrom(src => src.Gerenciador.Nome));
           CreateMap<Claim, ClaimDto>().ReverseMap();
           CreateMap<ChamadoDto, Chamado>();
           CreateMap<Chamado, ChamadoDto>()
               .ForMember(dest => dest.NomeGerenciador, 
                   opt => opt.MapFrom(src => src.Gerenciador.Nome))
               .ForMember(dest => dest.NomeCliente,
                   opt => opt.MapFrom(src => src.Cliente.Nome))
               .ForMember(dest => dest.NomeUsuarioGerador,
                   opt => opt.MapFrom(src => src.UsuarioGerador.Nome))
               .ForMember(dest => dest.NomeUsuarioResponsavel,
                   opt => opt.MapFrom(src => src.UsuarioResponsavel.Nome))
               .ForMember(dest => dest.DescricaoSituacaoChamado,
                   opt => opt.MapFrom(src => src.SituacaoChamado.Situacao));

        }
    }
}
