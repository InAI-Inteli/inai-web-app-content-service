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

        public void UpdateMaterialUsuario(MaterialUsuario materialUsuario)
        {
            _context.Entry(materialUsuario).State = EntityState.Modified;
            materialUsuario.UpdateAt = DateTime.Now;
        }

        public async Task<IEnumerable<MaterialUsuario>> GetAllMateriaisUsuarioAsync(int idUsuario)
        {
            return await _context.MaterialUsuarios.Where(m => m.IdUsuario == idUsuario).ToListAsync();
        }

        public void AddMaterialUsuario(MaterialUsuario materialUsuario)
        {
            materialUsuario.CreatedAt = DateTime.Now;
            materialUsuario.UpdateAt = DateTime.Now;
            _context.MaterialUsuarios.Add(materialUsuario);
        }
        public async Task<bool> MaterialExisteAsync(int id)
        {
            return await _context.Materials.AnyAsync(m => m.IdMaterial == id);
        }
        public async Task<bool> UsuarioJaPossuiMaterialAsync(int idUsuario, int idMaterial)
        {
            return await _context.MaterialUsuarios.AnyAsync(mu => mu.IdUsuario == idUsuario && mu.IdMaterial == idMaterial);
        }
    }
}
