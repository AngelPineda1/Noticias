using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noticias.Models.Entities;
using Noticias.Repositories;

namespace Noticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodistasController : ControllerBase
    {
        private readonly IRepository<Usuarios> _usuariosRepository;
        public PeriodistasController(IRepository<Usuarios> repository)
        {
            _usuariosRepository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {

        }
    }
}
