﻿using Microsoft.EntityFrameworkCore;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Infra.Data.Context;
using WebAPIContentService.Infra.Data.Repository.Interfaces;

namespace WebAPIContentService.Infra.Data.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ContentContext _context;

        public MaterialRepository(ContentContext context)
        {
            _context = context;
        }

        public async Task<Material?> GetMaterialByIdAsync(int id)
        {
            return await _context.Materials.FirstOrDefaultAsync(m => m.IdMaterial == id);
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            _context.Entry(material).State = EntityState.Modified;
            material.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task AddMaterialAsync(Material material)
        {
            material.CreatedAt = DateTime.Now;
            material.UpdateAt = DateTime.Now;
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Material>> GetMaterialByTituloAsync(string titulo)
        {
            titulo = titulo?.ToLower() ?? "";
            return await _context.Materials
                .Where(m => m.Titulo != null && m.Titulo.ToLower().Contains(titulo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Material>> GetMaterialsByIdDiretoriaAsync(int idDiretoria)
        {
            return await _context.Materials
                .Where(m => m.IdDiretoria == idDiretoria)
                .ToListAsync();
        }
    }
}
