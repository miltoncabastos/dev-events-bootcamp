using Dapper;
using DevEvents.API.Entidades;
using DevEvents.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly DevEventsDbContext _devEventsDbContext;

        public EventosController(DevEventsDbContext dbContext)
        {
            _devEventsDbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ObterEventos()
        {
            var eventos = _devEventsDbContext.Eventos.ToList();

            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterEvento(int id)
        {
            var evento = _devEventsDbContext.Eventos.SingleOrDefault(e => e.Id == id);

            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Evento evento)
        {
            _devEventsDbContext.Eventos.Add(evento);
            _devEventsDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Evento evento)
        {
            var eventoLocal = _devEventsDbContext.Eventos.SingleOrDefault(e => e.Id == evento.Id);
            if (eventoLocal == null) return NotFound();

            _devEventsDbContext.Eventos.Update(evento);
            _devEventsDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancelar(int id)
        {
            //var evento = _devEventsDbContext.Eventos.SingleOrDefault(e => e.Id == id);

            //if (evento == null) return NotFound();

            //evento.Ativo = false;
            //_devEventsDbContext.Eventos.Update(evento);
            //_devEventsDbContext.SaveChanges();

            var connectionString = _devEventsDbContext.Database.GetDbConnection().ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var script = "UPDATE Evento SET Ativo = 0 WHERE Id = @id";
                sqlConnection.Execute(script, new { id });
            }

            return Ok();
        }
    }
}
