using System;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class TrasladarPacienteInternadoRespuesta
    {
        public Guid InternacionId { get; set; }
        public Guid CamaAnteriorId { get; set; }
        public Guid CamaNuevaId { get; set; }
        public DateTime FechaTraslado { get; set; }
    }
}