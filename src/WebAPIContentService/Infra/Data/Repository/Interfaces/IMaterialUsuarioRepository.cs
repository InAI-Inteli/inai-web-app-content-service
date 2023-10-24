using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Infra.Data.Repository.Interfaces
{
    public interface IMaterialUsuarioRepository
    {
        Task<MaterialUsuario?> GetMaterialUsuarioByIdAsync(int idMaterialUsuario);
        void UpdateMaterialUsuario(MaterialUsuario material);
        Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario);
        void AddMaterialUsuario(MaterialUsuario materialUsuario);
    }
}
