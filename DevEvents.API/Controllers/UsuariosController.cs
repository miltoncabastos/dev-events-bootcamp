using DevEvents.API.Entidades;
using DevEvents.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevEvents.API.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {

        private readonly DevEventsDbContext _devEventsDbContext;

        public UsuariosController(DevEventsDbContext devEventsDbContext)
        {
            _devEventsDbContext = devEventsDbContext;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var usuarios = _devEventsDbContext.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {
            _devEventsDbContext.Usuarios.Add(usuario);
            _devEventsDbContext.SaveChanges();
            return Ok(usuario);
        }
    }
}
