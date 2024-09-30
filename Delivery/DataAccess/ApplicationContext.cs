using System;
using Delivery.Models;
using Microsoft.EntityFrameworkCore;

namespace Delivery.DataAccess;

public class ApplicationContext : DbContext
{
    private const string _port = "5432";
    private const string _host = "localhost";
    private const string _database = "delivery";
    private const string _username = "postgres";
    private const string _password = "postgres";

    private const string _databaseConnection =
        $"Host={_host};" +
        $"Port={_port};" +
        $"Database={_database};" +
        $"Username={_username};" +
        $"Password={_password}";

    public ApplicationContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    public DbSet<OrderViewModel> Orders { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseConnection);
    }
}
