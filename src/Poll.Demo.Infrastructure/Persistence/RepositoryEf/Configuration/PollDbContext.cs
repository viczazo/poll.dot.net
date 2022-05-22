using Microsoft.EntityFrameworkCore;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

public class PollDbContext : DbContext
{
    public PollDbContext(DbContextOptions<PollDbContext> options)
        : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("PollDb");
        //optionsBuilder.UseSqlServer("Data Source=tcp:localhost,1433;uid=YOURUSER;pwd=YOURPASSWORD; Initial Catalog=YOURDBNAME;MultipleActiveResultSets=True");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PollDbContext).Assembly);
    }
}