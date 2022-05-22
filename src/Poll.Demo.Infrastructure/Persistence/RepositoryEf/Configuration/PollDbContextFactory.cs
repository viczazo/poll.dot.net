using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

public class PollDbContextFactory: IDesignTimeDbContextFactory<PollDbContext>
{
    public PollDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PollDbContext>();
        optionsBuilder.UseSqlServer("Data Source=tcp:localhost,1433;uid=YOURUSER;pwd=YOURPASSWORD; Initial Catalog=YOURDBNAME;MultipleActiveResultSets=True");

        return new PollDbContext(optionsBuilder.Options);
    }
}