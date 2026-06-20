using Admision.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Admision.Aplicacion.Interfaces.IConsultas
{
    public interface IInternacionConsulta
    {
        Task<bool> TieneInternacionActivaAsync(Guid pacienteId);
        Task<Internacion?> ObtenerPorIdAsync(Guid internacionId);
        Task<Internacion?> ObtenerPorIdParaActualizarAsync(Guid internacionId);
        Task<InternacionCama?> ObtenerAsignacionActualAsync(Guid internacionId);
        Task<Internacion?> ObtenerConContextoCompletoAsync(Guid internacionId);
    }
}