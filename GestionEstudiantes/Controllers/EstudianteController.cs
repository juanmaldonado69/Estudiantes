using GestionEstudiantes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly GestionEstudiantesContext _dbEstudiante;

        public EstudianteController(GestionEstudiantesContext gestionEstudiantesContext)
        {
            _dbEstudiante = gestionEstudiantesContext;
        }
        [HttpGet]
        [Route("ListaEstudiantes")]

        public async Task<IActionResult> Lista()
        {
            var listaEstudiantes = await _dbEstudiante.Estudiantes.ToListAsync();
            return Ok(listaEstudiantes);
        }
       

        [HttpPost]
        [Route("AgregarEstudiante")]
        
        public async Task<IActionResult> AgregarEstudiante([FromBody] EstudianteDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estudiante = new Estudiante
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                FechaRegistro = DateTime.Now, // Asignar la fecha de registro actual
                Estado = request.Estado,
                Email = request.Email,
                Telefono = request.Telefono
            };

            try
            {
                await _dbEstudiante.Estudiantes.AddAsync(estudiante);
                await _dbEstudiante.SaveChangesAsync();
                return CreatedAtAction(nameof(ObtenerPorId), new { id = estudiante.EstudianteId }, estudiante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar los datos: " + ex.Message);
            }
        }

        [Route("Obtener/{id}")]
        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var estudiante = await _dbEstudiante.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el estudiante
            }

            return Ok(estudiante); // Retorna 200 con los datos del estudiante
        }

        [HttpPut]
        [Route("ActualizarEstudiante/{id}")]
        
        public async Task<IActionResult> ActualizarEstudiante(int id, [FromBody] EstudianteDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estudiante = await _dbEstudiante.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el estudiante
            }

            // Actualizar los campos del estudiante
            estudiante.Nombre = request.Nombre;
            estudiante.Apellido = request.Apellido;
            estudiante.FechaNacimiento = request.FechaNacimiento;
            estudiante.Estado = request.Estado;
            estudiante.Email = request.Email;
            estudiante.Telefono = request.Telefono;

            try
            {
                await _dbEstudiante.SaveChangesAsync(); // Guarda los cambios
                return Ok(estudiante); // Retorna el estudiante actualizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar los datos: " + ex.Message);
            }
        }

        

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var estudiante = await _dbEstudiante.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _dbEstudiante.Estudiantes.Remove(estudiante);
            await _dbEstudiante.SaveChangesAsync();
            return NoContent(); // Devuelve un código 204 sin contenido
        }

        [HttpGet]
        [Route("ObtenerPaginado")]
       
        public async Task<IActionResult> ObtenerTodos(int pagina = 1, int tamanioPagina = 10)
        {
            // Validar la página y el tamaño de la página
            if (pagina < 1 || tamanioPagina < 1)
            {
                return BadRequest("La página y el tamaño de la página deben ser mayores que cero.");
            }

            // Obtener la lista de estudiantes y aplicar la paginación
            var totalEstudiantes = await _dbEstudiante.Estudiantes.CountAsync();
            var estudiantes = await _dbEstudiante.Estudiantes
                .Skip((pagina - 1) * tamanioPagina) // Salta los estudiantes de las páginas anteriores
                .Take(tamanioPagina) // Toma el tamaño de la página
                .ToListAsync();

            // Crear el objeto de respuesta
            var resultado = new
            {
                TotalEstudiantes = totalEstudiantes,
                PaginaActual = pagina,
                TamanioPagina = tamanioPagina,
                TotalPaginas = (int)Math.Ceiling((double)totalEstudiantes / tamanioPagina),
                Estudiantes = estudiantes
            };

            return Ok(resultado); // Retorna la respuesta paginada
        }

    }
}
