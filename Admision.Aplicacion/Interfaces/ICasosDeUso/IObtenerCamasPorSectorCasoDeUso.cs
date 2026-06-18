using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerCamasPorSectorCasoDeUso
    {
        Task<IEnumerable<DetalleCamaRespuesta>> EjecutarAsync(Guid sectorId);
    }
}