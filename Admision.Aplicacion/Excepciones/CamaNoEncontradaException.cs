using System;

namespace Admision.Aplicacion.Excepciones
{
    public class CamaNoEncontradaException : ExcepcionDeNegocio
    {
        public CamaNoEncontradaException(Guid camaId)
            : base($"No se encontró la cama con Id {camaId}.") { }
    }
}