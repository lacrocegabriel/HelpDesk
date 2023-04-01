using System.Security.Claims;
using AutoMapper;
using HelpDesk.Domain.Models;
using HelpDesk.Services.Api.DTOs;

namespace HelpDesk.Services.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
           CreateMap<Gerenciador, GerenciadorDto>().ReverseMap();
           CreateMap<Cliente, ClienteDto>().ReverseMap();
           CreateMap<Pessoa, PessoaDto>().ReverseMap();
           CreateMap<Usuario, UsuarioDto>().ReverseMap();
           CreateMap<Endereco, EnderecoDto>().ReverseMap();
           CreateMap<Chamado, ChamadoDto>().ReverseMap();
           CreateMap<Tramite, TramiteDto>().ReverseMap();
           CreateMap<Setor, SetorDto>().ReverseMap();          
           CreateMap<Claim, ClaimDto>().ReverseMap();          

        }
    }
}
