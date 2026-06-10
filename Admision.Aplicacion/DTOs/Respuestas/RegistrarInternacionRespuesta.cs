using System;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class RegistrarInternacionRespuesta
    {
        public Guid InternacionId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; } = string.Empty;
        public Guid? CamaAsignadaId { get; set; }
        public bool TieneCamaAsignada => CamaAsignadaId.HasValue;
    }
}