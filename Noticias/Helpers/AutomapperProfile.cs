using AutoMapper;
using Noticias.Models.DTOs;
using Noticias.Models.Entities;

namespace Noticias.Helpers
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Usuarios, PeriodistaDto>();
            CreateMap<Periodista2Dto, Usuarios>();
            
        }
    }
}
