using System;

namespace Admision.Aplicacion.DTOs.Solicitudes
{
    public class TrasladarPacienteInternadoSolicitud
    {
        public Guid InternacionId { get; set; }
        public Guid CamaDestinoId { get; set; }
        public string MotivoTraslado { get; set; } = string.Empty;
    }
}