using HelpDesk.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Services;
using HelpDesk.Domain.Validator.Notificacoes;
using HelpDesk.Domain.Validator.Validators;
using HelpDesk.Infrastructure.Data.Context;
using HelpDesk.Infrastructure.Data.Repository;
using HelpDesk.Services.Api.Configurations;
using HelpDesk.Services.Api.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using HelpDesk.Application.AppService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(provider =>
{
    provider
        .AddKissLog(options =>
        {
            options.Formatter = (FormatterArgs args) =>
            {
                if (args.Exception == null)
                    return args.DefaultValue;

                string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
            };
        });
});

builder.Services.AddHttpContextAccessor();

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

builder.Services.AddScoped<IChamadoAppService, ChamadoAppService>();
builder.Services.AddScoped<IClienteAppService, ClienteAppService>();
builder.Services.AddScoped<IGerenciadorAppService, GerenciadorAppService>();
builder.Services.AddScoped<ISetorAppService, SetorAppService>();
builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddScoped<ITramiteAppService, TramiteAppService>();

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwaggerConfig(provider);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseKissLogMiddleware(options => {
    options.Listeners.Add(new RequestLogsApiListener(new Application(
        builder.Configuration["KissLog.OrganizationId"],    //  "701695a7-aa15-425e-9320-7f8b9970957a"
        builder.Configuration["KissLog.ApplicationId"])     //  "52538ec5-8103-45a9-a55b-fd67dcd2e8d7"
    )
    {
        ApiUrl = builder.Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
    });
});

app.Run();
