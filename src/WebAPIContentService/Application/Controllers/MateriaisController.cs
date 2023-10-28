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
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMateriais()
        {
            IEnumerable<Material> materiais = await _materialService.GetAllMateriaisAsync();

            IEnumerable<MaterialDto> materiaisResposta = _mapper.Map<IEnumerable<MaterialDto>>(materiais);

            return Ok(materiaisResposta);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaterialDto>> GetMaterialById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de material invalido");
            }

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
            if (id <= 0)
            {
                return BadRequest("ID de diretoria invalido");
            }

            IEnumerable<Material> materiais = await _materialService.GetMateriaisByIdDiretoriaAsync(id);

            IEnumerable<MaterialDto> materialsResposta = _mapper.Map<IEnumerable<MaterialDto>>(materiais);

            return Ok(materialsResposta);
        }

        [HttpGet("search:string")]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterialByTitulo([FromQuery] string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                return BadRequest("O parametro 'titulo' e invalido ou em branco.");
            }

            IEnumerable<Material> materials = await _materialService.GetMaterialByTituloAsync(titulo);

            IEnumerable<MaterialDto> materialsResposta = _mapper.Map<IEnumerable<MaterialDto>>(materials);

            return Ok(materialsResposta);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaterial(MaterialAddViewModel material)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }
            
            if (await _materialService.MaterialMesmoNomeAsync(material.Titulo))
            {
                return BadRequest("Já existe um material com o mesmo nome cadastrado.");
            }

            Material materialEntity = _mapper.Map<Material>(material);

            await _materialService.AddMaterialAsync(materialEntity);

            string resourceUrl = $"/materiais/{materialEntity.IdMaterial}";

            return Created(resourceUrl, null);
        }

        [HttpPut("alterarstatus/{id:int}")]
        public async Task<IActionResult> AlterarStatusMaterial(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de material invalido");
            }

            try
            {
                await _materialService.AlterarStatusMaterialAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("editar/{id:int}")]
        public async Task<IActionResult> UpdateMaterial(int id, MaterialUpdateViewModel material)
        {
            if (id <= 0 || id != material.IdMaterial)
            {
                return BadRequest("ID de material invalido ou incompativel.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            try
            {
                Material materialEntity = _mapper.Map<Material>(material);
                await _materialService.UpdateMaterialAsync(materialEntity);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}