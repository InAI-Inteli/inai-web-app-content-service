using System.ComponentModel.DataAnnotations;

namespace WebAPIContentService.Domain.DTOs.ViewModels
{
    public class MaterialAddViewModel
    {
        [Required(ErrorMessage = "O campo 'Titulo' e obrigatorio.")]
        public string Titulo { get; set; }

        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }
        public DateOnly? DataFinal { get; set; }
        public bool? Obrigatorio { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo' e obrigatorio.")]
        public string Tipo { get; set; }

        public string? Url { get; set; }
        public int? PesoNota { get; set; }

        [Required(ErrorMessage = "O campo 'IdDiretoria' e obrigatorio.")]
        public int IdDiretoria { get; set; }
    }
}
