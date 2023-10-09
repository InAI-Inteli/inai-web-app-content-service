using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Repository.Interfaces
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task AddMaterialAsync(Material material);
    }
}
