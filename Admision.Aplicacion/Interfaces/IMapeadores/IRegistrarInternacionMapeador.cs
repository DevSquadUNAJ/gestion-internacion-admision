using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Interfaces.IMapeadores
{
    public interface IRegistrarInternacionMapeador
    {
        RegistrarInternacionRespuesta Mapear(Internacion internacion, Cama? camaAsignada);
    }
}