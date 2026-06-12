using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Constantes;
using Admision.Infraestructura.Persistencia;

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
    }
}