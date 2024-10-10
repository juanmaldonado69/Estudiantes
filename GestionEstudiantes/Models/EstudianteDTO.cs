namespace GestionEstudiantes.Models
{
    public class EstudianteDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Estado { get; set; } = null!;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
