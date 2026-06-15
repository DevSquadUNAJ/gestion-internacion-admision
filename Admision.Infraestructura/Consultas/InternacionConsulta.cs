using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Admision.Infraestructura.Consultas
{
    public class InternacionConsulta : IInternacionConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public InternacionConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> TieneInternacionActivaAsync(Guid pacienteId)
        {
            return await _contexto.Internaciones
                .AsNoTracking()
                .AnyAsync(i => i.PacienteId == pacienteId
                            && i.Estado == EstadoInternacion.Activa);
        }

        public async Task<Internacion?> ObtenerPorIdAsync(Guid internacionId)
        {
            return await _contexto.Internaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == internacionId);
        }

        public async Task<InternacionCama?> ObtenerAsignacionActualAsync(Guid internacionId)
        {
            return await _contexto.InternacionesCamas
                .FirstOrDefaultAsync(ic => ic.InternacionId == internacionId
                                        && ic.EsActual);
        }
    }
}