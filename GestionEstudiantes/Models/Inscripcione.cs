using System;
using System.Collections.Generic;

namespace GestionEstudiantes.Models;

public partial class Inscripcione
{
    public int InscripcionId { get; set; }

    public int EstudianteId { get; set; }

    public int CursoId { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
