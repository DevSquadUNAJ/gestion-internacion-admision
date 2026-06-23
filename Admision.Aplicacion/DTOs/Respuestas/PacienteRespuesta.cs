using System;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class PacienteRespuesta
    {
        public Guid PacienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
    }
}