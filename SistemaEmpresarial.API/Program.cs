using Microsoft.EntityFrameworkCore;
using SistemaEmpresarial.BusinessLayer.Business;
using SistemaEmpresarial.Repository.Contexts;
using SistemaEmpresarial.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<AppDbContext>(Options => { Options.UseSqlServer(
    connectionString
    ); });
builder.Services.AddScoped<UnitWork>();
builder.Services.AddScoped<RegionBusiness>();
builder.Services.AddScoped<GeneralRepository>();
builder.Services.AddScoped<RegionRepository>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
