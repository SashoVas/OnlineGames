using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineGamesDbContext>(options => options.UseSqlServer(connectionString));
var appSettigns = builder.ConfigureAppSettings();
builder.AddIdentity();
builder.AddAuthenticationWithJWT(appSettigns);
//builder.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.AllowAnyOrigin()  
//              .AllowAnyMethod()  
//              .AllowAnyHeader(); 
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        {
            return origin != null && (origin.StartsWith("http://localhost") || origin.StartsWith("https://localhost") || origin.StartsWith("http://127.0.0.1") || origin.StartsWith("https://127.0.0.1"));
        })
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.AddServices();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
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
//app.UseCors("CorsPolicy");
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.AddHubs();
app.MapControllers();
app.Run();