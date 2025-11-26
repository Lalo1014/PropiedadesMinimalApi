using AutoMapper;
using PropiedadesMinimalApi.Modelo;
using PropiedadesMinimalApi.Modelo.Dtos;

namespace PropiedadesMinimalApi.Mapas
{
    public class ConfiguracionMapas : Profile
    {
        public ConfiguracionMapas()
        {
            CreateMap<Propiedad, CrearPropiedadDto>().ReverseMap();
            CreateMap<Propiedad, PropiedadDto>().ReverseMap();
        }
    }
}
