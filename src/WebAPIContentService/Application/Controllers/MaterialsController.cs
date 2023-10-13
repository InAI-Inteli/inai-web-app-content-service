using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIContentService.Domain.DTOs.Responses;
using WebAPIContentService.Domain.DTOs.ViewModels;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MateriaisController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public MateriaisController(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();

            IEnumerable<MaterialDto> materialsResposta = _mapper.Map<IEnumerable<MaterialDto>>(materials);

            return Ok(materialsResposta);
        }

        // Consultar um material pelo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDto>> GetMaterialById(int id)
        {
            Material? material = await _materialService.GetMaterialByIdAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            MaterialDto materialResposta = _mapper.Map<MaterialDto>(material);

            return Ok(materialResposta);
        }

        [HttpGet("diretoria/{id}")]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterialsByIdDiretoriaA(int id)
        {
            IEnumerable<Material> materials = await _materialService.GetMaterialsByIdDiretoriaAsync(id);

            if (materials == null || !materials.Any())
            {
                return Ok(new List<Material>());
            }

            IEnumerable<MaterialDto> materialsResposta = _mapper.Map<IEnumerable<MaterialDto>>(materials);

            return Ok(materialsResposta);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterialByTitulo([FromQuery] string titulo)
        {
            IEnumerable<Material> materials = await _materialService.GetMaterialByTituloAsync(titulo);

            if (materials == null || !materials.Any())
            {
                return Ok(new List<Material>());
            }

            IEnumerable<MaterialDto> materialsResposta = _mapper.Map<IEnumerable<MaterialDto>>(materials);

            return Ok(materialsResposta);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaterial(MaterialAddViewModel material)
        {

            Material materialEntity = _mapper.Map<Material>(material);

            await _materialService.AddMaterialAsync(materialEntity);

            return Ok();
        }

        [HttpPut("alterarstatus/{id}")]
        public async Task<IActionResult> AlterarStatusMaterial(int id)
        {
            try
            {
                await _materialService.AlterarStatusMaterialAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> UpdateMaterial(int id, MaterialUpdateViewModel material)
        {
            if (id != material.IdMaterial)
            {
                return BadRequest();
            }

            try
            {
                Material materialEntity = _mapper.Map<Material>(material);
                await _materialService.UpdateMaterialAsync(materialEntity);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}