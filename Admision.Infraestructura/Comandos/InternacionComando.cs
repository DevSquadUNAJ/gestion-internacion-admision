using System.Threading.Tasks;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;

namespace Admision.Infraestructura.Comandos
{
    public class InternacionComando : IInternacionComando
    {
        private readonly ContextoBaseDeDatos _contexto;

        public InternacionComando(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task AgregarAsync(Internacion internacion)
        {
            await _contexto.Internaciones.AddAsync(internacion);
        }

        public async Task AgregarInternacionCamaAsync(InternacionCama internacionCama)
        {
            await _contexto.InternacionesCamas.AddAsync(internacionCama);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}