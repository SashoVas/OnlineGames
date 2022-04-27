using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services;
using OnlineGames.Services.Contracts;
using OnlineGames.Web.Hubs;
using OnlineGames.Web.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineGamesDbContext>(options =>
    options.UseSqlServer(connectionString));
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<OnlineGamesDbContext>()
    .AddDefaultTokenProviders();

var appSettigns = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettigns.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Query.TryGetValue("token", out StringValues token)
            )
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            var te = context.Exception;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options=>
{
    options.AddPolicy("CorsPolicy", builder => builder
     .WithOrigins("http://localhost:4200")
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

builder.Services.AddTransient<ITicTacToeService, TicTacToeService>();
builder.Services.AddTransient<IConnect4Service, Connect4Service>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IRoomService, RoomService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
using (var serviceScope = app.Services.CreateScope())//app.ApplicationServices.CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetRequiredService<OnlineGamesDbContext>())
    {
        context.Database.EnsureCreated();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<TicTacToeHub>("/TicTacToe");
app.MapHub<Connect4Hub>("/Connect4");
app.MapControllers();

app.Run();
