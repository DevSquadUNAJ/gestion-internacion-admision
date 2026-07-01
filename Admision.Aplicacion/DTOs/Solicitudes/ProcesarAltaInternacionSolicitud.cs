using System;
using System.Text.Json.Serialization;

namespace Admision.Aplicacion.DTOs.Solicitudes
{
    public class ProcesarAltaInternacionSolicitud
    {
        [JsonIgnore]
        public Guid InternacionId { get; set; }
        public string EstadoEgreso { get; set; } = string.Empty;
    }
}