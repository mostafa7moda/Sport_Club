using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using Sport_Club.Repositories;
using Sport_Club.SeedAdmin;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers + Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter() // ✅ هذا يصلح API responses
        );
    });

builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration - يجب أن يكون هنا قبل builder.Build()
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sport Club API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Repositories Registration
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Connection String
var conn = builder.Configuration.GetConnectionString("Conn");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conn);
    options.LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sport Club API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();   // لازم قبل UseAuthorization
app.UseAuthorization();

app.MapControllers();

// Seed roles and admin - بعد بناء التطبيق
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await RoleSeeder.SeedRoles(roleManager);
    await AdminSeeder.SeedAdmin(userManager);
}

app.Run();




















//using AutoMapper;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Sport_Club.Interfaces;
//using Sport_Club.Repositories;
//using Sport_Club.SeedAdmin;
//using System.Text;
//using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);

//// Add Controllers + Swagger
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();



//builder.Services.AddAutoMapper(typeof(Program));
//// Repositories Registration
////builder.Services.AddScoped<ITrainerService, TrainerService>();
//builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
//builder.Services.AddScoped<IMemberRepository, MemberRepository>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


//// في Program.cs
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(
//            new JsonStringEnumConverter() // ✅ هذا يصلح API responses
//        );
//    });










//// Connection String
//var conn = builder.Configuration.GetConnectionString("Conn");

//// DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlServer(conn);
//    options.LogTo(Console.WriteLine, LogLevel.Information)
//           .EnableSensitiveDataLogging();
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});

//// Add Identity
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],
//        ValidAudience = builder.Configuration["JWT:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//    };
//});














//var app = builder.Build();

//using var scope = app.Services.CreateScope();
//var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//await RoleSeeder.SeedRoles(roleManager);
//await AdminSeeder.SeedAdmin(userManager);







//builder.Services.AddSwaggerGen(c =>
//{
//    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
//        Name = "Authorization",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] { }
//        }
//    });
//});




//// Swagger
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthentication();   // لازم قبل UseAuthorization
//app.UseAuthorization();

//app.MapControllers();
//app.Run();







