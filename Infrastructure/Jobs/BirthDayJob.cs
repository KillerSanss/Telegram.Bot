using Domain.Primitives;
using Infrastructure.Dal.EntityFramework;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.Jobs;

/// <summary>
/// Джоба для вывода всех персон у которых сегодня день рождения
/// </summary>
public class BirthDayJob : IJob
{
    private readonly TelegramBotDbContext _telegramBotDbContext;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly TelegramBotSettings _telegramBotSettings;
    private readonly ILogger<BirthDayJob> _logger;
        
    public BirthDayJob(
        TelegramBotDbContext telegramBotDbContext,
        IOptions<TelegramBotSettings> telegramBotSettings,
        ILogger<BirthDayJob> logger)
    {
        _telegramBotDbContext = telegramBotDbContext;
        _logger = logger;
        _telegramBotSettings = telegramBotSettings.Value;
        _telegramBotClient = new TelegramBotClient(_telegramBotSettings.BotToken);
    }
        
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var persons = await _telegramBotDbContext.Persons
                .Where(p => p.BirthDate.Month == DateTime.Today.Month && p.BirthDate.Day == DateTime.Today.Day)
                .ToListAsync();

            foreach (var person in persons)
            {
                var message = string.Format(Messages.BirthDayMessage,
                    $"{person.FullName.FirstName} {person.FullName.LastName}", $"{person.BirthDate:dd/MM}");
                _logger.LogInformation(message);
                await _telegramBotClient.SendTextMessageAsync(_telegramBotSettings.ChatId, message);
                var stickerId = "CAACAgIAAxkBAAEGJq1mbG3I28USRfEvbJWpqWIkCvOzTAACyRgAAt1iOUlm4vcITBKrajUE";
                await _telegramBotClient.SendStickerAsync(_telegramBotSettings.ChatId, new InputFileId(stickerId));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при выполнении задачи BirthDayJob");
        }
    }
}