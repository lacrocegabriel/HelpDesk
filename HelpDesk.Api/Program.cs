using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Services;
using HelpDesk.Data.Context;
using HelpDesk.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using HelpDesk.Business.Interfaces;
using HelpDesk.Business.Notificacoes;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<HelpDeskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<HelpDeskContext>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IGerenciadorRepository, GerenciadorRepository>();
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGerenciadorService, GerenciadorService>();
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<INotificador, Notificador>();


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
