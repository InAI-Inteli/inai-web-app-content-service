using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Repository.Interfaces
{
    public interface IMaterialRepository
    {
        Task<Material?> GetMaterialByIdAsync(int id);
        Task UpdateMaterialAsync(Material material);
        Task<IEnumerable<Material>> GetAllMateriaisAsync();
        Task AddMaterialAsync(Material material);
        Task<IEnumerable<Material>> GetMaterialByTituloAsync(string titulo);
        Task<IEnumerable<Material>> GetMateriaisByIdDiretoriaAsync(int idDiretoria);
    }
}
