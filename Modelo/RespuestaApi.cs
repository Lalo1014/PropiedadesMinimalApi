using System.Net;

namespace PropiedadesMinimalApi.Modelo
{
    public class RespuestaApi
    {

        public RespuestaApi()
        {
            Errores = new List<string>();
        }
        public bool Success { get; set; }
        public Object Resultado { get; set; }
        public HttpStatusCode CodigoEstado { get; set; }
        public List<string> Errores { get; set; }

    }
}
