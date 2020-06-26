using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Server.Extensions;
using TicTacToe.Server.Domain;

namespace TicTacToe.Server.Repositories.EntityTypeConfigurations
{
	public class PlayfieldStateEntityTypeConfiguration : IEntityTypeConfiguration<PlayfieldState>
	{
		public void Configure(EntityTypeBuilder<PlayfieldState> builder)
		{
			builder.ToTable("PlayfieldState");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnName("Id")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.State)
				.HasColumnName("State")
				.HasJsonConversion()
				.IsRequired();

			builder.Property(x => x.Options)
				.HasColumnName("Options")
				.HasJsonConversion()
				.IsRequired();
		}
	}
}
