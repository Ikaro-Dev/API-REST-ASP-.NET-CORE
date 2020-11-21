using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
	public class DataContext : DbContext
	{
		#region Construtores

		public DataContext(DbContextOptions<DataContext> options):base(options)
		{
		}

		#endregion

		#region Propriedades

		public DbSet<Ticket> Tickets { get; set; }

		#endregion

	}
}