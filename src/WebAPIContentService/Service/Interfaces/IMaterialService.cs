﻿using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Service.Interfaces
{
    public interface IMaterialService
    {
        Task<Material?> GetMaterialByIdAsync(int id);
        Task UpdateMaterialAsync(Material material);
        Task AlterarStatusMaterialAsync(int id);
        Task<IEnumerable<Material>> GetAllMateriaisAsync();
        Task AddMaterialAsync(Material material);
        Task<IEnumerable<Material>> GetMaterialByTituloAsync(string titulo);
        Task<IEnumerable<Material>> GetMateriaisByIdDiretoriaAsync(int idDiretoria);
    }
}
