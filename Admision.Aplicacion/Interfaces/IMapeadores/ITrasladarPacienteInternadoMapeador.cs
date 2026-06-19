using Admision.Aplicacion.DTOs.Respuestas;
using System;

namespace Admision.Aplicacion.Interfaces.IMapeadores
{
    public interface ITrasladarPacienteInternadoMapeador
    {
        TrasladarPacienteInternadoRespuesta Mapear(Guid internacionId, Guid camaAnteriorId, Guid camaNuevaId, DateTime fechaTraslado);
    }
}