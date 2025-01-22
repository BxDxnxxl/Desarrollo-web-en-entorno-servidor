using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class BebidaController : ControllerBase
   {
    private static List<Bebida> bebidas = new List<Bebida>();

    private readonly IBebidaRepository _repository;

    public BebidaController(IBebidaRepository repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Bebida>>> GetBebidas()
        {
            var bebidas = await _repository.GetAllAsync();
            return Ok(bebidas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Bebida>> GetBebida(int id)
        {
            var bebida = await _repository.GetByIdAsync(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return Ok(bebida);
        }

        [HttpPost]
        public async Task<ActionResult<Bebida>> CreateBebida(Bebida bebida)
        {
            await _repository.AddAsync(bebida);
            return CreatedAtAction(nameof(GetBebida), new { id = bebida.Id }, bebida);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBebida(int id, Bebida bebidaActualizada)
        {
            var bebidaExistente = await _repository.GetByIdAsync(id);
            if (bebidaExistente == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            bebidaExistente.Nombre = bebidaActualizada.Nombre;
            bebidaExistente.Precio = bebidaActualizada.Precio;
            bebidaExistente.EsAlcoholica = bebidaActualizada.EsAlcoholica;

            await _repository.UpdateAsync(bebidaExistente);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteBebida(int id)
       {
           var bebida = await _repository.GetByIdAsync(id);
           if (bebida == null)
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