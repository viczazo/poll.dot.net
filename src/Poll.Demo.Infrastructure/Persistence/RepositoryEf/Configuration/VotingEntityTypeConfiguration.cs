using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

public class VotingEntityTypeConfiguration : IEntityTypeConfiguration<Voting>
{
    public void Configure(EntityTypeBuilder<Voting> builder)
    {
        builder
            .HasKey(k => k.Id);
        builder.Property(x => x.Name);
        builder
            .Property(b => b.State);
    }
}