using Admision.Dominio.Constantes;
using System;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class RegistrarInternacionRespuesta
    {
        public Guid InternacionId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public EstadoInternacion Estado { get; set; } = EstadoInternacion.Activa;
        public Guid? CamaAsignadaId { get; set; }
        public bool TieneCamaAsignada => CamaAsignadaId.HasValue;
    }
}