using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface ICambiarEstadoCamaCasoDeUso
    {
        Task<CambiarEstadoCamaRespuesta> EjecutarAsync(CambiarEstadoCamaSolicitud solicitud);
    }
}