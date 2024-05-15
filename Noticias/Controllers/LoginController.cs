using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noticias.Helpers;
using Noticias.Models.DTOs;
using Noticias.Models.Entities;
using Noticias.Repositories;
using System.Security.Claims;

namespace Noticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<Usuarios> repository;
        private readonly JwtHelper jwtHelper;

        public LoginController(IRepository<Usuarios> repo, JwtHelper jwtHelper)
        {
            repository = repo;
            this.jwtHelper = jwtHelper;
        }
        [HttpPost]
        public IActionResult Authentication(LoginDto login)
        {
            var us = repository.GetAll().FirstOrDefault(x => x.NombreUsuario == login.UserName && x.Contraseña == login.Password);
            if (us == null)
            {
                return Unauthorized();
            }
            var token = jwtHelper.GetToken(us.Nombre, us.EsAdmin == true ? "Admin" : "Periodista", new List<Claim>
            { new Claim("Id",us.Id.ToString())});
            return Ok(token);
        }
    }
}
