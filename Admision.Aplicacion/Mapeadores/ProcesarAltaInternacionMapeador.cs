using System;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Mapeadores
{
    public class ProcesarAltaInternacionMapeador : IProcesarAltaInternacionMapeador
    {
        public ProcesarAltaInternacionRespuesta Mapear(Internacion internacion, Guid? camaLiberadaId)
        {
            return new ProcesarAltaInternacionRespuesta
            {
                InternacionId = internacion.Id,
                EstadoEgreso = internacion.Estado,
                FechaEgreso = internacion.FechaEgreso ?? DateTime.UtcNow,
                CamaLiberadaId = camaLiberadaId
            };
        }
    }
}