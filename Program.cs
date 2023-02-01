using BackEndProjetao.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// REFERENCIAR O CONTEXTO DO DB CONFIGURADO NA APLICA플O PARA QUE A API POSSA SE COMUNICAR COM A BASE DE DADOS
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ADICIONAR O SERVICE PARA O CORS(RECURSO) - COM O OBJETIVO DE "FACILITAR" O CRUZAMENTO DE DOMINIO ENTRE AS APLICA합ES FRONT E BACKEND
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", p =>
    {
        p.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// AQUI A APLICA플O FAR� USO DA CORS POLICY ESTABELECIDA ACIMA
app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
