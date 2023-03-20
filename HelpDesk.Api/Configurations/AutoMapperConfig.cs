using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Models;

namespace HelpDesk.Api.Configurations
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

        }
    }
}
