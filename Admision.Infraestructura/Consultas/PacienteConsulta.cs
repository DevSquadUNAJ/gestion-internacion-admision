using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Admision.Infraestructura.Consultas
{
    public class PacienteConsulta : IPacienteConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public PacienteConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<Paciente?> ObtenerPorIdAsync(Guid pacienteId)
        {
            return await _contexto.Pacientes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == pacienteId);
        }

        public async Task<Paciente?> ObtenerPorDniAsync(string dni)
        {
            return await _contexto.Pacientes
                .Include(p => p.Internaciones.Where(i => i.Estado == EstadoInternacion.Activa))
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni);
        }
    }
}