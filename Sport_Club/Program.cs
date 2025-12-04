using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sport_Club.Interfaces;
using Sport_Club.Repositories;
using Sport_Club.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAutoMapper(typeof(Program));
// Repositories Registration
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
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

// ** JWT Authentication **
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();




// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();   // لازم قبل UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.Run();









//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Sport_Club.Data;
//using Sport_Club.Models;

//namespace Sport_Club
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add Controllers
//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            // Read Connection String
//            var conn = builder.Configuration.GetConnectionString("Conn");

//            // ? DbContext + Logging + NoTracking ???????
//            builder.Services.AddDbContext<AppDbContext>(options =>
//            {
//                options.UseSqlServer(conn);

//                // ??? ?? ???????? ?? ??? Console + Debug Output
//                options.LogTo(Console.WriteLine, LogLevel.Information)
//                       .EnableSensitiveDataLogging(); // ???? ???????? ???????

//                // ??? ?? Queries ????????? NoTracking
//                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//            });

//            // Identity
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<AppDbContext>()
//                .AddDefaultTokenProviders();

//            var app = builder.Build();

//            // Swagger
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseAuthorization();
//            app.MapControllers();
//            app.Run();
//        }
//    }
//}
