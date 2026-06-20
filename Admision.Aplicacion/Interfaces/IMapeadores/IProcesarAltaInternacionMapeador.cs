using System;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Interfaces.IMapeadores
{
    public interface IProcesarAltaInternacionMapeador
    {
        ProcesarAltaInternacionRespuesta Mapear(Internacion internacion, Guid? camaLiberadaId);
    }
}