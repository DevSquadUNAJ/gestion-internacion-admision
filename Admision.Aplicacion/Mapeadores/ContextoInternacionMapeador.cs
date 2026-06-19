using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.Mapeadores
{
    public class ContextoInternacionMapeador : IContextoInternacionMapeador
    {
        public ContextoInternacionRespuesta Mapear(Internacion internacion, InternacionCama asignacionCama)
        {
            return new ContextoInternacionRespuesta
            {
                PacienteId = internacion.PacienteId,
                NombrePaciente = internacion.Paciente?.Nombre ?? "Desconocido",
                CamaId = asignacionCama.CamaId,
                NumeroCama = asignacionCama.Cama?.Numero ?? 0,
                SectorId = asignacionCama.Cama?.SectorId ?? System.Guid.Empty,
                NombreSector = asignacionCama.Cama?.Sector?.Nombre ?? "Desconocido"
            };
        }
    }
}