using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WebApi.BLayer.Classes;
using WebApi.BLayer.Interfaces;
using WebApi.BLayer.WorkerDB.Classes;
using WebApi.BLayer.WorkerDB.Interfaces;
using WebApi.DLayer.Classes;
using WebApi.DLayer.Entity;
using WebApi.DLayer.Interfaces;
using WebApi.DLayer.WorkerDB;
using WebApi.DLayer.WorkerDB.Classes;
using WebApi.DLayer.WorkerDB.Interfaces;
using WebApi.Models.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityDBContext>();
builder.Services.AddScoped(typeof(IDataAccess<>), typeof(DataAccess<>));
builder.Services.AddScoped<IBusinessAccess, BusinessAccess>();
builder.Services.AddTransient<ITokenManager, Tokengeeration>();

//WokerDB
builder.Services.AddDbContext<WorkerDBContext>();
builder.Services.AddScoped(typeof(IWorkerDBDataAccess<>), typeof(WorkerDBDataAccess<>));
builder.Services.AddScoped<IWorkerDB_BusinessAccess, WorkerDBBusinessAccess>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





//JWT TokenConfig
var jwtConfig = builder.Configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();

builder.Services.AddSingleton(jwtConfig);

var secretKey = jwtConfig.Secret;
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey))
    };
});


//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api's", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
                });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
