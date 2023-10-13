using AutoMapper;
using WebAPIContentService.Domain.DTOs.Responses;
using WebAPIContentService.Domain.DTOs.ViewModels;
using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Domain.DTOs.Helpers
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialAddViewModel, Material>();
            CreateMap<Material, MaterialAddViewModel>();
            CreateMap<MaterialUpdateViewModel, Material>();
            CreateMap<Material, MaterialUpdateViewModel>();
            CreateMap<MaterialDto, Material>();
            CreateMap<Material, MaterialDto>();
        }
    }
}
