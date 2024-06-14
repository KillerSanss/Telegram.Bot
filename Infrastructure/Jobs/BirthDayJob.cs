using Infrastructure.Dal.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Infrastructure.Jobs;

/// <summary>
/// Джоба для вывода всех персон у которых сегодня день рождения
/// </summary>
public class BirthDayJob : IJob
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BirthDayJob> _logger;
    
    public BirthDayJob(IServiceProvider serviceProvider, ILogger<BirthDayJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TelegramBotDbContext>();

        var todayMonth = DateTime.Today.Month;
        var todayDay = DateTime.Today.Day;
        var persons = await dbContext.Persons
            .Where(p => p.BirthDate.Month == todayMonth
                        && p.BirthDate.Day == todayDay)
            .ToListAsync();

        foreach (var person in persons)
        {
            _logger.LogInformation($"Сегодня день рождения у {person.FullName.FirstName} {person.FullName.LastName} {person.FullName.MiddleName} ({person.BirthDate:dd/MM/yyyy})");
        }
    }
}