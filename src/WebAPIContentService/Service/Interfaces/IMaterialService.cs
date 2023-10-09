﻿using WebAPIContentService.Domain.Entities;

namespace WebAPIContentService.Service.Interfaces
{
    public interface IMaterialService
    {
        Task<Material> GetMaterialByIdAsync(int id);
        Task UpdateMaterialAsync(Material material);
        Task InativarMaterialAsync(int id);
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task AddMaterialAsync(Material material);
    }
}
