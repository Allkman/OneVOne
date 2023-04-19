using Microsoft.EntityFrameworkCore;
using OneVOne.Infrastructure;
using OneVOne.Infrastructure.Services;
using OneVOne.Infrastructure.Services.Interfaces;
using OneVOne.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<UnitOfWorkOptions>(builder.Configuration.GetSection("UnitOfWork"));

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetSection("UnitOfWork:ConnectionString").Value));

builder.Services.AddScoped<UnitOfWork>().AddScoped<IUnitOfWork, UnitOfWork>(s => s.GetService<UnitOfWork>());

builder.Services.AddControllers();

builder.Services.AddScoped<GameService>().AddScoped<IGameService, GameService>(s => s.GetService<GameService>());
builder.Services.AddScoped<PersonService>().AddScoped<IPersonService, PersonService>(s => s.GetService<PersonService>());
builder.Services.AddScoped<PlayByPlayStatisticsService>().AddScoped<IPlayByPlayStatisticsService, PlayByPlayStatisticsService>(s => s.GetService<PlayByPlayStatisticsService>());
builder.Services.AddScoped<PlayerService>().AddScoped<IPlayerService, PlayerService>(s => s.GetService<PlayerService>());
builder.Services.AddScoped<PlayerImageService>().AddScoped<IPlayerImageService, PlayerImageService>(s => s.GetService<PlayerImageService>());
builder.Services.AddScoped<TeamService>().AddScoped<ITeamService, TeamService>(s => s.GetService<TeamService>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(_ => true)
               .AllowCredentials());

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();