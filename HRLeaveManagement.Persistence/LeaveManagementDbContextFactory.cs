using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HRLeaveManagement.Persistence;
public class LeaveManagementDbContextFactory : IDesignTimeDbContextFactory<LeaveManagmentDbContext>
{
    public LeaveManagmentDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<LeaveManagmentDbContext>();
        var connectionString = configuration.GetConnectionString("Default");

        builder.UseSqlServer(connectionString);

        return new LeaveManagmentDbContext(builder.Options);
    }
}