using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Infra.Data.Repository.Interfaces;
using WebAPIContentService.Infra.Data.UnitOfWork;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Service.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _uow;

        public MaterialService(IMaterialRepository materialRepository, IUnitOfWork uow)
        {
            _materialRepository = materialRepository;
            _uow = uow;
        }

        public async Task<Material?> GetMaterialByIdAsync(int id)
        {
            return await _materialRepository.GetMaterialByIdAsync(id);
        }
        public async Task AlterarStatusMaterialAsync(int id)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(id);

            if (material == null)
            {
                throw new Exception("Material not found");
            }

            material.Ativo = !material.Ativo;
            _materialRepository.UpdateMaterial(material);
            await _uow.Commit();
        }
        public async Task UpdateMaterialAsync(Material material)
        {
            _materialRepository.UpdateMaterial(material);
            await _uow.Commit();
        }

        public async Task<IEnumerable<Material>> GetAllMateriaisAsync()
        {
            return await _materialRepository.GetAllMateriaisAsync();
        }

        public async Task AddMaterialAsync(Material material)
        {
            _materialRepository.AddMaterial(material);
            await _uow.Commit();
        }

        public async Task<IEnumerable<Material>> GetMaterialByTituloAsync(string titulo)
        {
            return await _materialRepository.GetMaterialByTituloAsync(titulo);
        }

        public async Task<IEnumerable<Material>> GetMateriaisByIdDiretoriaAsync(int idDiretoria)
        {
            return await _materialRepository.GetMateriaisByIdDiretoriaAsync(idDiretoria);
        }
    }
}
