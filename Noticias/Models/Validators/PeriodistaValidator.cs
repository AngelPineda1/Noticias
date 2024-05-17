using FluentValidation;
using Noticias.Models.DTOs;
using Noticias.Models.Entities;
using Noticias.Repositories;

namespace Noticias.Models.Validators
{
    public class PeriodistaValidator:AbstractValidator<Periodista2Dto>
    {
        private readonly IRepository<Usuarios> repository;

        public PeriodistaValidator(IRepository<Usuarios> repository)
        {
            this.repository = repository;
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe escribir el nombre real del periodista");
            RuleFor(x => x.NombreUsuario).NotEmpty().WithMessage("Debe escribir el nombre de usuario del periodista");
            RuleFor(x => x.Contraseña).NotEmpty().WithMessage("Debe escribir la password del periodista");
            RuleFor(x => x.Contraseña).MinimumLength(5).WithMessage("La pass debe tener al menos 5 caracteres")
                .Matches("[A-Z]").WithMessage("La pass debe teber al menos una letra mayuscula");

            RuleFor(x => x).Must(NoExiste).WithMessage("Ya existe un periodista con el mismo nombre de usuario");

        }

        private bool NoExiste(Periodista2Dto dto)
        {
            return !repository.GetAll().Any(x=>x.NombreUsuario==dto.NombreUsuario &&x.Id!=dto.Id);
            
        }


    }
}
