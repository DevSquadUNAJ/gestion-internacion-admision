namespace Admision.Aplicacion.Excepciones
{
    public class EstadoEgresoInvalidoException : ExcepcionDeNegocio
    {
        public EstadoEgresoInvalidoException(string estadoSolicitado)
            : base($"El estado '{estadoSolicitado}' no es un estado de egreso válido. " +
                   $"Estados permitidos: AltaMedica, Traslado, Defuncion.")
        { }
    }
}