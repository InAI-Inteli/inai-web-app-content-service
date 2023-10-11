namespace WebAPIContentService.Domain.Entities
{
    public partial class Material
    {
        public Material()
        {
            MaterialUsuarios = new HashSet<MaterialUsuario>();
        }

        public int IdMaterial { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }
        public DateOnly? DataFinal { get; set; }
        public bool? Obrigatorio { get; set; }
        public string? Tipo { get; set; }
        public string? Url { get; set; }
        public int? PesoNota { get; set; }
        public int IdDiretoria { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int CreatedBy { get; set; }

        public virtual ICollection<MaterialUsuario> MaterialUsuarios { get; set; }
    }
}
