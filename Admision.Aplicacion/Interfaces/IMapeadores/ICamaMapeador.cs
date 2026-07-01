using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Dominio.Entidades;
using System.Collections.Generic;

namespace Admision.Aplicacion.Interfaces.IMapeadores
{
    public interface ICamaMapeador
    {
        IEnumerable<DetalleCamaRespuesta> Mapear(IEnumerable<Cama> camas);
    }
}