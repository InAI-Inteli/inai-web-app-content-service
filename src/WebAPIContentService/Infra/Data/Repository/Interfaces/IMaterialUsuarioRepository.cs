using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Repository.Interfaces
{
    public interface IMaterialUsuarioRepository
    {
        Task<MaterialUsuario?> GetMaterialUsuarioByIdAsync(int idMaterialUsuario);
        Task UpdateMaterialUsuarioAsync(MaterialUsuario material);
        Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario);
        Task AddMaterialUsuarioAsync(MaterialUsuario materialUsuario);
    }
}
