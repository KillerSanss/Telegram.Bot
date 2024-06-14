using Application.Interfaces.Repositories;
using Application.Mapping;
using Application.Services;
using Infrastructure.Dal.EntityFramework;
using Infrastructure.Dal.Repositories;
using Infrastructure.Jobs;
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

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    
    q.AddJob<BirthDayJob>(opts => opts.WithIdentity("BirthDayJob"));
    q.AddTrigger(opts => opts
        .ForJob("BirthDayJob")
        .WithIdentity("BirthDayJobTrigger")
        .WithCronSchedule("0/5 * * * * ?"));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();