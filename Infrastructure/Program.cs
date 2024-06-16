using Application.Interfaces.Repositories;
using Application.Mapping;
using Application.Services;
using Infrastructure.Dal.EntityFramework;
using Infrastructure.Dal.Repositories;
using Infrastructure.Jobs;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var connectionString = builder.Configuration.GetConnectionString("TelegramBotDatabase");
Console.WriteLine(connectionString);
builder.Services.AddDbContext<TelegramBotDbContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddAutoMapper(typeof(PersonMappingProfile));
builder.Services.AddScoped<PersonService>();

builder.Services.Configure<CronSettings>(builder.Configuration.GetSection(nameof(CronSettings)));
builder.Services.Configure<TelegramBotSettings>(builder.Configuration.GetSection(nameof(TelegramBotSettings)));

builder.Services.AddQuartz(q =>
{
    var cronExpression = builder.Configuration.GetSection("CronExpressions").Get<CronSettings>();
    
    q.AddJob<BirthDayJob>(opts => opts.WithIdentity(nameof(BirthDayJob)));
    q.AddTrigger(opts => opts
        .ForJob("BirthDayJob")
        .WithCronSchedule(cronExpression.BirthDayJob));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();