using Admision.Dominio.Constantes;
using System;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class CambiarEstadoCamaRespuesta
    {
        public Guid CamaId { get; set; }
        public EstadoCama EstadoAnterior { get; set; }
        public EstadoCama EstadoNuevo { get; set; }
        public DateTime FechaCambio { get; set; }
    }
}