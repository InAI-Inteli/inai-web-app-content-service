using System.ComponentModel.DataAnnotations;
using WebAPIContentService.Domain.Enumerations;

namespace WebAPIContentService.Domain.DTOs.ViewModels
{
    public class MaterialUsuarioAddViewModel
    {
        [Required(ErrorMessage = "O campo 'IdUsuario' e obrigatorio.")]
        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo 'IdMaterial' e obrigatorio.")]
        public int? IdMaterial { get; set; }

        public int? Nota { get; set; }
        public StatusEnum Status { get; set; }
        public DateOnly? DataEntrega { get; set; }
        public bool? EntregouNoPrazo { get; set; }
    }
}
