using System;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerContextoInternacionCasoDeUso
    {
        Task<ContextoInternacionRespuesta> EjecutarAsync(Guid internacionId);
    }
}