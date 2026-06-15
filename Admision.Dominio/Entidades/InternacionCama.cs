using System;

namespace Admision.Dominio.Entidades
{
    public class InternacionCama
    {
        public Guid Id { get; set; }
        public Guid InternacionId { get; set; }
        public Guid CamaId { get; set; }
        public DateTime FechaIngresoCama { get; set; }
        public DateTime? FechaSalidaCama { get; set; }
        public bool EsActual { get; set; }
        public string? MotivoTraslado { get; set; }

        public virtual Internacion Internacion { get; set; }
        public virtual Cama Cama { get; set; }
    }
}