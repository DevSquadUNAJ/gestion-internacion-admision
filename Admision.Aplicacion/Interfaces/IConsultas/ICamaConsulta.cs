using Admision.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admision.Aplicacion.Interfaces.IConsultas
{
    public interface ICamaConsulta
    {
        Task<Cama?> ObtenerPorIdAsync(Guid camaId);
        Task<IEnumerable<Cama>> ObtenerCamasPorSectorConPacienteAsync(Guid sectorId);
    }
}