using BerryessaUnion.IServices.JwtServices;
using BerryessaUnion.IServices.RepositoryConfig;
using BerryessaUnion.IServices.UserSetup;
using BerryessaUnion.Managers.JwtManager;
using BerryessaUnion.Managers.RepositoryConfig;
using BerryessaUnion.Managers.UserSetup;
using Busd_Backend.Extensions;
using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedLibrary.JwtModel;
using SharedLibrary.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOptions<BearerTokensOptions>()
                                .Bind(builder.Configuration.GetSection("BearerTokens"))
                                .Validate(bearerTokens =>
                                {
                                    return bearerTokens.AccessTokenExpirationMinutes < bearerTokens.RefreshTokenExpirationMinutes;
                                }, "RefreshTokenExpirationMinutes is less than AccessTokenExpirationMinutes. Obtaining new tokens using the refresh token should happen only if the access token has expired.");
builder.Services.AddOptions<ApiSettings>()
        .Bind(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<EmailConfigurationOption>(builder.Configuration.GetSection("EmailConfiguration"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString,
            serverDbContextOptionsBuilder =>
            {
                var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                serverDbContextOptionsBuilder.CommandTimeout(minutes);
                serverDbContextOptionsBuilder.EnableRetryOnFailure();
            });
});
builder.Services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddSingleton(provider => MapperFactory.CreateMapper());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieManager>();
builder.Services.AddScoped<IUnitOfWork, ApplicationDbContext>();
builder.Services.AddScoped<IUsersService, UsersManager>();
builder.Services.AddScoped<IRoleService, RoleManager>();
builder.Services.AddScoped<IRepositoryWrapperService, RepositoryWrapperManager>();
builder.Services.AddSingleton<ISecurityService, SecurityManager>();
builder.Services.AddScoped<ITokenFactoryService, TokenFactoryManager>();
builder.Services.AddScoped<IUsersRepoService, UsersRepoManager>();
builder.Services.AddScoped<ITokenStoreService, TokenStoreManager>();
builder.Services.AddScoped<ITokenValidatorService, TokenValidatorManger>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc(
       name: "LibraryOpenAPISpecification",
       info: new Microsoft.OpenApi.Models.OpenApiInfo()
       {
           Title = "BUSD",
           Version = "1",
           Description = "Through this API you can access the site's capabilities.",
           Contact = new Microsoft.OpenApi.Models.OpenApiContact()
           {
               Email = "name@site.com",
               Name = "DNT",
               Url = new Uri("http://www.dotnettips.info")
           },
           License = new Microsoft.OpenApi.Models.OpenApiLicense()
           {
               Name = "MIT License",
               Url = new Uri("https://opensource.org/licenses/MIT")
           }
       });

    //var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    //xmlFiles.ForEach(xmlFile => setupAction.IncludeXmlComments(xmlFile));

    setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Description = "Put **_ONLY_** your JWT Bearer token on text box below!",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin() //Note:  The URL must be specified without a trailing slash (/).
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed((host) => true)
            //.AllowCredentials()
            .WithExposedHeaders("X-Pagination"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

})
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.IncludeErrorDetails = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                        //ValidAudience = configuration["BearerTokens:Audience"], // site that consumes the token
                        ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokens:Key"])),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration dates
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("Authentication failed.", context.Exception);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                            return tokenValidatorService.ValidateAsync(context);
                        },
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                            return Task.CompletedTask;
                        }
                    };
                });

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    setupAction.SwaggerEndpoint(
        url: "/swagger/LibraryOpenAPISpecification/swagger.json",
        name: "BUSD");
    //setupAction.RoutePrefix = "swagger/index.html";
    //--> To be able to access it from this URL: https://localhost:5001/swagger/index.html
    setupAction.DefaultModelExpandDepth(2);
    setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
    setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    setupAction.EnableDeepLinking();
    setupAction.DisplayOperationId();
});// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
