using DevEvents.API.Entidades;
using DevEvents.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevEvents.API.Controllers
{
    [Route("api/[controller]")]
    public class InscricaoController : ControllerBase
    {
        private readonly DevEventsDbContext _devEventsDbContext;

        public InscricaoController(DevEventsDbContext devEventsDbContext)
        {
            _devEventsDbContext = devEventsDbContext;
        }

        [HttpGet]
        public IActionResult ObterTodasInscricoesPorEvento(int idEvento)
        {
            var inscricoes = _devEventsDbContext.Inscricoes
                .Where(i => i.IdEvento == idEvento)
                .ToList();

            return Ok(inscricoes);
        }

        [HttpPost]
        public IActionResult Inscricao([FromBody] Inscricao inscricao)
        {
            _devEventsDbContext.Inscricoes.Add(inscricao);
            _devEventsDbContext.SaveChanges();

            return Ok(inscricao);
        }
    }
}
