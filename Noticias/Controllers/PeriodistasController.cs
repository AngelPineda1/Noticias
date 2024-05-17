using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noticias.Models.DTOs;
using Noticias.Models.Entities;
using Noticias.Repositories;

namespace Noticias.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodistasController : ControllerBase
    {
        private readonly IRepository<Usuarios> _usuariosRepository;
        private readonly IMapper mapper;

        public PeriodistasController(IRepository<Usuarios> repository,IMapper mapper)
        {
            _usuariosRepository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var datos=_usuariosRepository.GetAll().Where(x=>x.EsAdmin==false).OrderBy(x=>x.Nombre)
                .Select(x=>mapper.Map<PeriodistaDto>(x));

            return Ok(datos);
        }

        [HttpPost]
        public IActionResult Agregar(Periodista2Dto dto)
        {

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var per=_usuariosRepository.Get(id);
            if (per == null)
            {
                return NotFound();
            }
            _usuariosRepository.Delete(per);
            return Ok();
        }
    }
}
