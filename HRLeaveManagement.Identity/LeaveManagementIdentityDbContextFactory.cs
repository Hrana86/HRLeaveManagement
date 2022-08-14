using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HRLeaveManagement.Identity;
public class LeaveManagementIdentityDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementIdentityDbContext>
{
    public LeaveManagementIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<LeaveManagementIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultIdentity");

        builder.UseSqlServer(connectionString);

        return new LeaveManagementIdentityDbContext(builder.Options);
    }
}