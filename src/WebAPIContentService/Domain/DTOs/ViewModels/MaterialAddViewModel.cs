namespace WebAPIContentService.Domain.DTOs.ViewModels
{
    public class MaterialAddViewModel
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }
        public DateOnly? DataFinal { get; set; }
        public bool? Obrigatorio { get; set; }
        public string? Tipo { get; set; }
        public string? Url { get; set; }
        public int? PesoNota { get; set; }
        public int IdDiretoria { get; set; }
    }
}
