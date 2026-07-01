using Admision.Dominio.Constantes;

namespace Admision.Aplicacion.Excepciones
{
    public class CambioEstadoCamaNoPermitidoException : ExcepcionDeNegocio
    {
        public CambioEstadoCamaNoPermitidoException(EstadoCama estadoActual, EstadoCama estadoSolicitado)
            : base($"No se permite cambiar el estado de la cama de '{estadoActual}' a '{estadoSolicitado}'. " +
                   $"El estado 'Ocupada' solo se gestiona mediante el flujo de internación.")
        { }

        public CambioEstadoCamaNoPermitidoException(EstadoCama estado)
            : base($"La cama ya se encuentra en estado '{estado}'.")
        { }
    }
}