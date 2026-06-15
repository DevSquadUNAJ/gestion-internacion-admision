namespace Admision.Aplicacion.Excepciones
{
    public class EstadoCamaInvalidoException : ExcepcionDeNegocio
    {
        public EstadoCamaInvalidoException(string estadoSolicitado)
            : base($"El estado '{estadoSolicitado}' no es un estado de cama válido.") { }
    }
}