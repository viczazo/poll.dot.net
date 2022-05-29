using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

public class VoterEntityTypeConfiguration : IEntityTypeConfiguration<Voter>
{
    public void Configure(EntityTypeBuilder<Voter> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(b => b.NationalIdentity);
        builder.Property(x => x.FirstName);
        builder.Property(x => x.LastName);
        builder.HasOne(v => v.Voting)
            .WithMany(x => x.Voters);
        builder.HasOne(v => v.Vote)
            .WithOne(x => x.Voter)
            .HasForeignKey<Vote>();
        builder.Navigation(nameof(Voter.Vote));
        builder.HasIndex(x => x.NationalIdentity)
            .IsUnique();
    }
}