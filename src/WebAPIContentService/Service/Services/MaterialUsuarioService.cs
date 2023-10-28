using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Domain.Enumerations;
using WebAPIContentService.Infra.Data.Repository;
using WebAPIContentService.Infra.Data.Repository.Interfaces;
using WebAPIContentService.Infra.Data.UnitOfWork;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Service.Services
{
    public class MaterialUsuarioService : IMaterialUsuarioService
    {
        private readonly IMaterialUsuarioRepository _materialUsuarioRepository;
        private readonly IUnitOfWork _uow;

        public MaterialUsuarioService(IMaterialUsuarioRepository materialRepository, IUnitOfWork uow)
        {
            _materialUsuarioRepository = materialRepository;
            _uow = uow;
        }
        public async Task AddMaterialUsuarioAsync(MaterialUsuario materialUsuario)
        {
            _materialUsuarioRepository.AddMaterialUsuario(materialUsuario);
            await _uow.Commit();
        }

        public async Task AlterarStatusMaterialUsuarioAsync(int idMaterialUsuario, StatusEnum status)
        {
            MaterialUsuario materialUsuario = await _materialUsuarioRepository.GetMaterialUsuarioByIdAsync(idMaterialUsuario) ?? throw new Exception("MaterialUsuario not found");
            materialUsuario.Status = status;
            _materialUsuarioRepository.UpdateMaterialUsuario(materialUsuario);
            await _uow.Commit();
        }

        public async Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario)
        {
            return await _materialUsuarioRepository.GetAllMateriaisUsuarioAsync(idUsuario);
        }

        public async Task<MaterialUsuario?> GetMaterialUsuarioByIdAsync(int idMaterialUsuario)
        {
            return await _materialUsuarioRepository.GetMaterialUsuarioByIdAsync(idMaterialUsuario);
        }
        public async Task<bool> MaterialExisteAsync(int id)
        {
            return await _materialUsuarioRepository.MaterialExisteAsync(id);
        }
        public async Task<bool> UsuarioJaPossuiMaterialAsync(int idUsuario, int idMaterial)
        {
            return await _materialUsuarioRepository.UsuarioJaPossuiMaterialAsync(idUsuario, idMaterial);
        }
    }
}
