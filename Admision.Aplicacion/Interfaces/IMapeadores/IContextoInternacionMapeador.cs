using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Interfaces.IMapeadores
{
    public interface IContextoInternacionMapeador
    {
        ContextoInternacionRespuesta Mapear(Internacion internacion, InternacionCama asignacionCama);
    }
}