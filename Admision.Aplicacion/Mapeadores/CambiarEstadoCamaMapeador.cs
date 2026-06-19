using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using System;

namespace Admision.Aplicacion.Mapeadores
{
    public class CambiarEstadoCamaMapeador : ICambiarEstadoCamaMapeador
    {
        public CambiarEstadoCamaRespuesta Mapear(Cama cama, EstadoCama estadoAnterior, DateTime fechaCambio)
        {
            return new CambiarEstadoCamaRespuesta
            {
                CamaId = cama.Id,
                EstadoAnterior = estadoAnterior,
                EstadoNuevo = cama.Estado,
                FechaCambio = fechaCambio
            };
        }
    }
}