using AvaliacaoAtendimentoCSAT5API.Models;
using AvaliacaoAtendimentoCSAT5API.Services;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;

internal class Program
{ 

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<AvaliacaoCSATDatabaseSettings>(
            builder.Configuration.GetSection("AvaliacaoCSATDatabase"));

        builder.Services.AddSingleton<ICSATService, CSATService>();
        
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapControllers();

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        app.Run();

    }
}