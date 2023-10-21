using Microsoft.EntityFrameworkCore;
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

        public void UpdateMaterial(Material material)
        {
            _context.Entry(material).State = EntityState.Modified;
            material.UpdateAt = DateTime.Now;
        }

        public async Task<IEnumerable<Material>> GetAllMateriaisAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        public void AddMaterial(Material material)
        {
            material.CreatedAt = DateTime.Now;
            material.UpdateAt = DateTime.Now;
            _context.Materials.Add(material);
        }

        public async Task<IEnumerable<Material>> GetMaterialByTituloAsync(string titulo)
        {
            titulo = titulo?.ToLower() ?? "";
            return await _context.Materials
                .Where(m => m.Titulo != null && m.Titulo.ToLower().Contains(titulo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Material>> GetMateriaisByIdDiretoriaAsync(int idDiretoria)
        {
            return await _context.Materials
                .Where(m => m.IdDiretoria == idDiretoria)
                .ToListAsync();
        }
    }
}
