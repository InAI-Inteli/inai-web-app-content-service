using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Infra.Data.Context;

namespace WebAPIContentService.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly ContentContext _context;

        public MaterialsController(ContentContext context)
        {
            _context = context;
        }

        // GET: /materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            return await _context.Materials.ToListAsync();
        }

        // POST: /materials
        [HttpPost]
        public async Task<ActionResult<Material>> PostMaterial(Material material)
        {
            material.CreatedAt = DateTime.Now;
            material.UpdateAt = DateTime.Now;
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}