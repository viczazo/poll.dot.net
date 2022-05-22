using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

public class VoteEntityTypeConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(k => k.Id);
        builder.HasOne(v => v.Voting);
        builder.HasOne(v => v.Voter)
            .WithOne(x => x.Vote)
            .HasForeignKey<Voter>();
        builder.Navigation(nameof(Vote.Voter));
        builder.Navigation(nameof(Vote.Voting));
        builder.Property(b => b.Type);
    }
}