using Admision.Dominio.Constantes;
using System;
using System.Collections.Generic;

namespace Admision.Dominio.Entidades
{
    public class Internacion
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEgreso { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public EstadoInternacion Estado { get; set; } = EstadoInternacion.Activa;
        public virtual Paciente Paciente { get; set; }
        public virtual ICollection<InternacionCama> HistorialCamas { get; set; } = new List<InternacionCama>();
    }
}