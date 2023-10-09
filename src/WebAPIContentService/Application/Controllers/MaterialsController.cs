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

        [HttpPost]
        public async Task<IActionResult> PostMaterial(Material material)
        {
            await _materialService.AddMaterialAsync(material);
            return Ok();
        }
    }
}