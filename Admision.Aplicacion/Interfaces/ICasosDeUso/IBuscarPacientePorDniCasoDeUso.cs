using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Respuestas;

namespace Admision.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IBuscarPacientePorDniCasoDeUso
    {
        Task<PacienteRespuesta> EjecutarAsync(string dni);
    }
}