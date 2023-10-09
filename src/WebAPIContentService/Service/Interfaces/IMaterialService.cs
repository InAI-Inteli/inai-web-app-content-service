using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Service.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task AddMaterialAsync(Material material);
    }
}
