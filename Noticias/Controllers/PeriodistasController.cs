using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noticias.Models.DTOs;
using Noticias.Models.Entities;
using Noticias.Models.Validators;
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
        private PeriodistaValidator validations;
        public PeriodistasController(IRepository<Usuarios> repository,IMapper mapper)
        {
            _usuariosRepository = repository;
            this.mapper = mapper;
            validations = new(_usuariosRepository);
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
            var result=validations.Validate(dto);
            if(result.IsValid)
            {
                var user = mapper.Map<Usuarios>(dto);
                _usuariosRepository.Insert(user);
                return Ok(mapper.Map<PeriodistaDto>(user));
            }
            return BadRequest(result.Errors.Select(x=>x.ErrorMessage));
        }

        [HttpPut]
        public IActionResult Editar(Periodista2Dto dto)
        {
            var result = validations.Validate(dto);
            if (result.IsValid)
            {
                var per=_usuariosRepository.Get(dto.Id);
                if (per != null)
                {

                    per.NombreUsuario=dto.NombreUsuario;
                    per.Nombre=dto.Nombre;

                    _usuariosRepository.Update(per);
                    return Ok(mapper.Map<PeriodistaDto>(per));
                }
                else
                {
                    return NotFound();
                }
                
            }
            return BadRequest(result.Errors.Select(x => x.ErrorMessage));
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
