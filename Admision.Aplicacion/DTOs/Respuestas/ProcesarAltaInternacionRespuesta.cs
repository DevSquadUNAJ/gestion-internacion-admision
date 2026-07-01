using System;
using Admision.Dominio.Constantes;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class ProcesarAltaInternacionRespuesta
    {
        public Guid InternacionId { get; set; }
        public EstadoInternacion EstadoEgreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public Guid? CamaLiberadaId { get; set; }
    }
}