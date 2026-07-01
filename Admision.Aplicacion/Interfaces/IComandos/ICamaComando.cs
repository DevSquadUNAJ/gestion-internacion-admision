using Admision.Dominio.Entidades;
using System.Threading.Tasks;

namespace Admision.Aplicacion.Interfaces.IComandos
{
    public interface ICamaComando
    {
        void Actualizar(Cama cama);
        Task GuardarCambiosAsync();
    }
}