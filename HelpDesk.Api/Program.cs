using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Services;
using HelpDesk.Data.Context;
using HelpDesk.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Business.Validator.Notificacoes;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Validator.Validators;
using HelpDesk.Api.Configurations;
using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Api.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HelpDeskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

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
builder.Services.AddScoped<ITramiteRepository, TramiteRepository>();

builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGerenciadorService, GerenciadorService>();
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITramiteService, TramiteService>();

builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<IGerenciadorValidator, GerenciadorValidator>();
builder.Services.AddScoped<IClienteValidator, ClienteValidator>();
builder.Services.AddScoped<IUsuarioValidator, UsuarioValidator>();
builder.Services.AddScoped<ISetorValidator, SetorValidator>();
builder.Services.AddScoped<IEnderecoValidator, EnderecoValidator>();
builder.Services.AddScoped<IChamadoValidator, ChamadoValidator>();
builder.Services.AddScoped<ITramiteValidator, TramiteValidator>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUser, AspNetUser>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddTransient<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();
builder.Services.AddControllers();

builder.Services.AddSwaggerConfig();

var app = builder.Build();

app.UseAuthentication();

app.UseSwaggerConfig(provider);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
