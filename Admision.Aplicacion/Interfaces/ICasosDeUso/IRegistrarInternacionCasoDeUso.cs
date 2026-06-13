using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IRegistrarInternacionCasoDeUso
    {
        Task<RegistrarInternacionRespuesta> EjecutarAsync(RegistrarInternacionSolicitud solicitud);
    }
}