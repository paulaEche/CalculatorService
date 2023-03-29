using CalculatorService.Server.Services;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSingleton<ILogging, Logging>()
    .AddScoped<IOperationService, AddService>()
    .AddScoped<IOperationService, SubtractService>()
    .AddScoped<IOperationService, MultiplyService>()
    .AddScoped<IOperationService, DivisionService>()
    .AddScoped<IOperationService, SquareRootService>()
    .AddSingleton<IJournalService, JournalService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<TrackIdHeader>();
;});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
