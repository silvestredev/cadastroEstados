using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EstadosController : ControllerBase
	{
		private readonly CadastroContext _context;

		public EstadosController(CadastroContext context)
		{
			_context = context;
		}

		// GET: api/Estados
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Estado>>> GetEstado()
		{
			if (_context.Estado == null)
			{
				return NotFound();
			}
			return await _context.Estado.ToListAsync();
		}

		// GET: api/Estados/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Estado>> GetEstado(string id)
		{
			if (_context.Estado == null)
			{
				return NotFound();
			}
			var estado = await _context.Estado.FindAsync(id);

			if (estado == null)
			{
				return NotFound();
			}

			return estado;
		}

		// PUT: api/Estados/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEstado(string id, Estado estado)
		{
			if (id != estado.Sigla)
			{
				return BadRequest();
			}

			_context.Entry(estado).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EstadoExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Estados
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Estado>> PostEstado(Estado estado)
		{
			if (_context.Estado == null)
			{
				return Problem("Entity set 'WebAPIContext.Estado'  is null.");
			}
			_context.Estado.Add(estado);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (EstadoExists(estado.Sigla))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetEstado", new { id = estado.Sigla }, estado);
		}

		// DELETE: api/Estados/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEstado(string id)
		{
			if (_context.Estado == null)
			{
				return NotFound();
			}
			var estado = await _context.Estado.FindAsync(id);
			if (estado == null)
			{
				return NotFound();
			}

			_context.Estado.Remove(estado);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool EstadoExists(string id)
		{
			return (_context.Estado?.Any(e => e.Sigla == id)).GetValueOrDefault();
		}
	}
}