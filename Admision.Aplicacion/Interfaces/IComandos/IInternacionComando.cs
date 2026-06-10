using System.Threading.Tasks;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Interfaces.IComandos
{
    public interface IInternacionComando
    {
        Task AgregarAsync(Internacion internacion);
        Task AgregarInternacionCamaAsync(InternacionCama internacionCama);
        Task GuardarCambiosAsync();
    }
}