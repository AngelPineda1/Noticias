namespace Noticias.Models.DTOs
{
    public class PeriodistaDto
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null!;


        public string Nombre { get; set; } = null!;


        
    }
    public class Periodista2Dto : PeriodistaDto
    {
        public string Contraseña { get; set; } = null!;
    }
}
