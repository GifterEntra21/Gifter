using BusinessLogicalLayer.Impl;
using BusinessLogicalLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Impl;
using DataAccessLayer.Interfaces;
using JwtAuthentication.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NeuralNetworkLayer.Impl;
using NeuralNetworkLayer.Interfaces;
using Shared.Settings;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using NeuralNetworkLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IUserBLL, UserBLL>();
builder.Services.AddTransient<IUserDAL, UserDAL>();

builder.Services.AddTransient<IProductBLL, ProductBLL>();
builder.Services.AddTransient<IProductDAL, ProductDAL>();

builder.Services.AddTransient<IWebScrapperBLL, WebScrapperBLL>();
builder.Services.AddTransient<IWebScrapperDAL, WebScrapperDAL>();

builder.Services.AddTransient<IRecommendationModel, RecommendationService>();

builder.Services.AddTransient<ICosmosDB>(opt =>
{
    string URL = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("URL");
    string primaryKey = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("PrimaryKey");

    var cosmosClient = new CosmosDb(URL,primaryKey);
    return cosmosClient;
});

builder.Services.AddTransient<IComputerVision>(opt =>
{
    string subscriptionKey = builder.Configuration.GetSection("ComputerVisionSettings").GetValue<string>("subscriptionKey");
    string endpoint = builder.Configuration.GetSection("ComputerVisionSettings").GetValue<string>("endpoint");
    var computerClient = new ComputerVision(subscriptionKey, endpoint);
    return computerClient;
});
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7008",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddDistributedRedisCache(opt =>
{
    opt.Configuration = builder.Configuration["AzureRedisConnection"];
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GifterWebApplication",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
AppSettings.IsDevelopingMode = app.Environment.IsDevelopment();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("EnableCORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
