using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneVOne.GameService.Core.Entities;

namespace OneVOne.GameService.Infrastructure.Configs
{
    internal class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            // set Id column as required (not nullable)
            builder.Property(p => p.Id).IsRequired();

            // set all other columns as optional (nullable)
            builder.Property(p => p.Position).IsRequired(false);
            builder.Property(p => p.OutsideScoring).IsRequired(false);
            builder.Property(p => p.InsideScoring).IsRequired(false);
            builder.Property(p => p.Defending).IsRequired(false);
            builder.Property(p => p.Athleticism).IsRequired(false);
            builder.Property(p => p.Playmaking).IsRequired(false);
            builder.Property(p => p.Rebounding).IsRequired(false);
            builder.Property(p => p.IsAttacker).IsRequired();
            builder.Property(p => p.PersonId).IsRequired(false);
            builder.Property(p => p.TeamId).IsRequired(false);

            // specify foreign key relationship
            builder.HasOne(p => p.Person)
                   .WithOne()
                   .HasForeignKey<Player>(p => p.PersonId);
        }
    }
}
