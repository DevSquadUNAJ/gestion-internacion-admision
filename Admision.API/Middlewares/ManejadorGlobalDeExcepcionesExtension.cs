using Microsoft.AspNetCore.Builder;

namespace Admision.API.Middlewares
{
    public static class ManejadorGlobalDeExcepcionesExtension
    {
        public static IApplicationBuilder UsarManejadorGlobalDeExcepciones(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ManejadorGlobalDeExcepcionesMiddleware>();
        }
    }
}