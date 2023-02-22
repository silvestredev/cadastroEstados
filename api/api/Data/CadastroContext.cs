using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
	public class CadastroContext : DbContext
	{
		public CadastroContext(DbContextOptions<CadastroContext> options)
		: base(options)
		{
		}

		public DbSet<Estado> Estado { get; set; }
	}
}