using System;
using System.Collections.Generic;

namespace GestionEstudiantes.Models;

public partial class Calificacione
{
    public int CalificacionId { get; set; }

    public int EstudianteId { get; set; }

    public int CursoId { get; set; }

    public decimal? Calificacion { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
