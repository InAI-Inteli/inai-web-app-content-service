using AutoMapper;
using WebAPIContentService.Domain.DTOs.Responses;
using WebAPIContentService.Domain.DTOs.ViewModels;
using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Domain.DTOs.Helpers
{
    public class MaterialUsuarioProfile : Profile
    {
        public MaterialUsuarioProfile()
        {
            CreateMap<MaterialUsuarioAddViewModel, MaterialUsuario>();
            CreateMap<MaterialUsuario, MaterialUsuarioAddViewModel>();
            CreateMap<MaterialUsuarioDto, MaterialUsuario>();
            CreateMap<MaterialUsuario, MaterialUsuarioDto>();
        }
    }
}