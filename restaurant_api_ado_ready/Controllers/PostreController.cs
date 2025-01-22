using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PostreController : ControllerBase
   {
    private static List<Postre> postres = new List<Postre>();

    private readonly IPostreRepository _repository;

    public PostreController(IPostreRepository repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Postre>>> GetPostres()
        {
            var postres = await _repository.GetAllAsync();
            return Ok(postres);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Postre>> GetPostre(int id)
        {
            var postre = await _repository.GetByIdAsync(id);
            if (postre == null)
            {
                return NotFound();
            }
            return Ok(postre);
        }

        [HttpPost]
        public async Task<ActionResult<Postre>> CreatePostre(Postre postre)
        {
            await _repository.AddAsync(postre);
            return CreatedAtAction(nameof(GetPostre), new { id = postre.Id }, postre);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostre(int id, Postre postreActualizado)
        {
            var postreExistente = await _repository.GetByIdAsync(id);
            if (postreExistente == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            postreExistente.Nombre = postreActualizado.Nombre;
            postreExistente.Precio = postreActualizado.Precio;
            postreExistente.Calorias = postreActualizado.Calorias;

            await _repository.UpdateAsync(postreExistente);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePostre(int id)
       {
           var postre = await _repository.GetByIdAsync(id);
           if (postre == null)
           {
               return NotFound();
           }
           await _repository.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _repository.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}