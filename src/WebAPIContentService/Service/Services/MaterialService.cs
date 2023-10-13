using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Infra.Data.Repository.Interfaces;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Service.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
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
            await _materialRepository.UpdateMaterialAsync(material);
        }
        public async Task UpdateMaterialAsync(Material material)
        {
            await _materialRepository.UpdateMaterialAsync(material);
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _materialRepository.GetAllMaterialsAsync();
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _materialRepository.AddMaterialAsync(material);
        }

        public async Task<IEnumerable<Material>> GetMaterialByTituloAsync(string titulo)
        {
            return await _materialRepository.GetMaterialByTituloAsync(titulo);
        }

        public async Task<IEnumerable<Material>> GetMaterialsByIdDiretoriaAsync(int idDiretoria)
        {
            return await _materialRepository.GetMaterialsByIdDiretoriaAsync(idDiretoria);
        }
    }
}
