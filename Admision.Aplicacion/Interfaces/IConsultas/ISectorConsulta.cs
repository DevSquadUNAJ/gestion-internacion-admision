using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Interfaces.IConsultas
{
    public interface ISectorConsulta
    {
        Task<IEnumerable<Sector>> ObtenerTodosConCamasAsync();
    }
}