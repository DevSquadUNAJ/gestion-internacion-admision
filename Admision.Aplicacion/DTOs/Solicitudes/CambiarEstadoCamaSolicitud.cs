using System;
using System.Text.Json.Serialization;

namespace Admision.Aplicacion.DTOs.Solicitudes
{
    public class CambiarEstadoCamaSolicitud
    {
        [JsonIgnore]
        public Guid CamaId { get; set; }
        public string NuevoEstado { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
    }
}