using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;

namespace Admision.Infraestructura.Consultas
{
    public class CamaConsulta : ICamaConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public CamaConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<Cama?> ObtenerPorIdAsync(Guid camaId)
        {
            return await _contexto.Camas
                .FirstOrDefaultAsync(c => c.Id == camaId);
        }

        public async Task<IEnumerable<Cama>> ObtenerCamasPorSectorConPacienteAsync(Guid sectorId)
        {
            return await _contexto.Camas
                .Include(c => c.HistorialInternaciones.Where(hi => hi.EsActual))
                    .ThenInclude(hi => hi.Internacion)
                        .ThenInclude(i => i.Paciente)
                .Where(c => c.SectorId == sectorId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}