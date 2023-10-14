using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Domain.Enumerations;
using WebAPIContentService.Infra.Data.Repository;
using WebAPIContentService.Infra.Data.Repository.Interfaces;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Service.Services
{
    public class MaterialUsuarioService : IMaterialUsuarioService
    {
        private readonly IMaterialUsuarioRepository _materialUsuarioRepository;

        public MaterialUsuarioService(IMaterialUsuarioRepository materialRepository)
        {
            _materialUsuarioRepository = materialRepository;
        }
        public async Task AddMaterialUsuarioAsync(MaterialUsuario materialUsuario)
        {
            await _materialUsuarioRepository.AddMaterialUsuarioAsync(materialUsuario);
        }

        public async Task AlterarStatusMaterialUsuarioAsync(int idMaterialUsuario, StatusEnum status)
        {
            MaterialUsuario materialUsuario = await _materialUsuarioRepository.GetMaterialUsuarioByIdAsync(idMaterialUsuario) ?? throw new Exception("MaterialUsuario not found");
            materialUsuario.Status = status;
            await _materialUsuarioRepository.UpdateMaterialUsuarioAsync(materialUsuario);
        }

        public async Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario)
        {
            return await _materialUsuarioRepository.GetAllMateriaisUsuarioAsync(idUsuario);
        }

        public async Task<MaterialUsuario?> GetMaterialUsuarioByIdAsync(int idMaterialUsuario)
        {
            return await _materialUsuarioRepository.GetMaterialUsuarioByIdAsync(idMaterialUsuario);
        }
    }
}
