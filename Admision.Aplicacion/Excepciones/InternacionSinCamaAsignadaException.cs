using System;

namespace Admision.Aplicacion.Excepciones
{
    public class InternacionSinCamaAsignadaException : ExcepcionDeNegocio
    {
        public InternacionSinCamaAsignadaException(Guid internacionId)
            : base($"La internación con Id {internacionId} no tiene una cama actualmente asignada. " +
                   $"Use el caso de uso de asignación de cama en lugar de traslado.")
        { }
    }
}