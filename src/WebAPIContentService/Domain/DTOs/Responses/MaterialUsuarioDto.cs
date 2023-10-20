﻿using WebAPIContentService.Domain.Enumerations;

namespace WebAPIContentService.Domain.DTOs.Responses
{
    public class MaterialUsuarioDto
    {
        public int IdUsuario { get; set; }
        public int IdMaterial { get; set; }
        public int IdMaterialusuario { get; set; }
        public int? Nota { get; set; }
        public StatusEnum Status { get; set; }
        public DateOnly? DataEntrega { get; set; }
        public bool? EntregouNoPrazo { get; set; }
    }
}
