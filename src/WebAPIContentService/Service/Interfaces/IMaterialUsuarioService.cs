using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Domain.Enumerations;

namespace WebAPIContentService.Service.Interfaces
{
    public interface IMaterialUsuarioService
    {
        Task<MaterialUsuario?> GetMaterialUsuarioByIdAsync(int id);
        Task AlterarStatusMaterialUsuarioAsync(int id, StatusEnum status);
        Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario);
        Task AddMaterialUsuarioAsync(MaterialUsuario materialUsuario);
        Task<bool> MaterialExisteAsync(int id);
    }
}
