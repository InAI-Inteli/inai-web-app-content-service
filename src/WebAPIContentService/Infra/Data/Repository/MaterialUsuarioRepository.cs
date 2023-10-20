using Microsoft.EntityFrameworkCore;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Infra.Data.Context;
using WebAPIContentService.Infra.Data.Repository.Interfaces;

namespace WebAPIContentService.Infra.Data.Repository
{
    public class MaterialUsuarioRepository : IMaterialUsuarioRepository
    {
        private readonly ContentContext _context;

        public MaterialUsuarioRepository(ContentContext context)
        {
            _context = context;
        }

        public async Task<MaterialUsuario?> GetMaterialUsuarioByIdAsync(int idMaterialUsuario)
        {
            return await _context.MaterialUsuarios.FirstOrDefaultAsync(m => m.IdMaterialUsuario == idMaterialUsuario);
        }

        public async Task UpdateMaterialUsuarioAsync(MaterialUsuario materialUsuario)
        {
            _context.Entry(materialUsuario).State = EntityState.Modified;
            materialUsuario.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario)
        {
            return await _context.MaterialUsuarios.Where(m => m.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task AddMaterialUsuarioAsync(MaterialUsuario materialUsuario)
        {
            materialUsuario.CreatedAt = DateTime.Now;
            materialUsuario.UpdateAt = DateTime.Now;
            _context.MaterialUsuarios.Add(materialUsuario);
            await _context.SaveChangesAsync();
        }
    }
}
