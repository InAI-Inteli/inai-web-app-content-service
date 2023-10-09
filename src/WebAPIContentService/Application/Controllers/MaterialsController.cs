using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Infra.Data.Context;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            return Ok(await _materialService.GetAllMaterialsAsync());
        }

        // Consultar um material pelo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterialById(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaterial(Material material)
        {
            await _materialService.AddMaterialAsync(material);
            return Ok();
        }

        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarMaterial(int id)
        {
            try
            {
                await _materialService.InativarMaterialAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> UpdateMaterial(int id, Material material)
        {
            if (id != material.IdMaterial)
            {
                return BadRequest();
            }

            try
            {
                await _materialService.UpdateMaterialAsync(material);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}