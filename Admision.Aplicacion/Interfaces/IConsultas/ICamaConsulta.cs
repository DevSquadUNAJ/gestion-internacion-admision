using System;
using System.Threading.Tasks;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Interfaces.IConsultas
{
    public interface ICamaConsulta
    {
        Task<Cama?> ObtenerPorIdAsync(Guid camaId);
    }
}