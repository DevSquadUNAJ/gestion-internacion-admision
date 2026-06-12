using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Dominio.Entidades;
using Admision.Infraestructura.Persistencia;

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
    }
}