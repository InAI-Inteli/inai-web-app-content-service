using System;
using System.Collections.Generic;

namespace WebAPIContentService.Domain.Entities
{
    public partial class MaterialUsuario
    {
        public int IdUsuario { get; set; }
        public int IdMaterial { get; set; }
        public int IdMaterialusuario { get; set; }
        public int? Nota { get; set; }
        public string? Status { get; set; }
        public DateOnly? DataEntrega { get; set; }
        public bool? EntregouNoPrazo { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Material IdMaterialNavigation { get; set; } = null!;
    }
}
