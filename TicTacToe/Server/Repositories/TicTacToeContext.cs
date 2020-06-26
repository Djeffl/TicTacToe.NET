using Microsoft.EntityFrameworkCore;
using TicTacToe.Server.Domain;
using TicTacToe.Server.Repositories.EntityTypeConfigurations;

namespace TicTacToe.Server.Repositories
{
	public class TicTacToeContext : DbContext
	{
		public TicTacToeContext(DbContextOptions<TicTacToeContext> options) :
			base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new PlayfieldStateEntityTypeConfiguration());
		}

		public DbSet<PlayfieldState> PlayfieldStates { get; set; }
	}
}
