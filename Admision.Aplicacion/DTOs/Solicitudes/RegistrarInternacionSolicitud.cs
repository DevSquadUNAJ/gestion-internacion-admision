using System;

namespace Admision.Aplicacion.DTOs.Solicitudes
{
    public class RegistrarInternacionSolicitud
    {
        public Guid PacienteId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public Guid? CamaId { get; set; }
    }
}