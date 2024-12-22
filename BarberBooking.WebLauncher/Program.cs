using System.Diagnostics;
using BarberBooking.DataAccess.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

if (OperatingSystem.IsWindows())
{
    var eventLogConfig = configuration.GetSection("Logging:EventLog");
    var logName = eventLogConfig["LogName"] ?? "Application";
    var sourceName = eventLogConfig["SourceName"] ?? "YourAppSource";

    if (!EventLog.SourceExists(sourceName))
    {
        EventLog.CreateEventSource(new EventSourceCreationData(sourceName, logName));
    }

    builder.Logging.ClearProviders();
    builder.Logging.AddEventLog(new EventLogSettings
    {
        LogName = logName,
        SourceName = sourceName
    });
}
else
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
}

builder.Services.AddDbContext<BarberDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("barber")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAnyOrigin");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BarberDbContext>();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Connection to database successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

app.Run();