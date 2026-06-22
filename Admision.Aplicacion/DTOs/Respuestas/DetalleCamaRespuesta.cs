using Admision.Dominio.Constantes;
using System;

namespace Admision.Aplicacion.DTOs.Respuestas
{
    public class DetalleCamaRespuesta
    {
        public Guid CamaId { get; set; }
        public int Numero { get; set; }
        public EstadoCama Estado { get; set; }
        public string? PacienteAsignado { get; set; }
        public Guid? InternacionId { get; set; }
    }
}