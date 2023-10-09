using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Repository.Interfaces
{
    public interface IMaterialRepository
    {
        Task<Material> GetMaterialByIdAsync(int id);
        Task UpdateMaterialAsync(Material material);
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task AddMaterialAsync(Material material);

    }
}
