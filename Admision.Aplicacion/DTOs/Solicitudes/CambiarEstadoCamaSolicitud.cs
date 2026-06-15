using System;

namespace Admision.Aplicacion.DTOs.Solicitudes
{
    public class CambiarEstadoCamaSolicitud
    {
        public Guid CamaId { get; set; }
        public string NuevoEstado { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
    }
}