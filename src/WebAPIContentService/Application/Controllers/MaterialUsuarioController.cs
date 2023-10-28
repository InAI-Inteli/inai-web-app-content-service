using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIContentService.Domain.DTOs.Responses;
using WebAPIContentService.Domain.DTOs.ViewModels;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Domain.Enumerations;
using WebAPIContentService.Service.Interfaces;

namespace WebAPIContentService.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialUsuarioController : ControllerBase
    {
        private readonly IMaterialUsuarioService _materialUsuarioService;
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public MaterialUsuarioController(IMaterialUsuarioService materialUsuarioService, IMaterialService materialService, IMapper mapper)
        {
            _materialUsuarioService = materialUsuarioService;
            _materialService = materialService;
            _mapper = mapper;
        }

        [HttpGet("materiaisusuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<MaterialUsuarioDto>>> GetMateriaisUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return BadRequest("ID de material de usuario invalido");
            }

            IEnumerable<MaterialUsuario> materiaisUsuario = await _materialUsuarioService.GetAllMateriaisUsuarioAsync(idUsuario);

            IEnumerable<MaterialUsuarioDto> materiaisUsuarioResposta = _mapper.Map<IEnumerable<MaterialUsuarioDto>>(materiaisUsuario);

            return Ok(materiaisUsuarioResposta);
        }

        // Consultar um material pelo Id
        [HttpGet("{idMaterialUsuario}")]
        public async Task<ActionResult<MaterialUsuarioDto>> GetMaterialUsuarioById(int idMaterialUsuario)
        {
            if (idMaterialUsuario <= 0)
            {
                return BadRequest("ID de material de usuario invalido");
            }

            MaterialUsuario? materialUsuario = await _materialUsuarioService.GetMaterialUsuarioByIdAsync(idMaterialUsuario);

            if (materialUsuario == null)
            {
                return NotFound();
            }

            MaterialUsuarioDto materialUsuarioResposta = _mapper.Map<MaterialUsuarioDto>(materialUsuario);

            return Ok(materialUsuarioResposta);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaterialUsuario(MaterialUsuarioAddViewModel materialUsuario)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            MaterialUsuario materialUsuarioEntity = _mapper.Map<MaterialUsuario>(materialUsuario);

            await _materialUsuarioService.AddMaterialUsuarioAsync(materialUsuarioEntity);

            string resourceUrl = $"/materialusuario/{materialUsuarioEntity.IdMaterialUsuario}";

            return Created(resourceUrl, null);
        }

        [HttpPut("alterarstatus/{id}")]
        public async Task<IActionResult> AlterarStatusMaterial(int id, [FromBody] StatusEnum status)
        {
            if (id <= 0)
            {
                return BadRequest("ID de material de usuario invalido");
            }

            if (!Enum.IsDefined(typeof(StatusEnum), status))
            {
                return BadRequest("Status invalido");
            }

            try
            {
                await _materialUsuarioService.AlterarStatusMaterialUsuarioAsync(id, status);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}