using AutoMapper;
using TelaAzul.Models;
using Entidades;

namespace TelaAzul
{
    public class AutoMapperConfig : Profile
    {
        public static MapperConfiguration RegisterMappings() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Mapear Entidades e Models
                // cfg.CreateMap<entidade,model>();
                // cfg.CreateMap<model, entidade>();

                cfg.CreateMap<Genero, GeneroModel>();
                cfg.CreateMap<GeneroModel, Genero>();
            });
            return config;
        }
    }
}
