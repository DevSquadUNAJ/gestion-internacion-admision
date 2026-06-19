using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using System;

namespace Admision.Aplicacion.Interfaces.IMapeadores
{
    public interface ICambiarEstadoCamaMapeador
    {
        CambiarEstadoCamaRespuesta Mapear(Cama cama, EstadoCama estadoAnterior, DateTime fechaCambio);
    }
}