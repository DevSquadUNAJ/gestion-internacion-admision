using System;
using System.Text.Json.Serialization;

namespace Admision.Aplicacion.DTOs.Solicitudes
{
    public class TrasladarPacienteInternadoSolicitud
    {
        [JsonIgnore]
        public Guid InternacionId { get; set; }
        public Guid CamaDestinoId { get; set; }
        public string MotivoTraslado { get; set; } = string.Empty;
    }
}