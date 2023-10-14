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
        private readonly IMapper _mapper;

        public MaterialUsuarioController(IMaterialUsuarioService materialService, IMapper mapper)
        {
            _materialUsuarioService = materialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialUsuarioDto>>> GetMateriaisUsuario(int idUsuario)
        {
            IEnumerable<MaterialUsuario> materiaisUsuario = await _materialUsuarioService.GetAllMateriaisUsuarioAsync(idUsuario);

            IEnumerable<MaterialUsuarioDto> materiaisUsuarioResposta = _mapper.Map<IEnumerable<MaterialUsuarioDto>>(materiaisUsuario);

            return Ok(materiaisUsuarioResposta);
        }

        // Consultar um material pelo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialUsuarioDto>> GetMaterialUsuarioById(int id)
        {
            MaterialUsuario? materialUsuario = await _materialUsuarioService.GetMaterialUsuarioByIdAsync(id);

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
            MaterialUsuario materialUsuarioEntity = _mapper.Map<MaterialUsuario>(materialUsuario);

            await _materialUsuarioService.AddMaterialUsuarioAsync(materialUsuarioEntity);

            return Ok();
        }

        [HttpPut("alterarstatus/{id}")]
        public async Task<IActionResult> AlterarStatusMaterial(int id, StatusEnum status)
        {
            try
            {
                await _materialUsuarioService.AlterarStatusMaterialUsuarioAsync(id, status);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}