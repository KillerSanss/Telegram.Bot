using Domain.Entities;
using Infrastructure.Dal.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.EntityFramework;

/// <summary>
/// Класс контекста
/// </summary>
public class TelegramBotDbContext : DbContext
{
    public DbSet<Person> Persons { get; init; }
    public DbSet<CustomField<string>> CustomFields { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(
            "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;");
    }
    
    /// <summary>
    /// Метод применения конфигураций
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CustomFieldConfiguration());
    }
}
