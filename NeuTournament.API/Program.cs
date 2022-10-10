using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NeuTournament.API.CustomExceptionMiddleware;
using NeuTournament.Application.Services;
using NeuTournament.Application.Services.Interface;
using NeuTournament.Domain.Entities;
using NeuTournament.Infrastructure;
using NeuTournament.Infrastructure.Repositories;
using NeuTournament.Infrastructure.Repositories.Interface;

namespace NeuTournament.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
            builder.Services.AddScoped<IGenericRepository<Event>, GenericRepository<Event>>();
            builder.Services.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();
            builder.Services.AddScoped<IGenericRepository<Registration>, GenericRepository<Registration>>();
            builder.Services.AddScoped<IGenericRepository<TeamMember>, GenericRepository<TeamMember>>();
            builder.Services.AddDbContext<TournamentDBContext>(c =>
            {
                c.UseSqlServer(configuration["ConnectionStrings:TournamentDBContext"]).
                EnableSensitiveDataLogging();
            });
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyHeader();
                                      policy.WithOrigins("http://localhost:4200", "https://neutournament-ui.azurewebsites.net");
                                  });
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v2");
                });
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);
            app.MapControllers();
            app.Run();
        }
    }
}