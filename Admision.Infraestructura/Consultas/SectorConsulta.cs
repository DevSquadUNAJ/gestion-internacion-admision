using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;

namespace Admision.Infraestructura.Consultas
{
    public class SectorConsulta : ISectorConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public SectorConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Sector>> ObtenerTodosConCamasAsync()
        {
            return await _contexto.Sectores
                .Include(s => s.Camas)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}