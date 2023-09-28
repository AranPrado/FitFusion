using AutoMapper;
using FitFusion.Models;

namespace FitFusion.DTOs.Mapeamento
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioModel, UsuarioDTO>().ReverseMap();

            CreateMap<UsuarioDTO, LoginDTO>().ReverseMap();
        }
    }
}