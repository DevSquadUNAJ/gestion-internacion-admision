using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;
using System.Threading.Tasks;

namespace Admision.Infraestructura.Comandos
{
    public class CamaComando : ICamaComando
    {
        private readonly ContextoBaseDeDatos _contexto;

        public CamaComando(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(Cama cama)
        {
            _contexto.Camas.Update(cama);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}