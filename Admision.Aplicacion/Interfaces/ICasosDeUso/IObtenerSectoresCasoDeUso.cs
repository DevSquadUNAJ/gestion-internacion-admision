using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerSectoresCasoDeUso
    {
        Task<IEnumerable<SectorOcupacionRespuesta>> EjecutarAsync();
    }
}