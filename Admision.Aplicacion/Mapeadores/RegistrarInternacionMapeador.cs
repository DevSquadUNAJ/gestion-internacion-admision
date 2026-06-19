using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Mapeadores
{
    public class RegistrarInternacionMapeador : IRegistrarInternacionMapeador
    {
        public RegistrarInternacionRespuesta Mapear(Internacion internacion, Cama? camaAsignada)
        {
            return new RegistrarInternacionRespuesta
            {
                InternacionId = internacion.Id,
                PacienteId = internacion.PacienteId,
                FechaIngreso = internacion.FechaIngreso,
                Estado = internacion.Estado,
                CamaAsignadaId = camaAsignada?.Id
            };
        }
    }
}