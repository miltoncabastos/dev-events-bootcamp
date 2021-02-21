using DevEvents.API.Entidades;
using DevEvents.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevEvents.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly DevEventsDbContext _devEventsDbContext;

        public CategoriasController(DevEventsDbContext devEventsDbContext)
        {
            _devEventsDbContext = devEventsDbContext;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var categorias = _devEventsDbContext.Categorias.ToList();
            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Categoria categoria)
        {
            var categoriaLocal = _devEventsDbContext
                .Categorias
                .SingleOrDefault(c => c.Descricao.ToLower() == categoria.Descricao.ToLower());

            if (categoria is not null)
                return NotFound("Categoria já cadastrada");

            _devEventsDbContext.Categorias.Add(categoria);
            _devEventsDbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Deletar(int id)
        {
            var categoria = _devEventsDbContext.Categorias
                .SingleOrDefault(c => c.Id == id);

            if (categoria is null) return NotFound();

            _devEventsDbContext.Categorias.Remove(categoria);
            _devEventsDbContext.SaveChanges();

            return Ok();
        }
    }
}
