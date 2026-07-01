using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using System;

namespace Admision.Aplicacion.Mapeadores
{
    public class TrasladarPacienteInternadoMapeador : ITrasladarPacienteInternadoMapeador
    {
        public TrasladarPacienteInternadoRespuesta Mapear(Guid internacionId, Guid camaAnteriorId, Guid camaNuevaId, DateTime fechaTraslado)
        {
            return new TrasladarPacienteInternadoRespuesta
            {
                InternacionId = internacionId,
                CamaAnteriorId = camaAnteriorId,
                CamaNuevaId = camaNuevaId,
                FechaTraslado = fechaTraslado
            };
        }
    }
}