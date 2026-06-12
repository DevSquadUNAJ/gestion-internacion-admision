using System;
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
    }
}