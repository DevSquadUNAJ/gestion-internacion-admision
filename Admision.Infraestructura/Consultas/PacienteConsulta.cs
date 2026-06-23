using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;

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
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni);
        }
    }
}