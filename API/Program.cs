using FluentValidation.AspNetCore;
using Scalar.AspNetCore;
using Serilog;
using System.Text;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http.Features;
using static System.Net.Mime.MediaTypeNames;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

            // Configure FluentValidation
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();// (config => config.RegisterValidatorsFromAssemblyContaining<Program>());

            // Configure Serilog
            builder.Host.UseSerilog((context, config) => config.WriteTo.Console());

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

            // Configure DbContext and Identity
            //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version")
                    );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                    var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //});

            builder.Services.AddAuthorization().AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // Add Dependency Injection for Repositories and Services
            //builder.Services.AddScoped<IMerchantService, MerchantService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddSingleton<IEmailSender, EmailSender>();

            var app = builder.Build();

            app.UseStatusCodePages(async statusCodeContext =>
            {
                // using static System.Net.Mime.MediaTypeNames;
                statusCodeContext.HttpContext.Response.ContentType = Text.Plain;

                await statusCodeContext.HttpContext.Response.WriteAsync($"Status Code Page: {statusCodeContext.HttpContext.Response.StatusCode}");
            });

            app.UseExceptionHandler();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                StatusCodeSelector = ex => ex switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                }
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");

                app.MapOpenApi();

                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("CreatxsBackend API - V1");
                    options.WithTheme(ScalarTheme.DeepSpace);
                    options.CustomCss = "* { font-family: 'Arial'; }";
                    options.WithModels(true);
                    options.WithSidebar(false);
                    options.WithLayout(ScalarLayout.Classic);
                    options.WithClientButton(true);
                    options.WithOperationSorter(OperationSorter.Method);
                    options.WithDotNetFlag(true);
                    options.OpenApiRoutePattern = "/openapi/{documentName}.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
