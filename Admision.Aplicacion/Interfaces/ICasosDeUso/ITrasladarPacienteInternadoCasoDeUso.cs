using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface ITrasladarPacienteInternadoCasoDeUso
    {
        Task<TrasladarPacienteInternadoRespuesta> EjecutarAsync(TrasladarPacienteInternadoSolicitud solicitud);
    }
}