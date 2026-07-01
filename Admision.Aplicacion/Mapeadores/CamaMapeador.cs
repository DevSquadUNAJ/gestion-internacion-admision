using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Admision.Aplicacion.Mapeadores
{
    public class CamaMapeador : ICamaMapeador
    {
        public IEnumerable<DetalleCamaRespuesta> Mapear(IEnumerable<Cama> camas)
        {
            return camas.Select(c => {
                var internacionActual = c.HistorialInternaciones.FirstOrDefault(hi => hi.EsActual)?.Internacion;

                return new DetalleCamaRespuesta
                {
                    CamaId = c.Id,
                    Numero = c.Numero,
                    Estado = c.Estado,
                    PacienteId = internacionActual?.Paciente?.Id,
                    PacienteAsignado = internacionActual?.Paciente?.Nombre,
                    InternacionId = internacionActual?.Id
                };
            }).OrderBy(c => c.Numero);
        }
    }
}