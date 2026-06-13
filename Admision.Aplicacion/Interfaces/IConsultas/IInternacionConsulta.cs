using System;
using System.Threading.Tasks;

namespace Admision.Aplicacion.Interfaces.IConsultas
{
    public interface IInternacionConsulta
    {
        Task<bool> TieneInternacionActivaAsync(Guid pacienteId);
    }
}